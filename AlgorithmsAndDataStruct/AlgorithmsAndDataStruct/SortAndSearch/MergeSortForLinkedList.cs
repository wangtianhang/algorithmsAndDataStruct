using System;
using System.Collections.Generic;
using System.Text;


class MergeSortForLinkedList
{
    node head = null; 

    class node
    {
        public int val;
        public node next;

        public node(int val)
        {
            this.val = val;
        }
    }

    // Utility function to get the middle of the linked list 
    node getMiddle(node h)
    {
        //Base case 
        if (h == null)
            return h;
        node fastptr = h.next;
        node slowptr = h;

        // Move fastptr by two and slow ptr by one 
        // Finally slowptr will point to middle node 
        while (fastptr != null)
        {
            fastptr = fastptr.next;
            if (fastptr != null)
            {
                slowptr = slowptr.next;
                fastptr = fastptr.next;
            }
        }
        return slowptr;
    }

    node sortedMerge(node a, node b)
    {
        node result = null;
        /* Base cases */
        if (a == null)
            return b;
        if (b == null)
            return a;

        /* Pick either a or b, and recur */
        if (a.val <= b.val)
        {
            result = a;
            result.next = sortedMerge(a.next, b);
        }
        else
        {
            result = b;
            result.next = sortedMerge(a, b.next);
        }
        return result;

    } 

    node mergeSort(node h)
    {
        // Base case : if head is null 
        if (h == null || h.next == null)
        {
            return h;
        }

        // get the middle of the list 
        node middle = getMiddle(h);
        node nextofmiddle = middle.next;

        // set the next of middle node to null 
        middle.next = null;

        // Apply mergeSort on left list 
        node left = mergeSort(h);

        // Apply mergeSort on right list 
        node right = mergeSort(nextofmiddle);

        // Merge the left and right lists 
        node sortedlist = sortedMerge(left, right);
        return sortedlist;
    } 
}

