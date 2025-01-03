namespace _18
{
	class Machine
	{
		private Dictionary<string, long> registers = new Dictionary<string, long>();
		private int sendCount = 0;
		private Queue<long> qIn;
		private Queue<long> qOut;
		private int index = 0;
		private bool running;

		public Machine(long p, Queue<long> qIn, Queue<long> qOut)
		{
			this["p"] = p;
			this.qIn = qIn;
			this.qOut = qOut;
		}

		private long this[string reg]
		{
			get => long.TryParse(reg, out var n)
				? n
				: registers.ContainsKey(reg)
					? registers[reg]
					: 0;
			set => registers[reg] = value;
		}

		public IEnumerable<(bool running, int valueSent)> Execute(string[] lines)
		{

			while (index >= 0 && index < lines.Length)
			{
				running = true;
				var line = lines[index];
				var parts = line.Split(' ');
				switch (parts[0])
				{
					case "snd":
						snd(parts[1]);
						break;
					case "rcv":
						rcv(parts[1]);
						break;
					case "set":
						set(parts[1], parts[2]);
						break;
					case "add":
						add(parts[1], parts[2]);
						break;
					case "mul":
						mul(parts[1], parts[2]);
						break;
					case "mod":
						mod(parts[1], parts[2]);
						break;
					case "jgz":
						jgz(parts[1], parts[2]);
						break;
				}
				yield return State();
			}

			running = false;

			yield return State();
		}

		private (bool running, int valueSent) State() => (running, sendCount);

		private void snd(string reg)
		{
			qOut.Enqueue(this[reg]);
			sendCount++;
			index++;
		}

		private void rcv(string reg)
		{
			if (qIn.Any())
			{
				this[reg] = qIn.Dequeue();
				index++;
			}
			else
			{
				running = false;
			}
		}

		private void set(string reg0, string reg1)
		{
			this[reg0] = this[reg1];
			index++;
		}

		private void add(string reg0, string reg1)
		{
			this[reg0] += this[reg1];
			index++;
		}

		private void mul(string reg0, string reg1)
		{
			this[reg0] *= this[reg1];
			index++;
		}

		private void mod(string reg0, string reg1)
		{
			this[reg0] %= this[reg1];
			index++;
		}

		private void jgz(string reg0, string reg1)
		{
			index += this[reg0] > 0 ? (int)this[reg1] : 1;
		}
	}
}
