using Kinder.MessageCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kinder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IChatService, ChatService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<MainWindow>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
