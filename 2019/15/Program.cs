using System.Collections.Immutable;
using _15;

var input = File.ReadAllTextAsync("Input.txt").Result;

(int dx, int dy)[] directions = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) };

var program = input.Split(',').Select(long.Parse).ToArray();

var computer = new IntcodeComputer(program);

var minSteps = Bfs(computer).First(s => s.output == 2).path.Count;

Console.WriteLine(minSteps);

// Part 2
computer = new IntcodeComputer(program);

var computer2 = Bfs(computer).First(s => s.output == 2).computer;

var totalSteps = Bfs(computer2).Last().path.Count;

Console.WriteLine(totalSteps);


IEnumerable<(IntcodeComputer computer, ImmutableList<int> path, int output)> Bfs(IntcodeComputer startComputer)
{
	var visited = new HashSet<(int x, int y)> { (0, 0) };

	var queue = new Queue<(IntcodeComputer computer, ImmutableList<int> path, int x, int y)>();

	queue.Enqueue((startComputer, ImmutableList<int>.Empty, 0, 0));

	while (queue.Count > 0)
	{
		var current = queue.Dequeue();

		for (var i = 0; i < directions.Length; i++)
		{
			var (nextX, nextY) = (current.x + directions[i].dx, current.y + directions[i].dy);

			if (!visited.Contains((nextX, nextY)))
			{
				visited.Add((nextX, nextY));

				var nextPath = current.path.Add(i + 1);

				var nextComputer = current.computer.Clone();

				nextComputer.Run(i + 1);

				var output = (int)nextComputer.Output.Dequeue();

				if (output != 0)
				{
					queue.Enqueue((nextComputer, nextPath, nextX, nextY));

					yield return (nextComputer, nextPath, output);
				}
			}
		}
	}
}