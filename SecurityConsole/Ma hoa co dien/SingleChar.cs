using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class SingleChar
    {
        //ma hoa chu don
        static string keys="abcdefghijklmnopqrstuvwxyz";
        public static string Encrypt(string key,string plaintext)
        {
            string result = "";
            foreach(char i in plaintext)
            {
                result += key[keys.IndexOf(i)];
            }
            return result;
        }
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine("ALLWORKANDNOPLAYMA".Length);
        //        string s=Encrypt("LGRJOCFIKAHQYTEMNBWUVSDPXZ".ToLower(), "ALLWORKANDNOPLAYMA".ToLower()).ToUpper();
        //    Console.WriteLine(s.Length);
        //}
    }
}
