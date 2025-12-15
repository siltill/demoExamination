using demoNEW_EFCoreVersion.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoNEW_EFCoreVersion
{
    internal class UserSession
    {
        public static string Role { get; set; }
        public static string FullName { get; set; }
        public static int? UserId { get; set; }

        public static void SetUser(Users user)
        {
            if (user == null)
            {
                Clear();
                return;
            }

            UserId = user.Id;
            Role = user.UsersRole?.Name ?? "Unknown";
            FullName = $"{user.LastName} {user.FirstName} {user.Patronymic}".Trim();
        }

        public static void SetGuest()
        {
            Clear();
            Role = "Guest";
            FullName = "Guest";
        }

        public static void Clear()
        {
            UserId = null;
            Role = null;
            FullName = null;
        }
    }
}
