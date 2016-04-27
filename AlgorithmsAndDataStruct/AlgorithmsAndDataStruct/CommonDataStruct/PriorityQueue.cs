using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PriorityQueue<T> 
{
    protected T[] m_pq = null;
    int m_n = 0;

    protected Comparer<T> m_comparer = null;

//     public PriorityQueue(Comparer<T> comparer)
//     {
//         m_comparer = comparer;
//     }

    public PriorityQueue(int max, Comparer<T> comparer)
    {
        m_pq = new T[max];
        m_comparer = comparer;
    }

//     public PriorityQueue(T[] a, Comparer<T> comparer)
//     {
// 
//     }

    void Insert(T a)
    {
        m_pq[++m_n] = a;
        Swim(m_n);
    }

//     T Top()
//     {
// 
//     }

    T DeleteTop()
    {
        T top = m_pq[1];
        Exch(1, m_n--);
        m_pq[m_n + 1] = default(T);
        Sink(1);
        return top;
    }

    bool IsEmpty()
    {
        return m_n == 0;
    }

    int Size()
    {
        return m_n;
    }

    protected virtual bool less(int i, int j)
    {
        return false;
    }

    void Swim(int k)
    {
        while(k > 1 && less(k /2 , k))
        {
            Exch(k / 2, k);
            k = k / 2;
        }
    }

    void Sink(int k)
    {
        while(2 * k <= m_n)
        {
            int j = 2 * k;
            if(j < m_n && less(j, j+1))
            {
                j++;
            }
            if(!less(k, j))
            {
                break;
            }
            Exch(k, j);
            k = j;
        }
    }

    void Exch(int i, int j)
    {
        T t = m_pq[i];
        m_pq[i] = m_pq[j];
        m_pq[j] = t;
    }
}

public class MinPQ<T> : PriorityQueue<T> 
{
    public MinPQ(int max, Comparer<T> comparer)
        : base(max, comparer)
    {
        m_pq = new T[max];
        m_comparer = comparer;
    }

    protected override bool less(int i, int j)
    {
        int ret = m_comparer.Compare(m_pq[i], m_pq[j]);
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

public class MaxPQ<T> : PriorityQueue<T>
{
    public MaxPQ(int max, Comparer<T> comparer)
        : base(max, comparer)
    {
        m_pq = new T[max];
        m_comparer = comparer;
    }

    protected override bool less(int i, int j)
    {
        int ret = m_comparer.Compare(m_pq[i], m_pq[j]);
        if (ret > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

