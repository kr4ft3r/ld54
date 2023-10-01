using LD54.UI.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.UI
{
    internal class AdventureInterface : UserInterface
    {
        public readonly InputBox inputBox;
        public readonly TextWindow textWindow;
        public readonly TypeWriter typeWriter;

        public AdventureInterface(Fonts fonts, SpriteBatch spriteBatch, GameWindow window, GraphicsDeviceManager graphics) : base(fonts, spriteBatch, window, graphics)
        {
            inputBox = new InputBox(
                fonts.large,
                new Vector2(10, graphics.PreferredBackBufferHeight - 50),
                this,
                (string input) => { InputHandler.Process(input); }
                );
            textWindow = new TextWindow(
                fonts.large,
                new Vector2(10, 100),
                28, 17,
                this
                );

            typeWriter = new TypeWriter(textWindow);
        }
    }
}
