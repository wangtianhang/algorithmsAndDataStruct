using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 判定是否为无环图
/// </summary>
class Cycle
{
    bool[] m_marked;
    bool m_hasCycle;
    public Cycle(Graph g)
    {
        m_marked = new bool[g.V()];
        for (int s = 0; s < g.V(); s++ )
        {
            if(!m_marked[s])
            {
                Dfs(g, s, s);
            }
        }
    }

    void Dfs(Graph g, int v, int u)
    {
        m_marked[v] = true;
        foreach (var w in g.Adj(v))
        {
            if (!m_marked[w])
            {
                Dfs(g, w, v);
            }
            else if(w != u)
            {
                m_hasCycle = true;
            }
        }
    }

    public bool HasCycle()
    {
        return m_hasCycle;
    }
}

