﻿using System;
using System.Collections.Generic;
using System.Text;


class Debug
{
    public static void Log(string str)
    {
        Console.WriteLine(str);
    }

    public static void LogWarning(string str)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(str);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void LogError(string str)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(str);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

