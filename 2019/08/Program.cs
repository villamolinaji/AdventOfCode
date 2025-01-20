using System.Text;

var input = File.ReadAllTextAsync("Input.txt").Result;

var wide = 25;
var tall = 6;

var layers = new List<string>();

var index = 0;


while (index < input.Length)
{
	var layer = input.Substring(index, wide * tall);
	layers.Add(layer);

	index += wide * tall;
}

var minLayer = layers.OrderBy(x => x.Count(y => y == '0')).First();

var result = minLayer.Count(x => x == '1') * minLayer.Count(x => x == '2');

Console.WriteLine(result);

// Part 2
var finalImage = new StringBuilder();

for (int i = 0; i < wide * tall; i++)
{
	var pixel = '2';

	foreach (var layer in layers)
	{
		if (layer[i] != '2')
		{
			pixel = layer[i];
			break;
		}
	}

	finalImage.Append(pixel);
}

for (int i = 0; i < tall; i++)
{
	Console.WriteLine(finalImage.ToString().Substring(i * wide, wide).Replace('0', ' ').Replace('1', '█'));
}
