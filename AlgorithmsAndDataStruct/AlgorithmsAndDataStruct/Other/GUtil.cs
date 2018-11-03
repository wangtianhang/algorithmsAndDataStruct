using System;
using System.Collections.Generic;
using System.Text;


class GUtil
{
    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}

