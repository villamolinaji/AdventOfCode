namespace _19
{
	public class Scanner
	{
		public (int x, int y, int z) Center { get; set; }

		public int Rotation { get; set; }

		public List<(int x, int y, int z)> Beacons { get; set; }

		public Scanner()
		{
			Beacons = new List<(int x, int y, int z)>();
		}

		public Scanner((int x, int y, int z) coordinates, int rotation, List<(int x, int y, int z)> beacons)
		{
			Center = coordinates;
			Rotation = rotation;
			Beacons = beacons;
		}

		public Scanner Rotate() => new Scanner(Center, Rotation + 1, Beacons);

		public Scanner Translate((int x, int y, int z) t) => new Scanner((Center.x + t.x, Center.y + t.y, Center.z + t.z), Rotation, Beacons);

		public (int x, int y, int z) Transform((int x, int y, int z) coordinates)
		{
			var x = coordinates.x;
			var y = coordinates.y;
			var z = coordinates.z;

			switch (Rotation % 6)
			{
				case 0:
					break;
				case 1:
					x = -coordinates.x;
					z = -coordinates.z;
					break;
				case 2:
					x = coordinates.y;
					y = -coordinates.x;
					break;
				case 3:
					x = -coordinates.y;
					y = coordinates.x;
					break;
				case 4:
					x = coordinates.z;
					z = -coordinates.x;
					break;
				case 5:
					x = -coordinates.z;
					z = coordinates.x;
					break;
			}

			var auxY = y;
			switch ((Rotation / 6) % 4)
			{
				case 0:
					break;
				case 1:
					y = -z;
					z = auxY;
					break;
				case 2:
					y = -y;
					z = -z;
					break;
				case 3:
					y = z;
					z = -auxY;
					break;
			}


			return (Center.x + x, Center.y + y, Center.z + z);
		}

		public IEnumerable<(int x, int y, int z)> GetBeaconsInWorld()
		{
			return Beacons.Select(Transform);
		}
	}
}
