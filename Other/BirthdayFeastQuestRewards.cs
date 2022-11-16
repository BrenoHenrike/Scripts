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

        ArlettesQuests();
        InanitasQuests();
        MemetsQuests();
        KotarosQuests();

        Core.SetOptions(false);
    }

    void ArlettesQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8385).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(8385);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                return;
            }
            Core.FarmingLogger(Reward.Name, 1);

            Core.HuntMonster("celestialrealm", "Celestial Bird of Paradise", "Celestial Artifact", 6, log: false);
            Core.HuntMonster("celestialrealm", " Infernal Imp| Infernal Knight", "Infernal Artifact ", 6, log: false);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }

    void InanitasQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8384).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8384);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                return;
            }

            Core.FarmingLogger(Reward.Name, 1);
            Core.HuntMonster("timeinn", "Ezrajal", "Ezrajal's Feather", log: false);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }

    }

    void KotarosQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8383).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8383);
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                return;
            }

            Core.FarmingLogger(Reward.Name, 1);

            Core.HuntMonster("shinkansen", "Saint Eta", "Saint Eta's Gauntlet", log: false);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
    }

    void MemetsQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8384).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        Core.RegisterQuests(8384);
        Core.EquipClass(ClassType.Solo);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                return;
            }

            Core.FarmingLogger(Reward.Name, 1);
            Core.HuntMonster("byrodax", "Mutated Critter", "Picture of Mutated Critter", 3, log: false);
            Core.HuntMonster("byrodax", "Byro-Dax Monstrosity", "Picture of Byro-Dax Monstrosity", log: false);
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }

    }
}