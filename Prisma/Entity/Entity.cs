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
        public Entity Parent { get; private set; }

        public Vector2 Position
        {
            get
            {
                if (Parent != null)
                    return RelativePosition + Parent.Position;

                return RelativePosition;
            }
            set
            {
                RelativePosition += value;
            }
        }

        public Vector2 RelativePosition, Size;

        public List<Entity> Children { get; private set; } = new List<Entity>();

        public Entity AddChild(Entity child)
        {
            child.Parent = this;

            Children.Add(child);

            return child;
        }

        public Entity(Entity parent = null)
        {
            RelativePosition = new Vector2();
            Size = new Vector2();
        }

        public Entity(Vector2 pos, Vector2 size)
        {
            RelativePosition = pos;
            Size = size;
        }

        public Entity(int x, int y, int width, int height) :
        this(new Vector2(y, x), new Vector2(width, height))
        { }

        public virtual void Update()
        {
            foreach (var child in Children)
                child.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var child in Children)
                child.Draw(spriteBatch);
        }
    }
}
