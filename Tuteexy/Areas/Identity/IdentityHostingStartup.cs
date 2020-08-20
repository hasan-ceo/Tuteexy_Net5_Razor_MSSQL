using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Tuteexy.Areas.Identity.IdentityHostingStartup))]
namespace Tuteexy.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}