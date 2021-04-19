using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class VigenereAuto
    {
        static string keys = "abcdefghijklmnopqrstuvwxyz";
        public static string Encrypt(string key, string plaintext)
        {
            string encode = "";
            string key_repeat = key+plaintext;
            
            for (int i = 0; i < plaintext.Length; i++)
            {
                int j = (keys.IndexOf(key_repeat[i]) + keys.IndexOf(plaintext[i])) % keys.Length;
                encode += keys[j];
            }
            return encode;
        }
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine("Auto key");
        //    Console.WriteLine(Encrypt("SAVEFORA".ToLower(), "ALLWORKANDNOPLAYMA".ToLower()).ToUpper());
        //}

    }
}
