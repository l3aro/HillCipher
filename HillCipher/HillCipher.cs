using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillCipher
{
    class HillCipher
    {
        private int det; // determinant of a matrix (2x2 is a*d-b*c)
        private int inverseOfDet; // modular multiplicative inverse of det modulo 26
        private int[][] key; // the key matrix
        private int[][] inverseOfKey { get; } // the inverse matrix of key 

        public HillCipher()
        {
            key = new int[2][];
            inverseOfKey = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                key[i] = new int[2];
                inverseOfKey[i] = new int[2];
            }

            setKey();
        }

        private void setKey()
        {
            Console.Write("Nhap key:\nBo trong neu muon he thong chon ngau nhien\nHoac nhap vao lan luot 4 so lon hon 0 va nho hon 26:\n");
            do
            {
                // import data from keyboard
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        bool isContinue = true;
                        do
                        {
                            string temp = Console.ReadLine();
                            try
                            {
                                key[i][j] = int.Parse(temp);
                            }
                            catch
                            {
                                // if fail to parse => not number
                                Console.Write("Ban vua nhap mot ky tu khong hop le! Nhap lai: ");
                                continue;
                            }
                            if (key[i][j] < 1 || key[i][j] > 25)
                            {
                                Console.Write("Ban vua nhap mot so khong hop le! Nhap lai: ");
                                continue;
                            }
                            isContinue = false;
                        }
                        while (isContinue);
                    }
                }

                // check check if exists inverse number of det of key matrix
                det = (key[0][0] * key[1][1] - key[1][0] * key[0][1]);

                det = det % 26;

                if (det < 0)
                {
                    det += 26;
                }
                    
                inverseOfDet = MMI(det);
                if (inverseOfDet != 0)
                {
                    break;
                }
                Console.WriteLine("ma tran nhap vao khong kha nghich! Nhap lai:");
            }
            while (true);

            // calculate inverse key matrix
            // 1st, we set the innate value as adjugate matrix value
            // adju(a, b, c, d) = (d, -b, -c, a)
            inverseOfKey[0][0] = key[1][1];
            inverseOfKey[0][1] = key[0][1] * (-1);
            inverseOfKey[1][0] = key[1][0] * (-1);
            inverseOfKey[1][1] = key[0][0];


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    inverseOfKey[i][j] = inverseOfKey[i][j] * inverseOfDet % 26;
                    if (inverseOfKey[i][j] < 0)
                    {
                        inverseOfKey[i][j] += 26;
                    }
                }
            }            
        }

        // Find Modular Multiplicative Inverse of a number modulo 26
        public int MMI(int number)
        {
            int modulo = 26;
            int y0 = 0;
            int y1 = 1;
            int q = modulo / number;
            int y = y0 - y1 * q;
            int r = modulo % number;
            int negative = modulo;

            while (number > 0)
            {
                modulo = number;
                number = r;
                y0 = y1;
                y1 = y;
                r = modulo % number;
                if (r == 0)
                {
                    break;
                }
                q = modulo / number;
                y = y0 - y1 * q;
            }

            if (number > 1)
                return 0;

            if (y < 0)
                y += negative;
            return y;
        }

        // Remove characters not in Z26 alphabet
        private string RemoveNonZ26(string raw)
        {
            raw = raw.ToUpper();
            do
            {
                int i = 0;
                for (; i < raw.Length; i++)
                {
                    int ch = (int)raw[i];
                    if (ch < 65 || ch > 90)
                    {
                        raw = raw.Remove(i, 1);
                        break;
                    }
                }
                if (i == raw.Length)
                {
                    break;
                }
            }
            while (true);

            return raw;

        }

        // Encrypt data
        public string Encrypt(string plainText)
        {
            plainText = RemoveNonZ26(plainText);

            if (plainText.Length % 2 != 0)
            {
                plainText += "Z";
            }

            byte[] plainTextData = Encoding.ASCII.GetBytes(plainText);

            for (int i = 0; i < plainText.Length; i += 2)
            {
                int result1 = (plainTextData[i] - 65) * key[0][0] + (plainTextData[i + 1] - 65) * key[0][1];
                int result2 = (plainTextData[i] - 65) * key[1][0] + (plainTextData[i + 1] - 65) * key[1][1];
                result1 = result1 % 26;
                result2 = result2 % 26;
                plainTextData[i] = (byte)(result1 + 65);
                plainTextData[i + 1] = (byte)(result2 + 65);
            }

            return Encoding.ASCII.GetString(plainTextData);
        }

        // Decrypt data
        public string Decrypt(string encrypted)
        {
            encrypted = RemoveNonZ26(encrypted);

            if (encrypted.Length % 2 != 0)
            {
                encrypted += "Z";
            }

            byte[] encryptedData = Encoding.ASCII.GetBytes(encrypted);

            for (int i = 0; i < encrypted.Length; i += 2)
            {
                int result1 = (encryptedData[i] - 65) * inverseOfKey[0][0] + (encryptedData[i + 1] - 65) * inverseOfKey[0][1];
                int result2 = (encryptedData[i] - 65) * inverseOfKey[1][0] + (encryptedData[i + 1] - 65) * inverseOfKey[1][1];
                result1 = result1 % 26;
                result2 = result2 % 26;
                encryptedData[i] = (byte)(result1 + 65);
                encryptedData[i + 1] = (byte)(result2 + 65);
            }

            return Encoding.ASCII.GetString(encryptedData);
        }
    }
}
