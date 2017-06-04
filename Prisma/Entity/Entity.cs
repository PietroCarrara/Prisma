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
                switch (value)
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

        public float Left
        {
            get => Position.X - Origin.X;
            set => Position = new Vector2(value + Origin.X, Position.Y);
        }

        public float Right
        {
            get => Position.X + Width - Origin.X;
            set => Position = new Vector2(value - Width + Origin.X, Position.Y);
        }

        public float Top
        {
            get => Position.Y - Origin.Y;
            set => Position = new Vector2(Position.X, value + Origin.Y);
        }

        public float Bottom
        {
            get => Position.Y + Height - Origin.Y;
            set => Position = new Vector2(Position.X, value - Height + Origin.Y);
        }

        public Entity AddChild(Entity child)
        {
            child.Parent = this;
            child.Group = Group;

            Children.Add(child);

            child.Initialize();
            return child;
        }

        public Entity RemoveChild(Entity child)
        {
            if (Children.Remove(child))
                return child;

            return null;
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
        /// it's parent.
        /// </summary>
        public virtual void Initialize()
        {
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

        public void Destroy()
        {
            while (Children.Any())
                Children[0].Destroy();

            OnDestroy();

            if (Group != null)
                Group.DestroyQueue.Add(this);
            else
                Parent.RemoveChild(this);
        }

        public virtual void OnDestroy()
        { }
    }
}
