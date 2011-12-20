using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace ryeSh
{
    class setup
    {
        public static string pfiles = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        public static string choice;
        public static string gpl;

        public struct _password_
        {
            public bool std;
            public string password;
            public int password_length;
        }

        public static int sys_setup()
        {
            /* Initialize structure */
            _password_ sys_settings = new _password_();
            
            
            if (Directory.Exists(pfiles + "/ryeShell") == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("(**\n*\n* Checking disk...\n* Running shDisk...\n* Verifying filesystem...\n* Returned 4!\n*\n**)\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the Rye Shell library setup!");
                Console.WriteLine("To use this program you must select the desired options.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter 'k' to proceed to the setup or any other key to exit\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string cc = Console.ReadLine();
                if (cc == "k")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.Write("Enter either: 'yes' or 'no'\n");
                    Console.WriteLine("Allow Rye Shell to access your filesystem?\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("# ");
                    choice = Console.ReadLine();
                    if (choice != "yes")
                        Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n** Access granted! **\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Allow Rye Shell create a cache?\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("# ");
                    choice = Console.ReadLine();
                    if (choice != "yes")
                        Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n** Access granted! **\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Allow Rye Shell to write settings to disk?\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("# ");
                    choice = Console.ReadLine();
                    if (choice != "yes")
                        Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n** Access granted! **\n");
                    /* Filesystem setup */
                    Program.ExecCmd("cd " + pfiles + " && mkdir ryeShell");
                    Console.Write(Program.ExecCmd("tasklist"));
                    Console.WriteLine("\n** Created: " + pfiles + "/ryeShell **");
                    Console.WriteLine("** Created: " + pfiles + "/ryeShell/cache **");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Do you accept the Terms and Conditions of the Rye language?\nGNU General Public License V3:\n");
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("http://www.gnu.org/licenses/gpl.txt", @pfiles + "/ryeShell/gpl.txt");
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(pfiles + "/ryeShell/gpl.txt"))
                    {
                        gpl = sr.ReadToEnd();
                        sr.Dispose();
                    }
                    Console.WriteLine(gpl);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Accept License?\n# ");
                    choice = Console.ReadLine();
                    if (choice != "yes")
                        Environment.Exit(0);
                    Console.ForegroundColor = ConsoleColor.White;
                    using (System.IO.StreamWriter sw = new StreamWriter("sts"))
                    {
                        Console.WriteLine("Created: " + Program.ExecCmd("cd " + pfiles + "/ryeShell" + "&& mkdir files") + pfiles + "/files\n");
                        sw.Write("accept");
                        sw.Dispose();
                    }
                    Console.WriteLine("Shell password protection (enter a password of atleast 7 characters): ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("# ");
                    sys_settings.password = Console.ReadLine();
                    sys_settings.password_length = sys_settings.password.Length;
                    if (sys_settings.password_length >= 7
                        && sys_settings.password != null
                        && sys_settings.password.IndexOf(" ") == (-1)
                        && sys_settings.password != "")
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(pfiles + "/ryeShell/" + "security.shbin"))
                        {
                            try
                            {
                                sw.WriteLine(sys_settings.password);
                                sw.WriteLine(sys_settings.password_length.ToString());
                                if (Environment.Is64BitOperatingSystem == true)
                                    sys_settings.std = true;
                                else { sys_settings.std = false; }
                                sw.WriteLine(sys_settings.std.ToString());

                                SHA1 sha1 = new SHA1CryptoServiceProvider();
                                string hashsum = string.Empty;
                                byte[] data = sha1.ComputeHash(Encoding.Unicode.GetBytes(sys_settings.password));
                                foreach (byte i in data)
                                {
                                    hashsum += String.Format("{0,2:X2}", i);
                                }
                                sw.WriteLine(hashsum);
                                sw.Dispose();
                                Environment.Exit(0);
                            }

                            catch
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\nError! Please try again!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid password! Second and LAST try:\n");
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(pfiles + "/ryeShell/" + "password.shbin"))
                        {
                            
                                sw.WriteLine(sys_settings.password);
                                sw.WriteLine(sys_settings.password_length.ToString());
                                if (Environment.Is64BitOperatingSystem == true)
                                    sys_settings.std = true;
                                else { sys_settings.std = false; }
                                sw.WriteLine(sys_settings.std.ToString());

                                SHA1 sha1 = new SHA1CryptoServiceProvider();
                                string hashsum = string.Empty;
                                byte[] data = sha1.ComputeHash(Encoding.Unicode.GetBytes(sys_settings.password));
                                foreach (byte i in data)
                                {
                                    hashsum += String.Format("{0,2:X2}", i);
                                }
                                sw.WriteLine(hashsum);
                                sw.Dispose();                        

                            
                        }
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
               
            }
                
            return 0;
        }

       
    }
}
