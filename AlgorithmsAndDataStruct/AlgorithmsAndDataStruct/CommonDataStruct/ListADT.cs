using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ListADT<T>
{
    static int m_default_capacity = 10;

    int m_theSize = 0;

    T[] m_theItems = null;

    public ListADT()
    {
        Clear();
    }

    void Clear()
    {
        m_theSize = 0;
        EnsureCapacity(m_default_capacity);
    }

    int Size()
    {
        return m_theSize;
    }

    bool IsEmpty()
    {
        return Size() == 0;
    }

    void TrimToSize()
    {
        EnsureCapacity(Size());
    }

    T Get(int idx)
    {
        if(idx < 0 || idx >= Size())
        {
            throw new ArgumentOutOfRangeException();
        }
        return m_theItems[idx];
    }

    void Set(int idx, T newVal)
    {
        if (idx < 0 || idx >= Size())
        {
            throw new ArgumentOutOfRangeException();
        }
        m_theItems[idx] = newVal;
    }

    void EnsureCapacity(int newCapacity)
    {
        if(newCapacity < m_theSize)
        {
            return;
        }

        T[] old = m_theItems;
        m_theItems = new T[newCapacity];
        for (int i = 0; i < Size(); ++i )
        {
            m_theItems[i] = old[i];
        }
    }

    void Add(T x)
    {
        Insert(Size(), x);
    }

    void Insert(int idx, T x)
    {
        if(m_theItems.Length == Size())
        {
            EnsureCapacity(Size() * 2 + 1);
        }
        for (int i = m_theSize; i > idx; --i )
        {
            m_theItems[i] = m_theItems[i - 1];
        }
        m_theItems[idx] = x;

        m_theSize++;
    }

    void RemoveAt(int idx)
    {
        for (int i = idx; i < Size() - 1; ++i )
        {
            m_theItems[i] = m_theItems[i + 1];
        }
        m_theSize--;
    }
}

