/*
name:  All Dailies
description:  Does all the avaiable dailies.
tags: all dailies, dailies, daily, all
*/
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

public class FarmAllDailies
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

        DoAllDailies();

        Core.SetOptions(false);
    }

    public void DoAllDailies()
    {
        Core.Logger("Doing all dailies");

        LOO.GetLoO();
        BLOD.UnlockMineCrafting();

        //With solo class
        Daily.MadWeaponSmith();
        Daily.CyserosSuperHammer();
        Daily.BrightKnightArmor();
        Glac.FrozenTower();
        Daily.Pyromancer();
        Daily.DeathKnightLord();
        Daily.ShadowScytheClass();
        Daily.GrumbleGrumble();
        Daily.MonthlyTreasureChestKeys();
        Daily.WheelofDoom();
        Daily.FreeDailyBoost();
        Daily.BallyhooAdRewards();
        Daily.PowerGem();
        Daily.DesignNotes();
        Daily.MoglinPets();
        // Daily.NSoDDaily();

        //With farm class
        Daily.CollectorClass();
        Daily.Cryomancer();
        Daily.EldersBlood();
        Daily.SparrowsBlood();
        Daily.ShadowShroud();
        Daily.DagesScrollFragment();
        Daily.BeastMasterChallenge();
        Daily.FungiforaFunGuy();
        Daily.MineCrafting(new[] { "Aluminum", "Barium", "Gold", "Iron", "Copper", "Silver", "Platinum" }, 10, ToBank: true);
        Daily.HardCoreMetals(new[] { "Arsenic", "Beryllium", "Chromium", "Palladium", "Rhodium", "Rhodium", "Thorium", "Mercury" }, 10, ToBank: true);
        Daily.CryptoToken();
        Daily.GoldenInquisitor();

        Core.Logger("All dailies are completed");
    }
}
