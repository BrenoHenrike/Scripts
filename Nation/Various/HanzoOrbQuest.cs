//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class HanzoOrbQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        HanzoOrb();

        Core.SetOptions(false);
    }


    public void HanzoOrb()
    {
        if (!Core.CheckInventory("Astral Hanzo Orb") || Core.CheckInventory("Crimson Hanzo Orb"))
        {
            Core.Logger("Neither orb owned, stopping");
            return;
        }

        int questID = 1;

        if (Core.CheckInventory("Astral Hanzo Orb"))
        {
            Core.Logger("using Astral Hanzo Orb Quest");
            questID = 4020;
        }
        if (Core.CheckInventory("Crimson Hanzo Orb"))
        {
            Core.Logger("using Crimson Hanzo Orb Quest");
            questID = 4019;
        }

        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        foreach (string item in RewardsList)
        {

            Core.RegisterQuests(questID);
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