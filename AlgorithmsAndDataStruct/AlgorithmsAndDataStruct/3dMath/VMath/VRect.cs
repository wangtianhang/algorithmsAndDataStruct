using System;

// Token: 0x02000215 RID: 533
public struct VRect
{
	// Token: 0x060010DE RID: 4318 RVA: 0x00059F04 File Offset: 0x00058104
	public VRect(int left, int top, int width, int height)
	{
		this.m_XMin = left;
		this.m_YMin = top;
		this.m_Width = width;
		this.m_Height = height;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00059F24 File Offset: 0x00058124
	public VRect(VRect source)
	{
		this.m_XMin = source.m_XMin;
		this.m_YMin = source.m_YMin;
		this.m_Width = source.m_Width;
		this.m_Height = source.m_Height;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x00059F68 File Offset: 0x00058168
	public static VRect MinMaxRect(int left, int top, int right, int bottom)
	{
		return new VRect(left, top, right - left, bottom - top);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x00059F78 File Offset: 0x00058178
	public void Set(int left, int top, int width, int height)
	{
		this.m_XMin = left;
		this.m_YMin = top;
		this.m_Width = width;
		this.m_Height = height;
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00059F98 File Offset: 0x00058198
	// (set) Token: 0x060010E3 RID: 4323 RVA: 0x00059FA0 File Offset: 0x000581A0
	public int x
	{
		get
		{
			return this.m_XMin;
		}
		set
		{
			this.m_XMin = value;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00059FAC File Offset: 0x000581AC
	// (set) Token: 0x060010E5 RID: 4325 RVA: 0x00059FB4 File Offset: 0x000581B4
	public int y
	{
		get
		{
			return this.m_YMin;
		}
		set
		{
			this.m_YMin = value;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00059FC0 File Offset: 0x000581C0
	// (set) Token: 0x060010E7 RID: 4327 RVA: 0x00059FD4 File Offset: 0x000581D4
	public VInt2 position
	{
		get
		{
			return new VInt2(this.m_XMin, this.m_YMin);
		}
		set
		{
			this.m_XMin = value.x;
			this.m_YMin = value.y;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00059FF0 File Offset: 0x000581F0
	// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0005A020 File Offset: 0x00058220
	public VInt2 center
	{
		get
		{
			return new VInt2(this.x + (this.m_Width >> 1), this.y + (this.m_Height >> 1));
		}
		set
		{
			this.m_XMin = value.x - (this.m_Width >> 1);
			this.m_YMin = value.y - (this.m_Height >> 1);
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060010EA RID: 4330 RVA: 0x0005A05C File Offset: 0x0005825C
	// (set) Token: 0x060010EB RID: 4331 RVA: 0x0005A070 File Offset: 0x00058270
	public VInt2 min
	{
		get
		{
			return new VInt2(this.xMin, this.yMin);
		}
		set
		{
			this.xMin = value.x;
			this.yMin = value.y;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060010EC RID: 4332 RVA: 0x0005A08C File Offset: 0x0005828C
	// (set) Token: 0x060010ED RID: 4333 RVA: 0x0005A0A0 File Offset: 0x000582A0
	public VInt2 max
	{
		get
		{
			return new VInt2(this.xMax, this.yMax);
		}
		set
		{
			this.xMax = value.x;
			this.yMax = value.y;
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x060010EE RID: 4334 RVA: 0x0005A0BC File Offset: 0x000582BC
	// (set) Token: 0x060010EF RID: 4335 RVA: 0x0005A0C4 File Offset: 0x000582C4
	public int width
	{
		get
		{
			return this.m_Width;
		}
		set
		{
			this.m_Width = value;
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0005A0D0 File Offset: 0x000582D0
	// (set) Token: 0x060010F1 RID: 4337 RVA: 0x0005A0D8 File Offset: 0x000582D8
	public int height
	{
		get
		{
			return this.m_Height;
		}
		set
		{
			this.m_Height = value;
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0005A0E4 File Offset: 0x000582E4
	// (set) Token: 0x060010F3 RID: 4339 RVA: 0x0005A0F8 File Offset: 0x000582F8
	public VInt2 size
	{
		get
		{
			return new VInt2(this.m_Width, this.m_Height);
		}
		set
		{
			this.m_Width = value.x;
			this.m_Height = value.y;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0005A114 File Offset: 0x00058314
	// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0005A11C File Offset: 0x0005831C
	public int xMin
	{
		get
		{
			return this.m_XMin;
		}
		set
		{
			int xMax = this.xMax;
			this.m_XMin = value;
			this.m_Width = xMax - this.m_XMin;
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0005A148 File Offset: 0x00058348
	// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0005A150 File Offset: 0x00058350
	public int yMin
	{
		get
		{
			return this.m_YMin;
		}
		set
		{
			int yMax = this.yMax;
			this.m_YMin = value;
			this.m_Height = yMax - this.m_YMin;
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0005A17C File Offset: 0x0005837C
	// (set) Token: 0x060010F9 RID: 4345 RVA: 0x0005A18C File Offset: 0x0005838C
	public int xMax
	{
		get
		{
			return this.m_Width + this.m_XMin;
		}
		set
		{
			this.m_Width = value - this.m_XMin;
		}
	}

	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060010FA RID: 4346 RVA: 0x0005A19C File Offset: 0x0005839C
	// (set) Token: 0x060010FB RID: 4347 RVA: 0x0005A1AC File Offset: 0x000583AC
	public int yMax
	{
		get
		{
			return this.m_Height + this.m_YMin;
		}
		set
		{
			this.m_Height = value - this.m_YMin;
		}
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0005A1BC File Offset: 0x000583BC
	public override string ToString()
	{
		object[] args = new object[]
		{
			this.x,
			this.y,
			this.width,
			this.height
		};
		return string.Format("(x:{0:F2}, y:{1:F2}, width:{2:F2}, height:{3:F2})", args);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0005A214 File Offset: 0x00058414
	public string ToString(string format)
	{
		object[] args = new object[]
		{
			this.x.ToString(format),
			this.y.ToString(format),
			this.width.ToString(format),
			this.height.ToString(format)
		};
		return string.Format("(x:{0}, y:{1}, width:{2}, height:{3})", args);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0005A27C File Offset: 0x0005847C
	public bool Contains(VInt2 point)
	{
		return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0005A2D4 File Offset: 0x000584D4
	public bool Contains(VInt3 point)
	{
		return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0005A32C File Offset: 0x0005852C
	public bool Contains(VInt3 point, bool allowInverse)
	{
		if (!allowInverse)
		{
			return this.Contains(point);
		}
		bool flag = false;
		if (((float)this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || ((float)this.width >= 0f && point.x >= this.xMin && point.x < this.xMax))
		{
			flag = true;
		}
		return flag && (((float)this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || ((float)this.height >= 0f && point.y >= this.yMin && point.y < this.yMax));
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0005A428 File Offset: 0x00058628
	private static VRect OrderMinMax(VRect rect)
	{
		if (rect.xMin > rect.xMax)
		{
			int xMin = rect.xMin;
			rect.xMin = rect.xMax;
			rect.xMax = xMin;
		}
		if (rect.yMin > rect.yMax)
		{
			int yMin = rect.yMin;
			rect.yMin = rect.yMax;
			rect.yMax = yMin;
		}
		return rect;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0005A498 File Offset: 0x00058698
	public bool Overlaps(VRect other)
	{
		return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0005A4F0 File Offset: 0x000586F0
	public bool Overlaps(VRect other, bool allowInverse)
	{
		VRect rect = this;
		if (allowInverse)
		{
			rect = VRect.OrderMinMax(rect);
			other = VRect.OrderMinMax(other);
		}
		return rect.Overlaps(other);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0005A524 File Offset: 0x00058724
	public override int GetHashCode()
	{
		return this.x.GetHashCode() ^ this.width.GetHashCode() << 2 ^ this.y.GetHashCode() >> 2 ^ this.height.GetHashCode() >> 1;
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0005A574 File Offset: 0x00058774
	public override bool Equals(object other)
	{
		if (!(other is VRect))
		{
			return false;
		}
		VRect vrect = (VRect)other;
		return this.x.Equals(vrect.x) && this.y.Equals(vrect.y) && this.width.Equals(vrect.width) && this.height.Equals(vrect.height);
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0005A5FC File Offset: 0x000587FC
	public static bool operator !=(VRect lhs, VRect rhs)
	{
		return lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0005A658 File Offset: 0x00058858
	public static bool operator ==(VRect lhs, VRect rhs)
	{
		return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
	}

	// Token: 0x04000BA1 RID: 2977
	private int m_XMin;

	// Token: 0x04000BA2 RID: 2978
	private int m_YMin;

	// Token: 0x04000BA3 RID: 2979
	private int m_Width;

	// Token: 0x04000BA4 RID: 2980
	private int m_Height;
}
