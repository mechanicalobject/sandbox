using GalaSoft.MvvmLight;

namespace MonitoringPlatform.ViewModels
{
    public abstract class TabViewModelBase : ViewModelBase
    {
        public abstract string TabName { get; }
    }
}
