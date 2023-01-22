/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class MadWeaponCrafting
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreToD ToD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(7070).Rewards;
        List<string> RewardsList = new List<string>();
        List<string> RewardList = RewardOptions.Select(x => x.Name).ToList();
        string[] Rewards = RewardList.ToArray();

        if (Core.CheckInventory(Rewards, toInv: false))
            return;

        Bot.Drops.Add(Rewards);

        ToD.ShiftingPyramid();
        Core.RegisterQuests(7070);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            //Mad Weapon Crafting 7070
            while (!Bot.ShouldExit && !Core.CheckInventory("Anti-Matter Gem", 15))
            {
                //Reflections of Victory 5188
                Core.AddDrop("Anti-Matter Gem");
                Core.EnsureAccept(5188);
                Core.HuntMonster("Whitehole", "Dimensional Crystal", "Crystal Shards", 5);
                Core.EnsureComplete(5188);
            }
            Core.HuntMonster("Artixpointe", "Enchanted Sushi", "Sushi!!!");
            Core.HuntMonster("Citadel", "Grizzly Bear", "Bear Feet", 2);
            Core.Jump("Wait", "Spawn");
            Core.ToBank(Rewards);
        }
        Core.CancelRegisteredQuests();
    }
}
