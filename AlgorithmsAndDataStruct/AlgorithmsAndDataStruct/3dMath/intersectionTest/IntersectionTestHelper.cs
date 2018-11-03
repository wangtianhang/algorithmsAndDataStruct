using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class IntersectionTestHelper
{
    public static bool IsIntersction3d(HitShape3dData hitData, Segment3d segment)
    {
        if (hitData.m_type == HitShape3dType.Sphere)
        {
            //Sphere3d hitSphere = new Sphere3d(hitData.m_pos, hitData.m_radius);
            return IntersectionTest3D.Segment3dWithSphere3d(segment, hitData.m_sphere);
        }
        else if (hitData.m_type == HitShape3dType.OBB)
        {
            //OBB3d obb = new OBB3d(hitData.m_pos, hitData.m_rotation, hitData.m_size);
            return IntersectionTest3D.Segment3dWithOBB3d(segment, hitData.m_obb);
        }
        else
        {
            return false;
        }
    }

    public static bool IsIntersction3d(HitShape3dData hitData, Capsule3d capsule)
    {
        if (hitData.m_type == HitShape3dType.Sphere)
        {
            //Sphere3d hitSphere = new Sphere3d(hitData.m_pos, hitData.m_radius);
            return IntersectionTest3D.Capsule3dWithSphere3d(capsule, hitData.m_sphere);
        }
        else if (hitData.m_type == HitShape3dType.OBB)
        {
            //OBB3d obb = new OBB3d(hitData.m_pos, hitData.m_rotation, hitData.m_size);
            return IntersectionTest3D.Capsule3dWithOBB3d(capsule, hitData.m_obb);
        }
        else
        {
            return false;
        }
    }

    public static bool IsIntersction3d(HitShape3dData hitData, OBB3d obb)
    {
        if (hitData.m_type == HitShape3dType.Sphere)
        {
            //Sphere3d hitSphere = new Sphere3d(hitData.m_pos, hitData.m_radius);
            return IntersectionTest3D.Sphere3dWithObb3d(hitData.m_sphere, obb);
        }
        else if (hitData.m_type == HitShape3dType.OBB)
        {
            //OBB3d obb2 = new OBB3d(hitData.m_pos, hitData.m_rotation, hitData.m_size);
            return IntersectionTest3D.OBB3dWithOBB3d(obb, hitData.m_obb);
        }
        else
        {
            return false;
        }
    }

    public static bool IsIntersction3d(HitShape3dData hitData1, HitShape3dData hitData2)
    {
        if (hitData1.m_type == HitShape3dType.Sphere && hitData2.m_type == HitShape3dType.Sphere)
        {
            return IntersectionTest3D.Sphere3dWithSphere3d(hitData1.m_sphere, hitData2.m_sphere);
        }
        else if(hitData1.m_type == HitShape3dType.Sphere && hitData2.m_type == HitShape3dType.OBB)
        {
            return IntersectionTest3D.Sphere3dWithObb3d(hitData1.m_sphere, hitData2.m_obb);
        }
        else if(hitData1.m_type == HitShape3dType.OBB && hitData2.m_type == HitShape3dType.Sphere)
        {
            return IntersectionTest3D.Sphere3dWithObb3d(hitData2.m_sphere, hitData1.m_obb);
        }
        else if(hitData1.m_type == HitShape3dType.OBB && hitData2.m_type == HitShape3dType.OBB)
        {
            return IntersectionTest3D.OBB3dWithOBB3d(hitData2.m_obb, hitData1.m_obb);
        }
        else
        {
            return false;
        }
    }

//     public static bool IsIntersection3d(SurroundHitData hitData, Segment3d segment)
//     {
//         if (hitData.m_type == HitShape3dType.Sphere)
//         {
//             Sphere3d hitSphere = new Sphere3d(hitData.m_pos, hitData.m_radius);
//             return IntersectionTest3D.Segment3dWithSphere3d(segment, hitSphere);
//         }
//         else if (hitData.m_type == HitShape3dType.Box)
//         {
//             OBB3d obb = new OBB3d(hitData.m_pos, hitData.m_rotation, hitData.m_size);
//             return IntersectionTest3D.Segment3dWithOBB3d(segment, obb);
//         }
//         else
//         {
//             return false;
//         }
//     }
}

