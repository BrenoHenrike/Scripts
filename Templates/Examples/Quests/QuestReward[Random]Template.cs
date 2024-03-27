/*
name: questgivername Quest Rewards
description: farms quest rewards from `quest name here` in /mapname
tags: mapname, questgivername, quest rewards
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class QuestRewardRandomTemplate
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

            Core.Logger(Core.CheckInventory(Reward.ID, toInv: false) ? $"{Reward.Name}: ✅" : $"{Reward.Name} ❌");

            Core.RegisterQuests(QuestID);
            Core.FarmingLogger(Reward.Name, 1);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
                //Edit teh HuuntMonster approriately
                Core.HuntMonster("map", "mob", "drop", quant, isTemp: truefalse, log: false);
            Core.CancelRegisteredQuests();

            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}
