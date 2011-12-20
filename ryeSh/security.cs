using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.IO;

namespace ryeSh
{
    class security
    {
        public static string psword = String.Empty;
        public static string pfiles = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "/ryeShell/";
        public static int verify()
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(pfiles + "security.shbin"))
            {
                psword = sr.ReadLine();                
                for (int i = 1; i < 3; i++)
                    sr.ReadLine();
                string hsum = sr.ReadLine();                
                sr.Dispose();

                SHA1 sha1 = new SHA1CryptoServiceProvider();
                string hashsum = string.Empty;
                byte[] data = sha1.ComputeHash(Encoding.Unicode.GetBytes(psword));
                foreach (byte i in data)
                {
                    hashsum += String.Format("{0,2:X2}", i);
                }

                if (hsum != hashsum)
                    return (-1);
                else
                    return 0;
            }
        }

        public static void reconfigure()
        {
            File.Delete(pfiles + "security" + ".shbin");
            File.Delete(pfiles + "gpl" + ".txt");
            Directory.Delete(pfiles + "files");
            Directory.Delete(pfiles);
        }
    }
}
