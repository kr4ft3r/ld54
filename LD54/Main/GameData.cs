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
        public static bool GameOver = false;
        private static Random rnd = new Random();
        private static Dictionary<Tables.Personality, int> _numPersonalities;

        public static void SetupCell()
        {
            Cell[0] = Player;
            _numPersonalities = new Dictionary<Tables.Personality, int>()
            {
                {Tables.Personality.Gossiper, 0 },
                {Tables.Personality.Brute, 0 },
                {Tables.Personality.Stabber, 0 },
                {Tables.Personality.Rapist, 0 },
                {Tables.Personality.Junky, 0 },
            };
            List<string> takenNames = new List<string>();
            for (int i = 1; i < OCCUPANCY; i++)
            {
                var inmate = new Inmate(i);
                string name = Tables.QueryUniqueName(takenNames);
                takenNames.Add(name);
                string safeName = name.ToLower();
                inmate.Name = safeName;
                OthersNames[i-1] = safeName;
                
                inmate.Personality = SetPersonality();
                inmate.Crime = Tables.Crimes[rnd.Next(Tables.Crimes.Length)];
                inmate.Color = new Microsoft.Xna.Framework.Color(rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle());

                Cell[i] = inmate;
                Debug.WriteLine("inmate created: " + inmate.Name);
            }
        }

        public static Tables.Personality SetPersonality()
        {// Don't want more than 2 of the same
            int numPersonalities = Enum.GetNames(typeof(Tables.Personality)).Length;
            Tables.Personality p = (Tables.Personality)rnd.Next(numPersonalities);
            if (_numPersonalities[p] == 2) return SetPersonality();

            _numPersonalities[p]++;
            return p;
        }

        public static Inmate NewInmate(int slot)
        {
            List<string> takenNames = new List<string>(OthersNames);

            var inmate = new Inmate(slot);
            string name = Tables.QueryUniqueName(takenNames);
            takenNames.Add(name);
            string safeName = name.ToLower();
            inmate.Name = safeName;
            OthersNames[slot - 1] = safeName;
            int numPersonalities = Enum.GetNames(typeof(Tables.Personality)).Length;
            inmate.Personality = (Tables.Personality)rnd.Next(numPersonalities);
            inmate.Crime = Tables.Crimes[rnd.Next(Tables.Crimes.Length)];
            inmate.Color = new Microsoft.Xna.Framework.Color(rnd.NextSingle(), rnd.NextSingle(), rnd.NextSingle());

            Cell[slot] = inmate;
            Debug.WriteLine("inmate created: " + inmate.Name);

            return inmate;
        }

        public static Inmate GetInmateByName(string name)
        {
            return Cell.Where((i) => { return !i.IsPlayer() && i.Name == name; }).FirstOrDefault();
        }

        public static Queue<Action<(Inmate actor,Inmate target)>> Outcomes = new Queue<Action<(Inmate,Inmate)>>();
        public static Queue<string> Paragraphs = new Queue<string>();
    }
}
