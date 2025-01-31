using System.Text.RegularExpressions;
using _04;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var passports = new List<Passport>();
var passportAux = new Passport();

var indexLine = 0;

while (indexLine < lines.Length)
{
	var line = lines[indexLine];

	indexLine++;

	if (string.IsNullOrWhiteSpace(line))
	{
		passports.Add(passportAux);
		passportAux = new Passport();
	}
	else
	{
		var fields = line.Split(' ');

		foreach (var field in fields)
		{
			var parts = field.Split(':');
			passportAux.Fields.Add((parts[0], parts[1]));
		}
	}
}

passports.Add(passportAux);

var validPassports = passports.Count(passport => IsValidPassport(passport, false));

Console.WriteLine(validPassports);

// Part 2
validPassports = passports.Count(passport => IsValidPassport(passport, true));

Console.WriteLine(validPassports);


bool IsValidPassport(Passport passport, bool isPart2)
{
	var isValid = false;

	if (passport.Fields.Count == 8)
	{
		isValid = true;
	}
	else if (passport.Fields.Count == 7)
	{
		var hasCid = passport.Fields.Any(f => f.field == "cid");

		if (!hasCid)
		{
			isValid = true;
		}
	}

	if (isValid && isPart2)
	{
		foreach (var field in passport.Fields)
		{
			switch (field.field)
			{
				case "byr":
					isValid = int.TryParse(field.value, out var byr) &&
						byr >= 1920 &&
						byr <= 2002;

					break;
				case "iyr":
					isValid = int.TryParse(field.value, out var iyr) &&
						iyr >= 2010 &&
						iyr <= 2020;

					break;
				case "eyr":
					isValid = int.TryParse(field.value, out var eyr) &&
						eyr >= 2020 &&
						eyr <= 2030;

					break;
				case "hgt":
					if (field.value.EndsWith("cm"))
					{
						isValid = int.TryParse(field.value.Substring(0, field.value.Length - 2), out var hgt) &&
							hgt >= 150 &&
							hgt <= 193;
					}
					else if (field.value.EndsWith("in"))
					{
						isValid = int.TryParse(field.value.Substring(0, field.value.Length - 2), out var hgt) &&
							hgt >= 59 &&
							hgt <= 76;
					}
					else
					{
						isValid = false;
					}

					break;
				case "hcl":
					isValid = Regex.IsMatch(field.value, "^#[0-9a-f]{6}$");

					break;
				case "ecl":
					isValid = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(field.value);

					break;
				case "pid":
					isValid = Regex.IsMatch(field.value, "^[0-9]{9}$");

					break;
			}

			if (!isValid)
			{
				break;
			}
		}
	}

	return isValid;
}
