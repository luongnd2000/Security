using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.RSA
{
    class RSA1
    {
        int p;
        int q;
        int e;

        public RSA1(int p, int q, int e)
        {
            this.p = p;
            this.q = q;
            this.e = e;
        }

        int n
        {
            get
            {
                return p * q;
            }
        }
        int Phi
        {
            get
            {
                return (p - 1) * (q - 1);
            }
        }
        int d
        {
            get
            {
                return EulerMethod.ModuloReverse(e, Phi);
            }
        }
        public int[] GetPU()
        {
            int []a= { e, n};
            return a;
        }
        public int[] GetPR()
        {
            int[] a = { d, n };
            return a;
        }

        public int AEncryptM(int M)
        {
            return ModuloBase.Power(M, d, n);
        }
        public int BDecryptC(int C)
        {
            return ModuloBase.Power(C, e, n);
        }
        public int ADecryptM(int C)
        {
            return ModuloBase.Power(M, d, n);
        }
        public int BEncryptC(int M)
        {
            return ModuloBase.Power(C, e, n);
        }
        public void Solve1()
        {
            int[] PU = GetPU();
            int[] PR = GetPR();
            Console.WriteLine("PU={"+PU[0]+","+PU[1]+"}");
            Console.WriteLine("PR={" + PR[0] + "," + PR[1] + "}");
            int M = 88;
            int C = AEncryptM(M);
            Console.WriteLine("Encrypt from M =  "+M);
            Console.WriteLine("C = " + C);
            Console.WriteLine("M = " + BDecryptC(C));
        }
        public static void Main(string [] args)
        {
            RSA1 rsa1 = new RSA1(17, 11, 7);
            rsa1.Solve1();
        }
    }
}
