using _13;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var rows = lines.Length;
var cols = lines[0].Length;

var grid = new char[rows, cols];

var carts = new List<Cart>();

var cartId = 0;
for (int y = 0; y < rows; y++)
{
	for (int x = 0; x < cols; x++)
	{
		grid[y, x] = lines[y][x];

		if (lines[y][x] == '<')
		{
			var cart = new Cart();
			cart.X = x;
			cart.Y = y;
			cart.Direction = 3;
			cart.NextTurn = 0;
			cart.Id = cartId++;
			carts.Add(cart);
		}
		else if (lines[y][x] == '>')
		{
			var cart = new Cart();
			cart.X = x;
			cart.Y = y;
			cart.Direction = 1;
			cart.NextTurn = 0;
			cart.Id = cartId++;
			carts.Add(cart);
		}
		else if (lines[y][x] == '^')
		{
			var cart = new Cart();
			cart.X = x;
			cart.Y = y;
			cart.Direction = 0;
			cart.NextTurn = 0;
			cart.Id = cartId++;
			carts.Add(cart);
		}
		else if (lines[y][x] == 'v')
		{
			var cart = new Cart();
			cart.X = x;
			cart.Y = y;
			cart.Direction = 2;
			cart.NextTurn = 0;
			cart.Id = cartId++;
			carts.Add(cart);
		}
	}
}

var endWhile = false;
while (!endWhile)
{
	carts = carts
		.Where(c => !c.IsCrashed)
		.OrderBy(c => c.Y)
		.ThenBy(c => c.X).ToList();

	foreach (var cart in carts)
	{
		if (cart.IsCrashed)
		{
			continue;
		}

		if (carts.Count == 1)
		{
			Console.WriteLine($"Last cart: {carts[0].X},{carts[0].Y}");

			endWhile = true;

			break;
		}

		if (cart.Direction == 0)
		{
			cart.Y--;
		}
		else if (cart.Direction == 1)
		{
			cart.X++;
		}
		else if (cart.Direction == 2)
		{
			cart.Y++;
		}
		else if (cart.Direction == 3)
		{
			cart.X--;
		}

		if (grid[cart.Y, cart.X] == '+')
		{
			if (cart.NextTurn == 0)
			{
				cart.Direction = (cart.Direction + 3) % 4;
			}
			else if (cart.NextTurn == 2)
			{
				cart.Direction = (cart.Direction + 1) % 4;
			}

			cart.NextTurn = (cart.NextTurn + 1) % 3;
		}
		else if (grid[cart.Y, cart.X] == '/')
		{
			if (cart.Direction == 0)
			{
				cart.Direction = 1;
			}
			else if (cart.Direction == 1)
			{
				cart.Direction = 0;
			}
			else if (cart.Direction == 2)
			{
				cart.Direction = 3;
			}
			else if (cart.Direction == 3)
			{
				cart.Direction = 2;
			}
		}
		else if (grid[cart.Y, cart.X] == '\\')
		{
			if (cart.Direction == 0)
			{
				cart.Direction = 3;
			}
			else if (cart.Direction == 1)
			{
				cart.Direction = 2;
			}
			else if (cart.Direction == 2)
			{
				cart.Direction = 1;
			}
			else if (cart.Direction == 3)
			{
				cart.Direction = 0;
			}
		}

		var crash = carts
			.FirstOrDefault(c => c.Id != cart.Id && c.X == cart.X && c.Y == cart.Y);

		if (crash != null)
		{
			Console.WriteLine($"Crash: {crash.X},{crash.Y}");

			crash.IsCrashed = true;
			cart.IsCrashed = true;
		}
	}
}
