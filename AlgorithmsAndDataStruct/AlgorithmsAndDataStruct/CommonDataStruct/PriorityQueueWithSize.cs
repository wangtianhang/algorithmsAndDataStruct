using System;
using System.Collections.Generic;
using System.Text;


public class MaxPQWithSize<T>
{
    MinPQ<T> m_minPQ = null;
    IComparer<T> m_comparer = null;
    int m_max = 0;
    public MaxPQWithSize(int max, IComparer<T> comparer)
    {
        //m_pq = new T[max + 2];
        m_max = max;
        m_minPQ = new MinPQ<T>(max, comparer);
        m_comparer = comparer;
    }

    public int Size()
    {
        return m_minPQ.Size();
    }

    public void Insert(T a)
    {
        if (m_minPQ.Size() < m_max)
        {
            m_minPQ.Insert(a);
        }
        else
        {
            if (m_comparer.Compare(m_minPQ.Top(), a) < 0)
            {
                m_minPQ.ChangeTop(a);
            }
        }
    }

    public List<T> GetAll()
    {
        List<T> ret = new List<T>();
        while (m_minPQ.Size() != 0)
        {
            ret.Add(m_minPQ.DeleteTop());
        }
        ret.Reverse();
        return ret;
    }
}

public class MinPQWithSize<T>
{
    MaxPQ<T> m_maxPQ = null;
    IComparer<T> m_comparer = null;
    int m_max = 0;
    public MinPQWithSize(int max, IComparer<T> comparer)
    {
        //m_pq = new T[max + 2];
        m_max = max;
        m_maxPQ = new MaxPQ<T>(max, comparer);
        m_comparer = comparer;
    }

    public int Size()
    {
        return m_maxPQ.Size();
    }

    public void Insert(T a)
    {
        if (m_maxPQ.Size() < m_max)
        {
            m_maxPQ.Insert(a);
        }
        else
        {
            if (m_comparer.Compare(m_maxPQ.Top(), a) > 0)
            {
                m_maxPQ.ChangeTop(a);
            }
        }
    }

    public List<T> GetAll()
    {
        List<T> ret = new List<T>();
        while (m_maxPQ.Size() != 0)
        {
            ret.Add(m_maxPQ.DeleteTop());
        }
        ret.Reverse();
        return ret;
    }
}

