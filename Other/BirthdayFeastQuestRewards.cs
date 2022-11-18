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
        Core.Logger("Arlette's quests started");
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8385).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        Core.EquipClass(ClassType.Farm);

        int RewardCount = 1;
        Core.RegisterQuests(8385);
        foreach (ItemBase Reward in RewardOptions)
        {
            Core.Logger($"{RewardCount} / {RewardOptions.Count} - {Reward.Name}");

            if (Core.CheckInventory(Reward.ID, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                RewardCount++;
            }

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.HuntMonster("celestialrealm", "Celestial Bird of Paradise", "Celestial Artifact", 6, log: false);
                Core.HuntMonster("celestialrealm", "Infernal Imp|Infernal Knight", "Infernal Artifact", 6, log: false);

                if (Bot.Inventory.FreeSlots == 0)
                    Core.ToBank(Core.EnsureLoad(8385).Rewards.ToString());

                if (Core.CheckInventory(Reward.ID, toInv: false))
                    Core.ToBank(Reward.Name);
            }
        }
        Core.Logger("Arlette's quests done");
    }

    void InanitasQuests()
    {
        Core.Logger("Inanita's quests started");
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8384).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8384);
        int RewardCount = 1;
        foreach (ItemBase Reward in RewardOptions)
        {
            Core.Logger($"{RewardCount} / {RewardOptions.Count} - {Reward.Name}");

            if (Core.CheckInventory(Reward.ID, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                RewardCount++;
            }

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.HuntMonster("timeinn", "Ezrajal", "Ezrajal's Feather", log: false);

                if (Bot.Inventory.FreeSlots == 0)
                    Core.ToBank(Core.EnsureLoad(8384).Rewards.ToString());

                if (Core.CheckInventory(Reward.ID, toInv: false))
                    Core.ToBank(Reward.Name);
            }
        }
        Core.Logger("Inanita's quests done");
    }

    void KotarosQuests()
    {
        Core.Logger("Kotaro's quests started");
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8383).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        Core.EquipClass(ClassType.Solo);

        int RewardCount = 1;
        Core.RegisterQuests(8383);
        foreach (ItemBase Reward in RewardOptions)
        {
            Core.Logger($"{RewardCount} / {RewardOptions.Count} - {Reward.Name}");

            if (Core.CheckInventory(Reward.ID, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                RewardCount++;
            }

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.HuntMonster("shinkansen", "Saint Eta", "Saint Eta's Gauntlet", log: false);

                if (Bot.Inventory.FreeSlots == 0)
                    Core.ToBank(Core.EnsureLoad(8383).Rewards.ToString());

                if (Core.CheckInventory(Reward.ID, toInv: false))
                    Core.ToBank(Reward.Name);
            }
        }
        Core.Logger("Kotaro's quests done");
    }

    void MemetsQuests()
    {
        Core.Logger("Memet's quests started");
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8382).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        int RewardCount = 1;
        Core.RegisterQuests(8382);
        Core.EquipClass(ClassType.Solo);
        foreach (ItemBase Reward in RewardOptions)
        {
            Core.Logger($"{RewardCount} / {RewardOptions.Count} - {Reward.Name}");

            if (Core.CheckInventory(Reward.ID, toInv: false))
            {
                Core.Logger($"{Reward.Name} Found.");
                RewardCount++;
            }

            while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID))
            {
                Core.HuntMonster("byrodax", "Mutated Critter", "Picture of Mutated Critter", 3, log: false);
                Core.HuntMonster("byrodax", "Byro-Dax Monstrosity", "Picture of Byro-Dax Monstrosity", log: false);

                if (Bot.Inventory.FreeSlots == 0)
                    Core.ToBank(Core.EnsureLoad(8382).Rewards.ToString());

                if (Core.CheckInventory(Reward.ID, toInv: false))
                    Core.ToBank(Reward.Name);
            }
        }
        Core.Logger("Memet's quests done");
    }
}