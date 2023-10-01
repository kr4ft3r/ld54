using LD54.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.Main
{
    internal class TitleGameState : GameState
    {
        public readonly TitleInterface ui;

        public TitleGameState(TitleInterface ui)
        {
            this.ui = ui;
        }

        public override void Enter()
        {
            ui.titleText.SetText("   Locked Together");
            ui.textWindow.SetText("");
            ui.typeWriter.paragraphs.Clear();
            ui.typeWriter.paragraphs.Enqueue(
                "       Enter your name and press enter to begin your   adventure, inmate!");
            ui.inputBox.state.ReceiveEvent("enable");
        }

        public override void Exit()
        {
            //throw new NotImplementedException();
        }

        public override void Draw(float deltaTime)
        {
            ui.inputBox.Draw();
            ui.titleText.Draw();
            ui.textWindow.Draw();
        }

        public override void Update(double elapsed)
        {
            ui.typeWriter.Update(elapsed);
        }
    }
}
