using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Services
{
    public class LoginService : ILoginService
    {
        private Dictionary<string, string> users;

        public LoginService()
        {
            users = new Dictionary<string, string>();
            users.Add("test", "123");
            users.Add("admin", "admin123");
        }

        public async Task<bool> CheckLogin(string userName, string password)
        {
            if (users.ContainsKey(userName))
            {
                return users[userName] == password;
            }

            return false;
        }
    }
}
