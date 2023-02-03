using WebApplication5.Data.Models;

namespace WebApplication5.Models
{
    public class EditUserModel
    {
        public string Role { get; set; }
        public UserData UserData { get; set; }
        public string[] Roles { get; set; }
    }
}
