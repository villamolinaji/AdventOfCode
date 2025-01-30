using _23;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var result = Resolve(false);

Console.WriteLine(result);

// Part 2
result = Resolve(true);

Console.WriteLine(result);


long Resolve(bool hasNat)
{
	var machines = (
		from address in Enumerable.Range(0, 50)
		select Nic(address)
	).ToList();

	var natAddress = 255;

	if (hasNat)
	{
		machines.Add(Nat(natAddress));
	}

	var packets = new List<(long address, long x, long y)>();

	while (!packets.Any(packet => packet.address == natAddress))
	{
		foreach (var machine in machines)
		{
			packets = machine(packets);
		}
	}

	return packets.Single(packet => packet.address == natAddress).y;
}

Func<List<(long address, long x, long y)>, List<(long address, long x, long y)>> Nic(int address)
{
	var computer = new IntcodeComputer(program);
	computer.Run(address);

	return (input) =>
	{
		var (data, packets) = Receive(input, address);

		if (!data.Any())
		{
			data.Add(-1);
		}

		computer.Run(data.ToArray());

		while (computer.Output.Count >= 3)
		{
			packets.Add((computer.Output.Dequeue(), computer.Output.Dequeue(), computer.Output.Dequeue()));
		}

		return packets;
	};
}

(List<long> data, List<(long address, long x, long y)> packets) Receive(List<(long address, long x, long y)> packets, int address)
{
	var filteredPackets = new List<(long address, long x, long y)>();
	var data = new List<long>();

	foreach (var packet in packets)
	{
		if (packet.address == address)
		{
			data.Add(packet.x);
			data.Add(packet.y);
		}
		else
		{
			filteredPackets.Add(packet);
		}
	}

	return (data, filteredPackets);
}

Func<List<(long address, long x, long y)>, List<(long address, long x, long y)>> Nat(int address)
{
	long? yLastSent = null;
	long? x = null;
	long? y = null;

	return (input) =>
	{
		var (data, packets) = Receive(input, address);

		if (data.Any())
		{
			(x, y) = (data[^2], data[^1]);
		}

		if (packets.Count == 0)
		{
			packets.Add((y == yLastSent ? 255 : 0, x!.Value, y!.Value));

			yLastSent = y;
		}

		return packets;
	};
}