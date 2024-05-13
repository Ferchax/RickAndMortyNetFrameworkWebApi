using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace RickAndMorty3
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(ctx =>
            {
                var services = new ServiceCollection();
                services.AddHttpClient("RickAndMortyAPI", c =>
                {
                    c.BaseAddress = new Uri("https://rickandmortyapi.com/api/");
                    c.Timeout = TimeSpan.FromSeconds(15);
                    c.DefaultRequestHeaders.Add(
                        "accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromSeconds(15));

                var provider = services.BuildServiceProvider();
                return provider.GetRequiredService<IHttpClientFactory>();

            }).SingleInstance();

            builder.RegisterType<CharacterService>()
                .As<ICharacterService>()
                .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}