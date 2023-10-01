using LD54.Gameplay;
using LD54.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54
{
    internal class LogicHandler
    {
        public static void Process(string action, string target, string context)
        {
            if (!Tables.Actions.Contains(action))
            {
                GameData.Paragraphs.Enqueue("What do you mean \"" + action + "\"?");
                Continue();

                return;
            }

            if (action == "look")
            {
                if (target == "")
                {
                    GameData.Paragraphs.Enqueue("Look at what exactly?");
                    Continue();

                    return;
                }

                if (GameData.OthersNames.Contains(target))
                {
                    GameData.Paragraphs.Enqueue(GetInmateDescription(target));
                    Continue();
                }
            } else if (action == "talk")
            {

            }

        }

        public static void Continue()
        {
            GameStateHandler.GameState.ReceiveEvent("continue");
        }

        public static string GetInmateDescription(string name)
        {
            return GameData.GetInmateByName(name).GetDescription();
        }
    }
}
