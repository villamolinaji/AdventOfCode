namespace _07
{
	public class IntcodeComputer
	{
		private readonly long[] positions;

		private int index;

		public Queue<long> Input { get; } = new();

		public Queue<long> Output { get; } = new();

		public bool IsHalt { get; private set; }

		public IntcodeComputer(long[] program)
		{
			positions = program.ToArray();
			index = 0;
		}

		public void Run()
		{
			while (!IsHalt)
			{
				long instruction = positions[index];
				long de = instruction % 100;
				long c = (instruction / 100) % 10;
				long b = (instruction / 1000) % 10;

				long value1 = 0;
				long value2 = 0;

				switch (de)
				{
					case 1:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
						value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

						positions[positions[index + 3]] = value1 + value2;

						index += 4;
						break;
					case 2:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
						value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

						positions[positions[index + 3]] = value1 * value2;
						index += 4;
						break;
					case 3:
						if (Input.Count == 0)
						{
							return;
						}

						positions[positions[index + 1]] = Input.Dequeue();
						index += 2;

						break;
					case 4:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];

						Output.Enqueue(value1);

						index += 2;

						break;
					case 5:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
						value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

						if (value1 != 0)
						{
							index = (int)value2;
						}
						else
						{
							index += 3;
						}

						break;
					case 6:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
						value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

						if (value1 == 0)
						{
							index = (int)value2;
						}
						else
						{
							index += 3;
						}

						break;
					case 7:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
						value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

						positions[positions[index + 3]] = value1 < value2 ? 1 : 0;

						index += 4;

						break;
					case 8:
						value1 = c == 0 ? positions[positions[index + 1]] : positions[index + 1];
						value2 = b == 0 ? positions[positions[index + 2]] : positions[index + 2];

						positions[positions[index + 3]] = value1 == value2 ? 1 : 0;

						index += 4;

						break;
					case 99:
						IsHalt = true;

						return;
					default:
						throw new InvalidOperationException($"Unknown opcode {positions[index]}");
				}
			}
		}
	}
}
