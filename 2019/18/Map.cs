namespace _18
{
	public class Map
	{
		public string[] Grid { get; set; } = new string[0];
		public Dictionary<char, (int, int)> PositionCache { get; set; } = new Dictionary<char, (int, int)>();
		public Dictionary<(char, char), int> DistanceCache { get; set; } = new Dictionary<(char, char), int>();
		public int Rows => this.Grid.Length;
		public int Cols => this.Grid[0].Length;

		public Map(string input) : this()
		{
			this.Grid = input.Split("\n");
		}

		public Map()
		{
		}
	}
}
