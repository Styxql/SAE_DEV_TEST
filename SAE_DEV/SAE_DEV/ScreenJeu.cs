using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace SAE_DEV
{
    public class ScreenJeu : GameScreen
    {
        //constantes
        public const int HAUTEUR_VEHICULE_BASIQUE = 47;
        public const int LARGEUR_VEHICULE_BASIQUE = 85;
        private const int INTERVALLE_RESPAWN = 1;

        //Autre
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState _keyboardState;

        //Champs tiled
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        //Map
        private float _mapYPosition = 0;
        private float _vitesseYMap = 300;

        //Champs de listes et tableau
        private List<Texture2D> _textureEnnemies;
        private List<VoitureEnnemie> _lesVoituresEnnemies;
        private string[] nomEnnemies = new string[] { "Car", "Ambulance", "miniTruck", "audi", "Blackviper", "truck", "taxi", "Police" };

        //radio
        private SoundEffect _radio;
        private SoundEffect _radioON;
        private SoundEffect _radioOFF;

        //champs joueur
        private VoitureJoueur _joueur;

        //score et chrono
        private SpriteFont _police;
        private Texture2D _fond;
        private Texture2D _coins;
        private int _score;
        private int _chrono;
        private Vector2 _positionScore;
        private Vector2 _positionChrono;

        //barre d'essence
        private Texture2D _textureBarreEssence;
        private Texture2D _textureJaugeEssence;
        private Vector2 _positionJerikan;
        private float _barreEssence;

        //barre de vie
        private Texture2D _textureBarreVie;
        private Texture2D _textureJaugeVie;
        private Texture2D _textureCoeur;
        private Vector2 _positionCoeur;
        private Vector2 _positionBarreVie;
        private float _pointDeVie;

        //timer de spawn ennemie
        private float _timerRespawnEnnemie;

        //pause
        private bool _estEntrainDeJouer = true;
        private float _dureeEnPause;


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
            _myGame._graphics.PreferredBackBufferWidth = 1600;
            _myGame._graphics.PreferredBackBufferHeight = 800;
            _myGame._graphics.ApplyChanges();

            _lesVoituresEnnemies = new List<VoitureEnnemie>();//création d'une liste sans ennemies
            _timerRespawnEnnemie = 0;

            _joueur = new VoitureJoueur("joueur", 250, new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / 3,
                GraphicsDevice.Viewport.Height - HAUTEUR_VEHICULE_BASIQUE));

            _positionScore = new Vector2(70, 0);
            _positionChrono = new Vector2(610, 0);
            _score = 0;
            _chrono = 60;

            //_positionJerikan = new Vector2(20, _myGame._graphics.PreferredBackBufferHeight - SIZE_JERIKAN-5);
            ////_positionBarreVie = new Vector2(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE);
            //_positionCoeur = new Vector2(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE - SIZE_HEART - 20, _myGame._graphics.PreferredBackBufferHeight - SIZE_HEART - 5);
            //_barreEssence = 100;
            //_pointDeVie = 100;


            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            //Chargement des textures pour les ennemies
            _textureEnnemies = new List<Texture2D>();
            foreach (string nom in nomEnnemies)
            {
                Texture2D texture = Content.Load<Texture2D>(nom);
                _textureEnnemies.Add(texture);
            }

            //créaation de deux ennemies au démarrage
            for (int i = 0; i < 2; i++)
            {
                this.SpawnEnnemie();
            }

            //_radio = Content.Load<SoundEffect>("Son radio");
            //_radioOFF = Content.Load<SoundEffect>("radioTurnOff");
            //_radioON = Content.Load<SoundEffect>("radioTurnON");

            //Chargement du joueur
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("CarSprite2.sf", new JsonContentLoader());
            _joueur.Sprite = new AnimatedSprite(spriteSheet);

            //Autre
            _fond = Content.Load<Texture2D>("fondmenu");
            _police = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _dureeEnPause += deltaSeconds;

            _keyboardState = Keyboard.GetState();

            if (_estEntrainDeJouer)
            {

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    _myGame.Exit();
                else if (_keyboardState.IsKeyDown(Keys.Space)) //pour debug
                {
                    Console.WriteLine("position Joueur en X:" + _joueur.Position.X);
                }
                else if (_keyboardState.IsKeyDown(Keys.P) && _dureeEnPause > 0.4)
                {
                    _estEntrainDeJouer = false;
                    _dureeEnPause = 0;
                }

                //autre
                _tiledMapRenderer.Update(gameTime);


                //diminution de l'essence
                _barreEssence -= 1 * deltaSeconds;


                //Mise à jour de la map et défilement 
                _tiledMapRenderer.Update(gameTime);
                _mapYPosition += _vitesseYMap * deltaSeconds;
                _mapYPosition %= 1000;


                //Mise à jour du déplacememnt joueur
                _joueur.Sprite.Update(deltaSeconds);

                if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
                {
                    _joueur.DeplacementDroite(deltaSeconds);
                }
                else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
                {
                    _joueur.DeplacementGauche(deltaSeconds);
                }
                else
                {
                    _joueur.Idle();
                }

                //Spawn d'un ennemie et timer          
                _timerRespawnEnnemie += deltaSeconds;

                if (_timerRespawnEnnemie > INTERVALLE_RESPAWN)
                {
                    this.SpawnEnnemie();
                    _timerRespawnEnnemie -= INTERVALLE_RESPAWN;
                }

                //Déplacement des ennemies
                for (int i = 0; i < _lesVoituresEnnemies.Count; i++)
                {
                    VoitureEnnemie voiture = _lesVoituresEnnemies[i];
                    voiture.Position = new Vector2(voiture.Position.X, voiture.Position.Y + voiture.Vitesse * deltaSeconds);
                    if (voiture.Position.Y > GraphicsDevice.Viewport.Height + HAUTEUR_VEHICULE_BASIQUE)
                    {
                        _lesVoituresEnnemies.Remove(voiture);
                        i--;//permet de vérifier les voitures toujours existantes 
                    }
                }
            }

            else
            {
                if (_dureeEnPause > 0.4 && _keyboardState.IsKeyDown(Keys.P))
                {
                    _estEntrainDeJouer = true;
                    _dureeEnPause = 0;
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(viewMatrix: Matrix.CreateTranslation(0, _mapYPosition - 1000, 0));

            _myGame.SpriteBatch.Begin();

            foreach (VoitureEnnemie voiture in _lesVoituresEnnemies)
            {
                _myGame.SpriteBatch.Draw(voiture.Texture, voiture.Position, null, Color.White, voiture.Sens,
                    new Vector2(LARGEUR_VEHICULE_BASIQUE, HAUTEUR_VEHICULE_BASIQUE), 1.3f, SpriteEffects.None, 0);
            }

            _myGame.SpriteBatch.Draw(_joueur.Sprite, _joueur.Position, _joueur.Angle);

            ///////BARRE ESSENCE///////
            //int largeurBarreEssence = (int)(_barreEssence / 100 * _textureJaugeEssence.Width);
            //Rectangle rectangleBarreEssence = new Rectangle(SIZE_JERIKAN + 50, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE - SIZE_JERIKAN / 3, largeurBarreEssence, HAUTEUR_BARRE);
            //Rectangle rectangleJaugeEssence = new Rectangle(SIZE_JERIKAN+50, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE-SIZE_JERIKAN/3, LARGEUR_BARRE, HAUTEUR_BARRE);
            //_myGame.SpriteBatch.Draw(_textureBarreEssence, rectangleJaugeEssence, Color.White);        
            //_myGame.SpriteBatch.Draw(_textureJaugeEssence, rectangleBarreEssence, Color.White);
            //_myGame.SpriteBatch.Draw(_textureJerikan,_positionJerikan,Color.White);

            /////////BARRE VIE///////
            // int largeurBarreVie = (int)(_pointDeVie / 100 * _textureJaugeEssence.Width);
            //Rectangle rectangleBarreVie = new Rectangle(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE-10, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE - SIZE_JERIKAN / 3, largeurBarreVie, HAUTEUR_BARRE);
            //Rectangle rectangleJaugeVie = new Rectangle(_myGame._graphics.PreferredBackBufferWidth - LARGEUR_BARRE-10, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE - SIZE_JERIKAN / 3, LARGEUR_BARRE, HAUTEUR_BARRE);
            //_myGame.SpriteBatch.Draw(_textureBarreVie, rectangleBarreVie, Color.White);
            //_myGame.SpriteBatch.Draw(_textureJaugeVie, rectangleJaugeVie, Color.White);
            //_myGame.SpriteBatch.Draw(_textureCoeur, _positionCoeur, Color.White);

            _myGame.SpriteBatch.End();

            // TODO: Add your drawing code here
        }

        /// <summary>
        /// Génère un ennemie aléatoire et l'implémente à la liste _lesVoituresEnnemies
        /// </summary>
        public void SpawnEnnemie()
        {
            Random rand = new Random();

            int[] positionsX = new int[] { 500, 700, 900, 1100 };//

            int i = rand.Next(0, nomEnnemies.Length - 1);//index du tableau
            int voie = rand.Next(0, positionsX.Length - 1);//index de la voie

            int x = positionsX[voie];
            x += rand.Next(0, 150);//assigne une position aléatoire dans l'une des 4 voies

            float sens = 0;
            int vitesse = 250;

            if (x < GraphicsDevice.Viewport.Width / 2)
            {
                sens = (float)Math.PI;//rotation de 180 degrés
                vitesse = 300;//vitesse de déplacement si voie de gauche
            }
            //else de le remettre à 0 ne sert à rien

            vitesse += rand.Next(0, 60);//variation de la vitesse

            VoitureEnnemie voiture = new VoitureEnnemie(nomEnnemies[i], vitesse, new Vector2(x, 0), sens, _textureEnnemies[i]);
            _lesVoituresEnnemies.Add(voiture);

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
