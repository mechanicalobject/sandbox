using System.Collections.Generic;
using MonitoringPlatform.Models;

namespace MonitoringPlatform.Repositories
{
    public interface IUsersRepository
    {
        IList<UserModel> GetUsers();
    }
    public class UsersRepository : IUsersRepository
    {
        public IList<UserModel> GetUsers()
        {
            // TODO
            return null;
        }

    }
}
