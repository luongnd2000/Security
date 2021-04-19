using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityConsole.RSA
{
    class DSS
    {
        int H_M;
        int p;
        int q;
        int h;
        int xA;
        int k;

        public DSS(int h_M, int p, int q, int h, int xA, int k)
        {
            H_M = h_M;
            this.p = p;
            this.q = q;
            this.h = h;
            this.xA = xA;
            this.k = k;
        }

        int g
        {
            get
            {
                return ModuloBase.Power(h,(p-1)*q,p);
            }
        }
        int yA {
            get
            {
                return ModuloBase.Power(g,xA,q);
            }
        }
        int r
        {
            get
            {
                return ModuloBase.Power(28, k, p);
            }
        }
        int s
        {
            get
            {
                return (EulerMethod.ModuloInverse(k, q) * (H_M + xA * r)) % q;
            }
        }
        int w
        {
            get
            {
                return EulerMethod.ModuloInverse(k, q) % q;
            }
        }
        int u1
        {
            get
            {
                return H_M*w %q; 
            }
        }
        int u2
        {
            get
            {
                return r*w %q;
            }
        }
        int v
        {
            get
            {
                return (ModuloBase.Power(g,u1,p)* ModuloBase.Power(yA, u2, p))%q;
            }
        }
        bool check
        {
            get
            {
                return v == r;
            }
        }
        //public static void Main(string [] args)
        //{
        //    DSS dss = new DSS(9, 47, 23, 34, 2, 10);
        //    Console.WriteLine(dss.r);
        //    Console.WriteLine(dss.s);
        //}
    }
}
