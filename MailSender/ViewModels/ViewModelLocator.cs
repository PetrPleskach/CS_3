using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public StatisticViewModel StatisticTabModel => App.Services.GetRequiredService<StatisticViewModel>();
    }
}
