/* This program is Copyright (C) 2011 KryptonX. All rights reserved */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace ryeSh
{
    class Program
    {
        static void Main(string[] args)
        {                    
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("(* Rye Shell *)\n");
            Console.Write("(* type:\tlearn()\tlegal()\tsource()\t*)\n---------------------------------------------------\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            int cc = 0;     /* Set counter to 0 */
            setup.sys_setup();
            if (security.verify() != 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Incorrect hash sum! Access denied!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\nI will delete the entire cache so you can re-configure.\n...");                
                Console.ReadLine();
                security.reconfigure();
            }
            do
            {
                Console.Write(">>> ");  /* Start every line with >>> */
                string c = Console.ReadLine().ToString();   /* Read input and set to c */
                if (c == "learn()")
                {
                    System.Diagnostics.Process.Start("http://kryptonx.webs.com/rye/docs/win/rye_win.pdf");
                } else if (c == "legal()")
                    System.Diagnostics.Process.Start("http://kryptonx.webs.com/rye/");
                else if (c == "source()")
                {
                    System.Diagnostics.Process.Start("http://github.com/krypton-project/rye/");
                }
                else
                {
                    string[] cout = c.Split(' ');                   /* Split c into an array */
                    Console.WriteLine(ExecCmd("rye -r " + c));    /* Initialize interpreter */

                }
            } while (cc < 5);   /* Forever */
        }
        public static string ExecCmd(string cmd)
        {
            /**
             * http://www.codeproject.com/KB/cs/Execute_Command_in_CSharp.aspx
             * 
             * Thanks to CodeProject!
             * 
             */

            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                   new System.Diagnostics.ProcessStartInfo("cmd", "/c " + cmd);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                return result;

            }
            catch (Exception objException)
            {
                // Log the exception
                return "caught";
            }

        }
    }
}
