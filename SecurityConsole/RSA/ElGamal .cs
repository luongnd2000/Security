using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.RSA
{
    class ElGamal
    {
        int q;
        int a;
        int xA;
        int k;
        int M;
        public ElGamal(int q, int a, int xA,int k,int M)
        {
            this.q = q;
            this.a = a;
            this.xA = xA;
            this.k = k;
            this.M = M;
        }
        public int yA
        {
            get
            {
                return ModuloBase.Power(a,xA,q);
            }
        }
        public int[] GetPU()
        {
            int[] PU = { q,a,yA};
            return PU;
        }
        public int K
        {
            get
            {
                return ModuloBase.Power(yA,k,q);
            }
        }
        public int C1
        {
            get
            {
                return ModuloBase.Power(a, k, q);
            }
        }
        public int C2
        {
            get
            {
                return (K * M) % q;
            }
        }
        public int DecryptC1C2()
        {
            int key = ModuloBase.Power(C1, C2, q);
            Console.WriteLine("Key : " + key);
            int Mdecrypt = (C2 * EulerMethod.ModuloReverse(key, q)) % q;
            return Mdecrypt;
        }
        public void Solve()
        {
            int[] PU = GetPU();
            Console.WriteLine("PU={" + PU[0] + "," + PU[1] + "," + PU[2] + "}");
            Console.WriteLine("B encrypt M : (C1,C2)=(" + C1 + "," + C2 + ")");
            Console.WriteLine("A decrypt (C1,C2) : M=" + DecryptC1C2());
        }
        //public static void Main(string [] args)
        //{
        //    ElGamal elgamal = new ElGamal(19, 10,5, 6, 17);
        //    elgamal.Solve();
        //}
    }
}
