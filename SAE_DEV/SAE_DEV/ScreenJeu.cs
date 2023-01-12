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
using Microsoft.Xna.Framework.Media;

namespace SAE_DEV
{
    public class ScreenJeu : GameScreen
    {
        //constantes
        public const int HAUTEUR_VEHICULE_BASIQUE = 85;
        public const int LARGEUR_VEHICULE_BASIQUE = 85;
        public const int HAUTEUR_VEHICULE_GRAND = 105;
        public const int LARGEUR_VEHICULE_GRAND = 105;
        public const int HAUTEUR_VEHICULE_JOUEUR = 85;
        public const int LARGEUR_VEHICULE_JOUEUR = 85;

        public const int LARGEUR_BOUTON = 200;
        public const int HAUTEUR_BOUTON = 70;
        public const int POSITION_BOUTON_X = 360;

        public const int LARGEUR_ECRAN = 1600;
        public const int HAUTEUR_ECRAN = 800;

        private const int LARGEUR_VOIE = 192;

        private const float INTERVALLE_RESPAWN = 0.8f;
        private const float INTERVALLE_SPAWN_BONUS = 8f;
        private const float INTERVALLE_SPAWN_MALUS = 1.7f;

        private const int HAUTEUR_BARRE = 30;
        private const int LARGEUR_BARRE = 300;

        private const int TAILLE_ITEM = 50;
        private const int DECOR_MAP = 448;// taille des tuiles ciel, herbe et barriere en px : x * 32 = taille px
        private const int ESPACE_LIGNE = 25;//petit espace entre la route et la ligne

        private const int ESSENCE_MAX = 100;
        private const int VIE_MAX = 100;

        //Musique
        private Song _musiqueNuit;
        //Autre
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState _keyboardState;
        private Keys _keyPressed;

        //Champs tiled
        private TiledMap _tiledMapJour;
        private TiledMap _tiledMapNuit;
        private TiledMapRenderer _tiledMapRendererJour;
        private TiledMapRenderer _tiledMapRendererNuit;

        //Map
        private float _mapYPosition = 0;
        private float _vitesseYMap = 400;

        //Champs de listes et tableau
        private List<Texture2D> _textureEnnemies;
        private List<VoitureEnnemie> _lesVoituresEnnemies;
        private string[] _nomEnnemies = new string[] { "Car", "Ambulance", "miniTruck", "audi", "Blackviper", "truck", "taxi", "Police" };

        //Champs listes et tableau bonus
        private List<Texture2D> _textureBonus;
        private List<Items> _lesObjetsBonus;
        private string[] _nomBonus = new string[] { "Jerricane", "coins" };

        //Champs listes et tableau malus
        private List<Texture2D> _textureMalus;
        private List<Items> _lesObjetsMalus;
        private string[] _nomMalus = new string[] { "tache" };

        //radio et klaxon
        //private SoundEffect _radio;
        //private SoundEffect _radioON;
        //private SoundEffect _radioOFF;

        private SoundEffect _klaxon;
        private float _delaiKlaxon;
        private SoundEffect _effetMalus;

        //champs joueur
        private VoitureJoueur _joueur;

        //score et chrono
        private SpriteFont _police;
        private Texture2D _fond;
        private Texture2D _textureCoins;
        private int _score;
        private Vector2 _positionScore;
        private Vector2 _positionChrono;

        //barre d'essence
        private Texture2D _textureBarreEssence;
        private Texture2D _textureJaugeEssence;
        private Texture2D _textureJerricane;
        private float _barreEssence;
        private Vector2 _positionJerricane;
        Rectangle _rectangleBarreEssence;
        Rectangle _rectangleJaugeEssence;

        //Menu pause

        private Texture2D _textureButtonPlay;
        private Texture2D _textureButtonMenu;
        private Texture2D _textureButtonExit;
        private Texture2D _textureButtonSettings;  
        private Texture2D _textureButtonPlayPressed;
        private Texture2D _textureButtonExitPressed;
        private Texture2D _textureButtonMenuPressed;
        private Texture2D _textureButtonSettingsPressed;

        //barre de vie
        private Texture2D _textureBarreVie;
        private Texture2D _textureJaugeVie;
        private Texture2D _textureCoeur;
        private Texture2D _textureBackgroundGameOver;
        private Vector2 _positionCoeur;
        private Vector2 _positionBarreVie;
        private float _pointDeVie;
        private int _largeurBarreEssence;
        private int _largeurBarreVie;
        private float _delaiCollision;
        Rectangle _rectangleBarreVie;
        Rectangle _rectangleJaugeVie;
        
