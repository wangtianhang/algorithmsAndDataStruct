using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class NFA
{
    char[] m_re;
    DirectedGraph m_g;
    int m_m;

    public NFA(string regexp)
    {
        Stack<int> ops = new Stack<int>();
        m_re = regexp.ToCharArray();
        m_m = m_re.Length;
        m_g = new DirectedGraph(m_m + 1);

        for (int i = 0; i < m_m; ++i )
        {
            int lp = i;
            if(m_re[i] == '(' || m_re[i] == '|')
            {
                ops.Push(i);
            }
            else if (m_re[i] == ')')
            {
                int or = ops.Pop();
                if(m_re[or] == '|')
                {
                    lp = ops.Pop();
                    m_g.AddEdge(lp, or + 1);
                    m_g.AddEdge(or, i);
                }
                else
                {
                    lp = or;
                }
            }
            if(i < m_m - 1 && m_re[i + 1] == '*')
            {
                m_g.AddEdge(lp, i + 1);
                m_g.AddEdge(i + 1, lp);
            }
            if(m_re[i] == '(' || m_re[i] == '*'|| m_re[i] == ')')
            {
                m_g.AddEdge(i, i + 1);
            }
        }
    }

    public bool Recognizes(string txt)
    {
        List<int> pc = new List<int>();

        // todo 参考算法4

        return false;
    }
}

