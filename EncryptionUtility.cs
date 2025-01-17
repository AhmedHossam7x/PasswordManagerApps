using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class EncryptionUtility
    {
        private static readonly string _ORIGINALSTRING = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; 
        private static readonly string _ALTSTRING = "Ma0Tk1pX2cZ3IO4rJ5bY6eQ7ul8hD9gwFmNsAtKPxCzLoRjByEqUViHdGvWfnS";

        public static string Encrypt(string password)
        {
            var sb = new StringBuilder();
            foreach (char c in password)
            {
                var line = _ORIGINALSTRING.IndexOf(c);
                sb.Append(_ALTSTRING[line]);
            }
            return sb.ToString();
        }
        public static string Decrypt(string password)
        {
            var sb = new StringBuilder();
            foreach (char c in password)
            {
                var line = _ALTSTRING.IndexOf(c);
                sb.Append(_ORIGINALSTRING[line]);
            }
            return sb.ToString();
        }
    }
}
