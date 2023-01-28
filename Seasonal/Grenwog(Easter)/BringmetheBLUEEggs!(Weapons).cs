/*
name: Steampunk Weapons
description: This will finish the Bring me the BLUE Eggs quest to obtain all of the steampunk weapons.
tags: steampunk-weapons, bring-me-the-blue-eggs, seasonal, easter
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class BlueEggs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBlueEggs();

        Core.SetOptions(false);
    }

    public void GetBlueEggs()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(5786).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        if (Core.CheckInventory(Rewards))
        {
            Core.Logger("You already own these items, Stopping Bot.");
            return;
        }

        while (!Bot.ShouldExit && (!Core.CheckInventory(Rewards)))
        {
            Core.EnsureAccept(5786);
            Core.HuntMonster("EarthStorm", "Blue Chick", "Blue Chick");
            Core.GetMapItem(5224, 6, "EarthStorm");
            Core.EnsureCompleteChoose(5786);
            Bot.Wait.ForPickup("*");
        }
    }
}



