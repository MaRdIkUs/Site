using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebApplication5.Data.Interfaces;
using WebApplication5.Data.Models;

namespace WebApplication5.Data.Serializers
{
    public class UserSerializer : IUserData
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UserData));
        public bool addUser(UserData user)
        {
            using (FileStream stream = new FileStream(getPath(user.Login), FileMode.Create))
            {
                serializer.Serialize(stream, user);
            }
            return true;
        }

        public UserData GetUser(string login)
        {
            using (FileStream stream = new FileStream(getPath(login), FileMode.Open))
            {
                return (UserData)serializer.Deserialize(stream);
            }
        }

        public List<UserData> GetUsers()
        {
            List<UserData> users = new List<UserData>();
            foreach (string i in getFiles()) { 
                if(i != null)
                    users.Add(GetUser(i));
            }
            return users;
        }

        public bool removeUser(UserData user)
        {
            File.Delete(getPath(user.Login));
            return true;
        }

        public bool updateUser(string login, UserData user)
        {
            File.Delete(getPath(login));
            addUser(user);
            return true;
        }

        private string getPath(string login) { 
            return Startup.userDataDirectory + login + ".xml";
        }
        private string[] getFiles()
        {
            FileInfo[] files = new DirectoryInfo(Startup.userDataDirectory).GetFiles();
            string[] result = new string[files.Length];
            int i = 0;
            foreach (FileInfo info in files)
            {
                if (info.Name.Substring(info.Name.Length - 4, 4) == ".xml")
                    result[i++] = info.Name.Substring(0, info.Name.Length - 4);
                else
                    result[i++] = null;
            }
            return result;
        }
    }
}
