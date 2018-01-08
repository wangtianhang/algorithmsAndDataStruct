using System;
using System.Collections.Generic;

using System.Text;


class FastGetMaxStack<T> where T: IComparable<T> 
{
    List<T> m_stackItemList = new List<T>();
    List<int> m_link2NextMaxItem = new List<int>();
    int m_maxStackItemIndex = -1;

    public void Push(T x)
    {
        m_stackItemList.Add(x);
        if (m_maxStackItemIndex < 0 || x.CompareTo(Max()) > 0)
        {
            m_link2NextMaxItem.Add(m_stackItemList.Count -1);
            m_maxStackItemIndex = m_stackItemList.Count - 1;
        }
        else
        {
            m_link2NextMaxItem.Add(m_maxStackItemIndex);
        }
    }

    public T Pop()
    {
        if (m_stackItemList.Count == 0)
        {
            throw new System.Exception("no item to pop");
        }
        else
        {
            T ret = m_stackItemList[m_stackItemList.Count - 1];
            if (m_maxStackItemIndex == m_stackItemList.Count - 1)
            {
                int willReadTop = m_stackItemList.Count - 1 - 1;
                if (willReadTop >= 0)
                {
                    m_maxStackItemIndex = m_link2NextMaxItem[willReadTop];
                }
                else
                {
                    m_maxStackItemIndex = -1;
                }
            }
            m_stackItemList.RemoveAt(m_stackItemList.Count - 1);
            m_link2NextMaxItem.RemoveAt(m_link2NextMaxItem.Count - 1);
            return ret;
        }
    }

    public int Count()
    {
        return m_stackItemList.Count;
    }

    public T Max()
    {
        if(m_maxStackItemIndex < 0)
        {
            throw new System.Exception("no max");
        }
        return m_stackItemList[m_maxStackItemIndex];
    }
}

class FastGetMaxQueue<T> where T : IComparable<T> 
{
    FastGetMaxStack<T> m_statkA = new FastGetMaxStack<T>();
    FastGetMaxStack<T> m_statkB = new FastGetMaxStack<T>();

    T MaxValue(T x, T y)
    {
        if(x.CompareTo(y) > 0)
        {
            return x;
        }
        else
        {
            return y;
        }
    }

    public int Count()
    {
        return m_statkA.Count() + m_statkB.Count();
    }

    public T Max()
    {
        if (m_statkA.Count() == 0 && m_statkB.Count() == 0)
        {
            throw new System.Exception("no max");
        }
        else if (m_statkA.Count() == 0)
        {
            return m_statkB.Max();
        }
        else if (m_statkB.Count() == 0)
        {
            return m_statkA.Max();
        }
        else
        {
            return MaxValue(m_statkA.Max(), m_statkB.Max());
        }
    }

    public void EnQueue(T x)
    {
        m_statkB.Push(x);
    }

    public T DeQueue()
    {
        if(Count() == 0)
        {
            throw new System.Exception("can not dequeue");
        }

        if(m_statkA.Count() == 0)
        {
            while(m_statkB.Count() != 0)
            {
                m_statkA.Push(m_statkB.Pop());
            }
        }
        return m_statkA.Pop();
    }
}

public class TestFastGetMaxQueue
{
    public static void Test()
    {
        FastGetMaxQueue<int> test = new FastGetMaxQueue<int>();
        test.EnQueue(400);
        test.EnQueue(300);
        test.EnQueue(200);
        test.EnQueue(100);

        Console.WriteLine(test.Max());
        test.DeQueue();
        Console.WriteLine(test.Max());

        //Console.ReadLine();
    }
}

