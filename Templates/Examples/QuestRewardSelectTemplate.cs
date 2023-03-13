/*
name: questgivername Quest Rewards
description: farms quest rewards from `quest name here` in /mapname
tags: mapname, questgivername, quest rewards
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class QuestRewardSelectTemplate
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetRewards();

        Core.SetOptions(false);
    }

    /*
    Replace the `0000` with the quest for the drops u want, this can be found in the 
    loader just press update or if no quests show /updateall (may take a few minutes)
    */
    int QuestID = 0000;
    public void GetRewards()
    {
        // Any Quest Requirements for the Quest(aka stories / quuests to unlock this farm go below herehere)
        //if you dont know how to do the references check the discord pins in #skua-chat for the video tutorial.
        //PrerequisiteStory.Storyline(); <- like this

        List<ItemBase> RewardOptions = Core.EnsureLoad(QuestID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;

            Core.FarmingLogger(Reward.Name, 1);

            Core.EnsureAccept(QuestID);

            Core.HuntMonster("map", "mob", "drop", quant, isTemp: truefalse, log: false);

            Core.EnsureComplete(QuestID, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}

/*
Possible extras:
    List<ItemBase> RewardOptions1 = Core.EnsureLoad(QuestID).Rewards;
    List<ItemBase> RewardOptions2 = Core.EnsureLoad(QuestID).Rewards;
  foreach (ItemBase item in RewardOptions1.Concat(RewardOptions2).ToArray())
  {    
    if(Core.CheckInventory(item.ID, Quant))
        return;
    
    Core.RegisterQuest(QuestID);
    while (!Bot.ShouldExit && !Core.CheckInventory(item.ID))
    {
        dostuff
    }
    Core.CancelRegisteredQuests();
  }
*/
