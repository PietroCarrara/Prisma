using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public class EntityGroup : IEnumerable<Entity>
    {
        public string Name;

        private List<Entity> entities = new List<Entity>();

        public EntityGroup(string groupName)
        {
            Name = groupName;
        }

        public void AddEntity(Entity e)
        {
            entities.Add(e);
        }

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
