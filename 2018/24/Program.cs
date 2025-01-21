using _24;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var groups = new List<Group>();

var result = Fight(0);

Console.WriteLine(result.units);

// Part 2
var l = 0;
var boost = int.MaxValue / 2;
while (boost - l > 1)
{
	var m = (boost + l) / 2;

	if (Fight(m).isImmuneSystem)
	{
		boost = m;
	}
	else
	{
		l = m;
	}
}

result = Fight(boost);

Console.WriteLine(result.units);


void ReadInput()
{
	groups = new List<Group>();

	bool isImmuneSystem = true;
	foreach (var line in lines)
	{
		if (line == "Immune System:" || string.IsNullOrEmpty(line))
		{
			continue;
		}

		if (line == "Infection:")
		{
			isImmuneSystem = false;
			continue;
		}

		var parts = line.Split(" ");
		var group = new Group
		{
			Units = int.Parse(parts[0]),
			HitPoints = int.Parse(parts[4]),
			AttackPower = int.Parse(parts[parts.Length - 6]),
			AttackType = parts[parts.Length - 5],
			Initiative = int.Parse(parts[parts.Length - 1]),
			IsImmuneSystem = isImmuneSystem
		};

		if (parts.Contains("weak") || parts.Contains("(weak"))
		{
			group.Weaks = new List<string>();

			var partIndex = Array.IndexOf(parts, "weak");

			if (partIndex == -1)
			{
				partIndex = Array.IndexOf(parts, "(weak");
			}

			partIndex++;

			var endWhile = false;
			while (!endWhile)
			{
				partIndex++;

				var weak = parts[partIndex].TrimEnd(',', ';');

				if (weak.EndsWith(')'))
				{
					weak = weak.TrimEnd(')');
					endWhile = true;
				}

				if (weak == "immune")
				{
					break;
				}

				group.Weaks.Add(weak);
			}
		}

		if (parts.Contains("immune") || parts.Contains("(immune"))
		{
			group.Immunities = new List<string>();

			var partIndex = Array.IndexOf(parts, "immune");

			if (partIndex == -1)
			{
				partIndex = Array.IndexOf(parts, "(immune");
			}

			partIndex++;

			var endWhile = false;
			while (!endWhile)
			{
				partIndex++;

				var immunity = parts[partIndex].TrimEnd(',', ';');

				if (immunity.EndsWith(')'))
				{
					immunity = immunity.TrimEnd(')');
					endWhile = true;
				}

				if (immunity == "weak")
				{
					break;
				}

				group.Immunities.Add(immunity);
			}
		}

		groups.Add(group);
	}
}


(bool isImmuneSystem, long units) Fight(int boost)
{
	ReadInput();

	foreach (var g in groups)
	{
		if (g.IsImmuneSystem)
		{
			g.AttackPower += boost;
		}
	}

	var attack = true;

	while (attack)
	{
		attack = false;
		var remainingTarget = new HashSet<Group>(groups);
		var targets = new Dictionary<Group, Group>();
		foreach (var g in groups.OrderByDescending(g => (g.EffectivePower, g.Initiative)))
		{
			var maxDamage = remainingTarget.Select(t => g.DamageDealtTo(t)).Max();

			if (maxDamage > 0)
			{
				var possibleTargets = remainingTarget.Where(t => g.DamageDealtTo(t) == maxDamage);

				targets[g] = possibleTargets.OrderByDescending(t => (t.EffectivePower, t.Initiative)).First();

				remainingTarget.Remove(targets[g]);
			}
		}

		foreach (var g in targets.Keys.OrderByDescending(g => g.Initiative))
		{
			if (g.Units > 0)
			{
				var target = targets[g];

				var damage = g.DamageDealtTo(target);

				if (damage > 0 && target.Units > 0)
				{
					var dies = damage / target.HitPoints;

					target.Units = Math.Max(0, target.Units - dies);

					if (dies > 0)
					{
						attack = true;
					}
				}
			}
		}

		groups = groups.Where(g => g.Units > 0).ToList();
	}

	return (groups.All(x => x.IsImmuneSystem), groups.Select(x => x.Units).Sum());
}
