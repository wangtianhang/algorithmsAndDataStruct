using System;
using System.Collections.Generic;

using System.Text;



class KruskaMST
{
    Queue<Edge> m_mst;

    public KruskaMST(EdgeWeightedGraph g)
    {
        m_mst = new Queue<Edge>();
        MinPQ<Edge> pq = new MinPQ<Edge>(g.Edges());
        UnionFind uf = new UnionFind(g.V());

        while(!pq.IsEmpty() && m_mst.Count < g.V() - 1)
        {
            Edge e = pq.DeleteTop();
            int v = e.Either();
            int w = e.Other(v);
            if(uf.Connected(v, w))
            {
                continue;
            }
            uf.Union(v, w);
            m_mst.Enqueue(e);
        }
    }

    public IEnumerable<Edge> Edges()
    {
        return m_mst;
    }

    public double Weight()
    {
        double weight = 0.0;
        foreach (var e in Edges())
            weight += e.Weight();
        return weight;
    }
}

