using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    //Mat ma ceasar
    class CeasarCipher
    {
        public static StringBuilder encrypt(String text, int s)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    char ch = (char)(((int)text[i] + s - 65) % 26 + 65);
                    result.Append(ch);
                }
                else
                {
                    char ch = (char)(((int)text[i] + s - 97) % 26 + 97);
                    result.Append(ch);
                }
            }
            return result;
        }
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(encrypt("ALLWORKANDNOPLAYMA", 4));
        //}
    }
}
