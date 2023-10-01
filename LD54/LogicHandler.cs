using Istina;
using LD54.Gameplay;
using LD54.Main;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LD54
{
    internal class LogicHandler
    {
        public static SoundEffect sfx;

        public static void Process(string action, string target, string context)
        {
            if (!Tables.Actions.Contains(action))
            {
                GameData.Paragraphs.Enqueue("What do you mean \"" + action + "\"?");
                Continue(false);

                return;
            }

            if (action == "look")
            {
                if (target == "")
                {
                    GameData.Paragraphs.Enqueue("Look at what exactly?");
                    Continue(false);

                    return;
                }

                if (GameData.OthersNames.Contains(target))
                {
                    GameData.Paragraphs.Enqueue(GetInmateDescription(target));
                    GameData.Paragraphs.Enqueue(GetInmateLikesDislikesDescription(target));
                }
                if (target == "note" || target == "help")
                {
                    GameData.Paragraphs.Enqueue(Tables.Strings["helpNote"]);
                }

                Continue(false);

                return;

            } else if (action == "talk")
            {

                if (target == "" && GameData.OthersNames.Contains(context))
                {
                    GameData.Paragraphs.Enqueue("Talk to " + context + " about what?");
                    Continue(false); return;
                }

                if (target == "" && Tables.Contexts.Contains(context))
                {
                    ProcessTopic(context, GameData.Player);
                    Continue(); return;
                }

                if (!GameData.OthersNames.Contains(target))
                {
                    GameData.Paragraphs.Enqueue("Who is " + target + "?");
                    Continue(false); return;
                }

                if (!Tables.Contexts.Contains(context))
                {
                    GameData.Paragraphs.Enqueue("Unknown topic " + context + ", only politics, sport, and crime apply.");
                    Continue(false); return;
                }

                if (Tables.Contexts.Contains(context))
                {
                    //TODO talk topic to person
                    Continue(); return;
                }
                
                GameData.Paragraphs.Enqueue("...");
            }

            Continue();
        }

        public static void Continue(bool nextDay = true)
        {
            GameStateHandler.GameState.ReceiveEvent("continue");
        }

        public static string GetInmateDescription(string name)
        {
            return GameData.GetInmateByName(name).GetDescription();
        }

        public static string GetInmateLikesDislikesDescription(string name)
        {
            return GameData.GetInmateByName(name).GetLikesDislikesDescription();
        }

        public static void ProcessTopic(string topic, Inmate source)
        {
            foreach (Inmate inmate in GameData.Cell)
            {
                if (inmate == source || inmate.IsPlayer()) continue;
                string result = inmate.Decide(topic, source);
                if (result == "hates") GameData.Paragraphs.Enqueue(inmate.Name + ": " + "Enough about " + GetParticle(topic) + "!");
                else if (result == "loves") GameData.Paragraphs.Enqueue(inmate.Name + ": I do love to speak of " + GetParticle(topic));

                ProcessReaction(inmate, source);
            }
        }

        public static void ProcessAction(string action, Inmate actor, Inmate target)
        {
            if (GameData.GameOver) return;

            switch (action) 
            {
                case "beat":
                    GameData.Paragraphs.Enqueue(
                        target.IsPlayer() ? (actor.Name + " gives you a nasty beating!") : (target.Name + " gets nasty beating from " + actor.Name + "!")
                        );
                    target.DecreaseHP(1);
                    break;
                case "stab":
                    GameData.Paragraphs.Enqueue(
                        target.IsPlayer() ? (actor.Name + " stabs you near the kidney!") : (target.Name + " gets stabbed by " + actor.Name + "!")
                        );
                    target.DecreaseHP(2);
                    break;
                case "rape":
                    GameData.Paragraphs.Enqueue(
                        target.IsPlayer() ? (actor.Name + " rapes you!") : (target.Name + " gets raped by " + actor.Name + "!")
                        );
                    target.DecreaseHP(1);
                    break;
                case "gossip":
                    GameData.Paragraphs.Enqueue(
                        target.IsPlayer() ? (actor.Name + " slanders you, turning everyone against you!") : (target.Name + " gets slandered by " + actor.Name + ", turning everyone against him!")
                        );
                    foreach (Inmate inmate in GameData.Cell)
                        if (inmate != actor && inmate != target && !inmate.IsPlayer())
                            inmate.relationshipStates[target.slot].ReceiveEvent("down");
                    break;
                case "drug":
                    GameData.Paragraphs.Enqueue(
                        target.IsPlayer() ? (actor.Name + " lets you use his drugs! You don't feel well..") : (actor.Name + " shares his drugs with " + target.Name + ", making him dizzy!")
                        );
                    break;
                default:
                    GameData.Paragraphs.Enqueue(actor.Name + " starts to " + action);
                    break;
            }

            foreach (Inmate inmate in GameData.Cell) 
            {
                if (inmate == actor || inmate == target || inmate.IsPlayer()) continue;
                string result = inmate.Decide(action, actor);
                if (result == "hates") GameData.Paragraphs.Enqueue(inmate.Name + ": " + "There is no place for " + GetParticle(action) + " in this cell, "+actor.Name+"!");
                else if (result == "loves") GameData.Paragraphs.Enqueue(inmate.Name + ": It was great to see some " + GetParticle(action) + ", good job " + actor.Name);
                if (result == "hates" || result == "loves") ProcessReaction(inmate, actor); //omg
            }

            foreach (Inmate inmate in GameData.Cell)
            {
                if (inmate.HP <= 2 && inmate.HP >= 1)
                    GameData.Paragraphs.Enqueue(inmate.Name + " doesn't look too well...");
                else if (inmate.HP <= 0)
                {
                    GameData.Paragraphs.Enqueue(
                        inmate.IsPlayer() ? "Your are almost dead! Guards take you away into solitary confinment where you will be causing no more problems."
                        : (inmate.Name + " is almost dead! Guards take him away into solitary confinment where he will be causing no more problems.")
                        );
                    if (inmate.IsPlayer())
                    {
                        GameData.Paragraphs.Enqueue("G A M E  O V E R");
                        GameData.GameOver = true;
                    } else
                    {
                        Inmate newInmate = GameData.NewInmate(inmate.slot);
                        GameData.Paragraphs.Enqueue("A new inmate enters the cell.");
                        GameData.Paragraphs.Enqueue(newInmate.Name + ": " + "Hi, my name is " + newInmate.Name + ", I am here for " + newInmate.Crime);
                    }
                }
            }
        }

        public static string GetParticle(string word)
        {
            switch(word) 
            {
                case "beat": return "beating";
                case "stab": return "stabbing";
                case "drug": return "doing drugs";
            }
            return word;
        }

        public static void ProcessReaction(Inmate actor, Inmate target)
        {
            if (GameData.GameOver) return;

            if (actor.GetRelationshipTowards(target) == "enraged")
            {
                switch(actor.Personality)
                {
                    case Tables.Personality.Brute:
                        ProcessAction("beat", actor, target);
                        break;
                    case Tables.Personality.Stabber:
                        ProcessAction("stab", actor, target);
                        break;
                    case Tables.Personality.Rapist:
                        ProcessAction("rape", actor, target);
                        break;
                    case Tables.Personality.Gossiper:
                        ProcessAction("gossip", actor, target);
                        break;
                    case Tables.Personality.Junky:
                        GameData.Paragraphs.Enqueue(actor.Name + " does some drugs to calm himself down.");
                        break;
                }
            } else if (actor.GetRelationshipTowards(target) == "extatic")
            {
                switch (actor.Personality)
                {
                    case Tables.Personality.Rapist:
                        GameData.Paragraphs.Enqueue(actor.Name + ": Sorry, but I really like you " + target.Name);
                        ProcessAction("rape", actor, target);
                        break;
                    case Tables.Personality.Junky:
                        ProcessAction("drug", actor, target);
                        break;
                }
            }
        }

        public static void ProcessRelationshipStateChange(object obj, string newState)
        {
            //GameData.Paragraphs.Enqueue(((State)obj).identifier + " is now " + newState + " about");
            
        }
    }
}
