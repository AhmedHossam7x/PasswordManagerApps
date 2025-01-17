using System.Collections.Generic;
using System.Text;

namespace PasswordManager
{
    internal class Program
    {
        private static readonly Dictionary<string, string> _PASSWORDENTRIES = new();

        private static void Main(string[] args)
        {
            while (!CheckPassword())
            {
                CheckPassword();
            }

            ReadPassword();
            
            while (true)
            {
                Console.WriteLine("Pls, select an optian: ");
                Console.WriteLine("1: List All Password");
                Console.WriteLine("2: Add/Change password");
                Console.WriteLine("3: Get password");
                Console.WriteLine("4: Delete password");

                string select = Console.ReadLine();

                switch(select)
                {
                    case "1":
                        ListAllPassord();
                        break;
                    case "2":
                        AddOrChangePassword();
                        break;
                    case "3":
                        GetPassord();
                        break;
                    case "4":
                        DeletePassord();
                        break;
                    default:
                        Console.WriteLine("Invalid Password");
                        break;
                }
                Console.WriteLine("----------------------------------");
            }
        }

        private static bool CheckPassword()
        {
            if (File.Exists("passwords.txt"))
            {
                var lineAllText = File.ReadAllText("passwords.txt");
                foreach (var line in lineAllText.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var lineIndex = line.IndexOf('=');
                        var name = line.Substring(0, lineIndex);
                        var pw = line.Substring(lineIndex + 1);
                       if(name.Equals("master", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Pls, Enter your password: ");
                            var inputPw = Console.ReadLine();
                            if (inputPw.Equals(EncryptionUtility.Decrypt(pw), StringComparison.OrdinalIgnoreCase))
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Welcome to Apps:");
                                Console.WriteLine("================");
                                Console.ForegroundColor = ConsoleColor.White;
                                return true;

                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Wrong Passowrd...!");
                                Console.ForegroundColor = ConsoleColor.White;
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The master Key no exist");
                            Console.WriteLine("=================");
                            Console.Write("Pls, Enter your master key: ");
                            var masterKey = Console.ReadLine();
                            Console.Write("Pls, Enter your master password: ");
                            var masterPassword = Console.ReadLine();
                            _PASSWORDENTRIES.Add(masterKey, masterPassword);
                            return true;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The file no exist");
                Console.WriteLine("=================");
                Console.Write("Pls, Enter your master key: ");
                var masterKey = Console.ReadLine();
                Console.Write("Pls, Enter your master password: ");
                var masterPassword = Console.ReadLine();
                _PASSWORDENTRIES.Add(masterKey, masterPassword);
                return true;
            }
            return false;
        }
        private static void ReadPassword()
        {
            if (File.Exists("passwords.txt"))
            {
                var lineAllText = File.ReadAllText("passwords.txt");
                foreach(var line in lineAllText.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var lineIndex = line.IndexOf('=');
                        var name = line.Substring(0, lineIndex);
                        var pw = line.Substring(lineIndex + 1);
                        _PASSWORDENTRIES.Add(name, EncryptionUtility.Decrypt(pw));
                    }
                }
            }
        }
        private static void SavePassword()
        {
            var sb = new StringBuilder();
            foreach (var item in _PASSWORDENTRIES)
            {
                sb.AppendLine($"{item.Key}={EncryptionUtility.Encrypt(item.Value)}");
                File.WriteAllText("passwords.txt",sb.ToString());
            }
        }
        private static void ListAllPassord()
        {
            if (_PASSWORDENTRIES.Count > 0)
            {
                foreach (var v in _PASSWORDENTRIES)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{v.Key}={v.Value}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("No data...");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void AddOrChangePassword()
        {
            Console.Write("Pls, Enter website/appName: ");
            var appName = Console.ReadLine();
            Console.Write("Pls, Enter password: ");
            var pw = Console.ReadLine();
            if (_PASSWORDENTRIES.ContainsKey(appName))
            {
                _PASSWORDENTRIES[appName] = pw;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data Change Successfully");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                _PASSWORDENTRIES.Add(appName, pw);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Data Add Successfully");
                Console.ForegroundColor = ConsoleColor.White;
            }
            SavePassword();
        }
        private static void GetPassord()
        {
            if(_PASSWORDENTRIES.Count > 0)
            {
                Console.Write("Pls, Enter websit/appName: ");
                var appName = Console.ReadLine();
                if (_PASSWORDENTRIES.ContainsKey(appName))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Your password is: {_PASSWORDENTRIES[appName]}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Not found Password");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void DeletePassord()
        {
            if (_PASSWORDENTRIES.Count > 0)
            {
                Console.Write("Pls, Enter websit/appName: ");
                var appName = Console.ReadLine();
                if (_PASSWORDENTRIES.ContainsKey(appName))
                {
                    _PASSWORDENTRIES.Remove(appName);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Deleted succefull");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
