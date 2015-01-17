using System;
using System.Collections.ObjectModel;
using System.Windows;
using AutoMapper;
using MonitoringPlatform.Models;
using MonitoringPlatform.Repositories;
using MonitoringPlatform.ViewModels.ObservableObjects;

namespace MonitoringPlatform.ViewModels
{
    public class ServicesViewModel : TabViewModelBase
    {
        private readonly IServicesRepository _servicesRepository;
        private ObservableCollection<ServiceOo> _windowsServices;

        public ServicesViewModel(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;

            InitServices();
        }

        private void InitServices()
        {
            try
            {
                if (WindowsServices == null)
                    WindowsServices = new ObservableCollection<ServiceOo>();

                WindowsServices.Clear();

                var services = _servicesRepository.GetServices();
                if (services != null)
                {
                    foreach (ServiceModel serviceModel in services)
                    {
                        WindowsServices.Add(Mapper.Map<ServiceModel, ServiceOo>(serviceModel));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error when getting windows services: {0}", ex.Message));
            }
        }

        public override string TabName
        {
            get
            {
                return "Services";
            }
        }

        public ObservableCollection<ServiceOo> WindowsServices
        {
            get
            {
                return this._windowsServices;
            }
            set
            {
                if (value == _windowsServices)
                    return;

                this._windowsServices = value;
                this.RaisePropertyChanged();
            }
        }
    }

}
