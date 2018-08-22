using System;
using System.Collections.Generic;
using System.Text;

// https://github.com/zhenbianshu/spatialIndex

class QuadTreeElement
{
    public float lng; // lng
    public float lat; // lat
    public string m_des = "";
}

class QuadTreeNode
{
    public int m_depth = 0;
    public bool m_isLeaf = false;
    public Rect m_region;
    public QuadTreeNode m_lu = null;
    public QuadTreeNode m_lb = null;
    public QuadTreeNode m_ru = null;
    public QuadTreeNode m_rb = null;
    //public int m_elementCount = 0;
    public List<QuadTreeElement> m_elementList = new List<QuadTreeElement>();
}

class QuadTree
{
    public static void Test()
    {
        QuadTree quadTree = new QuadTree();
        quadTree.Init(-90, 90, -180, 180);
        Common.Random random = new Common.Random();
        for (int i = 0; i < 100000; i++)
        {
            QuadTreeElement element = new QuadTreeElement();
            element.lng = (float)(random.Next() % 360 - 180 + (float)(random.Next() % 1000) / 1000);
            element.lat = (float)(random.Next() % 180 - 90 + (float)(random.Next() % 1000) / 1000);
            quadTree.InsertElement(element);
        }

        QuadTreeElement test = new QuadTreeElement();
        test.lng = -24f;
        test.lat = -45.4f;
        List<QuadTreeElement> retList = quadTree.queryEle(test);
        Debug.Log("附近有 " + retList.Count + " 个点");
        foreach(var iter in retList)
        {
            Debug.Log(iter.lng + " " + iter.lat);
        }
    }

    QuadTreeNode m_root = null;
    Rect m_rootRegion;

    void Init(float bottom, float up, float left, float right)
    {
        m_rootRegion = new Rect(left, up, right - left, up - bottom);
        m_root = new QuadTreeNode();
        m_root.m_depth = 1;
        m_root.m_isLeaf = true;
        //m_root.m_elementCount = 0;
        m_root.m_region = m_rootRegion;
    }

    void deleteEle(QuadTreeElement ele) {
    /**
     * 1.遍历元素列表，删除对应元素
     * 2.检查兄弟象限元素总数，不超过最大量时组合兄弟象限
     */
    }

    void combineNode(QuadTreeNode node) {
        /**
         * 遍历四个子象限的点，添加到象限点列表
         * 释放子象限的内存
         */
    }

    /**
     * 插入元素
     * 1.判断是否已分裂，已分裂的选择适合的子结点，插入；
     * 2.未分裂的查看是否过载，过载的分裂结点，重新插入；
     * 3.未过载的直接添加
     *
     * @param node
     * @param ele
     * todo 使用元素原地址，避免重新分配内存造成的效率浪费
     */
    void InsertElement(QuadTreeElement element)
    {

    }

    List<QuadTreeElement> queryEle(QuadTreeElement element)
    {
        List<QuadTreeElement> ret = new List<QuadTreeElement>();
        queryEleRecursion(m_root, element, ret);
        return ret;
    }

    void queryEleRecursion(QuadTreeNode node, QuadTreeElement ele, List<QuadTreeElement> ret)
    {
        if(node.m_isLeaf)
        {
            ret.AddRange(node.m_elementList);
            return;
        }

        double mid_vertical = (node.m_region.top + node.m_region.bottom) / 2;
        double mid_horizontal = (node.m_region.left + node.m_region.right) / 2;

        if (ele.lat > mid_vertical)
        {
            if (ele.lng > mid_horizontal)
            {
                queryEleRecursion(node.m_ru, ele, ret);
            }
            else
            {
                queryEleRecursion(node.m_lu, ele, ret);
            }
        }
        else
        {
            if (ele.lng > mid_horizontal)
            {
                queryEleRecursion(node.m_rb, ele, ret);
            }
            else
            {
                queryEleRecursion(node.m_lb, ele, ret);
            }
        }
    }
}

