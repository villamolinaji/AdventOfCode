var lines = File.ReadAllLinesAsync("Input.txt").Result;

var mapping = (
	from line in lines
	let parts = line.Trim(')').Split(" (contains ")
	let ingredientsMapping = parts[0].Split(" ").ToHashSet()
	let allergensMapping = parts[1].Split(", ").ToHashSet()
	select (ingredientsMapping, allergensMapping))
	.ToArray();

var allergens = mapping.SelectMany(entry => entry.allergensMapping).ToHashSet();
var ingredients = mapping.SelectMany(entry => entry.ingredientsMapping).ToHashSet();

var ingredientsByAllergene = GetIngredientsByAllergene();

var suspiciousIngredients = ingredientsByAllergene.SelectMany(kvp => kvp.Value).ToHashSet();

var result = mapping
	.Select(entry => entry.ingredientsMapping.Count(ingredient => !suspiciousIngredients.Contains(ingredient)))
	.Sum();

Console.WriteLine(result);

// Part 2
while (ingredientsByAllergene.Values.Any(ingredients => ingredients.Count > 1))
{
	foreach (var allergen in allergens)
	{
		var candidates = ingredientsByAllergene[allergen];

		if (candidates.Count == 1)
		{
			foreach (var allergenT in allergens)
			{
				if (allergen != allergenT)
				{
					ingredientsByAllergene[allergenT].Remove(candidates.Single());
				}
			}
		}
	}
}

var result2 = string.Join(",", allergens.OrderBy(a => a).Select(a => ingredientsByAllergene[a].Single()));

Console.WriteLine(result2);


Dictionary<string, HashSet<string>> GetIngredientsByAllergene() =>
	   allergens.ToDictionary(
		   allergen => allergen,
		   allergen => mapping
			   .Where(entry => entry.allergensMapping.Contains(allergen))
			   .Aggregate(
				   ingredients as IEnumerable<string>,
				   (res, entry) => res.Intersect(entry.ingredientsMapping))
			   .ToHashSet());