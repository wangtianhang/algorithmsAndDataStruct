using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class DijkstraSP
{
    DirectedEdge[] m_edgeTo;
    double[] m_distTo;
    IndexMinPQ<double> m_pq;

    public DijkstraSP(EdgeWeightDirectedGraph g, int s)
    {
        m_edgeTo = new DirectedEdge[g.V()];
        m_distTo = new double[g.V()];
        m_pq = new IndexMinPQ<double>(g.V());
        for (int v = 0; v < g.V(); ++v )
        {
            m_distTo[v] = Double.PositiveInfinity;
        }
        m_distTo[s] = 0;
        m_pq.insert(s, 0);
        while(!m_pq.isEmpty())
        {
            Relax(g, m_pq.delMin());
        }
    }

    void Relax(EdgeWeightDirectedGraph g, int v)
    {
        foreach(var e in g.Adj(v))
        {
            int w = e.To();
            if(m_distTo[w] > m_distTo[v] + e.Weight())
            {
                m_distTo[w] = m_distTo[v] + e.Weight();
                m_edgeTo[w] = e;
                if(m_pq.contains(w))
                {
                    m_pq.change(w, m_distTo[w]);
                }
                else
                {
                    m_pq.insert(w, m_distTo[w]);
                }
            }
        }
    }

    public double DistTo(int v)
    {
        return m_distTo[v];
    }

    public bool HasPathTo(int v)
    {
        return m_distTo[v] < Double.PositiveInfinity;
    }

    public IEnumerable<DirectedEdge> PathTo(int v)
    {
        if(!HasPathTo(v))
        {
            return null;
        }
        Stack<DirectedEdge> path = new Stack<DirectedEdge>();
        for (DirectedEdge e = m_edgeTo[v]; e != null; e = m_edgeTo[e.From()] )
        {
            path.Push(e);
        }
        return path;
    }
}

