//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class J6Saga
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        J6();

        Core.SetOptions(false);
    }

    public void J6()
    {
        if (Core.isCompletedBefore(2858))
            return;

        Core.AddDrop("Mission 1 Item", "Datadisk 5", "Datadisk 4", "Fanciful Feather", "Datadisk 3", "Absorbent Mop", "Hyperium Spaceship Key");

        Core.EquipClass(ClassType.Farm);

        //J6 Quiz Game
        Story.ChainQuest(674);

        //Access Zephyrus
        Story.ChainQuest(693);

        //Zephyrus End
        if (!Story.QuestProgression(694))
        {
            JoinHyperium();
            Story.MapItemQuest(694, "zephyrus", new[] { 116, 117 });
        }

        //Mission1
        Story.KillQuest(698, "forest", "Zardman Grunt");

        //Mission2
        Story.KillQuest(699, "boxes", "Sneeviltron");

        Core.KillMonster("frozenfotia", "r5", "Left", "*", "Datadisk 5", isTemp: false);

        JoinHyperium();
        //A Gate and Terrible Monster
        Story.KillQuest(1171, "moonyard", "Junkyard Wall");

        //I'm on the Hunt, I'm after 58-Sr3
        Story.MapItemQuest(1172, "moonyard", 495);

        //Intruder Alert! Intruder Alert!
        if (!Story.QuestProgression(1173))
        {
            if (!Bot.Map.Name.Equals("moonyard"))
            {
                JoinHyperium();
                Core.Join("moonyard");
            }
            Core.Jump("MoonCut", "Left");
            Story.KillQuest(1173, "moonyardb", "Robo Guard", GetReward: false);
        }

        //Mission 3
        Story.KillQuest(1177, "marsh2", "Lesser Shadow Serpent");

        //Mission 4
        Story.KillQuest(1178, "marsh2", "Lesser Groglurk");

        if (!Core.CheckInventory("Datadisk 4"))
            Core.GetMapItem(1258, map: "sewer");

        //Planet Banzai!
        if (!Story.QuestProgression(2168))
        {
            JoinHyperium();
            Story.MapItemQuest(2168, "banzai", 1259);
        }

        //L.O.S.E.R. Battle!
        Story.ChainQuest(2169);

        //Down to the Wire
        Story.ChainQuest(2170);

        //J6 in SPAAAAACE!
        Story.ChainQuest(2171);

        //Mission 5
        Story.KillQuest(2173, "djinn", "Harpy");

        if (!Core.CheckInventory("Datadisk 3"))
            Story.KillQuest(2830, "xantown", "*", GetReward: false);

        //Fuel For Flight
        if (!Story.QuestProgression(2831))
        {
            Core.GetMapItem(1741, map: "timevoid");
            Core.EnsureAccept(2831);
            Core.KillMonster("sandsea", "r8", "Left", "*", "Cactus Creeper Oil", 3);
            Core.KillMonster("cloister", "r7", "Left", "Acornent", "Acornent Oil", 3);
            JoinHyperium();
            Core.Join("moonyard", "MoonCut", "Left");
            Core.KillMonster("moonyardb", "r4", "Left", "*", "Robo Dog Oil", 3);
            Core.KillMonster("farm", "Crop1", "Left", "*", "Scarecrow Canola Oil", 3);
            Core.EnsureComplete(2831);
        }

        //Food For Flight
        if (!Story.QuestProgression(2832))
        {
            Core.EnsureAccept(2832);
            Core.KillMonster("arcangrove", "Right", "Left", "*", "Bag of Peanuts", 4);
            Core.KillMonster("earthstorm", "r9", "Left", "Amethite", "Stick of Rock Candy", 3);
            Core.KillMonster("palooza", "Act1", "Left", "Tune-a-Fish", "Tune-A-Fish Tuna Can", 2);
            Core.KillMonster("giant", "r1", "Left", "*", "Bag of Pretzels", 4);
            Core.EnsureComplete(2832);
        }

        //Vomit Comet
        if (!Story.QuestProgression(2833))
        {
            Core.EnsureAccept(2833);
            Core.KillMonster("bamboo", "r3", "Left", "Tanuki", "Umeboshi Plum", 4);
            Core.KillMonster("yokaigrave", "Enter2", "Right", "*", "Ginger");
            Core.KillMonster("guru", "Field2", "Left", "*", "Peppermint Leaf", 3);
            Core.BuyItem("yulgar", 16, "Absorbent Mop");
            Core.EnsureComplete(2833);
        }

        Farm.ChronoSpanREP(2);
        //Sweet Dreams for a Safe Flight
        Story.BuyQuest(2834, "thespan", 439, "Comfy Pillow");

        //Reach VR Room
        Story.MapItemQuest(2837, "hyperspace", 1742);

        //Speak with H.A.L.
        Story.MapItemQuest(2838, "hyperspace", 1743);

        //Defeat H.A.L.
        Story.MapItemQuest(2839, "hyperspace", 1744);

        //Spare J6
        Story.ChainQuest(2856);

        //Choose J6 cutscene
        Story.MapItemQuest(2840, "hyperspace", 1745);

        JoinAlley();
        //Reduce, Respawn, Recycle
        Story.KillQuest(2841, "alley", "Trash Can");

        //Mind your Manners
        Story.KillQuest(2842, "alley", "Thug Bully");

        //FUNdraisers!
        Story.KillQuest(2843, "alley", "Thug Bully");

        //Bully the Thug
        Story.KillQuest(2849, "alley", "Thug Boss");

        //Security Breach
        Story.KillQuest(2844, "alley", new[] { "Guard Robot", "Security Cam" });

        //No Sniffin' Around
        Story.KillQuest(2845, "alley", "Guard Dog Robot");

        //Key to Winning
        Story.KillQuest(2846, "alley", "Guard Dog Robot|Security Cam|Guard Robot");

        //Watch Chapter 1
        Story.MapItemQuest(2850, "hyperspace", 1747);

        JoinAlley();
        //Watch Chapter 1
        Story.KillQuest(2851, "alley", "Thug Minion");

        //Watch Chapter 1
        Story.MapItemQuest(2852, "hyperspace", 1749);

        //Watch Chapter 1
        Story.ChainQuest(2858);

        void JoinHyperium()
        {
            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
            while (Bot.Player.Cell != "R10")
                Core.Jump("R10", "Up");
        }

        void JoinAlley()
        {
            if (Bot.Map.Name == "alley")
                return;
            Core.Join("hyperspace", "R10", "Up");
            Core.Join("alley");
        }
    }

}