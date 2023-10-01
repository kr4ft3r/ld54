using LD54.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.UI.Component
{
    internal class InmateBox
    {
        private SpriteFont _font;
        private UserInterface _ui;
        public readonly Vector2 position;
        public readonly Inmate inmate;
        private Vector2 _labelPosition;
        public InmateBox(SpriteFont font, Vector2 position, UserInterface ui, Inmate inmate)
        {
            _font = font;
            this.position = position;
            _labelPosition = new Vector2(position.X, position.Y+60);
            _ui = ui;
            this.inmate = inmate;
        }

        public void Draw()
        {
            _ui.spriteBatch.DrawString(_font, GetSmiley() + inmate.Name.ToUpper(), _labelPosition, inmate.Color);
        }

        private string GetSmiley()
        {
            string relation = inmate.relationshipStates[0].Current;
            switch(relation)
            {
                case "enraged": return ">(";
                case "unhappy": return ";(";
                case "indifferent": return ":|";
                case "happy": return ":)";
                case "extatic": return "XD";
                default: return ":P";
            }
        }
    }
}
