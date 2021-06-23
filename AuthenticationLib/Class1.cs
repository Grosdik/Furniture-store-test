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
        public static bool ValidetePassword(string password)
        {
            if (password.Length < 8 || password.Length > 20)
            {
                return false;
            }
            if (!password.Any(Char.IsDigit))
            {
                return false;
            }
            if (password.Intersect("#%!?&_").Count() == 0)
            {
                return true;
            }

            return true;
        }
    }
}
