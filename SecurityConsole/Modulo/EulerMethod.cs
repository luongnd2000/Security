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
        // Tinh Phi(n)
        public static int phi(int n)
        {
            int result = 1;
            for (int i = 2; i < n; i++)
                if (gcd(i, n) == 1)
                    result++;
            return result;
        }
        //Tinh modulo bang euler
        public int Euler(int a, int b, int n)
        {
            int b1, amountA;
            if (coprime(a, n))
            {
                b1 = b % phi(n);
                return ModuloBase.Power(a, b1, n);
            }
            else
            {
                amountA = (int)b / (phi(n) + 1);
                b1 = (b % (phi(n) + 1)) + amountA;
                return ModuloBase.Power(a, b1, n);
            }
        }
        // Tinh Modulo nghich dao
        public static int ModuloInverse(int a, int n)
        {
            for (int i = 1; i < n; i++)
            {
                if (((long)a * i) % n == 1)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool coprime(int a, int b)
        {
            return gcd(a, b) == 1;
        }
        //public static void Main()
        //{
        //    Console.WriteLine(phi(12));
        //    Console.WriteLine(ModuloInverse(550, 1759));
        //}
    }
}
