using LD54.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.Main
{
    internal class InputAdventureGameState : GameState
    {
        public readonly AdventureInterface ui;

        public InputAdventureGameState(AdventureInterface ui)
        {
            this.ui = ui;
        }

        public override void Enter()
        {
            ui.inputBox.state.ReceiveEvent("enable");
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }

        public override void Draw(float deltaTime)
        {
            ui.inputBox.Draw();
            ui.textWindow.Draw();
        }

        public override void Update(double elapsed)
        {
            
        }
    }
}
