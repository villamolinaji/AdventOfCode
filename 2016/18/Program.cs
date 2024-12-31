using System.Text;

var firstRow = "^^.^..^.....^..^..^^...^^.^....^^^.^.^^....^.^^^...^^^^.^^^^.^..^^^^.^^.^.^.^.^.^^...^^..^^^..^.^^^^";

var totalRows = 40;

Console.WriteLine(GetSafeTiles());

// Part 2
totalRows = 400000;

Console.WriteLine(GetSafeTiles());


long GetSafeTiles()
{
	var rows = new List<string> { firstRow };

	while (rows.Count < totalRows)
	{
		var lastRow = rows[rows.Count - 1];
		var newRow = new StringBuilder();

		for (var i = 0; i < lastRow.Length; i++)
		{
			var left = i == 0
				? '.'
				: lastRow[i - 1];

			var center = lastRow[i];

			var right = i == lastRow.Length - 1
				? '.'
				: lastRow[i + 1];

			if (left == '^' && center == '^' && right == '.')
			{
				newRow.Append('^');
			}
			else if (left == '.' && center == '^' && right == '^')
			{
				newRow.Append('^');
			}
			else if (left == '^' && center == '.' && right == '.')
			{
				newRow.Append('^');
			}
			else if (left == '.' && center == '.' && right == '^')
			{
				newRow.Append('^');
			}
			else
			{
				newRow.Append('.');
			}
		}

		rows.Add(newRow.ToString());
	}

	return rows.Sum(c => c.Count(c => c == '.'));
}