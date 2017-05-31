using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public static class Graphics
    {
        public static GraphicsDeviceManager Manager { get; private set; }

        public static GraphicsDevice Device => Manager.GraphicsDevice;

        internal static void Setup(GraphicsDeviceManager manager)
        {
            Manager = manager;
        }
    }
}
