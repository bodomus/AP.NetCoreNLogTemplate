using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AP.NetCoreNLogTemplate
{
    public class App
    {
        private readonly IConfigurationRoot _config;
        private readonly ILogger<App> _logger;
        private readonly string _сonnectionString;
        private readonly string _domain;
        private Stopwatch _stopwatch;
        

        public App(IConfigurationRoot config, ILogger<App> logger)
        {
            _logger = logger;
            _config = config;
            _сonnectionString = _config.GetConnectionString("ConnectionString");
            _domain = _config.GetSection("Domain").Get<string>();
            
        }

        public async Task Run()
        {
            _logger.LogInformation(new EventId(2, "eventId2"), "+++++++++++++++++ Старт сервиса ++++++++++++++++");
            _logger.LogInformation(new EventId(2, "eventId2"), $"Настройки ConnectionString: {_сonnectionString} Domain: {_domain}");
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка в dbo.GetAccessUser {ex.Message}");
                throw;
            }
            _stopwatch.Stop();
            _logger.LogInformation($"---------------- Стоп сервиса. Время работы: {_stopwatch.Elapsed} ------------------------- ");
        }

    }
}