using LD54.Main;
using LD54.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;

namespace LD54
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Fonts _fonts;

        public static GameWindow gw;

        // Credit: Text input inspired by willmotil
        // https://community.monogame.net/t/how-do-i-create-a-user-input-text-box-that-stores-input-into-variables/11621/2

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            var adventureInterface = new AdventureInterface(
                 _fonts,
                 _spriteBatch,
                 Window,
                 _graphics
                );
            var titleInterface = new TitleInterface(
                _fonts,
                 _spriteBatch,
                 Window,
                 _graphics
                );

            GameStateHandler.State_title = new TitleGameState(titleInterface);
            GameStateHandler.State_adventureInput = new InputAdventureGameState(adventureInterface);
            GameStateHandler.State_adventureOutput = new OutputAdventureGameState(adventureInterface);
            GameStateHandler.Init();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _fonts = new Fonts(Content.Load<SpriteFont>("bittyLarge"), Content.Load<SpriteFont>("bittyHuge"));

            gw = Window;
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameStateHandler.Active.Update(gameTime.ElapsedGameTime.TotalSeconds);

                //mouseState = Mouse.GetState();
                //var isClicked = mouseState.LeftButton == ButtonState.Pressed;
                //CheckClickOnMyBox(mouseState.Position, isClicked, new Rectangle(0, 0, 200, 200));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            GameStateHandler.Active.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}