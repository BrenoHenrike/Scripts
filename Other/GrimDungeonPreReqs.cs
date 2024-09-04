/*
name: Grimskull Dungeon Pre Reqs
description: This script will complete the quest prereqs to unlock Grimskull Dungeon.
tags: grim, dungeon, grimskull, trolling, rep, skullcrusher, gaol, grimgaol, gaolcell
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DoomVault.cs
//cs_include Scripts/Story/DoomVaultB.cs
//cs_include Scripts/Other/MergeShops/InfernalArenaMerge.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/InfernalArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Story/J6Saga.cs
using Skua.Core.Interfaces;

public class GrimDungeonPreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private CoreAdvanced Adv = new();
    private DoomVaultB DVB = new();
    private InfernalArenaMerge IAM = new();
    private J6Saga J6 = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        PreReqs();

        Core.SetOptions(false);
    }

    public void PreReqs()
    {
        if (Core.isCompletedBefore(9465))
            return;

        DVB.StoryLine();

        if (!Core.isCompletedBefore(8740))
        {
            Core.Logger("You need to get Smite Forge Enhancement, run Unlock Forge Enhancement script first.");
            return;
        }

        // Smite the Boulder! (9463)
        if (!Story.QuestProgression(9463))
        {
            Core.EnsureAccept(9463);
            Adv.BuyItem("forge", 2142, 70990);
            Core.GetMapItem(12327, map: "gaolcell");
            Core.EnsureComplete(9463);
        }

        // Strike the Boulder! (9464)
        if (!Story.QuestProgression(9464))
        {
            IAM.BuyAllMerge("Scythe of Azalith");
            Core.EnsureAccept(9464);
            Core.GetMapItem(12328, map: "gaolcell");
            Core.EnsureComplete(9464);
        }

        // Smash the Boulder! (9465)
        if (!Story.QuestProgression(9465))
        {
            J6.J6(true);
            Adv.BuyItem("hyperspace", 194, "J6's Hammer");
            Core.EnsureAccept(9465);
            Core.GetMapItem(12329, map: "gaolcell");
            Core.EnsureComplete(9465);
        }


    }



}
