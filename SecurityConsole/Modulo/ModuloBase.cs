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
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(power(300, 34, 2555));
        //}

    }
}
