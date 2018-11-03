using System;
using System.Collections.Generic;
using System.Text;


public class DebugHelper
{
    public static void Assert(bool value, string str = null)
    {
        if(value)
        {
            //throw new System.Exception();
            UnityEngine.Debug.LogError("Assert " + str);
        }
    }
}

