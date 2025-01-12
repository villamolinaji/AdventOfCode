var numRecipes = 409551;

var recipes = new List<int> { 3, 7 };
var elf1 = 0;
var elf2 = 1;

while (recipes.Count < numRecipes + 10)
{
	AddNewRecipes();
}

var result = string.Join("", recipes.Skip(numRecipes).Take(10));

Console.WriteLine(result);

// Part 2
var target = numRecipes.ToString();
var targetLength = target.Length;

recipes = new List<int> { 3, 7 };
elf1 = 0;
elf2 = 1;

while (true)
{
	AddNewRecipes();

	if (recipes.Count >= targetLength)
	{
		string last = string.Join("", recipes.GetRange(recipes.Count - targetLength, targetLength));
		if (last == target)
		{
			break;
		}
	}

	if (recipes.Count > targetLength)
	{
		string secondLast = string.Join("", recipes.GetRange(recipes.Count - targetLength - 1, targetLength));
		if (secondLast == target)
		{
			break;
		}
	}
}

var result2 = recipes.Count - target.Length - 1;

Console.WriteLine(result2);


void AddNewRecipes()
{
	var sum = recipes[elf1] + recipes[elf2];

	if (sum >= 10)
	{
		recipes.Add(1);
		recipes.Add(sum - 10);
	}
	else
	{
		recipes.Add(sum);
	}

	elf1 = (elf1 + 1 + recipes[elf1]) % recipes.Count;
	elf2 = (elf2 + 1 + recipes[elf2]) % recipes.Count;
}