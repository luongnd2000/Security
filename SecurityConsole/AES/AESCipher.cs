using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SecurityConsole.AES
{
    class AESCipher
    {
        public string K { get; set; }
        public string M;
        public List<string> Key;
        public List<string> State;
        public List<string> inverseState;
        public string w0 { get; set; }
        public string w1 { get; set; }
        public string w2 { get; set; }
        public string w3 { get; set; }

        public AESCipher(string K, string M)
        {
            this.K = K;
            this.M = M;
            Key = new List<string>();
            State = new List<string>();
            inverseState = new List<string>();
        }
        private int[,] S =
        {
            {0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76},
            {0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0},
            {0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15},
            {0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75},
            {0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84},
            {0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF},
            {0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8},
            {0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2},
            {0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73},
            {0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB},
            {0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79},
            {0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08},
            {0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A},
            {0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E},
            {0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF},
            {0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16}
        };

        private int[] Rc = {
            0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8, 0xab, 0x4d, 0x9a,
            0x2f, 0x5e, 0xbc, 0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4, 0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91, 0x39
        };
        private int[,] mix = {
            {2,3,1,1},
            {1,2,3,1},
            {1,1,2,3},
            {3,1,1,2}
        };
        private int[,] inverseMix = {
            {14,11,13,9},
            {9,14,11,13},
            {13,9,14,11},
            {11,13,9,14}
        };
        public string HexToBin(string value)
        {
            Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
                { '0', "0000" },
                { '1', "0001" },
                { '2', "0010" },
                { '3', "0011" },
                { '4', "0100" },
                { '5', "0101" },
                { '6', "0110" },
                { '7', "0111" },
                { '8', "1000" },
                { '9', "1001" },
                { 'a', "1010" },
                { 'b', "1011" },
                { 'c', "1100" },
                { 'd', "1101" },
                { 'e', "1110" },
                { 'f', "1111" }
                };
            StringBuilder result = new StringBuilder();
            foreach (char c in value)
            {
                // This will crash for non-hex characters. You might want to handle that differently.
                result.Append(hexCharacterToBinary[char.ToLower(c)]);
            }
            string binarystring = result.ToString();
            binarystring = binarystring.TrimStart('0');
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
        public string DecToBin(int value)
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
        public string BinToHex(string binary)
        {
            if (binary.Length <= 64) return Convert.ToInt64(binary, 2).ToString("X"); ;
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString().TrimStart('0');
        }
        public string BinToHex(string bin, int maxLength)
        {
            string result = BinToHex(bin);
            int length = result.Length;
            if (result.Length < maxLength)
            {
                for (int i = 0; i < maxLength - length; i++)
                {
                    result = "0" + result;
                }
            }
            return result;
        }
        public string Xor(string bin1, string bin2)
        {
            int max = Math.Max(bin1.Length, bin2.Length);
            int min = Math.Min(bin1.Length, bin2.Length);
            if (max == bin1.Length)
            {
                for (int i = 0; i < max - min; i++)
                {
                    bin2 = "0" + bin2;
                }
            }
            else
            {
                for (int i = 0; i < max - min; i++)
                {
                    bin1 = "0" + bin1;
                }
            }
            //		System.out.println(bin1.length()+" "+bin2.length());
            string result = "";
            for (int i = 0; i < max; i++)
            {
                result += int.Parse(bin1.ElementAt(i).ToString()) ^ int.Parse(bin2.ElementAt(i).ToString());
            }
            return result;
        }
        public string DecToBin(int value, int k)
        {
            string result = Convert.ToString(value, 2);
            int length = result.Length;
            if (result.Length < k)
            {
                for (int i = 0; i < k - length; i++)
                {
                    result = '0' + result;
                }
            }
            return result;
        }
        public int HexToDec(string value)
        {

            return Convert.ToInt32(value, 16); ;
        }
        public string DecToHex(int value)
        {
            String result = value.ToString("X"); ;
            if (result.Length < 2)
            {
                result = "0" + result;
            }
            return result.ToUpper();
        }
        public string timesE(string value)
        {
            value = HexToBin(value);
            string result = Xor(Xor(value + "000", value + "00"), value + "0");
            if (!checkGF(result, 2))
            {
                result = Xor(result, "10001101100");
            }
            if (!checkGF(result, 1))
            {
                result = Xor(result, "1000110110");
            }
            if (!checkGF(result, 0))
            {
                result = Xor(result, "100011011");
            }
            result = and(result, "11111111");
            return result;
        }
        public string timesB(string value)
        {
            value = HexToBin(value);
            string result = Xor(Xor(value + "000", value + "0"), value);
            if (!checkGF(result, 2))
            {
                result = Xor(result, "10001101100");
            }
            if (!checkGF(result, 1))
            {
                result = Xor(result, "1000110110");
            }
            if (!checkGF(result, 0))
            {
                result = Xor(result, "100011011");
            }
            result = and(result, "11111111");
            return result;
        }
        public string timesD(string value)
        {
            value = HexToBin(value);
            string result = Xor(Xor(value + "000", value + "00"), value);
            if (!checkGF(result, 2))
            {
                result = Xor(result, "10001101100");
            }
            if (!checkGF(result, 1))
            {
                result = Xor(result, "1000110110");
            }
            if (!checkGF(result, 0))
            {
                result = Xor(result, "100011011");
            }
            result = and(result, "11111111");
            return result;
        }
        public string times9(string value)
        {
            value = HexToBin(value);
            string result = Xor(value + "000", value);
            if (!checkGF(result, 2))
            {
                result = Xor(result, "10001101100");
            }
            if (!checkGF(result, 1))
            {
                result = Xor(result, "1000110110");
            }
            if (!checkGF(result, 0))
            {
                result = Xor(result, "100011011");
            }
            result = and(result, "11111111");
            return result;
        }
        public string inverseMixColumns(string value)
        {
            string result = "";
            List<string> row = new List<string>();
            for (int i = 0; i < 32; i += 2)
            {
                row.Add(value.Substring(i, 2));
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 16; j += 4)
                {
                    string v1 = "";
                    string v2 = "";
                    string v3 = "";
                    string v4 = "";
                    if (inverseMix[i, 0] == 14)
                    {
                        v1 = timesE(row[j + 0]);
                    }
                    else if (mix[i, 0] == 13)
                    {
                        v1 = timesD(row[j + 0]);
                    }
                    else if (mix[i, 0] == 11)
                    {
                        v1 = timesB(row[j + 0]);
                    }
                    else
                    {
                        v1 = times9(row[j + 0]);
                    }

                    if (inverseMix[i, 1] == 14)
                    {
                        v2 = timesE(row[j + 1]);
                    }
                    else if (mix[i, 1] == 13)
                    {
                        v2 = timesD(row[j + 1]);
                    }
                    else if (mix[i, 1] == 11)
                    {
                        v2 = timesB(row[j + 1]);
                    }
                    else
                    {
                        v2 = times9(row[j + 1]);
                    }

                    if (inverseMix[i, 2] == 14)
                    {
                        v3 = timesE(row[j + 2]);
                    }
                    else if (mix[i, 2] == 13)
                    {
                        v3 = timesD(row[j + 2]);
                    }
                    else if (mix[i, 2] == 11)
                    {
                        v3 = timesB(row[j + 2]);
                    }
                    else
                    {
                        v3 = times9(row[j + 2]);
                    }

                    if (inverseMix[i, 3] == 14)
                    {
                        v4 = timesE(row[j + 3]);
                    }
                    else if (mix[i, 3] == 13)
                    {
                        v4 = timesD(row[j + 3]);
                    }
                    else if (mix[i, 3] == 11)
                    {
                        v4 = timesB(row[j + 3]);
                    }
                    else
                    {
                        v4 = times9(row[j + 3]);
                    }

                    Console.WriteLine("V1 : " + BinToHex(v1, 2));
                    Console.WriteLine("V2 : " + BinToHex(v2, 2));
                    Console.WriteLine("V3 : " + BinToHex(v3, 2));
                    Console.WriteLine("V4 : " + BinToHex(v4, 2));
                    Console.WriteLine("asdfasdfasdfasdf--->" + BinToHex(Xor(Xor(Xor(v1, v2), v3), v4), 2));
                    result += BinToHex(Xor(Xor(Xor(v1, v2), v3), v4), 2);
                }
            }
            Console.WriteLine("Result ----->" + result);

            List<string> list = new List<string>();
            for (int z = 0; z < result.Length; z += 2)
            {
                list.Add(result.Substring(z, 2));
            }
            string row1 = "";
            string row2 = "";
            string row3 = "";
            string row4 = "";

            for (int z = 0; z < list.Count; z++)
            {
                if (z % 4 == 0)
                {
                    row1 += list[z];
                }
                else if (z % 4 == 1)
                {
                    row2 += list[z];
                }
                else if (z % 4 == 2)
                {
                    row3 += list[z];
                }
                else
                {
                    row4 += list[z];
                }
            }
            return row1 + row2 + row3 + row4;

        }
        public void CutKeyToW(string key)
        {
            w0 = key.Substring(0, 8);
            w1 = key.Substring(8, 16 - 8);
            w2 = key.Substring(16, 24 - 16);
            w3 = key.Substring(24);
            Console.Write($"W0={w0,-10}");
            Console.Write($"W1={w1,-10}");
            Console.Write($"W2={w2,-10}");
            Console.WriteLine($"W3={w3,-10}");
            Console.WriteLine("-------------------------------------------");
        }
        public string getRC(int i)
        {
            return DecToHex(Rc[i]) + "000000";
        }
        public string rotWord(string value, int k, int x)
        {
            string result;
            result = value.Substring(k * x) + value.Substring(0, k * x);
            return result;
        }
        public string subBytes(string value, int[,] a)
        {
            string result = "";
            List<string> s = new List<string>();
            for (int i = 0; i < value.Length; i += 2)
            {
                s.Add(value.Substring(i, 2));
            }
            for (int i = 0; i < s.Count; i++)
            {
                result += DecToHex(a[HexToDec(s[i].ElementAt(0).ToString()), HexToDec(s[i].ElementAt(1).ToString())]);
            }
            return result;
        }
        public string and(string bin1, string bin2)
        {
            int max = Math.Max(bin1.Length, bin2.Length);
            int min = Math.Min(bin1.Length, bin2.Length);
            if (max == bin1.Length)
            {
                for (int i = 0; i < max - min; i++)
                {
                    bin2 = "0" + bin2;
                }
            }
            else
            {
                for (int i = 0; i < max - min; i++)
                {
                    bin1 = "0" + bin1;
                }
            }
            //		System.out.println(bin1.length()+" "+bin2.length());
            string result = "";
            for (int i = 0; i < max; i++)
            {
                result += int.Parse(bin1.ElementAt(i).ToString()) & int.Parse(bin2.ElementAt(i).ToString());
            }
            return result;
        }
        public string shilfRows(string value)
        {
            string result = "";
            List<string> list = new List<string>();
            for (int i = 0; i < value.Length; i += 2)
            {
                list.Add(value.Substring(i, 2));
            }

            string row1 = "";
            string row2 = "";
            string row3 = "";
            string row4 = "";

            for (int i = 0; i < list.Count; i++)
            {
                if (i % 4 == 0)
                {
                    row1 += list[i];
                }
                else if (i % 4 == 1)
                {
                    row2 += list[i];
                }
                else if (i % 4 == 2)
                {
                    row3 += list[i];
                }
                else
                {
                    row4 += list[i];
                }
            }
            row2 = rotWord(row2, 1, 2);
            row3 = rotWord(row3, 2, 2);
            row4 = rotWord(row4, 3, 2);
            for (int i = 0; i < row1.Length; i += 2)
            {
                result += row1.Substring(i, 2) + row2.Substring(i, 2) + row3.Substring(i, 2) + row4.Substring(i, 2);
            }
            return result;
        }
        public string inverseShilfRows(string value)
        {
            string result = "";
            List<string> list = new List<string>();
            for (int i = 0; i < value.Length; i += 2)
            {
                list.Add(value.Substring(i, 2));
            }

            string row1 = "";
            string row2 = "";
            string row3 = "";
            string row4 = "";

            for (int i = 0; i < list.Count; i++)
            {
                if (i % 4 == 0)
                {
                    row1 += list[i];
                }
                else if (i % 4 == 1)
                {
                    row2 += list[i];
                }
                else if (i % 4 == 2)
                {
                    row3 += list[i];
                }
                else
                {
                    row4 += list[i];
                }
            }
            row2 = rotWord(row2, 3, 2);
            row3 = rotWord(row3, 2, 2);
            row4 = rotWord(row4, 1, 2);
            for (int i = 0; i < row1.Length; i += 2)
            {
                result += row1.Substring(i, 2) + row2.Substring(i, 2) + row3.Substring(i, 2) + row4.Substring(i, 2);
            }
            return result;
        }
        public void setUpKey(string key)
        {
            CutKeyToW(key);
            Key.Add(w0 + w1 + w2 + w3);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Tinh khoa K{1 + i}");
                string afterRotWord = rotWord(w3, 1, 2);
                Console.Write($"rw = {afterRotWord,-10}");
                string afterSubWords = subBytes(afterRotWord, S);
                Console.Write($"sw = {afterSubWords,-10}");
                string rc = getRC(i + 1);
                //Console.WriteLine("Rc : " + rc);
                string rcXorAfter = Xor(HexToBin(rc), HexToBin(afterSubWords));
                Console.WriteLine("xcsw = " + BinToHex(rcXorAfter, 8));
                string w4 = Xor(HexToBin(w0), rcXorAfter);
                string w5 = Xor(HexToBin(w1), w4);
                string w6 = Xor(HexToBin(w2), w5);
                string w7 = Xor(HexToBin(w3), w6);
                Console.Write($"w4 = {BinToHex(w4),-12}");
                Console.Write($"w5 = {BinToHex(w5),-12}");
                Console.Write($"w6 = {BinToHex(w6),-12}");
                Console.WriteLine($"w7 = {BinToHex(w7),-12}");
                w0 = BinToHex(w4, 8);
                w1 = BinToHex(w5, 8);
                w2 = BinToHex(w6, 8);
                w3 = BinToHex(w7, 8);
                Key.Add(w0 + w1 + w2 + w3);
                Console.WriteLine($"Khoa Key[{i + 1}]={Key[i + 1]}");
            }
            Console.WriteLine("--------------------------------------------");
        }
        public string mixColumn(string value)
        {
            Console.WriteLine("Start mix : " + value);
            string result = "";
            List<string> row = new List<string>();
            for (int i = 0; i < 32; i += 2)
            {
                row.Add(value.Substring(i, 2));
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 16; j += 4)
                {
                    string v1 = "";
                    string v2 = "";
                    string v3 = "";
                    string v4 = "";

                    if (mix[i, 0] == 1)
                    {
                        v1 = HexToBin(row[j + 0]);
                    }
                    else if (mix[i, 0] == 2)
                    {
                        v1 = HexToBin(row[j + 0]) + "0";
                    }
                    else if (mix[i, 0] == 3)
                    {
                        v1 = Xor(HexToBin(row[j + 0]), HexToBin(row[j + 0]) + "0");
                    }
                    if (mix[i, 1] == 1)
                    {
                        v2 = HexToBin(row[j + 1]);
                    }
                    else if (mix[i, 1] == 2)
                    {
                        v2 = HexToBin(row[j + 1]) + "0";
                    }
                    else if (mix[i, 1] == 3)
                    {
                        v2 = Xor(HexToBin(row[j + 1]), HexToBin(row[j + 1]) + "0");
                    }
                    if (mix[i, 2] == 1)
                    {
                        v3 = HexToBin(row[j + 2]);
                    }
                    else if (mix[i, 2] == 2)
                    {
                        v3 = HexToBin(row[j + 2]) + "0";
                    }
                    else if (mix[i, 2] == 3)
                    {
                        v3 = Xor(HexToBin(row[j + 2]), HexToBin(row[j + 2]) + "0");
                    }
                    if (mix[i, 3] == 1)
                    {
                        v4 = HexToBin(row[j + 3]);
                    }
                    else if (mix[i, 3] == 2)
                    {
                        v4 = HexToBin(row[j + 3]) + "0";
                    }
                    else if (mix[i, 3] == 3)
                    {
                        v4 = Xor(HexToBin(row[j + 3]), HexToBin(row[j + 3]) + "0");
                    }

                    if (!checkGF(v1, 0))
                    {
                        v1 = Xor(v1, "100011011");
                        v1 = and(v1, "11111111");
                    }
                    if (!checkGF(v2, 0))
                    {
                        v2 = Xor(v2, "100011011");
                        v2 = and(v2, "11111111");
                    }
                    if (!checkGF(v3, 0))
                    {
                        v3 = Xor(v3, "100011011");
                        v3 = and(v3, "11111111");
                    }
                    if (!checkGF(v4, 0))
                    {
                        v4 = Xor(v4, "100011011");
                        v4 = and(v4, "11111111");
                    }

                    result += BinToHex(Xor(Xor(Xor(v1, v2), v3), v4), 2);
                }
                //			System.out.println();
            }
            Console.WriteLine("Test result : " + result);
            List<string> list = new List<string>();
            for (int i = 0; i < result.Length; i += 2)
            {
                list.Add(result.Substring(i, 2));
            }

            string row1 = "";
            string row2 = "";
            string row3 = "";
            string row4 = "";

            for (int i = 0; i < list.Count; i++)
            {
                if (i % 4 == 0)
                {
                    row1 += list[i];
                }
                else if (i % 4 == 1)
                {
                    row2 += list[i];
                }
                else if (i % 4 == 2)
                {
                    row3 += list[i];
                }
                else
                {
                    row4 += list[i];
                }
            }
            return row1 + row2 + row3 + row4;
        }
        public bool checkGF(string value, int k)
        {
            int i = 0;
            for (i = 0; i < value.Length; i++)
            {
                if (value.ElementAt(i) != '0')
                {
                    break;
                }
            }
            string value1 = value.Substring(i);
            if (value1.Length >= 9 + k)
            {
                return false;
            }
            else if (value1.Length < 9 + k)
            {
                return true;
            }
            else
            {
                for (int j = value1.Length - 1; j >= 1; j--)
                {
                    if (value1.ElementAt(j) != '0')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public string encrypt()
        {
            State.Add(Xor(HexToBin(K), HexToBin(M)));
            Console.WriteLine($"State = {BinToHex(State[0])}");
            Console.WriteLine();
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"Vong lap thu {i} : ");
                string afterSubBytes1 = subBytes(BinToHex(State[i - 1], 32), S);
                Console.WriteLine("Subbyte : " + afterSubBytes1);
                string afterShilfRows1 = shilfRows(afterSubBytes1);
                Console.WriteLine("Shilfrow : " + afterShilfRows1);
                string mix = mixColumn(afterShilfRows1);
                Console.WriteLine("MixColumn : " + mix);
                State.Add(Xor(HexToBin(Key[i]), HexToBin(mix)));
                Console.WriteLine("Add Round Key :" + BinToHex(State[i], 32));
                Console.WriteLine();
            }

            Console.WriteLine("Vong lap thu cuoi cung ");
            string afterSubBytes = subBytes(BinToHex(State[9], 32), S);
            string afterShilfRows = shilfRows(afterSubBytes);
            State.Add(Xor(HexToBin(Key[10]), HexToBin(afterShilfRows)));
            Console.WriteLine("State = " + BinToHex(State[10]));
            Console.WriteLine("-----------------------------------------------------------------------");
            return BinToHex(State[State.Count - 1], 32);
        }
        //public static void Main(string[] args)
        //{
        //    //KEY:CFD61D489E7C48BC46C9F875C1F04E1B
        //    //M:  18DC9095F9149EDB7323F20E4E462D92

        //    AESCipher aes = new AESCipher("CFD61D489E7C48BC46C9F875C1F04E1B", "18DC9095F9149EDB7323F20E4E462D92");
        //    aes.setUpKey(aes.K);
        //    string encode = aes.encrypt();
        //    Console.WriteLine("Ma hoa : " + encode);
        //}
    }
}
