using System.Collections.Generic;
using WebApplication5.Data.Models;

namespace WebApplication5.Models
{
    public class UsersListModel
    {
        public List<UserData> UsersData { get; set; }
        public string[] Roles { get; set; }
    }
}
