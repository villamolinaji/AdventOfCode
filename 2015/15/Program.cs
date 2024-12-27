using _15;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var ingredients = new List<Ingredient>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var name = parts[0];
	var capacity = parts[2].Replace(",", "");
	var durability = parts[4].Replace(",", "");
	var flavor = parts[6].Replace(",", "");
	var texture = parts[8].Replace(",", "");
	var calories = parts[10];

	ingredients.Add(new Ingredient
	{
		Name = name,
		Capacity = int.Parse(capacity),
		Durability = int.Parse(durability),
		Flavor = int.Parse(flavor),
		Texture = int.Parse(texture),
		Calories = int.Parse(calories)
	});
}

const int TeaSpoons = 100;

int maxScore = maxScore = GetScore(false);

Console.WriteLine(maxScore);

// Part 2
maxScore = GetScore(true);

Console.WriteLine(maxScore);


int GetScore(bool isPart2)
{
	int maxScore = 0;

	for (int i = 0; i < TeaSpoons; i++)
	{
		for (int j = 0; j < TeaSpoons; j++)
		{
			for (int k = 0; k < TeaSpoons; k++)
			{
				for (int l = 0; l < TeaSpoons; l++)
				{
					if (i + j + k + l == TeaSpoons)
					{
						var capacity = ingredients[0].Capacity * i + ingredients[1].Capacity * j + ingredients[2].Capacity * k + ingredients[3].Capacity * l;
						var durability = ingredients[0].Durability * i + ingredients[1].Durability * j + ingredients[2].Durability * k + ingredients[3].Durability * l;
						var flavor = ingredients[0].Flavor * i + ingredients[1].Flavor * j + ingredients[2].Flavor * k + ingredients[3].Flavor * l;
						var texture = ingredients[0].Texture * i + ingredients[1].Texture * j + ingredients[2].Texture * k + ingredients[3].Texture * l;
						var calories = ingredients[0].Calories * i + ingredients[1].Calories * j + ingredients[2].Calories * k + ingredients[3].Calories * l;

						if (capacity < 0 || durability < 0 || flavor < 0 || texture < 0)
						{
							continue;
						}

						if (isPart2 && calories != 500)
						{
							continue;
						}

						var score = capacity * durability * flavor * texture;

						if (score > maxScore)
						{
							maxScore = score;
						}
					}

				}
			}
		}
	}

	return maxScore;
}