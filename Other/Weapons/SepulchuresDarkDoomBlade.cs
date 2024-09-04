/*
name: Sepulchure's Dark DoomBlade
description: This script will get Sepulchure's Dark DoomBlade.
tags: sepulchure, doomblade, dark doomblade, doom seal, darkness shard, undead doomblade, doom blade
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Other/Materials/DarknessShard.cs
using Skua.Core.Interfaces;

public class GetSDDB
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private DarknessShard DS = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetWeapon();

        Core.SetOptions(false);
    }

    public void GetWeapon()
    {
        if (Core.CheckInventory("Sepulchure's Dark DoomBlade") || (!Core.CheckInventory("Sepulchure's Dark DoomBlade") && !Core.CheckInventory("Sepulchure's Undead DoomBlade")))
        {
            Core.Logger("You already own this weapon or you don't have the required weapon (Sepulchure's Undead DoomBlade).");
            return;
        }

        Core.AddDrop(Core.QuestRewards(6397));

        // Dark Doomblade (6397)
        Core.EnsureAccept(6397);
        Core.HuntMonster("ebondungeon", "Elite Dungeon Guard", "Doom Seal", 13, false);
        DS.GetShard(1);
        Core.EnsureComplete(6397);
    }
}
