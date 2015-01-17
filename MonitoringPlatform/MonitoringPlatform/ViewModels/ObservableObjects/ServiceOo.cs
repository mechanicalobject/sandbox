using GalaSoft.MvvmLight;
using MonitoringPlatform.Models;

namespace MonitoringPlatform.ViewModels.ObservableObjects
{
    public class ServiceOo : ObservableObject
    {
        private WindowsServiceStatus _status;
        private string _serviceName;

        public string ServiceName
        {
            get
            {
                return this._serviceName;
            }
            set
            {
                if (value == _serviceName)
                    return;

                this._serviceName = value;
                this.RaisePropertyChanged();
            }
        }

        public WindowsServiceStatus Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (value == _status)
                    return;

                this._status = value;
                this.RaisePropertyChanged();
            }
        }
    }

}
