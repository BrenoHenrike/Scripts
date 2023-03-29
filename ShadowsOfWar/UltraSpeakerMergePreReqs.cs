/*
name: Ultra Speaker Merge PreReqs
description: Gets the prerequisites for the Ultra Speaker merge.
tags: ultra speaker merge, ultra malgor merge
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/ShadowsOfWar/CoreSOfWar.cs
using Skua.Core.Interfaces;

public class UltraSpeakerMergePreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSoW SoW = new();
    public CoreSOfWar SOfWar = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetPrereqs();

        Core.SetOptions(false);
    }

    public void GetPrereqs()
    {
        SoW.CompleteCoreSoW();

        SOfWar.DragonsTear();
        SOfWar.Acquiescence(60);
        SOfWar.ElementalCore(81);
        //50 Brilliant Aura
        //1 Blinding Aura
        //250 Enchanted Scale
        //30 Dragon Scale
        //75 Ultimate Darkness Gem
        //5 Death's Oversight
        //25 Fire Avatar's Favor
        //13 Fragment of the Queen
        //250 ShadowChaos Mote
        //100 Warfury Emblem
        //3 Purified Undead Dragon Essence
        //Ascended BoA
        //50 Ice Shard
        //Hollowborn Doomblade
    }
}
