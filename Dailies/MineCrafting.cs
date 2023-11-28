/*
name: Mine Crafting Daily
description: Mine Crafting
tags: daily, mine crafting, metal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class MineCrafting
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreBLOD BLOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoMinecrafting();

        Core.SetOptions(false);
    }

    public void DoMinecrafting()
    {
        BLOD.UnlockMineCrafting();

        if (!Core.CheckInventory("Blinding Light of Destiny") && !Daily.CheckDaily(2091, false))
            Daily.MineCrafting(new[] { "Barium", "Copper", "Silver" }, 1, ToBank: true);
        
        else if (!Core.CheckInventory("Necrotic Sword of Doom") && !Daily.CheckDaily(2091, false))
            Daily.MineCrafting(new[] { "Barium" }, 4, ToBank: true);
        
        else if (!Core.CheckInventory("Sepulchure's DoomKnight Armor") && Core.IsMember)
        {
            Daily.HardCoreMetals(new[] { "rhodium " }, 2, true);
            Daily.HardCoreMetals(new[] { "Beryllium  " }, 1, true);
            Daily.HardCoreMetals(new[] { "Chromium " }, 2, true);
        }
        
        else if (Core.IsMember)
            Daily.HardCoreMetals(new[] { "Arsenic", "Beryllium", "Chromium", "Palladium", "Rhodium", "Rhodium", "Thorium", "Mercury" }, 10, ToBank: true);
        
        else Daily.MineCrafting(new[] { "Aluminum", "Barium", "Gold", "Iron", "Copper", "Silver", "Platinum" }, 10, ToBank: true);
    }
}
