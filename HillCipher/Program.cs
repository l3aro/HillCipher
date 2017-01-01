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
            int inverse = HillCipher.MMI(5);

            Console.ReadKey();
        }
    }
}
