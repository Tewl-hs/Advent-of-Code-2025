// Dayt 6
string[] rows = File.ReadAllLines("input.txt");
string oper = rows[4]; // Row with operators (+) (*)
var idx = new List<int>(); // Position of each operator. Signifies the start of each column

long part1 = 0; // Part 1 total 

long part2 = 0; // Part 2 total

for (int i = 0;i < oper.Length;i++)
{
    if (oper[i] == '+' || oper[i] == '*')
        idx.Add(i);
}

for (int c = 0; c < idx.Count; c++)
{
    int start = idx[c];
    int end = (c < idx.Count - 1) ? idx[c + 1] - 1 : rows[0].Length - 1;

    string[] values = new string[4];

    for (int row = 0; row < 4; row++)
    {
        for (int i = start; i <= end && i < rows[row].Length; i++)
            values[row] += rows[row][i];
    }

    // Convert to long[]
    long[] numbers = [.. values.Select(s => long.Parse(s.Trim()))];

    if (rows[4][start] == '+')
        part1 += numbers.Sum();
    else
        part1 += numbers.Aggregate(1L, (acc, val) => acc * val);

    // Part 2
    int maxLen = values.Max(v => v.Length);
    var vNumbers = new List<long>(); // Read numbers vertically

    // Read column right-to-left, top-to-bottom
    for (int i = maxLen - 1; i >= 0; i--)
    {
        string digits = "";
        for (int row = 0; row < 4; row++)
        {
            if (i < values[row].Length && char.IsDigit(values[row][i]))
                digits += values[row][i];
        }

        if (digits.Length > 0)
            vNumbers.Add(long.Parse(digits));
    }

    // Apply operator for Part 2
    if (rows[4][start] == '+')
        part2 += vNumbers.Sum();
    else
        part2 += vNumbers.Aggregate(1L, (acc, val) => acc * val);
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");