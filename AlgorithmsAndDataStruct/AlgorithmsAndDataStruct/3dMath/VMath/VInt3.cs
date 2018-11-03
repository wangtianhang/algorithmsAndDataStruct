using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
[Serializable]
public struct VInt3
{
	// Token: 0x06001076 RID: 4214 RVA: 0x00058824 File Offset: 0x00056A24
	public VInt3(Vector3 position)
	{
		this.x = (int)Math.Round((double)(position.x * 1000f));
		this.y = (int)Math.Round((double)(position.y * 1000f));
		this.z = (int)Math.Round((double)(position.z * 1000f));
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x00058880 File Offset: 0x00056A80
	public VInt3(int _x, int _y, int _z)
	{
		this.x = _x;
		this.y = _y;
		this.z = _z;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00058918 File Offset: 0x00056B18
	public VInt3 DivBy2()
	{
		this.x >>= 1;
		this.y >>= 1;
		this.z >>= 1;
		return this;
	}

	// Token: 0x1700013C RID: 316
	public int this[int i]
	{
		get
		{
			return (i != 0) ? ((i != 1) ? this.z : this.y) : this.x;
		}
		set
		{
			if (i == 0)
			{
				this.x = value;
			}
			else if (i == 1)
			{
				this.y = value;
			}
			else
			{
				this.z = value;
			}
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x000589C0 File Offset: 0x00056BC0
	public static float Angle(VInt3 lhs, VInt3 rhs)
	{
		double num = (double)VInt3.Dot(lhs, rhs) / ((double)lhs.magnitude * (double)rhs.magnitude);
		num = ((num >= -1.0) ? ((num <= 1.0) ? num : 1.0) : -1.0);
		return (float)Math.Acos(num);
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x00058A2C File Offset: 0x00056C2C
	public static VFactor AngleInt(VInt3 lhs, VInt3 rhs)
	{
		long den = (long)lhs.magnitude * (long)rhs.magnitude;
		return IntMath.acos((long)VInt3.Dot(ref lhs, ref rhs), den);
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x00058A5C File Offset: 0x00056C5C
	public static int Dot(ref VInt3 lhs, ref VInt3 rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x00058A88 File Offset: 0x00056C88
	public static int Dot(VInt3 lhs, VInt3 rhs)
	{
		return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x00058ABC File Offset: 0x00056CBC
	public static long DotLong(VInt3 lhs, VInt3 rhs)
	{
		return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00058AF4 File Offset: 0x00056CF4
	public static long DotLong(ref VInt3 lhs, ref VInt3 rhs)
	{
		return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x00058B28 File Offset: 0x00056D28
	public static long DotXZLong(ref VInt3 lhs, ref VInt3 rhs)
	{
		return (long)lhs.x * (long)rhs.x + (long)lhs.z * (long)rhs.z;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00058B4C File Offset: 0x00056D4C
	public static long DotXZLong(VInt3 lhs, VInt3 rhs)
	{
		return (long)lhs.x * (long)rhs.x + (long)lhs.z * (long)rhs.z;
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x00058B74 File Offset: 0x00056D74
	public static VInt3 Cross(ref VInt3 lhs, ref VInt3 rhs)
	{
		return new VInt3(IntMath.Divide(lhs.y * rhs.z - lhs.z * rhs.y, 1000), IntMath.Divide(lhs.z * rhs.x - lhs.x * rhs.z, 1000), IntMath.Divide(lhs.x * rhs.y - lhs.y * rhs.x, 1000));
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00058BF8 File Offset: 0x00056DF8
	public static VInt3 Cross(VInt3 lhs, VInt3 rhs)
	{
		return new VInt3(IntMath.Divide(lhs.y * rhs.z - lhs.z * rhs.y, 1000), IntMath.Divide(lhs.z * rhs.x - lhs.x * rhs.z, 1000), IntMath.Divide(lhs.x * rhs.y - lhs.y * rhs.x, 1000));
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x00058C88 File Offset: 0x00056E88
	public static VInt3 MoveTowards(VInt3 from, VInt3 to, int dt)
	{
		if ((to - from).sqrMagnitudeLong <= (long)(dt * dt))
		{
			return to;
		}
		return from + (to - from).NormalizeTo(dt);
	}

	// Token: 0x06001087 RID: 4231 RVA: 0x00058CC8 File Offset: 0x00056EC8
	public VInt3 Normal2D()
	{
		return new VInt3(this.z, this.y, -this.x);
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x00058CE4 File Offset: 0x00056EE4
	public VInt3 NormalizeTo(int newMagn)
	{
		long num = (long)(this.x * 100);
		long num2 = (long)(this.y * 100);
		long num3 = (long)(this.z * 100);
		long num4 = num * num + num2 * num2 + num3 * num3;
		if (num4 == 0L)
		{
			return this;
		}
		long b = (long)IntMath.Sqrt(num4);
		long num5 = (long)newMagn;
		this.x = (int)IntMath.Divide(num * num5, b);
		this.y = (int)IntMath.Divide(num2 * num5, b);
		this.z = (int)IntMath.Divide(num3 * num5, b);
		return this;
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06001089 RID: 4233 RVA: 0x00058D74 File Offset: 0x00056F74
	public Vector3 vec3
	{
		get
		{
			return new Vector3((float)this.x * 0.001f, (float)this.y * 0.001f, (float)this.z * 0.001f);
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x0600108A RID: 4234 RVA: 0x00058DB0 File Offset: 0x00056FB0
	public VInt2 xz
	{
		get
		{
			return new VInt2(this.x, this.z);
		}
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x0600108B RID: 4235 RVA: 0x00058DC4 File Offset: 0x00056FC4
	public int magnitude
	{
		get
		{
			long num = (long)this.x;
			long num2 = (long)this.y;
			long num3 = (long)this.z;
			return IntMath.Sqrt(num * num + num2 * num2 + num3 * num3);
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x0600108C RID: 4236 RVA: 0x00058DFC File Offset: 0x00056FFC
	public int magnitude2D
	{
		get
		{
			long num = (long)this.x;
			long num2 = (long)this.z;
			return IntMath.Sqrt(num * num + num2 * num2);
		}
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x00058E28 File Offset: 0x00057028
	public VInt3 RotateY(ref VFactor radians)
	{
		VFactor vfactor;
		VFactor vfactor2;
		IntMath.sincos(out vfactor, out vfactor2, radians.nom, radians.den);
		long num = vfactor2.nom * vfactor.den;
		long num2 = vfactor2.den * vfactor.nom;
		long b = vfactor2.den * vfactor.den;
		VInt3 vint;
		vint.x = (int)IntMath.Divide((long)this.x * num + (long)this.z * num2, b);
		vint.z = (int)IntMath.Divide((long)(-(long)this.x) * num2 + (long)this.z * num, b);
		vint.y = 0;
		return vint.NormalizeTo(1000);
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x00058ED8 File Offset: 0x000570D8
	public VInt3 RotateY(int degree)
	{
		VFactor vfactor;
		VFactor vfactor2;
		IntMath.sincos(out vfactor, out vfactor2, (long)(31416 * degree), 1800000L);
		long num = vfactor2.nom * vfactor.den;
		long num2 = vfactor2.den * vfactor.nom;
		long b = vfactor2.den * vfactor.den;
		VInt3 vint;
		vint.x = (int)IntMath.Divide((long)this.x * num + (long)this.z * num2, b);
		vint.z = (int)IntMath.Divide((long)(-(long)this.x) * num2 + (long)this.z * num, b);
		vint.y = 0;
		return vint.NormalizeTo(1000);
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600108F RID: 4239 RVA: 0x00058F8C File Offset: 0x0005718C
	public int costMagnitude
	{
		get
		{
			return this.magnitude;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06001090 RID: 4240 RVA: 0x00058F94 File Offset: 0x00057194
	public float worldMagnitude
	{
		get
		{
			double num = (double)this.x;
			double num2 = (double)this.y;
			double num3 = (double)this.z;
			return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3) * 0.001f;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06001091 RID: 4241 RVA: 0x00058FD0 File Offset: 0x000571D0
	public double sqrMagnitude
	{
		get
		{
			double num = (double)this.x;
			double num2 = (double)this.y;
			double num3 = (double)this.z;
			return num * num + num2 * num2 + num3 * num3;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06001092 RID: 4242 RVA: 0x00059000 File Offset: 0x00057200
	public long sqrMagnitudeLong
	{
		get
		{
			long num = (long)this.x;
			long num2 = (long)this.y;
			long num3 = (long)this.z;
			return num * num + num2 * num2 + num3 * num3;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06001093 RID: 4243 RVA: 0x00059030 File Offset: 0x00057230
	public long sqrMagnitudeLong2D
	{
		get
		{
			long num = (long)this.x;
			long num2 = (long)this.z;
			return num * num + num2 * num2;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06001094 RID: 4244 RVA: 0x00059054 File Offset: 0x00057254
	public int unsafeSqrMagnitude
	{
		get
		{
			return this.x * this.x + this.y * this.y + this.z * this.z;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06001095 RID: 4245 RVA: 0x00059080 File Offset: 0x00057280
	public VInt3 abs
	{
		get
		{
			return new VInt3(Math.Abs(this.x), Math.Abs(this.y), Math.Abs(this.z));
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06001096 RID: 4246 RVA: 0x000590B4 File Offset: 0x000572B4
	[Obsolete("Same implementation as .magnitude")]
	public float safeMagnitude
	{
		get
		{
			double num = (double)this.x;
			double num2 = (double)this.y;
			double num3 = (double)this.z;
			return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06001097 RID: 4247 RVA: 0x000590EC File Offset: 0x000572EC
	[Obsolete(".sqrMagnitude is now per default safe (.unsafeSqrMagnitude can be used for unsafe operations)")]
	public float safeSqrMagnitude
	{
		get
		{
			float num = (float)this.x * 0.001f;
			float num2 = (float)this.y * 0.001f;
			float num3 = (float)this.z * 0.001f;
			return num * num + num2 * num2 + num3 * num3;
		}
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x00059130 File Offset: 0x00057330
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"( ",
			this.x,
			", ",
			this.y,
			", ",
			this.z,
			")"
		});
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x00059194 File Offset: 0x00057394
	public override bool Equals(object o)
	{
		if (o == null)
		{
			return false;
		}
		VInt3 vint = (VInt3)o;
		return this.x == vint.x && this.y == vint.y && this.z == vint.z;
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x000591E8 File Offset: 0x000573E8
	public override int GetHashCode()
	{
		return this.x * 73856093 ^ this.y * 19349663 ^ this.z * 83492791;
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0005921C File Offset: 0x0005741C
	public static VInt3 Lerp(VInt3 a, VInt3 b, float f)
	{
		return new VInt3(Mathf.RoundToInt((float)a.x * (1f - f)) + Mathf.RoundToInt((float)b.x * f), Mathf.RoundToInt((float)a.y * (1f - f)) + Mathf.RoundToInt((float)b.y * f), Mathf.RoundToInt((float)a.z * (1f - f)) + Mathf.RoundToInt((float)b.z * f));
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x000592A0 File Offset: 0x000574A0
	public static VInt3 Lerp(VInt3 a, VInt3 b, VFactor f)
	{
		return new VInt3((int)IntMath.Divide((long)(b.x - a.x) * f.nom, f.den) + a.x, (int)IntMath.Divide((long)(b.y - a.y) * f.nom, f.den) + a.y, (int)IntMath.Divide((long)(b.z - a.z) * f.nom, f.den) + a.z);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0005933C File Offset: 0x0005753C
	public long XZSqrMagnitude(VInt3 rhs)
	{
		long num = (long)(this.x - rhs.x);
		long num2 = (long)(this.z - rhs.z);
		return num * num + num2 * num2;
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x00059370 File Offset: 0x00057570
	public long XZSqrMagnitude(ref VInt3 rhs)
	{
		long num = (long)(this.x - rhs.x);
		long num2 = (long)(this.z - rhs.z);
		return num * num + num2 * num2;
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x000593A4 File Offset: 0x000575A4
	public bool IsEqualXZ(VInt3 rhs)
	{
		return this.x == rhs.x && this.z == rhs.z;
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x000593D8 File Offset: 0x000575D8
	public bool IsEqualXZ(ref VInt3 rhs)
	{
		return this.x == rhs.x && this.z == rhs.z;
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x00059408 File Offset: 0x00057608
	public static bool operator ==(VInt3 lhs, VInt3 rhs)
	{
		return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x00059444 File Offset: 0x00057644
	public static bool operator !=(VInt3 lhs, VInt3 rhs)
	{
		return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x00059490 File Offset: 0x00057690
	public static explicit operator VInt3(Vector3 ob)
	{
		return new VInt3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x000594E0 File Offset: 0x000576E0
	public static explicit operator Vector3(VInt3 ob)
	{
		return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x00059514 File Offset: 0x00057714
	public static VInt3 operator -(VInt3 lhs, VInt3 rhs)
	{
		lhs.x -= rhs.x;
		lhs.y -= rhs.y;
		lhs.z -= rhs.z;
		return lhs;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x00059564 File Offset: 0x00057764
	public static VInt3 operator -(VInt3 lhs)
	{
		lhs.x = -lhs.x;
		lhs.y = -lhs.y;
		lhs.z = -lhs.z;
		return lhs;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x000595A0 File Offset: 0x000577A0
	public static VInt3 operator +(VInt3 lhs, VInt3 rhs)
	{
		lhs.x += rhs.x;
		lhs.y += rhs.y;
		lhs.z += rhs.z;
		return lhs;
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x000595F0 File Offset: 0x000577F0
	public static VInt3 operator *(VInt3 lhs, int rhs)
	{
		lhs.x *= rhs;
		lhs.y *= rhs;
		lhs.z *= rhs;
		return lhs;
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0005962C File Offset: 0x0005782C
	public static VInt3 operator *(VInt3 lhs, float rhs)
	{
		lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
		lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
		lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
		return lhs;
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x00059684 File Offset: 0x00057884
	public static VInt3 operator *(VInt3 lhs, double rhs)
	{
		lhs.x = (int)Math.Round((double)lhs.x * rhs);
		lhs.y = (int)Math.Round((double)lhs.y * rhs);
		lhs.z = (int)Math.Round((double)lhs.z * rhs);
		return lhs;
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x000596D8 File Offset: 0x000578D8
	public static VInt3 operator *(VInt3 lhs, Vector3 rhs)
	{
		lhs.x = (int)Math.Round((double)((float)lhs.x * rhs.x));
		lhs.y = (int)Math.Round((double)((float)lhs.y * rhs.y));
		lhs.z = (int)Math.Round((double)((float)lhs.z * rhs.z));
		return lhs;
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x00059740 File Offset: 0x00057940
	public static VInt3 operator *(VInt3 lhs, VInt3 rhs)
	{
		lhs.x *= rhs.x;
		lhs.y *= rhs.y;
		lhs.z *= rhs.z;
		return lhs;
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00059790 File Offset: 0x00057990
	public static VInt3 operator /(VInt3 lhs, float rhs)
	{
		lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
		lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
		lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
		return lhs;
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x000597E8 File Offset: 0x000579E8
	public static implicit operator string(VInt3 ob)
	{
		return ob.ToString();
	}

	// Token: 0x04000B90 RID: 2960
	public const int Precision = 1000;

	// Token: 0x04000B91 RID: 2961
	public const float FloatPrecision = 1000f;

	// Token: 0x04000B92 RID: 2962
	public const float PrecisionFactor = 0.001f;

	// Token: 0x04000B93 RID: 2963
	public int x;

	// Token: 0x04000B94 RID: 2964
	public int y;

	// Token: 0x04000B95 RID: 2965
	public int z;

	// Token: 0x04000B96 RID: 2966
	public static readonly VInt3 zero = new VInt3(0, 0, 0);

	// Token: 0x04000B97 RID: 2967
	public static readonly VInt3 one = new VInt3(1000, 1000, 1000);

	// Token: 0x04000B98 RID: 2968
	public static readonly VInt3 half = new VInt3(500, 500, 500);

	// Token: 0x04000B99 RID: 2969
	public static readonly VInt3 forward = new VInt3(0, 0, 1000);

	// Token: 0x04000B9A RID: 2970
	public static readonly VInt3 up = new VInt3(0, 1000, 0);

	// Token: 0x04000B9B RID: 2971
	public static readonly VInt3 right = new VInt3(1000, 0, 0);
}
