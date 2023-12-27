using System;
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

    public Vector3 GetUPoint(float t)
    {
        var p = GetPoint(t);
        return p / p.y;
    }
    
    public Vector3 GetPoint(float t)
    {
        if (t == 0F) return Points.First().WeightedPosition;
        if (t == 1F) return Points.Last().WeightedPosition;
        if (t is < 0F or > 1F) return Vector3.zero;
        
        var res = Vector3.zero;
        var ti = 1F;
        var tni = Mathf.Pow(1 - t, N);

        for (var i = 0; i <= N; i++)
        {
            var p = Points.ElementAt(i);
            var b = Factorial.Binomial(N, i) * ti * tni;
            
            res += b * p.WeightedPosition;
            
            ti *= t;
            tni /= 1 - t;
        }

        return res;
    }

    public Vector2 GetUDerivative(float t)
    {
        if (t == 0F) t += 0.001F;
        if (t == 1F) t -= 0.001F;
        if (t is < 0F or > 1F) return Vector2.zero;
        
        var res = Vector2.zero;

        for (var i = 0; i <= N - 1; i++)
        {
            var pi = Points.ElementAt(i);
            var pii = Points.ElementAt(i + 1);
            var deltaP = pii.UPosition - pi.UPosition;

            var lambda = 0F;

            var tj = 1F;
            var tnj = Mathf.Pow(1 - t, N - i);
            for (var j = 0; j <= i; j++)
            {
                var bj = Factorial.Binomial(N, j) * tj * tnj;
                
                var tk = (float) Math.Pow(t, i + 1);
                var tnk = Mathf.Pow(1 - t, N);
                for (var k = i + 1; k <= N; k++)
                {
                    var bk = Factorial.Binomial(N, k) * tk * tnk;
                    
                    lambda += (k - j) * bj * bk * Points.ElementAt(j).Weight * Points.ElementAt(k).Weight;
                    
                    tk *= t;
                    tnk /= 1 - t;
                }
                
                tj *= t;
                tnj /= 1 - t;
            }
            
            res += lambda * deltaP;
        }

        var w0N = 0F;
        var ti = 1F;
        var tni = Mathf.Pow(1 - t, N);
        for (var i = 0; i <= N; i++)
        {
            var b = Factorial.Binomial(N, i) * ti * tni;
            w0N += b * Points.ElementAt(i).Weight;
            
            ti *= t;
            tni /= 1 - t;
        }
        
        res /= (1 - t) * t * w0N;

        return res;
    }
}