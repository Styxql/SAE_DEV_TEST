using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;

namespace SAE_DEV
{


    public class ScreenMenu : GameScreen
    {
        const int HEIGHT_MAIN_BUTTON = 70;
        const int WIDTH_MAIN_BUTTON = 200;
        const int SIZE_OTHER_BUTTON = 50;


        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        private Game1 _myGame;
        Color _colorButtonAudio;

        // texture du menu avec 3 boutons
        private Texture2D _buttonAudio;

        private Texture2D _buttonExit;
        private Texture2D _buttonInfo;
        private Texture2D _buttonPlay;
        private Texture2D _buttonSettings;
        private Texture2D _background;
        private Texture2D _buttonPlayPressed;
        private Texture2D _buttonMenu;
        private Texture2D _buttonMenuPressed;
        private Texture2D _buttonSettingsPressed;
        private Texture2D _buttonExitPressed;
        private Texture2D _buttonInfoPressed;
        private Texture2D _buttonAudioPressed;
        private Texture2D[] _buttons;
        private Texture2D[] _buttonsPressed;
        private Song _song;

        public bool _isButtonPlayPressed;
        private bool _estActif = true;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;

        public ScreenMenu(Game1 game) : base(game)
        {


            _myGame = game;
            lesBoutons = new Rectangle[5];
            lesBoutons[0] = new Rectangle(362, 50, 200, 70);
            lesBoutons[1] = new Rectangle(362, 150, 200, 70);
            lesBoutons[2] = new Rectangle(362, 250, 200, 70);
            lesBoutons[3] = new Rectangle(362, 350, 200, 70);
            lesBoutons[4] = new Rectangle(0, 0, 50, 50);
            //lesBoutons[4] = new Rectangle(865, 670, 640, 160);



        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferHeight = 720;
            _myGame._graphics.PreferredBackBufferWidth = 925;
            _myGame._graphics.ApplyChanges();


        }

        public override void LoadContent()
        {
            _buttonInfo = Content.Load<Texture2D>("InfoSquare Button");
            _buttonExit = Content.Load<Texture2D>("ExitButton");
            _buttonPlay = Content.Load<Texture2D>("PlayButton");
            _buttonAudio = Content.Load<Texture2D>("AudioSquareButton");
            _buttonMenuPressed = Content.Load<Texture2D>("ButtonMenuPressed");
            _buttonSettingsPressed = Content.Load<Texture2D>("BoutonSettingsPressed");
            _buttonAudioPressed = Content.Load<Texture2D>("AudioSquareButtonOff");
            _buttonSettings = Content.Load<Texture2D>("SettingsButton");
            _buttonPlayPressed = Content.Load<Texture2D>("PlayButtonPressed");
            _buttonMenu = Content.Load<Texture2D>("MenuButton");
            _background = Content.Load<Texture2D>("fondmenu");
            _buttonExitPressed = Content.Load<Texture2D>("ExitButtonPressed");

            _buttonsPressed = new Texture2D[5];
            _buttonsPressed[0] = _buttonPlayPressed;
            _buttonsPressed[1] = _buttonMenuPressed;
            _buttonsPressed[2] = _buttonSettingsPressed;
            _buttonsPressed[3] = _buttonExitPressed;
            _buttonsPressed[4] = _buttonAudioPressed;
            //_buttons[4] = _buttonSettings;
            //Bouton a l'état initial
            _buttons = new Texture2D[5];
            _buttons[0] = _buttonPlay;
            _buttons[1] = _buttonMenu;
            _buttons[2] = _buttonSettings;
            _buttons[3] = _buttonExit;
            _buttons[4] = _buttonAudio;


            _song = Content.Load<Song>("sonmenu");


            if (_estActif)
            {
                MediaPlayer.Play(_song);
            }
                    
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            MouseState _mouseState = Mouse.GetState();         

                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int i = 0; i < lesBoutons.Length; i++)
                    {
                        // si le clic correspond à un des 3 boutons
                        if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                        {
                            // on change l'état défini dans Game1 en fonction du bouton cliqué
                            if (i == 0)
                            {
                                _myGame.Etat = Game1.Etats.Play;
                                _estActif = false;
                            }
                            else if (i == 1)
                                _myGame.Etat = Game1.Etats.Menu;
                            else if (i == 2)
                                _myGame.Etat = Game1.Etats.Settings;
                            else if (i == 3)
                                _myGame.Etat = Game1.Etats.Exit;

                            ///MARCHE PAS ;(/////
                            //else if (i == 4)
                            //{
                            //    _isSoundOn = !_isSoundOn;

                            //    if (_isSoundOn)
                            //    {
                            //        _buttonAudio = _buttonAudio2;
                            //    }
                            //    else
                            //    {
                            //        _buttonAudio = _buttonAudioOff;
                            //    }

                            //}

                            break;
                        }

                    }
                }
                if(!_estActif)
                {
                    MediaPlayer.Stop();
                }
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_background, new Vector2(GraphicsDevice.Viewport.Width / 2 - (925 / 2), GraphicsDevice.Viewport.Height / 2 - (720 / 2)), Color.White);


            //_myGame.SpriteBatch.Draw(_buttonPlayPressed,new Vector2(362,55),Color.White);


            //_myGame.SpriteBatch.Draw(_buttonPlay, new Vector2(362, 50), Color.Red);

            //_myGame.SpriteBatch.Draw(_buttonSettings, new Vector2(362, 150), Color.Red);
            //_myGame.SpriteBatch.Draw(_buttonMenu, new Vector2(362, 250), Color.Red);

            //_myGame.SpriteBatch.Draw(_buttonExit, new Vector2(362, 350), Color.Red);
            ////_myGame.SpriteBatch.Draw(_buttonPlayPressed,  
            ////_myGame.SpriteBatch.Draw(_buttonInfo, new Vector2(0, 0), Color.Red);

            //_myGame.SpriteBatch.Draw(_buttonAudio, new Vector2(0, 0), Color.Red);


            MouseState _mouseState1 = Mouse.GetState();
            for (int i = 0; i < _buttons.Length; i++)
            {

                // Si la souris est au-dessus du bouton actuel
                if (lesBoutons[i].Contains(_mouseState1.X, _mouseState1.Y))
                {
                    // Affiche le bouton pressé
                    _myGame.SpriteBatch.Draw(_buttonsPressed[i], lesBoutons[i], Color.Red);
                }
                else
                {
                    // Affiche le bouton normal
                    _myGame.SpriteBatch.Draw(_buttons[i], lesBoutons[i], Color.White);
                }

            }
            _myGame.SpriteBatch.End();


        }


    }

}