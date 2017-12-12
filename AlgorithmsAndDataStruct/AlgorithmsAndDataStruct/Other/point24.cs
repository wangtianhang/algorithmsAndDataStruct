using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

class Point24
{
    public static void Test()
    {
        //List<int> test = new List<int>();
        List<string> totalResult = new List<string>();

        //int count = 0;
        for (int a = 1; a <= 13; ++a )
        {
            for (int b = 1; b <= 13; ++b )
            {
                for (int c = 1; c <= 13; ++c )
                {
                    for (int d = 1; d <= 13; ++d )
                    {
                        Point24 point24 = new Point24();
                        List<string> results = point24.IsCanPoint24(a, b, c, d);
                        foreach (var result in results)
                        {
                            Console.WriteLine(result);
                            //count++;
                        }

                        totalResult.AddRange(results);
                    }
                }
            }
        }

        Console.WriteLine("total " + totalResult.Count);
        File.WriteAllLines("total24PointResult.txt", totalResult.ToArray(), Encoding.UTF8);
        Console.ReadLine();
    }
    
//     enum Operator
//     {
//         None,
//         Add,
//         Subtract,
//         Multiply,
//         Divide,
//     }

//     class OneCalculate
//     {
//         public float m_param1;
//         public string m_param1Source;
//         public float m_param2;
//         public string m_param2Source;
//         public Operator m_operator = Operator.None;
//     }

    List<string> m_result = new List<string>();
    //List<OneCalculate> m_calculateList = new List<OneCalculate>();

    class Param
    {
        public Param()
        {

        }

        public Param(float param, string source = "")
        {
            m_param = param;
            _m_paramSource = source;
        }
        public float m_param = 0;

        string _m_paramSource = "";
        public string m_paramSource 
        {
            get { return _m_paramSource; }
            set 
            {
                _m_paramSource = value;
                if(value.Contains("12"))
                {
                    int test = 0;
                }
            }
        }
    }

    public List<string> IsCanPoint24(int a, int b, int c, int d)
    {
        //List<string> ret = new List<string>();

        List<Param> set = new List<Param>();
        set.Add(new Param(a, a.ToString()));
        set.Add(new Param(b, b.ToString()));
        set.Add(new Param(c, c.ToString()));
        set.Add(new Param(d, d.ToString()));

        Check24(set);

        Dictionary<string, int> resultDic = new Dictionary<string, int>();
        foreach (var iter in m_result)
        {
            if(resultDic.ContainsKey(iter))
            {
                resultDic[iter] += 1;
            }
            else
            {
                resultDic.Add(iter, 1);
            }
        }
        List<string> noRepeatResult = new List<string>();
        foreach (var iter in resultDic)
        {
            noRepeatResult.Add(iter.Key);
        }
        return noRepeatResult;
    }

    void Check24(List<Param> inputSet)
    {
        if(inputSet.Count == 2)
        {
            
            //OneCalculate oneCalculate = new OneCalculate();
            //oneCalculate.m_param1 = inputSet[0];
            //oneCalculate.m_param2 = inputSet[1];

            for (int i = 1; i <= 4; ++i )
            {
                //oneCalculate.m_operator = (Operator)i;
                float result = 0;
                string operatorStr = "";

                if (i == 1)
                {
                    operatorStr = "+";
                    result = inputSet[0].m_param + inputSet[1].m_param;
                }
                else if (i == 2)
                {
                    operatorStr = "-";
                    result = inputSet[0].m_param - inputSet[1].m_param;
                }
                else if (i == 3)
                {
                    operatorStr = "*";
                    result = inputSet[0].m_param * inputSet[1].m_param;
                }
                else if (i == 4)
                {
                    operatorStr = "/";
                    if (Math.Abs(inputSet[1].m_param) < 0.01)
                    {
                        result = float.MaxValue;
                    }
                    else
                    {
                        result = inputSet[0].m_param / inputSet[1].m_param;
                    }
                }

                //m_calculateList.Add(oneCalculate);

                if (Math.Abs(result - 24) < 0.01f)
                {
                    //Check24AndGenExpression();
                    string express = "(" + inputSet[0].m_paramSource + operatorStr + inputSet[1].m_paramSource + ")";
                    m_result.Add(express);
                }

                //m_calculateList.RemoveAt(m_calculateList.Count - 1);
            }
        }
        else
        {
            CombineHelper<Param> combineHelper = new CombineHelper<Param>(inputSet, 2);
            foreach (List<Param> oneSet in combineHelper)
            {
                List<Param> lastSet = GetLastSet(inputSet, oneSet);

                
                //OneCalculate oneCalculate = new OneCalculate();
                //oneCalculate.m_param1 = oneSet[0];
                //oneCalculate.m_param2 = oneSet[1];
                for (int i = 1; i <= 4; ++i)
                {
                    //oneCalculate.m_operator = (Operator)i;
                    Param result = new Param();
                    string operatorStr = "";

                    if (i == 1)
                    {
                        operatorStr = "+";
                        result.m_param = oneSet[0].m_param + oneSet[1].m_param;
                        
                    }
                    else if (i == 2)
                    {
                        operatorStr = "-";
                        result.m_param = oneSet[0].m_param - oneSet[1].m_param;
                    }
                    else if (i == 3)
                    {
                        operatorStr = "*";
                        result.m_param = oneSet[0].m_param * oneSet[1].m_param;
                    }
                    else if (i == 4)
                    {
                        operatorStr = "/";
                        if (Math.Abs(oneSet[1].m_param) < 0.01)
                        {
                            result.m_param = float.MaxValue;
                        }
                        else
                        {
                            result.m_param = oneSet[0].m_param / oneSet[1].m_param;
                        }
                    }

                    result.m_paramSource = "(" + oneSet[0].m_paramSource + operatorStr + oneSet[1].m_paramSource + ")";
                    //m_calculateList.Add(oneCalculate);

                    List<Param> newSet = new List<Param>();
                    newSet.Add(result);
                    newSet.AddRange(lastSet);
                    Check24(newSet);

                    //m_calculateList.RemoveAt(m_calculateList.Count - 1);
                }
            }
        }
    }

    List<Param> GetLastSet(List<Param> totalSet, List<Param> noUseSet)
    {
        List<Param> lastSet = new List<Param>(totalSet);
        foreach(var iter in noUseSet)
        {
            for (int i = 0; i < lastSet.Count; ++i )
            {
                if (lastSet[i].m_param == iter.m_param)
                {
                    lastSet.RemoveAt(i);
                    break;
                }
            }
        }
        return lastSet;
    }

//     void Check24AndGenExpression()
//     {
//         string ret = "";
//         foreach(var iter in m_calculateList)
//         {
//             
//         }
//     }
}

