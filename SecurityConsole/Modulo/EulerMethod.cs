using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class EulerMethod
    {
        static int gcd(int a, int b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }
        //Tinh Phi(n)
        static int phi(int n)
        {
            int result = 1;
            for (int i = 2; i < n; i++)
                if (gcd(i, n) == 1)
                    result++;
            return result;
        }


        static int gcd_extend(int a, int b, ref int x,ref int y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }
            int x1=0, y1=0;
            int gcd = gcd_extend(b, a % b, ref x1,ref y1);
            x = y1;
            y = x1 - (a / b) * y1;
            return gcd;
        }
        //Tinh nghich dao modulo
        public static int ModuloReverse(int n, int m)
        {
            int x=0, y=0;
            if (gcd_extend(n, m, ref x, ref y) != 1) return -1; 
            return (x % m + m) % m; 
        }

        //public static void Main()
        //{
        //    Console.WriteLine(phi(12));
        //    Console.WriteLine(modulo_inverse_euclidean(550, 1759));
        //}
    }
}
