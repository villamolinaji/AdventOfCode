using System.Text.RegularExpressions;
using _25;

var input = File.ReadAllTextAsync("Input.txt").Result;

var directions = new List<string>()
{
	"south",
	"east",
	"west",
	"north"
};

var securityRoom = "== Security Checkpoint ==";

var program = input.Split(',').Select(long.Parse).ToArray();

var computer = new IntcodeComputer(program);

computer.Run();

var description = computer.ToAscii();

VisitRooms(securityRoom, computer, description, args =>
{
	foreach (var item in args.items)
	{
		if (item != "infinite loop")
		{
			var takeCmd = "take " + item;
			var clone = computer.Clone();
			clone.Run(takeCmd);
			if (!clone.IsHalt && Inventory(clone).Contains(item))
			{
				computer.Run(takeCmd);
			}
		}
	}

	return null;
});

var door = VisitRooms(
	securityRoom,
	computer,
	description,
	args => (args.room! == securityRoom)
		? args.doors.Single(door => door != ReverseDir(args.doorTaken))
		: null);

Random random = new Random();

var inventory = Inventory(computer).ToList();
var floor = new List<string>();

var password = string.Empty;

while (true)
{
	computer.Run(door);
	var output = computer.ToAscii();

	if (output.Contains("heavier"))
	{
		TakeOrDrop("take", floor, inventory);
	}
	else if (output.Contains("lighter"))
	{
		TakeOrDrop("drop", inventory, floor);
	}
	else
	{
		password = Regex.Match(output, @"\d+").Value;

		break;
	}
}

Console.WriteLine(password);


void TakeOrDrop(string cmd, List<string> from, List<string> to)
{
	var i = random.Next(from.Count);
	var item = from[i];
	from.RemoveAt(i);
	to.Add(item);
	computer.Run(cmd + " " + item);
}

string ReverseDir(string direction) => directions[3 - directions.IndexOf(direction)];

string? VisitRooms(
	string securityRoom,
	IntcodeComputer icm,
	string description,
	Func<(IEnumerable<string> items, string room, string? doorTaken, IEnumerable<string> doors), string> callback)
{
	var roomsSeen = new HashSet<string>();

	return DFS(description, null, roomsSeen, callback);
}

string? DFS(
	string description,
	string? doorTaken,
	HashSet<string> roomsSeen,
	Func<(IEnumerable<string> items, string room, string? doorTaken, IEnumerable<string> doors), string> callback)
{
	var room = description.Split("\n").Single(x => x.Contains("=="));
	var listing = GetListItems(description).ToHashSet();
	var doors = listing.Intersect(directions);
	var items = listing.Except(doors);

	if (!roomsSeen.Contains(room))
	{
		roomsSeen.Add(room);

		var res = callback((items, room, doorTaken, doors));

		if (res != null)
		{
			return res;
		}

		if (room != securityRoom)
		{
			foreach (var door in doors)
			{
				computer.Run(door);
				res = DFS(computer.ToAscii(), door, roomsSeen, callback);
				if (res != null)
				{
					return res;
				}
				computer.Run(ReverseDir(door));
			}
		}
	}

	return null;
}

IEnumerable<string> Inventory(IntcodeComputer icm)
{
	icm.Run("inv");

	return GetListItems(icm.ToAscii());
}

IEnumerable<string> GetListItems(string description)
{
	return description.Split("\n")
		.Where(l => l.StartsWith("- "))
		.Select(line => line.Substring(2));
}