using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

public class FunctionDraw
{
    public FunctionDraw(FunctionOfOneVariableD func, double step, double beginX, double endX)
    {
        m_func = func;
        m_step = step;
        m_beginX = beginX;
        m_endX = endX;
    }
    public FunctionOfOneVariableD m_func = null;
    public double m_step = 0.001d;
    public double m_beginX = 0;
    public double m_endX = 0;
}

public class MathHelper
{
    public static void Test()
    {
        string tmp = TenToTwo(10);
        Debug.Log(tmp);
        Debug.Log(TwoToTen(tmp).ToString());

        string tmp2 = TenToSixteen(10);
        Debug.Log(tmp2);
        Debug.Log(SixteenToTen(tmp2).ToString());

        //Bitmap bitmap = MathCommon.DrawFunction( (x) =>  1 / Math.Sqrt(4 - x * x)  , 0.001d, -2, 2,
        //    -3, 3, -1, +10, "y = 1 divide sqrt(4 - power(x, 2)");
        //bitmap.Save("y = 1 divide sqrt(4 - power(x, 2)" + ".bmp");

        List<FunctionDraw> drawList = new List<FunctionDraw>();
        //drawList.Add(new FunctionDraw((x) =>  1 / Math.Sqrt(4 - x * x)  , 0.001d, -2, 2));
        //drawList.Add(new FunctionDraw((x) => x * x, 0.001d, -3, 3));
        drawList.Add(new FunctionDraw((x) => (4 * x / (x * x + 1)), 0.001d, -3, 3));
        Bitmap bitmap = DrawFunction(drawList, -3, 3, -1, +10, "MathHelper Test");
        bitmap.Save("MathHelper Test" + ".bmp");
    }

    public static Bitmap DrawFunction(List<FunctionDraw> drawList,
        double xMin, double xMax, double yMin, double yMax, string des = "")
    {
        int width = 1136;
        int height = 640;
        Bitmap bitmap = new Bitmap(width, height);

        //================画边框==========================
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < 2; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = height - 2; j < height; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < height; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }
        for (int i = width - 2; i < width; i++)
        {
            for (int j = 0; j < height; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }
        //=================画边框结束=============================

        //===============画数轴=========================
        {
            int yAxis = (int)((0 - xMin) / (xMax - xMin) * width);
            int xAxis = (int)((0 - yMin) / (yMax - yMin) * height);
            for (int i = 0; i < width; i++)
            {
                if (i >= 0 && i < width
                    && xAxis >= 0 && xAxis < height)
                {
                    bitmap.SetPixel(i, height - xAxis - 1, Color.Black);
                }
            }
            for (int j = 0; j < height; ++j)
            {
                if (yAxis >= 0 && yAxis < width
                    && j >= 0 && j < height)
                {
                    bitmap.SetPixel(yAxis, height - j - 1, Color.Black);
                }
            }
        }
        //===============画数轴结束=======================

        foreach (var iter in drawList)
        {
            int num = (int)((iter.m_endX - iter.m_beginX) / iter.m_step);
            for (int i = 0; i < num; ++i)
            {
                double curX = iter.m_beginX + iter.m_step * i;
                double curY = iter.m_func(curX);

                int x = (int)((curX - xMin) / (xMax - xMin) * width);
                int y = (int)((curY - yMin) / (yMax - yMin) * height);

                if (x >= 0 && x < width
                    && y >= 0 && y < height)
                {
                    bitmap.SetPixel(x, height - y - 1, Color.Black);
                }

            }
        }


        using (Graphics g = Graphics.FromImage(bitmap))
        {
            using (Font font = new Font("宋体", 16f))
            {
                //string drawText = des;
                //g.TextContrast = 0;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                g.DrawString(des, font, Brushes.Black, new Point(20, 20));
                string drawText2 = "xMin " + xMin + " xMax " + xMax + " yMin " + yMin + " yMax " + yMax;
                g.DrawString(drawText2, font, Brushes.Black, new Point(20, 40));
            }
        }

        return bitmap;
    }

    /// <summary>
    /// 十进制转二进制
    /// </summary>
    /// <returns></returns>
    public static string TenToTwo(int x)
    {
        return System.Convert.ToString(x, 2);
    }

    /// <summary>
    /// 二进制转十进制
    /// </summary>
    /// <returns></returns>
    public static int TwoToTen(string s)
    {
        return System.Convert.ToInt32(s, 2);
    }

    /// <summary>
    /// 十进制转十六进制
    /// </summary>
    /// <returns></returns>
    public static string TenToSixteen(int x)
    {
        return string.Format("{0:X}", x);
    }

    /// <summary>
    /// 十六进制转十进制
    /// </summary>
    /// <returns></returns>
    public static int SixteenToTen(string s)
    {
        return System.Convert.ToInt32(s, 16);
    }
}

