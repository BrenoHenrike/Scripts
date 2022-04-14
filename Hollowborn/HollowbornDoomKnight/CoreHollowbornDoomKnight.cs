using RBot;

public class CoreHollowbornDoomKnight
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreHollowborn HB = new CoreHollowborn();
    public CoreHollowbornPaladin HBP = new CoreHollowbornPaladin();
    public CoreSDKA SDKA = new CoreSDKA();
    public NecroticSwordOfDoom NSOD = new NecroticSwordOfDoom();

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

    public void GetAll(bool prefarm)
    {
        if (Core.CBO_Active)
            prefarm = Core.CBOBool("HBDK_PreFarm");

        if (prefarm)
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
        while (!Core.CheckInventory("Dark Fragment", quant))
        {
            Core.EnsureAccept(8413);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("shadowrealmpast", "Shadow Guardian|Shadow Warrior", "Empowered Essence", 10, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Shadowworn", 1, false);
            Farm.Gold(100000);
            Core.BuyItem("shadowfall", 89, "Shadowscythe Venom Head", shopItemID: 23832);
            Core.HuntMonster("shadowrealm", "Hollowborn Sentinel", "Hollow Soul", 10, false);
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
        while (!Core.CheckInventory("Doom Fragment", quant))
        {
            Core.EnsureAccept(8414);

            ADK(5);
            if (!Core.CheckInventory("Doomatter", 10))
            {
                Farm.Gold(300000);
                Core.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle");
                Core.BuyItem("tercessuinotlim", 1951, "Doomatter", 10, 10);
            }
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Shadow DoomReaver", 1, false);
            Core.HuntMonster("lumafortress", "Corrupted Luma", "Worshipper of Doom", 1, false);
            Bot.Quests.UpdateQuest(2954);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Ingredients?", 10, false);

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
        NSOD.GetNSOD();
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
        NSOD.BonesVoidRealm(1);
        Adv.BoostHuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", 1, false);
        Bot.Quests.UpdateQuest(3008);
        Core.SetAchievement(18);
        Bot.Quests.UpdateQuest(3004);
        Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", 12, false);
        Adv.BoostHuntMonster("Desolich", "Desolich", "Desolich's Dark Horn", 3, false, publicRoom: true);

        Core.EnsureComplete(8416);
    }
}