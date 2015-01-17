using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
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
            throw new ApplicationException("Error just for test");
            IList<UserModel> users = new List<UserModel>();

            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
            DirectoryEntry admGroup = localMachine.Children.Find("users", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                UserModel user = new UserModel();
                user.Name = member.Name;
                users.Add(user);
            }
            return users;
        }
    }
}
