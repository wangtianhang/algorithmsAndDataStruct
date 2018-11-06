//using System;
using System.Collections.Generic;

using System.Text;

namespace Common
{
    // c库的random不符合正态分布...
//     public class Random
//     {
//         long m_curRandom = 0;
//         int m_seed = 0;
//         public Random()
//         {
// 
//         }
// 
//         public Random(int seed)
//         {
//             m_seed = seed;
//             m_curRandom = m_seed;
//         }
// 
//         public int Next(int minInclusive, int maxExclusive)
//         {
//             int num = maxExclusive - minInclusive;
//             int last = Rand() % num;
//             int ret = minInclusive + last;
//             return ret;
//         }
// 
//         public int Next()
//         {
//             return Rand();
//         }
// 
//         int Rand()
//         {
//             m_curRandom = (m_curRandom * 214013L + 2531011L) >> 16 & 0x7fff;
//             return (int)m_curRandom;
//         }
//     }
}

public class Random
{
    //static System.Random s_ran = null;
    static System.Random s_ran = null;
    static void Init()
    {
        if (s_ran == null)
        {
            long tick = System.DateTime.Now.Ticks;
            s_ran = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }
    }

    public static void SetSeed(int seed)
    {
        s_ran = new System.Random(seed);
    }

    /// <summary>
    /// 左闭右开区间 与unity一致
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static int Range(int min, int max)
    {
        Init();

        return s_ran.Next(min, max);
    }

    /// <summary>
    /// 左闭右闭区间 与unity一致
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static float Range(float minInclusive, float maxInclusive)
    {
        Init();

        int randomInteger = s_ran.Next(0, int.MaxValue);
        float randomFloat = (float)randomInteger / (float)int.MaxValue;
        float range = maxInclusive - minInclusive;
        float ret = minInclusive + randomFloat * range;
        //Debug.Log("Range float Index " + s_index + " " + ret);
        return ret;
    }

//     static int uniform(int n)
//     {
//         Init();
// 
//         return s_ran.Next() % n;
//     }


}

