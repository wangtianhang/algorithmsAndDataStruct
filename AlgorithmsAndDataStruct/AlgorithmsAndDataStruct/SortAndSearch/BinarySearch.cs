using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class BinarySearch<T>
{
    static Comparer<T> m_comparer;

    public BinarySearch(Comparer<T> comparer)
    {
        m_comparer = comparer;
    }

    public static int rank(T key, T[] a)
    {
        int lo = 0;
        int hi = a.Length - 1;
        while(lo <= hi)
        {
            int mid = lo + (hi - lo) / 2;
            if (Less(key, a[mid]))
            {
                hi = mid - 1;
            }
            else if (Less(a[mid], key))
            {
                lo = mid + 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }

    static bool Less(T a, T b)
    {
        int ret = m_comparer.Compare(a, b);
        if (ret > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

