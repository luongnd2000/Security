using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.Modulo
{
    class PrimitiveRoot
    {
        static bool isPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }
            if (n <= 3)
            {
                return true;
            }
            if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }

            for (int i = 5; i * i <= n; i = i + 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
            }

            return true;
        }
        static int power(int x, int y, int p)
        {
            int res = 1; 

            x = x % p;

            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    res = (res * x) % p;
                }
                y = y >> 1; 
                x = (x * x) % p;
            }
            return res;
        }

        static void findPrimefactors(HashSet<int> s, int n)
        {
            while (n % 2 == 0)
            {
                s.Add(2);
                n = n / 2;
            }
            for (int i = 3; i <= Math.Sqrt(n); i = i + 2)
            {
                while (n % i == 0)
                {
                    s.Add(i);
                    n = n / i;
                }
            }
            if (n > 2)
            {
                s.Add(n);
            }
        }
        static int findPrimitive(int n)
        {
            HashSet<int> s = new HashSet<int>();
            if (isPrime(n) == false)
            {
                return -1;
            }
            int phi = n - 1;
            findPrimefactors(s, phi);
            for (int r = 2; r <= phi; r++)
            {
                bool flag = false;
                foreach (int a in s)
                {
                    if (power(r, phi / (a), n) == 1)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    Console.WriteLine(r);
                }
            }
            return -1;
        }
        //public static void Main(string[] args)
        //{
        //    int n = 761;
        //    Console.WriteLine(" Smallest primitive root of " + n
        //            + " is " + findPrimitive(n));
        //}
    }
}
