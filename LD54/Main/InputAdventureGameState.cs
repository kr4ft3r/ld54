using LD54.UI;
using LD54.UI.Component;
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
            ui.UpdateInmateBoxes();
            ui.inputBox.SetPrompt("HP:" + GameData.Player.HP + ">> ");
            ui.inputBox.state.ReceiveEvent("enable");
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }

        public override void Draw(float deltaTime)
        {
            ui.inputBox.Draw();

            if (GameData.GameOver) return;

            ui.textWindow.Draw();
            foreach(InmateBox box in ui.inmates)
            {
                box.Draw();
            }
        }

        public override void Update(double elapsed)
        {
            
        }
    }
}
