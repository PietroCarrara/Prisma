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

        public float Width, Height;

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
                if (Parent != null)
                    RelativePosition = Parent.Position - value;
                else
                    RelativePosition = value;
            }
        }

        public float X
        {
            get => Position.X;
            set => RelativePosition.X = value;
        }

        public float Y
        {
            get => Position.Y;
            set => RelativePosition.Y = value;
        }

        public float Bottom
        {
            get => Position.Y + Height;
            set => RelativePosition.Y = value - Height;
        }

        public float Right
        {
            get => Position.X + Width;
            set => RelativePosition.X = value - Width;
        }

        public Entity AddChild(Entity child)
        {
            child.Parent = this;

            Children.Add(child);

            return child;
        }

        public Entity(Vector2 pos, float width, float height)
        {
            RelativePosition = pos;
            Width = width;
            Height = height;
        }

        public Entity() : this(Vector2.Zero, 0, 0)
        { }

        public Entity(float x, float y, float width, float height) :
        this(new Vector2(y, x), width, height)
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
