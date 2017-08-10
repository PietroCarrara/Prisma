using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace Prisma
{
	/// <summary>
	/// Something in your game.
	/// </summary>
	public class Entity : IUpdateable, IDrawable
	{
		internal bool IsInitialized = false;

		public Entity Parent { get; private set; }

		public Scene Scene { get; internal set; }

		public EntityGroup Group { get; internal set; }

		private float rotation = 0;
		/// <summary>
		/// Rotation in degrees.
		/// </summary>
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
					rotation = value - Parent.Rotation;
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
					RelativePosition = value - Parent.Position;
				else
					RelativePosition = value;
			}
		}

		/// <summary>
		/// The X axis of the position.
		/// </summary>
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

		/// <summary>
		/// The Y axis of the position.
		/// </summary>
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

		/// <summary>
		/// Adds a child.
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="child">Entity to be added.</param>
		public T AddChild<T>(T child)
			where T : Entity
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

		/// <summary>
		/// Returns every child of given type
		/// </summary>
		/// <returns>The children.</returns>
		/// <typeparam name="T">The wanted type.</typeparam>
		public List<T> GetChildren<T>()
			where T : Entity
		{
			var list = new List<T>();

			foreach (var child in Children)
			{
				var c = child as T;
				if (c != null)
					list.Add(c);
			}

			if (!list.Any())
				throw new Exception();

			return list;
		}

		/// <summary>
		/// Gets the FIRST child of given type.
		/// </summary>
		/// <returns>The child, null if not found.</returns>
		/// <typeparam name="T">The wanted type.</typeparam>
		public T GetChild<T>()
			where T : Entity
		{
			foreach (var child in Children)
			{
				var c = child as T;
				if (c != null)
					return c;
			}

			return null;
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

		/// <summary>
		/// Called once per frame.
		/// </summary>
		public virtual void Update()
		{
			foreach (var child in Children)
				child.Update();

			flushDestroyQueue();
		}

		public float Depth { get; set; }
		/// <summary>
		/// Called every draw cicle
		/// </summary>
		/// <param name="camera">The camera to use while drawing.</param>
		public virtual void Draw(Camera camera)
		{
			foreach (var child in Children)
				child.Draw(camera);
		}

		/// <summary>
		/// Destroy this entity and it's children.
		/// </summary>
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

		/// <summary>
		/// Action to be taken when destroied.
		/// </summary>
		public virtual void OnDestroy()
		{ }
	}
}
