//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/J6Saga.cs

using RBot;

public class CoreAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public J6Saga J6 = new J6Saga();
    public BattleUnder Under = new BattleUnder();

    public bool GuardianCheck()
    {
        Core.Logger("Checking AQ Guardian");
        if (Core.CheckInventory("Guardian Awe Pass", 1, true))
        {
            Core.Logger("You're AQ Guardian!");
            return true;
        }
        Core.BuyItem("museum", 53, "Guardian Awe Pass");
        if (Core.CheckInventory("Guardian Awe Pass"))
        {
            Core.Logger("Guardian Awe Pass bought successfully! You're AQ Guardian!");
            return true;
        }
        else
        {
            Core.Logger("You're not AQ Guardian.");
            return false;
        }
    }

    public void AweKill(int questID, string gear)
    {
        Core.EquipClass(ClassType.Solo);
        if (gear.Equals("pauldron"))
            Story.KillQuest(questID, "gravestrike", "Ultra Akriloth");
        else if (gear.Equals("breastplate"))
            Story.KillQuest(questID, "aqlesson", "Carnax");
        else if (gear.Equals("vambrace"))
            Story.KillQuest(questID, "bloodtitan", "Ultra Blood Titan");
        else if (gear.Equals("gauntlet"))
            Story.KillQuest(questID, "alteonbattle", "Ultra Alteon");
        else if (gear.Equals("greaves"))
            Story.KillQuest(questID, "bosschallenge", "Mutated Void Dragon");
        else if (gear.Equals("helm"))
        {
            Story.UpdateQuest(3008);
            Core.SendPackets("%xt%zm%setAchievement%108927%ia0%18%1%");
            Story.UpdateQuest(3004);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Helm Shard", 5, false);
        }
        else
        {
            Story.UpdateQuest(3008);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Cape Shard", 1, false);
        }
    }
    public void ArmorOfZular() {
        Core.AddDrop("Armor of Zular", "Djinn's Essence");
        if(Core.CheckInventory("Armor of Zular"))
            return;
        Core.EquipClass(ClassType.Farm);
        if(!Story.QuestProgression(6154))
        {
            Core.EnsureAccept(6153);
            Core.KillMonster("mobius", "Slugfit", "Left", "Slugfit", "Fragment 1");
            Core.KillMonster("faerie", "TopRock", "Left", "*", "Fragment 2");
            Core.KillMonster("faerie", "Side4", "Right", "*", "Fragment 3");
            Core.KillMonster("faerie", "End", "Center", "Cyclops Warlord", "Fragment 4");
            Core.KillMonster("cornelis", "Side1", "Left", "*", "Fragment 5");
            Core.EnsureComplete(6153);
        }
        if(!Story.QuestProgression(6155))
        {
            Core.EnsureAccept(6154);
            Core.KillMonster("arcangrove", "Left", "Left", "*", "Fragment 6");
            Core.KillMonster("cloister", "r8", "Left", "*", "Fragment 7");
            Core.KillMonster("gilead", "r5", "Right", "Bubblin", "Fragment 8");
            Core.KillMonster("natatorium", "r2", "Left", "Merdraconian", "Fragment 9");
            Core.KillMonster("mafic", "r6", "Left", "*", "Fragment 10");
            Core.EnsureComplete(6154);
        }
        if(!Story.QuestProgression(6156))
        {
            Core.EnsureAccept(6155);
            Core.KillMonster("mythsong", "Hill", "Left", "*", "Fragment 11");
            Core.KillMonster("palooza", "Act3", "Left", "Rock Lobster", "Fragment 12");
            Core.KillMonster("palooza", "Act2", "Left", "Stinger", "Fragment 13");
            Core.KillMonster("palooza", "Act3", "Left", "Mozard", "Fragment 15");
            Core.KillMonster("beehive", "r5", "Left", "*", "Fragment 14");
            Core.EnsureComplete(6155);
        }
        if(!Story.QuestProgression(6157))
        {
            Core.EnsureAccept(6156);
            Core.KillMonster("forestchaos", "Boss", "Left", "*", "Fragment 16");
            Core.KillMonster("guru", "Field2", "Left", "*", "Fragment 17");
            Core.KillMonster("marsh", "Forest3", "Left", "Dark Witch", "Fragment 18");
            Core.KillMonster("marsh", "Forest3", "Left", "Spider", "Fragment 19");
            Core.KillMonster("marsh2", "End", "Left", "Soulseeker", "Fragment 20");
            Core.EnsureComplete(6156);
        }
        if(!Story.QuestProgression(6158))
        {
            Core.EnsureAccept(6157);
            Core.KillMonster("pirates", "End", "Left", "Shark Bait", "Fragment 21");
            Core.KillMonster("pirates", "End", "Left", "Fishwing", "Fragment 25");
            Core.KillMonster("yokairiver", "r2", "Left", "Kappa Ninja", "Fragment 22");
            Core.KillMonster("bamboo", "Enter", "Spawn", "*", "Fragment 23");
            Core.KillMonster("yokaiwar", "War2", "Left", "Samurai Nopperabo", "Fragment 24");
            Core.EnsureComplete(6157);
        }
        if(!Story.QuestProgression(6159))
        {
            Core.EnsureAccept(6158);
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("doomkitten", "Enter", "Spawn", "*", "Potent DoomKitten Mana", publicRoom: true);
            Core.KillMonster("bloodtitan", "Ultra", "Left", "*", "Potent Blood Titan Mana");
            Core.HuntMonster("trigoras", "Trigoras", "Potent Trigoras Mana");
            Core.KillMonster("phoenixrise", "r8", "Left", "*", "Potent CinderClaw Mana");
            Core.KillMonster("thevoid", "r16", "Left", "*", "Potent Reaper Mana", publicRoom: true);
            Core.EnsureComplete(6158);
        }
        Story.MapItemQuest(6159, "djinngate", 5571, 5, false);
        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6160, "djinngate", "Harpy|Lamia");
    }

    
    public void ValothsCannonOfDoom()
    {
        Core.AddDrop("Valoth's Cannon of Doom", "Valoth's Broken Cannon", "Peanut", "Floozer");
        if (Core.CheckInventory("Valoth's Cannon of Doom"))
            return;
        if (!Core.CheckInventory("Valoth's Broken Cannon"))
        {
            Farm.Gold(5000000);
            Core.BuyItem("crashruins", 1212, "Valoth's Broken Cannon");
        }
        Core.EnsureAccept(8043);
        if (!Core.CheckInventory("Peanut"))
        {
            Core.Logger("Checking J6 Saga completition...");
            Core.BuyItem("hyperspace", 603, "Peanut");
        }
        if (!Core.CheckInventory("Peanut"))
        {
            Core.Logger("Failed.");
            J6.J6();
            Core.BuyItem("hyperspace", 603, "Peanut");
        }
        else
            Core.Logger("Successful!");
        Floozer();
        Core.EnsureComplete(8043);
    }

    public void Floozer()
    {
        Core.AddDrop("Floozer", "Ice Diamond", "Dark Bloodstone", "Songstone", "Butterfly Sapphire",
          "Understone", "Rainbow Moonstone");
        if (Core.CheckInventory("Floozer"))
            return;
        Story.KillQuest(7277, "wanders", "Kalestri Worshiper", GetReward: false);
        if (!Story.QuestProgression(7280))
        {
            Core.EnsureAccept(7278);
            if (!Core.CheckInventory("Ice Diamond"))
                Story.KillQuest(7279, "kingcoal", "Snow Golem");
            Core.EnsureComplete(7278);
        }
        if (!Story.QuestProgression(7282))
        {
            Core.EnsureAccept(7280);
            if (!Core.CheckInventory("Dark Bloodstone"))
                Story.KillQuest(7281, "safiria", "Blood Maggot");
            Core.EnsureComplete(7280);
        }
        Story.KillQuest(7282, "brightfall", "Painadin Overlord", GetReward: false);
        Story.KillQuest(7283, "timevoid", "Unending Avatar", GetReward: false);
        Story.MapItemQuest(7284, "downward", 6908, GetReward: false);
        if (!Story.QuestProgression(7286))
        {
            Core.EnsureAccept(7285);
            if (!Core.CheckInventory("Songstone"))
                Story.MapItemQuest(7297, "mythsong", 6909, 15);
            Core.EnsureComplete(7285);
        }
        if (!Story.QuestProgression(7288))
        {
            Core.EnsureAccept(7286);
            if (!Core.CheckInventory("Butterfly Sapphire"))
                Story.KillQuest(7287, "bloodtusk", "Trollola Plant");
            Core.EnsureComplete(7286);
        }
        if (!Story.QuestProgression(7290))
        {
            Core.EnsureAccept(7288);
            if (!Core.CheckInventory("Understone"))
            {
                Under.BattleUnderB();
                Under.Understone(1);
            }
            Core.EnsureComplete(7288);
        }
        Core.EnsureAccept(7290);
        if (!Core.CheckInventory("Rainbow Moonstone"))
            Story.KillQuest(7291, "earthstorm", new[] { "Diamond Golem", "Emerald Golem", "Ruby Golem", "Sapphire Golem" });
        Core.EnsureComplete(7290);
    }

}
