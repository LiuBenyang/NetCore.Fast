using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace NetCore.Fast.Infrastructure.AutoInject
{
    /// <summary>
    /// 批量注入服务
    /// </summary>
    public static class AutoInjectExtensions
    {

        /// <summary>
        ///  注入dll文件 需要接口模式
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName">程序集名称，不需要后缀</param>
        /// <param name="suffixName">注入的服务类后辍名 默认 Service 比如 IUserService</param>
        /// <param name="serviceLifetime"></param>
        public static void AddAutoInject(this IServiceCollection services,
            string assemblyName,
            string suffixName = "Service",
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));

            var types = assembly.GetTypes();
            var list = types.Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType && !u.IsInterface)
                .Where(t => t.Name.EndsWith(suffixName)).ToList();

            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();
                if (interfaceList.Any())
                {
                    var inter = interfaceList.First();

                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Transient:
                            services.AddTransient(inter, type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(inter, type);
                            break;

                    }
                    services.AddScoped(inter, type);
                }
            }
        }

        /// <summary>
        /// 自动创建映射
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var allProfile = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom)
                .SelectMany(y => y.DefinedTypes)
                .Where(x => typeof(Profile).IsAssignableFrom(x) && x.IsClass)
                .ToArray();
            services.AddAutoMapper(allProfile);
        }
    }
}
