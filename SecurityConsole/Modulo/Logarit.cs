using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.Modulo
{
    class Logarit
    {
        // k=log,a,(b) mod n
        public static int LogarithmRoiRac(int a, int b, int n)
        {
            for (int i = 1; i < n; i++)
            {
                if (b == ModuloBase.Power(a, i, n))
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
