using System;

public static class Tuple
{
    internal static int CombineHashCodes(int h1, int h2)
    {
        return (((h1 << 5) + h1) ^ h2);
    }
    internal static int CombineHashCodes(int h1, int h2, int h3)
    {
        return CombineHashCodes(CombineHashCodes(h1, h2), h3);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
    {
        return CombineHashCodes(CombineHashCodes(h1, h2), CombineHashCodes(h3, h4));
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
    {
        return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), h5);
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
    {
        return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6));
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
    {
        return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7));
    }

    internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
    {
        return CombineHashCodes(CombineHashCodes(h1, h2, h3, h4), CombineHashCodes(h5, h6, h7, h8));
    }
}

public class Tuple<T1> : IComparable<Tuple<T1>> 
    where T1 : IComparable<T1>
{
    private readonly T1 m_Item1;

    public T1 Item1 { get { return m_Item1; } }


    public Tuple(T1 item1)
    {
        m_Item1 = item1;
    }

    public override Boolean Equals(Object obj)
    {
        Tuple<T1> other = (Tuple<T1>)obj;
        return Item1.CompareTo(other.Item1) == 0;
    }

    public Int32 CompareTo(Tuple<T1> obj)
    {
        return Item1.CompareTo(obj.Item1);
    }

    public override int GetHashCode()
    {
        return Item1.GetHashCode();
    }

    public override string ToString()
    {
        return "Tuple " + m_Item1.ToString();
    }

    public int Length = 1;

    public object this[int index]
    {
        get
        {
            if (index != 0)
            {
                throw new IndexOutOfRangeException();
            }
            return Item1;
        }
    }
}

public class Tuple<T1, T2> : IComparable<Tuple<T1, T2>> 
    where T1 : IComparable<T1> 
    where T2 : IComparable<T2>
{
    private readonly T1 m_Item1;
    private readonly T2 m_Item2;

    public T1 Item1 { get { return m_Item1; } }
    public T2 Item2 { get { return m_Item2; } }

    public Tuple(T1 item1, T2 item2)
    {
        m_Item1 = item1;
        m_Item2 = item2;
    }

    public override Boolean Equals(Object obj)
    {
        var other = (Tuple<T1, T2>)obj;
        return Item1.CompareTo(other.Item1) == 0
            && Item2.CompareTo(other.Item2) == 0;
    }

    public Int32 CompareTo(Tuple<T1, T2> obj)
    {
        int c = Item1.CompareTo(obj.Item1);
        if(c != 0)
        {
            return c;
        }

        return Item2.CompareTo(obj.Item2);
    }

    public override int GetHashCode()
    {
        return Tuple.CombineHashCodes(m_Item1.GetHashCode(), m_Item2.GetHashCode());
    }

    public override string ToString()
    {
        return "Tuple " + m_Item1.ToString() + " " +m_Item2.ToString();
    }

    public int Length = 2;

    object this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return Item1;
                case 1:
                    return Item2;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
public class Tuple<T1, T2, T3> : IComparable<Tuple<T1, T2, T3>>
    where T1 : IComparable<T1>
    where T2 : IComparable<T2>
    where T3 : IComparable<T3>
{
    private readonly T1 m_Item1;
    private readonly T2 m_Item2;
    private readonly T3 m_Item3;

    public T1 Item1 { get { return m_Item1; } }
    public T2 Item2 { get { return m_Item2; } }
    public T3 Item3 { get { return m_Item3; } }

    public Tuple(T1 item1, T2 item2, T3 item3)
    {
        m_Item1 = item1;
        m_Item2 = item2;
        m_Item3 = item3;
    }

    public override Boolean Equals(Object obj)
    {
        var other = (Tuple<T1, T2, T3>)obj;
        return Item1.CompareTo(other.Item1) == 0
            && Item2.CompareTo(other.Item2) == 0
            && Item3.CompareTo(other.Item3) == 0;
    }

    public Int32 CompareTo(Tuple<T1, T2, T3> obj)
    {
        int c = Item1.CompareTo(obj.Item1);
        if (c != 0)
        {
            return c;
        }

        c = Item2.CompareTo(obj.Item2);
        if(c != 0)
        {
            return c;
        }
        return Item3.CompareTo(obj.m_Item3);
    }

    public override int GetHashCode()
    {
        return Tuple.CombineHashCodes(m_Item1.GetHashCode(), m_Item2.GetHashCode(), m_Item3.GetHashCode());
    }

    public override string ToString()
    {
        return "Tuple " + m_Item1.ToString() + " " + m_Item2.ToString() + " " + m_Item3.ToString();
    }

    public int Length = 3;

    object this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return Item1;
                case 1:
                    return Item2;
                case 2:
                    return Item3;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}