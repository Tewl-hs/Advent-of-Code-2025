// Day 5
var input = File.ReadAllLines("input.txt");
int blank = Array.FindIndex(input, string.IsNullOrWhiteSpace); // index of blank line

var ranges = input
    .Take(blank) // Read up to blank line
    .Select(range =>
    {
        var parts = range.Split('-');
        long start = long.Parse(parts[0]);
        long end = long.Parse(parts[1]);
        return (start, end);
    })
    .OrderBy(range => range.start)
    .ThenBy(range => range.end)
    .ToList();

var mergedRanges = new List<(long start, long end)>();

foreach (var r in ranges)
{
    // If no ranges in list or current range begins past the end of the last range
    // Append current range to the list
    if (mergedRanges.Count == 0 || r.start > mergedRanges.Last().end)
        mergedRanges.Add(r);
    // Else merge the current range with the last range in the list
    // Replacing the end of hte last range with the largest of the ranges
    else
        mergedRanges[^1] = (mergedRanges.Last().start, Math.Max(mergedRanges.Last().end, r.end));
}

// Day 5 - Part 1
// Get the number of ingredients that exist within the merged ranges
var freshCount = input
            .Skip(blank + 1) // Read past blank line
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Select(long.Parse)
            .Count(item => mergedRanges.Any(r => item >= r.start && item <= r.end));

Console.WriteLine($"Part 1: {freshCount}");

// Day 5 - Part 2
// Get total number of numbers in the merged ranges
long totalFresh = 0;

foreach (var (start, end) in mergedRanges)
{
    totalFresh += end - start + 1;
}

Console.WriteLine($"Part 2: {totalFresh}");