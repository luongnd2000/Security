using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class ChinesRemainder
    {
        //So du trung hoa
        static int FindMinX(int[] num, int[] rem,
                            int k)
        {
            int x = 1;
            while (true)
            {
                int j;
                for (j = 0; j < k; j++)
                    if (x % num[j] != rem[j])
                        break;
                if (j == k)
                    return x;
                x++;
            }

        }

        // Driver code
        //public static void Main()
        //{
        //    // rem[i] mod[i] num = x
        //    int[] num = { 17, 37, 71 };
        //    int[] rem = { 13, 10, 30 };
        //    int k = num.Length;
        //    Console.WriteLine("x is " + FindMinX(num, rem, k));
        //}
    }
}
