using _09;

var lines = File.ReadAllLinesAsync("input.txt").Result;

var routes = new List<Route>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var from = parts[0];
	var to = parts[2];
	var distance = int.Parse(parts[4]);

	routes.Add(new Route { From = from, To = to, Distance = distance });
	routes.Add(new Route { From = to, To = from, Distance = distance });
}

var cities = routes.Select(r => r.From).Distinct().ToList();

var minDistance = int.MaxValue;

var queue = new Queue<(string city, int distance, List<string> visited)>();

foreach (var city in cities)
{
	queue.Enqueue((city, 0, new List<string>()));
}

while (queue.Count > 0)
{
	var (city, distance, visited) = queue.Dequeue();

	visited.Add(city);

	if (visited.Count == cities.Count)
	{
		minDistance = Math.Min(minDistance, distance);
		continue;
	}

	var nextRoutes = routes.Where(r => r.From == city && !visited.Contains(r.To));

	foreach (var nextRoute in nextRoutes)
	{
		queue.Enqueue((nextRoute.To, distance + nextRoute.Distance, new List<string>(visited)));
	}
}

Console.WriteLine(minDistance);


// Part 2
var maxDistance = 0;

queue = new Queue<(string city, int distance, List<string> visited)>();

foreach (var city in cities)
{
	queue.Enqueue((city, 0, new List<string>()));
}

while (queue.Count > 0)
{
	var (city, distance, visited) = queue.Dequeue();

	visited.Add(city);

	if (visited.Count == cities.Count)
	{
		maxDistance = Math.Max(maxDistance, distance);
		continue;
	}

	var nextRoutes = routes.Where(r => r.From == city && !visited.Contains(r.To));

	foreach (var nextRoute in nextRoutes)
	{
		queue.Enqueue((nextRoute.To, distance + nextRoute.Distance, new List<string>(visited)));
	}
}

Console.WriteLine(maxDistance);
