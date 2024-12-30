var lines = File.ReadAllLinesAsync("Input.txt").Result;

var countTriangles = 0;

foreach (var line in lines)
{
	var parts = line.Trim().Split(' ');

	while (parts.Contains(""))
	{
		parts = parts.Where(x => x != "").ToArray();
	}

	var a = int.Parse(parts[0]);
	var b = int.Parse(parts[1]);
	var c = int.Parse(parts[2]);

	if (IsTriangle(a, b, c))
	{
		countTriangles++;
	}
}

Console.WriteLine(countTriangles);


// Part 2
var numbersA = new List<int>();
var numbersB = new List<int>();
var numbersC = new List<int>();
foreach (var line in lines)
{
	var parts = line.Trim().Split(' ');

	while (parts.Contains(""))
	{
		parts = parts.Where(x => x != "").ToArray();
	}

	var a = int.Parse(parts[0]);
	var b = int.Parse(parts[1]);
	var c = int.Parse(parts[2]);

	numbersA.Add(a);
	numbersB.Add(b);
	numbersC.Add(c);
}

var indexNumbers = 0;

countTriangles = 0;

while (indexNumbers < numbersA.Count)
{
	var a1 = numbersA[indexNumbers];
	var a2 = numbersA[indexNumbers + 1];
	var a3 = numbersA[indexNumbers + 2];

	var b1 = numbersB[indexNumbers];
	var b2 = numbersB[indexNumbers + 1];
	var b3 = numbersB[indexNumbers + 2];

	var c1 = numbersC[indexNumbers];
	var c2 = numbersC[indexNumbers + 1];
	var c3 = numbersC[indexNumbers + 2];

	if (IsTriangle(a1, a2, a3))
	{
		countTriangles++;
	}
	if (IsTriangle(b1, b2, b3))
	{
		countTriangles++;
	}
	if (IsTriangle(c1, c2, c3))
	{
		countTriangles++;
	}

	indexNumbers += 3;
}

Console.WriteLine(countTriangles);


bool IsTriangle(int a, int b, int c)
{
	return a + b > c &&
		a + c > b &&
		b + c > a;
}