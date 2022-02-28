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
        if(Core.isCompletedBefore(2858))
            return;
        Core.AddDrop("J6's Secret Hideout Map", "Dwakel Decoder", "Mission 1 Item", "Datadisk 5",
        "Datadisk 4", "Fanciful Feather", "Datadisk 3", "Absorbent Mop", "Hyperium Spaceship Key");
        Core.EquipClass(ClassType.Farm);
        if (!Core.CheckInventory("J6's Secret Hideout Map"))
            Core.KillMonster("j6", "R4", "Left", "*", "J6's Secret Hideout Map", isTemp: false);
        if (!Core.CheckInventory("Dwakel Decoder"))
            Core.GetMapItem(106, map: "crashsite");
        if (!Story.QuestProgression(674))
        {
            Core.SendPackets("%xt%zm%serverUseItem%327106%+%5041%345,200%saloon%");
            Story.MapItemQuest(674, "saloon", 109);
        }
        if (!Story.QuestProgression(694))
        {
            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
            while(!Bot.Player.Cell.Equals("R10"))
                Core.Jump("R10", "Up");
            Core.SendPackets($"%xt%zm%cmd%328430%tfer%{Bot.Player.Username}%zephyrus%Enter%Spawn%");
            Core.EnsureAccept(694);
            Core.GetMapItem(116, map: "zephyrus");
            Core.GetMapItem(117, map: "zephyrus");
            Core.EnsureComplete(694);
        }                                
        Story.KillQuest(698, "forest", "Zardman Grunt");
        Story.KillQuest(699, "boxes", "Sneeviltron");
        if(!Core.CheckInventory("Datadisk 5"))
            Core.KillMonster("frozenfotia", "r5", "Left", "*", "Datadisk 5", isTemp: false);
        if (!Story.QuestProgression(1171))
        {
            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
            while (!Bot.Player.Cell.Equals("R10"))
                Core.Jump("R10", "Up");
            Core.SendPackets($"%xt%zm%cmd%507613%tfer%{Bot.Player.Username}%moonyard%Enter%Spawn%");
            Story.KillQuest(1171, "moonyard", "Junkyard Wall");
        }
        Story.MapItemQuest(1172, "moonyard", 495);
        if (!Story.QuestProgression(1173))
        {
            if (!Bot.Map.Name.Equals("moonyard"))
            {
                Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
                while(!Bot.Player.Cell.Equals("R10"))
                    Core.Jump("R10", "Up");
                Core.SendPackets($"%xt%zm%cmd%507613%tfer%{Bot.Player.Username}%moonyard%Enter%Spawn%");
            }
            Core.Jump("MoonCut", "Left");
            Core.SendPackets($"%xt%zm%cmd%510002%tfer%{Bot.Player.Username}%moonyardb%r24%Right%");
            Story.KillQuest(1173, "moonyardb", "Robo Guard", GetReward: false);
        }
        Story.KillQuest(1177, "marsh2", "Lesser Shadow Serpent");
        Story.KillQuest(1178, "marsh2", "Lesser Groglurk");
        if(!Core.CheckInventory("Datadisk 4"))
        Core.GetMapItem(1258, map: "sewer");                      
        if (!Story.QuestProgression(2168))
        {
            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
            while(!Bot.Player.Cell.Equals("R10"))
                Core.Jump("R10", "Up");
            Core.SendPackets($"%xt%zm%cmd%512688%tfer%{Bot.Player.Username}%banzai%Enter%Spawn%");
            Story.MapItemQuest(2168, "banzai", 1259);
        }
        Story.ChainQuest(2169);
        Story.ChainQuest(2170);
        Story.ChainQuest(2171);
        Story.KillQuest(2173, "djinn", "Harpy");
        if(!Core.CheckInventory("Datadisk 3"))
            Story.KillQuest(2830, "xantown", "*", GetReward: false);
        if (!Story.QuestProgression(2831))
        {
            Core.GetMapItem(1741, map: "timevoid");
            Core.EnsureAccept(2831);
            Core.KillMonster("sandsea", "r8", "Left", "*", "Cactus Creeper Oil", 3);
            Core.KillMonster("cloister", "r7", "Left", "Acornent", "Acornent Oil", 3);
            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
            while (!Bot.Player.Cell.Equals("R10"))
                Core.Jump("R10", "Up");
            Core.SendPackets($"%xt%zm%cmd%507613%tfer%{Bot.Player.Username}%moonyard%Enter%Spawn%");
            Core.Jump("MoonCut", "Left");
            Core.SendPackets($"%xt%zm%cmd%510002%tfer%{Bot.Player.Username}%moonyardb%r24%Right%");
            Core.KillMonster("moonyardb", "r4", "Left", "*", "Robo Dog Oil", 3);
            Core.KillMonster("farm", "Crop1", "Left", "*", "Scarecrow Canola Oil", 3);
            Core.EnsureComplete(2831);
        }
        if (!Story.QuestProgression(2832))
        {
            Core.EnsureAccept(2832);
            Core.KillMonster("arcangrove", "Right", "Left", "*", "Bag of Peanuts", 4);
            Core.KillMonster("earthstorm", "r9", "Left", "Amethite", "Stick of Rock Candy", 3);
            Core.KillMonster("palooza", "Act1", "Left", "Tune-a-Fish", "Tune-A-Fish Tuna Can", 2);
            Core.KillMonster("giant", "r1", "Left", "*", "Bag of Pretzels", 4);
            Core.EnsureComplete(2832);
        }
        if (!Story.QuestProgression(2833))
        {
            Core.EnsureAccept(2833);
            Core.KillMonster("bamboo", "r3", "Left", "Tanuki", "Umeboshi Plum", 4);
            Core.KillMonster("yokaigrave", "Enter2", "Right", "*", "Ginger");
            Core.KillMonster("guru", "Field2", "Left", "*", "Peppermint Leaf", 3);
            Core.BuyItem("yulgar", 16, "Absorbent Mop");
            Core.EnsureComplete(2833);
        }
        if (!Story.QuestProgression(2834))
        {
            Farm.ChronoSpanREP(2);
            Story.BuyQuest(2834, "thespan", 439, "Comfy Pillow");
        }
        Core.Join("hyperspace");
        Story.MapItemQuest(2837, "hyperspace", 1742);
        Story.MapItemQuest(2838, "hyperspace", 1743);
        Story.MapItemQuest(2839, "hyperspace", 1744);
        Story.ChainQuest(2856);
        Story.MapItemQuest(2840, "hyperspace", 1745);
        if (!Story.QuestProgression(2841))
        {
            if (!Bot.Map.Name.Equals("hyperspace"))
                Core.Join("hyperspace");
            Core.Jump("R10", "Up");
            Core.SendPackets($"%xt%zm%cmd%136830%tfer%{Bot.Player.Username}%Alley%Enter%Spawn%");
            Story.KillQuest(2841, "alley", "Trash Can");
        }
        Story.KillQuest(2842, "alley", "Thug Bully");
        Story.KillQuest(2843, "alley", "Thug Bully");
        Story.KillQuest(2849, "alley", "Thug Boss");
        Story.KillQuest(2844, "alley", new[] {"Guard Robot", "Security Cam"});
        Story.KillQuest(2845, "alley", "Guard Dog Robot");
        Story.KillQuest(2846, "alley", "Guard Dog Robot|Security Cam|Guard Robot");
        Story.MapItemQuest(2850, "hyperspace", 1747);
        Story.KillQuest(2851, "alley", "Thug Minion");
        Story.MapItemQuest(2852, "hyperspace", 1749);
        Story.ChainQuest(2858);
    }   
}