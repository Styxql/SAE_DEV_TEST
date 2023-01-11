using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;

namespace SAE_DEV
{


    public class ScreenMenu : GameScreen
    {
        const int HAUTEUR_BOUTON_PRINCIPAL = 70;
        const int LARGEUR_BOUTON_PRINCIPAL = 200;
        public const int LARGEUR_ECRAN = 925;
        public const int HAUTEUR_ECRAN = 725;
        
        const int SIZE_OTHER_BUTTON = 60;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;

        // texture du menu avec 3 boutons
        private Texture2D _boutonAudio;

        private Texture2D _boutonExit;
        private Texture2D _BoutonInfo;
            
        private Texture2D _boutonPlay;
        private Texture2D _boutonSettings;
        private Texture2D _background;
        private Texture2D _boutonPlayPressé;
        private Texture2D _boutonMenu;
        private Texture2D _boutonMenuPressé;
        private Texture2D _boutonSettingsPressé;
        private Texture2D _boutonExitPressé;
        private Texture2D _boutonAudioPresse;
        private Texture2D _boutonClassement;
        private Texture2D[] _boutons;
        private Texture2D[] _boutonsPressés;
        private Song _sonMenu;
        bool _audioIsPressed;
        public enum Etats { Menu, Play, Settings, Exit, Lose };
        public Etats Etat;
        public bool _isButtonPlayPressed;
        public readonly ScreenManager _screenManager;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;

        public ScreenMenu(Game1 game) : base(game)
        {


            _myGame = game;
            lesBoutons = new Rectangle[6];
            lesBoutons[0] = new Rectangle(362, 50, 200, 70);
            lesBoutons[1] = new Rectangle(362, 150, 200, 70);
            lesBoutons[2] = new Rectangle(362, 250, 200, 70);
            lesBoutons[3] = new Rectangle(362, 350, 200, 70);
            lesBoutons[4] = new Rectangle(0, 0, 50, 50);
            lesBoutons[5]=new Rectangle(LARGEUR_ECRAN-SIZE_OTHER_BUTTON*2, 10, 50, 50);
            
            _screenManager = new ScreenManager();
            _myGame.Components.Add(_screenManager);
            //lesBoutons[4] = new Rectangle(865, 670, 640, 160);



        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferHeight = 720;
            _myGame._graphics.PreferredBackBufferWidth = 925;
            _myGame._graphics.ApplyChanges();
            _audioIsPressed = false;

        }

        public override void LoadContent()
        {
            _BoutonInfo = Content.Load<Texture2D>("InfoSquare Button");
            _boutonExit = Content.Load<Texture2D>("ExitButton");
            _boutonPlay = Content.Load<Texture2D>("PlayButton");
            _boutonAudio = Content.Load<Texture2D>("AudioSquareButton");
            _boutonMenuPressé = Content.Load<Texture2D>("ButtonMenuPressed");
            _boutonSettingsPressé = Content.Load<Texture2D>("BoutonSettingsPressed");
            _boutonAudioPresse = Content.Load<Texture2D>("AudioSquareButtonOff");
            _boutonSettings = Content.Load<Texture2D>("SettingsButton");
            _boutonPlayPressé = Content.Load<Texture2D>("PlayButtonPressed");
            _boutonMenu = Content.Load<Texture2D>("MenuButton");
            _background = Content.Load<Texture2D>("fondmenu");
            _boutonExitPressé = Content.Load<Texture2D>("ExitButtonPressed");
            _sonMenu = Content.Load<Song>("sonmenu");
            _boutonClassement = Content.Load<Texture2D>("Trophée");


            _boutonsPressés = new Texture2D[5];
            _boutonsPressés[0] = _boutonPlayPressé;
            _boutonsPressés[1] = _boutonMenuPressé;
            _boutonsPressés[2] = _boutonSettingsPressé;
            _boutonsPressés[3] = _boutonExitPressé;
            _boutonsPressés[4] = _boutonAudioPresse;
            
            //_buttons[4] = _buttonSettings;
            //Bouton a l'état initial
            _boutons = new Texture2D[5];
            _boutons[0] = _boutonPlay;
            _boutons[1] = _boutonMenu;
            _boutons[2] = _boutonSettings;
            _boutons[3] = _boutonExit;
            _boutons[4] = _boutonAudio;
            _boutons[5] = _boutonClassement;
            
            MediaPlayer.Play(_sonMenu);
            MediaPlayer.IsRepeating = true;
            

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
           
        MouseState _mouseState = Mouse.GetState();
            
            
                for (int i = 0; i < lesBoutons.Length ; i++)
                {
                if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))

                    // si le clic correspond à un des 3 boutons
                    if (_mouseState.LeftButton==ButtonState.Pressed)
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        if (i == 0)
                        {
                            _myGame.Etat = Game1.Etats.Play;
                            MediaPlayer.Stop();
                        }
                        else if (i == 1)
                            _myGame.Etat = Game1.Etats.MenuMap;
                        else if (i == 2)
                            _myGame.Etat = Game1.Etats.Settings;
                        else if (i == 3)
                            _myGame.Etat = Game1.Etats.Exit;
                        else if (i == 4)
                            _audioIsPressed = true;
                        else if (i == 5)
                            _myGame.Etat = Game1.Etats.Classement;
                        //_myGame.Etat=Game

                        else
                            //_audioIsPressed = false;

                            break;
                    }
                }
            
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_background, new Vector2(0,0), Color.White);

            MouseState _mouseState1 = Mouse.GetState();
            for (int i = 0; i < _boutons.Length-1; i++)
            {
                if (_audioIsPressed)
                {
                    _myGame.SpriteBatch.Draw(_boutonAudioPresse, lesBoutons[4], Color.Red);
                }
                else
                _myGame.SpriteBatch.Draw(_boutonAudio,lesBoutons[4], Color.White);

                // Si la souris est au-dessus du bouton actuel
                if (lesBoutons[i].Contains(_mouseState1.X, _mouseState1.Y))
                {
                    // Affiche le bouton pressé
                    _myGame.SpriteBatch.Draw(_boutonsPressés[i], lesBoutons[i], Color.Red);
                   

                }
                else
                {
                    // Affiche le bouton normal
                    _myGame.SpriteBatch.Draw(_boutons[i], lesBoutons[i], Color.White);
                }
               

            }
            _myGame.SpriteBatch.End();


        }
        

    }

}