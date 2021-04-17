using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityConsole
{
    class DESCipher
    {
        public string K { get; set; }
        private string M { get; set; }
        private static int[] s = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
        private static int[,] PC1 = {
            {57,49,41,33,25,17,9,1},
            {58,50,42,34,26,18,10,2},
            {59,51,43,35,27,19,11,3},
            {60,52,44,36,63,55,47,39},
            {31,23,15,7,62,54,46,38},
            {30,22,14,6,61,53,45,37},
            {29,21,13,5,28,20,12,4}
    };

        private static int[,] PC2 = {
            {14,17,11,24,1,5},
            {3,28,15,6,21,10},
            {23,19,12,4,26,8},
            {16,7,27,20,13,2},
            {41,52,31,37,47,55},
            {30,40,51,45,33,48},
            {44,49,39,56,34,53},
            {46,42,50,36,29,32}
    };

        private static int[,] IP = {
            {58,50,42,34,26,18,10,2},
            {60,52,44,36,28,20,12,4},
            {62,54,46,38,30,22,14,6},
            {64,56,48,40,32,24,16,8},
            {57,49,41,33,25,17,9,1},
            {59,51,43,35,27,19,11,3},
            {61,53,45,37,29,21,13,5},
            {63,55,47,39,31,23,15,7}
    };

        private static int[,] IP_1 = {
            {40, 8, 48, 16, 56, 24, 64, 32},
            {39, 7, 47, 15, 55, 23, 63, 31},
            {38, 6, 46, 14, 54, 22, 62, 30},
            {37, 5, 45, 13, 53, 21, 61, 29},
            {36, 4, 44, 12, 52, 20, 60, 28},
            {35, 3, 43, 11, 51, 19, 59, 27},
            { 34, 2, 42, 10, 50, 18, 58, 26},
            {33, 1, 41, 9, 49, 17, 57, 25}
    };

        private static int[,] E = {
            {32,1,2,3,4,5},
            {4,5,6,7,8,9},
            {8,9,10,11,12,13},
            {12,13,14,15,16,17},
            {16,17,18,19,20,21},
            {20,21,22,23,24,25},
            {24,25,26,27,28,29},
            {28,29,30,31,32,1}
    };
        private static int[,,] S = {
            {
                {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
                {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
                {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
                {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}
            },
            {
                {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9},
            },
            {
                {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12},
            },
            {
                {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14},
            },
             {
                {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3},
             },
             {
                    {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                    {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                    {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                    {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13},
             },
             {
                    {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                    {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                    {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                    {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12},
             },
             {
                    {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                    {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                    {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                    {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11},
             },
    };

        private static int[,] P = {
            {16, 7, 20, 21},
            {29, 12, 28, 17},
            {1, 15, 23, 26},
            {5, 18, 31, 10},
            { 2, 8, 24, 14},
            { 32, 27, 3, 9},
            {19, 13, 30, 6},
            {22, 11, 4, 25}
    };
        private List<string> C = new List<string>();
        private List<string> D = new List<string>();
        private List<string> Key = new List<string>();
        private List<string> L = new List<string>();
        private List<string> R = new List<string>();
        private List<string> LD = new List<string>();
        private List<string> RD = new List<string>();
        public DESCipher(string K, string M)
        {
            this.K = K;
            this.M = M;
        }
        public string hexToBin(string value)
        {
            string binarystring = string.Join(string.Empty,value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            int length = binarystring.Length;
            if (length < 64)
            {
                for (int i = 0; i < 64 - length; i++)
                {
                    binarystring = '0' + binarystring;
                }
            }
            return binarystring;
        }
        public string decToBin(int value)
        {
            string result = Convert.ToString(value, 2);
            int length = result.Length;
            if (result.Length < 4)
            {
                for (int i = 0; i < 4 - length; i++)
                {
                    result = '0' + result;
                }
            }
            return result;
        }
        public long BinToDex(string bin)
        {
            return Convert.ToInt64(bin, 2);
        }
        public string binToHex(string bin)
        {
            return Convert.ToInt64(bin, 2).ToString("X");
        }
        public string permutation(string value, int [,] a,int row,int col)
        {
            //for (int i = 0; i < row; i++)
            //{
            //    for (int j = 0; j < col; j++)
            //    {
            //        Console.Write(a[i, j] + " ");
            //    }
            //    Console.WriteLine("");
            //}
            string result = "";
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                        result += value.ElementAt(a[i, j] - 1);
                }
            }
            return result;
        }
        public string rotLeftShift(string value, int number)
        {
            string left = value.Substring(0, number);
            string right = value.Substring(number);
            return right + left;
        }
        public void setUpCD()
        {
            string result = permutation(hexToBin(K), PC1,7,8);
            C.Add(result.Substring(0, 28));
            D.Add(result.Substring(28));
            for (int i = 0; i < s.Length; i++)
            {   
                C.Add(rotLeftShift(C[i], s[i]));
            }
            for (int i = 0; i < s.Length; i++)
            {
                D.Add(rotLeftShift(D[i], s[i]));
            }
        }
        public void setUpKey()
        {
            string CD = "";
            for (int i = 1; i < C.Count; i++)
            {
                CD = C[i] + D[i];
                Key.Add(permutation(CD, PC2,8,6));
            }
        }
        public void setUpLR()
        {
            string IPM = permutation(hexToBin(M), IP,8,8);
            L.Add(IPM.Substring(0, 32));
            R.Add(IPM.Substring(32));
            for (int i = 0; i < 16; i++)
            {
                L.Add(R[i]);
                R.Add(xor(this.functionF(R[i], Key[i]), L[i]));
            }
        }
        public string xor(string bin1, string bin2)
        {
            string result = "";
            for (int i = 0; i < bin1.Length; i++)
            {
                result += int.Parse(bin1.ElementAt(i).ToString()) ^ int.Parse(bin2.ElementAt(i).ToString());
            }
            return result;
        }

        public string SBox(string value)
        {
            List<string> values = new List<string>();
            for (int z = 0; z < value.Length; z += 6)
            {
                values.Add(value.Substring(z,6));
            }
            string result = "";
            int i = 0;
            foreach (string x in values)
            {
                string row = x.ElementAt(0).ToString() + x.ElementAt(x.Length - 1).ToString();
                if ("00".Equals(row))
                {
                    result += decToBin(S[i,0,BinToDex(x.Substring(1, 4))]);
                }
                else if ("01".Equals(row))
                {
                    long indexx = BinToDex(x.Substring(1, 4));
                    result += decToBin(S[i,1,BinToDex (x.Substring(1, 4))]);
                }
                else if ("10".Equals(row))
                {
                    result +=decToBin(S[i,2,BinToDex(x.Substring(1, 4))]);
                }
                else
                {
                    result += decToBin(S[i,3,BinToDex (x.Substring(1, 4))]);
                }
                i++;
            }
            return result;
        }
        public string functionF(string R, string K)
        {
            string ER =permutation(R, E,8,6);
            string Sbox = this.SBox(this.xor(ER, K));
            Console.WriteLine("A = " +binToHex(this.xor(R, K)) + " Sbox: " + binToHex(Sbox));
            string PSB = permutation(Sbox, P,8,4);
            return PSB;
        }
        public String encrypt()
        {
            this.setUpCD();
            this.setUpKey();
            this.setUpLR();
            return permutation(R[R.Count - 1] + L[L.Count - 1], IP_1,8,8);
        }
        public string decrypt(string ciphertext)
        {
            string IPD = permutation(hexToBin(ciphertext), IP,8,8);
            LD.Add(IPD.Substring(0, 32));
            RD.Add(IPD.Substring(32));
            for (int i = 0; i < 16; i++)
            {
                LD.Add(RD[i]);
                RD.Add(this.xor(this.functionF(RD[i], Key[16 - i - 1]), LD[i]));
            }
            return permutation(RD[RD.Count - 1] + LD[LD.Count- 1], IP_1,8,8);
        }
        //public static void Main(string[] args)
        //{
        //    DESCipher des = new DESCipher("F7918DFD6815020C", "D8B8217DA16D5B5F");
        //    Console.WriteLine("TEST Xor " + des.xor("110101", "011001"));
        //    string ciphertext = des.encrypt();
        //    Console.WriteLine("Chuoi da ma hoa: " + des.binToHex(ciphertext));
        //    Console.WriteLine("Giai ma: " + des.binToHex(des.decrypt(des.binToHex(ciphertext))));
 
        //    Console.WriteLine("Khoa: ");
        //    for (int i = 1; i <= des.Key.Count; i++)
        //    {
        //        Console.WriteLine("K" + i + " = " + des.binToHex(des.Key[i - 1]));
        //    }
        //    Console.WriteLine("L,R: ");
        //    for (int i = 0; i < des.R.Count; i++)
        //    {
        //        Console.WriteLine("L" + i + " = " + des.binToHex(des.L[i]) + "       " + "R" + i + " = " + des.binToHex(des.R[i]));
        //    }
        //}

    }
}
