using System.Text;

var input = "11100010111110100";
var diskLength = 272;

var dragonCurve = GetDragonCurve();

dragonCurve = dragonCurve.Substring(0, diskLength);

var checksum = GetChecksum(dragonCurve);

Console.WriteLine(checksum);

// Part 2
diskLength = 35651584;

dragonCurve = GetDragonCurve();

dragonCurve = dragonCurve.Substring(0, diskLength);

checksum = GetChecksum(dragonCurve);

Console.WriteLine(checksum);


string GetDragonCurve()
{
	var result = input;

	while (result.Length < diskLength)
	{
		result = GenerateDragonCurve(result);
	}

	return result;
}

string GenerateDragonCurve(string input)
{
	var a = input;
	var b = new string(a.Reverse().Select(c => c == '0' ? '1' : '0').ToArray());

	return a + "0" + b;
}

string GetChecksum(string input)
{
	var checksum = input;

	while (checksum.Length % 2 == 0)
	{
		checksum = GenerateChecksum(checksum);
	}

	return checksum;
}

string GenerateChecksum(string input)
{
	var checksum = new StringBuilder();

	for (int i = 0; i < input.Length; i += 2)
	{
		checksum.Append(input[i] == input[i + 1] ? '1' : '0');
	}

	return checksum.ToString();
}