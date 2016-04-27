using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class KosarajuSCC
{
    bool[] m_marked;
    int[] m_id;
    int m_count;

    public KosarajuSCC(DirectedGraph g)
    {
        m_marked = new bool[g.V()];
        m_id = new int[g.V()];
        DepthFirstOrder order = new DepthFirstOrder(g.Reverses());
        foreach(var s in order.ReversePost())
        {
            if(!m_marked[s])
            {
                Dfs(g, s);
                m_count++;
            }
        }
    }

    void Dfs(DirectedGraph g, int v)
    {
        m_marked[v] = true;
        m_id[v] = m_count;
        foreach(var w in g.Adj(v))
        {
            if(!m_marked[w])
            {
                Dfs(g, w);
            }
        }
    }

    public bool StronglyConnected(int v, int w)
    {
        return m_id[v] == m_id[w];
    }

    public int Id(int v)
    {
        return m_id[v];
    }

    public int Count()
    {
        return m_count;
    }
}


