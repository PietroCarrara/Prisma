using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public static class Time
    {
        public static GameTime GameTime { get; private set; }

        internal static void Update(GameTime time)
        {
            GameTime = time;
        }

        public static float DeltaTime
        {
            get
            {
                return (float)GameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
