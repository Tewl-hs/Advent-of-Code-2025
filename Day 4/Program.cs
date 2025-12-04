var input = File.ReadAllLines("input.txt");

char[][] grid = [.. input.Select(line => line.ToCharArray())];

// Will return the number of rolls removed and a copy of the grid with the rolls (@) removed (replaced with (.))
static (int removed, char[][] nextGrid) GetValidRolls(char[][] grid)
{
    int removed = 0;
    int rows = grid.Length;
    int cols = grid[0].Length;

    // Create a copy for Part 2
    char[][] next = [.. grid.Select(a => a.ToArray())];

    int[][] dirs =
    [
            [-1, -1], [-1, 0], [-1, 1],
            [ 0, -1],          [ 0, 1],
            [ 1, -1], [ 1, 0], [ 1, 1]
    ];

    for (int r = 0; r < rows;r++)
    {
        for (int c = 0; c < cols;c++)
        {
            // if position is a roll, check adjacent
            if (grid[r][c] == '@')
            {
                int count = 0;

                foreach(var d in dirs)
                {
                    int ar = r + d[0]; // adjacent row
                    int ac = c + d[1]; // adjacent column

                    // Adjacent position is valid position on grid
                    if (ar >= 0 && ar < rows && ac >= 0 && ac < cols && grid[ar][ac] == '@')
                    { 
                        count++;

                        if (count >= 4)
                            break;
                    }
                }

                if (count < 4)
                {
                    next[r][c] = '.'; // Remove roll in copy
                    removed++;
                }
            }
        }
    }

    return (removed, next);
}

// For part 1 we only need rolls removed
Console.WriteLine(GetValidRolls(grid).removed);



// Part 2
int total = 0;
var result = GetValidRolls(grid);

while (true)
{
    total += result.removed;
    if (result.removed == 0)
        break;

    result = GetValidRolls(result.nextGrid);
}

Console.WriteLine(total);