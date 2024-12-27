var playerPoints = 50;
var playerMana = 500;
var bossPoints = 71;
var bossDamage = 10;

var spells = new List<(string name, int cost, int damage, int heal, int armor, int mana, int turns)>
{
	("Magic Missile", 53, 4, 0, 0, 0, 0),
	("Drain", 73, 2, 2, 0, 0, 0),
	("Shield", 113, 0, 0, 7, 0, 6),
	("Poison", 173, 3, 0, 0, 0, 6),
	("Recharge", 229, 0, 0, 0, 101, 5)
};

Console.WriteLine(GetMinMana(false));

Console.WriteLine(GetMinMana(true));


int GetMinMana(bool isPart2)
{
	var minMana = int.MaxValue;

	var queue = new Queue<(int points, int mana, int bossPoints, int shieldTurns, int poisonTurns, int rechargeTurns, int manaSpent, bool isPlayerTurn)>();

	foreach (var spell in spells)
	{
		var nextPlayerPoints = playerPoints;
		var nextPlayerMana = playerMana;
		var nextBossPoints = bossPoints;
		var nextShieldTurns = 0;
		var nextPoisonTurns = 0;
		var nextRechargeTurns = 0;
		var nextManaSpent = 0;

		switch (spell.name)
		{
			case "Magic Missile":
				nextBossPoints -= spell.damage;
				nextPlayerMana -= spell.cost;
				nextManaSpent += spell.cost;
				break;
			case "Drain":
				nextBossPoints -= spell.damage;
				nextPlayerPoints += spell.heal;
				nextPlayerMana -= spell.cost;
				nextManaSpent += spell.cost;
				break;
			case "Shield":
				nextShieldTurns = spell.turns;
				nextPlayerMana -= spell.cost;
				nextManaSpent += spell.cost;
				break;
			case "Poison":
				nextPoisonTurns = spell.turns;
				nextPlayerMana -= spell.cost;
				nextManaSpent += spell.cost;
				break;
			case "Recharge":
				nextRechargeTurns = spell.turns;
				nextPlayerMana -= spell.cost;
				nextManaSpent += spell.cost;
				break;
		}

		if (isPart2)
		{
			nextPlayerPoints--;
		}

		queue.Enqueue((nextPlayerPoints, nextPlayerMana, nextBossPoints, nextShieldTurns, nextPoisonTurns, nextRechargeTurns, nextManaSpent, false));
	}

	while (queue.Count > 0)
	{
		var (currentPlayerPoints, currentPlayerMana, currentBossPoints, currentShieldTurns, currentPoisonTurns, currentRechargeTurns, currentManaSpent, isPlayerTurn) = queue.Dequeue();

		if (currentManaSpent >= minMana)
		{
			continue;
		}

		if (isPlayerTurn)
		{
			if (isPart2)
			{
				currentPlayerPoints--;

				if (currentPlayerPoints <= 0)
				{
					continue;
				}
			}

			if (currentRechargeTurns > 0)
			{
				currentPlayerMana += 101;
				currentRechargeTurns--;
			}

			if (currentShieldTurns > 0)
			{
				currentShieldTurns--;
			}

			if (currentPoisonTurns > 0)
			{
				currentBossPoints -= 3;
				currentPoisonTurns--;
			}

			if (currentBossPoints <= 0)
			{
				minMana = Math.Min(minMana, currentManaSpent);
				continue;
			}

			foreach (var spell in spells)
			{
				if (spell.name == "Shield" && currentShieldTurns > 0)
				{
					continue;
				}
				if (spell.name == "Poison" && currentPoisonTurns > 0)
				{
					continue;
				}
				if (spell.name == "Recharge" && currentRechargeTurns > 0)
				{
					continue;
				}

				if (currentPlayerMana >= spell.cost)
				{
					var nextPlayerPoints = currentPlayerPoints;
					var nextPlayerMana = currentPlayerMana;
					var nextBossPoints = currentBossPoints;
					var nextShieldTurns = currentShieldTurns;
					var nextPoisonTurns = currentPoisonTurns;
					var nextRechargeTurns = currentRechargeTurns;
					var nextManaSpent = currentManaSpent;

					switch (spell.name)
					{
						case "Magic Missile":
							nextBossPoints -= spell.damage;
							nextPlayerMana -= spell.cost;
							nextManaSpent += spell.cost;
							break;
						case "Drain":
							nextBossPoints -= spell.damage;
							nextPlayerPoints += spell.heal;
							nextPlayerMana -= spell.cost;
							nextManaSpent += spell.cost;
							break;
						case "Shield":
							nextShieldTurns = spell.turns;
							nextPlayerMana -= spell.cost;
							nextManaSpent += spell.cost;
							break;
						case "Poison":
							nextPoisonTurns = spell.turns;
							nextPlayerMana -= spell.cost;
							nextManaSpent += spell.cost;
							break;
						case "Recharge":
							nextRechargeTurns = spell.turns;
							nextPlayerMana -= spell.cost;
							nextManaSpent += spell.cost;
							break;
					}

					queue.Enqueue((nextPlayerPoints, nextPlayerMana, nextBossPoints, nextShieldTurns, nextPoisonTurns, nextRechargeTurns, nextManaSpent, false));
				}
			}
		}
		else
		{
			if (currentPoisonTurns > 0)
			{
				currentBossPoints -= 3;
				currentPoisonTurns--;
			}

			if (currentRechargeTurns > 0)
			{
				currentPlayerMana += 101;
				currentRechargeTurns--;
			}

			if (currentBossPoints <= 0)
			{
				minMana = Math.Min(minMana, currentManaSpent);
				continue;
			}

			if (currentShieldTurns > 0)
			{
				currentPlayerPoints -= Math.Max(bossDamage - 7, 1);
				currentShieldTurns--;
			}
			else
			{
				currentPlayerPoints -= Math.Max(bossDamage, 1);
			}

			if (currentPlayerPoints <= 0)
			{
				continue;
			}

			queue.Enqueue((currentPlayerPoints, currentPlayerMana, currentBossPoints, currentShieldTurns, currentPoisonTurns, currentRechargeTurns, currentManaSpent, true));
		}
	}

	return minMana;
}