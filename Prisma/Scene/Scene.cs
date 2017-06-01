using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public class Scene : IUpdateable, IDrawable
    {
        public GroupList Groups { get; private set; } = new GroupList();

        internal bool IsInitialized = false;

        public virtual void Initialize()
        {

        }

        public virtual void Update()
        {
            foreach (var group in Groups)
                foreach (var ent in group)
                    ent.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Graphics.Device.Clear(Color.CornflowerBlue);

            foreach (var group in Groups)
                foreach (var ent in group)
                    ent.Draw(spriteBatch);
        }
    }
}
