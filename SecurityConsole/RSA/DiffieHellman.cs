using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.RSA
{
    class DiffieHellman
    {
        int q;
        int a;
        int xA;
        int xB;

        public DiffieHellman(int q, int a, int xA, int xB)
        {
            this.q = q;
            this.a = a;
            this.xA = xA;
            this.xB = xB;
        }

        public int yA
        {
            get
            {
                return ModuloBase.Power(a, xA, q);
            }
        }
        public int yB
        {
            get
            {
                return ModuloBase.Power(a, xB, q);
            }
        }
        public int KeyByA
        {
            get
            {
                return  ModuloBase.Power(yB, xA, q);
            }
        }
        public int KeyByB
        {
            get
            {
                return ModuloBase.Power(yA, xB, q);
            }
        }
        public void Solve()
        {
            Console.WriteLine("yA = " + yA);
            Console.WriteLine("yB = " + yB);
            Console.WriteLine("Key by A = " + KeyByA);
            Console.WriteLine("Key by B = " + KeyByB);

        }
        //public static void Main(string[] args)
        //{
        //    DiffieHellman obj = new DiffieHellman(353, 3, 97, 233);
        //    obj.Solve();
        //}
    }
}
