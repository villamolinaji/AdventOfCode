using _22;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var nodes = new List<Node>();

foreach (var line in lines.Where(l => l.StartsWith("/dev")))
{
	var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

	var node = new Node
	{
		X = int.Parse(parts[0].Split("-")[1].Substring(1)),
		Y = int.Parse(parts[0].Split("-")[2].Substring(1)),
		Size = int.Parse(parts[1].Replace("T", "")),
		Used = int.Parse(parts[2].Replace("T", "")),
		UsePercent = int.Parse(parts[4].Replace("%", ""))
	};
	nodes.Add(node);
}

var viablePairs = 0;

foreach (var nodeA in nodes)
{
	foreach (var nodeB in nodes)
	{
		if (nodeA == nodeB)
		{
			continue;
		}

		if (nodeA.Used == 0)
		{
			continue;
		}

		if (nodeA.Used <= nodeB.Avail)
		{
			viablePairs++;
		}
	}
}

Console.WriteLine(viablePairs);

// Part 2
var nodesGrid = GetNodesGrid(nodes);

int maxX = nodes.Select(x => x.X).Max() + 1;

int moves = 0;

int emptyY = 0;
int emptyX = 0;
foreach (var node in nodes)
{
	if (node.Used == 0)
	{
		emptyY = node.Y;
		emptyX = node.X;
		break;
	}
}

while (emptyY != 0)
{
	if (!IsWall(emptyY - 1, emptyX))
	{
		Move(-1, 0);
	}
	else
	{
		Move(0, -1);
	}
}
while (emptyX != maxX - 1)
{
	Move(0, 1);
}
while (!nodesGrid[0, 0].Goal)
{
	Move(1, 0);
	Move(0, -1);
	Move(0, -1);
	Move(-1, 0);
	Move(0, 1);
}

Console.WriteLine(moves);

Node[,] GetNodesGrid(List<Node> nodes)
{
	int maxY = nodes.Select(x => x.Y).Max() + 1;
	int maxX = nodes.Select(x => x.X).Max() + 1;

	var nodesGrid = new Node[maxY, maxX];

	foreach (var file in nodes)
	{
		nodesGrid[file.Y, file.X] = file;
	}
	nodesGrid[0, maxX - 1].Goal = true;

	return nodesGrid;
}

bool IsWall(int irow, int icol)
{
	return nodesGrid[irow, icol].Used > nodesGrid[emptyY, emptyX].Size;
}

void Move(int dY, int dX)
{
	int nextY = emptyY + dY;
	int nextX = emptyX + dX;

	nodesGrid[emptyY, emptyX].Used = nodesGrid[nextY, nextX].Used;
	nodesGrid[emptyY, emptyX].Goal = nodesGrid[nextY, nextX].Goal;

	emptyY = nextY;
	emptyX = nextX;

	nodesGrid[emptyY, emptyX].Used = 0;
	nodesGrid[emptyY, emptyX].Goal = false;

	moves++;
}
