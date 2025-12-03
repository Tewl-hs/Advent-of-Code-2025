// Greedy approach! O(n)

// Part 1 wants to create the max two digit number from the strings

var input = File.ReadAllLines("input.txt");

static int MaxTwoDigit(string s)
{
    var digits = "9876543210";

    foreach (char f in digits)
    {
        var i = s[..^1].IndexOf(f);
        if (i > -1)
        {
            foreach (char g in digits)
            {
                var j = s[(i + 1)..].IndexOf(g);
                if (j > -1)
                {
                    return (f - '0') * 10 + (g - '0');
                }
            }
        }
    }
    return 0;
}

Console.WriteLine(input.Sum(MaxTwoDigit));

// Part 2 whats toe create a 12 digit number from the strings

static long MaxTwelveDigit(string s)
{
    var digits = "9876543210";
    long result = 0;
    long m = 100000000000;
    int pos = 0;
    int len = s.Length;

    for (int i = 0; i < 12; i++)
    {
        int idx = len - (12 - i);
        foreach (char d in digits)
        {
            int x = s.AsSpan(pos, idx - pos + 1).IndexOf(d);
            if (x != -1)
            {
                result += (d - '0') * m;
                m /= 10;
                pos += x + 1;
                break;
            }
        }
    }

    return result; 
}

Console.WriteLine(input.Sum(MaxTwelveDigit));