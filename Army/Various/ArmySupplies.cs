//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SuppliesWheelArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreArmyLite Army = new();
    public CoreNation Nation = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArmySupplies";
    public int i = 0;

    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.Add("Relic of Chaos");

        Core.SetOptions();
        bot.Options.RestPackets = false;

        ArmySupplies();

        Core.SetOptions(false);
    }

    // public string[] SuppliesArmy =
    // {
    //     "Tainted Gem",
    //     "Dark Crystal Shard",
    //     "Diamond of Nulgath",
    //     "Voucher of Nulgath",
    //     "Voucher of Nulgath (non-mem)",
    //     "Unidentified 10",
    //     "Unidentified 13",
    //     "Gem of Nulgath",
    //     "Relic of Chaos"
    // };
    // public void handler(ItemBase item, bool addedToInv, int quantityNow)
    // {
    //     if (item.Name == "Relic of Chaos")
    //         {
    //             Core.ChainComplete(Bot.Quests.IsUnlocked(555) ? 555 : 2857);
    //             Core.Logger($"Quest completed x{i} times");
    //         }
    // }
    public void ArmySupplies()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(Nation.bagDrops);
        Core.AddDrop("Relic of Chaos");
        while (!Bot.ShouldExit && !Core.CheckInventory("Relic of Chaos", 14))
            ArmyHydra90("hydrachallenge", "h90", "Left", "*");
        Bot.Options.AggroMonsters = false;
    }

    public void ArmyHydra90(string map, string cell, string pad, string monster)
    {
        Core.Join(map, cell, pad);
        while ((cell != null && Bot.Map.CellPlayers.Count() > 0 ? Bot.Map.CellPlayers.Count() : Bot.Map.PlayerCount) < Bot.Config.Get<int>("armysize"))
        {
            Bot.Options.AggroMonsters = false;
            if (Bot.Player.Cell != "Enter")
                Core.Jump("Enter");
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(2000);
        }

        if (Bot.Player.Cell != cell)
            Core.Jump(cell, pad);

        while (Bot.Inventory.Contains("Relic of Chaos"))
        {
            Bot.Options.AggroMonsters = true;
            Bot.Combat.Attack(monster);
            Core.ChainComplete(Bot.Quests.IsUnlocked(555) ? 555 : 2857);
            i++;
            Core.Logger($"Quest completed x{i} times");
        }

        Bot.Options.AggroMonsters = true;
        Bot.Quests.EnsureAccept(Bot.Quests.IsUnlocked(555) ? 555 : 2857);
        Bot.Kill.Monster(monster);
    }
}
