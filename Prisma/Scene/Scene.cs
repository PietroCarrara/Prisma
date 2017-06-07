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

        public Color ClearColor = Color.CornflowerBlue;

        internal bool IsInitialized = false;

        public Scene()
        {
            Groups.Scene = this;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update()
        {
            // If the game is paused, don't update
            if (PrismaGame.IsPaused)
                return;

            foreach (var group in Groups)
                foreach (var ent in group)
                    ent.Update();

            foreach(var group in Groups)
                while(group.DestroyQueue.Any())
                {
                    group.RemoveEntity(group.DestroyQueue[0]);
                    group.DestroyQueue.RemoveAt(0);
                }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Graphics.Device.Clear(ClearColor);

            foreach (var group in Groups)
                foreach (var ent in group)
                    ent.Draw(spriteBatch);
        }
    }
}
