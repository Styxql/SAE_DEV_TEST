using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch { get; private set; }
        public readonly ScreenManager _screenManager;

        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Play, Menu, MenuMap, Settings, Exit, Lose, Classement };

        // on définit un champ pour stocker l'état en cours du jeu

        // on définit  2 écrans ( à compléter )
        //public ScreenMenu _screenMenu;
        //public ScreenJeu _screenJeu;
        //public ScreenSettings _screenSettings;
        //private ScreenRemerciement _screenRemerciement;
        //public ScreenGameOver _screenGameOver;
        //private ScreenChargement _screenChargement;
        //private ScreenMenuMap _screenMenuMap;
        //private ScreenClassement _screenClassement;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // Par défaut, le 1er état flèche l'écran de menu

            // on charge les 2 écrans 
            //_screenMenu = new ScreenMenu(this);
            //_screenJeu = new ScreenJeu(this);
            //_screenSettings = new ScreenSettings(this);
            //_screenGameOver = new ScreenGameOver(this);
            //_screenChargement = new ScreenChargement(this);
            //_screenMenuMap = new ScreenMenuMap(this);
            //_screenClassement = new ScreenClassement(this);
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


            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _screenManager.LoadScreen(new ScreenChargement(this), new FadeTransition(GraphicsDevice, Color.Black, 0));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState _mouseState = Mouse.GetState();



            base.Update(gameTime);
        }


        public void LoadScreen(Etats etat)
        {


            if (etat == Etats.Play)
                _screenManager.LoadScreen(new ScreenJeu(this), new FadeTransition(GraphicsDevice, Color.Black));



            else if (etat == Etats.Menu)
                _screenManager.LoadScreen(new ScreenMenu(this), new FadeTransition(GraphicsDevice, Color.Black));

            else if (etat == Etats.Settings)
                _screenManager.LoadScreen(new ScreenSettings(this), new FadeTransition(GraphicsDevice, Color.Black));

            else if (etat == Etats.Exit)
                Exit();
            else if (etat == Etats.Lose)
                _screenManager.LoadScreen(new ScreenGameOver(this), new FadeTransition(GraphicsDevice, Color.Black, 10));
            else if (etat == Etats.MenuMap)
                _screenManager.LoadScreen(new ScreenMenuMap(this), new FadeTransition(GraphicsDevice, Color.Black));
            else if (etat == Etats.Classement)
                _screenManager.LoadScreen(new ScreenClassement(this), new FadeTransition(GraphicsDevice, Color.Black));

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