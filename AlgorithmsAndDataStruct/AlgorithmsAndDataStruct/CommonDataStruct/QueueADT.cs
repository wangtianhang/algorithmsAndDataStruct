using System;
using System.Collections.Generic;

using System.Text;



class QueueADT<T>
{
    class Node
    {
        public T m_item;
        public Node m_next;
    }

    Node m_first = null;
    Node m_last = null;
    int m_n;

    public bool IsEmpty()
    {
        return m_first == null;
    }

    int Size()
    {
        return m_n;
    }

    void Enqueue(T item)
    {
        Node oldLast = m_last;
        m_last = new Node();
        m_last.m_item = item;
        m_last.m_next = null;
        if(IsEmpty())
        {
            m_first = m_last;
        }
        else
        {
            oldLast.m_next = m_last;
        }
        m_n++;
    }

    T Dequeue()
    {
        T item = m_first.m_item;
        m_first = m_first.m_next;
        if(IsEmpty())
        {
            m_last = null;
        }
        m_n--;
        return item;
    }
}

