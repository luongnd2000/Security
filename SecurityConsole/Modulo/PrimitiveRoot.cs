using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.Modulo
{
    class PrimitiveRoot
    {
        public static bool IsPrime(int n)
        {
            if (n <= 1)
                return false;
            else if (n == 2)
                return true;
            else if (n % 2 == 0)
                return false;
            for (int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
        public static bool isCanNguyenThuy(int a, int n)
        {
            int phiN = EulerMethod.phi(n);
            if (1 == ModuloBase.Power(a, phiN, n))
            {
                for (int i = 1; i < phiN; i++)
                {
                    if (phiN % i == 0)
                    {
                        if (1 ==ModuloBase.Power(a, i, n))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(isCanNguyenThuy(15, 397));
        //    Console.WriteLine(isCanNguyenThuy(11, 331));
        //    Console.WriteLine(isCanNguyenThuy(5, 463));
        //}
    }
}
