using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password);
        List<User> GetUsers();
        List<User> GetUsersExceptLogged(int userId);
        bool MakeTransfer(int senderId, int receiverId,int amount);
    }
}
