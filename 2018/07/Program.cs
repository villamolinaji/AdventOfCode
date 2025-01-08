using System.Text;
using _07;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var steps = GetSteps();

var resultOrder = new StringBuilder();

while (steps.Any())
{
	var nextStep = steps
		.Where(s => !s.Dependencies.Any())
		.OrderBy(s => s.StepName)
		.First();

	resultOrder.Append(nextStep.StepName);

	steps.Remove(nextStep);

	foreach (var step in steps)
	{
		step.Dependencies.Remove(nextStep.StepName);
	}
}

Console.WriteLine(resultOrder.ToString());

// Part 2
steps = GetSteps();

var secondsPerStep = 60;
var workers = 5;

var timeWorkers = new int[workers];
var stepWorkers = new char[workers];

int time = 0;

while (steps.Any())
{
	for (int i = 0; i < workers; i++)
	{
		if (timeWorkers[i] == 0)
		{
			var nextStep = steps
				.Where(s => !s.Dependencies.Any() && !stepWorkers.Contains(s.StepName))
				.OrderBy(s => s.StepName)
				.FirstOrDefault();

			if (nextStep != null)
			{
				timeWorkers[i] = secondsPerStep + nextStep.StepName - 'A' + 1;
				stepWorkers[i] = nextStep.StepName;
			}
			else
			{
				break;
			}
		}
	}

	var minTime = timeWorkers.Where(t => t > 0).Min();
	time += minTime;

	for (int i = 0; i < workers; i++)
	{
		if (timeWorkers[i] > 0)
		{
			timeWorkers[i] -= minTime;
		}

		if (timeWorkers[i] == 0 &&
			stepWorkers[i] != '\0')
		{
			var stepCompleted = steps.First(s => s.StepName == stepWorkers[i]);
			steps.Remove(stepCompleted);

			foreach (var step in steps)
			{
				step.Dependencies.Remove(stepCompleted.StepName);
			}

			stepWorkers[i] = '\0';
		}
	}
}

Console.WriteLine(time);


List<Step> GetSteps()
{
	var steps = new List<Step>();

	foreach (var line in lines)
	{
		var parts = line.Split(' ');
		var stepName = parts[7][0];
		var dependency = parts[1][0];

		var step = steps.FirstOrDefault(s => s.StepName == stepName);
		if (step == null)
		{
			step = new Step
			{
				StepName = stepName
			};
			steps.Add(step);
		}
		step.Dependencies.Add(dependency);

		var step2 = steps.FirstOrDefault(s => s.StepName == dependency);
		if (step2 == null)
		{
			step2 = new Step
			{
				StepName = dependency
			};
			steps.Add(step2);
		}
	}

	return steps;
}
