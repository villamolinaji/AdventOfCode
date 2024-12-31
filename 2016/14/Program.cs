using System.Security.Cryptography;
using System.Text;

var input = "ihaygndm";

var hashes = new Dictionary<(int index, int times), string>();

var index = 0;
var keyFounds = 0;
var times = 0;

while (keyFounds < 64)
{
	var hashString = GenerateHash(index, 0);

	if (IsValidHash(hashString, index, times))
	{
		keyFounds++;
	}

	index++;
}

Console.WriteLine(index - 1);

// Part 2
index = 0;
keyFounds = 0;
times = 2016;

while (keyFounds < 64)
{
	var hashString = GenerateHash(index, times);

	if (IsValidHash(hashString, index, times))
	{
		keyFounds++;
	}

	index++;
}

Console.WriteLine(index - 1);


bool IsValidHash(string hash, int index, int times)
{
	var triplet = FindTriplet(hash);
	if (triplet != null)
	{
		string quintuplet = new string((char)triplet, 5);

		for (int i = 1; i <= 1000; i++)
		{
			var hashString2 = GenerateHash(index + i, times);

			if (hashString2.Contains(quintuplet))
			{
				return true;
			}
		}
	}

	return false;
}

char? FindTriplet(string hash)
{
	for (int i = 0; i < hash.Length - 2; i++)
	{
		if (hash[i] == hash[i + 1] &&
			hash[i] == hash[i + 2])
		{
			return hash[i];
		}
	}

	return null;
}

string GenerateHash(int index, int times)
{
	if (hashes.ContainsKey((index, times)))
	{
		return hashes[(index, times)];
	}

	var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input + index));
	var hashString = string.Join("", hash.Select(x => x.ToString("x2")));

	for (int i = 0; i < times; i++)
	{
		hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(hashString));
		hashString = string.Join("", hash.Select(x => x.ToString("x2")));
	}

	hashes[(index, times)] = hashString;

	return hashString;
}