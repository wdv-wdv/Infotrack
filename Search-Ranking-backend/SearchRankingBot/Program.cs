using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScarperSelenium;
using SearchRankingBL;
using SearchRankingDL;
using Serilog;
using System.Threading.Tasks;

namespace SearchRankingBot
{
    internal class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

            return new HostBuilder()
                //.ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false))
                .UseSerilog(Log.Logger)
                .ConfigureServices(services => services.AddSingleton<IDatalayer, Datalayer>())
                .ConfigureServices(services => services.AddSingleton<ISearchWorker, SearchWorker>())
                .ConfigureServices(services => services.AddSingleton<IScarper, Scarper>())
                .ConfigureServices(services => services.AddHostedService<BotWorker>());
        }

        static Task Main(string[] args) =>
            CreateHostBuilder(args).Build().RunAsync();
    }
}