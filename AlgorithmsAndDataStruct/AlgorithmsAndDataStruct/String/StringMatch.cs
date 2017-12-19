using System;
using System.Collections.Generic;

using System.Text;



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

/// <summary>
/// 来自算法第四版
/// 貌似bm的性能更好
/// </summary>
class BoyerMoore
{
    private int[] right;
    private string pat;
    BoyerMoore(string pat)
    {
        this.pat = pat;
        int M = pat.Length;
        int R = 256;
        right = new int[R];
        for (int c = 0; c < R; c++ )
        {
            right[c] = -1;
        }
        for (int j = 0; j < M; ++j )
        {
            right[pat[j]] = j;
        }
    }

    public int search(string txt)
    {
        int N = txt.Length;
        int M = pat.Length;
        int skip = 0;
        for (int i = 0; i <= N - M; i += skip )
        {
            skip = 0;
            for (int j = M - 1; j >= 0; j-- )
            {
                if(pat[j] != txt[i + j])
                {
                    skip = j - right[txt[i + j]];
                    if(skip < 1)
                    {
                        skip = 1;
                    }
                    break;
                }
            }
            if(skip == 0)
            {
                return i;
            }
        }
        return N;
    }
}

