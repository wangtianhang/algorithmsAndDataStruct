using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;



class ThreadSafeQueue<T>
{
    #region private Fields
    private int isTaked = 0;
    private Queue<T> queue = new Queue<T>();
    private int MaxCount = 1000 * 1000;
    #endregion

    public void Enqueue(T t)
    {
        try
        {
            while (Interlocked.Exchange(ref isTaked, 1) != 0)
            {
            }
            this.queue.Enqueue(t);
        }
        finally
        {
            Thread.VolatileWrite(ref isTaked, 0);
        }
    }

    //     public int Count()
    //     {
    //         try
    //         {
    //             while (Interlocked.Exchange(ref isTaked, 1) != 0)
    //             {
    //                 
    //             }
    //             return this.queue.Count;
    //         }
    //         finally
    //         {
    //             Thread.VolatileWrite(ref isTaked, 0);
    //         }
    //     }

    public T Dequeue()
    {
        try
        {
            while (Interlocked.Exchange(ref isTaked, 1) != 0)
            {
            }
            T t = this.queue.Dequeue();
            return t;
        }
        finally
        {
            Thread.VolatileWrite(ref isTaked, 0);
        }
    }

    public bool TryEnqueue(T t)
    {
        try
        {
            for (int i = 0; i < MaxCount; i++)
            {
                if (Interlocked.Exchange(ref isTaked, 1) == 0)
                {
                    this.queue.Enqueue(t);
                    return true;
                }
            }
            return false;
        }
        finally
        {
            Thread.VolatileWrite(ref isTaked, 0);
        }
    }

    public bool TryDequeue(out T t)
    {
        try
        {
            for (int i = 0; i < MaxCount; i++)
            {
                if (Interlocked.Exchange(ref isTaked, 1) == 0)
                {
                    t = this.queue.Dequeue();
                    return true;
                }
            }
            t = default(T);
            return false;
        }
        finally
        {
            Thread.VolatileWrite(ref isTaked, 0);
        }
    }
}

