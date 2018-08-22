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

public class QuadTree
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
    const int MAX_ELE_NUM = 100;

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

    void InsertElement(QuadTreeElement element)
    {
        InsertElementRecursion(m_root, element);
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
    void InsertElementRecursion(QuadTreeNode node, QuadTreeElement ele)
    {
        if (node.m_isLeaf) 
        {
            if (node.m_elementList.Count > MAX_ELE_NUM) 
            {
                splitNode(node);
                InsertElementRecursion(node, ele);
            } 
            else 
            {
                // todo 点排重（不排重的话如果相同的点数目大于 MAX_ELE_NUM， 会造成无限循环分裂）
//                 struct ElePoint *ele_ptr = (struct ElePoint *) malloc(sizeof(struct ElePoint));
//                 ele_ptr->lat = ele.lat;
//                 ele_ptr->lng = ele.lng;
//                 strcpy(ele_ptr->desc, ele.desc);
//                 node->ele_list[node->ele_num] = ele_ptr;
//                 node->ele_num++;
                node.m_elementList.Add(ele);
            }

            return;
        }


        float mid_vertical = (node.m_region.top + node.m_region.bottom) / 2;
        float mid_horizontal = (node.m_region.left + node.m_region.right) / 2;
        if (ele.lat > mid_vertical) {
            if (ele.lng > mid_horizontal) {
                InsertElementRecursion(node.m_ru, ele);
            } else {
                InsertElementRecursion(node.m_lu, ele);
            }
        } else {
            if (ele.lng > mid_horizontal) {
                InsertElementRecursion(node.m_rb, ele);
            } else {
                InsertElementRecursion(node.m_lb, ele);
            }
        }
    }


    /**
     * 分裂结点
     * 1.通过父结点获取子结点的深度和范围
     * 2.生成四个结点，挂载到父结点下
     *
     * @param node
     */
    void splitNode(QuadTreeNode node) {
        float mid_vertical = (node.m_region.top + node.m_region.bottom) / 2;
        float mid_horizontal = (node.m_region.left + node.m_region.right) / 2;

        node.m_isLeaf = false;
        node.m_ru = createChildNode(node, mid_vertical, node.m_region.top, mid_horizontal, node.m_region.right);
        node.m_lu = createChildNode(node, mid_vertical, node.m_region.top, node.m_region.left, mid_horizontal);
        node.m_rb = createChildNode(node, node.m_region.bottom, mid_vertical, mid_horizontal, node.m_region.right);
        node.m_lb = createChildNode(node, node.m_region.bottom, mid_vertical, node.m_region.left, mid_horizontal);

//         for (int i = 0; i < node.ele_num; i++) {
//             insertEle(node, *node.ele_list[i]);
//             free(node.ele_list[i]);
//             node.ele_num--;
//         }
        foreach(var iter in node.m_elementList)
        {
            InsertElementRecursion(node, iter);
        }
        node.m_elementList.Clear();
    }

    QuadTreeNode createChildNode(QuadTreeNode node, float bottom, float up, float left, float right) 
    {
        int depth = node.m_depth + 1;
        //struct QuadTreeNode *childNode = (struct QuadTreeNode *) malloc(sizeof(struct QuadTreeNode));
        QuadTreeNode childNode = new QuadTreeNode();
        //struct Region *region = (struct Region *) malloc(sizeof(struct Region));
        //initRegion(region, bottom, up, left, right);
        //initNode(childNode, depth, *region);
        childNode.m_region = new Rect(left, up, right - left, up - bottom);
        childNode.m_isLeaf = true;
        childNode.m_depth = depth;

        return childNode;
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

        float mid_vertical = (node.m_region.top + node.m_region.bottom) / 2;
        float mid_horizontal = (node.m_region.left + node.m_region.right) / 2;

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

