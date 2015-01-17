using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MonitoringPlatform.ViewModels
{
    public abstract class TabViewModelBase : ViewModelBase
    {
        public abstract string TabName { get; }

        public abstract Task SetFocusAsync();
    }
}
