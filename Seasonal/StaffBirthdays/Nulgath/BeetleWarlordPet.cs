/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class BeetleWarlordPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreNation Nation = new();

    int questID = 9078;
    int quant = 1;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        AutoReward(questID, quant);

        Core.SetOptions(false);
    }

    public void AutoReward(int questID, int quant)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.AddDrop("Baby Chaos Dragon", "Reaper's Soul");
        Farm.Experience(80);
        
        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID))
            {
                Core.EnsureAccept(questID);
                Nation.FarmTotemofNulgath(1);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("dragonchallenge", "Chaos Dragon", "Baby Chaos Dragon", isTemp: false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("thevoid", "Reaper", "Reaper's Soul", isTemp: false);
                Core.EnsureComplete(questID, item.ID);
                Core.JumpWait();
                Core.ToBank(item.ID);
            }
        }
    }
}
