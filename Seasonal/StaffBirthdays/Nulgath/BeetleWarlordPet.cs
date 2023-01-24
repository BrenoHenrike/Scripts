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

    int questID = 9077;
    int quant = 1;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        QuestsIfNeeded();
        RequiredItems("Beetle Warlord Pet");
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

        Core.RegisterQuests(questID);
        Core.AddDrop("Baby Chaos Dragon", "Reaper's Soul");
        foreach (string item in Rewards)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item))
            {
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("dragonchallenge", "Chaos Dragon", "Baby Chaos Dragon", isTemp: false);
                Core.HuntMonster("thevoid", "Reaper", "Reaper's Soul", isTemp: false);
                Nation.FarmTotemofNulgath(1);
            }
            Core.CancelRegisteredQuests();
        }
    }

    public void QuestsIfNeeded()
    {
        Farm.Experience(80);
    }

    void RequiredItems(params string[] items)
    {
        if (Core.CheckInventory(items) || items == null)
            return;
        else Core.Logger("Required Items not found, Stopping", stopBot: true);
    }
}