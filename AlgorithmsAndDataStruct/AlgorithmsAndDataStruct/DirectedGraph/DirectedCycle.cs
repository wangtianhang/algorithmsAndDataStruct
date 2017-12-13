using System;
using System.Collections.Generic;

using System.Text;



class DirectedCycle
{
    bool[] m_marked;
    int[] m_edgeTo;
    Stack<int> m_cycle;
    bool[] m_onStack;

    public DirectedCycle(DirectedGraph g)
    {
        m_onStack = new bool[g.V()];
        m_edgeTo = new int[g.V()];
        m_marked = new bool[g.V()];
        for (int v = 0; v < g.V(); ++v )
        {
            if(!m_marked[v])
            {
                Dfs(g, v);
            }
        }
    }

    void Dfs(DirectedGraph g, int v)
    {
        m_onStack[v] = true;
        m_marked[v] = true;
        foreach(var w in g.Adj(v))
        {
            if(HasCycle())
            {
                return;
            }
            else if(!m_marked[w])
            {
                //Dfs(g, w);
                m_edgeTo[w] = v;
                Dfs(g, w);
            }
            else if(m_onStack[w])
            {
                m_cycle = new Stack<int>();
                for (int x = v; x != w; x = m_edgeTo[x] )
                {
                    m_cycle.Push(x);
                }
                m_cycle.Push(w);
                m_cycle.Push(v);
            }
        }
        m_onStack[v] = false;
    }

    public bool HasCycle()
    {
        return m_cycle != null;
    }

    public IEnumerable<int> Cycle()
    {
        return m_cycle;
    }
}

