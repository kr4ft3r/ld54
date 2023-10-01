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
            ui.typeWriter.paragraphs.Enqueue("Hello? Sup? Hello? Sup? Hello? Sup?");
            ui.typeWriter.paragraphs.Enqueue("Ola, ola!");
        }

        public override void Enter()
        {
            ui.typeWriter.paragraphs.Enqueue("Ola, ola!");
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

            if (ui.typeWriter.paragraphs.Count == 0 )
            {
                // That's all
                //GameStateHandler.
            }
        }
    }
}
