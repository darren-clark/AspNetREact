using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Microsoft.Owin.BuilderProperties;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

[assembly: OwinStartupAttribute(typeof(AspNetReact.Startup))]
namespace AspNetReact
{
    public class Startup
    {
        private class NodeLogger: ILogger
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {

            }
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }
            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }
        }
        public void Configuration(IAppBuilder app)
        {
            var properties = new AppProperties(app.Properties);
            var services = new ServiceCollection();
            var path = HostingEnvironment.MapPath("~/Scripts/es6");
            services.AddNodeServices(opts =>
            {
                opts.LaunchWithDebugging = false;
                opts.ProjectPath = path;
                opts.ApplicationStoppingToken = properties.OnAppDisposing;
                opts.NodeInstanceOutputLogger = new NodeLogger();
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var container = builder.Build();

            var nodeServices = container.Resolve<INodeServices>();
            var foo = nodeServices.InvokeAsync<string>("node_modules/webpack","--mode", "development", "--watch").Result;
        }
    }
}