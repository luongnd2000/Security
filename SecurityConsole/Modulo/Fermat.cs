using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.Modulo
{
    class Fermat
    {
        public static int Power(int a, int b, int n)
        {
            int b1;
            if (PrimitiveRoot.IsPrime(n) && a > 0)
            {
                b1 = b % (n - 1);
                return ModuloBase.Power(a, b1, n);
            }
            return -1;
        }
    }
}
