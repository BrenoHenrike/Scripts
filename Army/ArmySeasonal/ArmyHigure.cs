/*
name: Complete Elegy of Madness Story, then farm Higure sword
description: This will complete the Astravia story and farm the seasonal Higure sword.
tags: story, quest, elegy-of-madness, darkon, complete, all, higure
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;
using System.Collections.Generic;

public class FarmHigure
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();
    public CoreAstravia Astravia => new CoreAstravia();

    public string OptionsStorage = "ArmyHigure";
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(ClassType.Solo);
        Prereqs();
        Higure();

        // Reset options
        Core.SetOptions(false);
    }

    void Prereqs()
    {
        Astravia.CompleteCoreAstravia();
    }

    void Higure()
    {
        Core.BankingBlackList.Add("Darkon's Receipt");
        
        // Farm Darkon's Receipt
        Core.AddDrop("Darkon's Receipt");
        Core.AddDrop("Nulgath's Mask");
        Core.RegisterQuests(7326);
        while (!Bot.ShouldExit && !Core.CheckInventory("Darkon's Receipt", 66))
            ArmyHunt("tercessuinotlim", new[] { "Nulgath" }, "Nulgath's Mask", ClassType.Solo, false, 1);
        Core.CancelRegisteredQuests();

        // Farm La's Gratitude
        Core.RegisterQuests(8001);
        Army.AggroMonMIDs(4768, 4770);
        Army.AggroMonStart("astravia");
        Army.DivideOnCells("r6", "r7", "r8");
        while (!Bot.ShouldExit && !Core.CheckInventory("La's Gratitude", 66))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();

        // Farm Astravian Medal
        Core.RegisterQuests(8257);
        Army.AggroMonMIDs(4930, 4931, 4932, 4929);
        Army.AggroMonStart("astraviacastle");
        Army.DivideOnCells("r11", "r6", "r3", "r4");
        while (!Bot.ShouldExit && !Core.CheckInventory("Astravian Medal", 66))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();

        // Farm A Melody
        Core.RegisterQuests(8396);
        Army.AggroMonMIDs(5016, 5012, 5013);
        Army.AggroMonStart("astraviajudge");
        Army.DivideOnCells("r11", "r3", "r2");
        while (!Bot.ShouldExit && !Core.CheckInventory("A Melody", 66))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();

        // Farm Suki's Prestige
        Core.RegisterQuests(8602);
        Army.AggroMonMIDs(5117, 5116, 5119, 5120, 5121);
        Army.AggroMonStart("astraviapast");
        Army.DivideOnCells("r4", "r7", "r8", "r9");
        while (!Bot.ShouldExit && !Core.CheckInventory("Suki's Prestige", 66))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();

        // Farm Ancient Remnant
        Core.RegisterQuests(8641);
        Army.AggroMonMIDs(5137, 5136, 5138);
        Army.AggroMonStart("firstobservatory");
        Army.DivideOnCells("r10a", "r6", "r7");
        while (!Bot.ShouldExit && !Core.CheckInventory("Ancient Remnant", 66))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();

        // Farm Mourning Flower
        Core.RegisterQuests(8688);
        Army.AggroMonMIDs(5164, 5165, 5158, 5162, 5161);
        Army.AggroMonStart("genesisgarden");
        Army.DivideOnCells("r11", "r9", "r6");
        while (!Bot.ShouldExit && !Core.CheckInventory("Mourning Flower", 66))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();

        // Farm Unfinished Musical Score
        ArmyHunt("theworld", new[] { "Encore Darkon" }, "Unfinished Musical Score", ClassType.Solo, false, 66);

        // Farm Bounty Hunter Dubloon
        Core.FarmingLogger("Bounty Hunter Dubloon", 222);
        Core.RegisterQuests(9394);
        while (!Bot.ShouldExit && !Core.CheckInventory("Bounty Hunter Dubloon", 222))
            Core.HuntMonsterMapID("dreadspace", 48, "Trobble Captured");
        Bot.Wait.ForPickup("Bounty Hunter Dubloon");
        Core.CancelRegisteredQuests();

        // Buy Higure Sword
        Core.BuyItem("pirates", 2338, "Higure", 1, 79817);
    }

//⠄⠄⠄⠄⠄⠄⠄⠄⢀⢀⣴⣿⣿⣷⣶⣤⣄⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
//⠄⠄⠄⠄⠄⢀⣤⡶⠿⢘⣥⠢⠐⠗⣹⣿⣿⣿⣤⡀⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
//⠄⠄⠄⠄⠄⠘⣅⣂⠹⣪⣭⣥⣶⣿⡿⠿⢭⡻⣿⣷⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄
//⠄⠄⢀⣤⣤⡀⠄⣭⣧⣾⡿⠿⡋⢅⡪⠅⣢⣿⡿⠟⢁⣶⣶⣶⣤⣠⣄⡀⠄⠄
//⢠⣴⣿⣿⣟⣤⣤⡉⠭⣑⡨⢔⣊⣵⣶⡿⠛⢉⣴⡾⠿⠿⣿⣿⣿⣎⠻⣿⣦⡀
//⣼⣧⢻⣿⣿⣿⡈⣿⢰⢰⠌⣻⣭⣭⣶⡷⣠⡤⠶⠾⠛⢓⣒⣮⣝⡻⠸⣼⣿⣿
//⣿⣝⢶⣿⣿⣿⡃⠄⢏⣸⡄⢻⡿⣿⣟⣵⠶⢛⣛⣛⣛⡒⠦⣝⠿⣿⣦⡙⣿⡿
//⠻⣿⣿⣿⣿⣿⣷⣦⣜⡿⣿⣄⢓⡘⠃⣴⣾⣿⣿⣿⣿⢹⣯⣶⣅⢺⣿⡇⠻⠁
//⠄⠈⠛⠻⣿⣿⣿⣿⣿⣿⣿⡾⣿⣷⣾⣝⣻⢿⣿⣿⣿⠸⣛⣿⡟⣢⢻⣿⠄⠄
//⠄⠄⠄⠄⠘⢿⣿⣿⣿⣿⣿⣷⣦⣭⣿⣿⣿⣦⣵⡾⢃⣾⣿⣿⢱⡿⣸⠋⠄⠄
//⠄⠄⠄⠄⠄⠄⢻⣿⢿⣿⣿⠻⣿⣿⡿⠿⣟⣛⣉⣰⣿⣿⣿⠇⠛⠃⠄⠄⠄⠄
//⠄⠄⠄⠄⠄⠄⠄⠉⠲⣝⣫⣓⡙⣿⣜⣛⣛⣛⣻⡯⠹⠛⠁⠄⠄⠄⠄⠄⠄⠄
//⠄⠄⠄⠄⠄⠄⠄⠄⠄⠈⠙⠛⢻⡈⢿⡿⠟⠛⠁⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄⠄

    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);
        Army.waitForParty(map);

        Core.EquipClass(classType);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            if (monsters == new[] { "Hydra Head 90" })
            {
                Core.Logger("Swapping classes to 1 of the 3\n" +
                ">> so that we can be sure you arent doing multi targeting\n" +
                ">> as itd fuck it up");
                
                foreach (string Class in new[] { "StoneCrusher", "Lord of Order", "Void Highlord" })
                    if (Core.CheckInventory(Class))
                        Core.Equip(Class);

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                    Bot.Combat.Attack("*");
                break;
            }

            else if (monsters == new[] { "Tigoras" })
            {
                Core.KillTrigoras(item, quant, 1, isTemp);
                break;
            }

            else if (monsters != new[] { "Tigoras" } || monsters != new[] { "Hydra Head 90" })
                Bot.Combat.Attack("*");
        }
        Core.JumpWait();
        Army.AggroMonStop(true);

        while (!Bot.ShouldExit && Bot.Player.InCombat)
        {
            Core.JumpWait();
            Bot.Sleep(2500);
        }
        Army.waitForParty(map, item);
    }
}