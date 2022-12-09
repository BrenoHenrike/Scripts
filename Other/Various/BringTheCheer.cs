//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class BringTheCheer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public Frostvale Frostvale = new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "BringTheCheer";

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<RewardsSelection>("RewardSelect", "Choose Your Quest Reward", "Select Your Quest Reward for `Bring The Cheer`.", RewardsSelection.All)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.BankingBlackList.AddRange(Rewards);
        QuestItems();

        Core.SetOptions(false);
    }

    public void QuestItems(RewardsSelection reward = RewardsSelection.All)
    {
        if (!Core.isSeasonalMapActive("frostvalfuture"))
        {
            Core.Logger($"it is Currently {DateTime.Now.ToString("MMMM")}, The Maps Will Be out In December, as per the Design Notes.");
            return;
        }

        Frostvale.FrostvalPastPresentandFuture();

        var RewardOptions = Core.EnsureLoad(6651).Rewards.Select(x => x.Name).ToArray();
        Core.AddDrop(RewardOptions);
        bool getAll = (int)Bot.Config.Get<RewardsSelection>("RewardSelect") == 9999;

        ItemBase item = null;
        if (!getAll)
        {
            item = Core.EnsureLoad(6651).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));
            if (item == null)
            {
                Core.Logger($"{item.Name} not found in Quest Rewards");
                return;
            }
            if (Core.CheckInventory(item.Name))
                return;
            Core.FarmingLogger(item.Name, 1);
        }
        
        Core.EquipClass(ClassType.Solo);
        
        while (getAll ? !Core.CheckInventory(RewardOptions) : !Core.CheckInventory(item.Name))
        {
            Core.EnsureAccept(6651);
            Core.HuntMonster("frostvalfuture", "Wargoth the Frozen", "Wrapped Parcel", log: false);
            Core.HuntMonster("frostvalpresent", "Time Wraith", "Time Ribbon", log: false);
            Core.HuntMonster("frostvalnext", "Xanta Claus", "Burning Bow", log: false);
            Core.HuntMonster("frostvalpast", "Ice Master Yeti", "Yeti Fur", log: false);

            if (!getAll)
                Core.EnsureComplete(6651, item.ID);
            else Core.EnsureCompleteChoose(6651);
        }
    }

    public readonly string[] Rewards ={
    "CheerCaster Armor", "Cheery Hat", "Cheery Locks", "Cheery Hat + Scarf",
    "Cheery Locks + Scarf", "Stars of Joy", "Stars of the North",
    "Wand of Joy", "Dark CheerCaster", "Dark CheerCaster Hat",
    "Dark CheerCaster Hat + Locks", "Dark CheerCaster Accessories",
    "Dark CheerCaster Accessories + Locks", "Midnight CheerCaster Stars",
    "Dark CheerCaster Wand"};

    public enum RewardsSelection
    {
        All = 9999,
        CheerCaster_Armor = 46144,
        Cheery_Hat = 46145,
        Cheery_Locks = 46146,
        Cheery_Hat_Scarf = 46147,
        Cheery_Locks_Scarf = 46148,
        Stars_of_Joy = 46149,
        Stars_of_the_North = 46150,
        Wand_of_Joy = 46151,
        Dark_CheerCaster = 57791,
        Dark_CheerCaster_Hat = 57792,
        Dark_CheerCaster_Hat_Locks = 57793,
        Dark_CheerCaster_Accessories = 57794,
        Dark_CheerCaster_Accessories_Locks = 57795,
        Midnight_CheerCaster_Stars = 57796,
        Dark_CheerCaster_Wand = 57797,
    };
}