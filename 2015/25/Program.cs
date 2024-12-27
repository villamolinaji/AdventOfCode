int row = 2947;
int col = 3029;


int totalCells = 20000000;

long startValue = 20151125;

var calculatedValues = new List<long> { startValue };

for (int i = 1; i < totalCells; i++)
{
	var nextValue = (calculatedValues[calculatedValues.Count - 1] * 252533) % 33554393;

	calculatedValues.Add(nextValue);
}

var cells = new List<(int row, int col, long value)>();

var currentRow = 1;
var currentCol = 1;

foreach (var calculatedValue in calculatedValues)
{
	cells.Add((currentRow, currentCol, calculatedValue));

	if (currentRow == 1)
	{
		currentRow = currentCol + 1;
		currentCol = 1;
	}
	else
	{
		currentRow--;
		currentCol++;
	}
}

var result = cells.First(c => c.row == row && c.col == col).value;

Console.WriteLine(result);