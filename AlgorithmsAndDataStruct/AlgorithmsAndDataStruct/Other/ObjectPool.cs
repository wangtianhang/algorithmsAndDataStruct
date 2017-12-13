using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ObjectPool<T> where T : class, new()
{
    List<T> m_waitList = new List<T>();
    List<T> m_useList = new List<T>();

    public T CreateObject()
    {
        T tInstance = null;
        if (m_waitList.Count != 0)
        {
            tInstance = m_waitList[0];
            m_waitList.RemoveAt(0);
        }
        else
        {
            tInstance = new T();
        }
        m_useList.Add(tInstance);
        return tInstance;
    }

    public void ReleaseObject(T tInstance)
    {
        m_useList.Remove(tInstance);
        if (!m_waitList.Contains(tInstance))
        {
            m_waitList.Add(tInstance);
        }
    }

    public void Clear()
    {
        m_waitList.Clear();
        m_useList.Clear();
    }

    public void AddWaitList(int size)
    {
        for(int i = 0; i < size; ++i)
        {
            m_waitList.Add(new T());
        }
    }
}

