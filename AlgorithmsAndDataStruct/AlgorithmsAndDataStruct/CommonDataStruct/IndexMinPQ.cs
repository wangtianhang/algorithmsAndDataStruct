﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 小顶堆，求海量元素的前n个大元素
public class IndexMinPQ<Key> {
    private int maxN;        // maximum number of elements on PQ
    private int N;           // number of elements on PQ
    private int[] pq;        // binary heap using 1-based indexing
    private int[] qp;        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
    private Key[] keys;      // keys[i] = priority of i

    static Comparer<Key> m_comparer;
    /**
     * Initializes an empty indexed priority queue with indices between <tt>0</tt>
     * and <tt>maxN - 1</tt>.
     * @param  maxN the keys on this priority queue are index from <tt>0</tt>
     *         <tt>maxN - 1</tt>
     * @throws IllegalArgumentException if <tt>maxN</tt> &lt; <tt>0</tt>
     */
    public IndexMinPQ(int maxN) {
        if (maxN < 0) throw new Exception();
        this.maxN = maxN;
        keys = new Key[maxN + 1];    // make this of length maxN??
        pq   = new int[maxN + 1];
        qp   = new int[maxN + 1];                   // make this of length maxN??
        for (int i = 0; i <= maxN; i++)
            qp[i] = -1;
    }

    /**
     * Returns true if this priority queue is empty.
     *
     * @return <tt>true</tt> if this priority queue is empty;
     *         <tt>false</tt> otherwise
     */
    public bool isEmpty() {
        return N == 0;
    }

    /**
     * Is <tt>i</tt> an index on this priority queue?
     *
     * @param  i an index
     * @return <tt>true</tt> if <tt>i</tt> is an index on this priority queue;
     *         <tt>false</tt> otherwise
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     */
    public bool contains(int i) {
        if (i < 0 || i >= maxN) throw new Exception();
        return qp[i] != -1;
    }

    /**
     * Returns the number of keys on this priority queue.
     *
     * @return the number of keys on this priority queue
     */
    public int size() {
        return N;
    }

    /**
     * Associates key with index <tt>i</tt>.
     *
     * @param  i an index
     * @param  key the key to associate with index <tt>i</tt>
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @throws IllegalArgumentException if there already is an item associated
     *         with index <tt>i</tt>
     */
    public void insert(int i, Key key) {
        if (i < 0 || i >= maxN) throw new Exception();
        if (contains(i)) throw new Exception("index is already in the priority queue");
        N++;
        qp[i] = N;
        pq[N] = i;
        keys[i] = key;
        swim(N);
    }

    /**
     * Returns an index associated with a minimum key.
     *
     * @return an index associated with a minimum key
     * @throws NoSuchElementException if this priority queue is empty
     */
    public int minIndex() {
        if (N == 0) throw new Exception("Priority queue underflow");
        return pq[1];
    }

    /**
     * Returns a minimum key.
     *
     * @return a minimum key
     * @throws NoSuchElementException if this priority queue is empty
     */
    public Key minKey() {
        if (N == 0) throw new Exception("Priority queue underflow");
        return keys[pq[1]];
    }

    /**
     * Removes a minimum key and returns its associated index.
     * @return an index associated with a minimum key
     * @throws NoSuchElementException if this priority queue is empty
     */
    public int delMin() {
        if (N == 0) throw new Exception("Priority queue underflow");
        int min = pq[1];
        exch(1, N--);
        sink(1);
        qp[min] = -1;        // delete
        keys[min] = default(Key);    // to help with garbage collection
        pq[N+1] = -1;        // not needed
        return min;
    }

    /**
     * Returns the key associated with index <tt>i</tt>.
     *
     * @param  i the index of the key to return
     * @return the key associated with index <tt>i</tt>
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @throws NoSuchElementException no key is associated with index <tt>i</tt>
     */
    public Key keyOf(int i) {
        if (i < 0 || i >= maxN) throw new Exception();
        if (!contains(i)) throw new Exception("index is not in the priority queue");
        else return keys[i];
    }

