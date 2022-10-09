//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/CoreHollowbornPaladin.cs
//cs_include Scripts/Hollowborn/HollowbornDoomKnight/CoreHollowbornDoomKnight.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CoreHollowbornDoomKnight
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreHollowborn HB = new();
    public CoreHollowbornPaladin HBP = new();
    public CoreSDKA SDKA = new();
    public CoreNSOD NSoD = new();
    public SepulchuresOriginalHelm SOH = new();

    public string OptionsStorage = "HollowbornDoomKnightOptions";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("PreFarm", "Pre Farm Dark-/Doom Fragments", "Recommended setting: False", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public string[] ADKItems = {
        "Hollowborn Doom Visage",
        "Hollowborn DoomKnight Helm",
        "Hollowborn DoomKnight Hood",
        "Hollowborn Doom Cloak",
        "Hollowborn Doom Cape"
    };

    public string[] ADKRisesItems = {
        "Doom Fragment",
        "Classic Hollowborn DoomKnight"
    };
    public string[] ADKFallsItems = {
        "Hollowborn Empress' Blade",
        "Hollowborn DoomBlade"
    };

    public string[] ADKReturnsItems = {
        "Hollowborn DoomKnight",
        "Hollowborn Sepulchure's Helm",
        "Hollowborn Doom Shade",
        "Hollowborn Sword of Doom"
    };

    public void GetAll()
    {
        if (Core.CheckInventory(ADKItems, toInv: false) && Core.CheckInventory("Classic Hollowborn DoomKnight", toInv: false) &&
            Core.CheckInventory(ADKFallsItems, toInv: false) && Core.CheckInventory(ADKReturnsItems, toInv: false))
            return;

        if (Bot.Config.Get<bool>("PreFarm"))
        {
            ADK();
            ADKRises();
        }
        ADKFalls();
        ADKReturns();
    }

    public void ADK(int quant = 125)
    {
        if (Core.CheckInventory("Dark Fragment", quant))
            return;

        Core.AddDrop(ADKItems);
        Core.AddDrop("Dark Fragment");

        // Requirements
        HB.HardcoreContract();
        Farm.EvilREP();

        // Quest
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Fragment", quant))
        {
            Core.EnsureAccept(8413);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Empowered Essence", 10, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Shadowworn", 1, false);
            Farm.Gold(100000);
            Adv.BuyItem("shadowfall", 89, "Shadowscythe Venom Head");
            Core.HuntMonster("shadowrealm", "Hollowborn Sentinel", "Hollow Soul", 10, false);
            Core.Logger("Bought Shadowscythe Venom Head");
            if (Core.CheckInventory(ADKItems))
                Core.EnsureComplete(8413);
            else
                Core.EnsureCompleteChoose(8413, ADKItems);
        }
        Core.ToBank(ADKItems);
    }

    public void ADKRises(int quant = 15)
    {
        if (Core.CheckInventory("Doom Fragment", quant))
            return;

        Core.AddDrop(ADKRisesItems);

        // Requirements
        HB.HardcoreContract();

        // Quest
        while (!Bot.ShouldExit && !Core.CheckInventory("Doom Fragment", quant))
        {
            Core.EnsureAccept(8414);

            ADK(5);
            Adv.BuyItem("tercessuinotlim", 1951, "Doomatter", 10, 10);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Shadow DoomReaver", 1, false);
            Core.HuntMonster("lumafortress", "Corrupted Luma", "Worshipper of Doom", 1, false);
            Bot.Quests.UpdateQuest(3008);
            if (Core.IsMember)
                Core.HuntMonster("ultravoid", "Ultra Kathool", "Ingredients?", 10, false, log: false);
            else Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Ingredients?", 10, false, log: false);

            Core.EnsureComplete(8414);
        }
    }

    public void ADKFalls()
    {
        if (Core.CheckInventory(ADKFallsItems))
            return;

        Core.AddDrop(ADKFallsItems);

        // Requirements
        HB.HardcoreContract();
        Core.EnsureAccept(8415);

        // Quest
        ADKRises(5);
        ADK(20);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000100);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        if (!Core.CheckInventory("Royal ShadowScythe Blade"))
        {
            Farm.Gold(1000000);
            Farm.EvilREP(10);
            Core.BuyItem("shadowfall", 1639, "Royal ShadowScythe Blade");
        }

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", 1, false, publicRoom: true);
        Bot.Quests.UpdateQuest(3008);
        Core.SetAchievement(18);
        Bot.Quests.UpdateQuest(3004);
        Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", 1, false);

        Core.EnsureComplete(8415);
    }

    public void ADKReturns()
    {
        if (Core.CheckInventory(ADKReturnsItems))
            return;

        Core.AddDrop(ADKReturnsItems);

        Core.EnsureAccept(8416);

        // Requirements 
        SDKA.DoAll();
        SOH.DoAll();
        NSoD.GetNSOD();
        HB.HardcoreContract();
        HBP.HBShadowOfFate();
        Farm.Experience();

        // Quest
        ADKRises(10);
        ADK(30);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("dwarfhold", "r2", "Left", "Chaos Drow", "Dark Energy", 10000, false);
        Core.EquipClass(ClassType.Solo);
        Adv.BoostHuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", 3, false, publicRoom: true);
        NSoD.BonesVoidRealm(1);
        Adv.BoostHuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", 1, false);
        Bot.Quests.UpdateQuest(3008);
        Core.SetAchievement(18);
        Bot.Quests.UpdateQuest(3004);
        Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", 12, false);
        Adv.BoostHuntMonster("Desolich", "Desolich", "Desolich's Dark Horn", 3, false, publicRoom: true);

        Core.EnsureComplete(8416);
    }
}