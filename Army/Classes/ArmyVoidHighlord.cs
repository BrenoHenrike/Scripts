/*
name: Void HighLord Army
description: uses an army to get the void highlord class
tags: void highlord, class, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Army/ArmyNulgath/ArmyEmblemOfNulgath.cs
//cs_include Scripts/Army/ArmyNulgath/ArmyVoucherItemofNulgath.cs
//cs_include Scripts/Army/ArmyNulgath/ArmyTaintedGem.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Monsters;

public class VHLArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreDailies Daily = new();
    public CoreNation Nation = new();
    public CoreVHL VHL = new();
    public AssistingCragAndBamboozle ACAB = new();
    public SevenCircles SC = new();
    public ArmyEmblemOfNulgath EmblemOfNulgath = new();
    public ArmyVoucherItemofNulgath VoucherItemofNulgath = new();
    public ArmyTaintedGem TaintedGem = new();
    private CoreArmyLite Army = new();
    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();
    private string[] EmblemItems = { "Fiend Seal", "Gem of Domination", "Emblem of Nulgath" };


    public string OptionsStorage = "VHLArmy";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public string[] ItemsToFarm =
    {
        "Nulgath's Approval",
        "Archfiend's Favor",
        "Emblem of Nulgath",
        "Unidentified 13",
        "Gem of Nulgath",
        "Totem of Nulgath",
        "Tainted Gem",
        "Voucher of Nulgath (non-mem)",
        "Dark Crystal Shard",
        "Blood Gem of the Archfiend"
    };
    public string[] QuestRewards =
    {
        //VHL Challenge
        "Void Highlord Armor",
        "Helm of the Highlord",
        "Highlord's Void Wrap",
        "Unidentified 10",
        "Roentgenium of Nulgath",

    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(QuestRewards.Concat(ItemsToFarm).ToArray());

        Core.Logger("This script, and any army script that requires more then 1 map is entirely broken, and there is no plan to fix them, for army stuff please use GrimLi");
        // GetVHL();

        Core.SetOptions(false);
    }

    public void GetVHL()
    {
        // Core.DL_Enable();

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory("Void Highlord"))
        {
            Core.Logger("You already have the Void Highlord class retard!");
            return;
        }

        //===============================================================
        // Have to start handing in Roents here because of maxed Tainteds
        //===============================================================
        ElderBloodChecking();
        if (!Core.CheckInventory("Roentgenium of Nulgath", 10))
        {
            PreReqs();
            VHL.VHLChallenge(10);
            // //Army.waitForParty("whitemap", "Roentgenium of Nulgath");
            // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");
        }

        //======================Last 700 Tainted Gems====================
        Escherion("Tainted Gem", 700 - (100 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")));
        // //Army.waitForParty("whitemap", "Tainted Gem");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        //=======================Last 2 Uni 13===========================
        Larvae("Unidentified 13", 15 - Bot.Inventory.GetQuantity("Roentgenium of Nulgath"));
        // //Army.waitForParty("whitemap", "Unidentified 13");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        //=============DCS check in case somehow not enough==============
        DCSCheck();
        // //Army.waitForParty("whitemap", "Dark Crystal Shard");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        //========================Blood Gems============================
        BloodGems(30);
        // //Army.waitForParty("whitemap", "Blood Gem");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        //Check for elder blood here in case previous part of the farm
        //went over into next day and needed for the roents or crystals
        ElderBloodChecking();

        //========================Last 5 Roents========================
        // //Army.waitForParty("whitemap", "Roentgenium of Nulgath");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");
        VHL.VHLChallenge(15);

        //========================Void Crystals========================
        VHL.VHLCrystals();

        //========================Buy and rank up VHL==================
        BuyVHL();
    }

    public void ElderBloodChecking()
    {
        Daily.EldersBlood();
        if (!Core.CheckInventory("Elder's Blood", 14))
        {
            Core.Logger("Script waits until you have 14 Elder's Blood to start the farm, run ElderBlood.cs or this script every day until you have 14 Elder's Blood. Jecht assumed your army will take 3 days to complete it so let him know if it took longer or less to adjust this setting.");
            return;
        }
    }

    public void PreReqs()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;
        /*
        ********************************************************************************
        *****************************PREFarm Zone***************************************
        ********************************************************************************
        */
        Core.Logger("Starting prerequisites");
        Bot.Drops.Add(QuestRewards);
        //Check if Elder's Blood is available to run
        if (!Bot.Quests.IsDailyComplete(802))
            Daily.EldersBlood();
        /*
        ==================================================================================================
        this snippet is to check current amount of roents vs elder blood 
        in case we change the minimum elder blood required in the future and need this check for something
        ==================================================================================================

        Core.Unbank("Roentgenium of Nulgath");
        Core.Unbank("Elder's Blood");
        if (Bot.Inventory.GetQuantity("Elder's Blood") + Bot.Inventory.GetQuantity("Roentgenium of Nulgath") + 2 != 17)
            Daily.EldersBlood();
        */
        ArchfiendsFavorAndNulgathsApproval(4500);
        foreach (string reward in new[] { "Archfiends Favor", "Nulgaths Approval" })
        {
            // //Army.waitForParty("whitemap", reward);
            // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");
        }

        Emblems(quantity: 300);
        // //Army.waitForParty("whitemap", "Emblem of Nulgath");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        Larvae("Unidentified 13", 15 - Bot.Inventory.GetQuantity("Roentgenium of Nulgath"));
        // //Army.waitForParty("whitemap", "Unidentified 13");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        FarmGemsofNulgath(quant: 450);
        // //Army.waitForParty("whitemap", "Gem of Nulgath");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        FarmTotemsOfNulgath(quant: 15);
        // //Army.waitForParty("whitemap", "Totem of Nulgath");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        Escherion("Tainted Gem", 1000 - (100 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")));
        // //Army.waitForParty("whitemap", "Tainted Gem");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        Escherion("Voucher of Nulgath (non-mem)", 1);
        // //Army.waitForParty("whitemap", "Voucher of Nulgath (non-mem)");
        // Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");

        /* Farm lvl 80 and get minimum gold required to 
           buy 30 blood gems using swindle's ripoff emporium 
           and the 15 Nulgath's Chocolates at 2 mil each 
        */
        SCW(80, 43350000);
        Core.Logger("Prerequisites Finished.");
    }

    public void BuyVHL(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Void Highlord"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Void Highlord");
            return;
        }

        Adv.BuyItem("tercessuinotlim", 1355, "Void Highlord");

        if (rankUpClass)
            Adv.RankUpClass("Void Highlord");
    }

    /// <summary>
    /// Farms Archfiend's Favor and Nulgath's Approval in Evil War Nul.
    /// </summary>
    /// <param name="ArchfiendsFavorQuan">Desired quantity for Archfiend's Favor, 5000 = max stack</param>
    /// <param name="NulgathsApprovalQuant">Desired quantity for Nulgath's Approval, 5000 = max stack</param>
    public void ArchfiendsFavorAndNulgathsApproval(int ArchfiendsFavorQuan = 1, int NulgathsApprovalQuant = 1)
    {
        string[] itemsToFarm = { "Archfiend's Favor", "Nulgath's Approval" };

        if (Core.CheckInventory(itemsToFarm[0], ArchfiendsFavorQuan - (300 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")))
        && Core.CheckInventory(itemsToFarm[1], NulgathsApprovalQuant - (300 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath"))))
        {
            foreach (string reward in itemsToFarm)
            {
                Core.JumpWait();
                Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");
                //Army.waitForParty("evilwarnul", reward);
            }

            Core.Logger($"\"Archfiend's Favor\", x{ArchfiendsFavorQuan}, obtained");
            Core.Logger($"\"Nulgath's Approval\", x{NulgathsApprovalQuant}, obtained");
            return;
        }

        Core.Unbank("Roentgenium of Nulgath");
        Core.AddDrop("Archfiend's Favor", "Nulgath's Approval");
        Core.EquipClass(ClassType.Farm);

        int[] aggroMonMIDs = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        if (!string.IsNullOrEmpty(Bot.Config!.Get<string>("player4")))
        {
            aggroMonMIDs = aggroMonMIDs.Concat(new[] { 10, 11, 12, 13, 14, 15 }).ToArray();
        }

        Army.AggroMonMIDs(aggroMonMIDs);
        Army.AggroMonStart("evilwarnul");
        Army.DivideOnCells("r2", "r3", "r4", "r5", "r6");

        

        while (!Bot.ShouldExit
        && !(Core.CheckInventory(itemsToFarm[0], ArchfiendsFavorQuan - (300 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")))
        && Core.CheckInventory(itemsToFarm[1], NulgathsApprovalQuant - (300 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")))))
        {
            Bot.Combat.Attack("*");
        }

        Army.AggroMonStop(true);
        Core.JumpWait();
    }



    void Emblems(string item = "Emblem of Nulgath", int quantity = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quantity))
        {
            //Army.waitForParty("shadowblast", item);
            Core.Logger($"{item}, x{quantity}, obtained");
            return;
        }

        Core.Unbank("Roentgenium of Nulgath");

        if (!Core.CheckInventory("Nation Round 4 Medal"))
        {
            Core.Logger("Nation Round 4 Medal not found, getting it for you");
            Nation.NationRound4Medal();
        }

        Core.EquipClass(ClassType.Farm);
        Core.AddDrop(item);
        Core.RegisterQuests(4748);

        Army.AggroMonStart("shadowblast");
        // SetAggro();

        

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quantity - (20 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath"))))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }

    void VoucherItemOfNulgathQuest(string item = "Essence of Nulgath", int quant = 60)
    {
        // 5357 is ID for Totem of Nulgath, 6136 is ID for Gem of Nulgath
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
        {
            //Army.waitForParty("tercessuinotlim", item);
            Core.Logger($"{item}, x{quant}, obtained");
            return;
        }

        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Army.AggroMonMIDs(2, 3, 4, 5);
        Army.AggroMonStart("tercessuinotlim");
        Army.DivideOnCells("Enter", "m1", "m2");

        

        // Attack monsters until the inventory is filled with the specified quantity
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Bot.Combat.Attack("*");
            // Add a delay between attacks to avoid spamming server requests
            Core.Sleep();
        }

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
    }


    void FarmGemsofNulgath(string item = "Gem of Nulgath", int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
        {
            //Army.waitForParty("tercessuinotlim", item);
            Core.Logger($"{item}, x{quant}, obtained");
            return;
        }

        Core.Unbank("Roentgenium of Nulgath");
        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant - (20 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")));

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant - (20 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath"))))
        {
            Core.EnsureAccept(4778);
            VoucherItemOfNulgathQuest();
            // Add a delay between monster kills to avoid spamming server requests
            Core.Sleep();
            Core.EnsureComplete(4778, 6136);
        }
    }


    void FarmTotemsOfNulgath(string item = "Totem of Nulgath", int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
        {
            //Army.waitForParty("tercessuinotlim", item);
            Core.Logger($"{item}, x{quant}, obtained");
            return;
        }

        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EnsureAccept(4778);
            VoucherItemOfNulgathQuest();
            // Add a delay between monster kills to avoid spamming server requests
            Core.Sleep();
            Core.EnsureComplete(4778, 5357);
        }
    }


    void SCW(int Level, int goldamount)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Player.Gold >= goldamount - (900000 * Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")) - (2000000 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")) && Bot.Player.Level >= Level)
        {
            Core.Logger($"Gold: {goldamount}, Level: {Level} Reached.");
            return;
        }

        SC.CirclesWar(true);

        Core.RegisterQuests(7979, 7980, 7981);

        Army.AggroMonMIDs(1, 2, 3, 4, 5, 6);
        Army.AggroMonStart("sevencircleswar");
        Army.DivideOnCells("Enter", "r2", "r3");

        

        while (!Bot.ShouldExit && Bot.Player.Gold < goldamount - (900000 * Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")) - (2000000 * Bot.Inventory.GetQuantity("Roentgenium of Nulgath")) && Bot.Player.Level < Level)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Gold, false);
        Core.CancelRegisteredQuests();
    }


    public void DCSCheck()
    {
        if (Core.CheckInventory("Dark Crystal Shard", 200))
        {
            Core.Logger($"\"Dark Crystal Shard\", x200, obtained");
            return;
        }

        Escherion("Dark Crystal Shard", 200);
    }

    void BloodGems(int quantity)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory("Blood Gem of the Archfiend", quantity))
        {
            //Army.waitForParty("tercessuinotlim", "Blood Gem of the Archfiend");
            Core.Logger($"\"Blood Gem of the Archfiend\", x{quantity}, obtained");
            return;
        }

        SCW(80, (quantity - Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")) * 900000);

        while (Bot.Inventory.GetQuantity("Blood Gem of the Archfiend") < quantity)
        {
            Escherion("Unidentified 10", 200 + ((quantity - Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")) / 2 + 1) * 50);
            // Receipt of Swindle
            Core.BuyItem("tercessuinotlim", 1951, 57446, 3 * ((quantity - Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")) / 2 + 1), 7904);
            // Blood Gem of the Archfiend
            Core.BuyItem("tercessuinotlim", 1951, 22332, (quantity - Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")) / 2 + 1, 7913);
        }
    }

    void Escherion(string? item, int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
        {
            //Army.waitForParty("escherion", item);
            Core.Logger($"{item}, x{quant}, obtained");
            return;
        }

        Core.FarmingLogger(item, quant);

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Relic of Chaos");

        Core.RegisterQuests(2857);
        Core.Join("escherion");
        Core.EquipClass(ClassType.Farm);
        Army.AggroMonCells("Boss");
        Army.AggroMonStart("escherion");
        Army.DivideOnCells("Boss");

        Core.KillEscherion(item, quant, false);

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }


    void Larvae(string? item, int quant)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
        {
            //Army.waitForParty("elemental", item);
            Core.Logger($"{item}, x{quant}, obtained");
            return;
        }

        Core.FarmingLogger(item, quant);
        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Relic of Chaos");

        int questId = Bot.Quests.IsAvailable(2568) ? 2568 : 2566;
        Core.RegisterQuests(questId);

        bool manaEnergyNeeded = !Core.CheckInventory("Mana Energy for Nulgath", 13);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Army.AggroMonStop(true);
            Core.JumpWait();
            Core.EquipClass(manaEnergyNeeded ? ClassType.Solo : ClassType.Farm);
            Army.AggroMonCells(manaEnergyNeeded ? "r5" : "r3");
            Army.AggroMonStart("elemental");
            Army.DivideOnCells(manaEnergyNeeded ? "r5" : "r3");

            while (!Bot.ShouldExit && Core.CheckInventory("Mana Energy for Nulgath"))
            {
                Bot.Combat.Attack("*");
                Core.Sleep();
            }
        }

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }



    //                                   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░                    
    //                               ▓▓▓▓████████████████▓▓▓▓▒▒              
    //                           ▓▓▓▓████░░░░░░░░░░░░░░░░██████▓▓            
    //                         ▓▓████░░░░░░░░░░░░░░░░░░░░░░░░░░████          
    //                       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██        
    //                     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██      
    //                   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██      
    //                 ▓▓██░░░░░░▓▓██░░  ░░░░░░░░░░░░░░░░░░░░▓▓██░░  ░░██    
    //               ▓▓██░░░░░░░░██████░░░░░░░░░░░░░░░░░░░░░░██████░░░░░░██  
    //               ▓▓██░░░░░░░░██████▓▓░░░░░░██░░░░██░░░░░░██████▓▓░░░░██  
    //             ▓▓██▒▒░░░░░░░░▓▓████▓▓░░░░░░████████░░░░░░▓▓████▓▓░░░░░░██
    //           ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░██░░░░██░░░░░░░░░░░░░░░░░░░░██
    //           ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //           ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //         ░░▓▓▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //         ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //         ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //         ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██
    //       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░Script Made for Potatos ░░░░░░░░░░░░░░██    
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //   ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //     ░░▓▓▓▓░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██  
    //       ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██░░  
    //         ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██    
    //           ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██      
    //           ▓▓██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██        
    //             ▓▓████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░██          
    //               ▓▓▓▓████████░░░░░░░░░░░░░░░░░░░░░░░░████████░░          
    //               ░░░░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░   

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Core.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

    void SetAggro()
    {
        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForCombatExit();

        string[] playerConfigs = new[]
        {
        Bot.Config?.Get<string>("player1")?.Trim() ?? "",
        Bot.Config?.Get<string>("player2")?.Trim() ?? "",
        Bot.Config?.Get<string>("player3")?.Trim() ?? "",
        Bot.Config?.Get<string>("player4")?.Trim() ?? "",
        Bot.Config?.Get<string>("player5")?.Trim() ?? "",
        Bot.Config?.Get<string>("player6")?.Trim() ?? ""
        };


        int presentPlayersCount = playerConfigs.Count(pc => !string.IsNullOrWhiteSpace(pc));

        switch (presentPlayersCount)
        {
            case 6:
                Army.AggroMonCells("r13", "r14", "r15", "r16", "r17", "r4");
                Army.DivideOnCells("r13", "r14", "r15", "r16", "r17", "r4");
                break;

            case 5:
            case 4:
                Army.AggroMonCells("r13", "r14", "r15", "r16");
                Army.DivideOnCells("r13", "r14", "r15", "r16");
                break;

            case 3:
            case 2:
                Army.AggroMonCells("r13", "r14");
                Army.DivideOnCells("r13", "r14");
                break;

            default:
                Army.AggroMonCells("r13");
                Army.DivideOnCells("r13");
                break;
        }

        Core.Sleep(2500);
    }


}
