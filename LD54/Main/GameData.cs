using LD54.Gameplay;
using System;
using System.Collections.Generic;
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

        public static void SetupCell()
        {
            Cell[0] = Player;
            List<string> takenNames = new List<string>();
            for (int i = 1; i < OCCUPANCY; i++)
            {
                var inmate = new Inmate();
                inmate.Name = Tables.QueryUniqueName(takenNames);
                takenNames.Add(inmate.Name);

                Cell[i] = inmate;
            }
        }

        public static Queue<Action<Inmate>> Outcomes = new Queue<Action<Inmate>>();
    }
}
