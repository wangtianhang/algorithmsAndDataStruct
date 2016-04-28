using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetCode
{
    class _31Next_Permutation
    {
        static void Main2(string[] args)
        {
            _31Next_Permutation test = new _31Next_Permutation();
            int[] nums = new int[] { 1, 2, 3 };
            test.NextPermutation(nums);

            for (int i = 0; i < nums.Length; ++i )
            {
                Console.WriteLine(nums[i]);
            }

            Console.Read();
        }

        public void NextPermutation(int[] nums)
        {
            next_permutation(nums, 0, nums.Length);
        }

        bool next_permutation(int[] nums, int first, int last )
        {  
            if(first == last)  
                return false; //空序列  
  
            int i = first;  
            ++i;  
            if(i == last)  
                return false;  //一个元素，没有下一个序列了  
      
            i = last;  
            --i;  
  
            for(;;) {  
                int ii = i;  
                --i;  
                if(nums[i] < nums[ii]) {  
                    int j = last;  
                    while(!(nums[i] < nums[--j]));

                    int tmp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = tmp;
                    //iter_swap(i, j);
                    Array.Reverse(nums, ii, last - ii);
                    //nums.Reverse reverse(ii, last);
                    return true;  
                }  
          
                if(i == first) {  
                    //reverse(first, last);  //全逆向，即为最小字典序列，如cba变为abc
                    Array.Reverse(nums, first, last - first);
                    return false;  
                }  
            }   
        }
  
    }
}
