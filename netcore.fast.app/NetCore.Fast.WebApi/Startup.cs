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
            #region 统一处理（返回值、参数验证、异常）

            services.AddControllers(config =>
            {
                // 添加全局模型验证过滤器
                config.Filters.Add<ValidateModelAttribute>();
                // 添加全局异常处理过滤起
                config.Filters.Add<CustomExceptionAttribute>();
                // 添加统一友好返回过滤器
                config.Filters.Add<ApiResultFilterAttribute>();
            }).ConfigureApiBehaviorOptions(opts =>
            {
                // 关闭内置模型验证
                opts.SuppressModelStateInvalidFilter = true;
            });

            #endregion

            #region 跨域设置
            //设置跨域
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

            #region UnitOfWork & Repository & AutoMapper & 业务服务 注入

            // 自动创建映射
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper();
            services.AddAutoInject("NetCore.Fast.Services");

            #endregion

            #region Jwt 基础验证配置  单点登录验证

            AppSettingsInit.JwtSettingInit(Configuration);

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOpts =>
            {
                jwtOpts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//验证全局密钥 || 默认True
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsInit.jwtSettings.SecretKey)),
                    ValidateIssuer = true,//验证发布者 || 默认True
                    ValidIssuer = AppSettingsInit.jwtSettings.Issuer,
                    ValidateAudience = true,//验证访问者 || 默认True
                    ValidAudience = AppSettingsInit.jwtSettings.Audience,
                    ValidateLifetime = true,//验证生命周期 || 默认True
                    ClockSkew = TimeSpan.Zero
                };
                jwtOpts.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // 单点登录验证
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

            #region Swagger 在线接口文档 配置
            // 配置Swagger
            services.AddSwaggerGen(opts =>
            {
                // 配置文档基础信息
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NetCore.Fast.WebApi Document",
                    Version = "v1.0.0",
                    Description = "FastApi 在线接口文档",
                    Contact = new OpenApiContact
                    {
                        Name = "GerdonLiu",
                        Email = "GerdonLiu@outlook.com"
                    }
                });

                // 获取xml文件名
                string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var item in files)
                {
                    // 获取xml文件路径
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, item);
                    // 添加注释，true表示显示注释
                    opts.IncludeXmlComments(xmlPath, true);
                }

                // Jwt授权
                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
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

            #region 401 Unauthorized 友好返回

            app.UseStatusCodePages(new StatusCodePagesOptions()
            {
                HandleAsync = (context) =>
                {
                    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        var result = new ResultModel()
                        {
                            Code = (int)HttpStatusCode.Unauthorized,
                            Message = "未授权"
                        };
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
                        context.HttpContext.Response.WriteAsync(result.ToJson());
                    }
                    return Task.Delay(0);
                }
            });

            #endregion

            #region Nlog配置

            // Nlog 
            // 配置配置文件
            NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();
            // 避免日志中的中文输出乱码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            #endregion

            #region AutoMapper 配置

            // AutoMapper
            app.UseStateAutoMapper();

            #endregion

            #region SwaggerUI 配置
            // 配置Swagger
            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "FastApi Api Document v1");
                opts.DocumentTitle = "FastApi API";
                opts.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseRouting();

            #region 设置允许跨域
            // 允许所有跨域，cors是在ConfigureServices方法中配置的跨域策略名称
            app.UseCors(AllowCorssOrigin);
            #endregion

            // 配置认证授权
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
