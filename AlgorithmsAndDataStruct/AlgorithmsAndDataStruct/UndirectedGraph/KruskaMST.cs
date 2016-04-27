using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class KruskaMST
{
    Queue<Edge> m_mst;

    public KruskaMST(EdgeWeightedGraph g)
    {
        m_mst = new Queue<Edge>();
        MinPQ<Edge> pq = new MinPQ<Edge>(g.Edges());
        UnionFind uf = new UnionFind(g.V());


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

