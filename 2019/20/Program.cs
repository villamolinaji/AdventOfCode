string[] lines = File.ReadAllLinesAsync("Input.txt").Result;

var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

int rows = lines.Length;
int cols = lines[0].Length;
int[,] grid = new int[rows, cols];
var portals = new List<(string label, (int row, int col) position)>();

for (int r = 0; r < rows; r++)
{
	for (int c = 0; c < cols; c++)
	{
		if (lines[r][c] == '.')
		{
			if (char.IsLetter(lines[r - 1][c]))
			{
				portals.Add((lines[r - 2][c].ToString() + lines[r - 1][c], (r, c)));
			}
			else if (char.IsLetter(lines[r + 1][c]))
			{
				portals.Add((lines[r + 1][c].ToString() + lines[r + 2][c], (r, c)));
			}
			else if (char.IsLetter(lines[r][c - 1]))
			{
				portals.Add((lines[r][c - 2].ToString() + lines[r][c - 1], (r, c)));
			}
			else if (char.IsLetter(lines[r][c + 1]))
			{
				portals.Add((lines[r][c + 1].ToString() + lines[r][c + 2], (r, c)));
			}
		}
		else
		{
			grid[r, c] = 1;
		}
	}
}

var connections = new Dictionary<(int row, int col), (int row, int col)>();
int startRow = 0;
int startCol = 0;
int endRow = 0;
int endCol = 0;

for (int i = 0; i < portals.Count; i++)
{
	if (portals[i].label == "AA")
	{
		(startRow, startCol) = portals[i].position;
	}
	else if (portals[i].label == "ZZ")
	{
		(endRow, endCol) = portals[i].position;
	}
	else
	{
		for (int j = i + 1; j < portals.Count; j++)
		{
			if (portals[i].label == portals[j].label)
			{
				connections[portals[i].position] = portals[j].position;
				connections[portals[j].position] = portals[i].position;
			}
		}
	}
}

var minSteps = 0;

Solve(false);

Console.WriteLine(minSteps);

// Part 2
minSteps = 0;

Solve(true);

Console.WriteLine(minSteps);


void Solve(bool isPart2)
{
	var pq = new PriorityQueue<(int, int, int, int), int>();
	HashSet<(int, int, int)> visited = new HashSet<(int, int, int)>();
	pq.Enqueue((0, startRow, startCol, 0), 0);

	while (pq.Count > 0)
	{
		var (steps, row, col, level) = pq.Dequeue();

		if (visited.Contains((row, col, level)))
		{
			continue;
		}
		visited.Add((row, col, level));

		if (row == endRow
			&& col == endCol &&
			(level == 0 || !isPart2))
		{
			minSteps = steps;

			break;
		}

		if (connections.ContainsKey((row, col)))
		{
			var (newRow, newCol) = connections[(row, col)];

			if (isPart2)
			{
				bool goingIn = 3 <= col &&
					col < cols - 3 &&
					3 <= row &&
					row < rows - 3;

				if (level > 0 || goingIn)
				{
					pq.Enqueue((steps + 1, newRow, newCol, level - 1 + 2 * (goingIn ? 1 : 0)), steps + 1);
				}
			}
			else
			{
				pq.Enqueue((steps + 1, newRow, newCol, level), steps + 1);
			}
		}

		foreach (var (dr, dc) in directions)
		{
			var newRow = row + dr;
			var newCol = col + dc;

			if (grid[newRow, newCol] == 0)
			{
				pq.Enqueue((steps + 1, newRow, newCol, level), steps + 1);
			}
		}
	}
}
