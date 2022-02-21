//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs

using RBot;

public class J6Saga
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        J6();

        Core.SetOptions(false);
    }

    public void J6()
    {
        Core.AddDrop("J6's Secret Hideout Map", "Dwakel Decoder", "Mission 1 Item", "Datadisk 5",
        "Datadisk 4", "Fanciful Feather", "Datadisk 3", "Absorbent Mop", "Hyperium Spaceship Key");
        if(!Bot.Quests.IsUnlocked(2846))
        {
            if (!Bot.Quests.IsUnlocked(2849))
            {
                if (!Bot.Quests.IsUnlocked(2834))
                {
                    if (!Bot.Quests.IsUnlocked(2830))
                    {
                        if (!Bot.Quests.IsUnlocked(1178))
                        {
                            if (!Bot.Quests.IsUnlocked(1173))
                            {
                                if (!Bot.Quests.IsUnlocked(1172))
                                {
                                    if (!Bot.Quests.IsUnlocked(699))
                                    {
                                        if (!Bot.Quests.IsUnlocked(698))
                                        {
                                            if (!Core.CheckInventory("J6's Secret Hideout Map"))
                                                Core.KillMonster("j6", "R4", "Left", "*", "J6's Secret Hideout Map", isTemp: false);
                                            if (!Core.CheckInventory("Dwakel Decoder"))
                                                Core.GetMapItem(106, map: "crashsite");
                                            if (!Bot.Quests.IsUnlocked(693))
                                            {
                                                Core.SendPackets("%xt%zm%serverUseItem%327106%+%5041%345,200%saloon%");
                                                Core.EnsureAccept(674);
                                                Core.GetMapItem(109, map: "saloon");
                                                Core.EnsureComplete(674);
                                            }
                                            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
                                            while(!Bot.Player.Cell.Equals("R10"))
                                            Core.Jump("R10", "Up");
                                            Core.SendPackets($"%xt%zm%cmd%328430%tfer%{Bot.Player.Username}%zephyrus%Enter%Spawn%");
                                            Core.EnsureAccept(694);
                                            Core.GetMapItem(116, map: "zephyrus");
                                            Core.GetMapItem(117, map: "zephyrus");
                                            Core.EnsureComplete(694);
                                        }
                                        Core.EnsureAccept(698);
                                        Core.KillMonster("forest", "Forest3", "Left", "*", "Mission 1 Item", isTemp: false);
                                        Core.EnsureComplete(698);
                                    }
                                    if(!Bot.Quests.IsUnlocked(1171))
                                    {
                                        Core.EnsureAccept(699);
                                        Core.KillMonster("boxes", "Boss", "Left", "*", "Mission 2 Item");
                                        Core.EnsureComplete(699);
                                    }
                                    if(!Core.CheckInventory("Datadisk 5"))
                                    Core.KillMonster("frozenfotia", "r5", "Left", "*", "Datadisk 5", isTemp: false);
                                    Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
                                    while(!Bot.Player.Cell.Equals("R10"))
                                    Core.Jump("R10", "Up");
                                    Core.SendPackets($"%xt%zm%cmd%507613%tfer%{Bot.Player.Username}%moonyard%Enter%Spawn%");
                                    Core.EnsureAccept(1171);
                                    Core.HuntMonster("moonyard", "Junkyard Wall", "Gate Unlocked");
                                    Core.EnsureComplete(1171);
                                }
                                Core.MapItemQuest(1172, "moonyard", 495, GetReward: false);
                            }
                            if (!Bot.Quests.IsUnlocked(1177))
                            {
                                if (!Bot.Map.Name.Equals("moonyard"))
                                {
                                    Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
                                    while(!Bot.Player.Cell.Equals("R10"))
                                    Core.Jump("R10", "Up");
                                    Core.SendPackets($"%xt%zm%cmd%507613%tfer%{Bot.Player.Username}%moonyard%Enter%Spawn%");
                                    Core.Jump("MoonCut", "Left");
                                }
                                Core.SendPackets($"%xt%zm%cmd%510002%tfer%{Bot.Player.Username}%moonyardb%r24%Right%");
                                Core.EnsureAccept(1173);
                                Core.HuntMonster("moonyardb", "Robo Guard", "Drone Head");
                                Core.EnsureComplete(1173);
                            }
                                Core.EnsureAccept(1177);
                                Core.KillMonster("marsh2", "End", "Left", "Lesser Shadow Serpent", "Serpent Scales");
                                Core.EnsureComplete(1177);
                        }
                        if(!Core.CheckInventory("Datadisk 4"))
                        {
                            Core.EnsureAccept(1178);
                            Core.KillMonster("marsh2", "Forest3", "Left", "Lesser Groglurk", "Omega Horn");
                            Core.EnsureComplete(1178);
                            Core.GetMapItem(1258, map: "sewer");
                        }

                        if (!Bot.Quests.IsUnlocked(2169))
                        {
                            Core.EnsureAccept(2168);
                            Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
                            while(!Bot.Player.Cell.Equals("R10"))
                            Core.Jump("R10", "Up");
                            Core.SendPackets($"%xt%zm%cmd%512688%tfer%{Bot.Player.Username}%banzai%Enter%Spawn%");
                            Core.GetMapItem(1259, map: "banzai");
                            Core.EnsureComplete(2168);
                        }
                        if(!Bot.Quests.IsUnlocked(2170))
                            Core.ChainComplete(2169);
                        if(!Bot.Quests.IsUnlocked(2171))
                            Core.ChainComplete(2170);
                        if(!Bot.Quests.IsUnlocked(2173))
                            Core.ChainComplete(2171);
                        Core.KillQuest(2173, "djinn", "Harpy");
                    }
                    if(!Core.CheckInventory("Datadisk 3"))
                        Core.KillQuest(2830, "xantown", "*");
                    if(!Bot.Quests.IsUnlocked(2831))
                        Core.GetMapItem(1741, map: "timevoid");
                    if(!Bot.Quests.IsUnlocked(2832))
                    {
                        Core.EnsureAccept(2831);
                        Core.KillMonster("sandsea", "r8", "Left", "*", "Cactus Creeper Oil", 3);
                        Core.KillMonster("cloister", "r7", "Left", "Acornent", "Acornent Oil", 3);
                        Core.SendPackets("%xt%zm%serverUseItem%327705%+%5041%525,275%hyperium%");
                        while(!Bot.Player.Cell.Equals("R10"))
                        Core.Jump("R10", "Up");
                        Core.SendPackets($"%xt%zm%cmd%507613%tfer%{Bot.Player.Username}%moonyard%Enter%Spawn%");
                        Core.Jump("MoonCut", "Left");
                        Core.SendPackets($"%xt%zm%cmd%510002%tfer%{Bot.Player.Username}%moonyardb%r24%Right%");
                        Core.KillMonster("moonyardb", "r4", "Left", "*", "Robo Dog Oil", 3);
                        Core.KillMonster("farm", "Crop1", "Left", "*", "Scarecrow Canola Oil", 3);
                        Core.EnsureComplete(2831);
                    }
                    if(!Core.QuestProgression(2832))
                    {
                        Core.EnsureAccept(2832);
                        Core.KillMonster("arcangrove", "Right", "Left", "*", "Bag of Peanuts", 4);
                        Core.KillMonster("earthstorm", "r9", "Left", "Amethite", "Stick of Rock Candy", 3);
                        Core.KillMonster("palooza", "Act1", "Left", "Tune-a-Fish", "Tune-A-Fish Tuna Can", 2);
                        Core.KillMonster("giant", "r1", "Left", "*", "Bag of Pretzels", 4);
                        Core.EnsureComplete(2832);
                    }
                    if (!Core.QuestProgression(2833))
                    {
                        Core.EnsureAccept(2833);
                        Core.KillMonster("bamboo", "r3", "Left", "Tanuki", "Umeboshi Plum", 4);
                        Core.KillMonster("yokaigrave", "Enter2", "Right", "*", "Ginger");
                        Core.KillMonster("guru", "Field2", "Left", "*", "Peppermint Leaf", 3);
                        Core.BuyItem("yulgar", 16, "Absorbent Mop");
                        Core.EnsureComplete(2833);
                    }
                }
                if(!Core.QuestProgression(2834))
                {
                    Farm.ChronoSpanREP(2);
                    Core.BuyQuest(2834, "thespan", 439, "Comfy Pillow");
                }
                Core.Join("hyperspace");
                Core.MapItemQuest(2837, "hyperspace", 1742);
                Core.MapItemQuest(2838, "hyperspace", 1743);
                Core.MapItemQuest(2839, "hyperspace", 1744);
                Core.ChainQuest(2856);
                Core.MapItemQuest(2840, "hyperspace", 1745);
                if(!Bot.Map.Name.Equals("hyperspace"))
                    Core.Join("hyperspace");
                Core.Jump("R10", "Up");
                Core.SendPackets($"%xt%zm%cmd%136830%tfer%{Bot.Player.Username}%Alley%Enter%Spawn%");
                Core.KillQuest(2841, "alley", "Trash Can");
                Core.KillQuest(2842, "alley", "Thug Bully");
                Core.KillQuest(2843, "alley", "Thug Bully");
            }
            Core.KillQuest(2849, "alley", "Thug Boss");
            Core.KillQuest(2844, "alley", new[] {"Guard Robot", "Security Cam"});
            Core.KillQuest(2845, "alley", "Guard Dog Robot");
        }
        Core.KillQuest(2846, "alley", "Guard Dog Robot|Security Cam|Guard Robot");
        Core.MapItemQuest(2850, "hyperspace", 1747);
        Core.KillQuest(2851, "alley", "Thug Minion");
        Core.MapItemQuest(2852, "hyperspace", 1749);
        Core.ChainQuest(2858);
    }   
}