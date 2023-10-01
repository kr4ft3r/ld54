using LD54.Gameplay;
using LD54.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.Main
{
    internal class OutputAdventureGameState : GameState
    {
        public readonly AdventureInterface ui;

        public OutputAdventureGameState(AdventureInterface ui)
        {
            this.ui = ui;
            GameData.Paragraphs.Enqueue(Tables.Strings["cellIntro"]);
        }

        public override void Enter()
        {
            while(GameData.Paragraphs.Count > 0)
            {
                ui.typeWriter.paragraphs.Enqueue(GameData.Paragraphs.Dequeue());
            }
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }

        public override void Draw(float deltaTime)
        {
            //ui.inputBox.Draw();
            ui.textWindow.Draw();
        }

        public override void Update(double elapsed)
        {
            ui.typeWriter.Update(elapsed);

            if (!ui.typeWriter.Typing && ui.typeWriter.paragraphs.Count == 0 )
            {
                // That's all
                GameStateHandler.GameState.ReceiveEvent("continue");
            }
        }
    }
}
