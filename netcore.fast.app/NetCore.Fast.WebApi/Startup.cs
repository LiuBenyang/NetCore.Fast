using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore.Fast.Infrastructure.AutoInject;
using NetCore.Fast.Infrastructure.AutoMapper;
using NetCore.Fast.Infrastructure.ExceptionFilter;
using NetCore.Fast.Infrastructure.ExceptionFilter.Dto;
using NetCore.Fast.Infrastructure.JwtTokenHelper;
using NetCore.Fast.Infrastructure.JwtTokenHelper.Dto;
using NetCore.Fast.Infrastructure.Repositories;
using NetCore.Fast.Infrastructure.UnitOfWork;
using NetCore.Fast.Utility.Cache;
using NetCore.Fast.Utility.ToExtensions;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Fast.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string AllowCorssOrigin = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region ͳһ��������ֵ��������֤���쳣��

            services.AddControllers(config =>
            {
                // ���ȫ��ģ����֤������
                config.Filters.Add<ValidateModelAttribute>();
                // ���ȫ���쳣���������
                config.Filters.Add<CustomExceptionAttribute>();
                // ���ͳһ�Ѻ÷��ع�����
                config.Filters.Add<ApiResultFilterAttribute>();
            }).ConfigureApiBehaviorOptions(opts =>
            {
                // �ر�����ģ����֤
                opts.SuppressModelStateInvalidFilter = true;
            });

            #endregion

            #region ��������
            //���ÿ���
            services.AddCors(option =>
            {
                option.AddPolicy(AllowCorssOrigin,
                builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
            #endregion

            #region UnitOfWork & Repository & AutoMapper & ҵ����� ע��

            // �Զ�����ӳ��
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper();
            services.AddAutoInject("NetCore.Fast.Services");

            #endregion

            #region Jwt ������֤����  �����¼��֤

            AppSettingsInit.JwtSettingInit(Configuration);

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOpts =>
            {
                jwtOpts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//��֤ȫ����Կ || Ĭ��True
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsInit.jwtSettings.SecretKey)),
                    ValidateIssuer = true,//��֤������ || Ĭ��True
                    ValidIssuer = AppSettingsInit.jwtSettings.Issuer,
                    ValidateAudience = true,//��֤������ || Ĭ��True
                    ValidAudience = AppSettingsInit.jwtSettings.Audience,
                    ValidateLifetime = true,//��֤�������� || Ĭ��True
                    ClockSkew = TimeSpan.Zero
                };
                jwtOpts.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // �����¼��֤
                        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                        var tokenModel = JwtTokenHelper.SerializeJWT(token);
                        var cacheKey = $"Jwt:{tokenModel.UserId}";
                        var cacheToken = string.Empty;
                        if (UseCache.Instance.KeyExists(cacheKey))
                            cacheToken = UseCache.Instance.Get(cacheKey);
                        else
                            context.Fail(failureMessage: "Unauthorized");

                        if (!token.Equals(cacheToken))
                            context.Fail(failureMessage: "Unauthorized");
                        else
                            context.HttpContext.Items["UserData"] = tokenModel.ToJson();

                        return Task.CompletedTask;
                    }
                };
            });

            #endregion

            #region Swagger ���߽ӿ��ĵ� ����
            // ����Swagger
            services.AddSwaggerGen(opts =>
            {
                // �����ĵ�������Ϣ
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NetCore.Fast.WebApi Document",
                    Version = "v1.0.0",
                    Description = "FastApi ���߽ӿ��ĵ�",
                    Contact = new OpenApiContact
                    {
                        Name = "GerdonLiu",
                        Email = "GerdonLiu@outlook.com"
                    }
                });

                // ��ȡxml�ļ���
                string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var item in files)
                {
                    // ��ȡxml�ļ�·��
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, item);
                    // ���ע�ͣ�true��ʾ��ʾע��
                    opts.IncludeXmlComments(xmlPath, true);
                }

                // Jwt��Ȩ
                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region 401 Unauthorized �Ѻ÷���

            app.UseStatusCodePages(new StatusCodePagesOptions()
            {
                HandleAsync = (context) =>
                {
                    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        var result = new ResultModel()
                        {
                            Code = (int)HttpStatusCode.Unauthorized,
                            Message = "δ��Ȩ"
                        };
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
                        context.HttpContext.Response.WriteAsync(result.ToJson());
                    }
                    return Task.Delay(0);
                }
            });

            #endregion

            #region Nlog����

            // Nlog 
            // ���������ļ�
            NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();
            // ������־�е������������
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            #endregion

            #region AutoMapper ����

            // AutoMapper
            app.UseStateAutoMapper();

            #endregion

            #region SwaggerUI ����
            // ����Swagger
            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "FastApi Api Document v1");
                opts.DocumentTitle = "FastApi API";
                opts.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseRouting();

            #region �����������
            // �������п���cors����ConfigureServices���������õĿ����������
            app.UseCors(AllowCorssOrigin);
            #endregion

            // ������֤��Ȩ
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
