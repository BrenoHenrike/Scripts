/*
name: Fiendish Storm Orb Quest Pet
description: This script will complete the "Fiendish Storm Orb Quest Pet" [9507] quest.
tags: fiendish, cape of lightning, boltstriker, lightninglord, stormbringer, reaver, reavers
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ThunderFang.cs
//cs_include Scripts/Other/MergeShops/StormCacheMerge.cs
using Skua.Core.Interfaces;

public class FiendishStormOrbQuestPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private StormCacheMerge SCM = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(false);
    }

    public void DoQuest()
    {
        if ((!Core.CheckInventory(83230) && !Core.CheckInventory(83720)) || (Core.CheckInventory(83720) && !Core.IsMember))
        {
            Core.Logger("You need to own Fiendish Storm Orb Quest Pet, and have active membership for Member version to do this quest.");
            return;
        }

        Core.AddDrop(Core.QuestRewards(9576));

        if (Core.CheckInventory(83720))
            Core.EnsureAccept(9577);
        else
            Core.EnsureAccept(9576);

        SCM.BuyAllMerge("Cape of Lightning");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("pride", "Valsarian", "BoltStriker Armor", isTemp: false);
        Core.HuntMonster("queenreign", "Extriki", "LightningLord", isTemp: false);

        if (Core.CheckInventory(83720))
            Core.EnsureComplete(9577);
        else
            Core.EnsureComplete(9576);

    }
}
