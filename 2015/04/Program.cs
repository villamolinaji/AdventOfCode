using System.Security.Cryptography;
using System.Text;

var secretKey = "iwrupvqb";

Console.WriteLine(FindHash(secretKey, "00000"));

Console.WriteLine(FindHash(secretKey, "000000"));


int FindHash(string secretKey, string prefix)
{
	int i = 0;

	while (true)
	{
		i++;

		var newKey = secretKey + i.ToString();

		var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(newKey));

		var hashString = string.Join("", hash.Select(b => b.ToString("x2")));

		if (hashString.ToString()!.StartsWith(prefix))
		{
			return i;
		}
	}
}