using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class StackADT<T>
{
    class Node
    {
        public T m_item;
        public Node m_next;
    }

    Node m_first;
    int m_n;



    bool IsEmpty()
    {
        return m_first == null;
    }

    int Size()
    {
        return m_n;
    }

    void Push(T item)
    {
        Node oldFirst = m_first;
        m_first = new Node();
        m_first.m_item = item;
        m_first.m_next = oldFirst;
        m_n++;
    }

    public T Pop()
    {
        T item = m_first.m_item;
        m_first = m_first.m_next;
        m_n--;
        return item;
    }
}

