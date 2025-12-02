var nums = File.ReadAllText("input.txt").Trim().Split(',')
    .SelectMany(r => { 
        var p = r.Split('-'); 
        long start = long.Parse(p[0]), end = long.Parse(p[1]); 
        return Enumerable.Range(0, (int)(end - start + 1)).Select(i => start + i); 
    });

// Mirror numbers
Console.WriteLine(nums.Where(n => {
    var s = n.ToString();
    return s.Length % 2 == 0 && s[..(s.Length / 2)] == s[(s.Length / 2)..];
}).Sum());

// Repeating pattern numbers
Console.WriteLine(nums.Where(n => {
    var s = n.ToString();
    int L = s.Length;
    return Enumerable.Range(1, L / 2).Any(k => L % k == 0 && string.Concat(Enumerable.Repeat(s[..k], L / k)) == s);
}).Sum());