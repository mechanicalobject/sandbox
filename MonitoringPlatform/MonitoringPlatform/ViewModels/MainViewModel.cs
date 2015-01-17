using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MonitoringPlatform.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<TabViewModelBase> _tabs;
        private readonly ServicesViewModel _servicesViewModel;
        private readonly UsersViewModel _usersViewModel;

        private readonly ICommand _tabControlSelectionChangedCommand;

        public MainViewModel(ServicesViewModel servicesViewModel, UsersViewModel usersViewModel)
        {
            this._servicesViewModel = servicesViewModel;
            this._usersViewModel = usersViewModel;
            _tabControlSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(SelectedTabChangedAction);

            BuildTabs();
        }

        private async void SelectedTabChangedAction(SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] is TabViewModelBase)
            {
                TabViewModelBase tabVm = e.AddedItems[0] as TabViewModelBase;
                await tabVm.SetFocusAsync();
            }
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

        public ICommand TabControlSelectionChangedCommand
        {
            get
            {
                return _tabControlSelectionChangedCommand;
            }
        }
    }


}
