/*
name: LightCaster
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Other/Classes/LightMage.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Other/MergeShops/CelestialChampMerge.cs
using Skua.Core.Interfaces;
public class LightCaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public LightMage LM = new();
    public AvatarOfDeathsScythe AODS = new();
    public GuardianOfSpiritsBlade GOSB = new();
    public LanceOfTime LOT = new();
    public BurningBlade BB = new();
    public BurningBladeOfAbezeth BBOA = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetLC();

        Core.SetOptions(false);
    }

    public void GetLC(bool rankUpClass = true)
    {
        if (Core.CheckInventory(38153))
            return;

        Core.AddDrop("LightCaster", "Aranx's Pure Light");

        Farm.Experience(80);
        GOSB.GetGoSB();
        AODS.GetAoDS();
        LOT.GetLoT();
        BB.GetBurningBlade();
        LM.GetLM(false);        
        BBOA.GetBBoA();


        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(6495);
        Core.HuntMonster("celestialarenad", "Aranx", "Aranx's Pure Light", isTemp: false);
        Core.EnsureComplete(6495);
        Bot.Wait.ForPickup("LightCaster");

        if (rankUpClass)
            Adv.RankUpClass("LightCaster");
    }
}
