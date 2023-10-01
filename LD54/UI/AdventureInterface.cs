using LD54.Main;
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
        public readonly InmateBox[] inmates;

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
                30, 17,
                this
                );
            inmates = new InmateBox[GameData.OCCUPANCY-1];
            //UpdateInmateBoxes();

            typeWriter = new TypeWriter(textWindow);
        }

        public void UpdateInmateBoxes()
        {
            for (int i = 1; i < GameData.OCCUPANCY; i++)
            {
                inmates[i-1] = new InmateBox(fonts.large, new Vector2(10 + (i-1)*200, 0), this, GameData.Cell[i]);
            }
        }
    }
}
