using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	/// <summary>
	/// A group of entities
	/// </summary>
	public class EntityGroup : IEnumerable<Entity>
	{
		public string Name;

		internal Scene Scene;

		internal List<Entity> DestroyQueue = new List<Entity>();

		private List<Entity> entities = new List<Entity>();

		public EntityGroup(string groupName)
		{
			Name = groupName;
		}

		public int Count
		{
			get
			{
				return entities.Count;
			}
		}

		/// <summary>
		/// Adds a entity to the group.
		/// </summary>
		/// <returns>The entity.</returns>
		/// <param name="e">The entity.</param>
		public T AddEntity<T>(T e)
			where T : Entity
		{
			entities.Add(e);

			e.Scene = Scene;
			e.Group = this;

			if (!e.IsInitialized)
			{
				e.Initialize();
				e.IsInitialized = true;
			}

			return e;
		}

		internal void RemoveEntity(Entity e)
		{
			entities.Remove(e);
		}

		/// <summary>
		/// Gets the <see cref="T:Prisma.Entity"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public Entity this[int index]
		{
			get
			{
				return entities[index];
			}
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			return entities.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return entities.GetEnumerator();
		}
	}
}
