using System.Collections.Generic;
using WebApplication5.Data.Models;

namespace WebApplication5.Data.Interfaces
{
    public interface IUserData
    {
        public bool updateUser(string login, UserData user);
        public List<UserData> GetUsers();
        public bool addUser (UserData user);
        public bool removeUser (UserData user);
        public UserData GetUser(string login);
    }
}
