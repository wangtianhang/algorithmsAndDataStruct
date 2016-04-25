using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RedBlackBST<Key, Value>
{
    static bool Red = true;
    static bool Black = false;

    public class NodeRedBlack
    {
        public Key m_key;
        public Value m_value;
        public NodeRedBlack m_left, m_right;
        public int m_n;
        public bool m_color;
        public NodeRedBlack(Key key, Value val, int n, bool color)
        {
            m_key = key;
            m_value = val;
            m_n = n;
            m_color = color;
        }
    }

    NodeRedBlack m_root = null;

    static Comparer<Key> m_comparer;

    public RedBlackBST(Comparer<Key> comparer)
    {
        m_comparer = comparer;
    }

    bool IsRed(NodeRedBlack x)
    {
        if(x == null)
        {
            return false;
        }
        else
        {
            return x.m_color == Red;
        }
    }

    NodeRedBlack RotateLeft(NodeRedBlack h)
    {
        // todo 参考算法4
        return null;
    }

    NodeRedBlack RotateRight(NodeRedBlack h)
    {
        // todo 参考算法4
        return null;
    }

    void FlipColors(NodeRedBlack h)
    {
        // todo 参考算法4
    }

    int Size()
    {
        return Size(m_root);
    }

    int Size(NodeRedBlack x)
    {
        if(x == null)
        {
            return 0;
        }
        else
        {
            return x.m_n;
        }
    }

    void Insert(Key key, Value val)
    {
        m_root = Insert(m_root, key, val);
        m_root.m_color = Black;
    }

    NodeRedBlack Insert(NodeRedBlack h, Key key, Value val)
    {
        if(h == null)
        {
            return new NodeRedBlack(key, val, 1, Red);
        }
        int cmp = m_comparer.Compare(key, h.m_key);
        if(cmp < 0)
        {
            h.m_left = Insert(h.m_left, key, val);
        }
        else if(cmp > 0)
        {
            h.m_right = Insert(h.m_right, key, val);
        }
        else
        {
            h.m_value = val;
        }

        if(IsRed(h.m_right) && !IsRed(h.m_left))
        {
            h = RotateLeft(h);
        }
        if(IsRed(h.m_left) && IsRed(h.m_left.m_left))
        {
            h = RotateRight(h);
        }
        if(IsRed(h.m_left) && IsRed(h.m_right))
        {
            FlipColors(h);
        }

        h.m_n = Size(h.m_left) + Size(h.m_right) + 1;
        return h;
    }

    void Remove(Key key)
    {
        // todo 参考算法4
    }
}

