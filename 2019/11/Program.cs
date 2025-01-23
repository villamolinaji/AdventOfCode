using System.Text;
using _11;

var input = File.ReadAllTextAsync("Input.txt").Result;

var result = Run(0);

Console.WriteLine(result.Count);

// Part 2
result = Run(1);

var irowMin = result.Keys.Select(pos => pos.irow).Min();
var icolMin = result.Keys.Select(pos => pos.icol).Min();
var irowMax = result.Keys.Select(pos => pos.irow).Max();
var icolMax = result.Keys.Select(pos => pos.icol).Max();

var crow = irowMax - irowMin + 1;
var ccol = icolMax - icolMin + 1;

var st = new StringBuilder();

for (var irow = 0; irow < crow; irow++)
{
	for (var icol = 0; icol < ccol; icol++)
	{
		st.Append(" #"[result.GetValueOrDefault((irowMin + irow, icolMin + icol), 0)]);
	}

	st.AppendLine();
}

Console.WriteLine(st.ToString());


Dictionary<(int irow, int icol), int> Run(int startColor)
{
	var mtx = new Dictionary<(int irow, int icol), int>();
	(int irow, int icol) pos = (0, 0);
	(int drow, int dcol) dir = (-1, 0);

	mtx[(0, 0)] = startColor;

	var program = input.Split(',').Select(long.Parse).ToArray();

	var computer = new IntcodeComputer(program);

	while (true)
	{
		computer.Run(mtx.GetValueOrDefault(pos, 0));

		if (computer.IsHalt)
		{
			return mtx;
		}

		mtx[pos] = (int)computer.Output.Dequeue();

		dir = computer.Output.Dequeue() switch
		{
			0 => (-dir.dcol, dir.drow),
			1 => (dir.dcol, -dir.drow),
			_ => throw new ArgumentException("")
		};

		pos = (pos.irow + dir.drow, pos.icol + dir.dcol);
	}
}