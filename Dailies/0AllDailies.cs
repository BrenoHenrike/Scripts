//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class FarmAllDailys
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public LordOfOrder LOO = new();
    public GlaceraStory Glac = new();
    public CoreBLOD BLOD = new();

    public void ScriptMain(IScriptInterface bot)
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
        Glac.FrozenTower();
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
        Daily.MineCrafting(new[] { "Aluminum", "Barium", "Gold", "Iron", "Copper", "Silver", "Platinum" }, 10, ToBank: true);
        Daily.HardCoreMetals(ToBank: true);
        Daily.MonthlyTreasureChestKeys();
        Daily.WheelofDoom();
        Daily.FreeDailyBoost();
        // Daily.NSoDDaily();
        Daily.BallyhooAdRewards();
        Daily.PowerGem();
        Daily.GoldenInquisitor();
        Daily.DesignNotes();
        Daily.MoglinPets();

        Core.Logger("All dailies are completed");
    }
}