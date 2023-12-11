/*
name: Mine Crafting Daily
description: Automatically does the MineCrafting quest for you and picks what metals are needed.
tags: daily, mine, crafting, metal, barium, copper, silver, aluminum, gold, iron, platinum, BLOD
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
using Skua.Core.Interfaces;

public class MineCrafting
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreDailies Daily = new();
    private readonly CoreBLOD BLOD = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoMinecrafting();

        Core.SetOptions(false);
    }

    public void DoMinecrafting()
    {
        BLOD.UnlockMineCrafting();

        if (!Daily.CheckDaily(2091))
        {
            if (!Core.CheckInventory("Blinding Light of Destiny", toInv: false))
            {
                Core.Logger("Blinding Light of Destiny not owned yet getting metals");
                Daily.MineCrafting(new[] { "Barium", "Copper", "Silver" }, 1, ToBank: true);
            }
            else if (!Core.CheckInventory("Necrotic Sword of Doom", toInv: false))
            {
                Core.Logger("NSoD not owned yet getting metals");
                Daily.MineCrafting(new[] { "Barium" }, 4, ToBank: true);
            }
            else
            {
                Core.Logger("BLoD & NSoD owned, getting extra metals.");
                Daily.MineCrafting(new[] { "Aluminum", "Barium", "Gold", "Iron", "Copper", "Silver", "Platinum" }, 10, ToBank: true);
            }
        }

        if (!Core.IsMember || Daily.CheckDaily(2090))
        {
            Core.Logger(!Core.IsMember ? "Membership required for SDK + HardCoreMetals stopping." : "Daily already complete, try tomarrow.");
            return;
        }
        
        if (!Core.CheckInventory("Sepulchure's DoomKnight Armor", toInv: false))
        {
            Daily.HardCoreMetals(new[] { "Rhodium" }, 2, true);
            Daily.HardCoreMetals(new[] { "Beryllium" }, 1, true);
            Daily.HardCoreMetals(new[] { "Chromium" }, 2, true);
        }
        else Daily.HardCoreMetals(new[] { "Arsenic", "Beryllium", "Chromium", "Palladium", "Rhodium", "Rhodium", "Thorium", "Mercury" }, 10, ToBank: true);
    }
}