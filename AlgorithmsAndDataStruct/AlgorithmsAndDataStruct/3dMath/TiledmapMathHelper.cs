using System;
using System.Collections.Generic;
using System.Text;


class TiledmapMathHelper
{
    /// <summary>
    /// 交错转钻石
    /// </summary>
    /// <returns></returns>
    public static Vector2ixy ConvertStaggeredToDiamond(Vector2ixy v2)
    {
        int x2 = v2.m_x;
        int y2 = v2.m_y;
        int x1 = 0;
        int y1 = 0;
        if (y2 % 2 == 1)
        {
            x1 = x2 + y2 / 2 + 1;
            y1 = x1 - y2;
        }
        else
        {
            x1 = x2 + y2 / 2;
            y1 = x1 - y2;
        }
        return new Vector2ixy(x1, y1);
    }

    /// <summary>
    /// 钻石到交错
    /// </summary>
    /// <returns></returns>
    public static Vector2ixy ConvertDiamondToStaggered(Vector2ixy v1)
    {
        int x1 = v1.m_x;
        int y1 = v1.m_y;
        int x2;
        int y2;
        y2 = x1 - y1;
        if (y2 % 2 == 1)
        {
            x2 = x1 - 1 - y2 / 2;
        }
        else
        {
            x2 = x1 - y2 / 2;
        }
        return new Vector2ixy(x2, y2);
    }
}

