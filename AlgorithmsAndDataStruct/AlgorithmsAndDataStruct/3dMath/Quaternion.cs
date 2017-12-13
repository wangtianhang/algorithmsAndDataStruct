using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


struct Quaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        return new Quaternion();
    }
}

