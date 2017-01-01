using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            HillCipher HillCipher = new HillCipher();

            Console.Write("Nhap van ban can ma hoa: ");
            string plainText = Console.ReadLine();

            plainText = HillCipher.RemoveNonZ26(plainText);
            

            Console.ReadKey();
        }
    }
}
