using Microsoft.AspNetCore;

namespace UniversityScheduler.Api;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateWebHostBuilder(args).Build();
        await host.RunAsync();
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost
            .CreateDefaultBuilder(args)
            .ConfigureKestrel((_, options) =>
            {
                options.AllowSynchronousIO = true;
            }) //Kestrel is used for running the host
            .UseStartup<Startup>();
    }
}

//5432 -> postgres port