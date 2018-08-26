using System;
using System.Collections.Generic;
using System.Text;


public class OBB3d
{
    public Vector3 m_pos;
    public Quaternion m_rotation = Quaternion.identity;
    public float m_xLength;
    public float m_yLength;
    public float m_zLength;
    //public Vector3 m_size = Vector3.one;

    public OBB3d(Vector3 pos, Quaternion qua, float xLength, float yLength, float zLength)
    {
        m_pos = pos;
        m_rotation = qua;
        m_xLength = xLength;
        m_yLength = yLength;
        m_zLength = zLength;
    }

    public Vector3 GetAABBMin()
    {
        return new Vector3(m_pos.x - m_xLength * 0.5f, m_pos.y - m_yLength * 0.5f, m_pos.z - m_zLength * 0.5f);
    }

    public Vector3 GetAABBMax()
    {
        return new Vector3(m_pos.x + m_xLength * 0.5f, m_pos.y + m_yLength * 0.5f, m_pos.z + m_zLength * 0.5f);
    }

    public Matrix4x4 GetObjToWorld()
    {
        return Matrix4x4.TRS(m_pos, m_rotation, Vector3.one);
    }

    public float[] GetOrientationMatrixAsArray()
    {
        Matrix4x4 trs = Matrix4x4.TRS(Vector3.zero, m_rotation, Vector3.one);
        float[] matrixArray = new float[9];

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

    public Vector3 GetHalfSize()
    {
        return new Vector3(m_xLength / 2, m_yLength / 2, m_zLength / 2);
    }

    public float[] GetHalfSizeAsArray()
    {
        float[] ret = new float[3];
        ret[0] = m_xLength / 2;
        ret[1] = m_yLength / 2;
        ret[2] = m_zLength / 2;
        return ret;
    }
}

