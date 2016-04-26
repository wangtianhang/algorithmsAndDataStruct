using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class KMP
{
    string m_pat;
    int[,] m_dfa;
    public KMP(string pat)
    {
        m_pat = pat;
        int m = pat.Length;
        int r = 256;
        m_dfa = new int[r,m];
        m_dfa[pat[0], 0] = 1;
        for(int x = 0, j = 1; j < m; ++j)
        {
            for (int c = 0; c < r; c++ )
            {
                m_dfa[c, j] = m_dfa[c, x];
            }
            m_dfa[m_pat[j], j] = j + 1;
            x = m_dfa[pat[j], x];
        }
    }

    public int Search(string txt)
    {
        int i, j, n = txt.Length;
        int m = m_pat.Length;
        for (i = 0, j = 0; i < n && j < m; ++i )
        {
            j = m_dfa[txt[i], j];
        }
        if(j == m)
        {
            return i - m;
        }
        else
        {
            return n;
        }
    }
}

