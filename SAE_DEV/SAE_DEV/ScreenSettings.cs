using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_DEV
{
    public class ScreenSettings : GameScreen
    {
        public const int SIZE_ICONE=50;

        private Game1 _myGame;
        private Texture2D _settingsBackground;
        private Texture2D _back;
        private Texture2D _backPressed;
        private Vector2 _positionBack;
        private Rectangle _buttonBack;
        private bool _isHovered;

        

        public ScreenSettings(Game1 game) : base(game)
        {
            _myGame = game;

        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferWidth = 925;
            _myGame._graphics.PreferredBackBufferHeight = 720;
            _myGame._graphics.ApplyChanges();
            _positionBack = new Vector2(10, _myGame._graphics.PreferredBackBufferHeight-SIZE_ICONE);
            _buttonBack = new Rectangle(10, _myGame._graphics.PreferredBackBufferHeight-SIZE_ICONE, 50, 50);
            _isHovered = false;


        }
        public override void LoadContent()
        {
            _settingsBackground = Content.Load<Texture2D>("SettingsBackground");
            _back = Content.Load<Texture2D>("Back");
            _backPressed = Content.Load<Texture2D>("BackPressed");
            

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState _mouseState = Mouse.GetState();
            //          

            if (_buttonBack.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                _isHovered = true;


                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    _myGame.Etat = Game1.Etats.Menu;

                }

            }
            else
                _isHovered = false;       
                

            
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_settingsBackground, new Vector2(GraphicsDevice.Viewport.Width / 2 - (_myGame._graphics.PreferredBackBufferWidth / 2), GraphicsDevice.Viewport.Height / 2 - (_myGame._graphics.PreferredBackBufferHeight / 2)), Color.White);
            if (_isHovered)
                _myGame.SpriteBatch.Draw(_backPressed, _positionBack, Color.Red);
            else
                _myGame.SpriteBatch.Draw(_back, _positionBack, Color.White);

            _myGame.SpriteBatch.End();




        }
    }
}
