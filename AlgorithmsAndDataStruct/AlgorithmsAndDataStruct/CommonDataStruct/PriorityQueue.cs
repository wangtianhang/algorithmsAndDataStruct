using System;
using System.Collections.Generic;

using System.Text;


// 优先队列概念
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

        //m_pq.Length
    }

//     public PriorityQueue(T[] a, Comparer<T> comparer)
//     {
// 
//     }
    public PriorityQueue(IList<T> keys) 
    {
        m_n = keys.Count;
        m_pq = new T[keys.Count + 1];
        for (int i = 0; i < m_n; i++)
            m_pq[i + 1] = keys[i];
        for (int k = m_n / 2; k >= 1; k--)
            Sink(k);
    }

    void Insert(T a)
    {
        m_pq[++m_n] = a;
        Swim(m_n);
    }

//     T Top()
//     {
// 
//     }

    public T DeleteTop()
    {
        T top = m_pq[1];
        Exch(1, m_n--);
        m_pq[m_n + 1] = default(T);
        Sink(1);
        return top;
    }

    public bool IsEmpty()
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

    public MinPQ(IList<T> keys)
        : base(keys)
    {

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

