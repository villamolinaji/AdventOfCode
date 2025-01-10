using System.Text;
using _10;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var points = new List<Point>();

foreach (var line in lines)
{
	var parts = line.Split(new[] { ' ', ',', '<', '>', '>', ':' }, StringSplitOptions.RemoveEmptyEntries);

	points.Add(new Point
	{
		X = int.Parse(parts[1]),
		Y = int.Parse(parts[2]),
		VelocityX = int.Parse(parts[4]),
		VelocityY = int.Parse(parts[5])
	});
}

int seconds = 0;

long area = long.MaxValue;

while (true)
{
	var movedPoints = MovePoints(true);

	long areaNew = movedPoints.width * movedPoints.height;

	if (areaNew > area)
	{
		movedPoints = MovePoints(false);

		var printScreen = new StringBuilder();

		for (var irow = 0; irow < movedPoints.height; irow++)
		{
			for (var icol = 0; icol < movedPoints.width; icol++)
			{
				printScreen.Append(points.Any(p => p.X - movedPoints.left == icol && p.Y - movedPoints.top == irow) ? '#' : ' ');
			}

			printScreen.AppendLine();
		}

		Console.WriteLine(printScreen.ToString());

		Console.WriteLine(seconds);

		break;
	}

	area = areaNew;
}


(int left, int top, long width, long height) MovePoints(bool forward)
{
	foreach (var point in points)
	{
		if (forward)
		{
			point.X += point.VelocityX;
			point.Y += point.VelocityY;
		}
		else
		{
			point.X -= point.VelocityX;
			point.Y -= point.VelocityY;
		}
	}

	seconds += forward ? 1 : -1;

	var minX = points.Min(pt => pt.X);
	var maxX = points.Max(pt => pt.X);
	var minY = points.Min(pt => pt.Y);
	var maxY = points.Max(pt => pt.Y);

	return (minX, minY, maxX - minX + 1, maxY - minY + 1);
};
