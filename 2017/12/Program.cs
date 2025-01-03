var lines = File.ReadAllLinesAsync("Input.txt").Result;

var programConnections = new HashSet<(int, int)>();

foreach (var line in lines)
{
	var parts = line.Split(' ');
	var program = int.Parse(parts[0]);
	var connections = parts.Skip(2).Select(p => int.Parse(p.TrimEnd(',')));

	foreach (var connection in connections)
	{
		programConnections.Add((program, connection));
		programConnections.Add((connection, program));
	}
}

var groupPrograms = ProgramsInGroup(0);
Console.WriteLine(groupPrograms.Count);

// Part 2
var groups = new HashSet<string>();
foreach (var program in programConnections.Select(c => c.Item1).Distinct())
{
	var group = string.Join(",", ProgramsInGroup(program).OrderBy(p => p));
	groups.Add(group);
}

Console.WriteLine(groups.Count);


HashSet<int> ProgramsInGroup(int program)
{
	var programs = new HashSet<int>();
	var queue = new Queue<int>();

	queue.Enqueue(program);

	while (queue.Count > 0)
	{
		var currentProgram = queue.Dequeue();

		programs.Add(currentProgram);

		foreach (var connection in programConnections.Where(c => c.Item1 == currentProgram && !programs.Contains(c.Item2)))
		{
			queue.Enqueue(connection.Item2);
		}
	}

	return programs;
}
