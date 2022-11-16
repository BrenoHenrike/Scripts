//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class BirthdayFeastQuestRewards
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreSepulchure CoreSS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreSS.ShadowfallRise();
        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8383).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Solo);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;
            Core.FarmingLogger(Reward.Name, 1);

            Core.EnsureAccept(8383);
            Core.HuntMonster("shinkansen", "Saint Eta", "Saint Eta's Gauntlet", log: false);
            Core.EnsureComplete(8383, Reward.ID);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }
}