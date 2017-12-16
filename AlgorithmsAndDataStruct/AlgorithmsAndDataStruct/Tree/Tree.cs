using System;
using System.Collections.Generic;
using System.Text;

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
        Tree<int>.LayerOrderTraversal(one, TravelHandle);
        Debug.Log("先序遍历");
        Tree<int>.PreOrderTraversal(one, TravelHandle);
        Debug.Log("中序遍历");
        Tree<int>.InOrderTraversal(one, TravelHandle);
        Debug.Log("后序遍历");
        Tree<int>.PostOrderTraversal(one, TravelHandle);
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
    public static void LayerOrderTraversal(TreeNode<T> node, TreeTravelCallback callback)
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
    public static void PreOrderTraversal(TreeNode<T> node, TreeTravelCallback callback)
    {
        callback(node);
        if(node.m_left != null)
        {
            PreOrderTraversal(node.m_left, callback);
        }
        if(node.m_right != null)
        {
            PreOrderTraversal(node.m_right, callback);
        }
    }
    /// <summary>
    /// 中序遍历
    /// </summary>
    public static void InOrderTraversal(TreeNode<T> node, TreeTravelCallback callback)
    {
        if (node.m_left != null)
        {
            PreOrderTraversal(node.m_left, callback);
        }
        callback(node);
        if (node.m_right != null)
        {
            PreOrderTraversal(node.m_right, callback);
        }
    }

    /// <summary>
    /// 后序遍历
    /// </summary>
    public static void PostOrderTraversal(TreeNode<T> node, TreeTravelCallback callback)
    {
        if (node.m_left != null)
        {
            PreOrderTraversal(node.m_left, callback);
        }
        
        if (node.m_right != null)
        {
            PreOrderTraversal(node.m_right, callback);
        }
        callback(node);
    }
}

