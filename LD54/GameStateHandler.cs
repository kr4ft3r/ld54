using LD54.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Istina;
using Istina.Parser;
using System.Reflection;
using System.Diagnostics;

namespace LD54
{
    internal class GameStateHandler
    {
        public static GameState Active { get; private set; }
        public static TitleGameState State_title { get; set; }
        public static InputAdventureGameState State_adventureInput { get; set; }
        public static OutputAdventureGameState State_adventureOutput { get; set; }
        public static State GameState { get; private set; }

        public static void Init()
        {
            GameState = State.BuildFromString(
                "gameState",
                "title,adventureOutput,continue" + Environment.NewLine +
                "adventureOutput,adventureInput,continue" + Environment.NewLine +
                "adventureInput,adventureOutput,continue"// + Environment.NewLine +
                ,
                new NaiveCsvParser()
                );

            GameState.StateChanged += (object obj, string newState) => {
                Debug.WriteLine("Trying game state: " + "State_" + newState);
                PropertyInfo propInfo = typeof(GameStateHandler).GetProperty("State_" + newState);
                if (propInfo != null)
                {
                    Active.Exit();
                    Debug.WriteLine("Loading game state: " + propInfo.Name);
                    Active = (GameState)propInfo.GetValue(null, null);
                    Active.Enter();
                }
            };

            Active = State_title;
            Active.Enter();
        }
    }
}
