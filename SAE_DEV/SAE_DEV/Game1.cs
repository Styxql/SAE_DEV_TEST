using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Matrix _tiledMapMatrix;
        private Texture2D _textureVoiturePolice;
        private Vector2 _positionVoiturePolice;





        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _positionVoiturePolice = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("newroute");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //_tiledMapMatrix = Matrix.CreateTranslation(-650, -400, 0) * Matrix.CreateScale(3);
            _textureVoiturePolice  = Content.Load<Texture2D>("Police");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);
            _spriteBatch.Begin();

            _spriteBatch.Draw(_textureVoiturePolice,_positionVoiturePolice,Color.White);

            _spriteBatch.End();

            _tiledMapRenderer.Draw();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}