using _25;

var states = new List<State>();


var state = new State()
{
	Id = "A",
	Write0 = 1,
	Move0 = 1,
	NextState0 = "B",
	Write1 = 0,
	Move1 = 1,
	NextState1 = "C"
};
states.Add(state);

state = new State()
{
	Id = "B",
	Write0 = 0,
	Move0 = -1,
	NextState0 = "A",
	Write1 = 0,
	Move1 = 1,
	NextState1 = "D"
};
states.Add(state);

state = new State()
{
	Id = "C",
	Write0 = 1,
	Move0 = 1,
	NextState0 = "D",
	Write1 = 1,
	Move1 = 1,
	NextState1 = "A"
};
states.Add(state);

state = new State()
{
	Id = "D",
	Write0 = 1,
	Move0 = -1,
	NextState0 = "E",
	Write1 = 0,
	Move1 = -1,
	NextState1 = "D"
};
states.Add(state);

state = new State()
{
	Id = "E",
	Write0 = 1,
	Move0 = 1,
	NextState0 = "F",
	Write1 = 1,
	Move1 = -1,
	NextState1 = "B"
};
states.Add(state);

state = new State()
{
	Id = "F",
	Write0 = 1,
	Move0 = 1,
	NextState0 = "A",
	Write1 = 1,
	Move1 = 1,
	NextState1 = "E"
};
states.Add(state);

var steps = 12368930;

var tape = new List<int>();

var cursor = 0;

var currentState = "A";

for (var i = 0; i < steps; i++)
{
	if (cursor < 0)
	{
		tape.Insert(0, 0);
		cursor = 0;
	}
	else if (cursor >= tape.Count)
	{
		tape.Add(0);
	}

	state = states.First(s => s.Id == currentState);

	var value = tape[cursor];

	if (value == 0)
	{
		tape[cursor] = state.Write0;
		cursor += state.Move0;
		currentState = state.NextState0;
	}
	else
	{
		tape[cursor] = state.Write1;
		cursor += state.Move1;
		currentState = state.NextState1;
	}
}

var checksum = tape.Count(t => t == 1);
Console.WriteLine(checksum);