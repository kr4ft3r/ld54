using LD54.Gameplay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.Main
{
    internal class GameData
    {
        public const int OCCUPANCY = 5;

        public static Inmate Player;
        public static Inmate[] Cell = new Inmate[OCCUPANCY];
        public static string[] OthersNames = new string[OCCUPANCY-1];
        private static Random rnd = new Random();

        public static void SetupCell()
        {
            Cell[0] = Player;
            List<string> takenNames = new List<string>();
            for (int i = 1; i < OCCUPANCY; i++)
            {
                var inmate = new Inmate();
                string name = Tables.QueryUniqueName(takenNames);
                takenNames.Add(name);
                string safeName = name.ToLower();
                inmate.Name = safeName;
                OthersNames[i-1] = safeName;
                int numPersonalities = Enum.GetNames(typeof(Tables.Personality)).Length;
                inmate.Personality = (Tables.Personality)rnd.Next(numPersonalities);
                inmate.Crime = Tables.Crimes[rnd.Next(Tables.Crimes.Length)];

                Cell[i] = inmate;
                Debug.WriteLine("inmate created: " + inmate.Name);
            }
        }

        public static Inmate GetInmateByName(string name)
        {
            return Cell.Where((i) => { return !i.IsPlayer() && i.Name == name; }).FirstOrDefault();
        }

        public static Queue<Action<(Inmate actor,Inmate target)>> Outcomes = new Queue<Action<(Inmate,Inmate)>>();
        public static Queue<string> Paragraphs = new Queue<string>();
    }
}
