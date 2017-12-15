using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics;
using System.Numerics;

class BigMath
{
    public static void Test()
    {
        //BigRational test = new BigRational();
        //test = 2;
        //Debug.Log(test.ToString());
        //Debug.Log(((Double)test).ToString());
        //Debug.Log(((int)test).ToString());
        string pi = "3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679";

        BigRational result = CalculatePi_BBP_BigFraction();
        Debug.Log(result.ToString());
        Debug.Log(((float)result).ToString());
        Debug.Log(((double)result).ToString());
        Debug.Log(((decimal)result).ToString());
        string piStr = result.ToDecimalString(1000);
        Debug.Log(piStr);
        string lcsPi = AlgorithmsBase.LCS(piStr, pi);
        Debug.Log(lcsPi.Length.ToString());
        Debug.Log(CalculateFactorial(100).ToString());
        Debug.Log(CalculateFibonacci(10).ToString());
    }

    public static BigRational CalculatePi_BBP_BigFraction()
    {
        BigFraction pi = BigFraction.Zero;
        int maxN = 50;
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

    /// <summary>
    /// 阶乘
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger CalculateFactorial(int n)
    {
        BigInteger ret = 1;
        for (int i = 1; i <= n; ++i )
        {
            ret *= i;
        }
        return ret;
    }

    /// <summary>
    /// 斐波那契 超级慢 哈哈
    /// </summary>
    public static BigInteger CalculateFibonacci(int n)
    {
        if(n == 1 || n == 2)
        {
            return 1;
        }
        else
        {
            return CalculateFibonacci(n - 1) + CalculateFibonacci(n - 2);
        }
    }
}

