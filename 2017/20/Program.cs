using _20;

var lines = File.ReadAllLinesAsync("Input.txt").Result;

var index = 0;
var particles = lines.Select(line =>
{
	var parts = line.Split(new[] { ' ', ',', '<', '>', '=', 'p', 'v', 'a' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse)
		.ToArray();
	return new Particle
	{
		Index = index++,
		Position = (parts[0], parts[1], parts[2]),
		Velocity = (parts[3], parts[4], parts[5]),
		Acceleration = (parts[6], parts[7], parts[8])
	};
}).ToArray();

var closest = particles
	.OrderBy(particles => GetLength(particles.Acceleration.x, particles.Acceleration.y, particles.Acceleration.z))
	.ThenBy(particles => GetLength(particles.Velocity.x, particles.Velocity.y, particles.Velocity.z))
	.ThenBy(particles => GetLength(particles.Position.x, particles.Position.y, particles.Position.z))
	.First();

Console.WriteLine(closest.Index);

// Part 2
for (int i = 0; i < 1000; i++)
{
	foreach (var particle in particles.Where(p => !p.IsDestroyed))
	{
		particle.Velocity = (
			particle.Velocity.x + particle.Acceleration.x,
			particle.Velocity.y + particle.Acceleration.y,
			particle.Velocity.z + particle.Acceleration.z
		);
		particle.Position = (
			particle.Position.x + particle.Velocity.x,
			particle.Position.y + particle.Velocity.y,
			particle.Position.z + particle.Velocity.z
		);
	}

	var collisions = particles
		.GroupBy(particle => (particle.Position.x, particle.Position.y, particle.Position.z))
		.Where(group => group.Count() > 1)
		.SelectMany(group => group)
		.ToArray();

	foreach (var collision in collisions)
	{
		collision.IsDestroyed = true;
	}
}

Console.WriteLine(particles.Count(p => !p.IsDestroyed));


int GetLength(int x, int y, int z) => Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
