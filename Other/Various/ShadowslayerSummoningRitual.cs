/*
name: ShadowslayerSummoningRitual
description: null
tags: null
*/
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

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll(bool MovetoQuest2 = false, string? itemFarm = null)
    {
        ShadowStory.Storyline();

        List<ItemBase> RewardOptions = Core.EnsureLoad(8835).Rewards;
        List<string> RewardsList = new();
        foreach (ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        int count = 0;
        Core.CheckSpaces(ref count, Rewards);

        ShadowSlayersApprentice();

        // If itemFarm is not null, adjust the Rewards array to contain only the specified item
        if (!string.IsNullOrEmpty(itemFarm))
        {
            Rewards = new string[] { itemFarm };
        }

        foreach (string item in Rewards)
        {
            // Check if we need to move to Quest 2 or if the item is already in the inventory
            if ((!MovetoQuest2 && Core.CheckInventory(item, toInv: false))
                || (MovetoQuest2 && Core.CheckInventory("Sparkly Shadowslayer Relic")))
            {
                continue;
            }

            // Loop to acquire the item or relic
            while (!Bot.ShouldExit
                && ((!MovetoQuest2 && !Core.CheckInventory(item, toInv: false))
                    || (MovetoQuest2 && !Core.CheckInventory("Sparkly Shadowslayer Relic"))))
            {
                Core.Logger($"Getting {item}. Rewards Left: {Rewards.Length - count} more item" + ((Rewards.Length - count) > 1 ? "s" : ""));
                Core.EnsureAccept(8835);

                Scroll.BuyScroll(Scrolls.SpiritRend, 30);
                Scroll.BuyScroll(Scrolls.Eclipse, 15);
                Scroll.BuyScroll(Scrolls.BlessedShard, 30);

                Core.AddDrop("Meat Ration", "Grain Ration", "Dairy Ration");

                while (!Bot.ShouldExit && !Core.CheckInventory("Meat Ration"))
                {
                    Core.EnsureAccept(8263);
                    Core.HuntMonster("cellar", "GreenRat", "Green Mystery Meat", 10);
                    Core.EnsureComplete(8263);
                    Bot.Wait.ForPickup("Meat Ration");
                }

                while (!Bot.ShouldExit && !Core.CheckInventory("Grain Ration", 2))
                {
                    Core.EnsureAccept(8264);
                    Core.KillMonster("castletunnels", "r5", "Left", "Blood Maggot", "Bundle of Rice", 3);
                    Core.EnsureComplete(8264);
                    Bot.Wait.ForPickup("Grain Ration");
                }

                while (!Bot.ShouldExit && !Core.CheckInventory("Dairy Ration"))
                {
                    Core.EnsureAccept(8265);
                    Core.KillMonster("odokuro", "Boss", "Right", "O-dokuro", "Bone Hurt Juice", 5);
                    Core.EnsureComplete(8265);
                    Bot.Wait.ForPickup("Dairy Ration");
                }

                Core.EnsureComplete(8835);
                Core.ToBank(item);
            }

            // Abandon unnecessary quests
            int[] AbandonThese = { 2309, 2317, 8263, 8264, 8265, 8835 };
            Bot.Quests.UnregisterQuests(AbandonThese);
            Core.AbandonQuest(AbandonThese);
        }
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
