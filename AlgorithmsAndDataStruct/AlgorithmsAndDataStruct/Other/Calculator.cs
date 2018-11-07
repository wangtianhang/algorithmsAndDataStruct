using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// copy form geeksforgeeks 这版本居然连运算符优先级都不考虑 也是醉了
/// </summary>
public class Calculator
{
    public static void Test()
    {
        string exp = "1+2*5+3";
        Debug.Log(exp + " " + evaluate(exp));
    }

    // A utility function to check if a given character is operand 
    static bool isOperand(char c) { return (c >= '0' && c <= '9'); } 

    // utility function to find value of and operand 
    static int value(char c) { return (c - '0'); } 

    // This function evaluates simple expressions. It returns -1 if the 
    // given expression is invalid. 
    static int evaluate(string exp)
    {
        // Base Case: Given expression is empty 
        //if (*exp == '\0') return -1;
        if(string.IsNullOrEmpty(exp))
        {
            return -1;
        }

        // The first character must be an operand, find its value 
        int res = value(exp[0]);

        // Traverse the remaining characters in pairs 
        for (int i = 1; i < exp.Length; i += 2)
        {
            // The next character must be an operator, and 
            // next to next an operand 
            char opr = exp[i], opd = exp[i + 1];

            // If next to next character is not an operand 
            if (!isOperand(opd)) return -1;

            // Update result according to the operator 
            if (opr == '+') res += value(opd);
            else if (opr == '-') res -= value(opd);
            else if (opr == '*') res *= value(opd);
            else if (opr == '/') res /= value(opd);

            // If not a valid operator 
            else return -1;
        }
        return res;
    } 
}

