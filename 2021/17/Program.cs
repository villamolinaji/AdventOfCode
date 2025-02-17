var targetAreaXMin = 94;
var targetAreaXMax = 151;
var targetAreaYMin = -156;
var targetAreaYMax = -103;

var highestY = Resolve().Max();

Console.WriteLine(highestY);

// Part 2
var countVelocities = Resolve().Count();

Console.WriteLine(countVelocities);


IEnumerable<int> Resolve()
{
	for (var vx = 0; vx <= targetAreaXMax; vx++)
	{
		for (var vy = targetAreaYMin; vy <= -targetAreaYMin; vy++)
		{
			var posX = 0;
			var posY = 0;
			var velocityX = vx;
			var velocityY = vy;

			var maxY = 0;

			while (posX <= targetAreaXMax &&
				posY >= targetAreaYMin)
			{

				posX += velocityX;
				posY += velocityY;

				velocityY -= 1;
				velocityX = Math.Max(0, velocityX - 1);

				maxY = Math.Max(posY, maxY);

				if (posX >= targetAreaXMin &&
					posX <= targetAreaXMax &&
					posY >= targetAreaYMin &&
					posY <= targetAreaYMax)
				{
					yield return maxY;

					break;
				}
			}
		}
	}
}