/*
name: Supplies Wheel Army
description: uses an army to farm the "supplies to spin the wheen of chance" quest.
tags: nulgath, supplies to spin the wheels, army, reagents
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class SuppliesWheelArmy
{
    public static IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();
    public CoreNation Nation = new();

    private static readonly CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmySupplies";

    public List<IOption> Options = new()
{
    new Option<Cell>("mob", "h90 or h85", "h90 for more relic turn-ins, but more chance of getting stuck due to deaths - h85 for just Relics from Escherion", Cell.h90),
    new Option<bool>("sellToSync", "Sell to Sync", "Sell \"Relic of Chaos\" to make sure the army stays synchronized. If off, there is a higher chance your army might desynchronize", false),
    new Option<bool>("SwindlesReturnDuring", "Do Swindles Return", "Accept the Swindles Returns items, and goes to kill a makai for the rune, during the quest.", false),
    new Option<bool>("BloodyChaos", "Do Bloody Chaos", "Accept and complete the 'Bloody Chaos' quest for 'Blood Gem of the Archfiend'", false),
    sArmy.player1,
    sArmy.player2,
    sArmy.player3,
    sArmy.player4,
    sArmy.player5,
    sArmy.player6,
    sArmy.player7,
    sArmy.packetDelay,
    CoreBots.Instance.SkipOptions
};


    private readonly string[] SuppliesRewards =
      {
    "Tainted Gem",
    "Dark Crystal Shard",
    "Diamond of Nulgath",
    "Voucher of Nulgath",
    "Voucher of Nulgath (non-mem)",
    "Gem of Nulgath",
    "Unidentified 10"
    };

    private readonly string[] SwindlesReturnRewards =
    {
        "Tainted Gem",
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Gem of Nulgath",
        "Blood Gem of the Archfiend",
        "Receipt of Swindle"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.Add("Relic of Chaos");

        Core.SetOptions();

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");
        Core.OneTimeMessage("Dark Makai Rune/Sigil Solution", "Randomizing location for \"Dark Makai\"\n" +
        "as the drop can randomly stop showing up", forcedMessageBox: false);
        ArmySupplies();

        Core.SetOptions(false);
    }

    public void ArmySupplies()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("SwindlesReturnDuring"))
        {
            Core.AddDrop(Nation.SwindlesReturn);
            Core.AddDrop(Nation.SwindlesReturnRewards);
        }

        Core.AddDrop(Nation.SuppliesRewards);
        Core.AddDrop("Relic of Chaos");

        bool doBloodyChaos = Bot.Config!.Get<bool>("BloodyChaos");
        bool doSwindlesReturnDuring = Bot.Config!.Get<bool>("SwindlesReturnDuring");

        int[] questIDs = doBloodyChaos
            ? (doSwindlesReturnDuring ? new[] { 2857, 7551, 7816 } : new[] { 2857, 7816 })
            : (doSwindlesReturnDuring ? new[] { 2857, 7551 } : new[] { 2857 });

        Quest[] QuestData = questIDs.Select(id => Core.EnsureLoad(id)).ToArray();

        if (doSwindlesReturnDuring)
        {
            Core.Logger("Setting it so the Dark Makai rune can drop... why is aes s*t so broke.");
            Core.ResetQuest(7551);
        }

        foreach (Quest quest in QuestData)
        {
            ItemBase[] QuestReward = quest.Rewards
                .Where(item => SuppliesRewards.Contains(item.Name) || SwindlesReturnRewards.Contains(item.Name))
                .ToArray();

            foreach (ItemBase item in QuestReward)
            {
                Core.FarmingLogger(item.Name, item.MaxStack);

                while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, item.MaxStack))
                    ArmyHydra(item.Name, item.MaxStack);
            }
            Army.waitForParty("whitemap");
        }
    }

    void ArmyHydra(string item, int quant)
    {
        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop("Relic of Chaos", "Hydra Scale Piece");
        Core.AddDrop(item);


        bool doBloodyChaos = Bot.Config!.Get<bool>("BloodyChaos");

        Core.RegisterQuests(2857);
        Core.FarmingLogger(item, quant);
        Core.EquipClass(ClassType.Farm);
        bool AggroSet = false;
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            if (!AggroSet)
            {
                SetHydraAggro();
                AggroSet = true;
            }

            Bot.Combat.Attack(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "Hydra Head 85" : "Hydra Head 90");

            Core.Sleep();

            if (Core.CheckInventory(Nation.SwindlesReturn) && Bot.Config!.Get<bool>("SwindlesReturnDuring"))
            {
                Army.AggroMonStop(true);
                Core.JumpWait();
                Core.ResetQuest(7551);
                while (!Bot.ShouldExit && !Core.CheckInventory("Dark Makai Rune"))
                {
                    // Define the maps with their corresponding indexes
                    var maps = new[] { ("tercessuinotlim", "m1"), (Core.IsMember ? "Nulgath" : "evilmarsh", "Field1") };

                    // Randomly select a map
                    var randomMapIndex = new Random().Next(0, maps.Length);
                    var selectedMap = maps[randomMapIndex];

                    Core.Join(selectedMap.Item1, selectedMap.Item2, "Left");

                    while (!Bot.ShouldExit &&
                        (selectedMap.Item1 == "tercessuinotlim"
                            ? (Core.IsMonsterAlive(2, useMapID: true) || Core.IsMonsterAlive(3, useMapID: true))
                            : (Core.IsMonsterAlive(1, useMapID: true) || Core.IsMonsterAlive(2, useMapID: true))))
                    {
                        if (!Bot.Player.InCombat)
                            Core.Sleep();  // Use the built-in delay
                        Bot.Combat.Attack("*");
                        if (Core.CheckInventory("Dark Makai Rune"))
                            break;
                    }
                }

                Bot.Wait.ForPickup("Dark Makai Rune");

                var ReturnRewards = Core.EnsureLoad(7551).Rewards;
                ItemBase? ReturnRewardsItem = ReturnRewards.Find(x => x.Name == item);

                if (ReturnRewards.Any(reward => reward.Name == item && reward.Name != "Receipt of Swindle"))
                    Core.EnsureCompleteChoose(7551, new[] { ReturnRewardsItem!.Name });
                else
                    Core.EnsureCompleteChoose(7551, Core.QuestRewards(7551));
                AggroSet = false;
            }

            if (doBloodyChaos && Core.CheckInventory("Hydra Scale Piece", 200))
            {
                Army.AggroMonStop(true);
                Core.JumpWait();
                BloodyChaos();
                AggroSet = false;
            }

        }
        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    void SetHydraAggro()
    {
        Army.AggroMonCells(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "h85" : "h90");
        Army.AggroMonStart("hydrachallenge");
        Army.DivideOnCells(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "h85" : "h90");
    }

    private static void BloodyChaos()
    {
        if (Core.CheckInventory("Blood Gem of the Archfiend", 100))
            return;

        Core.EnsureAccept(7816);
        Core.KillVath("Shattered Legendary Sword of Dragon Control", isTemp: false);
        Core.KillEscherion("Escherion's Helm");
        Core.EnsureComplete(7816);
    }

    public enum Cell
    {
        h90 = 0,
        h85 = 1
    }
}
