using _16;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var targetChildren = 3;
var targetCats = 7;
var targetSamoyeds = 2;
var targetPomeranians = 3;
var targetAkitas = 0;
var targetVizslas = 0;
var targetGoldfish = 5;
var targetTrees = 3;
var targetCars = 2;
var targetPerfumes = 1;

var aunts = new List<Aunt>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var number = parts[1].Replace(":", "");
	var children = targetChildren;
	var cats = targetCats;
	var samoyeds = targetSamoyeds;
	var pomeranians = targetPomeranians;
	var akitas = targetAkitas;
	var vizslas = targetVizslas;
	var goldfish = targetGoldfish;
	var trees = targetTrees;
	var cars = targetCars;
	var perfumes = targetPerfumes;

	for (int i = 2; i < parts.Length; i += 2)
	{
		var property = parts[i].Replace(":", "");
		var value = int.Parse(parts[i + 1].Replace(",", ""));

		switch (property)
		{
			case "children":
				children = value;
				break;
			case "cats":
				cats = value;
				break;
			case "samoyeds":
				samoyeds = value;
				break;
			case "pomeranians":
				pomeranians = value;
				break;
			case "akitas":
				akitas = value;
				break;
			case "vizslas":
				vizslas = value;
				break;
			case "goldfish":
				goldfish = value;
				break;
			case "trees":
				trees = value;
				break;
			case "cars":
				cars = value;
				break;
			case "perfumes":
				perfumes = value;
				break;
		}
	}

	aunts.Add(new Aunt
	{
		Number = int.Parse(number),
		Children = children,
		Cats = cats,
		Samoyeds = samoyeds,
		Pomeranians = pomeranians,
		Akitas = akitas,
		Vizslas = vizslas,
		Goldfish = goldfish,
		Trees = trees,
		Cars = cars,
		Perfumes = perfumes
	});
}

var auntNumber = aunts
	.FirstOrDefault(a => a.Children == targetChildren &&
		a.Cats == targetCats &&
		a.Samoyeds == targetSamoyeds &&
		a.Pomeranians == targetPomeranians &&
		a.Akitas == targetAkitas &&
		a.Vizslas == targetVizslas &&
		a.Goldfish == targetGoldfish &&
		a.Trees == targetTrees &&
		a.Cars == targetCars &&
		a.Perfumes == targetPerfumes)?
	.Number;

Console.WriteLine(auntNumber);

// Part 2
foreach (var line in lines)
{
	var parts = line.Split(' ');
	var number = parts[1].Replace(":", "");
	var children = targetChildren;
	var cats = targetCats + 1;
	var samoyeds = targetSamoyeds;
	var pomeranians = targetPomeranians - 1;
	var akitas = targetAkitas;
	var vizslas = targetVizslas;
	var goldfish = targetGoldfish - 1;
	var trees = targetTrees + 1;
	var cars = targetCars;
	var perfumes = targetPerfumes;

	for (int i = 2; i < parts.Length; i += 2)
	{
		var property = parts[i].Replace(":", "");
		var value = int.Parse(parts[i + 1].Replace(",", ""));

		switch (property)
		{
			case "children":
				children = value;
				break;
			case "cats":
				cats = value;
				break;
			case "samoyeds":
				samoyeds = value;
				break;
			case "pomeranians":
				pomeranians = value;
				break;
			case "akitas":
				akitas = value;
				break;
			case "vizslas":
				vizslas = value;
				break;
			case "goldfish":
				goldfish = value;
				break;
			case "trees":
				trees = value;
				break;
			case "cars":
				cars = value;
				break;
			case "perfumes":
				perfumes = value;
				break;
		}
	}

	aunts.Add(new Aunt
	{
		Number = int.Parse(number),
		Children = children,
		Cats = cats,
		Samoyeds = samoyeds,
		Pomeranians = pomeranians,
		Akitas = akitas,
		Vizslas = vizslas,
		Goldfish = goldfish,
		Trees = trees,
		Cars = cars,
		Perfumes = perfumes
	});
}

auntNumber = aunts
	.FirstOrDefault(a => a.Children == targetChildren &&
		a.Cats > targetCats &&
		a.Samoyeds == targetSamoyeds &&
		a.Pomeranians < targetPomeranians &&
		a.Akitas == targetAkitas &&
		a.Vizslas == targetVizslas &&
		a.Goldfish < targetGoldfish &&
		a.Trees > targetTrees &&
		a.Cars == targetCars &&
		a.Perfumes == targetPerfumes)?
	.Number;

Console.WriteLine(auntNumber);