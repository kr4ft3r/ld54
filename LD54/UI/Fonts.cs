using LD54.Properties;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace LD54.UI
{
    internal class Fonts
    {
        public readonly SpriteFont large;
        public readonly SpriteFont huge;
        public Fonts(SpriteFont large, SpriteFont huge)
        {
            this.large = large;
            this.huge = huge;
        }
    }
}
