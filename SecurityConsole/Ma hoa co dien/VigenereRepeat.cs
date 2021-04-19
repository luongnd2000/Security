using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class VigenereRepeat
    {
        static string keys = "abcdefghijklmnopqrstuvwxyz";
        public static string Encrypt(string key,string plaintext)
        {
            string encode="";
            string key_repeat="";
            for(int i = 0; i < plaintext.Length; i++)
            {
                key_repeat += key[i % key.Length];
            }
            for (int i = 0; i < plaintext.Length; i++)
            {
               int j =( keys.IndexOf(key_repeat[i]) + keys.IndexOf(plaintext[i]))%keys.Length;
                encode += keys[j];
            }
            return encode;
        }
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(Encrypt("WHENINRO".ToLower(), "ALLWORKANDNOPLAYMA".ToLower()).ToUpper());
        //}
    }
}
