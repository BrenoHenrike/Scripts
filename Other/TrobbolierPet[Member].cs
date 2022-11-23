//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/Friday13th/CoreFriday13th.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class TrobbolierPet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFriday13th F13 = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        
        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        F13.Wormhole();

        List<ItemBase> RewardOptions = Core.EnsureLoad(5067).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Farm);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;
            Core.FarmingLogger(Reward.Name, 1);

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name))
            {
                Core.EnsureAccept(5067);
                Core.HuntMonster("wormhole", "Blue Trobbolier", "Blue Trobbolier Fluff", 4, false);
                Core.HuntMonster("wormhole", "Purple Trobbolier", "Purple Trobbolier Fluff", 4, false);
                Core.HuntMonster("wormhole", "Green Trobbolier", "Green Trobbolier Fluff", 4, false);
                Core.HuntMonster("wormhole", "Red Trobbolier", "Red Trobbolier Fluff", 4, false);
                Core.EnsureComplete(5067, Reward.ID);
                Core.JumpWait();
                Core.ToBank(Reward.Name);
            }
        }
    }
}
