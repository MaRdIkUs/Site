using System.Security.Claims;

namespace WebApplication5.Data.Interfaces
{
    public interface IRoles
    {
        public string GetRoleName(int Id);
        public string GetRolesEqualsOrGreater(int Id);
        public string[] getRoles();
        public bool IsUserHaveAccess(int id, ClaimsPrincipal User);
    }
}
