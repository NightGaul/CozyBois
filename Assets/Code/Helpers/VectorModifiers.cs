using System.Collections.Generic;
using UnityEngine;

namespace Code.Helpers
{
    public static class VectorModifiers
    {
        public static Vector3[] PointsOnSphere(int n)
        {
            List<Vector3> upts = new List<Vector3>();
            float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
            float off = 2.0f / n;
            float x = 0;
            float y = 0;
            float z = 0;
            float r = 0;
            float phi = 0;

            for (var k = 0; k < n; k++)
            {
                y = k * off - 1 + (off / 2);
                r = Mathf.Sqrt(1 - y * y);
                phi = k * inc;
                x = Mathf.Cos(phi) * r;
                z = Mathf.Sin(phi) * r;

                upts.Add(new Vector3(x, y, z));
            }

            Vector3[] pts = upts.ToArray();
            return pts;
        }

        
        public static Quaternion CalculateRotation(Vector3 point1, Vector3 point2)
        {
            Vector3 direction = point2 - point1;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
            return rotation;
        }

        public static Vector3 PointRotation(Vector3 P1, Vector3 P2, Quaternion rot)
        {
            var v = P1 - P2; //the relative vector from P2 to P1.
            v = rot * v; //rotatate
            v = P2 + v; //bring back to world space

            return v;
        }
        
        
    }
}