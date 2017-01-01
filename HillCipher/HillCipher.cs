using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillCipher
{
    class HillCipher
    {

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
    }
}
