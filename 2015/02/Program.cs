string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var totalPaper = 0;
var totalRibbon = 0;

foreach (var line in lines)
{
	var parts = line.Split('x');

	var l = int.Parse(parts[0]);
	var w = int.Parse(parts[1]);
	var h = int.Parse(parts[2]);

	var side1 = l * w;
	var side2 = w * h;
	var side3 = l * h;

	var minSide = Math.Min(side1, Math.Min(side2, side3));

	totalPaper += 2 * side1 + 2 * side2 + 2 * side3 + minSide;

	var minPerimeter = 2 * Math.Min(l + w, Math.Min(w + h, l + h));

	var volume = l * w * h;

	totalRibbon += minPerimeter + volume;

}

Console.WriteLine(totalPaper);
Console.WriteLine(totalRibbon);