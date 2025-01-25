var lines = File.ReadAllLinesAsync("Input.txt").Result;

var reactions =
	(
		from rule in lines
		let inout = rule.Split(" => ")
		let input = inout[0].Split(", ").Select(ParseChemical).ToArray()
		let output = ParseChemical(inout[1])
		select (output, input))
	.ToDictionary(inout => inout.output.chemical, inout => inout);

var oreForFuel = GetOreForFuel(1);

Console.WriteLine(oreForFuel);

// Part 2
var totalOre = 1000000000000L;

var fuel = 1L;
var totalFuel = 0L;

while (true)
{
	var newFuel = (int)((double)totalOre / GetOreForFuel(fuel) * fuel);

	if (newFuel == fuel)
	{
		totalFuel = newFuel;
		break;
	}

	fuel = newFuel;
}

Console.WriteLine(totalFuel);


(string chemical, long quantity) ParseChemical(string input)
{
	var parts = input.Split(" ");
	var quantity = long.Parse(parts[0]);
	var name = parts[1];

	return (name, quantity);
}

long GetOreForFuel(long fuel)
{
	var oreForFuel = 0L;
	var inventory = reactions.Keys.ToDictionary(chemical => chemical, _ => 0L);
	var productionList = new Queue<(string name, long quantity)>();

	productionList.Enqueue(("FUEL", fuel));

	while (productionList.Any())
	{
		var (chemical, quantity) = productionList.Dequeue();

		if (chemical == "ORE")
		{
			oreForFuel += quantity;
		}
		else
		{
			var reaction = reactions[chemical];

			var useFromInventory = Math.Min(quantity, inventory[chemical]);
			quantity -= useFromInventory;
			inventory[chemical] -= useFromInventory;

			if (quantity > 0)
			{
				var multiplier = (long)Math.Ceiling((decimal)quantity / reaction.output.quantity);
				inventory[chemical] = Math.Max(0, multiplier * reaction.output.quantity - quantity);

				foreach (var reagent in reaction.input)
				{
					productionList.Enqueue((reagent.chemical, reagent.quantity * multiplier));
				}
			}
		}
	}

	return oreForFuel;
}