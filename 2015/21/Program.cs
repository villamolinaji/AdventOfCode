int bossHealth = 100;
int bossDamage = 8;
int bossArmor = 2;

var weapons = new[]
{
	new { Cost = 8, Damage = 4 },
	new { Cost = 10, Damage = 5 },
	new { Cost = 25, Damage = 6 },
	new { Cost = 40, Damage = 7 },
	new { Cost = 74, Damage = 8 }
};

var armors = new[]
{
	new { Cost = 0, Armor = 0 },
	new { Cost = 13, Armor = 1 },
	new { Cost = 31, Armor = 2 },
	new { Cost = 53, Armor = 3 },
	new { Cost = 75, Armor = 4 },
	new { Cost = 102, Armor = 5 }
};

var rings = new[]
{
	new { Cost = 0, Damage = 0, Armor = 0 },
	new { Cost = 25, Damage = 1, Armor = 0 },
	new { Cost = 50, Damage = 2, Armor = 0 },
	new { Cost = 100, Damage = 3, Armor = 0 },
	new { Cost = 20, Damage = 0, Armor = 1 },
	new { Cost = 40, Damage = 0, Armor = 2 },
	new { Cost = 80, Damage = 0, Armor = 3 }
};

var minCost = int.MaxValue;

foreach (var weapon in weapons)
{
	foreach (var armor in armors)
	{
		foreach (var ring1 in rings)
		{
			foreach (var ring2 in rings)
			{
				if (ring1 == ring2)
				{
					continue;
				}
				for (int useArmor = 0; useArmor < 2; useArmor++)
				{
					for (int useRing1 = 0; useRing1 < 2; useRing1++)
					{
						for (int useRing2 = 0; useRing2 < 2; useRing2++)
						{
							var playerDamage = weapon.Damage + ring1.Damage * useRing1 + ring2.Damage * useRing2;
							var playerArmor = armor.Armor * useArmor + ring1.Armor * useRing1 + ring2.Armor * useRing2;
							var playerCost = weapon.Cost + armor.Cost * useArmor + ring1.Cost * useRing1 + ring2.Cost * useRing2;

							if (Fight(100, playerDamage, playerArmor))
							{
								minCost = Math.Min(minCost, playerCost);
							}
						}
					}
				}
			}
		}
	}
}

Console.WriteLine(minCost);

// Part 2
var maxCost = 0;

foreach (var weapon in weapons)
{
	foreach (var armor in armors)
	{
		foreach (var ring1 in rings)
		{
			foreach (var ring2 in rings)
			{
				if (ring1 == ring2)
				{
					continue;
				}
				for (int useArmor = 0; useArmor < 2; useArmor++)
				{
					for (int useRing1 = 0; useRing1 < 2; useRing1++)
					{
						for (int useRing2 = 0; useRing2 < 2; useRing2++)
						{
							var playerDamage = weapon.Damage + ring1.Damage * useRing1 + ring2.Damage * useRing2;
							var playerArmor = armor.Armor * useArmor + ring1.Armor * useRing1 + ring2.Armor * useRing2;
							var playerCost = weapon.Cost + armor.Cost * useArmor + ring1.Cost * useRing1 + ring2.Cost * useRing2;

							if (!Fight(100, playerDamage, playerArmor))
							{
								maxCost = Math.Max(maxCost, playerCost);
							}
						}
					}
				}
			}
		}
	}
}

Console.WriteLine(maxCost);


bool Fight(int playerHealth, int playerDamage, int playerArmor)
{
	int playerTurns = (int)Math.Ceiling((double)bossHealth / Math.Max(1, playerDamage - bossArmor));

	int bossTurns = (int)Math.Ceiling((double)playerHealth / Math.Max(1, bossDamage - playerArmor));

	return playerTurns <= bossTurns;
}