using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using WebApplication5.Data.Interfaces;

namespace WebApplication5.Data.Mocks
{
    public class MockRoles : IRoles
    {
        private readonly string[] roles = { "Member", "Moder", "Admin"};

        public string[] getRoles() { 
            return roles;
        }
        public string GetRoleName(int Id)
        {
            if (Id >= roles.Length)
                return null;
            return roles[Id];
        }
        public string GetRolesEqualsOrGreater(int Id) {
            string currRole = GetRoleName(Id);
            string result = "";
            if (currRole == null)
                return null;
            result += currRole;
            for (int i = 1; true; i++) {
                currRole= GetRoleName(Id+i);
                if (currRole == null)
                    return result;
                result +=',' + currRole;
            }
        }
        public bool IsUserHaveAccess(int id, ClaimsPrincipal User) {
            bool isAccessConfirmed = false;
            foreach (string role in GetRolesEqualsOrGreater(id).Split(","))
            {
                if (User.IsInRole(role))
                {
                    isAccessConfirmed = true;
                    break;
                }
            }
            return isAccessConfirmed;
        }
    }
}
