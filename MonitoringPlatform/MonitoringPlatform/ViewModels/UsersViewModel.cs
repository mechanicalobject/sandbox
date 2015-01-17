using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MonitoringPlatform.Messages;
using MonitoringPlatform.Models;
using MonitoringPlatform.Services;
using MonitoringPlatform.ViewModels.ObservableObjects;

namespace MonitoringPlatform.ViewModels
{
    public class UsersViewModel : TabViewModelBase
    {
        private readonly IUsersService _usersService;
        private ObservableCollection<UserOo> _users;

        private bool _isBusy;

        public UsersViewModel(IUsersService usersService)
        {
            _usersService = usersService;
        }

        private async Task InitUsers()
        {
            IsBusy = true;

            _users = new ObservableCollection<UserOo>();

            IList<UserModel> users = null;

            try
            {
                users = await _usersService.GetUsers();
            }
            catch (Exception ex)
            {
                MessengerInstance.Send(new UsersRepositoryErrorMessage { Error = ex });
            }

            IsBusy = false;
            if (users != null)
            {
                foreach (UserModel userModel in users)
                {
                    Users.Add(Mapper.Map<UserModel, UserOo>(userModel));
                }
            }
        }

        public override async Task SetFocusAsync()
        {
            await InitUsers();
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
                return _users;
            }
            set
            {
                if (_users == value)
                    return;

                _users = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                if (_isBusy == value)
                    return;

                this._isBusy = value;
                this.RaisePropertyChanged();
            }
        }
    }




}
