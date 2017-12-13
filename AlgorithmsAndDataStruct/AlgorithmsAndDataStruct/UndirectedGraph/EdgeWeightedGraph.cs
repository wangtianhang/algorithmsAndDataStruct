using System;
using System.Collections.Generic;

using System.Text;


public class Edge
{
    int m_v;
    int m_w;
    double m_weight;

    public Edge(int v, int w, double weight)
    {
        m_v = v;
        m_w = w;
        m_weight = weight;
    }

    public double Weight()
    {
        return m_weight;
    }

    public int Either()
    {
        return m_v;
    }

    public int Other(int vertex)
    {
        if(vertex == m_v)
        {
            return m_w;
        }
        else if(vertex == m_w)
        {
            return m_v;
        }
        else
        {
            throw new Exception("Inconsistent edge");
        }
    }

    public int CompareTo(Edge that)
    {
        if(Weight() < that.Weight())
        {
            return -1;
        }
        else if(Weight() > that.Weight())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

class EdgeWeightedGraph
{
    int m_v;
    int m_e;
    List<Edge>[] m_adj;

    public EdgeWeightedGraph(int v)
    {
        m_v = v;
        m_e = 0;
        m_adj = new List<Edge>[m_v];
        for (int i = 0; i < m_adj.Length; ++i )
        {
            m_adj[i] = new List<Edge>();
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

    public void AddEdge(Edge e)
    {
        int v = e.Either();
        int w = e.Other(v);
        m_adj[v].Add(e);
        m_adj[w].Add(e);
        m_e++;
    }

    public List<Edge> Adj(int v)
    {
        return m_adj[v];
    }

    public IList<Edge> Edges() 
    {
        List<Edge> list = new List<Edge>();
        for (int v = 0; v < V(); v++) 
        {
            int selfLoops = 0;
            foreach (var e in Adj(v)) 
            {
                if (e.Other(v) > v) 
                {
                    list.Add(e);
                }
                // only add one copy of each self loop (self loops will be consecutive)
                else if (e.Other(v) == v) 
                {
                    if (selfLoops % 2 == 0) 
                        list.Add(e);
                    selfLoops++;
                }
            }
        }
        return list;
    }
}

