using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Graph
{
    int m_v = 0;
    int m_e = 0;
    List<int>[] m_adj;

    public Graph(int v)
    {
        m_v = v;
        m_e = 0;
        //int[] test = new int[5];
        m_adj = new List<int>[m_v];
        for (int i = 0; i < m_adj.Length; ++i )
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

    public void  AddEdge(int v, int w)
    {
        m_adj[v].Add(w);
        m_adj[w].Add(v);
        m_e++;
    }

    public List<int> Adj(int v)
    {
        return m_adj[v];
    }

    public override string ToString()
    {
        string s = V() + " vertices " + E() + " edges\n";
        for (int v = 0; v < V(); ++v )
        {
            s += v + ": ";
            foreach(var w in Adj(v))
            {
                s += w + " ";
            }
            s += "\n";
        }
        return s;
    }

    public static int Degree(Graph g, int v)
    {
        int degree = 0;
        foreach(var iter in g.Adj(v))
        {
            degree++;
        }
        return degree;
    }

    public static int MaxDegree(Graph g)
    {
        int max = 0;
        for (int i = 0; i < g.V(); ++i)
        {
            int degree = Degree(g, i);
            if(degree > max)
            {
                max = degree;
            }
        }
        return max;
    }

    public static float AvgDegree(Graph g)
    {
        return 2 * g.E() / g.V();
    }

    public static int numberOfSelfLoops(Graph g)
    {
        int count = 0;
        for (int v = 0; v < g.V(); ++v )
        {
            foreach(var w in g.Adj(v))
            {
                if(v == w)
                {
                    count++;
                }
            }
        }
        return count / 2;
    }
}



