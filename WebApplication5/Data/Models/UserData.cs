using System;

namespace WebApplication5.Data.Models
{
    [Serializable]
    public class UserData
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public int AccessLevel { get; set; }

        public UserData(string password, string login, int accessLevel)
        {
            Password = password;
            Login = login;
            AccessLevel = accessLevel;
        }
        public UserData() {}
        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(UserData))
                return false;
            UserData o2 = (UserData)obj;
            if(o2.Login==null || o2.Password == null)
                return false;
            if(Password.Equals(o2.Password) && Login.Equals(o2.Login) && AccessLevel==o2.AccessLevel)
                return true;
            return false;
        }
    }
}
