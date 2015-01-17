using System.Collections.ObjectModel;
using AutoMapper;
using MonitoringPlatform.Models;
using MonitoringPlatform.Repositories;
using MonitoringPlatform.ViewModels.ObservableObjects;

namespace MonitoringPlatform.ViewModels
{
    public class UsersViewModel : TabViewModelBase
    {
        private readonly IUsersRepository _usersRepository;
        private ObservableCollection<UserOo> _users;

        public UsersViewModel(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;

            InitUsers();
        }

        private void InitUsers()
        {
            if (Users == null)
                Users = new ObservableCollection<UserOo>();

            Users.Clear();

            var users = _usersRepository.GetUsers();
            if (users != null)
            {
                foreach (UserModel userModel in users)
                {
                    Users.Add(Mapper.Map<UserModel, UserOo>(userModel));
                }
            }
        }

        public override string TabName
        {
            get
            {
                return "Users";
            }
        }

        public ObservableCollection<UserOo> Users
        {
            get
            {
                return this._users;
            }
            set
            {
                if (value == _users)
                    return;

                this._users = value;
                this.RaisePropertyChanged();
            }
        }
    }

}
