namespace _22
{
	public class Segment
	{
		public int Start { get; set; }

		public int End { get; set; }

		public bool IsEmpty => Start > End;

		public long Length =>
			IsEmpty
				? 0
				: End - Start + 1;

		public Segment(int start, int end)
		{
			Start = start;
			End = end;
		}

		public Segment Intersect(Segment segment) =>
			new Segment(Math.Max(this.Start, segment.Start), Math.Min(this.End, segment.End));
	}
}
