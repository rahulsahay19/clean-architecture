using Movies.Infrastructure.Data;

namespace Movies.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await CreateAndSeedDb(host);
            host.Run();
        }

        
        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        private static async Task CreateAndSeedDb(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var moviesContext = services.GetRequiredService<MovieContext>();
                    await MovieContextSeed.SeedAsync(moviesContext, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError($"Exception occured in API: {ex.Message}");
                }
            }
            
        }
    }
}

