using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MonitoringPlatform.Repositories;

namespace MonitoringPlatform.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            RegisterServices();

            RegisterRepositories();

            RegisterViewModels();
        }

        private static void RegisterRepositories()
        {
            SimpleIoc.Default.Register<IServicesRepository, ServicesRepository>();
            SimpleIoc.Default.Register<IUsersRepository, UsersRepository>();
        }

        private static void RegisterServices()
        {
            //
        }

        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ServicesViewModel>();
            SimpleIoc.Default.Register<UsersViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }

}
