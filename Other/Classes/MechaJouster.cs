/*
name: MechaJouster (Class)
description: This script will get MechaJouster class.
tags: jouster, mechajouster, treasure potion, class, mecha jouster
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class MechaJouster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetMJ();

        Core.SetOptions(false);
    }

    public void GetMJ(bool rankUpClass = true)
    {
        if (Core.CheckInventory("MechaJouster"))
        {
            if (rankUpClass)
                Adv.RankUpClass("MechaJouster");
            return;
        }

        if (!Core.CheckInventory("Jouster"))
        {
            Core.Logger("In order to get MechaJouster class you need to have Jouster armor in your inventory, buy it from Doom Merge.");
            return;
        }

        Core.AddDrop("MechaJouster");

        Core.EnsureAccept(3355);
        Core.HuntMonster("airstorm", "Lightning Ball", "Energy Orb");
        Core.EnsureComplete(3355);

        if (rankUpClass)
            Adv.RankUpClass("MechaJouster");
    }
}
