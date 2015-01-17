using System.Windows;
using AutoMapper;
using MonitoringPlatform.Models;
using MonitoringPlatform.ViewModels.ObservableObjects;

namespace MonitoringPlatform
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeAutomapper();
            base.OnStartup(e);
        }

        private void InitializeAutomapper()
        {
            Mapper.CreateMap<ServiceModel, ServiceOo>();
            Mapper.CreateMap<UserModel, UserOo>();
        }

    }
}
