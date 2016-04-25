/********************************************************************
	created:	2014/09/18
	created:	18:9:2014   16:02
	author:		wangtianhang(690879430@qq.com)
	
	purpose:	从itween中拆出来的cr曲线
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// copy from itween(Catmull-Rom class)
public class CRSpline
{
    public Vector3[] pts;

    /// <summary>
    /// 最少需要四个点
    /// </summary>
    /// <param name="pts"></param>
    public CRSpline(params Vector3[] pts)
    {
        this.pts = new Vector3[pts.Length];
        Array.Copy(pts, this.pts, pts.Length);
    }


    public Vector3 Interp(float t)
    {
        int numSections = pts.Length - 3;
        int currPt = Math.Min((int)Math.Floor(t * (float)numSections), numSections - 1);
        float u = t * (float)numSections - (float)currPt;
        Vector3 a = pts[currPt];
        Vector3 b = pts[currPt + 1];
        Vector3 c = pts[currPt + 2];
        Vector3 d = pts[currPt + 3];
        return .5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }
}

