using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prisma
{
    public class PrismaGame : Game
    {
        public static bool IsPaused;

        public static int ScreenHeight
        {
            get => Graphics.Manager.PreferredBackBufferHeight;
            set => Graphics.Manager.PreferredBackBufferHeight = value;
        }

        public static int ScreenWidth
        {
            get => Graphics.Manager.PreferredBackBufferWidth;
            set => Graphics.Manager.PreferredBackBufferWidth = value;
        }

        public static bool IsFullScreen
        {
            get => Graphics.Manager.IsFullScreen;
            set => Graphics.Manager.IsFullScreen = value;
        }

        public static bool MouseVisible
        {
            get
            {
                return instance.IsMouseVisible;
            }

            set
            {
                instance.IsMouseVisible = value;
            }
        }

        private Scene CurrentScene;

        internal SpriteBatch spriteBatch { get; private set; }
        public static SpriteBatch SpriteBatch
        {
            get => instance.spriteBatch;
        }

        private static PrismaGame instance;

        public static Scene Scene
        {
            get
            {
                return instance.CurrentScene;
            }

            set
            {
                if (!value.IsInitialized)
                    value.Initialize();

                instance.CurrentScene = value;
            }
        }

        public PrismaGame(Scene scene) : base()
        {
            Graphics.Manager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;

            ScreenWidth = 1280;
            ScreenHeight = 720;

            CurrentScene = scene;
        }

        public static void End()
        {
            instance.Exit();
        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene = CurrentScene;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Graphics.Device);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // Update Systems
            Time.Update(gameTime);
            Keyboard.Update();
            Mouse.Update();

            CurrentScene.Update();

            Graphics.Manager.ApplyChanges();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            CurrentScene.Draw();

            spriteBatch.End();
        }
    }
}
