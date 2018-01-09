using System;
using System.Collections.Generic;
using System.Text;


public class PriorityQueueAnySize<T>
{
    protected T[] m_pq = null;
    int m_n = 0;

    protected IComparer<T> m_comparer = null;

    public int m_max = 64;

    public PriorityQueueAnySize( IComparer<T> comparer)
    {
        m_pq = new T[m_max + 1];
        m_comparer = comparer;
    }

    public PriorityQueueAnySize(IList<T> keys)
    {
        m_n = keys.Count;
        m_pq = new T[keys.Count + 1];
        for (int i = 0; i < m_n; i++)
            m_pq[i + 1] = keys[i];
        for (int k = m_n / 2; k >= 1; k--)
            Sink(k);
    }

    public void Insert(T a)
    {
        ++m_n;
        if(m_n > m_max)
        {
            m_max *= 2;
            T[] newPQ = new T[m_max * 2 + 1];
            Array.Copy(m_pq, newPQ, m_pq.Length);
            m_pq = newPQ;
        }
        m_pq[m_n] = a;
        Swim(m_n);
    }

    public T Top()
    {
        T top = m_pq[1];
        return top;
    }

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

    public int Size()
    {
        return m_n;
    }

    protected virtual bool less(int i, int j)
    {
        return false;
    }

    void Swim(int k)
    {
        while (k > 1 && less(k / 2, k))
        {
            Exch(k / 2, k);
            k = k / 2;
        }
    }

    void Sink(int k)
    {
        while (2 * k <= m_n)
        {
            int j = 2 * k;
            if (j < m_n && less(j, j + 1))
            {
                j++;
            }
            if (!less(k, j))
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

public class MinPQAnySize<T> : PriorityQueueAnySize<T>
{
    public MinPQAnySize(IComparer<T> comparer)
        : base(comparer)
    {
        m_pq = new T[m_max + 1];
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

public class MaxPQAnySize<T> : PriorityQueueAnySize<T>
{
    public MaxPQAnySize(IComparer<T> comparer)
        : base(comparer)
    {
        m_pq = new T[m_max + 1];
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

