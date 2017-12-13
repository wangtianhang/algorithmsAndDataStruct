using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Math3d
{
    public const float Deg2Rad = 0.0174533f;
    public const float Rad2Deg = 57.2958f;

    public static Vector3 GetIntersectionPoint(Vector3 rayOrigin, Vector3 rayDir, Vector3 planeNormal, Vector3 planeOnePoint)
    {
        float t = (Vector3.Dot(planeNormal, planeOnePoint) - Vector3.Dot(planeNormal, rayOrigin))
            / (Vector3.Dot(planeNormal, rayDir));
        Vector3 intersectionPoint = rayOrigin + rayDir * t;
        return intersectionPoint;
    }

    /// <summary>
    /// 仅支持凸多边形
    /// </summary>
    /// <param name="p"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool IsInConvexPolygon(Vector3 p, List<Vector3> polygon)
    {
        if (polygon.Count < 3)
        {
            return false;
        }

        p.y = 0;
        for (int i = 0; i < polygon.Count; ++i)
        {
            Vector3 tmp = polygon[i];
            tmp.y = 0;
            polygon[i] = tmp;
        }

        List<Vector3> crossResultList = new List<Vector3>();
        for (int i = 0; i < polygon.Count; ++i)
        {
            if (i == polygon.Count - 1)
            {
                Vector3 v1 = p - polygon[i];
                Vector3 v2 = polygon[0] - polygon[i];
                Vector3 v3 = Vector3.Cross(v1, v2);
                crossResultList.Add(v3);
            }
            else
            {
                Vector3 v1 = p - polygon[i];
                Vector3 v2 = polygon[i + 1] - polygon[i];
                Vector3 v3 = Vector3.Cross(v1, v2);
                crossResultList.Add(v3);
            }
        }

        for (int i = 0; i < crossResultList.Count; ++i)
        {
            if (i == crossResultList.Count - 1)
            {
                if (crossResultList[i].y * crossResultList[0].y < 0)
                {
                    return false;
                }
            }
            else
            {
                if (crossResultList[i].y * crossResultList[i + 1].y < 0)
                {
                    return false;
                }
            }
        }

        return true;
    }
}

