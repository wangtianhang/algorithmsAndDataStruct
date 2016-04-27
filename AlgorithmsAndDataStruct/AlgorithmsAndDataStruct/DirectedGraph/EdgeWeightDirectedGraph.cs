using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DirectedEdge
{
    int m_v;
    int m_w;
    double m_weight;

    public DirectedEdge(int v, int w, double weight)
    {
        m_v = v;
        m_w = w;
        m_weight = weight;
    }

    public double Weight()
    {
        return m_weight;
    }

    public int From()
    {
        return m_v;
    }

    public int To()
    {
        return m_w;
    }

//     public int CompareTo(Edge that)
//     {
//         if(Weight() < that.Weight())
//         {
//             return -1;
//         }
//         else if(Weight() > that.Weight())
//         {
//             return 1;
//         }
//         else
//         {
//             return 0;
//         }
//     }
}

class EdgeWeightDirectedGraph
{
    int m_v;
    int m_e;
    List<DirectedEdge>[] m_adj;

    public EdgeWeightDirectedGraph(int v)
    {
        m_v = v;
        m_e = 0;
        m_adj = new List<DirectedEdge>[v];
        for(int i = 0; i < V(); ++i)
        {
            m_adj[i] = new List<DirectedEdge>();
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

    public void AddEdge(DirectedEdge e)
    {
        m_adj[e.From()].Add(e);
        m_e++;
    }

    public IEnumerable<DirectedEdge> Adj(int v)
    {
        return m_adj[v];
    }

    public IEnumerable<DirectedEdge> Edges()
    {
        List<DirectedEdge> list = new List<DirectedEdge>();
        for (int v = 0; v < V(); ++v )
        {
            foreach(var e in m_adj[v])
            {
                list.Add(e);
            }
        }

        return list;
    }
}
