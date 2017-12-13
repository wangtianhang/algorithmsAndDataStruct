using System;
using System.Collections.Generic;

using System.Text;


/// <summary>
/// 坐标系与unity保持一致 为左手坐标系
/// </summary>
struct Matrix4x4
{
    public float m00;

    public float m10;

    public float m20;

    public float m30;

    public float m01;

    public float m11;

    public float m21;

    public float m31;

    public float m02;

    public float m12;

    public float m22;

    public float m32;

    public float m03;

    public float m13;

    public float m23;

    public float m33;

    public float this[int row, int column]
    {
        get
        {
            return this[row + column * 4];
        }
        set
        {
            this[row + column * 4] = value;
        }
    }

    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return this.m00;
                case 1:
                    return this.m10;
                case 2:
                    return this.m20;
                case 3:
                    return this.m30;
                case 4:
                    return this.m01;
                case 5:
                    return this.m11;
                case 6:
                    return this.m21;
                case 7:
                    return this.m31;
                case 8:
                    return this.m02;
                case 9:
                    return this.m12;
                case 10:
                    return this.m22;
                case 11:
                    return this.m32;
                case 12:
                    return this.m03;
                case 13:
                    return this.m13;
                case 14:
                    return this.m23;
                case 15:
                    return this.m33;
                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    this.m00 = value;
                    break;
                case 1:
                    this.m10 = value;
                    break;
                case 2:
                    this.m20 = value;
                    break;
                case 3:
                    this.m30 = value;
                    break;
                case 4:
                    this.m01 = value;
                    break;
                case 5:
                    this.m11 = value;
                    break;
                case 6:
                    this.m21 = value;
                    break;
                case 7:
                    this.m31 = value;
                    break;
                case 8:
                    this.m02 = value;
                    break;
                case 9:
                    this.m12 = value;
                    break;
                case 10:
                    this.m22 = value;
                    break;
                case 11:
                    this.m32 = value;
                    break;
                case 12:
                    this.m03 = value;
                    break;
                case 13:
                    this.m13 = value;
                    break;
                case 14:
                    this.m23 = value;
                    break;
                case 15:
                    this.m33 = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid matrix index!");
            }
        }
    }
}

