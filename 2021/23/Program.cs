using _23;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var leastEnergy = Solve(lines);

Console.WriteLine(leastEnergy);

// Part 2
leastEnergy = Solve(FoldedUp(lines));

Console.WriteLine(leastEnergy);


int Solve(string[] input)
{
	var diagram = Diagram.Parse(input);

	var queue = new PriorityQueue<Diagram, int>();
	var cost = new Dictionary<Diagram, int>();

	queue.Enqueue(diagram, 0);
	cost.Add(diagram, 0);

	while (queue.Count > 0)
	{
		diagram = queue.Dequeue();

		if (diagram.IsFinished())
		{
			return cost[diagram];
		}

		foreach (var n in GetNeighbours(diagram))
		{
			if (cost[diagram] + n.cost < cost.GetValueOrDefault(n.maze, int.MaxValue))
			{
				cost[n.maze] = cost[diagram] + n.cost;

				queue.Enqueue(n.maze, cost[n.maze]);
			}
		}
	}

	throw new InvalidOperationException();
}

(Diagram maze, int cost) HallwayToRoom(Diagram maze)
{
	for (var col = 1; col < 12; col++)
	{
		var item = maze.GetItem(new Point(1, col));

		if (item == '.')
		{
			continue;
		}

		var colDistance = GetColDistance(item);

		if (maze.CanMoveToDoor(col, colDistance) &&
			maze.CanEnterRoom(item))
		{
			var steps = Math.Abs(colDistance - col);

			var point = new Point(1, colDistance);

			while (maze.GetItem(point.Down) == '.')
			{
				steps++;

				point = point.Down;
			}

			var hallwayToRoom = HallwayToRoom(maze.Move(new Point(1, col), point));

			return (hallwayToRoom.maze, hallwayToRoom.cost + steps * GetStepCost(item));
		}
	}

	return (maze, 0);
}

int GetStepCost(char amphipod)
{
	var cost = 0;

	switch (amphipod)
	{
		case 'A':
			cost = 1;
			break;
		case 'B':
			cost = 10;
			break;
		case 'C':
			cost = 100;
			break;
		case 'D':
			cost = 1000;
			break;
	}

	return cost;
}

int GetColDistance(char amphipod)
{
	var distance = 0;

	switch (amphipod)
	{
		case 'A':
			distance = 3;
			break;
		case 'B':
			distance = 5;
			break;
		case 'C':
			distance = 7;
			break;
		case 'D':
			distance = 9;
			break;
	}

	return distance;
}

IEnumerable<(Diagram maze, int cost)> GetNeighbours(Diagram maze)
{
	var hallwayToRoom = HallwayToRoom(maze);

	return hallwayToRoom.cost != 0
		? new[] { hallwayToRoom }
		: RoomToHallway(maze);
}

IEnumerable<(Diagram maze, int cost)> RoomToHallway(Diagram maze)
{
	var hallwayColumns = new int[] { 1, 2, 4, 6, 8, 10, 11 };

	foreach (var roomColumn in new[] { 3, 5, 7, 9 })
	{
		if (maze.IsColumnFinished(roomColumn))
		{
			continue;
		}

		var stepsVertical = 0;
		var pointFrom = new Point(1, roomColumn);

		while (maze.GetItem(pointFrom) == '.')
		{
			stepsVertical++;
			pointFrom = pointFrom.Down;
		}

		var item = maze.GetItem(pointFrom);

		if (item == '#')
		{
			continue;
		}

		foreach (var dc in new[] { -1, 1 })
		{
			var stepsHorizontal = 0;
			var pointTo = new Point(1, roomColumn);

			while (maze.GetItem(pointTo) == '.')
			{
				if (hallwayColumns.Contains(pointTo.Col))
				{
					yield return (maze.Move(pointFrom, pointTo), (stepsVertical + stepsHorizontal) * GetStepCost(item));
				}

				if (dc == -1)
				{
					pointTo = pointTo.Left;
				}
				else
				{
					pointTo = pointTo.Right;
				}

				stepsHorizontal++;
			}
		}
	}
}

string[] FoldedUp(string[] input)
{
	var lines = input.ToList();

	lines.Insert(3, "  #D#C#B#A#");
	lines.Insert(4, "  #D#B#A#C#");

	return lines.ToArray();
}
