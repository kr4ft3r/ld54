using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Istina;
using Istina.Parser;
using LD54.Main;
using static System.Net.Mime.MediaTypeNames;

namespace LD54.Gameplay
{
    internal class Inmate
    {
        public string Name { get; set; }
        public Tables.Personality Personality { get; set; }
        public string Crime { get; set; }

        public readonly State[] relationshipStates;

        public Inmate()
        {
            relationshipStates = new State[GameData.OCCUPANCY];
            var parser = new NaiveCsvParser();
            for (int i = 0; i < GameData.OCCUPANCY; i++)
            {
                relationshipStates[i] = State.BuildFromString(Name,
                // ..about
                "indifferent,unhappy,down" + Environment.NewLine +
                "unhappy,enraged,down" + Environment.NewLine +
                "enraged,unhappy,up" + Environment.NewLine +
                "unhappy,indifferent,up" + Environment.NewLine +
                "indifferent,happy,up" + Environment.NewLine +
                "happy,extatic,up" + Environment.NewLine +
                "*,enraged,superdown" + Environment.NewLine +
                "*,indifferent,superup"
                ,
                parser);
            }
        }

        public bool IsPlayer()
        {
            return this == GameData.Player;
        }

        public string GetPersonalityString()
        {
            return Personality.ToString().ToLower();
        }

        public string GetDescription()
        {
            return Name + " is " + GetPersonalityString() + " by nature. " + "He is in here for " + Crime;
        }
    }
}
