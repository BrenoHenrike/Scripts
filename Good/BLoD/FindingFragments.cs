/*
name: FindingFragments
description: Does the "finding fragments with blinding [insert] of destiny for the quest rewards.
tags: finding fragments, blinding light fragments, spirit orb, loyal spirit orb, bright Aura, brilliant aura, blinding aura
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
        new Option<bool>("MaxAllQuestRewards", "Max all Quest Rewards", "Does the \"Best\" quest for each drop until its maxed, else For specific weapon's quest. If enabled ignore the other option", false),
        new Option<FindingFragmentsIDs>("questID", "Quest ID", "ID of the desired Finding Fragments quest to do.", FindingFragmentsIDs.Blade)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] {
        "Blinding Light Fragments",
        "Spirit Orb",
        "Loyal Spirit Orb",
        "Bright Aura",
        "Brilliant Aura",
        "Blinding Aura"});
        Core.SetOptions();

        FindingFragments();

        Core.SetOptions(false);
    }

    public void FindingFragments()
    {

        if (!Bot.Config.Get<bool>("MaxAllQuestRewards"))
        {
            Quest QuestData = Core.EnsureLoad((int)Bot.Config.Get<FindingFragmentsIDs>("questID"));
            ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
            ItemBase[] QuestReward = QuestData.Rewards.ToArray();

            foreach (ItemBase item in QuestReward)
            {
                while (!Bot.ShouldExit && !Core.CheckInventory(item.Name, item.MaxStack))
                    BLOD.FindingFragments((int)Bot.Config.Get<FindingFragmentsIDs>("questID"), item.Name, item.MaxStack);
                Core.Logger($"{item.Name} has reached max stack {item.Quantity}/{item.MaxStack}");
            }
        }
        else
        {
            foreach (Quest q in Core.EnsureLoad(2174, 2175, 2176, 2177, 2178, 2179))
            {
                foreach (ItemBase item in q.Rewards)
                {
                    Core.AddDrop(q.Rewards.Select(x => x.ID).ToArray());
                    while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
                        BLOD.FindingFragments(q.ID, item.Name, item.MaxStack);
                    Core.Logger($"{item.Name} has reached max stack {item.Quantity}/{item.MaxStack}");
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
