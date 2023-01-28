/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class QuestRewardTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    //Insert Reward ID(load quest then -> tools > grabber > quests > grab > 
    //click the quest, and on the right scroll down to \"Rewards\", click the ... to the Right, and the ids are beside the names
    int itemID = 0000;
    int questID = 0000;
    int quant = 0;
    //Please fillin the above^

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(true);
    }

    public void DoQuest()
    {

        QuestsIfNeeded(); //fillin below
        RequiredItems("item", "item"); // Replace these
        
        // RandomReward(0000, 1);//-----|
        // SelectReward(0000, 1);//-----| Pick one
        // AutoReward(0000, 1);  //-----|
        // "//" the one you want to use.
    }


    //Use RandomReward for non-guaranteed Rewards
    //Use AutoReward for Quests with Selectable Rewards that you want all of them
    //Use SelectReward for Quests with Selectable Rewards that you want a select one of (itemID above is the item).
    //Use OptionsSelect for a List(must be filled in in the Enum Below.)

    //Basicly when making Set/simple quest reward scripts... pick one of these // the rest in scriptmain 👍

    private void RandomReward(int questID = 0000, int quant = 1)
    {
        int i = 0;

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(questID);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, quant, toInv: false))
                {

                    //kill/Hunt Goes here.

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }
        }
    }

    public void SelectReward(int questID = 0000, int quant = 1)
    {
        ItemBase item = Core.EnsureLoad(questID).Rewards.Find(x => x.ID == itemID); //<-- replace 0000 with the itemID

        Bot.Drops.Add(item.ID);

        while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
        {
            Core.EnsureAccept(questID);
            //Insert hunt/kill from quest heres
            Core.EnsureComplete(questID, item.ID);
        }
        Core.CancelRegisteredQuests();
    }

    public void AutoReward(int questID = 0000, int quant = 1)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
            {
               Core.EnsureAccept(questID);
                //Questing stuff here --
               Core.EnsureComplete(questID, item.ID);
            }
        }
    }

    public void QuestsIfNeeded()
    {
        //Import from other scritps as you normaly would.

        // Example: LOC.Complete13LOC(); for the chaos saga
    }

    void RequiredItems(params string[] items)
    {
        if (Core.CheckInventory(items) || items == null)
            return;
        else Core.Logger("Required Items not found, Stopping", stopBot: true);
    }
}
//Ignore below its.. storage




// stuff not needed:
//     public void OptionsSelect(Template reward = new(), int questID = 000)
//     {
//         List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
//         List<string> RewardsList = new List<string>();
//         foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
//             RewardsList.Add(Item.Name);

//         ItemBase item = Core.EnsureLoad(questID).Rewards.Find(x => x.ID == (int)Bot.Config.Get<Template>("RewardSelect"));

//         if (item == null)
//             Core.Logger($"{item.Name} not found in Quest Rewards", stopBot: true);

//         if (Core.CheckInventory(item.Name))
//             return;

//         while (!Core.CheckInventory(item.Name))
//         {
//             Core.EnsureAccept(questID);
//             //Questing stuff here --
//             if (Bot.Config.Get<Template>("RewardsSelection") != Template.All)
//                 Core.EnsureComplete(554, item.ID);
//             else Core.EnsureComplete(questID, item.ID);
//         }
//     }

// public enum Template
// {
//     All,
//     Reward1 = 1,
//     Reward2 = 2,
//     Reward3 = 3,
//     Reward4 = 4,
// };	

// public bool DontPreconfigure = true;

// public string OptionsStorage = "QuestRewardTemplate";


// public List<IOption> Options = new List<IOption>()
// {
//     CoreBots.Instance.SkipOptions,
//     new Option<int>("questID", "QuestID", "Insert Quest ID for Bot to use", 0000),
//     new Option<int>("RewardID", "ItemID:", "Insert Reward ID(load quest then -> tools > grabber > quests > grab > click the quest, and oh the right scroll down to \"Rewards\", click the ... and the ids are beside the names", "ItemName"),
//     new Option<bool>("SelectReward", "Choose the Reward?", "Select the Reward the Bot will Get, then stop.", false),
//     new Option<bool>("RandomReward", "Randomized Reward", "For Quests that the reward is \"You may also  receive at random:\"", false),
//     new Option<bool>("AutoReward", "AllRewards", "Complete the quest, untill all queust rewards are gotten.}", false),
// };
