using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace MonitoringPlatform.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<TabViewModelBase> _tabs;
        private readonly ServicesViewModel _servicesViewModel;
        private readonly UsersViewModel _usersViewModel;

        public MainViewModel(ServicesViewModel servicesViewModel, UsersViewModel usersViewModel)
        {
            this._servicesViewModel = servicesViewModel;
            this._usersViewModel = usersViewModel;

            BuildTabs();
        }

        private void BuildTabs()
        {
            if (Tabs == null)
                Tabs = new ObservableCollection<TabViewModelBase>();

            Tabs.Clear();

            Tabs.Add(_servicesViewModel);
            Tabs.Add(_usersViewModel);
        }

        public ObservableCollection<TabViewModelBase> Tabs
        {
            get
            {
                return _tabs;
            }
            set
            {
                if (value == _tabs)
                    return;

                _tabs = value;
                this.RaisePropertyChanged();
            }
        }
    }

}
