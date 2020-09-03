using ServiceStack;
using System;
using System.Web;
using CalculatorServiceStack.Web.Services;

namespace CalculatorServiceStack.Web
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }

    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("ServiceStackWeb",
        typeof(CalculatorService).Assembly)
        {
        }
        public override void Configure(Funq.Container container)
        {
            
        }
    }
}