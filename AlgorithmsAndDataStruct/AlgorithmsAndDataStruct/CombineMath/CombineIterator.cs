using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestCombineIterator
{
    public static void Test()
    {
        List<int> test = new List<int>();
        for (int i = 0; i < 4; ++i )
        {
            test.Add(i + 1);
        }
        CombineHelper<int> combineHelper = new CombineHelper<int>(test, 2);
        foreach(List<int> oneSet in combineHelper)
        {
            foreach(var iter in oneSet)
            {
                Console.Write(iter + ",");
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}

class CombineIterator<T> : IEnumerator
{
    //List<T> m_set = null;
    bool m_isFirst = false;

    List<List<T>> m_combineSetList = new List<List<T>>();
    public CombineIterator(List<T> set, int selectNum)
    {
        //m_set = set;

        int[] b = new int[set.Count];
        Combine(set, set.Count, selectNum, b, selectNum);

        int test = 0;
    }

    public object Current
    {
        get 
        {
            if(m_combineSetList.Count != 0)
            {
                return m_combineSetList[0];
            }
            else
            {
                return null;
            }   
        }
    }

    public bool MoveNext()
    {
        if (!m_isFirst)
        {
            m_isFirst = true;
            return true;
        }
        else
        {
            if (m_combineSetList.Count != 0)
            {
                m_combineSetList.RemoveAt(0);
                if (m_combineSetList.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }

    public void Reset()
    {

    }

    void Combine(List<T> a, int n, int m, int[] b, int M)
    {
        for (int i = n; i >= m; i--)   // 注意这里的循环范围
        {
            b[m - 1] = i - 1;
            if (m > 1)
            {
                Combine(a, i - 1, m - 1, b, M);
            }
            else                     // m == 1, 输出一个组合
            {
                List<T> oneSet = new List<T>();
                for (int j = M - 1; j >= 0; j--)
                {
                    //Console.Write(a[b[j]]);
                    oneSet.Add(a[b[j]]);
                }
                m_combineSetList.Add(oneSet);
            }
        }
    }
}

public class CombineHelper<T> : IEnumerable
{
    List<T> m_set = null;
    int m_selectNum = 0;
    public CombineHelper(List<T> set, int selectNum)
    {
        m_set = set;

        m_selectNum = selectNum;
    }

    public IEnumerator GetEnumerator()
    {
        return new CombineIterator<T>(m_set, m_selectNum);
    }
}

