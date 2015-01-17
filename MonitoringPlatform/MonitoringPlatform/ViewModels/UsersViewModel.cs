using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
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
        private bool _isBusy;

        public UsersViewModel(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        private void InitUsers()
        {
            IsBusy = true;

            _users = new ObservableCollection<UserOo>();

            IList<UserModel> users = null;
            
            Task.Run(
                () =>
                {
                    try
                    {
                        users = _usersRepository.GetUsers();
                    }
                    catch (Exception ex)
                    {
                        MessengerInstance.Send(new UsersRepositoryErrorMessage { Error = ex });
                    }

                    // The folowing line simulates a long task
                    // You can safely remove it
                    Thread.Sleep(2000);
                }).ContinueWith(
                        previous =>
                        {
                            IsBusy = false;
                            if (users != null)
                            {
                                foreach (UserModel userModel in users)
                                {
                                    _users.Add(Mapper.Map<UserModel, UserOo>(userModel));
                                }
                            }
                        }, TaskScheduler.FromCurrentSynchronizationContext());
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
