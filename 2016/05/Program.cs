using System.Security.Cryptography;
using System.Text;

var input = "cxdnnyjw";

var password = new StringBuilder();

var i = 0;
while (password.Length < 8)
{
	var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input + i));

	var hashString = string.Join("", hash.Select(x => x.ToString("x2")));

	if (hashString.StartsWith("00000"))
	{
		password.Append(hashString[5]);
	}

	i++;
}

Console.WriteLine(password.ToString());

// Part 2
var password2 = new List<(char character, int position)>();

i = 0;
while (password2.Count < 8)
{
	var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input + i));

	var hashString = string.Join("", hash.Select(x => x.ToString("x2")));

	if (hashString.StartsWith("00000"))
	{
		var position = hashString[5];
		var character = hashString[6];

		if (position >= '0' && position <= '7')
		{
			var pos = position - '0';

			if (!password2.Any(p => p.position == pos))
			{
				password2.Add((character, pos));
			}
		}
	}

	i++;
}

password = new StringBuilder();
foreach (var c in password2.OrderBy(x => x.position))
{
	password.Append(c.character);
}

Console.WriteLine(password.ToString());
