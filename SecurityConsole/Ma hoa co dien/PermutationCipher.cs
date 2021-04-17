using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class PermutationCipher
    {
        // mat ma hoan vi
        public static string Encrypt(int n,string plaintext)
        {
            string result = "";
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < plaintext.Length; j++)
                {
                    if (j % n == i)
                    {
                        result += plaintext[j];

                    }

                }
                result += " ";  
            }
            return result;
        }

        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(Encrypt(3, "Monarchy"));
        //}
    }
}
