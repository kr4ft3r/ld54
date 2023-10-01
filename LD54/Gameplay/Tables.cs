using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD54.Gameplay
{
    internal class Tables
    {
        public static string[] Names = 
        {
            "Joe", "Phil", "Jack", "Ivan", "Rasta", "Crusty", "Bubba", "Ahmed", "Cohen", "Boris", "Hans",
            "Fatso", "Bill", "Ronald", "Casper", "Willy", "Jeff", "Judah", "Tony", "Dan", "Donald", "Hank",
            "Tad", "Larry", "Doc", "Yuri", "Snoop", "Pedro"
        };

        public static string QueryUniqueName(List<string> taken)
        {
            Random rnd = new Random();
            string n = "";
            do
            {
                n = Names[rnd.Next(Names.Length)];
            } while (taken.Contains(n));

            return n;
        }

        public enum Personality
        {
            Stabber, Brute, Gossiper, Rapist
        }

        public static string[] Actions = { 
            "look", "talk"
        };

        public static string[] Contexts = { 
            "politics", "sport", "crime"
        };

        public static string[] Crimes = {
            "arsony", "robbery", "posession", "assault", "kidnapping", "murder", "sodomy", "piracy", "extortion", "burglary"
        };

        public static Dictionary<Personality, (Action<Inmate,Inmate> hateAction, Action<Inmate, Inmate> loveAction)>
            PersonalityActions = new Dictionary<Personality, (Action<Inmate, Inmate> hateAction, Action<Inmate, Inmate> loveAction)> {
                {Personality.Stabber, (
                    (Inmate actor,Inmate target) => {
                    },
                    (Inmate actor,Inmate target) => { }
                    ) 
                },
                {Personality.Brute, (
                    (Inmate actor,Inmate target) => {
                    },
                    (Inmate actor,Inmate target) => { }
                    )
                },
                {Personality.Gossiper, (
                    (Inmate actor,Inmate target) => {
                    },
                    (Inmate actor,Inmate target) => { }
                    )
                },
                {Personality.Rapist, (
                    (Inmate actor,Inmate target) => {
                    },
                    (Inmate actor,Inmate target) => { }
                    )
                },
            };

        public static Dictionary<string,string> Strings = new Dictionary<string, string>() {
            { "cellIntro", "You enter a crowded cell and greet your new friends. There is a help note on the wall, try reading it with \"look note\" or \"look help\". Good luck..."},
            { "helpNote", "Each command will make a day pass except for \"look\" so feel free to look at your inmates to learn more about them. You can \"talk\" to your inmates about any topic, as long as it is politics, sport, or crime. (\"talk (name) (topic)\"). If you omit the name you will talk to the whole cell." }
        };
    }

    
}