        //timer de spawn ennemie
        private float _timerRespawnEnnemie;

        //timer des spawn bonus
        private float _timerSpawnBonus;

        //timer des spawn bonus
        private float _timerSpawnMalus;

        //timer changement climat
        private float _timerClimat;

        //timer durée malus
        private float _timerDureeMalus;

        // temps de jeu
        private float _chrono;

        //pause
        private bool _estEntrainDeJouer = true;
        private float _dureeEnPause;
        private Rectangle[] lesBoutonsMenu;
        private Texture2D[] _buttons;
        private Texture2D[] _buttonsPressed;

        private Vector2 _positionBoutonPlay;
        private Vector2 _positionBoutonMenu;
        private Vector2 _positionBoutonSettings;
        private Vector2 _positionBoutonExit;

        public bool _estMort;


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
            _positionBoutonPlay =new Vector2(362, 50);

            _lesVoituresEnnemies = new List<VoitureEnnemie>();//création d'une liste sans ennemies
            _lesObjetsBonus = new List<Items>();
            _lesObjetsMalus = new List<Items>();
            _timerRespawnEnnemie = 0;
            _timerSpawnBonus = 0;
            _timerSpawnMalus = 0;
            _timerClimat = 0;
            _timerDureeMalus = 0.9f;
            _estMort = false;

            _joueur = new VoitureJoueur("joueur", 250, new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / 3,GraphicsDevice.Viewport.Height - HAUTEUR_VEHICULE_BASIQUE));
            
            _positionScore = new Vector2(70, 0);
            _positionChrono = new Vector2(610, 0);
            _score = 0;

            _positionJerricane = new Vector2(20, HAUTEUR_ECRAN - TAILLE_ITEM - 5);
            _positionCoeur = new Vector2(LARGEUR_ECRAN - LARGEUR_BARRE - TAILLE_ITEM - 20, HAUTEUR_ECRAN - TAILLE_ITEM - 5);
            
            _barreEssence = ESSENCE_MAX;
            _pointDeVie = VIE_MAX;
            _delaiCollision = 0;

            //_lesObjetsBonus = new List

            lesBoutonsMenu = new Rectangle[5];
            lesBoutonsMenu[0] = new Rectangle(362, 50, LARGEUR_BOUTON,HAUTEUR_BOUTON);
            lesBoutonsMenu[1] = new Rectangle(362, 150, LARGEUR_BOUTON,HAUTEUR_BOUTON);
            lesBoutonsMenu[2] = new Rectangle(362, 250,LARGEUR_BOUTON, HAUTEUR_BOUTON);
            lesBoutonsMenu[3] = new Rectangle(362, 350, LARGEUR_BOUTON,HAUTEUR_BOUTON);


