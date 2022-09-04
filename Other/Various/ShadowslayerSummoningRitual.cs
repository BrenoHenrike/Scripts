//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/Table.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Story/ShadowSlayerK.cs
using Skua.Core.Interfaces;

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

        GetAll();

        Core.SetOptions(false);
    }
    private string[] rewards =
        {
            "Ardent Familiar Raiment",
            "Ardent Familiar Visage",
            "Auspicious Arch Mammona Raiment",
            "Auspicious Arch Mammona Visage",
            "Dark Infernal Raiment",
            "Dark Infernal Visage",
            "Maid Lilim Raiment",
            "Maid Lilim Visage",
            "Maid Lilim Familiar",
            "Mail Lilim Combat Companion",
            "Maid Lilim Companion",
            "Eldritch Malarkey",
            "Eldritch Malarkeys",
            "Sparkly Shadowslayer Relic",
            "Shadowslayer Relic Armblade",
            "Shadowslayer Relic Armblades",
            "Shadowslayer Relic Bow",
            "Shadowslayer Relic Shield",
            "Shadowslayer Relic Spear",
            "Shadowslayer Relic Sword",
            "Shadowslayer Estoc"
        };

    public void GetAll()
    {
        if (Core.CheckInventory(rewards))
            return;

        int count = 0;
        Core.CheckSpaces(ref count, rewards);
        Core.AddDrop(rewards);
        ShadowStory.Storyline();

        if (!Core.CheckInventory("ShadowSlayer's Apprentice"))
        {
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
            Adv.BuyItem("alchemyacademy", 2036, "Sage Tonic", 3, 10);
            DD.HazMatSuit();
            Core.HuntMonster("sloth", "Phlegnn", "Unnatural Ooze", 8);
            Core.HuntMonster("beehive", "Killer Queen Bee", "Sleepy Honey");
            Core.EnsureComplete(8266);
            Core.BuyItem("safiria", 2044, "ShadowSlayer's Apprentice");
        }
        Core.RegisterQuests(8835);
        Bot.Events.ItemDropped += ItemDropped;
        Core.Logger($"Farm for the Shadowslayer Summoning Ritual items has started. Farming to get {rewards.Count() - count} more item" + ((rewards.Count() - count) > 1 ? "s" : ""));

        while (!Core.CheckInventory(rewards))
        {
            Scroll.BuyScroll(BuyScrolls.Scrolls.SpiritRend, 30);
            Scroll.BuyScroll(BuyScrolls.Scrolls.Eclipse, 15);
            Scroll.BuyScroll(BuyScrolls.Scrolls.BlessedShard, 30);
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
                Core.HuntMonster("odokuro", "O-dokuro", "Bone Hurt Juice", 5);
                Core.EnsureComplete(8265);
                Bot.Wait.ForPickup("Dairy Ration");
            }
        }

        Bot.Events.ItemDropped -= ItemDropped;

        void ItemDropped(ItemBase item, bool addedToInv, int quantityNow)
        {
            if (rewards.Contains(item.Name))
            {
                count++;
                Core.Logger($"Got {item.Name}, {rewards.Length - count} items to go");
            }
        }
    }



    //INSERT CODE HERE      
}

