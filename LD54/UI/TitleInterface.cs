using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LD54.UI.Component;
using LD54.Gameplay;
using LD54.Main;

namespace LD54.UI
{
    internal class TitleInterface : UserInterface
    {
        public readonly InputBox inputBox;
        public readonly TextWindow textWindow;
        public readonly TextWindow titleText;
        public readonly TypeWriter typeWriter;
        public TitleInterface(Fonts fonts, SpriteBatch spriteBatch, GameWindow window, GraphicsDeviceManager graphics) : base(fonts, spriteBatch, window, graphics)
        {
            inputBox = new InputBox(
                fonts.large,
                new Vector2(10, graphics.PreferredBackBufferHeight - 50),
                this,
                (string input) => {
                    if (input.Length <= 1 || input.Length >= 10)
                    {
                        if (input.Length <= 1) typeWriter.paragraphs.Enqueue("You call that a name?!     Enter a proper name! Use the keyboard!");
                        else if (input.Length >= 10) typeWriter.paragraphs.Enqueue("I asked for your name, not your life story! Ten characters maximum!");
                        inputBox.state.ReceiveEvent("enable");
                        return;
                    }

                    Inmate player = new Inmate();
                    player.Name = input.Trim();

                    GameData.Player = player;

                    GameData.SetupCell();
                    GameStateHandler.GameState.ReceiveEvent("continue");

                }
                );
            textWindow = new TextWindow(
                fonts.large,
                new Vector2(10, 100),
                28, 17,
                this
            );
            titleText = new TextWindow(
                fonts.huge,
                new Vector2(10, 10),
                18, 2,
                this
                );
            titleText.Color = Color.Yellow;

            typeWriter = new TypeWriter(textWindow);
        }
    }
}
