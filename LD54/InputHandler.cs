using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LD54
{
    internal class InputHandler
    {
        public static readonly string[] Actions = { "look", "talk" };
        public static readonly string[] Contexts = { "politics", "sport" };
        public static void Process(string input)
        {
            string[] parameters = input.Split(" ").Where((s) => s != "").ToArray();

            string action = ""; string target = ""; string context = "";
            if (parameters.Length > 3)
            {
                if (Actions.Contains(parameters[0])) action = parameters[0];
                else return;

                foreach (string p in parameters)
                {
                    if (p != action /*&& and check target.. */ ) target = p;
                    else if (Contexts.Contains(p)) context = p;
                }
            }

            Task.Run(() => {
                Thread.Sleep(500);

                LogicHandler.Process(action, target, context);
            });
            /*(string action, string target, string context) = new Func<(string,string,string)>() =>
            {
                return (parameters[0], parameters[0], parameters[0]);
            };*/
            // inmate process action, topic
        }
    }
}
