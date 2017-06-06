using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public static class FloatExtensions
    {
        public static float ToRadians(this float self)
        {
            return self * 0.01745329251f;
        }

        public static float ToDegrees(this float self)
        {
            return self * 57.2957795131f;
        }
    }
}

