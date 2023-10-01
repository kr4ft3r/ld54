using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LD54.Gameplay;

namespace LD54
{
    internal class InputHandler
    {
        public static void Process(string input)
        {
            string[] parameters = input.Split(" ").Where((s) => s != "").ToArray();
            parameters = parameters.Select((s) => { return s.ToLower(); }).ToArray();

            string action = ""; string target = ""; string context = "";

            //if (Tables.Actions.Contains(parameters[0])) action = parameters[0];
            action = parameters[0];

            foreach (string p in parameters)
            {
                if (p == action) continue;

                if (Tables.Contexts.Contains(p)) context = p;
                else target = p;
            }

            Task.Run(() => {
                Thread.Sleep(200);
                Debug.WriteLine(action + "," + target + "," + context);

                LogicHandler.Process(action, target, context);
            });

        }
    }
}
