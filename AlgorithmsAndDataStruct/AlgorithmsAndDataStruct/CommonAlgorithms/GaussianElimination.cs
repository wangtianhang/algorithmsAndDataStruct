using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 高斯消元法
/// </summary>
class GaussianElimination
{

    /** 
     * @列主元高斯消去法 
     */
    public double[][] m_param;
    public double[] m_d;
    public double[] m_x;
    int m_n = 0;
    int m_n2 = 0;//记录换行的次数  

    public void Elimination()
    {  //消元  
        PrintA();
        for (int k = 0; k < m_n; k++)
        {
            Wrap(k);
            for (int i = k + 1; i < m_n; i++)
            {
                double l = m_param[i][k] / m_param[k][k];
                m_param[i][k] = 0.0;
                for (int j = k + 1; j < m_n; j++)
                    m_param[i][j] = m_param[i][j] - l * m_param[k][j];
                m_d[i] = m_d[i] - l * m_d[k];
            }
            Debug.Log("第" + k + "次消元后：");
            PrintA();
        }

    }
    public void Back()//回代  
    {
        m_x[m_n - 1] = m_d[m_n - 1] / m_param[m_n - 1][m_n - 1];
        for (int i = m_n - 2; i >= 0; i--)
            m_x[i] = (m_d[i] - jisuan(i)) / m_param[i][i];
    }
    public double jisuan(int i)
    {
        double he = 0.0;
        for (int j = i; j <= m_n - 1; j++)
            he = he + m_x[j] * m_param[i][j];
        return he;
    }

    public void Wrap(int k)
    {//换行  
        double max = Math.Abs(m_param[k][k]);
        int n1 = k;                   //记住要交换的行  
        for (int i = k + 1; i < m_n; i++)     //找到要交换的行  
        {
            if (Math.Abs(m_param[i][k]) > max)
            {
                n1 = i;
                max = Math.Abs(m_param[i][k]);
            }
        }
        if (n1 != k)
        {
            m_n2++;
            Debug.Log("当k=" + k + "时,要交换的行是：" + k + "和" + n1);
            for (int j = k; j < m_n; j++)  //交换a的行  
            {
                double x1;
                x1 = m_param[k][j];
                m_param[k][j] = m_param[n1][j];
                m_param[n1][j] = x1;
            }
            double b1;   //交换b的行  
            b1 = m_d[k];
            m_d[k] = m_d[n1];
            m_d[n1] = b1;
            Debug.Log("交换后：");
            PrintA();
        }
    }

    public void Determinant()
    {//求行列式  
        double DM = 1.0;
        for (int i = 0; i < m_n; i++)
        {
            double a2 = m_param[i][i];
            DM = DM * a2;
        }
        double n3 = (double)m_n2;
        DM = DM * Math.Pow(-1.0, n3);
        Debug.Log("该方程组的系数行列式：det A = " + DM);
    }
    public void PrintA()
    {//输出增广矩阵  
        Debug.Log("增广矩阵为：");
        string outPut = "";
        for (int i = 0; i < m_n; i++)
        {
            for (int j = 0; j < m_n; j++)
                outPut += m_param[i][j] + "    ";
            outPut += m_d[i] + "    ";
            outPut += "\n";
        }
        Debug.Log(outPut);
    }
    public void Print()
    {//输出方程的根  
        Debug.Log("方程组的根为：");
        for (int i = 0; i < m_n; i++)
            Debug.Log("x" + i + " = " + m_x[i]);
    }

    public double[] Solve(int n, double[][] param, double[] d)
    {
        m_n = n;
        m_param = param;
        m_d = d;

        Elimination();
        Back();
        Print();
        Determinant();

        return m_x;
    }

}

