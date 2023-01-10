using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;


namespace SAE_DEV
{
    public class Screenchargement : GameScreen
    {
        public const int HAUTEUR_BARRE = 30;
        public const int LARGEUR_BARRE = 600;
        private Game1 _myGame;
        private Texture2D _textureBackground;
        Rectangle _rectangleJaugeVie;
        private int _largeurBarreChargement;
        private float _barreChargement;
        private Texture2D _textureJaugeChargement;


        public Screenchargement(Game1 game) : base(game)
        {
            _myGame = game;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferHeight = 720;
            _myGame._graphics.PreferredBackBufferWidth = 925;
            _myGame._graphics.ApplyChanges();

        }
        public override void LoadContent()
        {
            _textureBackground = Content.Load<Texture2D>("BackgroundChargement");
            _textureJaugeChargement = Content.Load<Texture2D>("BarreChargement");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
             _largeurBarreChargement= (int)(_barreChargement/ 100 * _textureJaugeChargement.Width);

            _rectangleJaugeVie = new Rectangle(_myGame._graphics.PreferredBackBufferWidth / 2, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE * 2, _largeurBarreChargement, HAUTEUR_BARRE);
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.SpriteBatch.Draw(_textureJaugeChargement, _rectangleJaugeVie, Color.White);

            _myGame.GraphicsDevice.Clear(Color.Yellow);
        }
    }
}
