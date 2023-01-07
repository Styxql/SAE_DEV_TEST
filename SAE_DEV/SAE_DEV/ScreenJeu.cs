using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Audio;
using System.Threading;
using System.Runtime.CompilerServices;
using System;
using System.Timers;
using MonoGame.Extended.Timers;

namespace SAE_DEV
{
    public class ScreenJeu : GameScreen
    {
        public const int HAUTEUR_VEHICULE_BASIQUE = 47;
        public const int LARGEUR_VEHICULE_BASIQUE = 85;
        public const int SIZE_JERIKAN = 50;
        public const int SIZE_HEART = 50;
        public const int LARGEUR_BARRE = 300;
        public const int HAUTEUR_BARRE = 30;
        //private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Texture2D _textureVoiturePolice;
        private Texture2D _textureAmbulance;
        private Texture2D _textureMiniTruck;
        private Texture2D _textureAudi;
        private Texture2D _textureMinivan;
        private Texture2D _textureVoitureBolide;
        private Texture2D _textureCar;
        private Texture2D _textureTruck;
        private AnimatedSprite _voitureJoueur;
        private SoundEffect _radio;
        private SoundEffect _radioON;
        private SoundEffect _radioOFF;

        private KeyboardState _keyboardState;

        private Vector2 _positionVoiture;
        private VoitureEnnemi ambulance;
        private VoitureEnnemi miniTruck;
        private VoitureEnnemi audi;
        private VoitureEnnemi minivan;
        private VoitureEnnemi voitureBolide;
        private VoitureEnnemi car;
        private VoitureEnnemi truck;
        private VoitureEnnemi taxi;
        private VoitureJoueur _joueur;
        private Vector2 _positionInitialVoitureEnnemi;
        private Texture2D _textureJerikan;


        private float _mapYPosition = 0;
        private float _vitesseYMap = 300;
        private float _angleVehicule;


        private int _directionVoiture;
        private int _vitesseVehicule;
        private int _maxPositionsX = 0;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _fond;
        private SpriteFont _police;
        private int _score;
        private float _barreEssence;
        private Vector2 _positionScore;
        private Vector2 _positionChrono;
        private int _chrono;
        private Texture2D _textureBarreEssence;
        private Texture2D _textureJaugeEssence;
        private Vector2 _positionJerikan;
        private Texture2D _textureBarreVie;
        private Texture2D _textureJaugeVie;
        private Texture2D _textureCoeur;
        Vector2 _positionCoeur;
        private Vector2 _positionBarreVie;
        private float _pointDeVie;

        private Game1 _myGame;
        // récup une ref à l'objet game qui permet d'accéder à ce qu'il y a dans Game1

        public ScreenJeu(Game1 game) : base(game)
        {

            Content.RootDirectory = "Content";
            game.IsMouseVisible = true;
            _myGame = game;

            
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _myGame._graphics.PreferredBackBufferWidth = 1920;
            _myGame._graphics.PreferredBackBufferHeight = 720;
            //_myGame._graphics.IsFullScreen = true;
            _myGame._graphics.ApplyChanges();
            _positionVoiture = new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height - HAUTEUR_VEHICULE_BASIQUE);
            _positionInitialVoitureEnnemi = new Vector2(100, 100);
            _directionVoiture = 1;
            _vitesseVehicule = 100;
            _angleVehicule = 0f;
            _positionScore = new Vector2(70, 0);
            _positionChrono = new Vector2(610, 0);
            _score = 0;
            _chrono = 60;
            _positionJerikan = new Vector2(20, _myGame._graphics.PreferredBackBufferHeight -SIZE_JERIKAN-5);
            //_positionBarreVie = new Vector2(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE);
            _positionCoeur = new Vector2(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE - SIZE_HEART - 20, _myGame._graphics.PreferredBackBufferHeight - SIZE_HEART - 5);
            _barreEssence = 100;
            _pointDeVie = 100;



