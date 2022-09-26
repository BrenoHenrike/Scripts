//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class SunkenTreasure
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(7715).Rewards;
        List<string> RewardsList = new List<string>();
        List<string> RewardList = RewardOptions.Select(x => x.Name).ToList();
        RewardList.Remove("Hidden Pirate Base");
        string[] Rewards = RewardList.ToArray();

        if (Core.CheckInventory("Rewards", toInv: false))
            return;

        Bot.Drops.Add(Rewards);

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(7715);
        while (!Bot.ShouldExit && !Core.CheckInventory("Rewards", toInv: false) && !Bot.House.Contains("Hidden Pirate Base"))
        {
            //Sunken Treasure? 7715
            Core.HuntMonster("Pirates", "Shark Bait", "Waterlogged Chest");
            Core.HuntMonster("Pirates", "Fishman Soldier", "Rusty Key");
            Core.Jump("Wait", "Spawn");
            Core.ToBank(Rewards);
        }
        Core.CancelRegisteredQuests();

    }
}
