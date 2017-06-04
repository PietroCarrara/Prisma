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
            protected set => Graphics.Manager.PreferredBackBufferHeight = value;
        }

        public static int ScreenWidth
        {
            get => Graphics.Manager.PreferredBackBufferWidth;
            protected set => Graphics.Manager.PreferredBackBufferWidth = value;
        }

        public bool IsFullScreen
        {
            get => Graphics.Manager.IsFullScreen;
            set => Graphics.Manager.IsFullScreen = value;
        }

        private Scene CurrentScene;

        SpriteBatch spriteBatch;

        private static PrismaGame instance;

        public static Scene Scene
        {
            get
            {
                return instance.CurrentScene;
            }

            set
            {
                instance.CurrentScene = value;
            }
        }

        public PrismaGame()
        {
            Graphics.Manager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;

            ScreenWidth = 1280;
            ScreenHeight = 720;
        }

        public static void End()
        {
            instance.Exit();
        }

        protected override void Initialize()
        {
            base.Initialize();
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
            Input.Update();

            //Update Current Scene
            if (!CurrentScene.IsInitialized)
            {
                CurrentScene.Initialize();
                CurrentScene.IsInitialized = true;
            }

            CurrentScene.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            CurrentScene.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
