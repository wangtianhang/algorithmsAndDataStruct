using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


public class Debug
{
    static bool s_hasInit = false;
    public static void Init()
    {
        if (!s_hasInit)
        {
            s_hasInit = true;
            //Console.SetWindowSize(1280, 720);
        }
    }

    public static void Log(string str)
    {
        Init();

        Console.WriteLine(str);
    }

    public static void LogWarning(string str)
    {
        Init();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(str);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogError(string str)
    {
        Init();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogException(System.Exception ex)
    {
        Init();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(ex.Message + "\n" + GetStackFrame());
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static string GetStackFrame()
    {
        string info = "";
        StackTrace st = new StackTrace(true);
        //得到当前的所以堆栈  
        StackFrame[] sf = st.GetFrames();
        for (int i = 0; i < sf.Length; ++i)
        {
            info += sf[i].ToString();
        }
        return info;
    }
}



