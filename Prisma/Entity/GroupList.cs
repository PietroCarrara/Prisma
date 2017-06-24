using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	public class GroupList : IEnumerable<EntityGroup>
	{
		private List<EntityGroup> list;

		internal Scene Scene;

		public GroupList()
		{
			list = new List<EntityGroup>();
		}

		public EntityGroup Add(EntityGroup group)
		{
			list.Add(group);

			group.Scene = Scene;

			return group;
		}

		public IEnumerator<EntityGroup> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public EntityGroup this[string name]
		{
			get
			{
				foreach (var group in list)
					if (group.Name == name)
						return group;

				throw new IndexOutOfRangeException("Group \"" + name + "\" not found");
			}
		}
	}
}