            base.Initialize();
        }
        public override void LoadContent()
        {
            _tiledMapJour = Content.Load<TiledMap>("mapJour");
            _tiledMapNuit = Content.Load<TiledMap>("mapNuit");
            _tiledMapRendererJour = new TiledMapRenderer(GraphicsDevice, _tiledMapJour);
            _tiledMapRendererNuit = new TiledMapRenderer(GraphicsDevice, _tiledMapNuit);

            
            //Chargement des textures pour les ennemies
            _textureEnnemies = new List<Texture2D>();
            foreach (string nom in _nomEnnemies)
            {
                Texture2D texture = Content.Load<Texture2D>(nom);
                _textureEnnemies.Add(texture);
            }

            //Chargement des textures pour les bonus
            _textureBonus = new List<Texture2D>();
            foreach(string nom in _nomBonus)
            {
                Texture2D texture = Content.Load<Texture2D>(nom);
                _textureBonus.Add(texture);
            }

            //Chargement des textures pour les malus
            _textureMalus = new List<Texture2D>();
            foreach (string nom in _nomMalus)
            {
                Texture2D texture = Content.Load<Texture2D>(nom);
                _textureMalus.Add(texture);
            }

            //création de deux ennemies au démarrage
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

            //Barre de vie //Barre d'essence
            _textureBarreEssence = Content.Load<Texture2D>("barreEssence");
            _textureBarreVie = Content.Load<Texture2D>("BarreVie");
            _textureJaugeVie = Content.Load<Texture2D>("JaugeVie");
            _textureCoeur = Content.Load<Texture2D>("heart");
            _textureJaugeEssence = Content.Load<Texture2D>("JaugeEssence");
            _textureJerricane = Content.Load<Texture2D>("Jerricane");
            _textureButtonExit = Content.Load<Texture2D>("ExitButton");
            _textureButtonExitPressed = Content.Load<Texture2D>("ExitButtonPressed");
            _textureButtonMenuPressed = Content.Load<Texture2D>("ExitButton");
            _textureButtonMenu = Content.Load<Texture2D>("MenuButton");
            _textureButtonMenuPressed = Content.Load<Texture2D>("ButtonMenuPressed");
            _textureButtonSettings = Content.Load<Texture2D>("SettingsButton");
            _textureButtonSettingsPressed = Content.Load<Texture2D>("BoutonSettingsPressed");
            _textureButtonPlay = Content.Load<Texture2D>("PlayButton");
            _textureBackgroundGameOver = Content.Load<Texture2D>("BackgroundGameOver");
            _textureButtonPlayPressed = Content.Load<Texture2D>("PlayButtonPressed");
            _klaxon= Content.Load<SoundEffect>("klaxon");
            _effetMalus = Content.Load<SoundEffect>("siren");

            //_musiqueNuit=Content.Load<SoundEffect>("")

            //Autre
            _fond = Content.Load<Texture2D>("fondmenu");
            _police = Content.Load<SpriteFont>("Font");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _buttonsPressed = new Texture2D[4];
            _buttonsPressed[0] = _textureButtonPlayPressed;
            _buttonsPressed[1] = _textureButtonMenuPressed;
            _buttonsPressed[2] = _textureButtonSettingsPressed;
            _buttonsPressed[3] = _textureButtonExitPressed;

            //Bouton a l'état initial
            _buttons = new Texture2D[4];
            _buttons[0] = _textureButtonPlay;
            _buttons[1] = _textureButtonMenu;
            _buttons[2] = _textureButtonSettings;
            _buttons[3] = _textureButtonExit;

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
                _delaiKlaxon += deltaSeconds;
                _timerClimat += deltaSeconds;
                _timerDureeMalus += deltaSeconds;
                _chrono += deltaSeconds;

                _score = (int)Math.Round(_chrono, 0);

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


                //diminution de l'essence
                _barreEssence -= deltaSeconds;

                //Mise à jour de la map et défilement 
                _tiledMapRendererJour.Update(gameTime);

                //_tiledMapRendererNuit.Update(gameTime);
                _mapYPosition += _vitesseYMap * deltaSeconds;
                _mapYPosition %= 800;

                //Mise à jour du déplacememnt joueur
                _joueur.Sprite.Update(deltaSeconds);
 
                    if ((_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)) && _timerDureeMalus > 0.9f) || (_timerDureeMalus < 0.9f && _keyPressed == Keys.Right))
                    {
                        _joueur.DeplacementDroite(deltaSeconds);
                    }
                    else if ((_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)) && _timerDureeMalus > 0.9f) || (_timerDureeMalus < 0.9f && _keyPressed == Keys.Left))
                    {
                        _joueur.DeplacementGauche(deltaSeconds);
                    }
                    else
                    {
                        _joueur.Idle();
                    }

                if (_keyboardState.IsKeyDown(Keys.K) && _delaiKlaxon > 1)
                {
                    _klaxon.Play(0.1f,0,0);
                    _delaiKlaxon = 0;
                }
                //Spawn d'un ennemie et timer          
                _timerRespawnEnnemie += deltaSeconds;

                if (_timerRespawnEnnemie > INTERVALLE_RESPAWN)
                {
                    this.SpawnEnnemie();
                    _timerRespawnEnnemie -= INTERVALLE_RESPAWN;
                }

                //Spawn des bonus
                _timerSpawnBonus += deltaSeconds;
                if(_timerSpawnBonus > INTERVALLE_SPAWN_BONUS)
                {
                    this.SpawnBonus();
                    _timerSpawnBonus -= INTERVALLE_SPAWN_BONUS;
                }

                //Spawn des malus
                _timerSpawnMalus += deltaSeconds;
                if (_timerSpawnMalus > INTERVALLE_SPAWN_MALUS)
                {
                    this.SpawnMalus();
                    _timerSpawnMalus -= INTERVALLE_SPAWN_MALUS;
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

                //Déplacemement des bonus
                for(int i = 0; i < _lesObjetsBonus.Count; i++)
                {
                    Items bonus = _lesObjetsBonus[i];
                    bonus.Position = new Vector2(bonus.Position.X, bonus.Position.Y + bonus.Vitesse * deltaSeconds);
                    if(bonus.Position.Y > GraphicsDevice.Viewport.Height + _textureJerricane.Height)
                    {
                        _lesObjetsBonus.Remove(bonus);
                        i--;
                    }
                }


                //Déplacemement des Malus
                for (int i = 0; i < _lesObjetsMalus.Count; i++)
                {
                    Items malus = _lesObjetsMalus[i];
                    malus.Position = new Vector2(malus.Position.X, malus.Position.Y + malus.Vitesse * deltaSeconds);
                    if (malus.Position.Y > GraphicsDevice.Viewport.Height + TAILLE_ITEM)
                    {
                        _lesObjetsMalus.Remove(malus);
                        i--;
                    }
                }

                //delaiSecondes collision
                _delaiCollision += deltaSeconds;

                //appel methode
                this.CollisionItems();
                this.CollisionVehicule();



                //positions barre d'essence et barre de vie
                _largeurBarreEssence = (int)(_barreEssence / 100 * _textureJaugeEssence.Width);
                _largeurBarreVie = (int)(_pointDeVie / 100 * _textureJaugeVie.Width);

                //aspect de la barre d'essence
                _rectangleJaugeEssence = new Rectangle(TAILLE_ITEM + 50,HAUTEUR_ECRAN - HAUTEUR_BARRE - TAILLE_ITEM / 3, LARGEUR_BARRE, HAUTEUR_BARRE);
                _rectangleBarreEssence = new Rectangle(TAILLE_ITEM + 50,HAUTEUR_ECRAN - HAUTEUR_BARRE - TAILLE_ITEM / 3, _largeurBarreEssence, HAUTEUR_BARRE);
           
                //aspect de la barre de vie
                _rectangleBarreVie = new Rectangle(LARGEUR_ECRAN - LARGEUR_BARRE - 10, HAUTEUR_ECRAN - HAUTEUR_BARRE - TAILLE_ITEM / 3, LARGEUR_BARRE, HAUTEUR_BARRE);
                _rectangleJaugeVie = new Rectangle(LARGEUR_ECRAN - LARGEUR_BARRE - 10, HAUTEUR_ECRAN- HAUTEUR_BARRE - TAILLE_ITEM / 3, _largeurBarreVie, HAUTEUR_BARRE);


                if (_pointDeVie <= 0 || _barreEssence <= 0)
                {
                    _estMort = true;
                    _estEntrainDeJouer = false;
                    List<Classement> _lesClassements = Classement.ReadAll();
                    _lesClassements.Add(new Classement("joueur" + (_lesClassements.Count + 1), _score));
                    _lesClassements.Sort();
                    Classement.WriteAll(_lesClassements);
                    _myGame.LoadScreen(Game1.Etats.Lose);
                }
            }

            else
            {
                    MouseState _mouseState = Mouse.GetState();
                    //          
                    if (_mouseState.LeftButton == ButtonState.Pressed)
                    {
                        for (int i = 0; i < lesBoutonsMenu.Length; i++)
                        {
                            // si le clic correspond à un des 3 boutons
                            if (lesBoutonsMenu[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                            {
                                // on change l'état défini dans Game1 en fonction du bouton cliqué
                                if (i == 0)
                                    _estEntrainDeJouer = true;                    

                                else if (i == 1)
                                { 
                                     _myGame.Etat = Game1.Etats.Menu;
                                     _myGame.LoadScreen(Game1.Etats.Menu);                       
                                }
                                else if (i == 2)
                                {
                                     _myGame.Etat = Game1.Etats.Settings;
                                     _myGame.LoadScreen(Game1.Etats.Settings);
                                }
                                else if (i == 3)
                                {
                                    _myGame.Etat = Game1.Etats.Exit;
                                    _myGame.LoadScreen(Game1.Etats.Exit);
                                }

                                break;
                            }

                        }
           
                    }
                if (_dureeEnPause > 0.4 && _keyboardState.IsKeyDown(Keys.P) && _estMort == false)
                {
                    _estEntrainDeJouer = true;
                    _dureeEnPause = 0;
                }
            }
                                  



            
        }

        public override void Draw(GameTime gameTime)
        {
            
            if (_timerClimat > 240)
            {
                _timerClimat -= _timerClimat;
                
            } 
            else if(_timerClimat > 120)
            {
                _tiledMapRendererNuit.Draw(viewMatrix: Matrix.CreateTranslation(0, _mapYPosition - 800, 0));
            }
            else
            {
                _tiledMapRendererJour.Draw(viewMatrix: Matrix.CreateTranslation(0, _mapYPosition - 800, 0));
            }
           

            _myGame.SpriteBatch.Begin();

            foreach (Items malus in _lesObjetsMalus)
            {
                _myGame.SpriteBatch.Draw(malus.Texture, malus.Position, null, Color.White, 0,
                    new Vector2(TAILLE_ITEM, TAILLE_ITEM), 1f, SpriteEffects.None, 0);
            }

            foreach (Items bonus in _lesObjetsBonus)
            {
                _myGame.SpriteBatch.Draw(bonus.Texture, bonus.Position, null, Color.White, 0,
                    new Vector2(TAILLE_ITEM, TAILLE_ITEM), 1f, SpriteEffects.None, 0);
            }            

            foreach (VoitureEnnemie voiture in _lesVoituresEnnemies)
            {
                _myGame.SpriteBatch.Draw(voiture.Texture, voiture.Position, null, Color.White, voiture.Sens,
                    new Vector2(LARGEUR_VEHICULE_BASIQUE, HAUTEUR_VEHICULE_BASIQUE), 1.5f, SpriteEffects.None, 0);
            }

            float _angleSup = 0;
            if(_timerDureeMalus < 0.9f)
            {
                _angleSup = (2 * (float)Math.PI) * (_timerDureeMalus / 0.9f);
            }
            _myGame.SpriteBatch.Draw(_joueur.Sprite, _joueur.Position, _joueur.Angle + _angleSup);

            //_myGame.SpriteBatch.Draw()

            /////BARRE ESSENCE///////
            _myGame.SpriteBatch.Draw(_textureBarreEssence, _rectangleJaugeEssence, Color.White);
            _myGame.SpriteBatch.Draw(_textureJaugeEssence, _rectangleBarreEssence, Color.White);
            _myGame.SpriteBatch.Draw(_textureJerricane, _positionJerricane, Color.White);

            ///////BARRE VIE///////         
            _myGame.SpriteBatch.Draw(_textureBarreVie, _rectangleBarreVie, Color.White);
            _myGame.SpriteBatch.Draw(_textureJaugeVie, _rectangleJaugeVie, Color.White);
            _myGame.SpriteBatch.Draw(_textureCoeur, _positionCoeur, Color.White);
            if (!_estEntrainDeJouer)
            {
                MouseState _mouseState1 = Mouse.GetState();
                for (int i = 0; i < _buttons.Length; i++)
                {

                    // Si la souris est au-dessus du bouton actuel
                    if (lesBoutonsMenu[i].Contains(_mouseState1.X, _mouseState1.Y))
                    {
                        // Affiche le bouton pressé
                        _myGame.SpriteBatch.Draw(_buttonsPressed[i], lesBoutonsMenu[i], Color.Red);
                    }
                    else
                    {
                        // Affiche le bouton normal
                        _myGame.SpriteBatch.Draw(_buttons[i], lesBoutonsMenu[i], Color.White);
                    }

                }
            }

            _myGame.SpriteBatch.End();

            // TODO: Add your drawing code here
        }

        /// <summary>
        /// Génère un ennemie aléatoire et l'implémente à la liste _lesVoituresEnnemies
        /// </summary>
        public void SpawnEnnemie()
        {
            Random rand = new Random();

            int[] positionsX = new int[] { DECOR_MAP + ESPACE_LIGNE,
                                           DECOR_MAP + ESPACE_LIGNE + LARGEUR_VOIE ,
                                           LARGEUR_ECRAN - DECOR_MAP - LARGEUR_VOIE ,
                                           LARGEUR_ECRAN - DECOR_MAP - ESPACE_LIGNE};


            int i = rand.Next(0, _nomEnnemies.Length);//index du tableau
            int voie = rand.Next(0, positionsX.Length);//index de la voie

            int x = positionsX[voie];
            //x += rand.Next(0,150);//variation de positions dans les 4 voies

            float sens = 0;
            int vitesse = 600;

            if (x < GraphicsDevice.Viewport.Width / 2)
            {
                sens = (float)Math.PI;//rotation de 180 degrés
                vitesse = 750;//vitesse de déplacement si voie de gauche
            }
            //else de le remettre à 0 ne sert à rien
            int _coef = (int)(_chrono / 100);

            if(_chrono > _chrono + 30)
            {
                vitesse += rand.Next(500 * _coef, 1000 * _coef);//variation de la vitesse
            }

            VoitureEnnemie voiture = new VoitureEnnemie(_nomEnnemies[i], vitesse, new Vector2(x, 0), sens, _textureEnnemies[i]);
            _lesVoituresEnnemies.Add(voiture);

        }

        public void SpawnBonus()
        {
            Random rand = new Random();              

            int[] positionsX = new int[] { DECOR_MAP + ESPACE_LIGNE + LARGEUR_VEHICULE_GRAND,
                                          _myGame._graphics.GraphicsDevice.Viewport.Width - DECOR_MAP - LARGEUR_VEHICULE_GRAND };//posx bonus

            int i = rand.Next(0, _nomMalus.Length);
            int voie = rand.Next(0, positionsX.Length);

            int x = positionsX[voie];
            int vitesse = 400;

            Items itemBonus = new Items(_nomBonus[i], vitesse, new Vector2(x, 0), _textureBonus[i]);
            _lesObjetsBonus.Add(itemBonus);
        }

        public void SpawnMalus()            
        {
            Random rand = new Random();

            int[] positionsX = new int[] { DECOR_MAP + ESPACE_LIGNE + LARGEUR_VOIE ,
                                           LARGEUR_ECRAN - DECOR_MAP - LARGEUR_VOIE };//posx malus

            int i = rand.Next(0, _nomMalus.Length);
            int voie = rand.Next(0, positionsX.Length);

            int x = positionsX[voie];
            int vitesse = 400;

            Items itemMalus = new Items(_nomMalus[i], vitesse, new Vector2(x, 0), _textureMalus[i]);
            _lesObjetsMalus.Add(itemMalus);
        }

        public void CollisionVehicule()
        {
            if (_delaiCollision > 0.5)
            {
                Rectangle rect1 = new Rectangle((int)_joueur.Position.X, (int)_joueur.Position.Y, LARGEUR_VEHICULE_JOUEUR , HAUTEUR_VEHICULE_JOUEUR);
                foreach (VoitureEnnemie voiture in _lesVoituresEnnemies)
                {
                    Rectangle rect2 = new Rectangle((int)voiture.Position.X, (int)voiture.Position.Y, LARGEUR_VEHICULE_BASIQUE, HAUTEUR_VEHICULE_BASIQUE);
                    if (rect1.Intersects(rect2))
                    {
                        _pointDeVie -= 20;
                        _delaiCollision = 0;
                        _lesVoituresEnnemies.Remove(voiture);
                        break;
                    }
                }
            }
        }

        public void CollisionItems()
        {
            if (_delaiCollision > 0.5)
            {

                Rectangle rect1 = new Rectangle((int)_joueur.Position.X, (int)_joueur.Position.Y, LARGEUR_VEHICULE_JOUEUR, HAUTEUR_VEHICULE_JOUEUR);
                foreach (Items Bonus in _lesObjetsBonus)
                {
                    Rectangle rect2 = new Rectangle((int)Bonus.Position.X, (int)Bonus.Position.Y, TAILLE_ITEM, TAILLE_ITEM);
                    if (rect1.Intersects(rect2))
                    {
                       
                        _barreEssence += 10;
                        if(_barreEssence > ESSENCE_MAX)
                        {
                            _barreEssence = ESSENCE_MAX;
                        }
                        _lesObjetsBonus.Remove(Bonus);
                        break;
                    }
                }

                foreach (Items Malus in _lesObjetsMalus)
                {
                    Rectangle rect2 = new Rectangle((int)Malus.Position.X, (int)Malus.Position.Y, 228, 150);
                    if (rect1.Intersects(rect2))
                    {

                        if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
                        {
                            _keyPressed = Keys.Right;
                            _effetMalus.Play(0.1f, 0, 0);
                        }
                        else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
                        {
                            _keyPressed = Keys.Left;
                            _effetMalus.Play(0.1f, 0, 0);
                        }

                        _timerDureeMalus = 0;
                        break;
                        
                    }
                }

            }
        }

    }
   
}
