//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
//cs_include Scripts/Legion/SwordMaster.cs
using RBot;

public class ThePathtoPower
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreLegion Legion = new CoreLegion();
    public CoreFarms Farm = new CoreFarms();
    public DarkAlly_Story DarkAlly = new DarkAlly_Story();
    public SwordMaster SM = new SwordMaster();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BlademasterSwordScroll();

        Core.SetOptions(false);
    }

    public void BlademasterSwordScroll(int quant = 1)
    {
        if (Core.CheckInventory("Blademaster Sword Scroll", quant))
            return;
        DarkAlly.DarkAlly_Questline();
        Core.AddDrop("Blademaster Sword Scroll");
        Core.EnsureAccept(7410);
        Core.KillMonster("frozenlair", "r3", "Left", "Legion Lich Lord", "Sapphire Orb", 13, false, publicRoom: true);
        Core.KillMonster("Judgement", "r10a", "Spawn", "Ultra Aeacus", "Aeacus Empowered", 50, false, publicRoom: true);
        Core.HuntMonster("darkally", "Underfiend", "Traitor's Tract", 250, false);
        Core.HuntMonster("shadowsong", "Oh'Garr", "Ogre Titan's Resonance", 250, false);
        Core.HuntMonster("shadowgrove", "Titan Shadow Dragonlord", "Shadow Dragonlord's Shroud", 250, false);
        Core.HuntMonster("evilwardage", "Blade Master", "Discipline", isTemp: false);
        Legion.DagePvP(400, 50, 1000);
        Core.EnsureComplete(7410);
    }

    public void Meditation(int quant = 1)
    {
        if (Core.CheckInventory("Meditation", quant))
            return;
        Core.AddDrop("Meditation");
        SwordMaster();
        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(7414);
        Legion.BoneSigil(1);
        Core.EnsureComplete(7414);
    }

    public void SwordMaster()
    {
        SM.GetSwordMaster();
        Adv.rankUpClass("SwordMaster");
    }

}