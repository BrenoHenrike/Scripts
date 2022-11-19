//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;


public class BirthdayFeastQuestRewards
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreSepulchure CoreSS = new();

    public string OptionsStorage = "BirthdayFeast";

    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("ArlettesQuests", "ArlettesQuests Rewards", "Farms All \"Arlette's Birthday Quest (8385)\" Rewards.", false),
        new Option<bool>("InanitasQuests", "InanitasQuests Rewards", "Farms All \"Inanitas' Birthday Quest (8384)\" Rewards.", false),
        new Option<bool>("MemetsQuests", "MemetsQuests Rewards", "Farms All \"Memet's Birthday Quest (8382)\" Rewards.", false),
        new Option<bool>("KotarosQuests", "KotarosQuests Rewards", "Farms All \"Kotaro's Birthday Quest (8383)\" Rewards.", false),

    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        if (Bot.Config.Get<bool>("ArlettesQuests"))
        {
            ArlettesQuests();
            Core.ToBank(Core.EnsureLoad(8385).Rewards.ToString());
        }

        if (Bot.Config.Get<bool>("InanitasQuests"))
        {
            InanitasQuests();
            Core.ToBank(Core.EnsureLoad(8384).Rewards.ToString());
        }
        if (Bot.Config.Get<bool>("MemetsQuests"))
        {
            MemetsQuests();
            Core.ToBank(Core.EnsureLoad(8382).Rewards.ToString());
        }
        if (Bot.Config.Get<bool>("KotarosQuests"))
        {
            KotarosQuests();
            Core.ToBank(Core.EnsureLoad(8383).Rewards.ToString());
        }

        Core.SetOptions(false);
    }

    int i = 0;

    void ArlettesQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8385).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(8385); //Arlette's Birthday Quest 8385
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("celestialrealm", "Celestial Bird of Paradise", "Celestial Artifact", 6, log: false);
                    Core.HuntMonster("celestialrealm", "Infernal Imp| Infernal Knight", "Infernal Artifact", 6, log: false);

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }

            }

        }


        // foreach (ItemBase Reward in RewardOptions) { Core.ToBank(Reward.Name); }
    }

    void InanitasQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8384).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8384); // Inanitas' Birthday Quest 8384
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("timeinn", "Ezrajal", "Ezrajal's Feather", log: false);

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }

            }
        }
        // foreach (ItemBase Reward in RewardOptions) { Core.ToBank(Reward.Name); }
    }

    void KotarosQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8383).Rewards;

        foreach (ItemBase item in RewardOptions)
            Bot.Drops.Add(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8383); //Kotaro's Birthday Quest 8383
        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);

                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("shinkansen", "Saint Eta", "Saint Eta's Gauntlet", log: false);

                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }


        }
        // foreach (ItemBase Reward in RewardOptions) { Core.ToBank(Reward.Name); }
    }

    void MemetsQuests()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8384).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        string[] QuestRewards = RewardOptions.Select(x => x.Name).ToArray();

        Core.RegisterQuests(8382); // Memet's Birthday Quest 8382
        Core.EquipClass(ClassType.Solo);

        foreach (ItemBase Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                Core.Logger($"{Reward.Name} Found.");
            else
            {
                Core.FarmingLogger(Reward.Name, 1);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, toInv: false))
                {
                    Core.HuntMonster("byrodax", "Mutated Critter", "Picture of Mutated Critter", 3, log: false);
                    Core.HuntMonster("byrodax", "Byro-Dax Monstrosity", "Picture of Byro-Dax Monstrosity", log: false);
                    i++;

                    if (i % 5 == 0)
                    {
                        Core.JumpWait();
                        Core.ToBank(QuestRewards);
                    }
                }
            }
        }
        // foreach (ItemBase Reward in RewardOptions) { Core.ToBank(Reward.Name); }
    }
}