var input = File.ReadAllTextAsync("Input.txt").Result;

var sumMetadata = 0;
int index = 0;

var numbers = input.Split(' ').Select(int.Parse).ToList();
index = 0;

sumMetadata += GetMetadata(numbers);

Console.WriteLine(sumMetadata);

// Part 2
sumMetadata = 0;
index = 0;

sumMetadata += GetMetadata2(numbers);

Console.WriteLine(sumMetadata);


int GetMetadata(List<int> numbers)
{
	int childCount = numbers[index++];
	int metadataCount = numbers[index++];
	int sum = 0;

	for (int i = 0; i < childCount; i++)
	{
		sum += GetMetadata(numbers);
	}

	for (int i = 0; i < metadataCount; i++)
	{
		sum += numbers[index++];
	}

	return sum;
}

int GetMetadata2(List<int> numbers)
{
	int childCount = numbers[index++];
	int metadataCount = numbers[index++];
	int sum = 0;

	var childValues = new List<int>();

	for (int i = 0; i < childCount; i++)
	{
		var childValue = GetMetadata2(numbers);
		childValues.Add(childValue);
	}

	if (childCount == 0)
	{
		for (int i = 0; i < metadataCount; i++)
		{
			sum += numbers[index++];
		}
	}
	else
	{
		for (int i = 0; i < metadataCount; i++)
		{
			var metadata = numbers[index++];
			if (metadata > 0 &&
				metadata <= childValues.Count)
			{
				sum += childValues[metadata - 1];
			}
		}
	}

	return sum;
}