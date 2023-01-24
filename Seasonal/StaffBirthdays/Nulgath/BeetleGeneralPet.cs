//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiegeMerge.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class BeetleGeneralPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private TempleSiegeMerge TSM = new();
    private CoreNation Nation = new();
    private CoreHollowborn HB = new();
    int questID = 9076;
    int quant = 1;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        QuestsIfNeeded();
        RequiredItems("Beetle General Pet");
        AutoReward(questID, quant);
    }

    public void AutoReward(int questID, int quant)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);
        Core.AddDrop("Red Ant Pet", "Beetle EXP");
        Core.RegisterQuests(questID);
        foreach (string item in Rewards)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
            {
                Core.HuntMonster("giant", "Red Ant", "Red Ant Pet", isTemp: false);
                Nation.EssenceofNulgath(10);
                HB.FreshSouls(0, 10);
            }
            Core.CancelRegisteredQuests();
        }
    }

    public void QuestsIfNeeded()
    {
        if (!Core.CheckInventory("Beetle General Pet"))
            TSM.BuyAllMerge("Beetle General Pet");
    }

    void RequiredItems(params string[] items)
    {
        if (Core.CheckInventory(items) || items == null)
            return;
        else Core.Logger("Required Items not found, Stopping", stopBot: true);
    }
}
