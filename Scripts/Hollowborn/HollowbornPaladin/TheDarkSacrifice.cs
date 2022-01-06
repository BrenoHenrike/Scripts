//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/LetsGetYouASuit.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/IGotYourBackAndYourTop.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Chaos/DrakathArmorBot.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class TheDarkSacrifice
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreHollowborn HB = new CoreHollowborn();
    public IGotYourBackAndYourTop HBPalHelm = new IGotYourBackAndYourTop();
    public LetsGetYouASuit HBPal = new LetsGetYouASuit();
    public AscendedDrakathGear ADG = new AscendedDrakathGear();
    public CoreNulgath Nulgath = new CoreNulgath();
    public Artixpointe APointe = new Artixpointe();
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Daily = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Bot.Player.LoadBank();

        HBShadowOfFate();

        Core.SetOptions(false);
    }

    public void HBShadowOfFate()
    {
        if (Core.CheckInventory("Hollowborn Shadow of Fate"))
            return;
        HB.HardcoreContract();
        HBPal.HBPaladin();
        HBPalHelm.HBPaladinHelmet();
        ADG.AscendedGear("Ascended Light of Destiny");
        Farm.IcestormArena(95);

        Core.AddDrop("Undead Skull", "Hollowborn Shadow of Fate");
        Core.EnsureAccept(7559);
        Nulgath.TheAssistant("Unidentified 25");
        if (!Core.CheckInventory("Seal of Light") || !Core.CheckInventory("Seal of Darkness"))
            Daily.BrightKnightArmor(false);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("artixpointe", "Skeletal Minion", "Undead Skull", 1, false);
        APointe.OmniArtifact();
        HB.HumanSoul(300);
        Core.EnsureComplete(7559);
        return;
    }
}