//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
//cs_include Scripts/Story/CitadelRuins.cs
//cs_include Scripts/Story/LivingDungeon.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using RBot;

public class FarmAllDailys
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public LordOfOrder LOO = new();
    public GlaceraStory Glac = new();
    public CoreBLOD BLOD = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAllDailys();

        Core.SetOptions(false);
    }

    public void DoAllDailys()
    {
        Core.Logger("Doing all dailies");

        LOO.GetLoO();

        Daily.MadWeaponSmith();
        Daily.CyserosSuperHammer();
        Daily.BrightKnightArmor();
        Daily.CollectorClass();
        Glac.DoAll();
        Daily.Cryomancer();
        Daily.Pyromancer();
        Daily.DeathKnightLord();
        Daily.ShadowScytheClass();
        Daily.GrumbleGrumble();
        Daily.EldersBlood();
        Daily.SparrowsBlood();
        Daily.ShadowShroud();
        Daily.DagesScrollFragment();
        Daily.CryptoToken();
        Daily.BeastMasterChallenge();
        Daily.FungiforaFunGuy();
        BLOD.UnlockMineCrafting();
        Daily.MineCrafting();
        Daily.HardCoreMetals();
        Daily.MonthlyTreasureChestKeys();
        Daily.WheelofDoom();
        Daily.FreeDailyBoost();

        Core.Logger("Dailies completed");
    }
}