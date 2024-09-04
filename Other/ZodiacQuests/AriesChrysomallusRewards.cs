/*
name: Aries Chrysomallus Rewards
description: does the "aries chrysomallus" quest for the rewards.
tags: aries chrysomallus, quest rewards, darkblood starshard, kolyaban, arcangrove, kylokos, zodiac
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;


public class AriesChrysomallusRewards
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();
        Core.SetOptions(false);
    }

    public void Example()
    {
        if (Core.CheckInventory(Core.QuestRewards(9192)))
            return;

        List<ItemBase> RewardOptions = Core.EnsureLoad(9192).Rewards;
        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        Core.EquipClass(ClassType.Farm);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    //Aries Chrysomallus
                    Core.EnsureAccept(9192);
                    Core.KillMonster("Kolyaban", "r2", "Left", "*", "Darkblood Starshard", 20, isTemp: false, log: false);
                    Core.EnsureComplete(9192, Reward.ID);
                }
            }
            Core.CancelRegisteredQuests();
        }
    }
}
