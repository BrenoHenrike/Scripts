//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

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

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);
        Bot.Drops.Add("Hidden Pirate Base");

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;
            Core.FarmingLogger(Reward.Name, 1);

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name) && !Bot.House.Contains("Hidden Pirate Base"))
            {
                //Sunken Treasure? 7715
                Core.EnsureAccept(7715);
                Core.HuntMonster("Pirates", "Shark Bait", "Waterlogged Chest");
                Core.HuntMonster("Pirates", "Fishman Soldier", "Rusty Key");
                Core.KillMonster("lairattack", "Eggs", "Left", "Flame Dragon General Defeated", log: false);
                Core.EnsureComplete(7715, Reward.ID);
                Core.JumpWait();
                Core.ToBank(Reward.Name);
            }
        }
    }
}
