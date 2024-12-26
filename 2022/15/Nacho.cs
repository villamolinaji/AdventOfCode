/*
using System.Text.RegularExpressions;
var input = File.ReadAllLines("input.txt");
int lineToCheck = 2000000;
var positions = new HashSet<(int, int)>();
var beacons = new HashSet<(int, int)>();
var sensorsAndBeacons = input.Select(line =>
{
    var m = Regex.Match(line, @"Sensor at x=(-?\d+), y=(-?\d+)\: closest beacon is at x=(-?\d+), y=(-?\d+)");
    var xs = Convert.ToInt32(m.Groups[1].Value);
    var ys = Convert.ToInt32(m.Groups[2].Value);
    var xb = Convert.ToInt32(m.Groups[3].Value);
    var yb = Convert.ToInt32(m.Groups[4].Value);
    beacons.Add((xb, yb));
    return ((xs, ys), (xb, yb));
}).ToList();
foreach (var sensorAndBeacon in sensorsAndBeacons)
{
    PopulateForbiddenPositions(positions, sensorAndBeacon.Item1, sensorAndBeacon.Item2, lineToCheck);
}
var res1 = positions.Count() - beacons.Count(b => b.Item2 == lineToCheck);
Console.WriteLine($"Part 1: {res1}");
var searchRange = (0, 4000000);
var availablePosition = (-1, -1);
for (int i = searchRange.Item1; i <= searchRange.Item2; i++)
{
    var forbiddenRanges = new HashSet<(int, int)>();
    foreach (var sensorAndBeacon in sensorsAndBeacons)
    {
        var forbiddenRange = GetForbiddenRanges(sensorAndBeacon.Item1, sensorAndBeacon.Item2, i, 0, 4000000);
        if (forbiddenRange != (-1, -1)) forbiddenRanges.Add(forbiddenRange);
    }
    var availableX = GetMissingNumber(forbiddenRanges);
    if (availableX != -1)
    {
        availablePosition = (availableX, i);
        break;
    }
}
var res2 = (long)availablePosition.Item1 * 4000000 + availablePosition.Item2;
Console.WriteLine($"Part 2: {res2}");
int GetMissingNumber(HashSet<(int, int)> ranges)
{
    var rangesOrdered = ranges.OrderBy(r => r.Item1).ThenBy(r => r.Item2).ToList();
    for (int i = 0; i < rangesOrdered.Count - 1; i++)
    {
        if (Overlap(rangesOrdered[i], rangesOrdered[i + 1]))
        {
            rangesOrdered[i + 1] = (rangesOrdered[i].Item1, Math.Max(rangesOrdered[i].Item2, rangesOrdered[i + 1].Item2));
        }
        else if (rangesOrdered[i + 1].Item1 - rangesOrdered[i].Item2 > 1)
        {
            return rangesOrdered[i].Item2 + 1;
        }
    }
    return -1;
}
void PopulateForbiddenPositions(HashSet<(int, int)> positions, (int x, int y) sensor, (int x, int y) beacon, int lineToCheck)
{
    var dis = Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
    var yDis = Math.Abs(lineToCheck - sensor.y);
    var xDis = dis - yDis;
    if (xDis <= 0) return;
    for (int i = sensor.x - xDis; i <= sensor.x + xDis; i++)
    {
        positions.Add((i, lineToCheck));
    }
}
(int, int) GetForbiddenRanges((int x, int y) sensor, (int x, int y) beacon, int lineToCheck, int min, int max)
{
    var dis = Math.Abs(sensor.x - beacon.x) + Math.Abs(sensor.y - beacon.y);
    var yDis = Math.Abs(lineToCheck - sensor.y);
    var xDis = dis - yDis;
    return xDis > 0 ? (Math.Max(sensor.x - xDis, min), Math.Min(sensor.x + xDis, max)) : (-1, -1);
}
bool Overlap((int a1, int b1) a, (int a2, int b2) b) => !(a.b1 < b.a2 || a.a1 > b.b2);
*/