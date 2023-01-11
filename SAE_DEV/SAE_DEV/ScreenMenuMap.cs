using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class ScreenMenuMap : GameScreen
    {
        public const int HAUTEUR_ECRAN = 720;
        public const int LARGEUR_ECRAN = 925;
        public const int SIZE_ICONE = 50;
        private Game1 _myGame;
        private Texture2D _textureBackgroundComingSoon;
        private Texture2D _textureBack;
        private Texture2D _textureBackPressed;
        private Vector2 _positionBack;
        private Rectangle _buttonBack;

        private bool _isHovered;

        public ScreenMenuMap(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferHeight = HAUTEUR_ECRAN;
            _myGame._graphics.PreferredBackBufferWidth = LARGEUR_ECRAN;
            _myGame._graphics.ApplyChanges();
            _positionBack = new Vector2(10, _myGame._graphics.PreferredBackBufferHeight - SIZE_ICONE * 2);
            _buttonBack = new Rectangle(10, _myGame._graphics.PreferredBackBufferHeight - SIZE_ICONE * 2, 50, 50);


        }
        public override void LoadContent()
        {
            _textureBackgroundComingSoon = Content.Load<Texture2D>("BackgroundComingSoon");
            _textureBack = Content.Load<Texture2D>("Back");
            _textureBackPressed = Content.Load<Texture2D>("BackPressed");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            MouseState _mouseState = Mouse.GetState();


            if (_buttonBack.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                _isHovered = true;


                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    _myGame.LoadScreen(Game1.Etats.Menu);

                }

            }
            else
                _isHovered = false;
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureBackgroundComingSoon,new Vector2(0,0), Color.White);
            if (_isHovered)
                _myGame.SpriteBatch.Draw(_textureBackPressed, _positionBack, Color.White);
            else
                _myGame.SpriteBatch.Draw(_textureBack, _positionBack, Color.White);

            _myGame.GraphicsDevice.Clear(Color.Yellow);
            _myGame.SpriteBatch.End();
        }
    }
}
