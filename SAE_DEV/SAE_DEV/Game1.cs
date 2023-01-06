using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Audio;
using System.Threading;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch {get;private set;}
        public readonly ScreenManager _screenManager;
        private ScreenJeu _myScreenJeu;


        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Menu, Play, Settings, Exit };

        // on définit un champ pour stocker l'état en cours du jeu
        public Etats Etat;

        // on définit  2 écrans ( à compléter )
        private ScreenMenu _screenMenu;
        private ScreenJeu _screenJeu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // Par défaut, le 1er état flèche l'écran de menu
            Etat = Etats.Menu;

            // on charge les 2 écrans 
            _screenMenu = new ScreenMenu(this);
            _screenJeu = new ScreenJeu(this);
        }

        protected override void Initialize()
        {          
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
           

         
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _myScreenJeu = new ScreenJeu(this);
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black, 0));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // On teste le clic de souris et l'état pour savoir quelle action faire 
            MouseState _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Exit)
                    Exit();

                else if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_screenJeu, new FadeTransition(GraphicsDevice, Color.Black));

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //SpriteBatch.Begin();

            //SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}