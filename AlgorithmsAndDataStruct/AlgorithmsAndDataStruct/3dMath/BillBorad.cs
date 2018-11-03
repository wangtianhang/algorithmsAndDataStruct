using System;
using System.Collections.Generic;

using System.Text;
using UnityEngine;


class BillBorad
{
    bool hasInit = false;
    Quaternion m_selfOriginRotation = new Quaternion();

    Quaternion m_curRotation = new Quaternion();
    void Update()
    {
        if (!hasInit)
        {
            hasInit = true;
            CacheSelfRotation();
        }

        m_curRotation = GetCameraRotation() * m_selfOriginRotation;
    }

    Quaternion GetCameraRotation()
    {
        return new Quaternion();
    }

    void CacheSelfRotation()
    {
        // todo 设置m_selfOriginRotation
    }
}

