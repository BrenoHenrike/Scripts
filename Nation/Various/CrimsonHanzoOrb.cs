//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class CrimsonHanzoOrbQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CrimsonHanzoOrb();

        Core.SetOptions(false);
    }


    public void CrimsonHanzoOrb()
    {

        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(4019).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        foreach (string item in RewardsList)
        {

            Core.RegisterQuests(4019);
            while (!Bot.ShouldExit() && !Core.CheckInventory(item) && !Bot.Inventory.IsMaxStack(item) && !Core.CheckInventory("Blood Star Blade"))
            {
                Core.HuntMonster("graveyard", "Big Jack Sprat", "Jacked Eye", 5);
                Core.HuntMonster("marsh", "Dreadspider", "Dreadspider Silk");
                Core.HuntMonster("nulgath", "Dreadspider", "Dreadspider Silk");
                Core.HuntMonster(Core.IsMember ? "nulgath" : "tercessuinotlim", "Dark Makai", "Makai Fang", 5);
                Core.HuntMonster("bludrut", "Rattlebones", "Rattle Bones", 3);
            }
        }
        Core.CancelRegisteredQuests();
    }
}