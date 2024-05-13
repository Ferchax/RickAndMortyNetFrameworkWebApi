using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty3.DependencyInjection;

namespace RickAndMorty3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        internal static IServiceProvider ServiceProvider { get; private set; }

        protected void Application_Start()
        {
            AutofacWebApiConfig.Initialize(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
