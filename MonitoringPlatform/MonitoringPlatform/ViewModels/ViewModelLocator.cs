using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace MonitoringPlatform.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register Services in here

            // Register ViewModels in here
            SimpleIoc.Default.Register<MainViewModel>();
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
