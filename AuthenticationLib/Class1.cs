using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationLib
{
    public static class Class1
    {
        private static int _userId = 0;
        private static int _userRoleId = 0;
        private static string _userLogin = "";
        private static string _userPass = "";

        public static int Auth(string login, string password)
        {
            try
            {
                if ((login != "") && (password != ""))
                {
                    string loginQuery = "SELECT * FROM Users WHERE Login = '" + login + "'";
                    var str = Furniture_storeEntities.GetContext().Users.SqlQuery(loginQuery).ToList();
                    var row = str[0];
                    _userId = row.id;
                    _userRoleId = row.RoleId;
                    _userLogin = row.Login;
                    _userPass = row.Password;
                    if ((_userRoleId == 1) && (_userPass == password))
                    {
                        return 1;
                    }
                    else if ((_userRoleId == 2) && (_userPass == password))
                    {
                        return 2;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }

        }
    }
}
