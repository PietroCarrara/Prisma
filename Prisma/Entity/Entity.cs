using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public class Entity : IUpdateable, IDrawable
    {
        public Vector2 Position, Size;

        public Entity()
        {
            Position = new Vector2();
            Size = new Vector2();
        }

        public Entity(Vector2 pos, Vector2 size)
        {
            Position = pos;
            Size = size;
        }

        public Entity(int x, int y, int width, int height) :
        this(new Vector2(y, x), new Vector2(width, height))
        { }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
