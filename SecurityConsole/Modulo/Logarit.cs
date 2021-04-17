using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.Modulo
{
    class Logarit
    {
        public static int logarithmRoiRac(int a, int b, int n)
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
