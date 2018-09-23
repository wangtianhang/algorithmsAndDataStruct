using System;

// Token: 0x02000214 RID: 532
[Serializable]
public struct VInt
{
	// Token: 0x060010CE RID: 4302 RVA: 0x00059DA8 File Offset: 0x00057FA8
	public VInt(int i)
	{
		this.i = i;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x00059DB4 File Offset: 0x00057FB4
	public VInt(float f)
	{
		this.i = (int)Math.Round((double)(f * 1000f));
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x00059DCC File Offset: 0x00057FCC
	public override bool Equals(object o)
	{
		if (o == null)
		{
			return false;
		}
		VInt vint = (VInt)o;
		return this.i == vint.i;
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x00059DF8 File Offset: 0x00057FF8
	public override int GetHashCode()
	{
		return this.i.GetHashCode();
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x00059E08 File Offset: 0x00058008
	public static VInt Min(VInt a, VInt b)
	{
		return new VInt(Math.Min(a.i, b.i));
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x00059E24 File Offset: 0x00058024
	public static VInt Max(VInt a, VInt b)
	{
		return new VInt(Math.Max(a.i, b.i));
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x00059E40 File Offset: 0x00058040
	public override string ToString()
	{
		return this.scalar.ToString();
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00059E5C File Offset: 0x0005805C
	public float scalar
	{
		get
		{
			return (float)this.i * 0.001f;
		}
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x00059E6C File Offset: 0x0005806C
	public static explicit operator VInt(float f)
	{
		return new VInt((int)Math.Round((double)(f * 1000f)));
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00059E84 File Offset: 0x00058084
	public static implicit operator VInt(int i)
	{
		return new VInt(i);
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x00059E8C File Offset: 0x0005808C
	public static explicit operator float(VInt ob)
	{
		return (float)ob.i * 0.001f;
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x00059E9C File Offset: 0x0005809C
	public static explicit operator long(VInt ob)
	{
		return (long)ob.i;
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x00059EA8 File Offset: 0x000580A8
	public static VInt operator +(VInt a, VInt b)
	{
		return new VInt(a.i + b.i);
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x00059EC0 File Offset: 0x000580C0
	public static VInt operator -(VInt a, VInt b)
	{
		return new VInt(a.i - b.i);
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x00059ED8 File Offset: 0x000580D8
	public static bool operator ==(VInt a, VInt b)
	{
		return a.i == b.i;
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x00059EEC File Offset: 0x000580EC
	public static bool operator !=(VInt a, VInt b)
	{
		return a.i != b.i;
	}

	// Token: 0x04000BA0 RID: 2976
	public int i;
}
