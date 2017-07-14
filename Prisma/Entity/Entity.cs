using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

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

		public List<Entity> Children { get; private set; } = new List<Entity>();
		private List<Entity> RemoveQueue = new List<Entity>();

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
			get
			{
				return Position.X;
			}
			set
			{
				Position = new Vector2(value, this.Y);
			}
		}

		public float Y
		{
			get
			{
				return Position.Y;
			}
			set
			{
				Position = new Vector2(this.X, value);
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

		public Entity(Vector2 pos)
		{

			RelativePosition = pos;
		}

		public Entity() : this(Vector2.Zero)
		{ }

		public Entity(float x, float y) :
		this(new Vector2(y, x))
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

		public float Depth { get; set; }
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
				Parent.RemoveQueue.Add(this);
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
