namespace _23
{
	public class IntcodeComputer
	{
		private Dictionary<long, long> memory;

		private int index;

		private long relativeBase;

		public Queue<long> Input { get; set; } = new();

		public Queue<long> Output { get; } = new();

		public bool IsHalt { get; private set; }

		public IntcodeComputer(long[] program)
		{
			memory = program
				.Select((value, index) => new { value, index })
				.ToDictionary(x => (long)x.index, x => x.value);
			index = 0;
			relativeBase = 0;
		}

		public IntcodeComputer()
		{
			memory = new Dictionary<long, long>();
			index = 0;
			relativeBase = 0;
		}

		public IntcodeComputer Clone()
		{
			var cloneComputer = new IntcodeComputer();

			cloneComputer.memory = new Dictionary<long, long>(memory);
			cloneComputer.index = index;
			cloneComputer.relativeBase = relativeBase;
			cloneComputer.Input = new Queue<long>(Input);

			return cloneComputer;
		}

		public void Run(params string[] input)
		{
			Run(AsciiEncode(string.Join("", from line in input select line + "\n")));
		}

		public void Run(long first)
		{
			Run(new[] { first });
		}

		public void Run(long[] input)
		{
			foreach (var i in input)
			{
				this.Input.Enqueue(i);
			}

			while (!IsHalt)
			{
				int instruction = (int)GetMemory(index);
				int opcode = instruction % 100;
				int mode1 = (instruction / 100) % 10;
				int mode2 = (instruction / 1000) % 10;
				int mode3 = (instruction / 10000) % 10;


				switch (opcode)
				{
					case 1:
						SetParameter(3, mode3, GetParameter(1, mode1) + GetParameter(2, mode2));

						index += 4;
						break;
					case 2:
						SetParameter(3, mode3, GetParameter(1, mode1) * GetParameter(2, mode2));

						index += 4;
						break;
					case 3:
						if (Input.Count == 0)
						{
							return;
						}

						SetParameter(1, mode1, Input.Dequeue());

						index += 2;

						break;
					case 4:
						Output.Enqueue(GetParameter(1, mode1));

						index += 2;

						break;
					case 5:
						index = GetParameter(1, mode1) != 0 ? (int)GetParameter(2, mode2) : index + 3;

						break;
					case 6:
						index = GetParameter(1, mode1) == 0 ? (int)GetParameter(2, mode2) : index + 3;

						break;
					case 7:
						SetParameter(3, mode3, GetParameter(1, mode1) < GetParameter(2, mode2) ? 1 : 0);

						index += 4;

						break;
					case 8:
						SetParameter(3, mode3, GetParameter(1, mode1) == GetParameter(2, mode2) ? 1 : 0);

						index += 4;

						break;
					case 9:
						relativeBase += GetParameter(1, mode1);

						index += 2;

						break;
					case 99:
						IsHalt = true;

						return;
					default:
						throw new InvalidOperationException($"Unknown opcode {opcode}");
				}
			}
		}

		private long GetMemory(long address) => memory.ContainsKey(address) ? memory[address] : 0;

		private void SetMemory(long address, long value)
		{
			memory[address] = value;
		}

		private long GetParameter(long offset, int mode)
		{
			long value = GetMemory(index + offset);

			switch (mode)
			{
				case 0:
					return GetMemory(value);
				case 1:
					return value;
				case 2:
					return GetMemory(relativeBase + value);
			}

			return 0;
		}

		private void SetParameter(long offset, int mode, long value)
		{
			long address = GetMemory(index + offset);

			if (mode == 2)
			{
				address += relativeBase;
			}

			SetMemory(address, value);
		}

		private static long[] AsciiEncode(string st)
		{
			return (from ch in st select (long)ch).ToArray();
		}
	}
}
