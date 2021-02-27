using MailSender.Infrastructure.Interfaces;
using MailSender.Services;
using MailSender.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(c => c.AddJsonFile("appsettings.json", false, false)).ConfigureServices(ServicesConfiguration);

        private static void ServicesConfiguration(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IStatistic, InMemoryStatisticService>();

#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif

            //Хранилища данных
            var memoryStorage = new InMemoryDataStorage();
            services.AddSingleton<IServersStorage>(memoryStorage);
            services.AddSingleton<ISendersStorage>(memoryStorage);
            services.AddSingleton<IRecipientsStorage>(memoryStorage);
            services.AddSingleton<IMessagesStorage>(memoryStorage);
        }
    }
}
