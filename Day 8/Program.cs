// Day 8
// I have to admit I asked a friend for help with the math.

var lines = File.ReadAllLines("input.txt");
var points = lines
    .Select(l =>
    {
        var s = l.Split(',');
        return new Point(
            int.Parse(s[0]),
            int.Parse(s[1]),
            int.Parse(s[2]));
    })
    .ToList();

int n = points.Count;

var edges = new List<Edge>(n * n / 2);

for (int i = 0; i < n; i++)
{
    for (int j = i + 1; j < n; j++)
    {
        edges.Add(new Edge
        {
            A = i,
            B = j,
            Dist = SquaredDistance(points[i], points[j])
        });
    }
}

edges.Sort((a, b) => a.Dist.CompareTo(b.Dist));

var uf = new UnionFind(n);

int count = 0;
long part1 = 0;
long part2 = 0;

foreach (var e in edges)
{
    uf.Union(e.A, e.B);
    count++;

    if (count == 1000 && part1 == 0)
    {
        var cmpsz = new Dictionary<int, int>();
        for (int i = 0;i < n;i++)
        {
            int r = uf.Find(i);
            if (!cmpsz.ContainsKey(r))
                cmpsz[r] = 0;
            cmpsz[r]++;
        }
        part1 = cmpsz.Values
                     .OrderByDescending(v => v)
                     .Take(3)
                     .Aggregate(1L, (acc,v)=>acc*v);
    }

    if (uf.Count == 1 && part2 == 0)
    {
        part2 = (long)points[e.A].X * (long)points[e.B].X;
        break;
    }    
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

static long SquaredDistance(Point p1, Point p2)
{
    long dx = p1.X - p2.X;
    long dy = p1.Y - p2.Y;
    long dz = p1.Z - p2.Z;
    return dx * dx + dy * dy + dz * dz;
}

struct Point(long x, long y, long z)
{
    public long X = x, Y = y, Z = z;
}

struct Edge(int a, int b, long sqd)
{
    public int A = a, B = b;
    public long Dist = sqd;
}

public class UnionFind
{
    public int[] Parent;
    public int[] Size;
    public int Count;

    public UnionFind(int n)
    {
        Parent = new int[n];
        Size = new int[n];
        Count = n;

        for (int i = 0; i < n; i++)
        {
            Parent[i] = i;
            Size[i] = 1;
        }
    }

    public int Find(int x)
    {
        while (Parent[x] != x)
        {
            Parent[x] = Parent[Parent[x]];
            x = Parent[x];
        }
        return x;
    }

    public bool Union(int a, int b)
    {
        int ra = Find(a);
        int rb = Find(b);
        if (ra == rb) return false;

        if (Size[ra] < Size[rb])
        {
            Parent[ra] = rb;
            Size[rb] += Size[ra];
        }
        else
        {
            Parent[rb] = ra;
            Size[ra] += Size[rb];
        }

        Count--;
        return true;
    }
}