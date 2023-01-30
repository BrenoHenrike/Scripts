/*
name: Dragon of Time Army
description: sues an army to get the dragon of time class
tags: dragon of time, class, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class DoTArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreDarkon Darkon = new();
    public GoldenBladeOfFate GBoF = new();
    public PinkBladeOfDestruciton PBoD = new();
    public CoreQOM QOM = new();
    public CoreToD TOD = new();
    public MysteriousEgg Egg = new();
    public CoreSummer Coll = new();
    public Borgars Borgars = new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();


    public bool DontPreconfigure = true;
    // public string OptionsStorage = "DoTArmy";
    // public List<IOption> Options = new List<IOption>()
    // {
    //     new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1), //so that it waits
    //     CoreBots.Instance.SkipOptions
    // };

    public List<IOption> Options = new()
    {
        new Option<bool>("sellToSync", "Sell to Sync", "Sell items to make sure the army stays syncronized.\nIf off, there is a higher chance your army might desyncornize", false),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };


    private string[] Extras = { "Dragon of Time Horns", "Dragon of Time Horns + Ponytail", "Dragon of Time Wings + Tail" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Dragon of Time");
        Core.SetOptions(disableClassSwap: true);

        GetDoTArmy();

        Core.SetOptions(false);
    }

    public void GetDoTArmy(bool rankUpClass = true, bool doExtra = true)
    {
        if ((!doExtra && Core.CheckInventory("Dragon of Time")) || (doExtra && Core.CheckInventory(Extras)))
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Bot.Events.PlayerAFK += PlayerAFK;

        DoQuest1();
        DoQuest2();
        DoQuest3();
        DoQuest4();
        DoQuest5();
        DoQuest6();
        DoQuest7();
        DoQuest8();
        DoQuest9();
        if (doExtra)
            DoQuest10();
    }

    public void DoQuest1()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7716).Rewards.Select(x => x.Name).ToArray());

        Story.PreLoad(this);

        // Acquiring Ancient Secrets 7716
        Core.EnsureAccept(7716);

        Quest QuestData = Core.EnsureLoad(7716);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Farm.LoremasterREP(4);

        if (Bot.Config.Get<bool>("sellToSync"))
        {
            Core.SellItem("Lost Hieroglyphic", all: true);
            Core.SellItem("Frost King's Story", all: true);
        }

        Bot.Quests.UpdateQuest(4614);
        Core.EquipClass(ClassType.Farm);
        ArmyHunt("mummies", new[] { "Mummy" }, "Lost Hieroglyphic", ClassType.Solo, false, 30);
        ArmyHunt("timelibrary", new[] { "Training Globe", "Tog", "Moglin Ghost" }, "Historia Page", ClassType.Solo, false, 100);
        Core.EquipClass(ClassType.Solo);
        Core.EquipClass(ClassType.Solo);
        ArmyHunt("kingcoal", new[] { "Frost King" }, "Frost King's Story", ClassType.Solo);
        Core.KillMonster("baconcatyou", "Enter", "Spawn", "*", "Your Own Memories", isTemp: false);
        Core.BuyItem("librarium", 651, "Myths of Lore");
        Core.EnsureComplete(7716);
        Core.Logger($"Quest 1: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
    }

    public void DoQuest2()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7717).Rewards.Select(x => x.Name).ToArray());

        Core.EnsureAccept(7717);

        Quest QuestData = Core.EnsureLoad(7717);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Core.EquipClass(ClassType.Solo);
        ArmyHunt("dragonchallenge", new[] { "Desoloth the Final" }, "Desoloth's Destructive Aura", ClassType.Solo);
        Bot.Quests.UpdateQuest(899);
        ArmyHunt("blindingsnow", new[] { "Nythera" }, "Nythera's Patience", ClassType.Solo);
        Core.AddDrop("Key of Greed");
        ArmyHunt("greed", new[] { "Goregold" }, "Goregold's Luck", ClassType.Solo);
        ArmyHunt("darkplane", new[] { "Victorious" }, "Victorious's Dignity", ClassType.Solo);
        ArmyHunt("trigoras", new[] { "Trigoras" }, "Trigoras's Tenacity", ClassType.Solo, false, 3);
        Core.EnsureComplete(7717);
        Core.Logger($"Quest 2: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
    }

    public void DoQuest3()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7718).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(31);
        Core.EnsureAccept(7718);

        Quest QuestData = Core.EnsureLoad(7718);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        GBoF.GetGBoF();
        PBoD.GetPBoD();

        Core.EquipClass(ClassType.Solo);
        ArmyHunt("underworld", new[] { "Laken" }, "Cross-Era Stabilizer", ClassType.Solo);
        if (!Core.CheckInventory("Chronomancer's Codex"))
        {
            ArmyHunt("mqlesson", new[] { "Dragonoid" }, "Dragonoid of Hours", ClassType.Solo);
            ArmyHunt("timespace", new[] { "Chaos Lord Iadoa" }, "Chronomancer's Codex", ClassType.Solo);
        }

        Core.EquipClass(ClassType.Farm);
        ArmyHunt("arena", new[] { "Timestream Rider" }, "Timestream String", ClassType.Solo, false, 100);
        Core.EnsureComplete(7718);
        Core.Logger($"Quest 3: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank("Dragon of Time FangBlade", "Dual Dragon of Time FangBlades");
    }

    public void DoQuest4()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7719).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(40);
        Core.EnsureAccept(7719);

        Quest QuestData = Core.EnsureLoad(7719);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Core.EquipClass(ClassType.Solo);
        ArmyHunt("cathedral", new[] { "Incarnation of Time" }, "Time Loop Broken", ClassType.Solo);
        ArmyHunt("ubear", new[] { "Cornholio" }, "Is This a Wormhole?", ClassType.Solo);
        Core.EquipClass(ClassType.Farm);
        ArmyHunt("portalwar", new[] { "Chronorysa", "Tempus Larva", "Time Wraith" }, "Anomaly Silenced", ClassType.Solo, false, 100);
        ArmyHunt("portalmaze", new[] { "ChronoLord" }, "Chronolord Stopped", ClassType.Solo, false, 50);
        Core.EnsureComplete(7719);
        Core.Logger($"Quest 4: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
    }

    public void DoQuest5()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7720).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(60);
        Core.EnsureAccept(7720);

        Quest QuestData = Core.EnsureLoad(7720);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Core.EquipClass(ClassType.Solo);
        ArmyHunt("lairdefend", new[] { "Dragon Summoner" }, "Dimensional Dragon Portal", ClassType.Solo, false, 2);
        ArmyHunt("bosschallenge", new[] { "Grievous Inbunche" }, "Brutal Slash Studied", ClassType.Solo, false, 10);
        ArmyHunt("hydrachallenge", new[] { "Hydra Head 90" }, "Epic Hydra Fang", ClassType.Solo, false, 123);
        Core.EnsureComplete(7720);
        Core.Logger($"Quest 5: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
    }

    public void DoQuest6()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7721).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(60);
        Core.EnsureAccept(7721);

        Quest QuestData = Core.EnsureLoad(7721);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Core.EquipClass(ClassType.Solo);
        ArmyHunt("ivoliss", new[] { "Ivoliss" }, "Sword of Voids", ClassType.Solo);
        Bot.Wait.ForPickup("Sword of Voids");

        Darkon.FarmReceipt(100);

        QOM.TheReshaper();
        if (!Core.CheckInventory("Semiramis Feather"))
        {
            Core.AddDrop("Semiramis Feather");
            // Take Down Terrane 6286
            Core.EnsureAccept(6286);
            Core.EquipClass(ClassType.Solo);
            ArmyHunt("guardiantree", new[] { "Terrane" }, "Terrane Defeated", ClassType.Solo);
            Core.EnsureComplete(6286);
            Bot.Wait.ForPickup("Semiramis Feather");
        }

        Core.EquipClass(ClassType.Farm);
        ArmyHunt("aqw3d", new[] { "Nightlocke Axe", "Nightlocke Blade", "Nightlocke Staff" }, "Cross-Dimensional Weapons", ClassType.Solo, false, 300);
        TOD.ShiftingPyramid();
        if (!Core.CheckInventory("Starlight Singularity"))
        {
            Core.AddDrop("Starlight Singularity");
            // Serpent of the Stars 5186
            Core.EnsureAccept(5186);
            Core.EquipClass(ClassType.Solo);
            ArmyHunt("whitehole", new[] { "Mehensi Serpent" }, "Mehen Slain", ClassType.Solo);
            Core.EnsureComplete(5186);
            Bot.Wait.ForPickup("Starlight Singularity");
        }

        Coll.Collector();
        Core.BuyItem("collection", 325, "Collectible Collector");
        Bot.Wait.ForPickup("Collectible Collector");

        Core.EnsureComplete(7721);

        Core.Logger($"Quest 6: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());

    }

    public void DoQuest7()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7722).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(65);
        Core.EnsureAccept(7722);

        Quest QuestData = Core.EnsureLoad(7722);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Core.EquipClass(ClassType.Farm);
        ArmyHunt("moonlab", new[] { "Slime Mold" }, "Unyielding Slime", ClassType.Farm, false, 300);
        Core.EquipClass(ClassType.Solo);
        ArmyHunt("bosschallenge", new[] { "Mutated Void Dragon" }, "Omnipotent rs", ClassType.Solo, false, 20);
        ArmyHunt("underlair", new[] { "ArchFiend Dragonlord" }, "Dragon's Plasma", ClassType.Solo, false, 20);
        ArmyHunt("chaoskraken", new[] { "Chaos Kraken" }, "Chaotic Invertebrae", ClassType.Solo, false, 20);
        Bot.Quests.UpdateQuest(9, 159);
        ArmyHunt("towerofdoom9", new[] { "Dread Fang" }, "Cryostatic Essence", ClassType.Farm, false, 20);
        ArmyHunt("castleroof", new[] { "Ultra Chaos Dragon" }, "Salvaged Chaos Dragon Biomass", ClassType.Solo, false, 20);
        Core.EnsureComplete(7722);
        Core.Logger($"Quest 7: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());

    }

    public void DoQuest8()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7723).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(70);
        Core.EnsureAccept(7723);

        Quest QuestData = Core.EnsureLoad(7723);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());


        Core.EquipClass(ClassType.Farm);
        ArmyHunt("volcano", new[] { "Fire Imp" }, "Fire Essence", ClassType.Farm, false, 3000);
        Core.EquipClass(ClassType.Solo);
        ArmyHunt("charredplains", new[] { "Akriloth" }, "Akriloth's Flametongue", ClassType.Solo, false, 100);
        ArmyHunt("ultraphedra", new[] { "Ultra Phedra" }, "Immortal Embers", ClassType.Solo, false, 50);
        ArmyHunt("thevoid", new[] { "Reaper" }, "Ashes from the Void Realm", ClassType.Solo, false, 50);
        Core.EnsureComplete(7723);
        Core.Logger($"Quest 8: 🖕");
        Bot.Wait.ForPickup("*");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());

    }

    public void DoQuest9()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7724).Rewards.Select(x => x.Name).ToArray());

        Farm.Experience(75);
        if (!Core.CheckInventory("Blade of Awe"))
            Farm.BladeofAweREP(6, true);
        Core.EnsureAccept(7724);

        Quest QuestData = Core.EnsureLoad(7724);
        Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

        Egg.GetMysteriousEgg();

        Core.EquipClass(ClassType.Solo);
        Bot.Quests.UpdateQuest(3880);
        Core.KillMonster("chaoslord", "r2", "Left", "*", "Conquered Past", isTemp: false);
        Bot.Quests.UpdateQuest(10, 159);
        ArmyHunt("towerofdoom10", new[] { "Slugbutter" }, "Slugbutter Trophy", ClassType.Solo, false, 100);
        ArmyHunt("icestormarena", new[] { "Warlord Icewing" }, "Icewing's Laurel", ClassType.Solo, false, 30);

        Core.EnsureComplete(7724);
        Core.Logger($"Quest 9: 🖕");
        Bot.Wait.ForPickup("Dragon of Time");
        Adv.rankUpClass("Dragon of Time");
        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
    }

    public void DoQuest10()
    {
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();
        Bot.Drops.Add(Core.EnsureLoad(7725).Rewards.Select(x => x.Name).ToArray());

        int i = 0;

        while (!Bot.ShouldExit && !Core.CheckInventory(Extras, toInv: false))
        {

            Farm.Experience(75);
            Core.EnsureAccept(7725);

            Quest QuestData = Core.EnsureLoad(7725);
            Core.TrashCan(QuestData.Requirements.Where(x => !x.Temp).Select(y => y.Name).ToArray());

            Borgars.StoryLine();
            Core.AddDrop("Burger Buns");
            Core.EquipClass(ClassType.Solo);

            if (!Core.CheckInventory("Borgar"))
            {
                while (!Bot.ShouldExit && !Core.CheckInventory("Burger Buns", 5))
                {
                    // Burglinster's Revenge 7522
                    Core.EnsureAccept(7522);
                    Core.EquipClass(ClassType.Solo);
                    ArmyHunt("borgars", new[] { "Burglinster" }, "Burglinster Cured", ClassType.Solo);
                    Core.EnsureComplete(7522);
                    Bot.Wait.ForPickup("Burger Buns");
                }
            }
            Core.BuyItem("borgars", 1884, 54650, shopItemID: 7387);

            Core.EnsureCompleteChoose(7725, Extras);
            Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
            Core.Logger($"Quest 10 completed {i++} times: 🖕");
            Bot.Wait.ForPickup("*");
        }
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



    // /// <summary>
    // /// Joins a map, jump & set the spawn point and kills the specified monster - with an army check that waits for the input number of players
    // /// </summary>
    // /// <param name="map">Map to join</param>
    // /// <param name="r">r to jump to</param>
    // /// <param name="Left">Left to jump to</param>
    // /// <param name="monster">Name of the monster to kill</param>
    // /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    // /// <param name="quant">Desired quantity of the item</param>
    // /// <param name="isTemp">Whether the item is temporary</param>
    // /// <param name="log">Whether it will log that it is killing the monster</param>
    // public void ArmyKillMonster(string map, string r, string Left, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    // {
    //     // Core.PrivateRooms = true;
    //     // Core.PrivateRoomNumber = Army.getRoomNr();

    //     if (item != null && isTemp ? Bot.TempInv.Contains(item, quant) : Core.CheckInventory(item, quant))
    //         return;
    //     if (!isTemp && item != null)
    //         Core.AddDrop(item);
    //     Core.Join(map, r, Left);
    //     Core.Jump(r, Left);
    //     while ((r != null && Bot.Map.PlayerNames.Count() > 0 ? Bot.Map.PlayerNames.Count() : Bot.Map.PlayerCount) < Bot.Config.Get<int>("armysize"))
    //     {
    //         Core.Logger($"[{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}] Waiting For The Squad!");
    //         Bot.Sleep(5000);
    //     }
    //     if (item == null)
    //     {
    //         if (log)
    //             Core.Logger($"Killing {monster}");
    //         Core.HuntMonster(map, monster);
    //         Core.Rest();
    //     }
    //     else
    //     {
    //         if (Bot.Map.Name == "trigoras")
    //             Core.HuntMonster("trigoras", "Trigoras", "Trigoras's Tenacity", 3, false);
    //         else Core.HuntMonster(map, monster, item, quant, isTemp, log: log);
    //     }

    // }


    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.EquipClass(classType);
        Core.AddDrop(item);

        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }


    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }
}
