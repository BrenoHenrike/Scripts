//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class TheDarkBox
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8375).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();
        Bot.Drops.Add(Rewards);

        if (Core.CheckInventory(Rewards, toInv: false) || !Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
            return;

        //The Dark Box 5710
        Core.RegisterQuests(5710);
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            if (Core.IsMember)
                Core.HuntMonster("darkfortress", "Dark Elemental", "Dark Gem", isTemp: false);
            else Core.HuntMonster("ruins", "Dark Elemental", "Dark Gem", isTemp: false);
            Core.JumpWait();
            Core.ToBank(Rewards);
        }
        Core.CancelRegisteredQuests();
        
    }
}
