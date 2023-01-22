/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class EmpoweringItems
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        EmpoweringStuff();

        Core.SetOptions(false);
    }
    public void EmpoweringStuff()
    {
        if (Core.CheckInventory("Death Scythe of Nulgath"))
            return;

        Core.AddDrop("Death Scythe of Nulgath");

        Core.EnsureAccept(558);
        Nation.FarmUni13();
        Nation.FarmDiamondofNulgath(10);
        Core.HuntMonster("underworld", "Undead Bruiser", "Undead Bruiser Sigil");
        Core.EnsureComplete(558);
        Bot.Wait.ForPickup("Death Scythe of Nulgath");
    }
}
