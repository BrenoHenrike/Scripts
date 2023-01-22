/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
using Skua.Core.Interfaces;

public class EnchantedFrozenClaymore
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public DragonFableOrigins DFO = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSword();

        Core.SetOptions(false);
    }

    public void GetSword()
    {
        if (Core.CheckInventory("Enchanted Frozen Claymore"))
            return;

        DFO.DragonFableOriginsAll();
        Core.EquipClass(ClassType.Solo);

        Core.AddDrop("Ice Shard");

        while (!Bot.ShouldExit && !Core.CheckInventory(43712, 50))
        {
            Core.EnsureAccept(6311);
            Core.HuntMonster("northmountain", "Izotz", "Ice Crystal");
            Core.EnsureComplete(6311);
            Bot.Wait.ForPickup("Ice Shard");
        }
        Core.BuyItem("northmountain", 1595, "Enchanted Frozen Claymore");
    }
}
