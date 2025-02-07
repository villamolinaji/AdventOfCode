using System.Text;

namespace _20
{
	public class Tile
	{
		public long Id { get; set; }

		public string[] Image { get; set; }

		public int Orientation { get; set; } = 0;

		public int Size => this.Image.Length;

		public Tile(long id, string[] image)
		{
			this.Id = id;
			this.Image = image;
		}

		public string Row(int irow) => GetSlice(irow, 0, 0, 1);

		public string Col(int icol) => GetSlice(0, icol, 1, 0);

		public string Top() => Row(0);

		public string Bottom() => Row(this.Size - 1);

		public string Left() => Col(0);

		public string Right() => Col(this.Size - 1);

		public void ChangeOrientation()
		{
			this.Orientation++;
		}

		public override string ToString()
		{
			return $"Tile {Id}:\n" + string.Join("\n", Enumerable.Range(0, Size).Select(i => Row(i)));
		}

		public char GetPixel(int row, int col)
		{
			for (var i = 0; i < this.Orientation % 4; i++)
			{
				(row, col) = (col, this.Size - 1 - row);
			}

			if (this.Orientation % 8 >= 4)
			{
				col = Size - 1 - col;
			}

			return this.Image[row][col];
		}

		private string GetSlice(int irow, int icol, int drow, int dcol)
		{
			var sb = new StringBuilder();

			for (var i = 0; i < Size; i++)
			{
				sb.Append(GetPixel(irow, icol));
				irow += drow;
				icol += dcol;
			}

			return sb.ToString();
		}
	}
}
