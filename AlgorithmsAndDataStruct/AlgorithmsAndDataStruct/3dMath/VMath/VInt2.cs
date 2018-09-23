using System;
//using UnityEngine;

// Token: 0x02000213 RID: 531
[Serializable]
public struct VInt2
{
	// Token: 0x060010AF RID: 4271 RVA: 0x000597F4 File Offset: 0x000579F4
	public VInt2(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00059838 File Offset: 0x00057A38
	public int sqrMagnitude
	{
		get
		{
			return this.x * this.x + this.y * this.y;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00059858 File Offset: 0x00057A58
	public long sqrMagnitudeLong
	{
		get
		{
			long num = (long)this.x;
			long num2 = (long)this.y;
			return num * num + num2 * num2;
		}
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0005987C File Offset: 0x00057A7C
	public int magnitude
	{
		get
		{
			long num = (long)this.x;
			long num2 = (long)this.y;
			return IntMath.Sqrt(num * num + num2 * num2);
		}
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x000598A8 File Offset: 0x00057AA8
	public static int Dot(VInt2 a, VInt2 b)
	{
		return a.x * b.x + a.y * b.y;
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000598CC File Offset: 0x00057ACC
	public static long DotLong(ref VInt2 a, ref VInt2 b)
	{
		return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x000598F0 File Offset: 0x00057AF0
	public static long DotLong(VInt2 a, VInt2 b)
	{
		return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x00059918 File Offset: 0x00057B18
	public static long DetLong(ref VInt2 a, ref VInt2 b)
	{
		return (long)a.x * (long)b.y - (long)a.y * (long)b.x;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0005993C File Offset: 0x00057B3C
	public static long DetLong(VInt2 a, VInt2 b)
	{
		return (long)a.x * (long)b.y - (long)a.y * (long)b.x;
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00059964 File Offset: 0x00057B64
	public override bool Equals(object o)
	{
		if (o == null)
		{
			return false;
		}
		VInt2 vint = (VInt2)o;
		return this.x == vint.x && this.y == vint.y;
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000599A4 File Offset: 0x00057BA4
	public override int GetHashCode()
	{
		return this.x * 49157 + this.y * 98317;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x000599C0 File Offset: 0x00057BC0
	public static VInt2 Rotate(VInt2 v, int r)
	{
		r %= 4;
		return new VInt2(v.x * VInt2.Rotations[r * 4] + v.y * VInt2.Rotations[r * 4 + 1], v.x * VInt2.Rotations[r * 4 + 2] + v.y * VInt2.Rotations[r * 4 + 3]);
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x00059A24 File Offset: 0x00057C24
	public static VInt2 Min(VInt2 a, VInt2 b)
	{
		return new VInt2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x00059A54 File Offset: 0x00057C54
	public static VInt2 Max(VInt2 a, VInt2 b)
	{
		return new VInt2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x00059A84 File Offset: 0x00057C84
	public static VInt2 FromInt3XZ(VInt3 o)
	{
		return new VInt2(o.x, o.z);
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x00059A9C File Offset: 0x00057C9C
	public static VInt3 ToInt3XZ(VInt2 o)
	{
		return new VInt3(o.x, 0, o.y);
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00059AB4 File Offset: 0x00057CB4
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"(",
			this.x,
			", ",
			this.y,
			")"
		});
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00059B00 File Offset: 0x00057D00
	public void Min(ref VInt2 r)
	{
		this.x = Mathf.Min(this.x, r.x);
		this.y = Mathf.Min(this.y, r.y);
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00059B3C File Offset: 0x00057D3C
	public void Max(ref VInt2 r)
	{
		this.x = Mathf.Max(this.x, r.x);
		this.y = Mathf.Max(this.y, r.y);
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x00059B78 File Offset: 0x00057D78
	public void Normalize()
	{
		long num = (long)(this.x * 100);
		long num2 = (long)(this.y * 100);
		long num3 = num * num + num2 * num2;
		if (num3 == 0L)
		{
			return;
		}
		long b = (long)IntMath.Sqrt(num3);
		this.x = (int)IntMath.Divide(num * 1000L, b);
		this.y = (int)IntMath.Divide(num2 * 1000L, b);
	}

	// Token: 0x1700014D RID: 333
	// (get) Token: 0x060010C4 RID: 4292 RVA: 0x00059BDC File Offset: 0x00057DDC
	public VInt2 normalized
	{
		get
		{
			VInt2 result = new VInt2(this.x, this.y);
			result.Normalize();
			return result;
		}
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x00059C04 File Offset: 0x00057E04
	public static VInt2 ClampMagnitude(VInt2 v, int maxLength)
	{
		long sqrMagnitudeLong = v.sqrMagnitudeLong;
		long num = (long)maxLength;
		if (sqrMagnitudeLong > num * num)
		{
			long b = (long)IntMath.Sqrt(sqrMagnitudeLong);
			int num2 = (int)IntMath.Divide((long)(v.x * maxLength), b);
			int num3 = (int)IntMath.Divide((long)(v.x * maxLength), b);
			return new VInt2(num2, num3);
		}
		return v;
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x00059C5C File Offset: 0x00057E5C
	public static explicit operator Vector2(VInt2 ob)
	{
		return new Vector2((float)ob.x * 0.001f, (float)ob.y * 0.001f);
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00059C80 File Offset: 0x00057E80
	public static explicit operator VInt2(Vector2 ob)
	{
		return new VInt2((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)));
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x00059CB0 File Offset: 0x00057EB0
	public static VInt2 operator +(VInt2 a, VInt2 b)
	{
		return new VInt2(a.x + b.x, a.y + b.y);
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x00059CD8 File Offset: 0x00057ED8
	public static VInt2 operator -(VInt2 a, VInt2 b)
	{
		return new VInt2(a.x - b.x, a.y - b.y);
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x00059D00 File Offset: 0x00057F00
	public static bool operator ==(VInt2 a, VInt2 b)
	{
		return a.x == b.x && a.y == b.y;
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x00059D34 File Offset: 0x00057F34
	public static bool operator !=(VInt2 a, VInt2 b)
	{
		return a.x != b.x || a.y != b.y;
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x00059D60 File Offset: 0x00057F60
	public static VInt2 operator -(VInt2 lhs)
	{
		lhs.x = -lhs.x;
		lhs.y = -lhs.y;
		return lhs;
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00059D84 File Offset: 0x00057F84
	public static VInt2 operator *(VInt2 lhs, int rhs)
	{
		lhs.x *= rhs;
		lhs.y *= rhs;
		return lhs;
	}

	// Token: 0x04000B9C RID: 2972
	public int x;

	// Token: 0x04000B9D RID: 2973
	public int y;

	// Token: 0x04000B9E RID: 2974
	public static VInt2 zero = default(VInt2);

	// Token: 0x04000B9F RID: 2975
	private static readonly int[] Rotations = new int[]
	{
		1,
		0,
		0,
		1,
		0,
		1,
		-1,
		0,
		-1,
		0,
		0,
		-1,
		0,
		-1,
		1,
		0
	};
}
