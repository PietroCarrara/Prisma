using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public static class Vector2Extensions
    {
        public static float AngleBetween(this Vector2 self, Vector2 point)
        {
            return FloatMath.Atan2(point.Y - self.Y, point.X - self.X);
        }

        public static float DistanceTo(this Vector2 self, Vector2 point)
        {
            var dist = point - self;
            
            return (float)(Math.Sqrt(Math.Pow(dist.X, 2) + Math.Pow(dist.Y, 2)));
        }
    }
}
