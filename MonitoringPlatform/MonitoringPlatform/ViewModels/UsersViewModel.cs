using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using MonitoringPlatform.Messages;
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
        }

        private void InitUsers()
        {
            _users = new ObservableCollection<UserOo>();

            IList<UserModel> users = null;
            try
            {
                users = _usersRepository.GetUsers();
            }
            catch (Exception ex)
            {
                MessengerInstance.Send(new UsersRepositoryErrorMessage { Error = ex });
            }

            if (users != null)
            {
                foreach (UserModel userModel in users)
                {
                    _users.Add(Mapper.Map<UserModel, UserOo>(userModel));
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
                if (_users == null)
                    InitUsers();

                return this._users;
            }
        }
    }


}
