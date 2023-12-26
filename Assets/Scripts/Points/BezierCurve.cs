using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BezierCurve
{
    public BezierCurve(IEnumerable<WeightedPoint> points)
    {
        Points = points;
    }
    
    private IEnumerable<WeightedPoint> Points { get; set; }
    
    private int N => Points.Count() - 1;

    public Vector3 GetPoint(float t)
    {
        if (t == 0F) return Points.First().WeightedPosition;
        if (t == 1F) return Points.Last().WeightedPosition;
        if (t is < 0F or > 1F) return Vector3.zero;
        
        var num = Vector3.zero;
        var denom = 0F;
        var ti = 1F;
        var tni = Mathf.Pow(1 - t, N);

        for (var i = 0; i <= N; i++)
        {
            var p = Points.ElementAt(i);
            var b = Factorial.Binomial(N, i) * ti * tni;
            
            num += b * p.WeightedPosition;
            denom += b;
            
            ti *= t;
            tni /= 1 - t;
        }
        
        return num / denom;
    }
}