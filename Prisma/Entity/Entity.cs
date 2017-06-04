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
        public enum OriginEnum
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
            Center,
            None
        }

        public Entity Parent { get; private set; }

        public Scene Scene { get; internal set; }

        public EntityGroup Group { get; internal set; }

        public float Rotation = 0;

        public Vector2 RelativePosition;

        public int Width, Height;

        private OriginEnum originPoint = OriginEnum.TopLeft;
        public OriginEnum OriginPoint
        {
            get => originPoint;
            set
            {
                switch(value)
                {
                    case OriginEnum.TopLeft:
                        Origin = new Vector2(0, 0);
                        break;
                    case OriginEnum.TopRight:
                        Origin = new Vector2(Width, 0);
                        break;
                    case OriginEnum.BottomLeft:
                        Origin = new Vector2(0, Height);
                        break;
                    case OriginEnum.BottomRight:
                        Origin = new Vector2(Width, Height);
                        break;
                    case OriginEnum.Center:
                        Origin = new Vector2(Width / 2, Height / 2);
                        break;
                }

                originPoint = value;
            }
        }

        private Vector2 origin = Vector2.Zero;
        public Vector2 Origin
        {
            get => origin;
            set
            {
                OriginPoint = OriginEnum.None;
                origin = value;
            }
        }

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

        public Entity(Vector2 pos, int width, int height)
        {
            RelativePosition = pos;
            Width = width;
            Height = height;
        }

        public Entity() : this(Vector2.Zero, 0, 0)
        { }

        public Entity(float x, float y, int width, int height) :
        this(new Vector2(y, x), width, height)
        { }

        /// <summary>
        /// In this method the entity already has access to
        /// it's parent. Base should be called AFTER your logic.
        /// </summary>
        public virtual void Initialize()
        {
            foreach (var child in Children)
                child.Initialize();
        }

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
