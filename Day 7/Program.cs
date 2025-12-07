// Day 6
var lines = File.ReadAllLines("input.txt");

long part1 = 0;

var beams = new Dictionary<int, long> { [lines[0].IndexOf('S')] = 1 };

for (int i = 1; i < lines.Length; i++)
{
    var line = lines[i].ToCharArray();
    var beamPaths = new Dictionary<int, long>();

    foreach (var kvp in beams)
    {
        int b = kvp.Key;
        long ways = kvp.Value;

        if (line[b] == '^')
        {
            part1++; // increment beam split count

            if (b > 0)
                beamPaths[b - 1] = beamPaths.GetValueOrDefault(b - 1, 0) + ways;

            if (b + 1 < line.Length)
                beamPaths[b + 1] = beamPaths.GetValueOrDefault(b + 1, 0) + ways;
        }
        else
            beamPaths[b] = beamPaths.GetValueOrDefault(b, 0) + ways;
    }

    lines[i] = new string(line);
    beams = beamPaths;
}

long part2 = beams.Values.Sum(); // Sum of possible beam paths

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
