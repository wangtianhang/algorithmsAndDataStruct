using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 自底向上的归并排序，对链表类序列更高效?
/// </summary>
class MergeBUSort<T>
{
    static T[] aux;
    static Comparer<T> m_comparer;

    public MergeBUSort(Comparer<T> comparer)
    {
        m_comparer = comparer;
    }

    public static void Sort(T[] a)
    {
        int n = a.Length;
        aux = new T[n];
        for (int sz = 1; sz < n; sz = sz + sz )
        {
            for (int lo = 0; lo < n - sz; lo += sz + sz )
            {
                Merge(a, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, n - 1));
            }
        }
    }

    static void Merge(T[] a, int lo, int mid, int hi)
    {
        int i = lo, j = mid + 1;
        for (int k = lo; k <= hi; ++k )
        {
            aux[k] = a[k];
        }
        for (int k = lo; k <= hi; ++k )
        {
            if(i > mid)
            {
                a[k] = aux[j++];
            }
            else if(j > hi)
            {
                a[k] = aux[i++];
            }
            else if(Less(aux[j], aux[i]))
            {
                a[k] = aux[j++];
            }
            else
            {
                a[k] = aux[i++];
            }
        }
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

