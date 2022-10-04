//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/MustyCave.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class GrimDarkSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public MustyCave Cave = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(7049).Rewards;

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
                Core.EnsureAccept(7049);
                Core.HuntMonster("mustycave", "Mogdring", "Golden Gear", 5, false);
                Core.HuntMonster("mustycave", "Spy Drone", "Aura Core", 25, false);
                Core.HuntMonster("mustycave", "Guard Drone", "Dimension Stabilizer", 35, false);
                Core.EnsureComplete(7049, Reward.ID);
                Core.JumpWait();
                Core.ToBank(Reward.Name);
            }
        }
    }
}
