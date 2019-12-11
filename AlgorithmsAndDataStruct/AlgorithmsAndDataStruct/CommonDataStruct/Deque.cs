using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 双端队列
/// </summary>
class Deque
{
        // Driver program to test above function 
    public static void Test()
    { 
          
         Deque dq = new Deque(5); 
           
         Console.WriteLine("Insert element at rear end  : 5 "); 
         dq.insertrear(5); 
           
         Console.WriteLine("insert element at rear end : 10 "); 
         dq.insertrear(10); 
           
         Console.WriteLine("get rear element : "+ dq.getRear()); 
           
         dq.deleterear(); 
         Console.WriteLine("After delete rear element new rear become : " +  
                                dq.getRear()); 
           
         Console.WriteLine("inserting element at front end"); 
         dq.insertfront(15); 
           
         Console.WriteLine("get front element: " +dq.getFront()); 
           
         dq.deletefront();

         Console.WriteLine("After delete front element new front become : " + 
                                    +  dq.getFront()); 
          
    } 

    const int MAX = 100; 
    int  []arr; 
    int  front; 
    int  rear; 
    int  size; 
      
    public Deque(int size) 
    { 
        arr = new int[MAX]; 
        front = -1; 
        rear = 0; 
        this.size = size; 
    } 
   
    /*// Operations on Deque: 
    void  insertfront(int key); 
    void  insertrear(int key); 
    void  deletefront(); 
    void  deleterear(); 
    bool  isFull(); 
    bool  isEmpty(); 
    int  getFront(); 
    int  getRear();*/
   
    // Checks whether Deque is full or not. 
    bool isFull() 
    { 
        return ((front == 0 && rear == size-1)|| 
            front == rear+1); 
    } 
   
    // Checks whether Deque is empty or not. 
    bool isEmpty () 
    { 
        return (front == -1); 
    } 
   
    // Inserts an element at front 
    void insertfront(int key) 
    { 
        // check whether Deque if  full or not 
        if (isFull()) 
        { 
            Console.WriteLine("Overflow");  
            return; 
        } 
   
        // If queue is initially empty 
        if (front == -1) 
        { 
            front = 0; 
            rear = 0; 
        } 
          
        // front is at first position of queue 
        else if (front == 0) 
            front = size - 1 ; 
   
        else // decrement front end by '1' 
            front = front-1; 
   
        // insert current element into Deque 
        arr[front] = key ; 
    } 
   
    // function to inset element at rear end 
    // of Deque. 
    void insertrear(int key) 
    { 
        if (isFull()) 
        {
            Console.WriteLine(" Overflow "); 
            return; 
        } 
   
        // If queue is initially empty 
        if (front == -1) 
        { 
            front = 0; 
            rear = 0; 
        } 
   
        // rear is at last position of queue 
        else if (rear == size-1) 
            rear = 0; 
   
        // increment rear end by '1' 
        else
            rear = rear+1; 
          
        // insert current element into Deque 
        arr[rear] = key ; 
    } 
   
    // Deletes element at front end of Deque 
    void deletefront() 
    { 
        // check whether Deque is empty or not 
        if (isEmpty()) 
        {
            Console.WriteLine("Queue Underflow\n"); 
            return ; 
        } 
   
        // Deque has only one element 
        if (front == rear) 
        { 
            front = -1; 
            rear = -1; 
        } 
        else
            // back to initial position 
            if (front == size -1) 
                front = 0; 
   
            else // increment front by '1' to remove current 
                // front value from Deque 
                front = front+1; 
    } 
   
    // Delete element at rear end of Deque 
    void deleterear() 
    { 
        if (isEmpty()) 
        {
            Console.WriteLine(" Underflow"); 
            return ; 
        } 
   
        // Deque has only one element 
        if (front == rear) 
        { 
            front = -1; 
            rear = -1; 
        } 
        else if (rear == 0) 
            rear = size-1; 
        else
            rear = rear-1; 
    } 
   
    // Returns front element of Deque 
    int getFront() 
    { 
        // check whether Deque is empty or not 
        if (isEmpty()) 
        {
            Console.WriteLine(" Underflow"); 
            return -1 ; 
        } 
        return arr[front]; 
    } 
   
    // function return rear element of Deque 
    int getRear() 
    { 
        // check whether Deque is empty or not 
        if(isEmpty() || rear < 0) 
        {
            Console.WriteLine(" Underflow\n"); 
            return -1 ; 
        } 
        return arr[rear]; 
    }
}

