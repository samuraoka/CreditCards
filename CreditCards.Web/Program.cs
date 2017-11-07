using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CreditCards.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Add configuration providers
        /// https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/#add-configuration-providers
        /// 
        /// Configure an ASP.NET Core App
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration?tabs=basicconfiguration
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
