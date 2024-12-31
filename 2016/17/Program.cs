using System.Security.Cryptography;
using System.Text;

var passcode = "ioramepc";

var open = "bcdef";

var paths = new List<string>();

var queue = new Queue<(int x, int y, string path)>();
queue.Enqueue((0, 0, ""));

while (queue.Count > 0)
{
	var (x, y, path) = queue.Dequeue();

	if (x == 3 && y == 3)
	{
		paths.Add(path);

		continue;
	}

	var hash = GetHash(path);

	if (y > 0 && open.Contains(hash[0]))
	{
		queue.Enqueue((x, y - 1, path + "U"));
	}
	if (y < 3 && open.Contains(hash[1]))
	{
		queue.Enqueue((x, y + 1, path + "D"));
	}
	if (x > 0 && open.Contains(hash[2]))
	{
		queue.Enqueue((x - 1, y, path + "L"));
	}
	if (x < 3 && open.Contains(hash[3]))
	{
		queue.Enqueue((x + 1, y, path + "R"));
	}
}

var first = paths.OrderBy(s => s.Length).First();

Console.WriteLine(first);

// Part 2
var longest = paths.OrderByDescending(s => s.Length).First();

Console.WriteLine(longest.Length);


string GetHash(string path)
{
	var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(passcode + path));

	return string.Join("", hash.Select(x => x.ToString("x2")));
}