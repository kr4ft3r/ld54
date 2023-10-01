using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Istina;
using Istina.Parser;
using LD54.Main;
using Microsoft.Xna.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace LD54.Gameplay
{
    internal class Inmate
    {
        public string Name { get; set; }
        public Tables.Personality Personality { get; set; }
        public string Crime { get; set; }
        public Color Color { get; set; }
        public int HP { get; private set; }

        public readonly State[] relationshipStates;
        public readonly string[] loves;
        public readonly string[] hates;
        private Random _rnd = new Random();
        public readonly int slot;

        public Inmate(int slot)
        {
            this.slot = slot;
            HP = 9;
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
                "extatic,happy,down" + Environment.NewLine +
                "happy,indifferent,down" + Environment.NewLine +
                "*,enraged,superdown" + Environment.NewLine +
                "*,indifferent,superup"
                ,
                parser);
                relationshipStates[i].StateChanged += LogicHandler.ProcessRelationshipStateChange;
            }

            // Likes and dislikes
            for (int i = 0; i < 3; i++)
            {
                HashSet<string> stuff = new HashSet<string>();
                while(stuff.Count < 6)
                {
                    if (_rnd.NextDouble() <= .5)
                        stuff.Add(Tables.Contexts[_rnd.Next(Tables.Contexts.Length)]);
                    else stuff.Add(Tables.Actions[_rnd.Next(Tables.Actions.Length - 2) + 2]);
                }

                loves = stuff.Take(3).ToArray();
                hates = stuff.TakeLast(3).ToArray();
            }
        }

        public string GetRelationshipTowards(Inmate inmate)
        {
            return relationshipStates[inmate.slot].Current;
        }

        public string Decide(string term, Inmate inmate)
        {
            term = term.ToLower();
            if (loves.Contains(term)) { relationshipStates[inmate.slot].ReceiveEvent("up"); return "loves"; }
            if (hates.Contains(term)) { relationshipStates[inmate.slot].ReceiveEvent("down"); return "hates"; }

            return "";
        }

        public bool IsPlayer()
        {
            return this == GameData.Player;
        }

        public void IncreaseHP(int amount)
        {
            HP += amount;
            if (HP > 9) HP = 9;
        }

        public void DecreaseHP(int amount)
        {
            HP -= amount;
            if (HP < 0) HP = 0;
        }

        public string GetPersonalityString()
        {
            return Personality.ToString().ToLower();
        }

        public string GetDescription()
        {
            return Name + " is a " + GetPersonalityString() + " by nature. He is in here for " + Crime + ".";
        }

        public string GetLikesDislikesDescription()
        {
            return Name + " loves " + string.Join(", ", loves) + ", and hates " + string.Join(", ", hates) + ". You make him feel " + relationshipStates[0].Current;
        }
    }
}
