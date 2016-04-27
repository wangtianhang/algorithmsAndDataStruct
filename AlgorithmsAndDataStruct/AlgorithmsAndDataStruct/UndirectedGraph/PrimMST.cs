using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class PrimMST
{
    Edge[] m_edgeTo;
    double[] m_distTo;
    bool[] m_marked;
    IndexMinPQ<double> m_pq;

    public PrimMST(EdgeWeightedGraph g)
    {
        m_edgeTo = new Edge[g.V()];
        m_distTo = new double[g.V()];
        m_marked = new bool[g.V()];
        for (int v = 0; v < g.V(); ++v )
        {
            m_distTo[v] = Double.PositiveInfinity;
        }
        m_pq = new IndexMinPQ<double>(g.V());

        m_distTo[0] = 0.0d;
        m_pq.insert(0, 0.0d);
        while(!m_pq.isEmpty())
        {
            Visit(g, m_pq.delMin());
        }
    }

    void Visit(EdgeWeightedGraph g, int v)
    {
        m_marked[v] = true;
        foreach(var e in g.Adj(v))
        {
            int w = e.Other(v);
            if(m_marked[w])
            {
                continue;
            }
            if(e.Weight() < m_distTo[w])
            {
                m_edgeTo[w] = e;
                m_distTo[w] = e.Weight();
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

    public IEnumerable<Edge> Edges()
    {
        Queue<Edge> mst = new Queue<Edge>();
        for (int v = 0; v < m_edgeTo.Length; v++)
        {
            Edge e = m_edgeTo[v];
            if (e != null)
            {
                mst.Enqueue(e);
            }
        }
        return mst;
    }

    public double Weight()
    {
        double weight = 0.0;
        foreach (var e in Edges())
            weight += e.Weight();
        return weight;
    }
}

