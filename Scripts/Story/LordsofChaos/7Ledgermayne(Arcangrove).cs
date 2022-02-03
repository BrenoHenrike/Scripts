//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaArcangrove
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Bot.Quests.IsUnlocked(847))
            return;


        //Observing the Observatory
        Core.MapItemQuest(805, 139, map: "arcangrove");
        //Ewa the Treekeeper
        Core.MapItemQuest(806, 142, map: "cloister");

        //Bear Necessities of LifeRoot
        Core.MapItemQuest(807, 140, map: "cloister");

        //Acorny Quest
        Core.KillQuest(808, "cloister", "Acornent");

        //Ravenloss
        Core.KillQuest(809, "cloister", "Karasu");

        //It's A Bough-t Time
        Core.BuyQuest(810, arcangrove, 211, "Mana Potion", AutoCompleteQuest: false);
        Core.MapItemQuest(810, 141, 3, "cloister");

        //Wendigo Whereabouts
        Core.KillQuest(811, "cloister", "Wendigo");

        //Find Paddy Lump
        Core.MapItemQuest(812, 143, map: "mudluk");

        //Toothy Smiles
        Core.KillQuest(814, "mudluk", "Swamp Lurker");

        //Slimy Cyrus
        Core.KillQuest(815, "mudluk", "Swamp Lurker");

        //Lord Of The Fleas
        Core.KillQuest(816, "arcangrove", "Gorillaphant");

        //Not The Best Idea
        Core.KillQuest(817, "mudluk", "Swamp Frogdrake");

        //Gates and Guardians
        Core.KillQuest(818, "mudluk", "Tiger Leech", FollowupIDOverwrite: 825);

        //Water You Waiting For--Find Nisse
        Core.MapItemQuest(825, 144, map: "natatorium");

        //Dive Right In
        Core.MapItemQuest(826, 145, 12, "natatorium");

        //Seafood Diet
        Core.KillQuest(827, "natatorium", "Anglerfish");

        //Mercenaries
        Core.KillQuest(828, "natatorium", "Merdraconian");

        //Synchronized Slaying
        Core.KillQuest(829, "arcangrove", "Seed Spitter|Gorillaphant");
        Core.KillQuest(829, "cloister", new[] { "Acornent", "Karasu", "Wendigo" });
        Core.KillQuest(829, "mudluk", "Swamp Frogdrake|Swamp Lurker");

        //The Deep End
        Core.KillQuest(830, "natatorium", "Nessie");

        //Find Umbra, the Master Shaman
        Core.MapItemQuest(831, 146, map: "gilead");

        //The Root of Elementals
        Core.KillQuest(832, "gilead", "Earth Elemental");
        Core.KillQuest(832, "arcangrove", "Seed Spitter");

        //Eupotamic Elementals
        Core.KillQuest(833, "gilead", "Water Elemental");
        Core.KillQuest(833, "natatorium", "Merdraconian");

        //Breaking Wind Elementals
        Core.KillQuest(834, "gilead", "Wind Elemental");
        Core.KillQuest(834, "cloister", "Karasu");

        //Fight Fire With Fire Salamanders
        Core.KillQuest(835, "gilead", "Fire Elemental");
        Core.KillQuest(835, "mudluk", "Swamp Frogdrake");

        //Guardian of the Gilead Wrap
        Core.KillQuest(836, "gilead", "Mana Elemental", FollowupIDOverwrite: 838);

        //Find Felsic the Magma Golem
        Core.MapItemQuest(838, 147, map: "mafic");

        //Liquid Hot Magma Maggots
        Core.KillQuest(839, "mafic", "Volcanic Maggot");

        //Scorched Serpents
        Core.KillQuest(840, "mafic", "Scoria Serpent");

        //Playing With Living Fire
        Core.KillQuest(841, "mafic", "Living Fire");

        //Kindling Relationship
        Core.KillQuest(842, "mafic", "Mafic Dragon");

        //Obey Your Thirst for Adventure
        Core.KillQuest(843, "elemental", "Mana Imp");

        //Captain Falcons
        Core.KillQuest(844, "elemental", "Mana Falcon");

        //Big, bad, and Baddest Bosses
        Core.KillQuest(845, "cloister", "Wendigo");
        Core.KillQuest(845, "mudluk", "Tiger Leech");
        Core.KillQuest(845, "natatorium", "Nessie");
        Core.KillQuest(845, "gilead", "Mana Elemental");
        Core.KillQuest(846, "mafic", "Mafic Dragon");
        //The Great Mana Golem
        Core.KillQuest(846, "elemental", "Mana Golem");
        //Chaos Lord Ledgermayne
        Core.KillQuest(847, "ledgermayne", "Ledgermayne");
    }
}