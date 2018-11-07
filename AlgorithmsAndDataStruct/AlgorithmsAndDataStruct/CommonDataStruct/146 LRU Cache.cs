using System;
using System.Collections.Generic;

using System.Text;


/// <summary>
/// 双向链表配合hashmap
/// </summary>
public class LRUCache
{
    public class Node
    {
        public Node pre, next;
        public int key;
        public int val;
        public Node(int key, int value)
        {
            this.key = key;
            this.val = value;
        }
    }

    private int capacity;
    private int num;
    private Dictionary<int, Node> map;
    private Node first, last;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        num = 0;
        map = new Dictionary<int, Node>();
        first = null;
        last = null;
    }

    public int Get(int key)
    {
        Node node = null;
        map.TryGetValue(key, out node);
        if (node == null)
            return -1;
        else if (node != last)
        {
            if (node == first)
                first = first.next;
            else
                node.pre.next = node.next;
            node.next.pre = node.pre;
            last.next = node;
            node.pre = last;
            node.next = null;
            last = node;
        }
        return node.val;
    }

    public void Put(int key, int value)
    {
        Node node = null;
        map.TryGetValue(key, out node);
        if (node != null)
        {
            node.val = value;
            if (node != last)
            {
                if (node == first)
                    first = first.next;
                else
                    node.pre.next = node.next;
                node.next.pre = node.pre;
                last.next = node;
                node.pre = last;
                node.next = null;
                last = node;
            }
        }
        else
        {
            Node newNode = new Node(key, value);

            if (num >= capacity)
            {
                map.Remove(first.key);
                first = first.next;
                if (first != null)
                    first.pre = null;
                else
                    last = null;
                num--;
            }
            if (first == null || last == null)
            {
                first = newNode;
            }
            else
            {
                last.next = newNode;
            }
            newNode.pre = last;
            last = newNode;
            map.Add(key, newNode);
            num++;
        }

    }
}  
