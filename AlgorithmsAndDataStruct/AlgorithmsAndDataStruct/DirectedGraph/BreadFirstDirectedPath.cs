﻿using System;
using System.Collections.Generic;

using System.Text;



class BreadFirstDirectedPath
{
    bool[] m_marked = null;
    int[] m_edgeTo = null;
    int m_s = 0;

    public BreadFirstDirectedPath(DirectedGraph g, int s)
    {
        m_marked = new bool[g.V()];
        m_edgeTo = new int[g.V()];
        m_s = s;
        bfs(g, s);
    }

    void bfs(DirectedGraph g, int s)
    {
        Queue<int> queue = new Queue<int>();
        m_marked[s] = true;
        queue.Enqueue(s);
        while(queue.Count != 0)
        {
            int v = queue.Dequeue();
            foreach(var w in g.Adj(v))
            {
                m_edgeTo[w] = v;
                m_marked[w] = true;
                queue.Enqueue(w);
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
            for (int x = v; x != m_s; x = m_edgeTo[x])
            {
                path.Add(x);
            }
            path.Add(m_s);
            return path;
        }
    }
}

