using _04;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var records = new List<Record>();

foreach (var line in lines)
{
	var record = new Record()
	{
		RecordDateTime = Convert.ToDateTime(line.Substring(1, 16)),
		Instruction = line.Substring(19)
	};
	records.Add(record);
}

records = records.OrderBy(x => x.RecordDateTime).ToList();

var guards = new List<Guard>();
var guard = new Guard()
{
	SleepMinutes = new Dictionary<DateTime, List<int>>()
};
var sleepMinute = 0;

foreach (var record in records)
{
	if (record.Instruction.Contains("Guard"))
	{
		var guardId = Convert.ToInt32(record.Instruction.Split(' ')[1].Substring(1));
		guard = guards.FirstOrDefault(x => x.Id == guardId);
		if (guard == null)
		{
			guard = new Guard()
			{
				Id = guardId,
				SleepMinutes = new Dictionary<DateTime, List<int>>()
			};
			guards.Add(guard);
		}
	}
	else if (record.Instruction.Contains("falls"))
	{
		if (!guard.SleepMinutes.ContainsKey(record.RecordDateTime.Date))
		{
			guard.SleepMinutes.Add(record.RecordDateTime.Date, new List<int>());
		}

		sleepMinute = record.RecordDateTime.Minute;
	}
	else if (record.Instruction.Contains("wakes"))
	{
		var wakeMinute = record.RecordDateTime.Minute;
		for (int i = sleepMinute; i < wakeMinute; i++)
		{
			guard.SleepMinutes[record.RecordDateTime.Date].Add(i);
		}
	}
}

var guardWithMostSleep = guards.OrderByDescending(x => x.SleepMinutes.Sum(y => y.Value.Count)).First();

var minuteMostSlept = guardWithMostSleep.SleepMinutes.SelectMany(x => x.Value).GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;

var result = guardWithMostSleep.Id * minuteMostSlept;

Console.WriteLine(result);

// Part 2
for (int i = 0; i < guards.Count; i++)
{
	var guardAux = guards[i];
	var guardMinuteMostSlept = guardAux.SleepMinutes.SelectMany(x => x.Value).GroupBy(x => x).OrderByDescending(x => x.Count()).FirstOrDefault();
	if (guardMinuteMostSlept != null)
	{
		guardAux.MinuteMostSlept = (guardMinuteMostSlept.Key, guardMinuteMostSlept.Count());
	}
}

var guardWithMostSleepOnSameMinute = guards.OrderByDescending(g => g.MinuteMostSlept.times).First();

result = guardWithMostSleepOnSameMinute.Id * guardWithMostSleepOnSameMinute.MinuteMostSlept.minute;

Console.WriteLine(result);
