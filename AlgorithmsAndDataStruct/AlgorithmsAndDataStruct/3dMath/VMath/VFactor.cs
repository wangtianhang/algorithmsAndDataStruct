using System;

// Token: 0x02000216 RID: 534
[Serializable]
public struct VFactor
{
	// Token: 0x06001108 RID: 4360 RVA: 0x0005A6B4 File Offset: 0x000588B4
	public VFactor(long n, long d)
	{
		this.nom = n;
		this.den = d;
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x0600110A RID: 4362 RVA: 0x0005A734 File Offset: 0x00058934
	public int roundInt
	{
		get
		{
			return (int)IntMath.Divide(this.nom, this.den);
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x0600110B RID: 4363 RVA: 0x0005A748 File Offset: 0x00058948
	public int integer
	{
		get
		{
			return (int)(this.nom / this.den);
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x0600110C RID: 4364 RVA: 0x0005A758 File Offset: 0x00058958
	public float single
	{
		get
		{
			double num = (double)this.nom / (double)this.den;
			return (float)num;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x0600110D RID: 4365 RVA: 0x0005A778 File Offset: 0x00058978
	public bool IsPositive
	{
		get
		{
			DebugHelper.Assert(this.den != 0L, "VFactor: denominator is zero !");
			if (this.nom == 0L)
			{
				return false;
			}
			bool flag = this.nom > 0L;
			bool flag2 = this.den > 0L;
			return !(flag ^ flag2);
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x0600110E RID: 4366 RVA: 0x0005A7C8 File Offset: 0x000589C8
	public bool IsNegative
	{
		get
		{
			DebugHelper.Assert(this.den != 0L, "VFactor: denominator is zero !");
			if (this.nom == 0L)
			{
				return false;
			}
			bool flag = this.nom > 0L;
			bool flag2 = this.den > 0L;
			return flag ^ flag2;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x0600110F RID: 4367 RVA: 0x0005A814 File Offset: 0x00058A14
	public bool IsZero
	{
		get
		{
			return this.nom == 0L;
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0005A820 File Offset: 0x00058A20
	public override bool Equals(object obj)
	{
		return obj != null && base.GetType() == obj.GetType() && this == (VFactor)obj;
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0005A858 File Offset: 0x00058A58
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06001112 RID: 4370 RVA: 0x0005A86C File Offset: 0x00058A6C
	public VFactor Inverse
	{
		get
		{
			return new VFactor(this.den, this.nom);
		}
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x0005A880 File Offset: 0x00058A80
	public override string ToString()
	{
		return this.single.ToString();
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x0005A89C File Offset: 0x00058A9C
	public void strip()
	{
		while ((this.nom & VFactor.mask_) > VFactor.upper_ && (this.den & VFactor.mask_) > VFactor.upper_)
		{
			this.nom >>= 1;
			this.den >>= 1;
		}
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x0005A8F8 File Offset: 0x00058AF8
	public static bool operator <(VFactor a, VFactor b)
	{
		long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num < num2) : (num > num2);
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x0005A950 File Offset: 0x00058B50
	public static bool operator >(VFactor a, VFactor b)
	{
		long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num > num2) : (num < num2);
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x0005A9A8 File Offset: 0x00058BA8
	public static bool operator <=(VFactor a, VFactor b)
	{
		long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num <= num2) : (num >= num2);
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x0005AA08 File Offset: 0x00058C08
	public static bool operator >=(VFactor a, VFactor b)
	{
		long num = a.nom * b.den;
		long num2 = b.nom * a.den;
		bool flag = b.den > 0L ^ a.den > 0L;
		return (!flag) ? (num >= num2) : (num <= num2);
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x0005AA68 File Offset: 0x00058C68
	public static bool operator ==(VFactor a, VFactor b)
	{
		return a.nom * b.den == b.nom * a.den;
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0005AA98 File Offset: 0x00058C98
	public static bool operator !=(VFactor a, VFactor b)
	{
		return a.nom * b.den != b.nom * a.den;
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0005AAC0 File Offset: 0x00058CC0
	public static bool operator <(VFactor a, long b)
	{
		long num = a.nom;
		long num2 = b * a.den;
		return (a.den <= 0L) ? (num > num2) : (num < num2);
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x0005AAFC File Offset: 0x00058CFC
	public static bool operator >(VFactor a, long b)
	{
		long num = a.nom;
		long num2 = b * a.den;
		return (a.den <= 0L) ? (num < num2) : (num > num2);
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0005AB38 File Offset: 0x00058D38
	public static bool operator <=(VFactor a, long b)
	{
		long num = a.nom;
		long num2 = b * a.den;
		return (a.den <= 0L) ? (num >= num2) : (num <= num2);
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0005AB78 File Offset: 0x00058D78
	public static bool operator >=(VFactor a, long b)
	{
		long num = a.nom;
		long num2 = b * a.den;
		return (a.den <= 0L) ? (num <= num2) : (num >= num2);
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0005ABB8 File Offset: 0x00058DB8
	public static bool operator ==(VFactor a, long b)
	{
		return a.nom == b * a.den;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0005ABCC File Offset: 0x00058DCC
	public static bool operator !=(VFactor a, long b)
	{
		return a.nom != b * a.den;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0005ABE4 File Offset: 0x00058DE4
	public static VFactor operator +(VFactor a, VFactor b)
	{
		return new VFactor
		{
			nom = a.nom * b.den + b.nom * a.den,
			den = a.den * b.den
		};
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0005AC38 File Offset: 0x00058E38
	public static VFactor operator +(VFactor a, long b)
	{
		a.nom += b * a.den;
		return a;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0005AC54 File Offset: 0x00058E54
	public static VFactor operator -(VFactor a, VFactor b)
	{
		return new VFactor
		{
			nom = a.nom * b.den - b.nom * a.den,
			den = a.den * b.den
		};
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0005ACA8 File Offset: 0x00058EA8
	public static VFactor operator -(VFactor a, long b)
	{
		a.nom -= b * a.den;
		return a;
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0005ACC4 File Offset: 0x00058EC4
	public static VFactor operator *(VFactor a, long b)
	{
		a.nom *= b;
		return a;
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x0005ACD8 File Offset: 0x00058ED8
	public static VFactor operator /(VFactor a, long b)
	{
		a.den *= b;
		return a;
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0005ACEC File Offset: 0x00058EEC
	public static VInt3 operator *(VInt3 v, VFactor f)
	{
		return IntMath.Divide(v, f.nom, f.den);
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0005AD04 File Offset: 0x00058F04
	public static VInt2 operator *(VInt2 v, VFactor f)
	{
		return IntMath.Divide(v, f.nom, f.den);
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0005AD1C File Offset: 0x00058F1C
	public static VInt3 operator /(VInt3 v, VFactor f)
	{
		return IntMath.Divide(v, f.den, f.nom);
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x0005AD34 File Offset: 0x00058F34
	public static VInt2 operator /(VInt2 v, VFactor f)
	{
		return IntMath.Divide(v, f.den, f.nom);
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x0005AD4C File Offset: 0x00058F4C
	public static int operator *(int i, VFactor f)
	{
		return (int)IntMath.Divide((long)i * f.nom, f.den);
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0005AD68 File Offset: 0x00058F68
	public static VFactor operator -(VFactor a)
	{
		a.nom = -a.nom;
		return a;
	}

	// Token: 0x04000BA5 RID: 2981
	public long nom;

	// Token: 0x04000BA6 RID: 2982
	public long den;

	// Token: 0x04000BA7 RID: 2983
	[NonSerialized]
	public static VFactor zero = new VFactor(0L, 1L);

	// Token: 0x04000BA8 RID: 2984
	[NonSerialized]
	public static VFactor one = new VFactor(1L, 1L);

	// Token: 0x04000BA9 RID: 2985
	[NonSerialized]
	public static VFactor pi = new VFactor(31416L, 10000L);

	// Token: 0x04000BAA RID: 2986
	[NonSerialized]
	public static VFactor twoPi = new VFactor(62832L, 10000L);

	// Token: 0x04000BAB RID: 2987
	private static long mask_ = long.MaxValue;

	// Token: 0x04000BAC RID: 2988
	private static long upper_ = 16777215L;
}
