using _09;

var input = File.ReadAllTextAsync("Input.txt").Result;

var program = input.Split(',').Select(long.Parse).ToArray();

var computer = new IntcodeComputer(program);

computer.Run(1);

Console.WriteLine(computer.Output.Dequeue());

// Part 2
computer = new IntcodeComputer(program);

computer.Run(2);

Console.WriteLine(computer.Output.Dequeue());
