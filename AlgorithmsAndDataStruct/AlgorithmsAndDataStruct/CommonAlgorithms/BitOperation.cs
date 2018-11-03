using System;
using System.Collections.Generic;
using System.Text;


class BitOperation
{
    public static void Test()
    {
        Debug.Log(NumberOf1(82).ToString());
    }

    //求二进制形式中1的数量
    public static int NumberOf1(int n)
    {
        int count = 0;
        // csharp中没办法用unsigned int 改用long吧
        long flag = 1;
        while (flag != 0)
        {
            if ((n & flag) != 0)
            {
                count++;
            }
            flag = flag << 1;
        }
        return count;
    }

    //求二进制形式中1的数量 另一种方法 这么trick的代码有意义么
    public static int NumberOf1_Another(int n)
    {
        int count = 0;
        while (n != 0)
        {
            ++count;
            n = (n - 1) & n;
        }
        return count;
    }
}

