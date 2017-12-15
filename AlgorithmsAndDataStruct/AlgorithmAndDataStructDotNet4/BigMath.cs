using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics;

class BigMath
{
    public static void Test()
    {
        //BigRational test = new BigRational();
        //test = 2;
        //Debug.Log(test.ToString());
        //Debug.Log(((Double)test).ToString());
        //Debug.Log(((int)test).ToString());

        BigRational result = CalculatePi_BBP_BigFraction();
        Debug.Log(result.ToString());
        Debug.Log(((float)result).ToString());
        Debug.Log(((double)result).ToString());
        Debug.Log(((decimal)result).ToString());
    }

    public static BigRational CalculatePi_BBP_BigFraction()
    {
        BigFraction pi = BigFraction.Zero;
        int maxN = 30;
        for (int k = 0; k < maxN; ++k)
        {
            BigFraction par1 = new BigFraction(1, 1);
            for (int i = 0; i < k; ++i )
            {
                par1 *= 16;
            }
            par1 = 1 / par1;
            pi += par1 * (new BigFraction(4, 8 * k + 1) - new BigFraction(2, 8 * k + 4) - new BigFraction(1, 8 * k + 5) - new BigFraction(1, 8 * k + 6));
        }
        return pi.ToBigRational();
    }
}

