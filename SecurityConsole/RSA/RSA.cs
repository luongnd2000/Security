using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.RSA
{
    class RSA
    {
        int p;
        int q;
        int e;

        public RSA(int p, int q, int e)
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
        public int BEncryptM(int M)
        {
            return ModuloBase.Power(M, e, n);
        }
        public int BDecryptC(int C)
        {
            return ModuloBase.Power(C, e, n);
        }
        public int ADecryptC(int C)
        {
            return ModuloBase.Power(C, d, n);
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
            Console.WriteLine("A Encrypt M : C = " + C);
            Console.WriteLine("B Decrypt C : M = " + BDecryptC(C));
        }
        public void Solve2()
        {
            int[] PU = GetPU();
            int[] PR = GetPR();
            Console.WriteLine("PU={" + PU[0] + "," + PU[1] + "}");
            Console.WriteLine("PR={" + PR[0] + "," + PR[1] + "}");
            int M = 88;
            int C = BEncryptM(M);
            Console.WriteLine("Encrypt from M =  " + M);
            Console.WriteLine("B Encrypt M : C = " + C);
            Console.WriteLine("A Decrypt C : M = " + ADecryptC(C));
        }
        //public static void Main(string [] args)
        //{
        //    RSA rsa = new RSA(17, 11, 7);
        //    Console.WriteLine("Bai Toan 1 : ");
        //    rsa.Solve1();
        //    Console.WriteLine();
        //    Console.WriteLine("Bai Toan 2 : ");
        //    rsa.Solve2();
        //}
    }
}
