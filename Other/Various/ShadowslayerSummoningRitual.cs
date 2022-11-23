//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/GiantTaleStory.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Story/ShadowSlayerK.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class ShadowslayerSummoningRitual
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDailies Daily = new();
    public CoreAdvanced Adv = new();
    public ShadowSlayerK ShadowStory = new();
    public Core7DD DD = new();
    public BuyScrolls Scroll = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ShadowStory.Storyline();
        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(8835).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        int count = 0;
        Core.CheckSpaces(ref count, Rewards);

        ShadowSlayersApprentice();

        Core.RegisterQuests(8835);
        foreach (string item in Rewards)
        {
            if (!Core.CheckInventory(item, toInv: false))
            {
                Core.Logger($"Getting {item}. Rewards Left: {Rewards.Count() - count} more item" + ((Rewards.Count() - count) > 1 ? "s" : ""));

                Scroll.BuyScroll(Scrolls.SpiritRend, 30);
                Scroll.BuyScroll(Scrolls.Eclipse, 15);
                Scroll.BuyScroll(Scrolls.BlessedShard, 30);
                if (!Core.CheckInventory("Meat Ration"))
                {
                    Core.AddDrop("Meat Ration");
                    Core.EnsureAccept(8263);
                    Core.HuntMonster("cellar", "GreenRat", "Green Mystery Meat", 10);
                    Core.EnsureComplete(8263);
                    Bot.Wait.ForPickup("Meat Ration");
                }
                if (!Core.CheckInventory("Grain Ration", 2))
                {
                    Core.AddDrop("Grain Ration");
                    Core.EnsureAccept(8264);
                    Core.HuntMonster("castletunnels", "Blood Maggot", "Bundle of Rice", 3);
                    Core.EnsureComplete(8264);
                    Bot.Wait.ForPickup("Grain Ration");
                }
                if (!Core.CheckInventory("Dairy Ration"))
                {
                    Core.AddDrop("Dairy Ration");
                    Core.EnsureAccept(8265);
                    Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro", "Bone Hurt Juice", 5);
                    Core.EnsureComplete(8265);
                    Bot.Wait.ForPickup("Dairy Ration");
                }
                Core.ToBank(item);
            }
        }
        Core.CancelRegisteredQuests();
    }

    void ShadowSlayersApprentice()
    {
        if (!Core.CheckInventory("ShadowSlayer's Apprentice"))
        {
            Core.FarmingLogger("ShadowSlayer's Apprentice", 1);
            Core.AddDrop("Shadowslayer Apprentice Badge");
            Core.HuntMonster("chaosbeast", "Kathool", "Chibi Eldritch Yume", isTemp: false);
            Core.EnsureAccept(8266);
            Daily.EldersBlood();
            if (!Core.CheckInventory("Holy Wasabi"))
            {
                Core.AddDrop("Holy Wasabi");
                Core.EnsureAccept(1075);

                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("doomwood", "Doomwood Ectomancer", "Dried Wasabi Powder", 4);
                Core.GetMapItem(428, 1, "lightguard");

                Core.EnsureComplete(1075);
                Bot.Wait.ForPickup("Holy Wasabi");
            }
            Adv.BuyItem("alchemyacademy", 2036, "Sage Tonic", 3);
            DD.HazMatSuit();
            Core.HuntMonster("sloth", "Phlegnn", "Unnatural Ooze", 8);
            Core.HuntMonster("beehive", "Killer Queen Bee", "Sleepy Honey");
            Core.EnsureComplete(8266);
            Core.BuyItem("safiria", 2044, "ShadowSlayer's Apprentice");
        }
    }
}