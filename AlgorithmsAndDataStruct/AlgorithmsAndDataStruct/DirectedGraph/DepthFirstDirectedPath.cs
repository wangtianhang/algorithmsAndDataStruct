using System;
using System.Collections.Generic;

using System.Text;



class DepthFirstDirectedPath
{
    bool[] m_marked = null; // 这个顶点上调用过dfs()了么
    int[] m_edgeTo = null; // 从起点到一个顶点的已知路径上的最后一个顶点
    int m_s; // 起点

    public DepthFirstDirectedPath(DirectedGraph g, int s)
    {
        m_marked = new bool[g.V()];
        m_edgeTo = new int[g.V()];
        m_s = s;
        Dfs(g, s);
    }

    void Dfs(DirectedGraph g, int v)
    {
        m_marked[v] = true;
        foreach (var w in g.Adj(v))
        {
            if (!m_marked[w])
            {
                m_edgeTo[w] = v;
                Dfs(g, w);
            }
        }
    }

    public bool HasPathTo(int v)
    {
        return m_marked[v];
    }

    public List<int> PathTo(int v)
    {
        if (!HasPathTo(v))
        {
            return new List<int>();
        }
        else    
        {
            //Stack<int> test = new Stack<int>();
            List<int> path = new List<int>();
            for(int x = v; x != m_s; x = m_edgeTo[x])
            {
                path.Add(x);
            }
            path.Add(m_s);
            return path;
        }
    }
}