            base.Initialize();
        }
        public override void LoadContent()
        {
            // _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureCar = Content.Load<Texture2D>("Car");
            _textureAmbulance = Content.Load<Texture2D>("Ambulance");
            _textureAudi = Content.Load<Texture2D>("Audi");
            _textureMiniTruck = Content.Load<Texture2D>("MiniTruck");
            _textureTruck = Content.Load<Texture2D>("Truck");
            _textureMinivan = Content.Load<Texture2D>("Minivan");
            _textureVoitureBolide = Content.Load<Texture2D>("Blackviper");
            _textureVoiturePolice = Content.Load<Texture2D>("Police");
            _textureAudi = Content.Load<Texture2D>("Audi");
            _textureBarreEssence = Content.Load<Texture2D>("barreEssence");
            _textureJaugeEssence = Content.Load<Texture2D>("JaugeEssence");
            _textureJerikan = Content.Load <Texture2D>("Jerikan");
            _textureBarreVie = Content.Load<Texture2D>("BarreVie");
            _textureJaugeVie = Content.Load<Texture2D>("JaugeVie");
            _textureCoeur = Content.Load<Texture2D>("heart");
            ////_radio = Content.Load<SoundEffect>("Son radio");
            //_radioOFF = Content.Load<SoundEffect>("radioTurnOff");
            //_radioON = Content.Load<SoundEffect>("radioTurnON");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("CarSprite2.sf", new JsonContentLoader());
            _voitureJoueur = new AnimatedSprite(spriteSheet);


            //////////////ENNEMI///////////////////////////
            ambulance = new VoitureEnnemi("Ambulance", 100, _positionInitialVoitureEnnemi, _textureAmbulance);
            audi = new VoitureEnnemi("audi", 100, _positionInitialVoitureEnnemi, _textureAudi);
            voitureBolide = new VoitureEnnemi("Voiture de Course", 100, _positionInitialVoitureEnnemi, _textureVoitureBolide);
            car = new VoitureEnnemi("Car", 100, _positionInitialVoitureEnnemi, _textureCar);
            miniTruck = new VoitureEnnemi("Car", 100, _positionInitialVoitureEnnemi, _textureMiniTruck);
            minivan = new VoitureEnnemi("MiniVan", 100, _positionInitialVoitureEnnemi, _textureMinivan);
            taxi = new VoitureEnnemi("Taxi", 100, _positionInitialVoitureEnnemi, _textureMinivan);
            truck = new VoitureEnnemi("Truck", 100, _positionInitialVoitureEnnemi, _textureMinivan);

            /////////////////////////////JOUEUR/////////////////////////////////////

            _joueur = new VoitureJoueur("Voiture Basique", 100, _positionVoiture, _voitureJoueur, 0);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _fond = Content.Load<Texture2D>("fondmenu");
            _police = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _myGame.Exit();

            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _barreEssence -= 1f * deltaSeconds;



            _keyboardState = Keyboard.GetState();

            _tiledMapRenderer.Update(gameTime);
            _voitureJoueur.Update(deltaSeconds);


            _joueur.DeplacementDroite(gameTime);
            _joueur.DeplacementGauche(gameTime);


            _mapYPosition += _vitesseYMap * deltaSeconds;
            _mapYPosition %= 1000;
        }

        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(viewMatrix: Matrix.CreateTranslation(0, _mapYPosition - 1000, 0));
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureVoiturePolice, _positionInitialVoitureEnnemi, Color.White);
            _myGame.SpriteBatch.Draw(_textureCar, _positionInitialVoitureEnnemi, Color.White);
            _myGame.SpriteBatch.Draw(_voitureJoueur, _positionVoiture, _angleVehicule);

            ///////BARRE ESSENCE///////
            int largeurBarreEssence = (int)(_barreEssence / 100 * _textureJaugeEssence.Width);

            Rectangle rectangleBarreEssence = new Rectangle(SIZE_JERIKAN + 50, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE - SIZE_JERIKAN / 3, largeurBarreEssence, HAUTEUR_BARRE);
            Rectangle rectangleJaugeEssence = new Rectangle(SIZE_JERIKAN+50, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE-SIZE_JERIKAN/3, LARGEUR_BARRE, HAUTEUR_BARRE);
            _myGame.SpriteBatch.Draw(_textureBarreEssence, rectangleJaugeEssence, Color.White);        
            _myGame.SpriteBatch.Draw(_textureJaugeEssence, rectangleBarreEssence, Color.White);
            _myGame.SpriteBatch.Draw(_textureJerikan,_positionJerikan,Color.White);
            ///////BARRE VIE///////
             int largeurBarreVie = (int)(_pointDeVie / 100 * _textureJaugeEssence.Width);
            Rectangle rectangleBarreVie = new Rectangle(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE-10, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE - SIZE_JERIKAN / 3, largeurBarreVie, HAUTEUR_BARRE);
            Rectangle rectangleJaugeVie = new Rectangle(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE-10, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE - SIZE_JERIKAN / 3, LARGEUR_BARRE, HAUTEUR_BARRE);
            _myGame.SpriteBatch.Draw(_textureBarreVie, rectangleBarreVie, Color.White);
            _myGame.SpriteBatch.Draw(_textureJaugeVie, rectangleJaugeVie, Color.White);
            _myGame.SpriteBatch.Draw(_textureCoeur, _positionCoeur, Color.White);
            _myGame.SpriteBatch.End();

            // TODO: Add your drawing code here
        }

        /////////////////////////////////RADIO(Phase de test son dégeu jsp pk)/////////////////////////////////////////////////////
        //if (_keyboardState.IsKeyDown(Keys.K))
        //{
        //    _radioON.Play();
        //    Thread.Sleep(5000);


        //    //_radio.Play();
        //}
        //else if (_keyboardState.IsKeyDown(Keys.L))
        //{

        //    _radioOFF.Play();
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }


}
