//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs

using Skua.Core.Interfaces;

public class CoreNation
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    //CanChange: If enabled will sell the "Voucher of Nulgath" item during farms if it's not needed.
    bool sellMemVoucher = true;
    //CanChange: If enabled will do "Swindles Return Policy" passively during "Supplies To Spin The Wheels of Fate".
    bool returnPolicyDuringSupplies = true;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    /// <summary>
    /// Crag & Bamboozle name in game
    /// </summary>
    public string CragName => "Crag &amp; Bamboozle";

    /// <summary>
    /// All principal drops from Nulgath
    /// </summary>
    public string[] bagDrops =
    {
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Gem of Nulgath",
        "Tainted Gem",
        "Unidentified 10",
        "Unidentified 13",
        "Unidentified 24",
        "Voucher of Nulgath",
        "Voucher of Nulgath (non-mem)",
        "Essence of Nulgath",
        "Unidentified 25",
        "Totem of Nulgath",
        "Fiend Token",
        "Blood Gem of the Archfiend",
        "Emblem of Nulgath",
        "Receipt of Swindle",
        "Bone Dust",
        "Nulgath's Approval",
        "Archfiend's Favor",
        "Unidentified 34"
    };

    /// <summary>
    /// Drops from the bosses that used to give acess to tercess
    /// </summary>
    public string[] tercessBags = { "Bone Dust" };

    /// <summary>
    /// List of Betrayal Blades
    /// </summary>
    public string[] betrayalBlades =
    {
        "1st Betrayal Blade of Nulgath",
        "2nd Betrayal Blade of Nulgath",
        "3rd Betrayal Blade of Nulgath",
        "4th Betrayal Blade of Nulgath",
        "5th Betrayal Blade of Nulgath",
        "6th Betrayal Blade of Nulgath",
        "7th Betrayal Blade of Nulgath",
        "8th Betrayal Blade of Nulgath"
    };

    /// <summary>
    /// Shadow Blast Arena medals
    /// </summary>
    public string[] nationMedals =
    {
        "Nation Round 1 Medal",
        "Nation Round 2 Medal",
        "Nation Round 3 Medal",
        "Nation Round 4 Medal"
    };

    public string[] Receipt =
    {
        "Unidentified 1",
        "Unidentified 6",
        "Unidentified 9",
        "Unidentified 16",
        "Unidentified 20",
        "Receipt of Swindle",
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Gem of Nulgath",
        "Blood Gem of the Archfiend"
    };

    /// <summary>
    /// Misc items to accept during Bloody Chaos if turned on
    /// </summary>
    public string[] BloodyChaosSupplies =
    {
        "Tainted Gem",
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Voucher of Nulgath",
        "Voucher of Nulgath (non-mem)",
        "Unidentified 10",
        "Unidentified 13",
        "Gem of Nulgath",
        "Relic of Chaos"
    };

    public string Uni(int nr)
        => $"Unidentified {nr}";

    /// <summary>
    /// Does Essence of Defeat Reagent quest for Dark Crystal Shards
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void EssenceofDefeatReagent(int quant = 1000)
    {
        if (Core.CheckInventory("Dark Crystal Shard", quant))
            return;

        Core.AddDrop(tercessBags);
        Core.AddDrop(bagDrops);
        int i = 1;
        Core.Logger($"Farming {quant} Dark Crystal Shard");

        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Crystal Shard", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(570);
            Core.HuntMonster("faerie", "Aracara", "Aracara's Fang", 1, false, log: false);
            Core.HuntMonster("hydra", "Hydra Head", "Hydra Scale", 1, false, log: false);
            if (!Core.CheckInventory("Strand of Vath's Hair"))
            {
                Core.Join("stalagbite", "r2", "Left");
                Bot.Kill.Monster("Vath");
                Core.JumpWait();
                if (Bot.Drops.Exists("Strand of Vath's Hair"))
                    Bot.Drops.Pickup("Strand of Vath's Hair");
            }
            Core.HuntMonster("yokaiwar", "O-Dokuro's Head", "O-dokuro's Tooth", 1, false, log: false);
            Core.KillEscherion("Escherion's Chain", publicRoom: true);
            if (!Core.CheckInventory("Defeated Makai", 50))
            {
                Core.EquipClass(ClassType.Farm);
                Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Defeated Makai", 50, false, log: false);
                Core.JumpWait();
            }
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("djinn", "Tibicenas", "Tibicenas' Chain", publicRoom: true, log: false);
            Core.EnsureComplete(570);
            Bot.Wait.ForPickup("Dark Crystal Shard");
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.IsMaxStack("Dark Crystal Shard"))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"Dark Crystal Shard: {Bot.Inventory.GetQuantity("Dark Crystal Shard")}/{quant}");
        }
    }

    /// <summary>
    /// Does NWNO from Nulgath's Birthday Gift/Bounty Hunter's Drone Pet
    /// </summary>
    /// <param name=item>Desired item to get</param>
    /// <param name="quant">Desired quantity to get</param>
    public void NewWorldsNewOpportunities(string item = "Any", int quant = 1)
    {
        if (Core.CheckInventory(item, quant) || (!Core.CheckInventory("Nulgath's Birthday Gift") && !Core.CheckInventory("Bounty Hunter's Drone Pet")))
            return;

        if (item != "Any")
        {
            Core.AddDrop(item);
            Core.FarmingLogger(item, quant);
        }
        Core.AddDrop(bagDrops);

        Core.RegisterQuests(Core.CheckInventory("Bounty Hunter's Drone Pet") ? 6183 : 6697);
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant) && !Bot.Inventory.IsMaxStack(item))
        {
            if (!Core.CheckInventory("Slugfit Horn", 5) || !Core.CheckInventory("Cyclops Horn", 3))
            {
                Core.JoinSWF("mobius", "ChiralValley/town-Mobius-21Feb14.swf");
                Core.HuntMonster("mobius", "Slugfit", "Slugfit Horn", 5, log: false);
                Core.HuntMonster("mobius", "Cyclops Warlord", "Cyclops Horn", 3, log: false);
            }
            Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Makai Fang", 5, log: false);
            Core.HuntMonster("hydra", "Fire Imp", "Imp Flame", 3, log: false);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Wereboar Tusk", 2, log: false);

            if (item != "Any")
                Bot.Wait.ForPickup(item);

            Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Farm Diamonds from Evil War Nul quests (does Member one if possible)
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void DiamondEvilWar(int quant = 1000)
    {
        if (Core.CheckInventory("Diamond of Nulgath", quant))
            return;

        Core.AddDrop("Legion Blade", "Dessicated Heart", "Diamond of Nulgath");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Diamonds");
        int i = 1;
        Core.Join("evilwarnul");

        while (!Bot.ShouldExit && !Core.CheckInventory("Diamond of Nulgath", quant))
        {
            if (Core.IsMember)
                Core.EnsureAccept(2221);
            else
                Core.EnsureAccept(2219);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Legion Blade", 1, false, log: false);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Dessicated Heart", 22, false, log: false);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Legion Helm", 5, log: false);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Undead Skull", 3, log: false);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Legion Champion Medal", 5, log: false);
            if (Core.IsMember)
                Core.EnsureComplete(2221);
            else
                Core.EnsureComplete(2219);
            Bot.Drops.Pickup("Diamond of Nulgath");
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.IsMaxStack("Diamond of Nulgath"))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"Diamond of Nulgath: {Bot.Inventory.GetQuantity("Diamond of Nulgath")}/{quant}");
        }
    }

    /// <summary>
    /// Farms Approvals and Favors in Evil War Nul
    /// </summary>
    /// <param name="quantApproval">Desired quantity for Approvals, 5000 = max stack</param>
    /// <param name="quantFavor">Desired quantity for Favors, 5000 = max stack</param>
    public void ApprovalAndFavor(int quantApproval = 5000, int quantFavor = 5000)
    {
        if (Core.CheckInventory("Nulgath's Approval", quantApproval) && Core.CheckInventory("Archfiend's Favor", quantFavor))
            return;

        Core.AddDrop("Nulgath's Approval", "Archfiend's Favor");

        bool shouldLog = true;
        if (quantApproval > 0 && quantFavor > 0)
        {
            Core.Logger($"Farming Nulgath's Approval ({Bot.Inventory.GetQuantity("Nulgath's Approval")}/{quantApproval}) " +
                            $"and Archfiend's Favor ({Bot.Inventory.GetQuantity("Archfiend's Favor")}/{quantFavor})");
            shouldLog = false;
        }

        Core.EquipClass(ClassType.Farm);

        Core.KillMonster("evilwarnul", "r2", "Down", "*", "Nulgath's Approval", quantApproval, false, shouldLog);
        Core.KillMonster("evilwarnul", "r2", "Down", "*", "Archfiend's Favor", quantFavor, false, shouldLog);
    }


    /// <summary>
    /// Farms specific item with Swindles Return Policy quest
    /// </summary>
    /// <param name="quant">Desired Item quantity</param>
    /// <param name=item>Desired Item</param>
    public void SwindleReturn(string item = "Any", int quant = 1000)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(bagDrops);
        Core.AddDrop(Receipt);

        if (Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher))
            sellMemVoucher = _sellMemVoucher;

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EnsureAccept(7551);
            Supplies("Unidentified 1");
            Supplies("Unidentified 6");
            Supplies("Unidentified 9");
            Supplies("Unidentified 16");
            Supplies("Unidentified 20");
            Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Rune", log: false);
            switch (item)
            {
                case "Dark Crystal Shard":
                    Core.EnsureComplete(7551, 4770);
                    break;
                case "Diamond of Nulgath":
                    Core.EnsureComplete(7551, 4771);
                    break;
                case "Gem of Nulgath":
                    Core.EnsureComplete(7551, 6136);
                    break;
                case "Blood Gem of the Archfiend":
                    Core.EnsureComplete(7551, 22332);
                    break;
                default: //Tainted Gem
                    Core.EnsureComplete(7551, 4769);
                    break;
            }
            if (item != "Voucher of Nulgath" && sellMemVoucher)
                Core.SellItem("Voucher of Nulgath", all: true);
            if (Bot.Inventory.IsMaxStack(item))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
    }

    /// <summary>
    /// Farms Tainted Gem with Swindle Bulk quest
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void SwindleBulk(int quant = 1000)
    {
        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Tainted Gems");
        int i = 1;
        Core.AddDrop("Cubes", "Tainted Gem");
        Core.AddDrop(bagDrops);

        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.EnsureAccept(quant % 25 == 0 ? 7817 : 569);
            Core.KillMonster("boxes", "Fort2", "Left", "*", "Cubes", quant % 25 == 0 ? 500 : 25, false, log: false);
            Core.KillMonster("mountfrost", "War", "Left", "Snow Golem", "Ice Cubes", quant % 25 == 0 ? 6 : 1, log: false);
            Core.EnsureComplete(quant % 25 == 0 ? 7817 : 569);
            Bot.Drops.Pickup("Tainted Gem");
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.IsMaxStack("Tainted Gem"))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"Tainted Gem: {Bot.Inventory.GetQuantity("Tainted Gem")}/{quant}");
        }
    }

    /// <summary>
    /// Farms Emblem of Nulgath in Shadow Blast Arena
    /// </summary>
    /// <param name="quant">Desired quantity, 500 = max stack</param>
    public void EmblemofNulgath(int quant = 500)
    {
        if (Core.CheckInventory("Emblem of Nulgath", quant))
            return;

        if (!Core.CheckInventory("Nation Round 4 Medal"))
            NationRound4Medal();

        Core.AddDrop("Fiend Seal", "Gem of Domination", "Emblem of Nulgath");
        Core.AddDrop(bagDrops);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Emblems");

        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory("Emblem of Nulgath", quant))
        {
            Core.EnsureAccept(4748);
            Core.KillMonster("shadowblast", "r13", "Left", "*", "Gem of Domination", 1, false, log: false);
            Core.KillMonster("shadowblast", "r13", "Left", "*", "Fiend Seal", 25, false, log: false);
            Core.EnsureComplete(4748);
            Bot.Drops.Pickup("Emblem of Nulgath");
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.IsMaxStack("Emblem of Nulgath"))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"Emblem of Nulgath: {Bot.Inventory.GetQuantity("Emblem of Nulgath")}/{quant}");
        }
    }

    /// <summary>
    /// Farms Nation Round 4 Medal in Shadow Blast Arena
    /// </summary>
    public void NationRound4Medal()
    {
        if (Core.CheckInventory("Nation Round 4 Medal"))
            return;

        Core.AddDrop(nationMedals);
        Core.Logger("Farming Nation Round 4 Medal");
        Core.Join("shadowblast");

        while (!Bot.ShouldExit && !Core.CheckInventory("Nation Round 4 Medal"))
        {
            if (!Core.CheckInventory("Nation Round 1 Medal") &&
                !Core.CheckInventory("Nation Round 2 Medal") &&
                !Core.CheckInventory("Nation Round 3 Medal"))
            {
                Core.EnsureAccept(4744);
                Core.HuntMonster("shadowblast", "Legion Airstrike", "Legion Rookie Defeated", 5, true, log: false);
                Core.HuntMonster("shadowblast", "Shadowrise Guard", "Shadowscythe Rookie Defeated", 5, true, log: false);
                Core.EnsureComplete(4744);
                Bot.Drops.Pickup("Nation Round 1 Medal");
                Core.Logger("Medal 1 acquired");
            }

            if (Core.CheckInventory("Nation Round 1 Medal"))
            {
                Core.EnsureAccept(4745);
                Core.HuntMonster("shadowblast", "Legion Fenrir", "Legion Veteran Defeated", 7, true, log: false);
                Core.HuntMonster("shadowblast", "Doombringer", "Shadowscythe Veteran Defeated", 7, true, log: false);
                Core.EnsureComplete(4745);
                Bot.Drops.Pickup("Nation Round 2 Medal");
                Core.Logger("Medal 2 acquired");
            }

            if (Core.CheckInventory("Nation Round 2 Medal"))
            {
                Core.EnsureAccept(4746);
                Core.HuntMonster("shadowblast", "Legion Cannon", "Legion Elite Defeated", 10, true, log: false);
                Core.HuntMonster("shadowblast", "Draconic Doomknight", "Shadowscythe Elite Defeated", 10, true, log: false);
                Core.EnsureComplete(4746);
                Bot.Drops.Pickup("Nation Round 3 Medal");
                Core.Logger("Medal 3 acquired");
            }

            if (Core.CheckInventory("Nation Round 3 Medal"))
            {
                Core.EnsureAccept(4747);
                Core.HuntMonster("shadowblast", "Grimlord Boss", "Grimlord Vanquished", 1, true, log: false);
                Core.EnsureComplete(4747);
                Bot.Drops.Pickup("Nation Round 4 Medal");
                Core.Logger("Medal 4 acquired");
            }
        }
    }

    /// <summary>
    /// Farms Totem of Nulgath/Gem of Nulgath with Voucher Item: Totem of Nulgath quest
    /// </summary>
    /// <param name="reward">Which reward to pick (totem or gem)</param>
    public void VoucherItemTotemofNulgath(ChooseReward reward = ChooseReward.TotemofNulgath)
    {
        if (!Core.CheckInventory("Voucher of Nulgath (non-mem)"))
            FarmVoucher(false);

        Core.AddDrop("Gem of Nulgath", "Totem of Nulgath");
        Core.AddDrop(bagDrops);
        Core.Logger($"Reward selected: {reward}");
        Core.EnsureAccept(4778);

        EssenceofNulgath();
        if (!Bot.Quests.CanComplete(4778))
            EssenceofNulgath(65);
        Core.EnsureComplete(4778, (int)reward);
        Bot.Drops.Pickup("Gem of Nulgath", "Totem of Nulgath");
    }

    /// <summary>
    /// Farms Essences of Nulgath from Dark Makais in Tercessuinotlim
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    public void EssenceofNulgath(int quant = 60)
    {
        if (Core.CheckInventory("Essence of Nulgath", quant))
            return;

        Core.AddDrop("Essence of Nulgath");
        Core.EquipClass(ClassType.Farm);

        Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Essence of Nulgath", quant, false, log: false);
    }

    /// <summary>
    /// Does Nulgath Larvae quest for the desired item
    /// </summary>
    /// <param name=item>Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
    public void NulgathLarvae(string item = "Any", int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("Mana Energy for Nulgath");
        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(bagDrops);
        Core.FarmingLogger(item, quant);
        Bot.Quests.UpdateQuest(847);
        Core.RegisterQuests(2566);
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonsterMapID("elemental", 7, "Mana Energy for Nulgath", isTemp: false, log: false);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("elemental", "r3", "Down", "*", "Charged Mana Energy for Nulgath", 5, log: false);
            Bot.Drops.Pickup(item);
            if (Bot.Inventory.IsMaxStack(item))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
    }

    /// <summary>
    /// Does Supplies to Spin the Whell of Chance for the desired item with the best method available
    /// </summary>
    /// <param name=item>Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
    public void Supplies(string item = "Any", int quant = 1, bool voucherNeeded = false)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (Core.CheckInventory(CragName))
            BambloozevsDrudgen(item, quant);
        else
        {
            if (Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher))
                sellMemVoucher = _sellMemVoucher;

            if (item != "Any")
            {
                Core.AddDrop(item);
                if (sellMemVoucher)
                    Core.AddDrop("Voucher of Nulgath");
            }
            else
                Core.AddDrop(bagDrops[..^11]);

            if (Core.CBOBool("Nation_ReturnPolicyDuringSupplies", out bool _returnSupplies))
                returnPolicyDuringSupplies = _returnSupplies;
            string[] rPDSuni = null;
            if (returnPolicyDuringSupplies)
            {
                rPDSuni = new[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20) };
                Core.AddDrop(rPDSuni);
                Core.AddDrop("Blood Gem of Nulgath");
            }

            Core.FarmingLogger(item, quant);

            Core.RegisterQuests(returnPolicyDuringSupplies ? new[] { 2857, 7551 } : new[] { 2857 });
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                if (returnPolicyDuringSupplies && !Core.CheckInventory("Dark Makai Rune"))
                {
                    if (Core.IsMember)
                        Core.HuntMonster("nulgath", "Dark Makai", "Dark Makai Rune", log: false);
                    Core.HuntMonster("tercessuinotlim", "Dark Makai", "Dark Makai Rune", log: false);
                }

                Core.KillEscherion("Relic of Chaos", publicRoom: true, log: false);
                Bot.Drops.Pickup(item);

                if (item != "Voucher of Nulgath" && sellMemVoucher && Core.CheckInventory("Voucher of Nulgath") && !voucherNeeded)
                {
                    Bot.Drops.Pickup("Voucher of Nulgath");
                    Core.SellItem("Voucher of Nulgath", all: true);
                }

                if (Bot.Inventory.IsMaxStack(item))
                {
                    Core.Logger($"Max-Stack for {item} has been reached ({Bot.Inventory.GetItem(item).MaxStack})");
                    break;
                }
            }
            Core.CancelRegisteredQuests();
        }
    }

    /// <summary>
    /// Does The Assistant quest for the desired item
    /// </summary>
    /// <param name=item>Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
    /// <param name="farmGold"></param>
    public void TheAssistant(string item = "Any", int quant = 1, bool farmGold = true)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(bagDrops);
        int i = 1;

        void AssistantLoop()
        {
            if (!Core.CheckInventory("War-Torn Memorabilia"))
            {
                Core.Join("yulgar");
                while (!Bot.ShouldExit && Bot.Player.Gold >= 100000 && !Core.CheckInventory("War-Torn Memorabilia", 5))
                {
                    Bot.Shops.BuyItem(41, "War-Torn Memorabilia");
                    Bot.Wait.ForItemBuy();
                }
            }
            Core.EnsureAccept(2859);
            while (!Bot.ShouldExit && Core.CheckInventory("War-Torn Memorabilia") && !Core.CheckInventory(item, quant))
            {
                Core.ChainComplete(2859);
                Bot.Drops.Pickup(bagDrops);
                Core.Logger($"Completed x{i++}");
                if (Bot.Inventory.IsMaxStack(item))
                    Core.Logger("Max Stack Hit.");
                else Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
            }
        }

        Core.FarmingLogger(item, quant);
        if (farmGold)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                AssistantLoop();
                if (Bot.Player.Gold < 100000 && !Core.CheckInventory(item, quant))
                    Farm.Gold(1000000);
                if (Bot.Inventory.IsMaxStack(item))
                    Core.Logger("Max Stack Hit.");
                else Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
            }
        }
        else
        {
            while (!Bot.ShouldExit && Bot.Player.Gold > 100000)
                AssistantLoop();

            if (!Core.CheckInventory(item, quant))
                Core.Logger($"Couldn't get {item}({quant})");
        }
    }

    /// <summary>
    /// Does Bamblooze vs Drudgen quest for the desired item
    /// </summary>
    /// <param name=item>Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
    public void BambloozevsDrudgen(string item = "Any", int quant = 1)
    {
        if (!Core.CheckInventory(CragName) || Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("Relic of Chaos", "Tainted Core");
        if (item != "Any")
            Core.AddDrop(item);
        else Core.AddDrop(bagDrops);

        bool OBoNPet = (Core.IsMember && Core.CheckInventory("Oblivion Blade of Nulgath")
            & Bot.Inventory.Items.Where(obon => obon.Category == Skua.Core.Models.Items.ItemCategory.Pet
            && obon.Name == "Oblivion Blade of Nulgath").Any());
        if (OBoNPet || Core.CheckInventory("Oblivion Blade of Nulgath Pet (Rare)"))
            Core.AddDrop("Tainted Soul");

        string[] rPDSuni = null;
        if (returnPolicyDuringSupplies)
        {
            rPDSuni = new[]
            {
                "Unidentified 1",
                "Unidentified 6",
                "Unidentified 9",
                "Unidentified 16",
                "Unidentified 20"
            };
            Core.AddDrop(rPDSuni);
            Core.AddDrop("Blood Gem of Nulgath");
        }

        if (Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher))
        {
            Core.Logger("Sell Voucher of Nulgath: true");
            sellMemVoucher = _sellMemVoucher;
        }
        if (Core.CBOBool("Nation_ReturnPolicyDuringSupplies", out bool _returnSupplies))
            returnPolicyDuringSupplies = _returnSupplies;

        if (item != "Any")
            Core.FarmingLogger(item, quant);

        if (Core.CheckInventory("Oblivion Blade of Nulgath Pet (Rare)") && Core.IsMember)
            Core.RegisterQuests(2857, 609, 599);
        else if (OBoNPet)
            Core.RegisterQuests(2857, 609, 2561);
        else
            Core.RegisterQuests(2857, 609);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.HuntMonster("evilmarsh", "Tainted Elemental", log: false);

            if (Core.CheckInventory("Voucher of Nulgath") && item != "Voucher of Nulgath" && sellMemVoucher)
                Core.SellItem("Voucher of Nulgath", all: true);

            if (returnPolicyDuringSupplies && Core.CheckInventory(rPDSuni))
            {
                Core.EquipClass(ClassType.Farm);
                Core.EnsureAccept(7551);

                Core.HuntMonster("Tercessuinotlim", "dark makai", "Dark Makai Rune");

                switch (item)
                {
                    case "Tainted Gem":
                        Core.EnsureComplete(7551, 4769);
                        break;
                    case "Dark Crystal Shard":
                        Core.EnsureComplete(7551, 4770);
                        break;
                    case "Gem of Nulgath":
                        Core.EnsureComplete(7551, 6136);
                        break;
                    case "Blood Gem of the Archfiend":
                        Core.EnsureComplete(7551, 22332);
                        break;
                    default: // Diamond of Nulgath
                        Core.EnsureComplete(7551, 4771);
                        break;
                }
            }

            // if (Core.CheckInventory("Voucher of Nulgath (non-mem)") && item != "Voucher of Nulgath (non-mem)")
            // {
            //     Core.EquipClass(ClassType.Farm);
            //     Core.EnsureAccept(605);

            //     Core.HuntMonster("cloister", "Acornent", "Diamonds of Time", isTemp: false);
            //     Core.HuntMonster("evilmarsh", "Tainted Elemental", "Tainted Rune of Evil");

            //     switch (item)
            //     {
            //         case "Tainted Gem":
            //             Core.EnsureComplete(605, 4769);
            //             break;
            //         case "Dark Crystal Shard":
            //             Core.EnsureComplete(605, 4770);
            //             break;
            //         case "Gem of Nulgath":
            //             Core.EnsureComplete(605, 6136);
            //             break;
            //         case "Blood Gem of the Archfiend":
            //             Core.EnsureComplete(605, 22332);
            //             break;
            //         default: // Diamond of Nulgath
            //             Core.EnsureComplete(605, 4771);
            //             break;
            //     }
            // } //Disabled to to "Diamonds of Time"'s low Drop rate.

            if (item != "Any")
                Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Does the "AssistingDrudgen" Quest for Fiend Tokens (and other possible drops).
    /// Requires either "Drudgen the Assistant" or "Twin Blade of Nulgath" to accept.
    /// </summary>
    /// <param name="farmDiamond">Whether or not farm Diamonds</param>
    public void AssistingDrudgen(string item = "Any", int quant = 1)
    {
        if (Core.CheckInventory(item, quant) || !Core.CheckInventory("Drudgen the Assistant") || !Core.CheckInventory("Twin Blade of Nulgath") || !Bot.Player.IsMember)
            return;

        if (!Bot.Quests.IsAvailable(3826))
        {
            Core.Logger("Quest \"Seal of Light\"[Daily] is not available yet today.");
            return;
        }

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EnsureAccept(5816);
            Core.HuntMonster("willowcreek", "Hidden Spy", "The Secret 1", isTemp: false);
            EssenceofNulgath(20);
            ApprovalAndFavor(50, 50);
            Core.KillMonster("boxes", "Fort2", "Left", "*", "Cubes", 50, false);
            Core.KillMonster("shadowblast", "r13", "Left", "*", "Fiend Seal", 10, false);
            Bot.Quests.UpdateQuest(3824);
            if (Bot.Quests.IsAvailable(3826) && !Core.CheckInventory(25026))
            {
                Core.EnsureAccept(3826);
                Core.HuntMonster("alteonbattle", "*", "Seal of Light");
                Core.EnsureComplete(3826);
            }
            Core.EnsureComplete(5816);
        }
    }

    public void VoidKightSwordQuest(string item = "Any", int quant = 1)
    {
        if (Core.CheckInventory(item, quant) || (!Core.CheckInventory(38275) && !Core.CheckInventory(38254)))
            return;

        Core.AddDrop(bagDrops);
        Core.AddDrop(item);

        if (Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher))
            sellMemVoucher = _sellMemVoucher;

        if (item != "Any")
            Core.FarmingLogger(item, quant);

        Core.RegisterQuests(Core.CheckInventory(38275) ? 5662 : 5659);
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("mobius", "Slugfit", "Slugfit Horn", 5);
            Core.HuntMonster("faerie", "Aracara", "Aracara Silk");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Makai Fang", 5);
            Core.HuntMonster("hydra", "Fire Imp", "Imp Flame", 3);
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Aquamarine of Nulgath", 3, false);

            Bot.Drops.Pickup(bagDrops);
        }
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Do Diamond Exchange quest 1 time, if farmDiamond is true, will farm 15 Diamonds before if needed
    /// </summary>
    /// <param name="farmDiamond">Whether or not farm Diamonds</param>
    public void DiamondExchange(bool farmDiamond = true)
    {
        if ((!Core.CheckInventory("Diamond of Nulgath", 15) && !farmDiamond) || !Core.CheckInventory(CragName))
            return;

        Core.AddDrop("Diamond of Nulgath");

        if (farmDiamond)
            BambloozevsDrudgen("Diamond of Nulgath", 15);
        Core.EnsureAccept(869);
        Core.KillMonster("evilmarsh", "Field1", "Left", "Dark Makai", log: false);
        Core.EnsureComplete(869);
        Core.Logger("Completed");
    }

    /// <summary>
    /// Do Contract Exchange quest 1 time, if <paramref name="farmUni13"/> is true, will farm Uni 13 before if needed
    /// </summary>
    /// <param name="reward">Desired reward</param>
    /// <param name="farmUni13">Whether or not farm Uni 13</param>
    public void ContractExchange(ChooseReward reward = ChooseReward.DiamondofNulgath, bool farmUni13 = true)
    {
        if ((!Core.CheckInventory("Unidentified 13") && !farmUni13) || !Core.CheckInventory("Drudgen the Assistant"))
            return;

        Core.AddDrop(bagDrops);

        if (farmUni13 && !Core.CheckInventory("Unidentified 13"))
            FarmUni13();
        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(870);
        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Blade Master Rune", log: false);
        Core.EnsureComplete(870, (int)reward);
        Bot.Drops.Pickup(bagDrops);
        Core.Logger($"Exchanged for {reward}");
    }

    /// <summary>
    /// Does Swindles Dirt-y Deeds Done Dirt Cheap quest, only use if you have /TowerofDoom10 completed and a good solo class
    /// </summary>
    /// <param name="quant"></param>
    public void DirtyDeedsDoneDirtCheap(int quant = 1000)
    {
        if (Core.CheckInventory("Unidentified 10", quant))
            return;

        Core.AddDrop("Emerald Pickaxe", "Seraphic Grave Digger Spade", "Unidentified 10", "Receipt of Swindle", "Blood Gem of the Archfiend");

        if (!Core.CheckInventory("Emerald Pickaxe"))
            Core.KillEscherion("Emerald Pickaxe", publicRoom: true);

        if (!Core.CheckInventory("Seraphic Grave Digger Spade"))
            Core.KillMonster("legioncrypt", "r1", "Top", "Gravedigger", "Seraphic Grave Digger Spade", 1, false, log: false);
        Core.EquipClass(ClassType.Solo);
        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory("Unidentified 10", quant))
        {
            Core.EnsureAccept(7818);
            Bot.Quests.UpdateQuest(3484);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Slugbutter Digging Advice", publicRoom: true, log: false);
            Core.HuntMonster("crownsreach", "Chaos Tunneler", "Chaotic Tunneling Techniques", 2, log: false);
            Core.HuntMonster("downward", "Crystal Mana Construct", "Crystalized Corporate Digging Secrets", 3, log: false);
            Core.EnsureComplete(7818);
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.IsMaxStack("Unidentified 10"))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"Unidentified 10: {Bot.Inventory.GetQuantity("Unidentified 10")}/{quant}");
        }
    }

    /// <summary>
    /// Farms Unidentified 13 with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 13 = max stack</param>
    public void FarmUni13(int quant = 1)
    {
        if (Core.CheckInventory("Unidentified 13", quant))
            return;

        Core.AddDrop("Unidentified 13");
        quant = quant > 13 ? 13 : quant;

        if (Core.CheckInventory(CragName))
            while (!Bot.ShouldExit && !Core.CheckInventory("Unidentified 13", quant))
                DiamondExchange();
        NewWorldsNewOpportunities("Unidentified 13", quant);
        VoidKightSwordQuest("Unidentified 13", quant);
        NulgathLarvae("Unidentified 13", quant);
    }

    /// <summary>
    /// Farms Unidentified 10 with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void FarmUni10(int quant = 1000)
    {
        if (Core.CheckInventory("Unidentified 10", quant))
            return;

        Core.AddDrop("Unidentified 10");

        BambloozevsDrudgen("Unidentified 10", quant);
        NulgathLarvae("unidentified 10", quant);
    }

    /// <summary>
    /// Farms Dark Crystal Shard with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void FarmDarkCrystalShard(int quant = 1000)
    {
        if (Core.CheckInventory("Dark Crystal Shard", quant))
            return;

        Core.AddDrop("Dark Crystal Shard");
        if (Bot.Player.Gold > 30000000)
            TheAssistant("Gem of Nulgath", quant);
        NewWorldsNewOpportunities("Dark Crystal Shard", quant);
        VoidKightSwordQuest("Dark Crystal Shard", quant);
        Supplies("Dark Crystal Shard", quant);
        EssenceofDefeatReagent(quant);
    }

    /// <summary>
    /// Farms Diamond of Nulgath with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void FarmDiamondofNulgath(int quant = 1000)
    {
        if (Core.CheckInventory("Diamond of Nulgath", quant))
            return;

        Core.AddDrop("Diamond of Nulgath");

        NewWorldsNewOpportunities("Diamond of Nulgath", quant);
        VoidKightSwordQuest("Diamond of Nulgath", quant);
        Supplies("Diamond of Nulgath", quant);
        DiamondEvilWar(quant);
    }

    public void FarmFiendToken(int quant = 30)
    {
        if (Core.CheckInventory("Fiend Token", quant))
            return;

        NewWorldsNewOpportunities("Fiend Token", quant);
        VoidKightSwordQuest("Fiend Token", quant);
        AssistingDrudgen("Fiend Token", quant);
    }


    /// <summary>
    /// Farms Gem of Nulgath with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 300 = max stack</param>
    public void FarmGemofNulgath(int quant = 300)
    {
        if (Core.CheckInventory("Gem of Nulgath", quant))
            return;

        Core.AddDrop("Gem of Nulgath");
        if (Bot.Player.Gold > 30000000)
            TheAssistant("Gem of Nulgath", quant);
        NewWorldsNewOpportunities("Gem of Nulgath", quant);
        VoidKightSwordQuest("Gem of Nulgath", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Gem of Nulgath", quant))
            VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
    }

    /// <summary>
    /// Farms Blood Gem of the Archfiend with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    public void FarmBloodGem(int quant = 100)
    {
        if (Core.CheckInventory("Blood Gem of the Archfiend", quant))
            return;

        Core.AddDrop("Blood Gem of the Archfiend");

        if (Core.CheckInventory("Drudgen the Assistant"))
            while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
                ContractExchange(ChooseReward.BloodGemoftheArchfiend);
        NewWorldsNewOpportunities("Blood Gem of the Archfiend", quant);
        VoidKightSwordQuest("Blood Gem of the Archfiend", quant);
        BloodyChaos(quant, true);
        KisstheVoid(quant);
    }

    /// <summary>
    /// Completes the lair questline to unlock Nation mats if not completed
    /// </summary>
    public void DragonSlayerReward()
    {
        if (Core.isCompletedBefore(169))
            return;

        Core.EquipClass(ClassType.Farm);
        if (!Core.isCompletedBefore(169))
        {
            if (!Core.isCompletedBefore(168))
            {
                if (!Core.isCompletedBefore(167))
                {
                    if (!Core.isCompletedBefore(166))
                    {
                        if (!Core.isCompletedBefore(165))
                        {
                            // 165 - DragonSlayer Veteran
                            Core.EnsureAccept(165);
                            Core.HuntMonster("lair", "Water Draconian", "Dragonslayer Veteran Medal", 8);
                            Core.EnsureComplete(165);
                        }
                        // 166 - DragonSlayer Sergeant
                        Core.EnsureAccept(166);
                        Core.KillMonster("lair", "Hole", "Down", "*", "Dragonslayer Sergeant Medal", 8);
                        Core.EnsureComplete(166);
                    }
                    // 167 - DragonSlayer Captain
                    Core.EnsureAccept(167);
                    Core.KillMonster("lair", "Ledge", "Right", "*", "Dragonslayer Captain Medal", 8);
                    Core.EnsureComplete(167);
                }
                // 168 - DragonSlayer Marshal
                Core.EquipClass(ClassType.Solo);
                Core.EnsureAccept(168);
                Core.HuntMonster("lair", "Red Dragon", "Dragonslayer Marshal Medal", 8);
                Core.EnsureComplete(168);
            }
            // 169 - DragonSlayer Reward
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(169);
            Core.KillMonster("lair", "Hole", "Down", "*", "Wisp of Dragonspirit", 12);
            Core.EnsureComplete(169);
        }
    }

    /// <summary>
    /// Farms Totem of Nulgath with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    public void FarmTotemofNulgath(int quant = 100)
    {
        if (Core.CheckInventory("Totem of Nulgath", quant))
            return;

        Core.AddDrop("Totem of Nulgath");

        NewWorldsNewOpportunities("Totem of Nulgath", quant);
        VoidKightSwordQuest("Totem of Nulgath", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Totem of Nulgath", quant))
            VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);
        if (Bot.Inventory.IsMaxStack("Totem of Nulgath"))
            Core.Logger("Max Stack Hit.");
        else Core.Logger($"Totem of Nulgath: {Bot.Inventory.GetQuantity("Totem of Nulgath")}/{quant}");
    }

    /// <summary>
    /// Farms Voucher of Nulgath (member or not) with the best method available
    /// </summary>
    /// <param name="member">If true will farm Voucher of Nulgath; false Voucher of Nulgath (nom-mem)</param>
    public void FarmVoucher(bool member)
    {
        if ((Core.CheckInventory("Voucher of Nulgath (non-mem)") && !member) || (Core.CheckInventory("Voucher of Nulgath") && member))
            return;

        Core.AddDrop(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");

        BambloozevsDrudgen(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
        NewWorldsNewOpportunities(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
        VoidKightSwordQuest(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
        Supplies(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
    }

    /// <summary>
    /// Do Bloody Chaos quest for Blood Gems
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    public void BloodyChaos(int quant = 100, bool Relic = false)
    {
        if (Core.CheckInventory("Blood Gem Of The Archfiend", quant) || Bot.Player.Level < 80)
            return;

        Core.AddDrop("Blood Gem of the Archfiend", "Hydra Scale Piece");
        if (Relic)
            Core.AddDrop(BloodyChaosSupplies);
        Core.FarmingLogger($"Blood Gem Of The Archfiend", quant);
        Core.RegisterQuests(Relic ? new[] { 7816, 2857 } : new[] { 7816 });
        Bot.Quests.UpdateQuest(363);
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem Of The Archfiend", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.KillEscherion("Escherion's Helm", isTemp: false);
            Core.KillVath("Shattered Legendary Sword of Dragon Control", isTemp: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("hydrachallenge", "Hydra Head 85", "Hydra Scale Piece", 200, false);
        }
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Do Kiss the Void quest for Blood Gems
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    public void KisstheVoid(int quant = 100)
    {
        if (Core.CheckInventory("Blood Gem of the Archfiend", quant))
            return;

        Core.AddDrop("Tendurrr The Assistant", "Fragment of Chaos", "Blood Gem of the Archfiend");
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Blood Gems");
        int i = 1;

        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            Core.EnsureAccept(3743);
            if (!Core.CheckInventory("Tendurrr The Assistant"))
            {
                Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Tendurrr The Assistant", 1, false, log: false);
                Core.JumpWait();
            }
            Core.HuntMonster("lair", "Water Draconian", "Fragment of Chaos", 80, false, log: false);
            Core.KillMonster("evilwarnul", "r13", "Left", "Legion Fenrir", "Broken Betrayal Blade", 8, log: false);
            Core.EnsureComplete(3743);
            Bot.Wait.ForPickup("Blood Gem of the Archfiend");
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.IsMaxStack("Blood Gem of the Archfiend"))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"Blood Gem of the Archfiend: {Bot.Inventory.GetQuantity("Blood Gem of the Archfiend")}/{quant}");
        }
    }

    /// <summary>
    /// [Member] Does Demanding Approval from Nulgath [Quest] to get You Gemstone Receipt of Nulgath with your specific quantities
    /// </summary>
    public void GemStoneReceiptOfNulgath(int quant = 10)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }
        if (Core.CheckInventory("Gemstone Receipt of Nulgath", quant))
            return;
        Core.AddDrop("Gemstone Receipt of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Gemstone Receipt of Nulgath", quant))
        {
            //Demanding Approval from Nulgath [Member] 4917
            Core.EnsureAccept(4917);
            FarmUni13(3);
            while (!Bot.ShouldExit && !Core.CheckInventory("Receipt of Nulgath"))
            {
                //Receipt of Nulgath [Member] 4924
                Farm.VampireREP();
                Core.EnsureAccept(4924);
                Core.BuyItem("Tercessuinotlim", 68, "Blade of Affliction");
                EssenceofNulgath(10);
                ApprovalAndFavor(0, 100);
                Core.HuntMonster("Extinction", "Control Panel", "Coal", 15, isTemp: false);
                Core.RegisterQuests(Core.IsMember ? 4798 : 4797);
                while (!Bot.ShouldExit && !Core.CheckInventory("Dwobo Coin", 10))
                {
                    if (!Core.IsMember)
                        Core.KillMonster("crashruins", "r2", "Left", "Unlucky Explorer", "Ancient Treasure", 10);
                    else Core.KillMonster("crashruins", "r2", "Left", "Unlucky Explorer", "Ancient Treasure", 8);

                    if (!Core.IsMember)
                        Core.KillMonster("crashruins", "r2", "Left", "Spacetime Anomaly", "Pieces of Future Tech", 7);
                    else Core.KillMonster("crashruins", "r2", "Left", "Spacetime Anomaly", "Pieces of Future Tech", 5);

                    Core.HuntMonster("crashruins", "Cluckmoo Idol", "Idol Heart");

                    Bot.Wait.ForPickup("Dwobo Coin");
                }
                Core.CancelRegisteredQuests();
                Core.EnsureComplete(4924);
            }
            FarmVoucher(member: true);
            FarmVoucher(member: false);
            EssenceofNulgath(100);
            FarmTotemofNulgath(1);
            Core.HuntMonster("ShadowfallWar", "Bonemuncher", "Ultimate Darkness Gem", 5, isTemp: false);
            Core.EnsureComplete(4917);
        }
    }

    /// <summary>
    /// [Member] Does Forge Gemstones for Nulgath [Quest] to get You Bloodstone|Quartz|Tanzanite|Unidentified GemStone of Nulgath with your specific quantities
    /// </summary>
    public void GemStonesOfnulgath(int BloodStone = 15, int Quartz = 20, int Tanzanite = 10, int UniGemStone = 1)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }

        FarmUni13(1);
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 4");
        Core.AddDrop("Gem of Nulgath", "Bloodstone of Nulgath", "Quartz of Nulgath", "Tanzanite of Nulgath", "Unidentified Gemstone of Nulgath");
        while (!Bot.ShouldExit && !Core.CheckInventory("Bloodstone of Nulgath", BloodStone)
        && !Core.CheckInventory("Quartz of Nulgath", Quartz)
        && !Core.CheckInventory("Tanzanite of Nulgath", Tanzanite)
        && !Core.CheckInventory("Unidentified Gemstone of Nulgath", UniGemStone))
        {
            //Forge Gemstones for Nulgath [Member] 4918
            Core.EnsureAccept(4918);
            Core.HuntMonster("Twilight", "Abaddon", "Balor's Cruelty", isTemp: false);
            if (!Core.isCompletedBefore(376))
            {
                if (!Core.isCompletedBefore(374))
                {
                    Core.EnsureAccept(374);
                    Core.HuntMonster("battleundera", "Skeletal Warrior", "Yara's Ring");
                    Core.EnsureComplete(374);
                }
                if (!Core.isCompletedBefore(375))
                {
                    Core.EnsureAccept(375);
                    Core.HuntMonster("battleundera", "Skeletal Warrior", "Skeletal Claymore", 6);
                    Core.HuntMonster("battleundera", "Skeletal Warrior", "Bony Chestplate", 3);
                    Core.EnsureComplete(375);
                }
                if (!Core.isCompletedBefore(376))
                {
                    Core.EnsureAccept(376);
                    Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror's Head");
                    Core.EnsureComplete(376);
                }
            }
            while (!Bot.ShouldExit && !Core.CheckInventory("Yara's Sword"))
            {
                Core.AddDrop("Yara's Sword");
                Core.EnsureAccept(377);
                Core.HuntMonster("battleundera", "Skeletal Warrior", "Unidentified Weapon");
                Core.EnsureComplete(377);
            }
            Core.HuntMonster("ShadowfallWar", "Bonemuncher", "Ultimate Darkness Gem", isTemp: false);
            Core.EnsureComplete(4918);
        }
    }

    /// <summary>
    /// [Member] Does Forge Tainted Gems for Nulgath [Quest] to get You Tainted Gems with your specific quantities
    /// </summary>
    public void ForgeTaintedGems(int quant = 1000)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        Core.AddDrop("Tainted Gem", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            //Forge Gemstones for Nulgath [Member] 4918
            Core.EnsureAccept(4919);
            FarmGemofNulgath(1);
            GemStonesOfnulgath(0, 1, 1, 0);
            Core.EnsureComplete(4919);
        }

    }

    /// <summary>
    /// [Member] Does Forge Dark Crystal Shards for Nulgath [Quest] to get You Dark Crystal Shards with your specific quantities
    /// </summary>
    public void ForgeDarkCrystalShards(int quant = 1000)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        Core.AddDrop("Dark Crystal Shards", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Crystal Shards", quant))
        {
            //Forge Dark Crystal Shards for Nulgath [Member] 4920
            Core.EnsureAccept(4920);
            FarmGemofNulgath(1);
            GemStonesOfnulgath(0, 5, 2, 0);
            Core.EnsureComplete(4920);
        }

    }

    /// <summary>
    /// [Member] Does Forge Diamonds for Nulgath [Quest] to get You Diamonds for Nulgath with your specific quantities
    /// </summary>
    public void ForgeDiamondsOfNulgath(int quant = 1000)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        Core.AddDrop("Diamonds for Nulgath", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Diamonds for Nulgath", quant))
        {
            //Forge Diamonds for Nulgath [Member] 4921
            Core.EnsureAccept(4921);
            FarmGemofNulgath(1);
            GemStonesOfnulgath(0, 2, 0, 0);
            Core.EnsureComplete(4921);
        }
    }

    /// <summary>
    /// [Member] Does Forge Blood Gems for Nulgath [Quest] to get You Blood Gem of the Archfiend with your specific quantities
    /// </summary>
    public void ForgeBloodGems(int quant = 100)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        Core.AddDrop("Blood Gem of the Archfiend", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            //Forge Blood Gems for Nulgath [Member] 4922
            Core.EnsureAccept(4922);
            FarmGemofNulgath(7);
            GemStonesOfnulgath(3, 5, 0, 0);
            Core.EnsureComplete(4922);
        }

    }
    public string[] QuestDrops = { "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Gem of Nulgath", "Blood Gem of the Archfiend" };

    /// <summary>
    /// [Member] Does Carve the Unidentified Gemstone [Quest] to get You number of nation supply drops
    /// </summary>
    public void CarveUniGemStone(string item = "Any", int quant = 1000)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }
        if (Core.CheckInventory(item, quant))
            return;

        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Hadean Onyx of Nulgath", isTemp: false);
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(QuestDrops);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            //Carve the Unidentified Gemstone [Member] 4923
            Core.EnsureAccept(4923);
            Core.HuntMonster("WillowCreek", "Hidden Spy", "The Secret 1");
            FarmGemofNulgath(7);
            GemStonesOfnulgath(1, 3, 1, 1);
            switch (item)
            {
                case "Dark Crystal Shard":
                    Core.EnsureComplete(4923, 4770);
                    break;
                case "Diamond of Nulgath":
                    Core.EnsureComplete(4923, 4771);
                    break;
                case "Gem of Nulgath":
                    Core.EnsureComplete(4923, 6136);
                    break;
                case "Blood Gem of the Archfiend":
                    Core.EnsureComplete(4923, 22332);
                    break;
                default: //Tainted Gem
                    Core.EnsureComplete(4923, 4769);
                    break;
            }
            if (Bot.Inventory.IsMaxStack(item))
                Core.Logger("Max Stack Hit.");
            else Core.Logger($"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
    }

    public void HireNulgathLarvae()
    {
        if (Core.CheckInventory("Nulgath Larvae"))
            return;

        Core.AddDrop("Nulgath Larvae");
        Core.EnsureAccept(867);

        FarmVoucher(true);
        Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune", log: false);
        Core.EnsureComplete(867);
        Bot.Wait.ForPickup("Nulgath Larvae");
    }


    public void SwindlesBilk(string item, int quantity)
    {

        string Uni(int nr)
            => $"Unidentified {nr}";

        string[] rPDSuni = new[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20) };
        Core.AddDrop(rPDSuni);
        Core.AddDrop("Blood Gem of Nulgath");




    }
}
public enum ChooseReward
{
    TaintedGem = 4769,
    DarkCrystalShard = 4770,
    DiamondofNulgath = 4771,
    GemofNulgath = 6136,
    BloodGemoftheArchfiend = 22332,
    TotemofNulgath = 5357
}
