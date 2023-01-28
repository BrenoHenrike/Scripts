/*
name: Berserker Bunny Armor
description: This will finish the quest to obtain the Berserker Bunny Armor.
tags: berserker-bunny-armor, seasonal, easter, BBA
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class BerserkerBunnyArmorEaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBBA();

        Core.SetOptions(false);
    }

    public void GetBBA()
    {
        if (Core.CheckInventory("Berserker Bunny Armor"))
            return;

        if (!Core.CheckInventory("Berserker Bunny Armor"))
        {
            Core.EnsureAccept(236);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Were Egg", log: false);
            Core.EnsureComplete(236);
            Bot.Wait.ForPickup("Berserker Bunny Armor");
        }
    }
}



