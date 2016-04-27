using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 顶点对的可达性
/// </summary>
class TransitiveClosure
{
    DirectedDFS[] m_all;
    public TransitiveClosure(DirectedGraph g)
    {
        m_all = new DirectedDFS[g.V()];
        for (int v = 0; v < g.V(); ++v )
        {
            m_all[v] = new DirectedDFS(g, v);
        }
    }

    bool Reachable(int v, int w)
    {
        return m_all[v].Marked(w);
    }
}

