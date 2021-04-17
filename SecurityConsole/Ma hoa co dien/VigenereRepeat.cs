using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class VigenereRepeat
    {
        static string keys = "abcdefghijklmnopqrstuvwxyz";
        public VigenereRepeat()
        {
            
        }
        public static string Encrypt(string key,string plaintext)
        {
            string result = "";
            string key_repeat = "";
            for(int i = 0; i < plaintext.Length; i++)
            {
                key_repeat += key[i % key.Length];
            }
            for (int i = 0; i < plaintext.Length; i++)
            {
                int j = keys.IndexOf(key_repeat[i]) + keys.IndexOf(plaintext[i]) %keys.Length;
                result += keys[j];
            }
            return result;
        }
        //public static void Main(string [] args)
        //{
        //    string plaintext = Console.ReadLine();
        //    string key = Console.ReadLine();
        //    Console.WriteLine(Encrypt(key, plaintext));
        //}

    }
}
