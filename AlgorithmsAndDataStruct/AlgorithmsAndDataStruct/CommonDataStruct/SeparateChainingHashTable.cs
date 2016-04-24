using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class SeparateChainingHashTable<T>
{
    static int m_default_talbe_size = 101;
    LinkedList<T>[] m_theLists;
    int m_currentSize = 0;

    public SeparateChainingHashTable()
    {
        Construct(m_default_talbe_size);
    }

    public SeparateChainingHashTable(int size)
    {
        Construct(size);
    }

    void Construct(int size)
    {
        m_theLists = new LinkedList<T>[nextPrime(size)];
        for (int i = 0; i < m_theLists.Length;++i )
        {
            m_theLists[i] = new LinkedList<T>();
        }
    }

    void Insert(T x)
    {
        LinkedList<T> whichList = m_theLists[MyHash(x)];
        if(!whichList.Contains(x))
        {
            whichList.AddLast(new LinkedListNode<T>(x));
            if(++m_currentSize > m_theLists.Length)
            {
                Rehash();
            }
        }
    }

    void Remove(T x)
    {
        LinkedList<T> whichList = m_theLists[MyHash(x)];
        if(whichList.Contains(x))
        {
            whichList.Remove(x);
            m_currentSize--;
        }
    }

    bool Contains(T x)
    {
        LinkedList<T> whichList = m_theLists[MyHash(x)];
        return whichList.Contains(x);
    }

    void MakeEmpty()
    {
        for (int i = 0; i < m_theLists.Length; ++i )
        {
            m_theLists[i].Clear();
        }
        m_currentSize = 0;
    }

    void Rehash()
    {
        LinkedList<T>[] oldList = m_theLists;
        m_theLists = new LinkedList<T>[nextPrime(2 * m_theLists.Length)];
        for (int j = 0; j < m_theLists.Length; ++j )
        {
            m_theLists[j] = new LinkedList<T>();
        }
        m_currentSize = 0;
        for (int i = 0; i < oldList.Length; ++i )
        {
            foreach(var iter in oldList[i])
            {
                Insert(iter);
            }
        }
    }

    int MyHash(T x)
    {
        int hashVal = x.GetHashCode();
        hashVal %= m_theLists.Length;
        if(hashVal < 0)
        {
            hashVal += m_theLists.Length;
        }
        return hashVal;
    }

    int nextPrime(int n)
    {
        if (n % 2 == 0)
            n++;

        for (; !isPrime(n); n += 2)
            ;

        return n;
    }

    bool isPrime(int n)
    {
        if (n == 2 || n == 3)
            return true;

        if (n == 1 || n % 2 == 0)
            return false;

        for (int i = 3; i * i <= n; i += 2)
            if (n % i == 0)
                return false;

        return true;
    }
}

