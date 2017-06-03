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

        public float Rotation = 0;

        public Vector2 RelativePosition;

        public Rectangle Size;

        public List<Entity> Children { get; private set; } = new List<Entity>();

        public float RotationRadians
        {
            get
            {
                return Rotation * 0.01745329251f;
            }
            set
            {
                Rotation = value * 57.2957795131f;
            }
        }

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
                RelativePosition = value;
            }
        }

        public Entity AddChild(Entity child)
        {
            child.Parent = this;

            Children.Add(child);

            return child;
        }

        public Entity(Vector2 pos, Rectangle size)
        {
            RelativePosition = pos;
            Size = size;
        }

        public Entity() : this(Vector2.Zero, Rectangle.Empty)
        { }

        public Entity(int x, int y, int width, int height) :
        this(new Vector2(y, x), new Rectangle(0, 0, width, height))
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
