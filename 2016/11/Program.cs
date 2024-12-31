var floors = new List<List<string>>();
floors.Add(new List<string> { "PG", "TG", "TM", "UG", "RG", "RM", "CG", "CM" });
floors.Add(new List<string> { "PM", "UM" });
floors.Add(new List<string>());
floors.Add(new List<string>());

Console.WriteLine(GetMinSteps());

// Part 2
floors = new List<List<string>>();

floors.Add(new List<string> { "PG", "TG", "TM", "UG", "RG", "RM", "CG", "CM", "EG", "EM", "DG", "DM" });
floors.Add(new List<string> { "PM", "UM" });
floors.Add(new List<string>());
floors.Add(new List<string>());

Console.WriteLine(GetMinSteps());


int GetMinSteps()
{
	var minSteps = int.MaxValue;
	var queue = new PriorityQueue<(int elevator, List<List<string>> floors, int steps), int>();
	var visited = new HashSet<string>();

	queue.Enqueue((0, floors, 0), 0);

	while (queue.Count > 0)
	{
		var (elevator, floors, steps) = queue.Dequeue();

		if (steps >= minSteps)
		{
			continue;
		}

		if (floors[0].Count == 0 &&
			floors[1].Count == 0 &&
			floors[2].Count == 0 &&
			elevator == 3)
		{
			minSteps = steps;
			continue;
		}

		var state = GetStateHash(elevator, floors);
		if (!visited.Add(state))
		{
			continue;
		}

		var currentFloorItems = floors[elevator];

		for (int i = 0; i < currentFloorItems.Count; i++)
		{
			for (int j = i; j < currentFloorItems.Count; j++)
			{
				foreach (var e in new[] { -1, 1 })
				{
					var newElevator = elevator + e;
					if (newElevator < 0 || newElevator >= floors.Count)
					{
						continue;
					}

					var newFloors = floors.Select(list => new List<string>(list)).ToList();
					var newFloor = newFloors[newElevator];
					var oldFloor = newFloors[elevator];

					newFloor.Add(currentFloorItems[i]);
					oldFloor.Remove(currentFloorItems[i]);

					if (i != j)
					{
						newFloor.Add(currentFloorItems[j]);
						oldFloor.Remove(currentFloorItems[j]);
					}

					if (IsValidFloor(newFloors))
					{
						int estimatedCost = steps + 1 + Heuristic(newFloors);
						queue.Enqueue((newElevator, newFloors, steps + 1), estimatedCost);
					}
				}
			}
		}
	}

	return minSteps;
}


bool IsValidFloor(List<List<string>> floors)
{
	foreach (var floor in floors)
	{
		var chips = floor
			.Where(item => item.EndsWith('M'))
			.Select(item => item[0])
			.ToList();

		var generators = floor
			.Where(item => item.EndsWith('G'))
			.Select(item => item[0])
			.ToList();

		if (generators.Count > 0)
		{
			foreach (var chip in chips)
			{
				if (!generators.Contains(chip))
				{
					return false;
				}
			}
		}
	}

	return true;
}

string GetStateHash(int elevator, List<List<string>> floors)
{
	return $"{elevator}:{string.Join("|", floors.Select(f => string.Join("", f.OrderBy(x => x))))}";
}

int Heuristic(List<List<string>> floors)
{
	int cost = 0;

	for (int i = 0; i < floors.Count - 1; i++)
	{
		cost += floors[i].Count * (3 - i);
	}

	return cost / 2;
}