using CreditCards.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.IntegrationTests
{
    // Microsoft.AspNetCore.TestHost
    // https://www.nuget.org/packages/Microsoft.AspNetCore.TestHost/
    // Install-Package -Id Microsoft.AspNetCore.TestHost -ProjectName CreditCards.IntegrationTests
    //
    // Integration tests in ASP.NET Core
    // https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing?view=aspnetcore-2.0
    public class CreditCardApplicationShould
    {
        private readonly ITestOutputHelper output;

        // Capturing Output
        // https://xunit.github.io/docs/capturing-output.html
        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        // Testing ASP.NET Core MVC web apps in-memory
        // https://blogs.msdn.microsoft.com/webdev/2017/12/07/testing-asp-net-core-mvc-web-apps-in-memory/
        [Fact]
        public async void RenderApplicationFormAsync()
        {
            // Arrange
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\CreditCards.Web");
            output.WriteLine(rootPath);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<Startup>()
                .Build();

            var builder = new WebHostBuilder()
                .UseContentRoot(rootPath)
                //.UseEnvironment("Development") Uncomment this to show detailed error message
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseApplicationInsights();
            var server = new TestServer(builder);
            var client = server.CreateClient();

            // Act
            var response = await client.GetAsync("/Apply");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("New Credit Card Application", responseString);
        }
    }
}
