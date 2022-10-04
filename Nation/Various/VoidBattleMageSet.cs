//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/VasalkarLairWar.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class VoidBattleMageSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public LairWar War = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(6694).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);
            
        Core.EquipClass(ClassType.Solo);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;
            Core.FarmingLogger(Reward.Name, 1);

            Core.EnsureAccept(6694);
            Core.KillMonster("lairattack", "Eggs", "Left", "Flame Dragon General Defeated", log: false);
            Core.EnsureComplete(6694, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}
