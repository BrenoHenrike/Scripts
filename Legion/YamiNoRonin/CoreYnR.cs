/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Legion/SwordMaster.cs
using Skua.Core.Interfaces;

public class CoreYnR
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new();
    public CoreSoW SOW = new();
    public SwordMaster SM = new();

    private bool nonLegion = false;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void GetYnR(bool rankUpClass = true)
    {
        // There is an armor called YNR.
        if (Core.CheckInventory(53841))
            return;

        Core.AddDrop("Yami no Ronin");

        Core.EnsureAccept(7408);
        BlademasterSwordScroll();
        YokaiSwordScroll();
        Core.EnsureComplete(7408);
        Bot.Wait.ForPickup("Yami no Ronin");

        if (rankUpClass)
            Adv.rankUpClass("Yami no Ronin");
    }

    public void YokaiSwordScroll()
    {
        if (Core.CheckInventory("Yokai Sword Scroll"))
            return;

        SOW.DarkAlly();
        Core.FarmingLogger("Yokai Sword Scroll", 1);
        Core.AddDrop("Yami no Ronin Katana", "Yokai Sword Scroll");

        Core.EnsureAccept(7445);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("shadowfortress", "r12", "Top", "*", "Perfect Orochi Scales", 888, false);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 600, false);

        Adv.BuyItem("akiba", 131, "Oni Skull Charm");
        if (!Core.CheckInventory("Shadow Blade Katana"))
        {
            Yami(10);
            FoldedSteel();
            Adv.BuyItem("underworld", 238, "Platinum Paragon Medal", 15);
            Adv.BuyItem("underworld", 577, "Shadow Katana Blade", 1);
        }

        Core.EnsureComplete(7445);
    }

    public void Yami(int quant = 10)
    {
        if (Core.CheckInventory("Yami", quant))
            return;

        Core.FarmingLogger("Yami", quant);
        Core.AddDrop("Yami");
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(7409);
        while (!Bot.ShouldExit && !Core.CheckInventory("Yami", quant))
        {
            Core.KillMonster("darkally", "r2", "Left", "*", "Dark Wisp", 444, false);
            Bot.Wait.ForPickup("Yami");
        }
        Core.CancelRegisteredQuests();
    }

    public void FoldedSteel()
    {
        if (Core.CheckInventory("Folded Steel"))
            return;

        Core.Logger("Checking which method to use");
        if (!Core.isCompletedBefore(793))
            nonLegion = true;
        else nonLegion = false;

        Core.FarmingLogger("Folded Steel", 1);
        Core.AddDrop("Folded Steel");

        Core.EnsureAccept(7444);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("fotia", "Amia the Cult Leader", "Eternity Flame", 1, false);
        Core.HuntMonster("shadowfortress", "Jaaku", "Shadow Katana Blueprint", 1, false);
        if (!Core.CheckInventory("Obsidian Rock", 108))
        {
            if (nonLegion)
                Legion.ObsidianRock(108);
            else
            {
                Legion.SoulForgeHammer();
                Legion.FarmLegionToken(220);
                Core.BuyItem("underworld", 577, "Obsidian Rock", 108);
            }
        }
        FlameForgedMetal(13);
        Core.EquipClass(ClassType.Solo);
        Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", 15, false);

        Core.EnsureComplete(7444);
    }

    public void FlameForgedMetal(int quant = 13)
    {
        if (Core.CheckInventory("Flame-Forged Metal", quant))
            return;

        Core.FarmingLogger("Flame-Forged Metal", quant);
        Core.AddDrop("Flame-Forged Metal");
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(6975);
        while (!Bot.ShouldExit && !Core.CheckInventory("Flame-Forged Metal", quant))
        {
            Core.HuntMonster("Underworld", "Frozen Pyromancer", "Stolen Flame", 1, true);
            Bot.Wait.ForPickup("Flame-Forged Metal");
        }
        Core.CancelRegisteredQuests();
    }

    public void BlademasterSwordScroll(bool nonLegion = false)
    {
        if (Core.CheckInventory("Blademaster Sword Scroll"))
            return;

        Core.Logger("Checking which method to use");
        if (!Core.isCompletedBefore(793))
            nonLegion = true;
        else nonLegion = false;

        SOW.DarkAlly();
        Core.FarmingLogger("Blademaster Sword Scroll", 1);
        Core.AddDrop("Blademaster Sword Scroll");

        Core.EquipClass(ClassType.Solo);
        if (nonLegion)
        {
            Core.EnsureAccept(7410);
            Core.Logger("Using Non-Legion variant for the Blademaster Sword Scroll");
            Core.KillMonster("frozenlair", "r3", "Left", "Legion Lich Lord", "Sapphire Orb", 26, false, publicRoom: true);
            Core.KillMonster("Judgement", "r10a", "Spawn", "Ultra Aeacus", "Aeacus Empowered", 100, false, publicRoom: true);
            Core.HuntMonster("darkally", "Underfiend", "Traitor's Tract", 250, false);
            Core.HuntMonster("shadowsong", "Oh'Garr", "Ogre Titan's Resonance", 250, false);
            Core.HuntMonster("shadowgrove", "Titan Shadow Dragonlord", "Shadow Dragonlord's Shroud", 250, false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("evilwardage", "Blade Master", "Discipline", isTemp: false);
            Legion.DagePvP(400, 50, 1000);
            Core.EnsureComplete(7410);
        }

        else
        {
            Core.Logger("Using Legion variant for the Blademaster Sword Scroll");
            Core.EnsureAccept(7443);
            Core.KillMonster("frozenlair", "r3", "Left", "Legion Lich Lord", "Sapphire Orb", 13, false, publicRoom: true);
            Legion.FarmLegionToken(17500);
            Core.KillMonster("Judgement", "r10a", "Spawn", "Ultra Aeacus", "Aeacus Empowered", 50, false, publicRoom: true);

            if (!Core.CheckInventory("Meditation"))
            {
                Core.AddDrop("Meditation");

                int m_questID = -1;
                if (Core.CheckInventory(22933))
                {
                    // BM LT
                    Core.Logger("Using LT BladeMaster for Meditation");
                    m_questID = 7412;
                    Adv.rankUpClass("BladeMaster");
                }
                else if (Core.CheckInventory(22859))
                {
                    // BM AC
                    Core.Logger("Using AC BladeMaster for Meditation");
                    m_questID = 7411;
                    Adv.rankUpClass("BladeMaster");
                }
                else if (Core.CheckInventory(53836))
                {
                    // SM AC
                    Core.Logger("Using AC SwordMaster for Meditation");
                    m_questID = 7413;
                    Adv.rankUpClass("SwordMaster");
                }
                else
                {
                    Core.Logger("Using LT SwordMaster for Meditation");
                    if (!Core.CheckInventory("SwordMaster"))
                    {
                        Core.Logger("Farming LT SwordMaster for Meditation");
                        SM.GetSwordMaster();
                    }
                    m_questID = 7414;
                    Adv.rankUpClass("SwordMaster");
                }
                Core.EquipClass(ClassType.Solo);

                Core.EnsureAccept(m_questID);
                Legion.BoneSigil(1);
                Core.EnsureComplete(m_questID);
                Bot.Wait.ForPickup("Meditation");
            }
            Core.EnsureComplete(7443);
        }
    }
}
