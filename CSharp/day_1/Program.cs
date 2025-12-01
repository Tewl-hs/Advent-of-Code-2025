long pos = 50;
ulong part1 = 0;
ulong part2 = 0;

foreach (var line in File.ReadLines("input.txt"))
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    char dir = line[0];
    long amount = long.Parse(line.Substring(1));

    if (dir == 'L')
    {
        part2 += (ulong)(amount / 100);
        if (pos != 0 && amount % 100 >= pos)
            part2 += 1;

        pos = (pos - amount) % 100;
        if (pos < 0) pos += 100;
    }
    else
    {
        pos += amount;
        part2 += (ulong)(pos / 100);

        pos = pos % 100;
        if (pos < 0) pos += 100;
    }

    if (pos == 0)
        part1 += 1;
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");