using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma
{
    public class FollowCamera : Camera
    {
        private Entity e;

        public bool UseBounds;

        public float MinHeight, MinWidth, MaxHeight, MaxWidth;

        public FollowCamera(Entity e)
        {
            this.e = e;
        }

        public override void Update()
        {
            Position = e.Position;

            Position.X -= PrismaGame.ScreenWidth / 2;
            Position.Y -= PrismaGame.ScreenHeight / 2;

            if (!UseBounds)
                return;

            if (Position.X < MinWidth)
                Position.X = MinWidth;
            else if (Position.X > MaxWidth)
                Position.X = MaxWidth;

            if (Position.Y < MinHeight)
                Position.Y = MinHeight;
            else if (Position.Y > MaxHeight)
                Position.Y = MaxHeight;
        }

        public override void Draw(Texture2D texture, Vector2? position = default(Vector2?), Rectangle? destinationRectangle = default(Rectangle?), Rectangle? sourceRectangle = default(Rectangle?), Vector2? origin = default(Vector2?), Vector2? scale = default(Vector2?), Color? color = default(Color?), float rotation = 0, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            if (position != null)
                position -= Position;

            base.Draw(texture, position, destinationRectangle, sourceRectangle, origin, scale, color, rotation, effects, layerDepth);
        }
    }
}
