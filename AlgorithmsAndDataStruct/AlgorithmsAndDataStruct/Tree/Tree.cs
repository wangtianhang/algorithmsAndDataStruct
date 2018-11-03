using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TreeNode<T>
{
    public TreeNode(T data)
    {
        m_data = data;
    }
    public T m_data = default(T);
    public TreeNode<T> m_left = null;
    public TreeNode<T> m_right = null;
}

public class TestTree
{
    public static void Test()
    {
        TreeNode<int> one = new TreeNode<int>(1);
        TreeNode<int> two = one.m_left = new TreeNode<int>(2);
        TreeNode<int> three = one.m_right = new TreeNode<int>(3);
        two.m_left = new TreeNode<int>(4);
        two.m_right = new TreeNode<int>(5);
        three.m_left = new TreeNode<int>(6);
        three.m_right = new TreeNode<int>(7);

        Debug.Log("层序遍历");
        Tree<int>.LayerOrderTraversal_Iteration(one, TravelHandle);
        Debug.Log("先序遍历递归");
        Tree<int>.PreOrderTraversal_Recursion(one, TravelHandle);
        Debug.Log("先序遍历迭代");
        Tree<int>.PreOrderTraversal_Iteration(one, TravelHandle);
        Debug.Log("中序遍历递归");
        Tree<int>.InOrderTraversal_Recursion(one, TravelHandle);
        Debug.Log("中序遍历迭代");
        Tree<int>.InOrderTraversal_Iteration(one, TravelHandle);
        Debug.Log("后序遍历递归");
        Tree<int>.PostOrderTraversal_Recursion(one, TravelHandle);
        Debug.Log("后序遍历迭代");
        Tree<int>.PostOrderTraversal_Iteration(one, TravelHandle);
    }

    public static void TravelHandle(TreeNode<int> node)
    {
        Debug.Log(node.m_data.ToString());
    }
}

public class Tree<T>
{
    public delegate void TreeTravelCallback(TreeNode<T> node);

    /// <summary>
    /// 层序遍历
    /// </summary>
    public static void LayerOrderTraversal_Iteration(TreeNode<T> node, TreeTravelCallback callback)
    {
        Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
        queue.Enqueue(node);
        while(queue.Count != 0)
        {
            TreeNode<T> front = queue.Dequeue();
            callback(front);
            if(front.m_left != null)
            {
                queue.Enqueue(front.m_left);
            }
            if(front.m_right != null)
            {
                queue.Enqueue(front.m_right);
            }
        }
    }

    /// <summary>
    /// 先序遍历
    /// </summary>
    public static void PreOrderTraversal_Recursion(TreeNode<T> node, TreeTravelCallback callback)
    {
        callback(node);
        if(node.m_left != null)
        {
            PreOrderTraversal_Recursion(node.m_left, callback);
        }
        if(node.m_right != null)
        {
            PreOrderTraversal_Recursion(node.m_right, callback);
        }
    }
    public static void PreOrderTraversal_Iteration(TreeNode<T> node, TreeTravelCallback callback)
    {
        Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
        stack.Push(node);
        while(stack.Count != 0)
        {
            TreeNode<T> first = stack.Pop();
            callback(first);
            if(first.m_right != null)
            {
                stack.Push(first.m_right);
            }
            if(first.m_left != null)
            {
                stack.Push(first.m_left);
            }
        }
    }
    /// <summary>
    /// 中序遍历
    /// </summary>
    public static void InOrderTraversal_Recursion(TreeNode<T> node, TreeTravelCallback callback)
    {
        if (node.m_left != null)
        {
            InOrderTraversal_Recursion(node.m_left, callback);
        }
        callback(node);
        if (node.m_right != null)
        {
            InOrderTraversal_Recursion(node.m_right, callback);
        }
    }

    public static void InOrderTraversal_Iteration(TreeNode<T> node, TreeTravelCallback callback)
    {
        Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
        while(stack.Count != 0 || node != null)
        {
            if(node != null)
            {
                stack.Push(node);
                node = node.m_left;
            }
            else
            {
                node = stack.Pop();
                callback(node);
                node = node.m_right;
            }
        }
    }

    /// <summary>
    /// 后序遍历
    /// </summary>
    public static void PostOrderTraversal_Recursion(TreeNode<T> node, TreeTravelCallback callback)
    {
        if (node.m_left != null)
        {
            PostOrderTraversal_Recursion(node.m_left, callback);
        }
        
        if (node.m_right != null)
        {
            PostOrderTraversal_Recursion(node.m_right, callback);
        }
        callback(node);
    }

    public static void PostOrderTraversal_Iteration(TreeNode<T> head, TreeTravelCallback callback)
    {
        if(head != null)
        {
            Stack<TreeNode<T>> s1 = new Stack<TreeNode<T>>();
            Stack<TreeNode<T>> s2 = new Stack<TreeNode<T>>();
            s1.Push(head);
            while(s1.Count != 0)
            {
                head = s1.Pop();
                s2.Push(head);
                if(head.m_left != null)
                {
                    s1.Push(head.m_left);
                }
                if(head.m_right != null)
                {
                    s1.Push(head.m_right);
                }
            }
            while(s2.Count != 0)
            {
                callback(s2.Pop());
            }
        }
    }
}

