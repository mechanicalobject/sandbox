using System.Collections.Generic;
using System.Threading.Tasks;
using MonitoringPlatform.Models;
using MonitoringPlatform.Repositories;

namespace MonitoringPlatform.Services
{
    public interface IUsersService
    {
        Task<IList<UserModel>> GetUsers();
    }
    
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public async Task<IList<UserModel>> GetUsers()
        {
            var taskResult = await Task.Run(() => this._usersRepository.GetUsers());
            return taskResult;
        }
    }

}
