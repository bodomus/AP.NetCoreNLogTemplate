using System;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;

namespace AP.NetCoreNLogTemplate
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static int Main(string[] args)
        {
            try
            {
                MainAsync(args).Wait();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        private static void SetupNLog(IServiceCollection services, IConfiguration config)
        {
            LogManager.Setup()
                .SetupExtensions(s => s.AutoLoadAssemblies(false))
                .SetupExtensions(s => s.RegisterConfigSettings(config))
                .LoadConfigurationFromSection(config)
                .GetCurrentClassLogger();

            services.AddLogging(builder =>
            {
                builder.AddNLog(config);
            });
        }

        static async Task MainAsync(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            SetupNLog(serviceCollection, configuration);

            try
            {
                await serviceProvider.GetService<App>().Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            serviceCollection.AddLogging();

            serviceCollection.AddSingleton(LoggerFactory.Create(builder =>
            {
                builder.AddNLog();
            }));

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
            serviceCollection.AddSingleton<DbProviderFactory>(System.Data.SqlClient.SqlClientFactory.Instance);
            serviceCollection.AddTransient<App>();
        }
    }
}