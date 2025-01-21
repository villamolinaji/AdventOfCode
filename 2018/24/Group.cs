namespace _24
{
	public class Group
	{
		public int HitPoints { get; set; }

		public int AttackPower { get; set; }

		required public string AttackType { get; set; }

		public List<string> Weaks { get; set; } = new List<string>();

		public List<string> Immunities { get; set; } = new List<string>();

		public int Initiative { get; set; }

		public int Units { get; set; }

		public bool IsImmuneSystem { get; set; }

		public int EffectivePower
		{
			get
			{
				return Units * AttackPower;
			}
		}

		public int DamageDealtTo(Group target)
		{
			if (target.IsImmuneSystem == IsImmuneSystem)
			{
				return 0;
			}
			else if (target.Immunities.Contains(AttackType))
			{
				return 0;
			}
			else if (target.Weaks.Contains(AttackType))
			{
				return EffectivePower * 2;
			}
			else
			{
				return EffectivePower;
			}
		}
	}
}
