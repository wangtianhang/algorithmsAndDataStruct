using System;
using System.Collections.Generic;

using System.Text;



/// <summary>
/// 只对有向无环图有效
/// </summary>
class DepthFirstOrder
{
    bool[] m_marked;
    Queue<int> m_pre;
    Queue<int> m_post;
    Stack<int> m_reversePost;
    public DepthFirstOrder(DirectedGraph g)
    {
        m_pre = new Queue<int>();
        m_post = new Queue<int>();
        m_reversePost = new Stack<int>();
        m_marked = new bool[g.V()];
        for (int v = 0; v < g.V(); ++v )
        {
            if(!m_marked[v])
            {
                Dfs(g, v);
            }
        }
    }

    void Dfs(DirectedGraph g, int v)
    {
        m_pre.Enqueue(v);

        m_marked[v] = true;

        foreach(var w in g.Adj(v))
        {
            if(!m_marked[w])
            {
                Dfs(g, w);
            }
        }

        m_post.Enqueue(v);

        m_reversePost.Push(v);
    }

    IEnumerable<int> Pre()
    {
        return m_pre;
    }

    IEnumerable<int> Post()
    {
        return m_post;
    }

    public IEnumerable<int> ReversePost()
    {
        return m_reversePost;
    }
}

public class Topological
{
    IEnumerable<int> m_order = null;

    public Topological(DirectedGraph g)
    {
        DirectedCycle cycleFinder = new DirectedCycle(g);
        if(!cycleFinder.HasCycle())
        {
            DepthFirstOrder dfs = new DepthFirstOrder(g);
            m_order = dfs.ReversePost();
        }
    }

    public IEnumerable<int> Order()
    {
        return m_order;
    }

    public bool IsDAG()
    {
        return m_order != null;
    }
}

