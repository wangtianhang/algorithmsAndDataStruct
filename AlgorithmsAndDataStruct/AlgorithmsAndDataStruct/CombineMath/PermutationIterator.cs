using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PermutationTest
{
    public static void Test()
    {
        List<int> test = new List<int>();
        for (int i = 0; i < 4; ++i)
        {
            test.Add(i + 1);
        }
        PermutationHelper<int> permutationHelper = new PermutationHelper<int>(test);
        int count = 0;
        foreach (List<int> oneSet in permutationHelper)
        {
            foreach (var iter in oneSet)
            {
                Console.Write(iter + ",");
            }
            count += 1;
            Console.WriteLine();
        }

        Console.WriteLine("total " + count);
        Console.ReadLine(); 
    }
}

class PermutationIterator<T> : IEnumerator
{
    //List<T> m_set = null;
    List<List<T>> m_PermutationSetList = new List<List<T>>();
    bool m_isFirst = false;
    //int n = 0;
    public PermutationIterator(List<T> set)
    {
        //m_set = set;
        perm(set, 0, set.Count - 1);
    }

    void swap(List<T> set, int i, int j) 
    {     
        T a = set[i];
        T b = set[j];
        set[j] = a;
        set[i] = b;
    }  

    void perm(List<T> list, int k, int m) 
     {     
         int i;     
         if(k > m)     
         {
             List<T> oneSet = new List<T>();
             for(i = 0; i <= m; i++)
             {
                 oneSet.Add(list[i]);
             }
             m_PermutationSetList.Add(oneSet); 
         }     
         else     
         {         
             for(i = k; i <= m; i++)         
             {
                 swap(list, k, i);             
                 perm(list, k + 1, m);             
                 swap(list, k, i);         
             }     
         } 
     } 

    public object Current
    {
        get 
        {
            if (m_PermutationSetList.Count != 0)
            {
                return m_PermutationSetList[0];
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
            if (m_PermutationSetList.Count != 0)
            {
                m_PermutationSetList.RemoveAt(0);
                if (m_PermutationSetList.Count != 0)
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
}

public class PermutationHelper<T> : IEnumerable
{
    List<T> m_set = null;
    //int m_selectNum = 0;
    public PermutationHelper(List<T> set)
    {
        m_set = set;

        //m_selectNum = selectNum;
    }

    public IEnumerator GetEnumerator()
    {
        return new PermutationIterator<T>(m_set);
    }
}

