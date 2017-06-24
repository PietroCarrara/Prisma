using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

		internal bool IsInitialized = false;

		public Entity Parent { get; private set; }

		public Scene Scene { get; internal set; }

		public EntityGroup Group { get; internal set; }

		private float rotation = 0;
		public float Rotation
		{
			get
			{
				if (Parent != null)
					return Parent.Rotation + rotation;
				else
					return rotation;
			}
			set
			{
				if (Parent != null)
					Rotation = value - Parent.Rotation;
				else
					rotation = value;
			}
		}

		public Vector2 RelativePosition;

		public int Width, Height;

		private OriginEnum originPoint = OriginEnum.TopLeft;
		public OriginEnum OriginPoint
		{
			get
			{
				return originPoint;
			}
			set
			{
				switch (value)
				{
					case OriginEnum.TopLeft:
						origin = new Vector2(0, 0);
						break;
					case OriginEnum.TopRight:
						origin = new Vector2(Width, 0);
						break;
					case OriginEnum.BottomLeft:
						origin = new Vector2(0, Height);
						break;
					case OriginEnum.BottomRight:
						origin = new Vector2(Width, Height);
						break;
					case OriginEnum.Center:
						origin = new Vector2(Width / 2, Height / 2);
						break;
				}

				originPoint = value;
			}
		}

		private Vector2 origin = Vector2.Zero;
		public Vector2 Origin
		{
			get
			{
				return origin;
			}
			set

			{
				OriginPoint = OriginEnum.None;
				origin = value;
			}
		}


		public List<Entity> Children { get; private set; } = new List<Entity>();
		private List<Entity> RemoveQueue = new List<Entity>();

		public float RotationRadians
		{
			get
			{
				return Rotation.ToRadians();
			}
			set
			{
				Rotation = value.ToDegrees();
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
			get
			{
				return Position.X - Origin.X;
			}
			set
			{
				Position = new Vector2(value + Origin.X, Position.Y);
			}
		}


		public float Right
		{
			get
			{
				return Position.X + Width - Origin.X;
			}
			set
			{
				Position = new Vector2(value - Width + Origin.X, Position.Y);
			}
		}


		public float Top
		{
			get
			{
				return Position.Y - Origin.Y;
			}
			set
			{
				Position = new Vector2(Position.X, value + Origin.Y);
			}
		}


		public float Bottom
		{
			get
			{
				return Position.Y + Height - Origin.Y;
			}
			set
			{
				Position = new Vector2(Position.X, value - Height + Origin.Y);
			}
		}


		public Entity AddChild(Entity child)
		{
			child.Parent = this;
			child.Group = Group;

			Children.Add(child);

			if (!child.IsInitialized)
			{
				child.Initialize();
				child.IsInitialized = true;
			}

			return child;
		}

		public Entity RemoveChild(Entity child)
		{
			RemoveQueue.Add(child);

			return child;
		}

		public EntityGroup DetatchFromParent()
		{
			RelativePosition = Position;
			rotation = Rotation;

			Parent.RemoveChild(this);
			Parent = null;

			Group.AddEntity(this);

			return Group;
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
		{ }

		public virtual void Update()
		{
			foreach (var child in Children)
				child.Update();

			flushDestroyQueue();
		}

		public virtual void Draw(Camera camera)
		{
			foreach (var child in Children)
				child.Draw(camera);
		}

		public void Destroy()
		{
			foreach (var child in Children)
				child.Destroy();

			OnDestroy();

			if (Parent != null)
				Parent.RemoveChild(this);
			else
				Group.DestroyQueue.Add(this);
		}

		private void flushDestroyQueue()
		{
			while (RemoveQueue.Any())
			{
				Children.Remove(RemoveQueue[0]);
				RemoveQueue.RemoveAt(0);
			}
		}

		public virtual void OnDestroy()
		{ }
	}
}
