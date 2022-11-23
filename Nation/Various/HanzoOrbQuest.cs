//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class HanzoOrbQuest
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        HanzoOrb();

        Core.SetOptions(false);
    }


    public void HanzoOrb(string Reward = "Any", int quant = 1, bool AnyReward = false)
    {
        if (!Core.CheckInventory("Astral Orb Pet") && !Core.CheckInventory("Crimson Orb Pet"))
        {
            Core.Logger("Neither orb owned, stopping");
            return;
        }

        int questID = 1;

        if (Core.CheckInventory("Astral Orb Pet"))
        {
            Core.Logger("using Astral Orb Pet Quest");
            questID = 4020;
        }
        if (Core.CheckInventory("Crimson Orb Pet"))
        {
            Core.Logger("using Crimson Orb Pet Quest");
            questID = 4019;
        }

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);
        if (AnyReward)
        {
            foreach (string item in RewardsList)
            {

                Core.RegisterQuests(questID);
                while (!Bot.ShouldExit && !Core.CheckInventory(item) && !Bot.Inventory.IsMaxStack(item) && !Core.CheckInventory("Blood Star Blade"))
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
        else
        {
            Core.RegisterQuests(questID);
            while (!Bot.ShouldExit && !Core.CheckInventory(Reward, quant))
            {
                Core.HuntMonster("graveyard", "Big Jack Sprat", "Jacked Eye", 5);
                Core.HuntMonster("marsh", "Dreadspider", "Dreadspider Silk");
                Core.HuntMonster("nulgath", "Dreadspider", "Dreadspider Silk");
                Core.HuntMonster(Core.IsMember ? "nulgath" : "tercessuinotlim", "Dark Makai", "Makai Fang", 5);
                Core.HuntMonster("bludrut", "Rattlebones", "Rattle Bones", 3);
            }
            Core.CancelRegisteredQuests();
        }
    }
}