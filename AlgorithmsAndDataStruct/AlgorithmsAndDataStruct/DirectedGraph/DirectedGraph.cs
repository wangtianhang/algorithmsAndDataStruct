using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DirectedGraph
{
    int m_v;
    int m_e;
    List<int>[] m_adj;

    public DirectedGraph(int v)
    {
        m_v = v;
        m_e = 0;
        m_adj = new List<int>[m_v];
        for (int i = 0; i < m_v; i++ )
        {
            m_adj[i] = new List<int>();
        }
    }

    public int V()
    {
        return m_v;
    }

    public int E()
    {
        return m_e;
    }

    public void AddEdge(int v, int w)
    {
        m_adj[v].Add(w);
        m_e++;
    }

    public List<int> Adj(int v)
    {
        return m_adj[v];
    }

    public DirectedGraph Reverses()
    {
        DirectedGraph r = new DirectedGraph(m_v);
        for (int v = 0; v < m_v; ++v )
        {
            foreach(var w in Adj(v))
            {
                r.AddEdge(w, v);
            }
        }
        return r;
    }
}

