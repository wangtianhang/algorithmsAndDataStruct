using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RedBlackBST<Key, Value>
{
    static bool RED = true;
    static bool BLACK = false;

    public class Node
    {
        public Key m_key;
        public Value m_val;
        public Node m_left, m_right;
        public int m_n;
        public bool m_color;
        public Node(Key key, Value val, int n, bool color)
        {
            m_key = key;
            m_val = val;
            m_n = n;
            m_color = color;
        }
    }

    Node m_root = null;

    static Comparer<Key> m_comparer;

    public RedBlackBST(Comparer<Key> comparer)
    {
        m_comparer = comparer;
    }

    bool IsRed(Node x)
    {
        if(x == null)
        {
            return false;
        }
        else
        {
            return x.m_color == RED;
        }
    }

    Node RotateLeft(Node h)
    {
        // todo 参考算法4
        Node x = h.m_right;
        h.m_right = x.m_left;
        x.m_left = h;
        x.m_color = x.m_left.m_color;
        x.m_left.m_color = RED;
        x.m_n = h.m_n;
        h.m_n = Size(h.m_left) + Size(h.m_right) + 1;
        return x;
    }

    Node RotateRight(Node h)
    {
        Node x = h.m_left;
        h.m_left = x.m_right;
        x.m_right = h;
        x.m_color = x.m_right.m_color;
        x.m_right.m_color = RED;
        x.m_n = h.m_n;
        h.m_n = Size(h.m_left) + Size(h.m_right) + 1;
        return x;
    }

    void FlipColors(Node h)
    {
        h.m_color = !h.m_color;
        h.m_left.m_color = !h.m_left.m_color;
        h.m_right.m_color = !h.m_right.m_color;
    }

    int Size()
    {
        return Size(m_root);
    }

    int Size(Node x)
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
        m_root.m_color = BLACK;
    }

    Node Insert(Node h, Key key, Value val)
    {
        if(h == null)
        {
            return new Node(key, val, 1, RED);
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
            h.m_val = val;
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

    public Value Get(Key key)
    {
        if (key == null) 
            throw new Exception("argument to get() is null");
        return Get(m_root, key);
    }

    Value Get(Node x, Key key)
    {
        while (x != null)
        {
            int cmp = m_comparer.Compare(key, x.m_key);
            if (cmp < 0) 
                x = x.m_left;
            else if (cmp > 0) 
                x = x.m_right;
            else return x.m_val;
        }

        throw new Exception("argument to get() is null");
        return default(Value);
    }

    public bool Contains(Key key)
    {
        return Get(key) != null;
    }

    public bool IsEmpty()
    {
        return m_root == null;
    }

    void Delete(Key key)
    {
        if (key == null) throw new Exception("argument to delete() is null");
        if (!Contains(key)) return;

        // if both children of root are black, set root to red
        if (!IsRed(m_root.m_left) && !IsRed(m_root.m_right))
            m_root.m_color = RED;

        m_root = Delete(m_root, key);
        if (!IsEmpty()) 
            m_root.m_color = BLACK;
    }

    public Key Min()
    {
        if (IsEmpty()) 
            throw new Exception("called min() with empty symbol table");
        return Min(m_root).m_key;
    }

    Node Min(Node x)
    {
        // assert x != null;
        if (x.m_left == null) return x;
        else return Min(x.m_left);
    }

    Node deleteMin(Node h)
    {
        if (h.m_left == null)
            return null;

        if (!IsRed(h.m_left) && !IsRed(h.m_left.m_left))
            h = MoveRedLeft(h);

        h.m_left = deleteMin(h.m_left);
        return Balance(h);
    }

    Node Balance(Node h)
    {
        // assert (h != null);

        if (IsRed(h.m_right)) h = RotateLeft(h);
        if (IsRed(h.m_left) && IsRed(h.m_left.m_left)) h = RotateRight(h);
        if (IsRed(h.m_left) && IsRed(h.m_right)) FlipColors(h);

        h.m_n = Size(h.m_left) + Size(h.m_right) + 1;
        return h;
    }

    Node Delete(Node h, Key key)
    {
        // assert get(h, key) != null;

        if (m_comparer.Compare(key, h.m_key) < 0)
        {
            if (!IsRed(h.m_left) && !IsRed(h.m_left.m_left))
                h = MoveRedLeft(h);
            h.m_left = Delete(h.m_left, key);
        }
        else
        {
            if (IsRed(h.m_left))
                h = RotateRight(h);
            if (m_comparer.Compare(key, h.m_key) == 0 && (h.m_right == null))
                return null;
            if (!IsRed(h.m_right) && !IsRed(h.m_right.m_left))
                h = MoveRedRight(h);
            if (m_comparer.Compare(key, h.m_key) == 0)
            {
                Node x = Min(h.m_right);
                h.m_key = x.m_key;
                h.m_val = x.m_val;
                // h.val = get(h.right, min(h.right).key);
                // h.key = min(h.right).key;
                h.m_right = deleteMin(h.m_right);
            }
            else h.m_right = Delete(h.m_right, key);
        }
        return Balance(h);
    }

    Node MoveRedLeft(Node h)
    {
        // assert (h != null);
        // assert isRed(h) && !isRed(h.left) && !isRed(h.left.left);

        FlipColors(h);
        if (IsRed(h.m_right.m_left))
        {
            h.m_right = RotateRight(h.m_right);
            h = RotateLeft(h);
            FlipColors(h);
        }
        return h;
    }

    Node MoveRedRight(Node h)
    {
        // assert (h != null);
        // assert isRed(h) && !isRed(h.right) && !isRed(h.right.left);
        FlipColors(h);
        if (IsRed(h.m_left.m_left))
        {
            h = RotateRight(h);
            FlipColors(h);
        }
        return h;
    }
}

