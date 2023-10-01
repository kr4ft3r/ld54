using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace LD54.UI.Component
{
    internal class TextWindow
    {
        private SpriteFont _font;
        private UserInterface _ui;
        private StringBuilder _characters = new StringBuilder();
        public readonly Vector2 position;
        private int _rows; private int _columns;
        private string NL = Environment.NewLine;
        private (int x, int y) _head = (1, 1);
        public Color Color;

        public TextWindow(SpriteFont font, Vector2 position, int columns, int rows, UserInterface ui)
        {
            _font = font;
            _ui = ui;
            this.position = position;
            _rows = rows;
            _columns = columns;
            this.Color = Color.Wheat;
        }

        public void SetText(string text)
        {
            _characters = new StringBuilder(text.ToUpper());
        }

        public void TypeIn(char c)
        {
            c = char.ToUpper(c);
            _head.x++;
            if (_head.x > _columns)
            {
                NewLine();
            }

            _characters.Append(c);
        }

        public void NewLine()
        {
            _head.x = 1;
            _head.y++;
            _characters.Append(NL);
            if (_head.y > _rows)
            {
                int firstBreak = _characters.ToString().IndexOf(NL);
                if (firstBreak >= 0)
                {
                    //Debug.WriteLine("PRE>>>>" + _characters.ToString());
                    int toRemove = _characters.ToString().Split(NL)[0].Length;
                    _characters = _characters.Remove(0, toRemove + NL.Length);

                }
                _head.y = _rows;
            }
        }

        public void Draw()
        {
            _ui.spriteBatch.DrawString(_font, _characters, position, Color);
        }
    }
}
