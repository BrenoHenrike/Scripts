/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedBloodOrb.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedHexOrb.cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedShadowOrb[Mem].cs
//cs_include Scripts/Nation/EvolvedOrb/EvolvedShadowOrbItems[Mem].cs
//cs_include Scripts/Other/Classes/REP-based/Bard.cs
//cs_include Scripts/Other/MergeShops/BattleConGearMerge.cs
//cs_include Scripts/Other/Various/Potions.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class EvolvedOrb
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public EvolvedBloodOrb EBO = new EvolvedBloodOrb();
    public EvolvedHexOrb EHO = new EvolvedHexOrb();
    public EvolvedShadowOrb ESO = new EvolvedShadowOrb();
    public EvolvedShadowOrbItems ESOItems = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoBoth();

        Core.SetOptions(false);
    }

    public void DoBoth()
    {
        GetAllOrb();
        GetAllItems();
    }

    public void GetAllOrb()
    {
        EBO.GetEvolvedBloodOrb();
        EHO.GetEvolvedHexOrb();
        ESO.GetEvolvedShadowOrb();
        Core.Logger($"Done, you have the balls");
    }

    public void GetAllItems()
    {
        ESOItems.GetItems();
        Core.Logger($"Done, you have hatched the balls");
    }
}
