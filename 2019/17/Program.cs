using System.Collections.Immutable;
using System.Text;
using _17;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var computer = new IntcodeComputer(program);

computer.Run(0);

var map = GetMap();

var rows = map.Length;
var cols = map[0].Length;
var cross = ".#.\n###\n.#.".Split("\n");

bool IsCross(int irow, int icol) =>
	(
		from drow in new[] { -1, 0, 1 }
		from dcol in new[] { -1, 0, 1 }
		select cross[1 + drow][1 + dcol] == map[irow + drow][icol + dcol]
	)
	.All(x => x);

var result = (
	from irow in Enumerable.Range(1, rows - 2)
	from icol in Enumerable.Range(1, cols - 2)
	where IsCross(irow, icol)
	select icol * irow
).Sum();

Console.WriteLine(result);

// Part 2
var path = GetPath();
var runCommand = GetRunCommand(path);

var runCommandArray = runCommand.Select(c => (long)c).ToArray();

program[0] = 2;
computer = new IntcodeComputer(program);
computer.Input.Clear();
computer.Output.Clear();

computer.Run(runCommandArray);

var result2 = computer.Output.Last();

Console.WriteLine(result2);


string[] GetMap()
{
	var map = new StringBuilder();

	while (computer.Output.Count > 0)
	{
		var output = (char)computer.Output.Dequeue();

		map.Append(output);
	}

	return map.ToString().Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
}

string GetRunCommand(string path)
{
	foreach (var (indices, functions) in GenerateRec(path, ImmutableList<string>.Empty))
	{
		var compressed = functions.Select(Compress).ToArray();

		if (indices.Count <= 20 &&
			compressed.All(c => c.Length <= 20))
		{
			var main = string.Join(",", indices.Select(i => "ABC"[i]));

			return $"{main}\n{compressed[0]}\n{compressed[1]}\n{compressed[2]}\nn\n";
		}
	}

	return string.Empty;
}

IEnumerable<(ImmutableList<int> indices, ImmutableList<string> functions)> GenerateRec(string path, ImmutableList<string> functions)
{
	if (path.Length == 0)
	{
		yield return (ImmutableList<int>.Empty, functions);
	}

	for (var i = 0; i < functions.Count; i++)
	{
		var function = functions[i];

		if (path.StartsWith(function))
		{

			var pathT = path.Substring(function.Length);

			foreach (var res in GenerateRec(pathT, functions))
			{
				yield return (res.indices.Insert(0, i), res.functions);
			}
		}
	}

	if (functions.Count < 3)
	{
		for (var length = 1; length <= path.Length; length++)
		{
			var function = path[0..length].ToString();
			var functionsT = functions.Add(function);
			var pathT = path.Substring(function.Length);

			foreach (var res in GenerateRec(pathT, functionsT))
			{
				yield return (res.indices.Insert(0, functions.Count), res.functions);
			}
		}
	}
}

string Compress(string st)
{
	var steps = new List<string>();
	var l = 0;

	for (var i = 0; i < st.Length; i++)
	{
		var ch = st[i];

		if (l > 0 && ch != 'F')
		{
			steps.Add(l.ToString());
			l = 0;
		}

		if (ch == 'R' || ch == 'L')
		{
			steps.Add(ch.ToString());
		}

		else
		{
			l++;
		}
	}

	if (l > 0)
	{
		steps.Add(l.ToString());
	}

	return string.Join(",", steps);
}

string GetPath()
{
	var (pos, dir) = FindRobot();

	var path = new StringBuilder();
	var finished = false;

	while (!finished)
	{
		finished = true;

		foreach (var (nextDir, step) in new[]
		{
			((drow:  dir.drow, dcol:  dir.dcol), "F"),
			((drow: -dir.dcol, dcol:  dir.drow), "LF"),
			((drow:  dir.dcol, dcol: -dir.drow), "RF")
		})
		{
			var nextPos = (pos.irow + nextDir.drow, pos.icol + nextDir.dcol);

			if (GetMapPosition(nextPos) == '#')
			{
				path.Append(step);
				pos = nextPos;
				dir = nextDir;
				finished = false;

				break;
			}
		}
	}

	return path.ToString();
}

char GetMapPosition((int irow, int icol) pos)
{
	var (irow, icol) = pos;

	return irow < 0 || irow >= rows || icol < 0 || icol >= cols
		? '.'
		: map[irow][icol];
}

((int irow, int icol) pos, (int drow, int dcol) dir) FindRobot() => (
		from irow in Enumerable.Range(0, rows)
		from icol in Enumerable.Range(0, cols)
		let ch = map[irow][icol]
		where "^v<>".Contains(ch)
		let dir = map[irow][icol] switch
		{
			'^' => (-1, 0),
			'v' => (1, 0),
			'<' => (0, -1),
			'>' => (0, 1),
			_ => throw new InvalidOperationException("Robot not found")
		}
		select ((irow, icol), dir)
	).First();
