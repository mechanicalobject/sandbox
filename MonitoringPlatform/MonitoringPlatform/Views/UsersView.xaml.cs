using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MonitoringPlatform.Messages;

namespace MonitoringPlatform.Views
{
    public partial class UsersView
    {
        public UsersView()
        {
            InitializeComponent();

            Messenger.Default.Register<UsersRepositoryErrorMessage>(this, ReactOnUserError);
        }

        private void ReactOnUserError(UsersRepositoryErrorMessage error)
        {
            MessageBox.Show(error.Error.Message);
        }

        ~UsersView()
        {
            Messenger.Default.Unregister<UsersRepositoryErrorMessage>(this, ReactOnUserError);
        }
    }

}
