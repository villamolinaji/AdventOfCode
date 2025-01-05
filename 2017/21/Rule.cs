namespace _21
{
	public class Rule
	{
		required public List<List<char>> From { get; set; }

		required public List<List<char>> To { get; set; }

		public string FromString => string.Join("/", From.Select(x => string.Join("", x)));
	}
}
