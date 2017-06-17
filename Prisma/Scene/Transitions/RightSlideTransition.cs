using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public class RightSlideTransition : Transition
    {
        private Vector2 prevPos, nextPos;

        private float speed;

        public RightSlideTransition(Scene previous, Scene next, float duration = .5f, Action modifier = null) :
        base(previous, next, modifier)
        {
            nextPos = new Vector2(PrismaGame.ScreenWidth, 0);

            speed = PrismaGame.ScreenWidth / duration;
        }

        public override void Update()
        {
            base.Update();

            prevPos.X -= speed * Time.DeltaTime;

            nextPos.X -= speed * Time.DeltaTime;

            if (nextPos.X <= 0)
                End();
        }

        public override void Draw()
        {
            Camera.Draw(prevRt, prevPos);

            Camera.Draw(nextRt, nextPos);
        }
    }
}
