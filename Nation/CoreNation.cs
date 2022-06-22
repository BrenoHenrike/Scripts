//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class CoreNation
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
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

        while (!Bot.ShouldExit() && !Core.CheckInventory("Dark Crystal Shard", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(570);
            Core.HuntMonster("faerie", "Aracara", "Aracara's Fang", 1, false);
            Core.HuntMonster("hydra", "Hydra Head", "Hydra Scale", 1, false);
            if (!Core.CheckInventory("Strand of Vath's Hair"))
            {
                Core.Join("stalagbite");
                Core.Jump("r2", "Left");
                Bot.Player.Kill("Vath");
                Core.JumpWait();
                if (Bot.Player.DropExists("Strand of Vath's Hair"))
                    Bot.Player.Pickup("Strand of Vath's Hair");
            }
            Core.HuntMonster("yokaiwar", "O-Dokuro's Head", "O-dokuro's Tooth", 1, false);
            Core.KillEscherion("Escherion's Chain", publicRoom: true);
            if (!Core.CheckInventory("Defeated Makai", 50))
            {
                Core.EquipClass(ClassType.Farm);
                Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Defeated Makai", 50, false);
                Core.JumpWait();
            }
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("djinn", "Tibicenas", "Tibicenas' Chain", publicRoom: true);
            Core.EnsureComplete(570);
            Bot.Wait.ForPickup("Dark Crystal Shard");
            Core.Logger($"Completed x{i++}");
        }
    }

    /// <summary>
    /// Does NWNO from Nulgath's Birthday Gift/Bounty Hunter's Drone Pet
    /// </summary>
    /// <param name="item">Desired item to get</param>
    /// <param name="quant">Desired quantity to get</param>
    public void NewWorldsNewOpportunities(string item = "Any", int quant = 1)
    {
        if (Core.CheckInventory(item, quant) || (!Core.CheckInventory("Nulgath's Birthday Gift") && !Core.CheckInventory("Bounty Hunter's Drone Pet")))
            return;

        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(bagDrops);
        Core.Logger($"Farming for {item}({quant})");
        int i = 1;

        while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
        {
            if (Core.CheckInventory("Bounty Hunter's Drone Pet"))
                Core.EnsureAccept(6183);
            else
                Core.EnsureAccept(6697);
            Bot.Options.AttackWithoutTarget = true;
            Core.HuntMonster("mobius", "Slugfit", "Slugfit Horn", 5);
            Bot.Options.AttackWithoutTarget = false;
            Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Makai Fang", 5);
            Core.HuntMonster("hydra", "Fire Imp", "Imp Flame", 3);
            Core.HuntMonster("faerie", "Cyclops Warlord", "Cyclops Horn", 3);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Wereboar Tusk", 2);
            if (Core.CheckInventory("Bounty Hunter's Drone Pet"))
                Core.EnsureComplete(6183);
            else
                Core.EnsureComplete(6697);
            Bot.Player.Pickup(bagDrops);
            Core.Logger($"Completed x{i++}");
        }
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

        while (!Bot.ShouldExit() && !Core.CheckInventory("Diamond of Nulgath", quant))
        {
            if (Core.IsMember)
                Core.EnsureAccept(2221);
            else
                Core.EnsureAccept(2219);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Legion Blade", 1, false);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Dessicated Heart", 22, false);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Legion Helm", 5);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Undead Skull", 3);
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Legion Champion Medal", 5);
            if (Core.IsMember)
                Core.EnsureComplete(2221);
            else
                Core.EnsureComplete(2219);
            Bot.Player.Pickup("Diamond of Nulgath");
            Core.Logger($"Completed x{i++}");
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

        Core.Logger($"Farming {quantApproval} Nulgath's Approval and {quantFavor} Archfiend's Favor");
        Core.AddDrop("Nulgath's Approval", "Archfiend's Favor");
        Core.EquipClass(ClassType.Farm);

        if (quantApproval > 0)
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Nulgath's Approval", quantApproval, false);
        if (quantFavor > 0)
            Core.KillMonster("evilwarnul", "r2", "Down", "*", "Archfiend's Favor", quantFavor, false);
        Core.Logger($"Finished");
    }


    /// <summary>
    /// Farms specific item with Swindles Return Policy quest
    /// </summary>
    /// <param name="quant">Desired Item quantity</param>
    /// <param name="item">Desired Item</param>
    public void SwindleReturn(string item = "Any", int quant = 1000, bool sellMemVoucher = true)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(bagDrops);
        Core.AddDrop(Receipt);

        while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
        {
            Core.EnsureAccept(7551);
            Supplies("Unidentified 1");
            Supplies("Unidentified 6");
            Supplies("Unidentified 9");
            Supplies("Unidentified 16");
            Supplies("Unidentified 20");
            Core.HuntMonster("evilmarsh", "Dark Makai", "Dark Makai Rune");
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

        while (!Bot.ShouldExit() && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.EnsureAccept(7817);
            Core.KillMonster("boxes", "Fort2", "Left", "*", "Cubes", 500, false);
            Core.KillMonster("mountfrost", "War", "Left", "Snow Golem", "Ice Cubes", 6);
            Core.EnsureComplete(7817);
            Bot.Player.Pickup("Tainted Gem");
            Core.Logger($"Completed x{i++}");
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
        while (!Bot.ShouldExit() && !Core.CheckInventory("Emblem of Nulgath", quant))
        {
            Core.EnsureAccept(4748);
            Core.KillMonster("shadowblast", "r13", "Left", "*", "Gem of Domination", 1, false);
            Core.KillMonster("shadowblast", "r13", "Left", "*", "Fiend Seal", 25, false);
            Core.EnsureComplete(4748);
            Bot.Player.Pickup("Emblem of Nulgath");
            Core.Logger($"Completed x{i++}");
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

        while (!Bot.ShouldExit() && !Core.CheckInventory("Nation Round 4 Medal"))
        {
            if (!Core.CheckInventory("Nation Round 1 Medal") &&
                !Core.CheckInventory("Nation Round 2 Medal") &&
                !Core.CheckInventory("Nation Round 3 Medal"))
            {
                Core.EnsureAccept(4744);
                Core.HuntMonster("shadowblast", "Legion Airstrike", "Legion Rookie Defeated", 5, true);
                Core.HuntMonster("shadowblast", "Shadowrise Guard", "Shadowscythe Rookie Defeated", 5, true);
                Core.EnsureComplete(4744);
                Bot.Player.Pickup("Nation Round 1 Medal");
                Core.Logger("Medal 1 acquired");
            }

            if (Core.CheckInventory("Nation Round 1 Medal"))
            {
                Core.EnsureAccept(4745);
                Core.HuntMonster("shadowblast", "Legion Fenrir", "Legion Veteran Defeated", 7, true);
                Core.HuntMonster("shadowblast", "Doombringer", "Shadowscythe Veteran Defeated", 7, true);
                Core.EnsureComplete(4745);
                Bot.Player.Pickup("Nation Round 2 Medal");
                Core.Logger("Medal 2 acquired");
            }

            if (Core.CheckInventory("Nation Round 2 Medal"))
            {
                Core.EnsureAccept(4746);
                Core.HuntMonster("shadowblast", "Legion Cannon", "Legion Elite Defeated", 10, true);
                Core.HuntMonster("shadowblast", "Draconic Doomknight", "Shadowscythe Elite Defeated", 10, true);
                Core.EnsureComplete(4746);
                Bot.Player.Pickup("Nation Round 3 Medal");
                Core.Logger("Medal 3 acquired");
            }

            if (Core.CheckInventory("Nation Round 3 Medal"))
            {
                Core.EnsureAccept(4747);
                Core.HuntMonster("shadowblast", "Grimlord Boss", "Grimlord Vanquished", 1, true);
                Core.EnsureComplete(4747);
                Bot.Player.Pickup("Nation Round 4 Medal");
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
        Bot.Player.Pickup("Gem of Nulgath", "Totem of Nulgath");
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

        Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Essence of Nulgath", quant, false);
    }

    /// <summary>
    /// Does Nulgath Larvae quest for the desired item
    /// </summary>
    /// <param name="item">Desired item name</param>
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
        int i = 1;
        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {quant} {item}");

        while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
        {
            Core.EnsureAccept(2566);
            if (!Core.CheckInventory("Mana Energy for Nulgath"))
                Core.HuntMonster("elemental", "Mana Golem", "Mana Energy for Nulgath", 1, false);
            while (!Bot.ShouldExit() && Core.CheckInventory("Mana Energy for Nulgath"))
            {
                Core.EnsureAccept(2566);
                Core.KillMonster("elemental", "r3", "Down", "*", "Charged Mana Energy for Nulgath", 5);
                Core.EnsureComplete(2566);
                Bot.Sleep(Core.ActionDelay);
                Core.Logger($"Completed x{i++}");
            }
            Bot.Player.Pickup(item);
        }
    }

    /// <summary>
    /// Does Supplies to Spin the Whell of Chance for the desired item with the best method available
    /// </summary>
    /// <param name="item">Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
    public void Supplies(string item = "Any", int quant = 1, bool sellMemVoucher = true)
    {
        if (Core.CheckInventory(item, quant))
            return;

        if (Core.CheckInventory(CragName))
            BambloozevsDrudgen(item, quant, sellMemVoucher);
        else
        {
            if (item != "Any")
            {
                Core.AddDrop(item);
                if (sellMemVoucher)
                    Core.AddDrop("Voucher of Nulgath");
            }
            else
                Core.AddDrop(bagDrops[..^11]);
            Core.Logger($"Farming {quant} {item}");
            int i = 1;

            while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
            {
                Core.EnsureAccept(2857);
                Core.KillEscherion("Relic of Chaos", publicRoom: true);
                Core.EnsureComplete(2857);

                Bot.Player.Pickup(item);
                if (sellMemVoucher)
                    Bot.Player.Pickup("Voucher of Nulgath");
                if (item != "Voucher of Nulgath" && sellMemVoucher && Core.CheckInventory("Voucher of Nulgath"))
                {
                    Core.SellItem("Voucher of Nulgath");
                }
                Core.Logger($"Completed x{i++}");
            }
        }
    }

    /// <summary>
    /// Does The Assistant quest for the desired item
    /// </summary>
    /// <param name="item">Desired item name</param>
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
                while (!Bot.ShouldExit() && Bot.Player.Gold >= 100000 && !Core.CheckInventory("War-Torn Memorabilia", 5))
                {
                    Bot.Shops.BuyItem(41, "War-Torn Memorabilia");
                    Bot.Sleep(Core.ActionDelay);
                }
            }
            Core.EnsureAccept(2859);
            while (!Bot.ShouldExit() && Core.CheckInventory("War-Torn Memorabilia") && !Core.CheckInventory(item, quant))
            {
                Core.ChainComplete(2859);
                Bot.Player.Pickup(bagDrops);
                Core.Logger($"Completed x{i++}");
            }
        }

        Core.Logger($"Farming {quant} {item}");
        if (farmGold)
        {
            while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
            {
                AssistantLoop();
                if (Bot.Player.Gold < 100000 && !Core.CheckInventory(item, quant))
                    Farm.Gold(1000000);
            }
        }
        else
        {
            while (!Bot.ShouldExit() && Bot.Player.Gold > 100000)
                AssistantLoop();

            if (!Core.CheckInventory(item, quant))
                Core.Logger($"Couldn't get {item}({quant})");
        }
    }

    /// <summary>
    /// Does Bamblooze vs Drudgen quest for the desired item
    /// </summary>
    /// <param name="item">Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
    public void BambloozevsDrudgen(string item = "Any", int quant = 1, bool sellMemVoucher = true)
    {
        if (!Core.CheckInventory(CragName) || Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("Relic of Chaos", "Tainted Core");
        if (item != "Any")
            Core.AddDrop(item);
        else
            Core.AddDrop(bagDrops);

        bool OBoNPet = (Core.CheckInventory("Oblivion Blade of Nulgath")
                    & Bot.Inventory.Items.Where(obon => obon.Category == RBot.Items.ItemCategory.Pet && obon.Name == "Oblivion Blade of Nulgath").Any());

        if (OBoNPet || Core.CheckInventory("Oblivion Blade of Nulgath (Rare)"))
            Core.AddDrop("Tainted Soul");

        Core.EquipClass(ClassType.Solo);
        Core.Logger($"Farming {quant} {item}");

        while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
        {
            Core.RegisterQuests(2857, 609);
            if (Core.CheckInventory("Oblivion Blade of Nulgath (Rare)"))
                Core.RegisterQuests(599);
            else if (OBoNPet)
                Core.RegisterQuests(2561);

            Core.KillMonster("evilmarsh", "End", "Left", "Tainted Elemental", "Tainted Core", 10, false);
        }

        Bot.Player.Pickup(Bot.Drops.Pickup.ToArray());
        if (Core.CheckInventory("Voucher of Nulgath") && item != "Voucher of Nulgath" && sellMemVoucher)
            Core.SellItem("Voucher of Nulgath");
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
        Core.KillMonster("evilmarsh", "Field1", "Left", "Dark Makai");
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
        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Blade Master Rune");
        Core.EnsureComplete(870, (int)reward);
        Bot.Player.Pickup(bagDrops);
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
            Core.KillMonster("legioncrypt", "r1", "Top", "Gravedigger", "Seraphic Grave Digger Spade", 1, false);
        Core.EquipClass(ClassType.Solo);
        int i = 1;
        while (!Bot.ShouldExit() && !Core.CheckInventory("Unidentified 10", quant))
        {
            Core.EnsureAccept(7818);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Slugbutter Digging Advice", publicRoom: true);
            Core.HuntMonster("crownsreach", "Chaos Tunneler", "Chaotic Tunneling Techniques", 2);
            Core.HuntMonster("downward", "Crystal Mana Construct", "Crystalized Corporate Digging Secrets", 3);
            Core.EnsureComplete(7818);
            Core.Logger($"Completed x{i++}");
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
            while (!Bot.ShouldExit() && !Core.CheckInventory("Unidentified 13", quant))
                DiamondExchange();
        NewWorldsNewOpportunities("Unidentified 13", quant);
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

        NewWorldsNewOpportunities("Dark Crystal Shard", quant);
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
        Supplies("Diamond of Nulgath", quant);
        DiamondEvilWar(quant);
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

        NewWorldsNewOpportunities("Gem of Nulgath", quant);
        while (!Bot.ShouldExit() && !Core.CheckInventory("Gem of Nulgath", quant))
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
            while (!Bot.ShouldExit() && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
                ContractExchange(ChooseReward.BloodGemoftheArchfiend);
        NewWorldsNewOpportunities("Blood Gem of the Archfiend", quant);
        KisstheVoid(quant);
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
        while (!Bot.ShouldExit() && !Core.CheckInventory("Totem of Nulgath", quant))
            VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);
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
        Supplies(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
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

        while (!Bot.ShouldExit() && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            Core.EnsureAccept(3743);
            if (!Core.CheckInventory("Tendurrr The Assistant"))
            {
                Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Tendurrr The Assistant", 1, false);
                Core.JumpWait();
            }
            Core.HuntMonster("lair", "Water Draconian", "Fragment of Chaos", 80, false);
            Core.KillMonster("evilwarnul", "r13", "Left", "Legion Fenrir", "Broken Betrayal Blade", 8);
            Core.EnsureComplete(3743);
            Bot.Wait.ForPickup("Blood Gem of the Archfiend");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void HireNulgathLarvae()
    {
        if (Core.CheckInventory("Nulgath Larvae"))
            return;

        Core.AddDrop("Nulgath Larvae");
        Core.EnsureAccept(867);

        FarmVoucher(true);
        Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune");
        Core.EnsureComplete(867);
        Bot.Wait.ForPickup("Nulgath Larvae");
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
