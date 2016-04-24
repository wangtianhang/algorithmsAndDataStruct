using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class NodeADT<T>
{
    public NodeADT(T d, NodeADT<T> p, NodeADT<T> n)
    {
        m_data = d;
        m_prev = p;
        m_next = n;
    }
    public T m_data;
    public NodeADT<T> m_prev;
    public NodeADT<T> m_next;
}

class LinkedListADT<T> // where T : new()
{
    int m_theSize = 0;
    //int modCount = 0;
    NodeADT<T> m_beginMarker = null;
    NodeADT<T> m_endMarker = null;

    public LinkedListADT()
    {
        Clear();
    }

    void Clear()
    {
        m_beginMarker = new NodeADT<T>(default(T), null, null);
        m_endMarker = new NodeADT<T>(default(T), m_beginMarker, null);
        m_beginMarker.m_next = m_endMarker;
        m_theSize = 0;
    }

    int Size()
    {
        return m_theSize;
    }

    bool IsEmpty()
    {
        return Size() == 0;
    }

    void Add(T x)
    {
        Insert(Size(), x);
    }

    void Insert(int idx, T x)
    {
        AddBefore(GetNode(idx), x);
    }

    T Get(int idx)
    {
       return GetNode(idx).m_data;
    }

    void Set(int idx, T newVal)
    {
        NodeADT<T> p = GetNode(idx);
        p.m_data = newVal;
    }

    void AddBefore(NodeADT<T> p, T x)
    {
        NodeADT<T> newNode = new NodeADT<T>(x, p.m_prev, p);
        newNode.m_prev.m_next = newNode;
        p.m_prev = newNode;
        m_theSize++;
    }

    void Remove(NodeADT<T> p)
    {
        p.m_next.m_prev = p.m_prev;
        p.m_prev.m_next = p.m_next;
        m_theSize--;
    }

    NodeADT<T> GetNode(int idx)
    {
        NodeADT<T> p;
        if(idx < 0 || idx > Size())
        {
            throw new ArgumentOutOfRangeException();
        }

        if(idx < Size() / 2)
        {
            p = m_beginMarker.m_next;
            for (int i = 0; i < idx; ++i )
            {
                p = p.m_next;
            }
        }
        else
        {
            p = m_endMarker;
            for (int i = Size(); i > idx; --i )
            {
                p = p.m_prev;
            }
        }

        return p;
    }
}

