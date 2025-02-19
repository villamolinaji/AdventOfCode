namespace _23
{
	record Diagram
	{
		const int columnMaskA = (1 << 11) | (1 << 15) | (1 << 19) | (1 << 23);
		const int columnMaskB = (1 << 12) | (1 << 16) | (1 << 20) | (1 << 24);
		const int columnMaskC = (1 << 13) | (1 << 17) | (1 << 21) | (1 << 25);
		const int columnMaskD = (1 << 14) | (1 << 18) | (1 << 22) | (1 << 26);

		readonly int a;
		readonly int b;
		readonly int c;
		readonly int d;

		Diagram(int a, int b, int c, int d)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		public static Diagram Parse(string[] map)
		{
			var diagram = new Diagram(columnMaskA, columnMaskB, columnMaskC, columnMaskD);

			foreach (var irow in Enumerable.Range(0, map.Length))
			{
				foreach (var icol in Enumerable.Range(0, map[0].Length))
				{
					diagram = diagram.SetItem(new Point(irow, icol), irow < map.Length && icol < map[irow].Length ? map[irow][icol] : '#');
				}
			}

			return diagram;
		}

		public bool CanEnterRoom(char amphipod) =>
			amphipod switch
			{
				'A' => (b & columnMaskA) == 0 && (c & columnMaskA) == 0 && (d & columnMaskA) == 0,
				'B' => (a & columnMaskB) == 0 && (c & columnMaskB) == 0 && (d & columnMaskB) == 0,
				'C' => (a & columnMaskC) == 0 && (b & columnMaskC) == 0 && (d & columnMaskC) == 0,
				'D' => (a & columnMaskD) == 0 && (b & columnMaskD) == 0 && (c & columnMaskD) == 0,
				_ => throw new InvalidOperationException()
			};

		public bool CanMoveToDoor(int colFrom, int colTo)
		{
			var pt = Step(new Point(1, colFrom), colFrom, colTo);

			while (pt.Col != colTo)
			{
				if (this.GetItem(pt) != '.')
				{
					return false;
				}

				pt = Step(pt, colFrom, colTo);
			}

			return true;
		}

		public bool IsColumnFinished(int col) =>
			col switch
			{
				3 => a == columnMaskA,
				5 => b == columnMaskB,
				7 => c == columnMaskC,
				9 => d == columnMaskD,
				_ => throw new InvalidOperationException()
			};

		public bool IsFinished() =>
			IsColumnFinished(3) && IsColumnFinished(5) && IsColumnFinished(7) && IsColumnFinished(9);

		public char GetItem(Point pt)
		{
			var bit = BitFromPoint(pt);

			if (bit == 1 << 31)
			{
				return '#';
			}
			else if ((a & bit) != 0)
			{
				return 'A';
			}
			else if ((b & bit) != 0)
			{
				return 'B';
			}
			else if ((c & bit) != 0)
			{
				return 'C';
			}
			else if ((d & bit) != 0)
			{
				return 'D';
			}
			else
			{
				return '.';
			}
		}

		public Diagram Move(Point from, Point to) =>
			SetItem(to, GetItem(from))
			.SetItem(from, '.');

		private static Point Step(Point pt, int colFrom, int colTo)
		{
			return colFrom < colTo
				? pt.Right
				: pt.Left;
		}

		private static int BitFromPoint(Point point) =>
			(point.Row, point.Col) switch
			{
				(1, 1) => 1 << 0,
				(1, 2) => 1 << 1,
				(1, 3) => 1 << 2,
				(1, 4) => 1 << 3,
				(1, 5) => 1 << 4,
				(1, 6) => 1 << 5,
				(1, 7) => 1 << 6,
				(1, 8) => 1 << 7,
				(1, 9) => 1 << 8,
				(1, 10) => 1 << 9,
				(1, 11) => 1 << 10,

				(2, 3) => 1 << 11,
				(2, 5) => 1 << 12,
				(2, 7) => 1 << 13,
				(2, 9) => 1 << 14,

				(3, 3) => 1 << 15,
				(3, 5) => 1 << 16,
				(3, 7) => 1 << 17,
				(3, 9) => 1 << 18,

				(4, 3) => 1 << 19,
				(4, 5) => 1 << 20,
				(4, 7) => 1 << 21,
				(4, 9) => 1 << 22,

				(5, 3) => 1 << 23,
				(5, 5) => 1 << 24,
				(5, 7) => 1 << 25,
				(5, 9) => 1 << 26,

				_ => 1 << 31,
			};

		private Diagram SetItem(Point pt, char ch)
		{
			if (ch == '#')
			{
				return this;
			}

			var bit = BitFromPoint(pt);

			if (bit == 1 << 31)
			{
				return this;
			}

			return ch switch
			{
				'.' => new Diagram(
					a & ~bit,
					b & ~bit,
					c & ~bit,
					d & ~bit
				),
				'A' => new Diagram(a | bit, b & ~bit, c & ~bit, d & ~bit),
				'B' => new Diagram(a & ~bit, b | bit, c & ~bit, d & ~bit),
				'C' => new Diagram(a & ~bit, b & ~bit, c | bit, d & ~bit),
				'D' => new Diagram(a & ~bit, b & ~bit, c & ~bit, d | bit),
				_ => throw new InvalidOperationException()
			};
		}
	}
}
