using GalaSoft.MvvmLight;

namespace MonitoringPlatform.ViewModels.ObservableObjects
{
    public class UserOo : ObservableObject
    {
        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value == _name)
                    return;

                this._name = value;
                this.RaisePropertyChanged();
            }
        }
    }

}
