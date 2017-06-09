using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public class Camera : IUpdateable
    {
        public Vector2 Position = Vector2.Zero;

        public float Right
        {
            get
            {
                return PrismaGame.ScreenWidth + Position.X;
            }
        }

        public float Left
        {
            get
            {
                return Position.X;
            }
        }

        public float Top
        {
            get
            {
                return Position.Y;
            }
        }

        public float Bottom
        {
            get
            {
                return PrismaGame.ScreenHeight + Position.Y;
            }
        }

        public virtual void Draw(
            Texture2D texture,
            Vector2? position = default(Vector2?),
            Rectangle? destinationRectangle = default(Rectangle?),
            Rectangle? sourceRectangle = default(Rectangle?),
            Vector2? origin = default(Vector2?),
            Vector2? scale = default(Vector2?),
            Color? color = default(Color?),
            float rotation = 0,
            SpriteEffects effects = SpriteEffects.None,
            float layerDepth = 0)
        {
            PrismaGame.SpriteBatch.Draw(texture,
                position,
                destinationRectangle,
                sourceRectangle,
                origin,
                rotation,
                scale,
                color,
                effects,
                layerDepth);
        }

        public virtual void Update()
        {

        }
    }
}
