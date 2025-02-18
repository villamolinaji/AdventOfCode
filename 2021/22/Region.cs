namespace _22
{
	public class Region
	{
		public Segment X { get; set; }

		public Segment Y { get; set; }

		public Segment Z { get; set; }

		public bool IsEmpty => X.IsEmpty || Y.IsEmpty || Z.IsEmpty;

		public long Volume => X.Length * Y.Length * Z.Length;

		public Region(Segment x, Segment y, Segment z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Region Intersect(Region region) =>
			new Region(this.X.Intersect(region.X), this.Y.Intersect(region.Y), this.Z.Intersect(region.Z));
	}
}
