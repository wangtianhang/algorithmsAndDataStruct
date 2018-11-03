using System;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class IntMath
{
	// Token: 0x06000FC9 RID: 4041 RVA: 0x0005693C File Offset: 0x00054B3C
	public static VFactor atan2(int y, int x)
	{
		int num;
		int num2;
		if (x < 0)
		{
			if (y < 0)
			{
				x = -x;
				y = -y;
				num = 1;
			}
			else
			{
				x = -x;
				num = -1;
			}
			num2 = -31416;
		}
		else
		{
			if (y < 0)
			{
				y = -y;
				num = -1;
			}
			else
			{
				num = 1;
			}
			num2 = 0;
		}
		int dim = Atan2LookupTable.DIM;
		long num3 = (long)(dim - 1);
		long b = (long)((x >= y) ? x : y);
		int num4 = (int)IntMath.Divide((long)x * num3, b);
		int num5 = (int)IntMath.Divide((long)y * num3, b);
		int num6 = Atan2LookupTable.table[num5 * dim + num4];
		return new VFactor
		{
			nom = (long)((num6 + num2) * num),
			den = 10000L
		};
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x000569FC File Offset: 0x00054BFC
	public static VFactor acos(long nom, long den)
	{
		int num = (int)IntMath.Divide(nom * (long)AcosLookupTable.HALF_COUNT, den) + AcosLookupTable.HALF_COUNT;
		num = Mathf.Clamp(num, 0, AcosLookupTable.COUNT);
		return new VFactor
		{
			nom = (long)AcosLookupTable.table[num],
			den = 10000L
		};
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00056A54 File Offset: 0x00054C54
	public static VFactor sin(long nom, long den)
	{
		int index = SinCosLookupTable.getIndex(nom, den);
		return new VFactor((long)SinCosLookupTable.sin_table[index], (long)SinCosLookupTable.FACTOR);
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x00056A7C File Offset: 0x00054C7C
	public static VFactor cos(long nom, long den)
	{
		int index = SinCosLookupTable.getIndex(nom, den);
		return new VFactor((long)SinCosLookupTable.cos_table[index], (long)SinCosLookupTable.FACTOR);
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00056AA4 File Offset: 0x00054CA4
	public static void sincos(out VFactor s, out VFactor c, long nom, long den)
	{
		int index = SinCosLookupTable.getIndex(nom, den);
		s = new VFactor((long)SinCosLookupTable.sin_table[index], (long)SinCosLookupTable.FACTOR);
		c = new VFactor((long)SinCosLookupTable.cos_table[index], (long)SinCosLookupTable.FACTOR);
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00056AE4 File Offset: 0x00054CE4
	public static void sincos(out VFactor s, out VFactor c, VFactor angle)
	{
		int index = SinCosLookupTable.getIndex(angle.nom, angle.den);
		s = new VFactor((long)SinCosLookupTable.sin_table[index], (long)SinCosLookupTable.FACTOR);
		c = new VFactor((long)SinCosLookupTable.cos_table[index], (long)SinCosLookupTable.FACTOR);
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00056B30 File Offset: 0x00054D30
	public static long Divide(long a, long b)
	{
		long num = (long)((ulong)((a ^ b) & long.MinValue) >> 63);
		long num2 = num * -2L + 1L;
		return (a + b / 2L * num2) / b;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00056B64 File Offset: 0x00054D64
	public static int Divide(int a, int b)
	{
		int num = (int)((uint)((a ^ b) & int.MinValue) >> 31);
		int num2 = num * -2 + 1;
		return (a + b / 2 * num2) / b;
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00056B90 File Offset: 0x00054D90
	public static VInt3 Divide(VInt3 a, long m, long b)
	{
		a.x = (int)IntMath.Divide((long)a.x * m, b);
		a.y = (int)IntMath.Divide((long)a.y * m, b);
		a.z = (int)IntMath.Divide((long)a.z * m, b);
		return a;
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00056BE8 File Offset: 0x00054DE8
	public static VInt2 Divide(VInt2 a, long m, long b)
	{
		a.x = (int)IntMath.Divide((long)a.x * m, b);
		a.y = (int)IntMath.Divide((long)a.y * m, b);
		return a;
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00056C1C File Offset: 0x00054E1C
	public static VInt3 Divide(VInt3 a, int b)
	{
		a.x = IntMath.Divide(a.x, b);
		a.y = IntMath.Divide(a.y, b);
		a.z = IntMath.Divide(a.z, b);
		return a;
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x00056C5C File Offset: 0x00054E5C
	public static VInt3 Divide(VInt3 a, long b)
	{
		a.x = (int)IntMath.Divide((long)a.x, b);
		a.y = (int)IntMath.Divide((long)a.y, b);
		a.z = (int)IntMath.Divide((long)a.z, b);
		return a;
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x00056CAC File Offset: 0x00054EAC
	public static VInt2 Divide(VInt2 a, long b)
	{
		a.x = (int)IntMath.Divide((long)a.x, b);
		a.y = (int)IntMath.Divide((long)a.y, b);
		return a;
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x00056CDC File Offset: 0x00054EDC
	public static uint Sqrt32(uint a)
	{
		uint num = 0u;
		uint num2 = 0u;
		for (int i = 0; i < 16; i++)
		{
			num2 <<= 1;
			num <<= 2;
			num += a >> 30;
			a <<= 2;
			if (num2 < num)
			{
				num2 += 1u;
				num -= num2;
				num2 += 1u;
			}
		}
		return num2 >> 1 & 65535u;
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00056D30 File Offset: 0x00054F30
	public static ulong Sqrt64(ulong a)
	{
		ulong num = 0UL;
		ulong num2 = 0UL;
		for (int i = 0; i < 32; i++)
		{
			num2 <<= 1;
			num <<= 2;
			num += a >> 62;
			a <<= 2;
			if (num2 < num)
			{
				num2 += 1UL;
				num -= num2;
				num2 += 1UL;
			}
		}
		//return num2 >> 1 & (ulong)-1;
        return num2 >> 1 & ulong.MaxValue;
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00056D88 File Offset: 0x00054F88
	public static long SqrtLong(long a)
	{
		if (a <= 0L)
		{
			return 0L;
		}
		//if (a <= (long)((ulong)-1))
        if (a <= long.MinValue)
		{
			return (long)((ulong)IntMath.Sqrt32((uint)a));
		}
		return (long)IntMath.Sqrt64((ulong)a);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x00056DB8 File Offset: 0x00054FB8
	public static int Sqrt(long a)
	{
		if (a <= 0L)
		{
			return 0;
		}
		//if (a <= (long)((ulong)-1))
        if(a <= long.MinValue)
		{
			return (int)IntMath.Sqrt32((uint)a);
		}
		return (int)IntMath.Sqrt64((ulong)a);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00056DDC File Offset: 0x00054FDC
	public static long Clamp(long a, long min, long max)
	{
		if (a < min)
		{
			return min;
		}
		if (a > max)
		{
			return max;
		}
		return a;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x00056DF4 File Offset: 0x00054FF4
	public static long Max(long a, long b)
	{
		return (a <= b) ? b : a;
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00056E04 File Offset: 0x00055004
	public static VInt3 Transform(ref VInt3 point, ref VInt3 axis_x, ref VInt3 axis_y, ref VInt3 axis_z, ref VInt3 trans)
	{
		return new VInt3(IntMath.Divide(axis_x.x * point.x + axis_y.x * point.y + axis_z.x * point.z, 1000) + trans.x, IntMath.Divide(axis_x.y * point.x + axis_y.y * point.y + axis_z.y * point.z, 1000) + trans.y, IntMath.Divide(axis_x.z * point.x + axis_y.z * point.y + axis_z.z * point.z, 1000) + trans.z);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00056EC8 File Offset: 0x000550C8
	public static VInt3 Transform(VInt3 point, ref VInt3 axis_x, ref VInt3 axis_y, ref VInt3 axis_z, ref VInt3 trans)
	{
		return new VInt3(IntMath.Divide(axis_x.x * point.x + axis_y.x * point.y + axis_z.x * point.z, 1000) + trans.x, IntMath.Divide(axis_x.y * point.x + axis_y.y * point.y + axis_z.y * point.z, 1000) + trans.y, IntMath.Divide(axis_x.z * point.x + axis_y.z * point.y + axis_z.z * point.z, 1000) + trans.z);
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00056F94 File Offset: 0x00055194
	public static VInt3 Transform(ref VInt3 point, ref VInt3 axis_x, ref VInt3 axis_y, ref VInt3 axis_z, ref VInt3 trans, ref VInt3 scale)
	{
		long num = (long)point.x * (long)scale.x;
		long num2 = (long)point.y * (long)scale.x;
		long num3 = (long)point.z * (long)scale.x;
		return new VInt3((int)IntMath.Divide((long)axis_x.x * num + (long)axis_y.x * num2 + (long)axis_z.x * num3, 1000000L) + trans.x, (int)IntMath.Divide((long)axis_x.y * num + (long)axis_y.y * num2 + (long)axis_z.y * num3, 1000000L) + trans.y, (int)IntMath.Divide((long)axis_x.z * num + (long)axis_y.z * num2 + (long)axis_z.z * num3, 1000000L) + trans.z);
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0005706C File Offset: 0x0005526C
	public static VInt3 Transform(ref VInt3 point, ref VInt3 forward, ref VInt3 trans)
	{
		VInt3 up = VInt3.up;
		VInt3 vint = VInt3.Cross(VInt3.up, forward);
		return IntMath.Transform(ref point, ref vint, ref up, ref forward, ref trans);
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0005709C File Offset: 0x0005529C
	public static VInt3 Transform(VInt3 point, VInt3 forward, VInt3 trans)
	{
		VInt3 up = VInt3.up;
		VInt3 vint = VInt3.Cross(VInt3.up, forward);
		return IntMath.Transform(ref point, ref vint, ref up, ref forward, ref trans);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000570CC File Offset: 0x000552CC
	public static VInt3 Transform(VInt3 point, VInt3 forward, VInt3 trans, VInt3 scale)
	{
		VInt3 up = VInt3.up;
		VInt3 vint = VInt3.Cross(VInt3.up, forward);
		return IntMath.Transform(ref point, ref vint, ref up, ref forward, ref trans, ref scale);
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x000570FC File Offset: 0x000552FC
	public static int Lerp(int src, int dest, int nom, int den)
	{
		return IntMath.Divide(src * den + (dest - src) * nom, den);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00057110 File Offset: 0x00055310
	public static long Lerp(long src, long dest, long nom, long den)
	{
		return IntMath.Divide(src * den + (dest - src) * nom, den);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00057124 File Offset: 0x00055324
	public static bool IsPowerOfTwo(int x)
	{
		return (x & x - 1) == 0;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00057130 File Offset: 0x00055330
	public static int CeilPowerOfTwo(int x)
	{
		x--;
		x |= x >> 1;
		x |= x >> 2;
		x |= x >> 4;
		x |= x >> 8;
		x |= x >> 16;
		x++;
		return x;
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00057164 File Offset: 0x00055364
	public static void SegvecToLinegen(ref VInt2 segSrc, ref VInt2 segVec, out long a, out long b, out long c)
	{
		a = (long)segVec.y;
		b = (long)(-(long)segVec.x);
		c = (long)segVec.x * (long)segSrc.y - (long)segSrc.x * (long)segVec.y;
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0005719C File Offset: 0x0005539C
	private static bool IsPointOnSegment(ref VInt2 segSrc, ref VInt2 segVec, long x, long y)
	{
		long num = x - (long)segSrc.x;
		long num2 = y - (long)segSrc.y;
		return (long)segVec.x * num + (long)segVec.y * num2 >= 0L && num * num + num2 * num2 <= segVec.sqrMagnitudeLong;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x000571EC File Offset: 0x000553EC
	public static bool IntersectSegment(ref VInt2 seg1Src, ref VInt2 seg1Vec, ref VInt2 seg2Src, ref VInt2 seg2Vec, out VInt2 interPoint)
	{
		long num;
		long num2;
		long num3;
		IntMath.SegvecToLinegen(ref seg1Src, ref seg1Vec, out num, out num2, out num3);
		long num4;
		long num5;
		long num6;
		IntMath.SegvecToLinegen(ref seg2Src, ref seg2Vec, out num4, out num5, out num6);
		long num7 = num * num5 - num4 * num2;
		if (num7 != 0L)
		{
			long num8 = IntMath.Divide(num2 * num6 - num5 * num3, num7);
			long num9 = IntMath.Divide(num4 * num3 - num * num6, num7);
			bool result = IntMath.IsPointOnSegment(ref seg1Src, ref seg1Vec, num8, num9) && IntMath.IsPointOnSegment(ref seg2Src, ref seg2Vec, num8, num9);
			interPoint.x = (int)num8;
			interPoint.y = (int)num9;
			return result;
		}
		interPoint = VInt2.zero;
		return false;
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0005728C File Offset: 0x0005548C
	public static bool PointInPolygon(ref VInt2 pnt, VInt2[] plg)
	{
		if (plg == null || plg.Length < 3)
		{
			return false;
		}
		bool flag = false;
		int i = 0;
		int num = plg.Length - 1;
		while (i < plg.Length)
		{
			VInt2 vint = plg[i];
			VInt2 vint2 = plg[num];
			if ((vint.y <= pnt.y && pnt.y < vint2.y) || (vint2.y <= pnt.y && pnt.y < vint.y))
			{
				int num2 = vint2.y - vint.y;
				long num3 = (long)(pnt.y - vint.y) * (long)(vint2.x - vint.x) - (long)(pnt.x - vint.x) * (long)num2;
				if (num2 > 0)
				{
					if (num3 > 0L)
					{
						flag = !flag;
					}
				}
				else if (num3 < 0L)
				{
					flag = !flag;
				}
			}
			num = i++;
		}
		return flag;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0005739C File Offset: 0x0005559C
	public static bool SegIntersectPlg(ref VInt2 segSrc, ref VInt2 segVec, VInt2[] plg, out VInt2 nearPoint, out VInt2 projectVec)
	{
		nearPoint = VInt2.zero;
		projectVec = VInt2.zero;
		if (plg == null || plg.Length < 2)
		{
			return false;
		}
		bool result = false;
		long num = -1L;
		int num2 = -1;
		for (int i = 0; i < plg.Length; i++)
		{
			VInt2 vint = plg[(i + 1) % plg.Length] - plg[i];
			VInt2 vint2;
			if (IntMath.IntersectSegment(ref segSrc, ref segVec, ref plg[i], ref vint, out vint2))
			{
				long sqrMagnitudeLong = (vint2 - segSrc).sqrMagnitudeLong;
				if (num < 0L || sqrMagnitudeLong < num)
				{
					nearPoint = vint2;
					num = sqrMagnitudeLong;
					num2 = i;
					result = true;
				}
			}
		}
		if (num2 >= 0)
		{
			VInt2 lhs = plg[(num2 + 1) % plg.Length] - plg[num2];
			VInt2 vint3 = segSrc + segVec - nearPoint;
			long num3 = (long)vint3.x * (long)lhs.x + (long)vint3.y * (long)lhs.y;
			if (num3 < 0L)
			{
				num3 = -num3;
				lhs = -lhs;
			}
			long sqrMagnitudeLong2 = lhs.sqrMagnitudeLong;
			projectVec.x = (int)IntMath.Divide((long)lhs.x * num3, sqrMagnitudeLong2);
			projectVec.y = (int)IntMath.Divide((long)lhs.y * num3, sqrMagnitudeLong2);
		}
		return result;
	}
}
