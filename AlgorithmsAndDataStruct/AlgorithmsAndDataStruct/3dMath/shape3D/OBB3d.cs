using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct OBB3d
{
    public Vector3L m_pos;
    public QuaternionL m_rotation;
    //public FloatL m_xLength;
    //public FloatL m_yLength;
    //public FloatL m_zLength;
    //public Vector3L m_size = Vector3L.one;
    public Vector3L m_size;

    public OBB3d(Vector3L pos, QuaternionL qua, FloatL xLength, FloatL yLength, FloatL zLength)
    {
        m_pos = pos;
        m_rotation = qua;
        //m_xLength = xLength;
        //m_yLength = yLength;
        //m_zLength = zLength;
        m_size.x = xLength;
        m_size.y = yLength;
        m_size.z = zLength;
    }

    public OBB3d(Vector3L pos, QuaternionL qua, Vector3L size)
    {
        m_pos = pos;
        m_rotation = qua;

        //m_xLength = size.x;
        //m_yLength = size.y;
        //m_zLength = size.z;
        m_size = size;
    }

//     public Vector3L GetAABBMin()
//     {
//         return new Vector3L(m_pos.x - m_xLength * 0.5f, m_pos.y - m_yLength * 0.5f, m_pos.z - m_zLength * 0.5f);
//     }
// 
//     public Vector3L GetAABBMax()
//     {
//         return new Vector3L(m_pos.x + m_xLength * 0.5f, m_pos.y + m_yLength * 0.5f, m_pos.z + m_zLength * 0.5f);
//     }

    public Matrix4x4L GetObjToWorld()
    {
        return Matrix4x4L.TRS(m_pos, m_rotation, Vector3L.one);
    }

    public FloatL[] GetOrientationMatrixAsArray()
    {
        Matrix4x4L trs = Matrix4x4L.TRS(Vector3L.zero, m_rotation, Vector3L.one);
        FloatL[] matrixArray = new FloatL[9];

        matrixArray[0] = trs.m00;
        matrixArray[1] = trs.m01;
        matrixArray[2] = trs.m02;

        matrixArray[3] = trs.m10;
        matrixArray[4] = trs.m11;
        matrixArray[5] = trs.m12;

        matrixArray[6] = trs.m20;
        matrixArray[7] = trs.m21;
        matrixArray[8] = trs.m22;

        return matrixArray;
    }

    public Vector3L GetHalfSize()
    {
        return m_size / 2;
    }

    public FloatL[] GetHalfSizeAsArray()
    {
        FloatL[] ret = new FloatL[3];
        ret[0] = m_size.x / 2;
        ret[1] = m_size.y / 2;
        ret[2] = m_size.z / 2;
        return ret;
    }
}

