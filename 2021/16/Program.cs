using System.Collections;
using _16;

var input = File.ReadAllTextAsync("Input.txt").Result;

Console.WriteLine(GetTotalVersion(GetPacket(GetReader(input))));

Console.WriteLine(Evaluate(GetPacket(GetReader(input))));


BitReader GetReader(string input) => new BitReader(
	new BitArray((
		from hexChar in input
		let value = Convert.ToInt32(hexChar.ToString(), 16)
		from mask in new[] { 8, 4, 2, 1 }
		select (mask & value) != 0)
	.ToArray()));

Packet GetPacket(BitReader reader)
{
	var version = reader.ReadInt(3);
	var type = reader.ReadInt(3);
	var packets = new List<Packet>();
	var payload = 0L;

	if (type == 0x4)
	{
		while (true)
		{
			var isLast = reader.ReadInt(1) == 0;

			payload = payload * 16 + reader.ReadInt(4);

			if (isLast)
			{
				break;
			}
		}
	}
	else if (reader.ReadInt(1) == 0)
	{
		var length = reader.ReadInt(15);

		var subPackages = reader.GetBitReader(length);

		while (subPackages.Any())
		{
			packets.Add(GetPacket(subPackages));
		}
	}
	else
	{
		var packetCount = reader.ReadInt(11);

		packets.AddRange(Enumerable.Range(0, packetCount).Select(_ => GetPacket(reader)));
	}

	return new Packet(version, type, payload, packets.ToArray());
}

int GetTotalVersion(Packet packet) =>
		packet.version + packet.packets.Select(GetTotalVersion).Sum();

long Evaluate(Packet packet)
{

	var parts = packet.packets.Select(Evaluate).ToArray();

	return packet.type switch
	{
		0 => parts.Sum(),
		1 => parts.Aggregate(1L, (acc, x) => acc * x),
		2 => parts.Min(),
		3 => parts.Max(),
		4 => packet.payload,
		5 => parts[0] > parts[1] ? 1 : 0,
		6 => parts[0] < parts[1] ? 1 : 0,
		7 => parts[0] == parts[1] ? 1 : 0,
		_ => throw new InvalidOperationException()
	};
}