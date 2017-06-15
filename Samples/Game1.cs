using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Prisma;
using Prisma.Prototyping;

namespace Samples
{
    public class Game1 : PrismaGame
    {
        public Game1() : base()
        {
            ScreenWidth = 1280;
            ScreenHeight = 720;

            IsMouseVisible = true;

            Scene = new MainScene();
        }
    }

    public class MainScene : Scene
    {
        private Scene parent;

        public MainScene(Scene parent = null)
        {
            if (parent != null)
                this.parent = parent;
            else
                this.parent = this;

            ClearColor = Color.Black;
        }

        public override void Initialize()
        {
            Groups.Add(new EntityGroup("shoots"));

            var gp = Groups.Add(new EntityGroup("squares"));

            var player = gp.AddEntity(new PlayerEntity());

            var e = gp.AddEntity(new Entity());
            e.Height = e.Width = 50;
            e.Position = new Vector2(200);
            e.AddChild(new PrototypeSprite(Color.Green));

            var cam = new DelayFollowCamera(player, 200);
            cam.UseBounds = false;

            Camera = cam;

            base.Initialize();
        }

        public override void Update()
        {
            base.Update();

            if (Prisma.Keyboard.IsKeyDown(Keys.Escape))
                PrismaGame.End();

            if (Prisma.Keyboard.IsKeyPressed(Keys.Space))
                PrismaGame.Scene = parent;
            else if (Prisma.Keyboard.IsKeyPressed(Keys.C))
                PrismaGame.Scene = new MainScene(this);

            if (Prisma.Keyboard.IsKeyPressed(Keys.P))
                PrismaGame.IsPaused = !PrismaGame.IsPaused;
        }
    }

    public class ShootEntity : Entity
    {
        public ShootEntity(Vector2 position, float rotation) : base(position, 20, 20)
        {
            Rotation = rotation;
            OriginPoint = OriginEnum.Center;
        }

        public override void Initialize()
        {
            base.Initialize();

            var r = new System.Random();

            var color = Color.FromNonPremultiplied(r.Next(256), r.Next(256), r.Next(256), 255);

            AddChild(new PrototypeSprite(color));
        }

        public override void Update()
        {
            base.Update();

            Right += 400 * FloatMath.Cos(RotationRadians) * Time.DeltaTime;
            Top += 400 * FloatMath.Sin(RotationRadians) * Time.DeltaTime;

            if (Left > Scene.Camera.Right || Right < Scene.Camera.Left || Bottom < Scene.Camera.Top || Top > Scene.Camera.Bottom)
            {
                Destroy();
            }
        }
    }

    public class PlayerEntity : Entity
    {
        private const int baseSpeed = 300;

        private int speed = baseSpeed;

        public PlayerEntity() : base(0, 0, 100, 50)
        {
            OriginPoint = OriginEnum.Center;

            Rotation = 0;
        }

        public override void Initialize()
        {
            base.Initialize();

            AddChild(new PrototypeSprite(Color.Red));
        }

        public override void Update()
        {
            base.Update();

            if (Prisma.Keyboard.IsKeyDown(Keys.LeftShift))
                speed = baseSpeed / 2;
            else
                speed = baseSpeed;

            if (Prisma.Keyboard.IsKeyDown(Keys.D))
                this.Left += speed * Time.DeltaTime;
            else if (Prisma.Keyboard.IsKeyDown(Keys.A))
                this.Left -= speed * Time.DeltaTime;

            if (Prisma.Keyboard.IsKeyDown(Keys.S))
                this.Top += speed * Time.DeltaTime;
            else if (Prisma.Keyboard.IsKeyDown(Keys.W))
                this.Top -= speed * Time.DeltaTime;

            RotationRadians = Position.AngleBetween(Prisma.Mouse.Position);

            if (Prisma.Mouse.IsButtonPressed(MouseButton.Left))
            {
                Scene.Groups["shoots"].AddEntity(new ShootEntity(Position, Rotation));
            }

            if (Prisma.Mouse.IsButtonDown(MouseButton.Right))
                Destroy();
        }
    }
}