    /**
     * Change the key associated with index <tt>i</tt> to the specified value.
     *
     * @param  i the index of the key to change
     * @param  key change the key associated with index <tt>i</tt> to this key
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @throws NoSuchElementException no key is associated with index <tt>i</tt>
     */
    public void changeKey(int i, Key key) {
        if (i < 0 || i >= maxN) throw new Exception();
        if (!contains(i)) throw new Exception("index is not in the priority queue");
        keys[i] = key;
        swim(qp[i]);
        sink(qp[i]);
    }

    /**
     * Change the key associated with index <tt>i</tt> to the specified value.
     *
     * @param  i the index of the key to change
     * @param  key change the key associated with index <tt>i</tt> to this key
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @deprecated Replaced by {@link #changeKey(int, Key)}.
     */
    public void change(int i, Key key) {
        changeKey(i, key);
    }

    /**
     * Decrease the key associated with index <tt>i</tt> to the specified value.
     *
     * @param  i the index of the key to decrease
     * @param  key decrease the key associated with index <tt>i</tt> to this key
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @throws IllegalArgumentException if key &ge; key associated with index <tt>i</tt>
     * @throws NoSuchElementException no key is associated with index <tt>i</tt>
     */
    public void decreaseKey(int i, Key key) {
        if (i < 0 || i >= maxN) throw new Exception();
        if (!contains(i)) throw new Exception("index is not in the priority queue");
        if (m_comparer.Compare(keys[i], key) <= 0)
            throw new Exception("Calling decreaseKey() with given argument would not strictly decrease the key");
        keys[i] = key;
        swim(qp[i]);
    }

    /**
     * Increase the key associated with index <tt>i</tt> to the specified value.
     *
     * @param  i the index of the key to increase
     * @param  key increase the key associated with index <tt>i</tt> to this key
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @throws IllegalArgumentException if key &le; key associated with index <tt>i</tt>
     * @throws NoSuchElementException no key is associated with index <tt>i</tt>
     */
    public void increaseKey(int i, Key key) {
        if (i < 0 || i >= maxN) throw new Exception();
        if (!contains(i)) throw new Exception("index is not in the priority queue");
        if (m_comparer.Compare(keys[i], key) >= 0)
            throw new Exception("Calling increaseKey() with given argument would not strictly increase the key");
        keys[i] = key;
        sink(qp[i]);
    }

    /**
     * Remove the key associated with index <tt>i</tt>.
     *
     * @param  i the index of the key to remove
     * @throws IndexOutOfBoundsException unless 0 &le; <tt>i</tt> &lt; <tt>maxN</tt>
     * @throws NoSuchElementException no key is associated with index <t>i</tt>
     */
    public void delete(int i) {
        if (i < 0 || i >= maxN) throw new Exception();
        if (!contains(i)) throw new Exception("index is not in the priority queue");
        int index = qp[i];
        exch(index, N--);
        swim(index);
        sink(index);
        keys[i] = default(Key);
        qp[i] = -1;
    }


   /***************************************************************************
    * General helper functions.
    ***************************************************************************/
    private bool greater(int i, int j) {
        return m_comparer.Compare(keys[pq[i]], keys[pq[j]]) > 0;
    }

    private void exch(int i, int j) {
        int swap = pq[i];
        pq[i] = pq[j];
        pq[j] = swap;
        qp[pq[i]] = i;
        qp[pq[j]] = j;
    }


   /***************************************************************************
    * Heap helper functions.
    ***************************************************************************/
    private void swim(int k) {
        while (k > 1 && greater(k/2, k)) {
            exch(k, k/2);
            k = k/2;
        }
    }

    private void sink(int k) {
        while (2*k <= N) {
            int j = 2*k;
            if (j < N && greater(j, j+1)) j++;
            if (!greater(k, j)) break;
            exch(k, j);
            k = j;
        }
    }
}
