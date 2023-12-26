using System.Collections.Generic;

public static class Factorial
{
    private static readonly Dictionary<int, int> Cache = new();
    
    public static int Get(int n)
    {
        if (Cache.TryGetValue(n, out var factorial)) return factorial;
        var result = Implementation(n);
        Cache.Add(n, result);
        return result;
    }
    
    private static int Implementation(int n)
    {
        if (n <= 1) return 1;
        return n * Get(n - 1);
    }

    public static float Binomial(int n, int i)
    {
        return (float) Get(n) / (Get(i) * Get(n - i));
    }
}