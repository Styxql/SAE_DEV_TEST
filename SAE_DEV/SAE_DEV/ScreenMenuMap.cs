using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Game1 _myGame;
        private Texture2D _textureBackgroundComingSoon;
        private bool _ishovered;

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

        }
        public override void LoadContent()
        {
            _textureBackgroundComingSoon = Content.Load<Texture2D>("BackgroundComingSoon");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureBackgroundComingSoon,new Vector2(0,0), Color.White);
            //if (_isHovered)
            //    _myGame.SpriteBatch.Draw(_backPressed, _positionBack, Color.Red);
            //else
            //    _myGame.SpriteBatch.Draw(_back, _positionBack, Color.White);

            _myGame.GraphicsDevice.Clear(Color.Yellow);
            _myGame.SpriteBatch.End();
        }
    }
}
