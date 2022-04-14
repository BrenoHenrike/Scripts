//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
using RBot;

public class TheEdgeofanEra
{

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreLegion Legion = new CoreLegion();
    public DarkAlly_Story DarkAlly = new DarkAlly_Story();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        YokaiSwordScroll();

        Core.SetOptions(false);
    }

    public void YokaiSwordScroll(int quant = 1)
    {
        if (Core.CheckInventory("Yokai Sword Scroll", quant))
            return;

        Adv.GearStore();
        DarkAlly.DarkAlly_Questline();
        Core.AddDrop("Yami no Ronin Katana", "Yokai Sword Scroll");
        Core.EnsureAccept(7445);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("shadowfortress", "1st Head of Orochi", "Perfect Orochi Scales", 888, false);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 600, false);
        Farm.YokaiREP();
        Adv.GearStore(true);
        Core.BuyItem("akiba", 131, "Oni Skull Charm", 1);
        ShadowKatanaBlade(1);
        Core.EnsureComplete(7445);
    }

    public void Yami(int quant = 10)
    {
        if (Core.CheckInventory("Yami", quant))
            return;
        Core.AddDrop("Yami");
        while (!Core.CheckInventory("Yami", quant))
        {
            Core.EnsureAccept(7409);
            Core.KillMonster("darkally", "r2", "Left", "*", "Dark Wisp", 444, false);
            Core.EnsureComplete(7409);
        }
    }

    public void FoldedSteel(int quant = 1)
    {
        if (Core.CheckInventory("Folded Steel", quant))
            return;
        Core.AddDrop("Folded Steel");
        Core.EnsureAccept(7444);
        Core.HuntMonster("fotia", "Amia the Cult Leader", "Eternity Flame", 1, false);
        Core.HuntMonster("shadowfortress", "Jaaku", "Shadow Katana Blueprint", 1, false);
        Legion.SoulForgeHammer();
        if (!Core.CheckInventory("Obsidian Rock", 108))
        {
            Legion.FarmLegionToken(220);
            Core.BuyItem("underworld", 577, "Obsidian Rock", 108);
        }
        FlameForgedMetal(13);
        if (!Core.CheckInventory("Weapon Imprint", 15))
        {
            Bot.Quests.UpdateQuest(3008);
            Core.SendPackets("%xt%zm%setAchievement%108927%ia0%18%1%");
            Bot.Quests.UpdateQuest(3004);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", 15, false);
        }

        Core.EnsureComplete(7444);
    }

    public void FlameForgedMetal(int quant = 13)
    {
        if (Core.CheckInventory("Flame-Forged Metal", quant))
            return;
        Core.AddDrop("Flame-Forged Metal");
        while (!Core.CheckInventory("Flame-Forged Metal", quant))
        {
            Core.EnsureAccept(6975);
            Core.HuntMonster("Underworld", "Frozen Pyromancer", "Stolen Flame", 1, true);
            Core.EnsureComplete(6975);
        }
    }

    public void ShadowKatanaBlade(int quant = 1)
    {
        if (Core.CheckInventory("Shadow Katana Blade"))
            return;
        Yami(10);
        FoldedSteel(1);
        if (!Core.CheckInventory("Platinum Paragon Medal", 15))
        {
            Farm.Gold(15000000);
            Core.BuyItem("underworld", 238, "Platinum Paragon Medal", 15);
        }
        Farm.YokaiREP(10);
        Core.BuyItem("underworld", 577, "Shadow Katana Blade", 1);
    }

}
