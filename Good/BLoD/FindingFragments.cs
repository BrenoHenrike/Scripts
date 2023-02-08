/*
name: FindingFragments
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Quests;
using Skua.Core.Models.Items;

public class FindingFragments_Any
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();

    public string OptionStorage = "Finding_Fragments";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("Max Everything", "Max all Rewards", "Does the \"Best\" quest for each drop until its maxed, if enabled ignore the other option", false),
        new Option<FindingFragmentsIDs>("questID", "Quest ID", "ID of the desired Finding Fragments quest to do.", FindingFragmentsIDs.Blade)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FindingFragments();

        Core.SetOptions(false);
    }

    public void FindingFragments()
    {

        if (!Bot.Config.Get<bool>("Max Everything"))
        {
            Quest QuestData = Core.EnsureLoad((int)Bot.Config.Get<FindingFragmentsIDs>("questID"));
            ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
            ItemBase[] QuestReward = QuestData.Rewards.ToArray();

            foreach (ItemBase item in RequiredItems.Concat(QuestReward))
                Bot.Drops.Add(item.ID);

            Core.EquipClass(ClassType.Farm);

            foreach (ItemBase item in QuestReward)
            {
                while (!Bot.ShouldExit && !Bot.Inventory.IsMaxStack(item.ID))
                    BLOD.FindingFragments((int)Bot.Config.Get<FindingFragmentsIDs>("questID"), item.Name, Bot.Inventory.GetItem(item.ID).MaxStack);

            }
        }
        else
        {
            foreach (Quest q in Core.EnsureLoad(2174, 2175, 2176, 2177, 2178, 2179))
            {
                foreach (ItemBase item in q.Rewards)
                {
                    Core.AddDrop(q.Rewards.Select(x => x.ID).ToArray());
                    while (!Bot.ShouldExit && !Bot.Inventory.IsMaxStack(item.ID))
                        BLOD.FindingFragments(q.ID, item.Name, Bot.Inventory.GetItem(item.ID).MaxStack);
                }
            }
        }
    }
}

public enum FindingFragmentsIDs
{
    Bow = 2174,
    Dagger = 2175,
    Mace = 2176,
    Scythe = 2177,
    Broadsword = 2178,
    Blade = 2179
}
