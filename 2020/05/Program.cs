var lines = File.ReadAllLinesAsync("Input.txt").Result;

var highestSeatId = 0;

var visistedSeats = new HashSet<(int row, int col)>();

foreach (var line in lines)
{
	var row = GetRow(line.Substring(0, 7), 0, 127);

	var column = GetColumn(line.Substring(7), 0, 7);

	var seatId = row * 8 + column;

	visistedSeats.Add((row, column));

	if (seatId > highestSeatId)
	{
		highestSeatId = seatId;
	}
}

Console.WriteLine(highestSeatId);

// Part 2
var mySeatId = 0;
bool endLoop = false;

for (var row = 1; row < 127 && !endLoop; row++)
{
	for (var col = 0; col < 8; col++)
	{
		if (!visistedSeats.Contains((row, col)) &&
			visistedSeats.Contains((row, col - 1)) && visistedSeats.Contains((row, col + 1)))
		{
			mySeatId = row * 8 + col;

			endLoop = true;

			break;
		}
	}
}

Console.WriteLine(mySeatId);


int GetRow(string row, int start, int end)
{
	if (start == end)
	{
		return start;
	}

	var letter = row[0];

	row = row.Substring(1);

	if (letter == 'F')
	{
		end = (start + end) / 2;
	}
	else
	{
		start = (start + end) / 2 + 1;
	}

	return GetRow(row, start, end);
}

int GetColumn(string column, int start, int end)
{
	if (start == end)
	{
		return start;
	}

	var letter = column[0];

	column = column.Substring(1);

	if (letter == 'L')
	{
		end = (start + end) / 2;
	}
	else
	{
		start = (start + end) / 2 + 1;
	}

	return GetColumn(column, start, end);
}