using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.UI
{
    internal class UserInterface
    {
        public readonly Fonts fonts;
        public readonly GameWindow window;
        public readonly SpriteBatch spriteBatch;
        public readonly GraphicsDeviceManager graphics;

        public UserInterface(Fonts fonts, SpriteBatch spriteBatch, GameWindow window, GraphicsDeviceManager graphics) 
        {
            this.fonts = fonts;
            this.spriteBatch = spriteBatch;
            this.window = window;
            this.graphics = graphics;
        }
    }
}
