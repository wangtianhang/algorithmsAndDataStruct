using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DirectedDFS
{
    bool[] m_marked;

    public DirectedDFS(DirectedGraph g, int s)
    {
        m_marked = new bool[g.V()];
        Dfs(g, s);
    }

    public DirectedDFS(DirectedGraph g, IEnumerable<int> sources)
    {
        m_marked = new bool[g.V()];
        foreach(var s in sources)
        {
            if(!Marked(s))
            {
                Dfs(g, s);
            }
        }
    }

    void Dfs(DirectedGraph g, int v)
    {
        m_marked[v] = true;
        foreach(var w in g.Adj(v))
        {
            if(!m_marked[w])
            {
                Dfs(g, w);
            }
        }
    }

    public bool Marked(int v)
    {
        return m_marked[v];
    }
}

