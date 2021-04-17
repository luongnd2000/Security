using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole
{
    class ModuloBase
    {
        //Tinh modulo bang cach ha bac luy thua
        public static int Power(int x, int y, int p)
        {
            int res = 1;
            x = x % p;
            if (x == 0) return 0;

            while (y > 0)
            {
                if ((y & 1) != 0)
                {
                    res = (res * x) % p;
                }
                y = y >> 1;
                x = (x * x) % p;
            }
            return res;
        }
        public string Modulo(int a, int b, int x, int y, int n)
        {
            int A1, A2, A3, A4, A5;
            A1 = (Power(a, x, n) + Power(b, y, n)) % n;
            A2 = (Power(a, x, n) - Power(b, y, n) + n) % n;
            A3 = (Power(a, x, n) * Power(b, y, n)) % n;
            A4 = EulerMethod.ModuloReverse(Power(b, y, n), n);
            A5 = (Power(a, x, n) * A4) % n;
            return "A1 = " + A1 + "\nA2 = " + A2 + "\nA3 = " + A3 + "\nA4 = " + A4 + "\nA5 = " + A5;
        }

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(power(300, 34, 2555));
        //}

    }
}
