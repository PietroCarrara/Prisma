using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Prisma;
using Prisma.Prototyping;

namespace Samples
{
	public class Game1 : PrismaGame
	{
		public Game1() : base(new MainScene())
		{
			ScreenWidth = 1280;
			ScreenHeight = 720;

			IsMouseVisible = true;
		}
	}

	public class MainScene : Scene
	{
		private Scene parent;

		private DelayFollowCamera camera;

		private Entity player, green;

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

			player = gp.AddEntity(new PlayerEntity());

			green = gp.AddEntity(new Entity());
			green.Position = new Vector2(200);
			green.AddChild(new PrototypeSprite(Color.Green, 100, 100));

			camera = new DelayFollowCamera(player, 200);
			camera.UseBounds = false;

			Camera = camera;

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

			if (Prisma.Mouse.IsButtonPressed(MouseButton.Right))
			{
				if (camera.Entity == player)
					camera.Entity = green;
				else
					camera.Entity = player;
			}

			if (Prisma.Keyboard.IsKeyPressed(Keys.U))
			{
				var trans = new RightSlideTransition(this, this, .5f,
					() =>
					{
						green.Position = player.Position;
						ClearColor = Color.DodgerBlue;
					});

				PrismaGame.Scene = trans;
			}
		}

		public override void Draw()
		{
			base.Draw();
		}
	}

	public class ShootEntity : Entity
	{
		public ShootEntity(Vector2 position, float rotation) : base(position)
		{
			Rotation = rotation;
		}

		public override void Initialize()
		{
			base.Initialize();

			var r = new System.Random();

			var color = Color.FromNonPremultiplied(r.Next(256), r.Next(256), r.Next(256), 255);

			AddChild(new PrototypeSprite(color, 20, 20));
		}

		public override void Update()
		{
			base.Update();

			X += 400 * FloatMath.Cos(Rotation.ToRadians()) * Time.DeltaTime;
			Y += 400 * FloatMath.Sin(Rotation.ToRadians()) * Time.DeltaTime;

			if (X > Scene.Camera.Right || X < Scene.Camera.Left || Y < Scene.Camera.Top || Y > Scene.Camera.Bottom)
			{
				Destroy();
			}
		}
	}

	public class PlayerEntity : Entity
	{
		private const int baseSpeed = 300;

		private int speed = baseSpeed;

		public PlayerEntity() : base(0, 0)
		{ }

		public override void Initialize()
		{
			base.Initialize();

			AddChild(new PrototypeSprite(Color.Red, 100, 50));
		}

		public override void Update()
		{
			base.Update();

			if (Prisma.Keyboard.IsKeyDown(Keys.LeftShift))
				speed = baseSpeed / 2;
			else
				speed = baseSpeed;

			if (Prisma.Keyboard.IsKeyDown(Keys.D))
				this.X += speed * Time.DeltaTime;
			else if (Prisma.Keyboard.IsKeyDown(Keys.A))
				this.X -= speed * Time.DeltaTime;

			if (Prisma.Keyboard.IsKeyDown(Keys.S))
				this.Y += speed * Time.DeltaTime;
			else if (Prisma.Keyboard.IsKeyDown(Keys.W))
				this.Y -= speed * Time.DeltaTime;

			Rotation = Position.AngleBetween(Prisma.Mouse.Position).ToDegrees();

			if (Prisma.Mouse.IsButtonPressed(MouseButton.Left))
				Scene.Groups["shoots"].AddEntity(new ShootEntity(Position, Rotation));
		}
	}
}
