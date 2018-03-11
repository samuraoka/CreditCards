using CreditCards.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CreditCards.Infrastructure
{
    /// <summary>
    /// To create a data model, add following NuGet package using the below command in Package Manager
    /// PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer -ProjectName CreditCards.Data -Version 2.0.0
    /// https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/
    ///   
    /// To get help messages, use a following command in Package Manager.
    /// PM> get-help entityframeworkcore
    ///   
    /// To add/remove a new migration, execute a following command
    /// PM> Add-Migration -Name Init -Context AppDbContext -Project CreditCards.Data -StartupProject CreditCards.Web
    /// PM> Add-Migration -Name AddedColumn -Context AppDbContext -Project CreditCards.Data -StartupProject CreditCards.Web
    /// And if you have a error, try to add properties to the csproj file.
    /// <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    /// <GenerateBindingRedirectsOutputType>true</GenerateBindingRedirectsOutputType>
    /// https://stackoverflow.com/questions/45978173/system-valuetuple-version-0-0-0-0-required-for-add-migration-on-net-4-6-1-cla
    /// https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-entity-framework-core-2-0/
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<CreditCardApplication> CreditCardApplication { get; set; }
    }
}
