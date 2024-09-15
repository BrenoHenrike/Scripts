/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;

public class CoreNation
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreFarms Farm = new();

    //CanChange: If enabled will sell the "Voucher of Nulgath" item during farms if it's not needed.
    bool sellMemVoucher = true;
    //CanChange: If enabled will do "Swindles Return Policy" passively during "Supplies To Spin The Wheels of Fate".
    bool returnPolicyDuringSupplies = true;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.RunCore();
    }

    /// <summary>
    /// Crag and Bamboozle name in game
    /// </summary>
    public string CragName => "Crag &amp; Bamboozle";

    /// <summary>
    /// All principal drops from Nulgath
    /// </summary>
    public string[] bagDrops =
    {
        "Blood Gem of the Archfiend",
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Gem of Nulgath",
        "Totem of Nulgath",
        "Tainted Gem",
        "Unidentified 10",
        "Unidentified 13",
        "Voucher of Nulgath",
        "Voucher of Nulgath (non-mem)",

        // extras
        "Unidentified 24",
        "Essence of Nulgath",
        "Unidentified 25",
        "Fiend Token",
        "Emblem of Nulgath",
        "Receipt of Swindle",
        "Bone Dust",
        "Nulgath's Approval",
        "Archfiend's Favor",
        "Unidentified 34",
        "Essence of Nulgath"
    };

    public string[] SuppliesRewards =
    {
    "Tainted Gem",
    "Dark Crystal Shard",
    "Diamond of Nulgath",
    "Voucher of Nulgath",
    "Voucher of Nulgath (non-mem)",
    "Gem of Nulgath",
    "Unidentified 10",
    "Essence of Nulgath",
    "Receipt of Swindle"
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

    public string[] SwindlesReturn =
    {
        "Unidentified 1",
        "Unidentified 6",
        "Unidentified 9",
        "Unidentified 16",
        "Unidentified 20",
    };

    public string[] SwindlesReturnRewards =
    {
        "Tainted Gem",
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Gem of Nulgath",
        "Blood Gem of the Archfiend",
        "Receipt of Swindle"
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

        Core.AddDrop(tercessBags.Concat(bagDrops).ToArray());
        Core.FarmingLogger("Dark Crystal Shard", quant);

        Core.RegisterQuests(570);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Crystal Shard", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("faerie", "Aracara", "Aracara's Fang", isTemp: false, log: false);
            Core.HuntMonster("hydra", "Hydra Head", "Hydra Scale", isTemp: false, log: false);
            Core.KillVath("Strand of Vath's Hair", 1, isTemp: false);
            Core.HuntMonster("yokaiwar", "O-dokuro's Head", "O-dokuro's Tooth", isTemp: false, log: false);
            Core.KillEscherion("Escherion's Chain", publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Defeated Makai", 50, false, log: false);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("djinn", "Tibicenas", "Tibicenas' Chain", publicRoom: true, log: false);
            Bot.Wait.ForPickup("Dark Crystal Shard");
        }
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Does NWNO from Nulgath's Birthday Gift/Bounty Hunter's Drone Pet
    /// </summary>
    /// <param name="item">Desired item to get</param>
    /// <param name="quant">Desired quantity to get</param>
    public void NewWorldsNewOpportunities(string? item = null, int quant = 1)
    {
        if ((item != null && Core.CheckInventory(item, quant)) || (!Core.CheckInventory("Nulgath's Birthday Gift") && !Core.CheckInventory("Bounty Hunter's Drone Pet")))
            return;

        Core.AddDrop(Core.QuestRewards(Core.CheckInventory("Bounty Hunter's Drone Pet") ? 6183 : 6697));
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(Core.CheckInventory("Bounty Hunter's Drone Pet") ? 6183 : 6697);
        if (item == null)
        {
            ItemBase[] QuestRewards = Core.EnsureLoad(Core.CheckInventory("Bounty Hunter's Drone Pet") ? 6183 : 6697).Rewards.ToArray();
            foreach (var Item in QuestRewards)
            {
                if (Core.CheckInventory(Item.Name, Item.MaxStack))
                    continue;

                Core.FarmingLogger(Item.Name, Item.MaxStack);

                while (!Bot.ShouldExit && !Core.CheckInventory(Item.ID, Item.MaxStack))
                {
                    if (!Core.CheckInventory("Slugfit Horn", 5) || !Core.CheckInventory("Cyclops Horn", 3))
                    {
                        Core.JoinSWF("mobius-999999", "ChiralValley/town-Mobius-21Feb14.swf", "Slugfit", "Bottom");

                        foreach ((string mobName, string itemName, int quantity) in new[] { ("Slugfit", "Slugfit Horn", 5), ("Cyclops Warlord", "Cyclops Horn", 3) })
                        {
                            if (Core.CheckInventory(itemName, quantity))
                                continue;

                            while (!Bot.ShouldExit && !Core.CheckInventory(itemName, quantity))
                            {
                                int mapId = mobName == "Slugfit" ? 10 : 9; // Determine the map ID based on the mob name
                                if (Bot.Monsters.CurrentAvailableMonsters.Any(monster => monster.Name == mobName))
                                    Bot.Combat.Attack(mobName);
                                else
                                    Core.Sleep();
                            }
                            Bot.Wait.ForPickup(itemName);
                        }

                    }
                    Core.KillMonster("tercessuinotlim", "m2", "Top", "Dark Makai", "Makai Fang", 5, log: false);
                    Core.KillMonster("hydra", "Rune2", "Left", "Fire Imp", "Imp Flame", 3, log: false);
                    Core.KillMonster("greenguardwest", "West12", "Up", "Big Bad Boar", "Wereboar Tusk", 2, log: false);
                }
            }
            Core.Logger("all items quant maxed");
            Core.CancelRegisteredQuests();
        }
        else
        {
            Core.FarmingLogger(item, quant);
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                if (!Core.CheckInventory("Slugfit Horn", 5) || !Core.CheckInventory("Cyclops Horn", 3))
                {
                    Core.JoinSWF("mobius", "ChiralValley/town-Mobius-21Feb14.swf", "Slugfit", "Bottom");

                    foreach ((string mobName, string itemName, int quantity) in new[] { ("Slugfit", "Slugfit Horn", 5), ("Cyclops Warlord", "Cyclops Horn", 3) })
                    {
                        while (!Bot.ShouldExit && !Core.CheckInventory(itemName, quantity))
                        {
                            if (!Core.CheckInventory(itemName, quantity))
                            {
                                int mapId = mobName == "Slugfit" ? 10 : 9; // Determine the map ID based on the mob name
                                if (Bot.Monsters.CurrentAvailableMonsters.Any(monster => monster.Name == mobName))
                                    Bot.Combat.Attack(mobName);
                                else
                                    Core.Sleep();
                            }
                            else
                                break;

                            Core.Sleep();
                        }
                        Bot.Wait.ForPickup(itemName);
                    }

                }

                Core.KillMonster("tercessuinotlim", "m2", "Top", "Dark Makai", "Makai Fang", 5, log: false);
                Core.KillMonster("hydra", "Rune2", "Left", "Fire Imp", "Imp Flame", 3, log: false);
                Core.KillMonster("greenguardwest", "West12", "Up", "Big Bad Boar", "Wereboar Tusk", 2, log: false);
            }
            Core.Logger("items quant maxed");
            Core.CancelRegisteredQuests();
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
            Core.HuntMonster("evilwarnul", "Blade Master", "Legion Blade", isTemp: false, log: false);
            Core.HuntMonster("evilwarnul", "Blade Master", "Dessicated Heart", 20, false, log: false);
            Core.HuntMonster("underworld", "Skull Warrior", "Legion Helm", 5, log: false);
            Core.HuntMonster("underworld", "Skull Warrior", "Undead Skull", 3, log: false);
            Core.HuntMonster("underworld", "Skull Warrior", "Legion Champion Medal", 5, log: false);
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

        Core.FarmingLogger("Nulgath's Approval", quantApproval);
        Core.FarmingLogger("Archfiend's Favor", quantFavor);

        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && (!Core.CheckInventory("Nulgath's Approval", quantApproval) || !Core.CheckInventory("Archfiend's Favor", quantFavor)))
        {
            if (Bot.Map.Name != "evilwarnul")
                Core.Join("evilwarnul");
            if (Bot.Player.Cell != "r2")
                Core.Jump("r2", "Down");

            foreach (Monster Mob in Bot.Monsters.CurrentAvailableMonsters.Where(m => m.Cell == "r2"))
            {
                Bot.Kill.Monster(Mob.MapID);
                Core.Sleep();

                if (Core.CheckInventory("Nulgath's Approval", quantApproval) && Core.CheckInventory("Archfiend's Favor", quantFavor))
                    break;
            }
        }
    }

    /// <summary>
    /// Farms specific item with Swindles Return Policy quest
    /// </summary>
    /// <param name="item">Desired Item</param>
    /// <param name="quant">Desired Item quantity</param>
    public void SwindleReturn(string? item = null, int quant = 1000)
    {
        ItemBase? Item = Core.EnsureLoad(7551).Rewards.Find(x => x.Name == item);

        if (Item == null || Core.CheckInventory(Item.Name, quant))
            return;

        Core.AddDrop(Receipt);
        if (item != null)
            Core.AddDrop(Item.ID);

        sellMemVoucher = Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher) && _sellMemVoucher;

        Core.FarmingLogger(Item.Name, quant);

        while (!Bot.ShouldExit && !Core.CheckInventory(Item.Name, quant))
        {
            Core.EnsureAccept(7551);
            Supplies("Unidentified 1");
            Supplies("Unidentified 6");
            Supplies("Unidentified 9");
            Supplies("Unidentified 16");
            Supplies("Unidentified 20");
            Core.ResetQuest(7551);
            Core.DarkMakaiItem("Dark Makai Rune");
            Core.EnsureComplete(7551, Item.ID);
            if (Item.Name != "Voucher of Nulgath" && sellMemVoucher)
                Core.SellItem("Voucher of Nulgath", all: true);

            Core.FarmingLogger(Item.Name, quant);
        }
    }

    /// <summary>
    /// Farms Tainted Gem with Swindle Bulk quest.
    /// </summary>
    /// <param name="quant">Desired quantity, 1000 = max stack</param>
    public void SwindleBulk(int quant = 1000)
    {
        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Tainted Gem", quant);

        int questId = quant % 25 == 0 ? 7817 : 569;
        int cubeKillCount = quant % 25 == 0 ? 500 : 25;
        int snowGolemKillCount = quant % 25 == 0 ? 6 : 1;

        int attemptCount = 1;
        Core.AddDrop("Cubes", "Tainted Gem");
        Core.AddDrop(bagDrops);

        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.EnsureAccept(questId);
            Core.KillMonster("boxes", "Fort2", "Left", "*", "Cubes", cubeKillCount, false, log: false);
            Core.KillMonster("mountfrost", "War", "Left", "Snow Golem", "Ice Cubes", snowGolemKillCount, log: false);
            Core.EnsureComplete(questId);

            Bot.Drops.Pickup("Tainted Gem");
            Core.Logger($"Completed x{attemptCount++}");

            if (Bot.Inventory.IsMaxStack("Tainted Gem"))
            {
                Core.Logger("Max Stack Hit.");
                break;
            }
            else
            {
                Core.Logger($"Tainted Gem: {Bot.Inventory.GetQuantity("Tainted Gem")}/{quant}");
            }
        }
    }


    /// <summary>
    /// Farms specified items or a specific item in the specified location.
    /// </summary>
    /// <param name="item">The item to farm. If null, it farms a list of rewards.</param>
    /// <param name="quant">Desired quantity, 1000 = max stack.</param>
    public void FarmContractExchage(string? item = null, int quant = 1)
    {
        if (!Core.CheckInventory("Drudgen the Assistant") || (item != null && Core.CheckInventory(item, quant)))
        {
            if (!Core.CheckInventory("Drudgen the Assistant"))
                Core.Logger("Missing \"Drudgen the Assistant\"");
            return;
        }

        string?[] rewards = { "Tainted Gem", "Dark Crystal Shard", "Gem of Nulgath", "Blood Gem of the Archfiend" };

        Core.EquipClass(ClassType.Farm);
        Core.AddDrop(Core.QuestRewards(870));

        if (item != null)
        {
            ItemBase? Reward = Core.EnsureLoad(870)?.Rewards.Find(x => x.Name == item);
            string rewardName = Reward?.Name ?? string.Empty;
            Core.FarmingLogger(rewardName, quant > 1 ? quant : Reward?.MaxStack ?? default);
            while (!Bot.ShouldExit && !Core.CheckInventory(rewardName, quant > 1 ? quant : Reward?.MaxStack ?? default))
            {
                switch (rewardName)
                {
                    case "Tainted Gem":
                        Supplies("Diamond of Nulgath", 45);
                        ContractExchange(ContractExchangeRewards.Tainted_Gem, quant > 1 ? quant : Reward?.MaxStack ?? default);
                        break;
                    case "Dark Crystal Shard":
                        Supplies("Diamond of Nulgath", 45);
                        ContractExchange(ContractExchangeRewards.Dark_Crystal_Shard, quant > 1 ? quant : Reward?.MaxStack ?? default);
                        break;
                    case "Gem of Nulgath":
                        Supplies("Diamond of Nulgath", 45);
                        ContractExchange(ContractExchangeRewards.Gem_of_Nulgath, quant > 1 ? quant : Reward?.MaxStack ?? default);
                        break;
                    case "Blood Gem of the Archfiend":
                        Supplies("Diamond of Nulgath", 45);
                        ContractExchange(ContractExchangeRewards.Blood_Gem_of_the_Archfiend, quant > 1 ? quant : Reward?.MaxStack ?? default);
                        break;
                }
            }
        }
        else
        {
            foreach (string? thing in rewards)
            {
                ItemBase? Reward = Core.EnsureLoad(870)?.Rewards.Find(item => item.Name == thing) ?? new ItemBase();
                Core.FarmingLogger(Reward.Name, quant);
                while (!Bot.ShouldExit && !Core.CheckInventory(Reward.Name, quant > 1 ? quant : Reward.MaxStack))
                {
                    switch (Reward.Name)
                    {
                        case "Tainted Gem":
                            Supplies("Diamond of Nulgath", 45);
                            ContractExchange(ContractExchangeRewards.Tainted_Gem, quant > 1 ? quant : Reward.MaxStack);
                            break;
                        case "Dark Crystal Shard":
                            Supplies("Diamond of Nulgath", 45);
                            ContractExchange(ContractExchangeRewards.Dark_Crystal_Shard, quant > 1 ? quant : Reward.MaxStack);
                            break;
                        case "Gem of Nulgath":
                            Supplies("Diamond of Nulgath", 45);
                            ContractExchange(ContractExchangeRewards.Gem_of_Nulgath, quant > 1 ? quant : Reward.MaxStack);
                            break;
                        case "Blood Gem of the Archfiend":
                            Supplies("Diamond of Nulgath", 45);
                            ContractExchange(ContractExchangeRewards.Blood_Gem_of_the_Archfiend, quant > 1 ? quant : Reward.MaxStack);
                            break;
                    }
                }
            }
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
        NationRound4Medal();

        Core.AddDrop("Fiend Seal", "Gem of Domination", "Emblem of Nulgath");
        Core.AddDrop(bagDrops);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Emblem of Nulgath", quant);

        Core.RegisterQuests(4748);
        while (!Bot.ShouldExit && !Core.CheckInventory("Emblem of Nulgath", quant))
        {
            Core.HuntMonster("shadowblast", "Shadowrise Guard", "Gem of Domination", isTemp: false, log: false);
            Core.HuntMonster("shadowblast", "Legion Fenrir", "Fiend Seal", 25, isTemp: false, log: false);
        }
    }

    /// <summary>
    /// Farms the required medals for Nation Round 4 in Shadow Blast Arena.
    /// </summary>
    public void NationRound4Medal()
    {
        if (Core.CheckInventory("Nation Round 4 Medal"))
        {
            Core.Logger("Medal 4 owned, no need to farm it");
            return;
        }

        foreach (string medal in new[] { "Nation Round 1 Medal", "Nation Round 2 Medal", "Nation Round 3 Medal", "Nation Round 4 Medal" })
        {
            if (Core.CheckInventory(medal))
            {
                Core.Logger($"\"{medal}\" owned.");
            }
            else
            {
                switch (medal)
                {
                    // The Nation Needs YOU!
                    case "Nation Round 1 Medal":
                        Core.EnsureAccept(4744);
                        Core.HuntMonster("shadowblast", "Legion AirStrike", "Legion Rookie Defeated", 5);
                        Core.HuntMonster("shadowblast", "Shadowrise Guard", "Shadowscythe Rookie Defeated", 5);
                        Core.EnsureComplete(4744);
                        break;

                    // Show Me More, Nation-Noob
                    case "Nation Round 2 Medal":
                        Core.EnsureAccept(4745);
                        Core.HuntMonster("shadowblast", "Legion Fenrir", "Legion Veteran Defeated", 7);
                        Core.HuntMonster("shadowblast", "Doombringer", "Shadowscythe Veteran Defeated", 7);
                        Core.EnsureComplete(4745);
                        break;

                    // For the Nation!
                    case "Nation Round 3 Medal":
                        Core.EnsureAccept(4746);
                        Core.HuntMonster("shadowblast", "Legion Cannon", "Legion Elite Defeated", 10);
                        Core.HuntMonster("shadowblast", "Draconic Doomknight", "Shadowscythe Elite Defeated", 10);
                        Core.EnsureComplete(4746);
                        break;

                    // Nulgath Likes Your Style
                    case "Nation Round 4 Medal":
                        Core.EnsureAccept(4747);
                        Core.HuntMonster("shadowblast", "Grimlord Boss", "Grimlord Vanquished");
                        Core.EnsureComplete(4747);
                        break;
                }

                Bot.Drops.Pickup(medal);
                Core.Logger($"Medal {medal} acquired");
            }
        }
    }

    /// <summary>
    /// Farms Totem of Nulgath/Gem of Nulgath with Voucher Item: Totem of Nulgath quest
    /// </summary>
    /// <param name="reward">Which reward to pick (totem or gem)</param>
    /// <param name="quant"></param>
    public void VoucherItemTotemofNulgath(VoucherItemTotem reward = VoucherItemTotem.Totem_of_Nulgath, int quant = 100)
    {
        if (!Core.CheckInventory("Voucher of Nulgath (non-mem)"))
            FarmVoucher(false, true);


        Quest quest = Core.EnsureLoad(4778);
        ItemBase? Reward = quest.Rewards.FirstOrDefault(x => x.ID == (int)reward);

        if (Reward == null)
        {
            Core.Logger("Reward not found.");
            return;
        }

        foreach (ItemBase item in quest.Requirements.Concat(quest.Rewards))
            Core.AddDrop(item.ID);

        Core.FarmingLogger(Reward.Name, quant);
        while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID, quant))
        {
            Core.EnsureAccept(4778);
            EssenceofNulgath();
            if (!Bot.Quests.EnsureComplete(4778, Reward.ID))
            {
                EssenceofNulgath(Bot.Inventory.GetQuantity("Essence of Nulgath") < 100 ? Bot.Inventory.GetQuantity("Essence of Nulgath") + 1 : 60);
                Core.EnsureComplete(4778, Reward.ID);
            }
            Bot.Wait.ForPickup(Reward.ID);
        }
    }

    /// <summary>
    /// Farms Essences of Nulgath from Dark Makais in Tercessuinotlim
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    public void EssenceofNulgath(int quant = 60)
    {
        if (Core.CheckInventory("Essence of Nulgath", quant))
            return;

        Core.FarmingLogger("Essence of Nulgath", quant);

        Core.AddDrop("Essence of Nulgath");
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Essence of Nulgath", quant, false, false);
        Core.JumpWait();
    }

    /// <summary>
    /// Farms the specified item or all items from the Nulgath Larvae quest.
    /// </summary>
    /// <param name="item">The item to farm. If null, all items are farmed.</param>
    /// <param name="quant">The quantity of the item to farm.</param>
    public void NulgathLarvae(string? item = null, int quant = 1)
    {
        Quest? larvaeQuest = Bot.Quests.EnsureLoad(2566);
        if (larvaeQuest == null)
        {
            Core.Logger("Nulgath Larvae quest not found.");
            return;
        }

        Quest? voucherQuest = Bot.Quests.EnsureLoad(4778);
        if (voucherQuest == null)
            Core.Logger("Voucher quest not found.");

        if (item != null)
        {
            // Check if the item is a valid drop from quest 2566
            bool isValidItem = larvaeQuest.Rewards.Any(reward => reward.Name == item);
            if (!isValidItem)
            {
                Core.Logger($"{item} is not a valid drop from Nulgath Larvae quest.");
                return;
            }

            // Farming for a specific item
            FarmItem(larvaeQuest, voucherQuest, item, quant);
        }
        else
        {
            // Farming for all drops
            foreach (var reward in larvaeQuest.Rewards.Where(x => !Core.CheckInventory(x.ID, x.MaxStack, false)))
                FarmItem(larvaeQuest, voucherQuest, reward.Name, reward.MaxStack);
        }

        void FarmItem(Quest? larvaeQuest, Quest? voucherQuest, string item, int quant)
        {
            int itemId = voucherQuest?.Rewards.FirstOrDefault(x => x.Name == item)?.ID ?? -1;
            bool shouldFarm4778 = item != null && voucherQuest != null && voucherQuest.Rewards.Any(x => x.Name == item);

            Bot.Drops.Add("Mana Energy for Nulgath", item ?? string.Empty);

            Core.FarmingLogger(item, quant);
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                Core.EnsureAccept(2566);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("elemental", "Mana Golem", "Mana Energy for Nulgath", 13, isTemp: false);
                Core.EquipClass(ClassType.Farm);

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant) && Core.CheckInventory("Mana Energy for Nulgath"))
                {
                    Core.EnsureAccept(2566);
                    Core.HuntMonster("elemental", "Mana Falcon", "Charged Mana Energy for Nulgath", 5);
                    Core.EnsureComplete(2566);
                    Bot.Wait.ForPickup(item ?? string.Empty);
                    if (shouldFarm4778 && Core.CheckInventory("Voucher of Nulgath (non-mem)") && Core.CheckInventory("Essence of Nulgath", 60))
                    {
                        Core.EnsureAccept(4778);
                        Core.EnsureCompleteMulti(4778, itemId);
                        Bot.Wait.ForPickup(item ?? string.Empty);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Performs the necessary actions to obtain the desired item with the best available method.
    /// </summary>
    /// <param name="item">Desired item name.</param>
    /// <param name="quant">Desired item quantity.</param>
    /// <param name="UltraAlteon">Flag indicating if Ultra Alteon should be used.</param>
    /// <param name="KeepVoucher">Flag indicating if the voucher should be kept.</param>
    /// <param name="AssistantDuring">Flag indicating if the assistant should be active during the process.</param>
    /// <param name="ReturnItem">Item to return, if any.</param>
    /// <param name="ReturnItemQuant">Quantity of the return item.</param>
    public void Supplies(string? item = null, int quant = 1, bool UltraAlteon = false, bool KeepVoucher = false, bool AssistantDuring = false, string? ReturnItem = null, int ReturnItemQuant = 1)
    {
        //warning for idiots that wont read it
        Core.Logger("if Swindles is enabled, it will only accept the quest when it has the required Unis it needs");

        bool sellMemVoucher = Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher) && _sellMemVoucher == true;
        bool returnPolicyDuringSupplies = Core.CBOBool("Nation_ReturnPolicyDuringSupplies", out bool _returnSupplies) && _returnSupplies == true;

        Core.Logger($"Do Return Policy?: {returnPolicyDuringSupplies}");
        Core.Logger($"Sell Voucher(Mem)?: {sellMemVoucher}");

        Core.RegisterQuests(item != Uni(13) && Core.CheckInventory("Swindle Bilk's To Go Hut")
                            ? (Core.CheckInventory("Drudgen the Assistant")
                                ? new int[] { 870, 2857, 9542 }
                                : new int[] { 2857, 9542 })
                            : new int[] { 2857 });

        Core.AddDrop(
            // Include 'item' if it's not null
            (item != null ? new string[] { item } : Enumerable.Empty<string>())
            .Concat(Core.QuestRewards(9542)) // Add quest rewards

            // Concatenate supplies rewards including 'Voucher of Nulgath' if 'sellMemVoucher' is true
            .Concat(SuppliesRewards.Concat(sellMemVoucher ? new string[] { "Voucher of Nulgath" } : Enumerable.Empty<string>()).Append("Relic of Chaos"))

            // Add additional items during supplies if 'returnPolicyDuringSupplies' is true, including 'Receipt of Swindle'
            .Concat(returnPolicyDuringSupplies
                ? new string[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20), "Receipt of Swindle" }
                : Enumerable.Empty<string>()
            )
            .ToArray() // Convert to array for adding to the drop list
        );

        Core.EquipClass(ClassType.Solo);
        if (item == null)
        {
            foreach (string Thing in SuppliesRewards)
            {
                List<ItemBase> rewards = Core.EnsureLoad(2857).Rewards;
                ItemBase? Item = rewards.Find(x => x.Name == Thing);

                if (Item != null)
                {
                    if (Core.CheckInventory(CragName) && !UltraAlteon)
                        BambloozevsDrudgen(Item!.Name, Item.MaxStack, KeepVoucher, AssistantDuring, ReturnItem, ReturnItemQuant, true);
                    else
                    {
                        while (!Bot.ShouldExit && !Core.CheckInventory(Item.ID, Item.MaxStack) && returnPolicyDuringSupplies && ReturnItem != null && !Core.CheckInventory(ReturnItem, ReturnItemQuant))
                        {
                            if (UltraAlteon)
                                Core.KillMonster("ultraalteon", "r10", "Left", "Ultra Alteon", log: false);
                            else
                                Core.KillEscherion(log: false);
                            Core.Sleep();

                            if (item != "Voucher of Nulgath" && sellMemVoucher && Core.CheckInventory("Voucher of Nulgath"))
                            {
                                while (!Bot.ShouldExit && (Bot.Player.HasTarget || Bot.Player.InCombat) && Bot.Player.Cell != "Enter")
                                {
                                    Core.Jump("Enter", "Spawn");
                                    Core.Sleep();
                                    if (Bot.Player.Cell == "Enter")
                                        break;
                                }

                                Bot.Wait.ForPickup("Voucher of Nulgath");
                                Core.SellItem("Voucher of Nulgath", KeepVoucher ? 1 : 0, !KeepVoucher);
                                Bot.Wait.ForItemSell();

                                if (Bot.Player.Gold >= 1000000 && AssistantDuring)
                                {
                                    Core.JumpWait();

                                    decimal calculatedAmount = Bot.Player.Gold / 100000M;
                                    int quantityToBuy = (int)calculatedAmount;

                                    quantityToBuy = Math.Min(quantityToBuy, 250);

                                    Core.EnsureAccept(2859);
                                    Core.BuyItem("yulgar", 41, "War-Torn Memorabilia", quantityToBuy);
                                    Core.EnsureCompleteMulti(2859);
                                }
                            }
                            if (returnPolicyDuringSupplies && Core.CheckInventory(new[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20) }))
                            {
                                Core.ResetQuest(7551);
                                Core.DarkMakaiItem("Dark Makai Rune");
                                Quest? quest = Core.EnsureLoad(7551);

                                if (quest != null)
                                {
                                    List<ItemBase> ReturnRewards = Core.EnsureLoad(7551).Rewards;
                                    ItemBase? ReturnRewardsItem = rewards.Find(x => x.Name == item);

                                    foreach (ItemBase R in quest.Rewards)
                                    {
                                        if (Core.CheckInventory(R.ID, R.MaxStack) || !Bot.Quests.CanCompleteFullCheck(7551))
                                            continue;

                                        if (ReturnItem != null)
                                        {
                                            Core.EnsureComplete(7551, ReturnItem == "Receipt of Swindle" ? -1 : R.ID);
                                        }
                                        else
                                        {
                                            ItemBase? itemToComplete = quest.Rewards
                                                .FirstOrDefault(x => x.Name == ReturnItem);

                                            if (itemToComplete != null)
                                            {
                                                Core.EnsureComplete(7551, itemToComplete.ID);
                                            }
                                        }

                                        break;
                                    }
                                }
                                else
                                    Core.Logger("Failed to load quest 7551.");
                            }
                            if (Core.CheckInventory("Voucher of Nulgath (non-mem)") && Core.CheckInventory("Essence of Nulgath", 60))
                                Core.EnsureCompleteMulti(4778);
                        }
                    }
                }
            }
        }
        else // Handle the case when item is not null
        {
            if (Core.CheckInventory(CragName) && !UltraAlteon)
                BambloozevsDrudgen(item, quant, KeepVoucher, AssistantDuring, ReturnItem, ReturnItemQuant);
            else
            {
                List<ItemBase> rewards = Core.EnsureLoad(2857).Rewards;
                ItemBase? Item = rewards.Find(x => x.Name == item);

                if (Item != null && Core.CheckInventory(Item.ID, quant))
                {
                    Core.Logger($"Item: {item}, {Bot.Inventory.GetQuantity(Item.ID)}/ {Item.MaxStack}");

                    if (ReturnItem != null)
                        Core.Logger($"Item: {ReturnItem}, {Bot.Inventory.GetQuantity(ReturnItem)}/ {ReturnItemQuant}");
                    else
                        Core.Logger("ReturnItem is null.");

                    return;
                }
                else if (Item == null)
                    Core.Logger("Item is null.");

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                {
                    if (UltraAlteon)
                        Core.KillMonster("ultraalteon", "r10", "Left", "*", log: false);
                    else
                        Core.KillEscherion(item, quant, log: false);

                    // Sell voucher area
                    if (item != "Voucher of Nulgath" && sellMemVoucher && Core.CheckInventory("Voucher of Nulgath"))
                    {
                        while (!Bot.ShouldExit && (Bot.Player.HasTarget || Bot.Player.InCombat) && Bot.Player.Cell != "Enter")
                        {
                            Bot.Combat.CancelTarget();
                            Bot.Wait.ForCombatExit();
                            Core.JumpWait();
                            Core.Sleep();
                        }

                        Bot.Wait.ForPickup("Voucher of Nulgath");
                        Core.SellItem("Voucher of Nulgath", KeepVoucher ? 1 : 0, !KeepVoucher);
                        Bot.Wait.ForItemSell();
                    }

                    // Return Policy area
                    if (returnPolicyDuringSupplies && Core.CheckInventory(new[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20) }))
                    {
                        Core.ResetQuest(7551);
                        Core.DarkMakaiItem("Dark Makai Rune");
                        List<ItemBase> ReturnRewards = Core.EnsureLoad(7551).Rewards;
                        ItemBase? ReturnRewardsItem = rewards.Find(x => x.Name == item);

                        if (ReturnRewardsItem != null && ReturnRewards.Any(reward => reward.Name == item && reward.Name != "Receipt of Swindle"))
                            Core.EnsureComplete(7551, ReturnRewardsItem.ID);
                        else
                        {
                            Quest? quest = Bot.Quests.EnsureLoad(7551);
                            if (quest != null)
                            {
                                foreach (ItemBase R in quest.Rewards)
                                {
                                    if (Core.CheckInventory(R.ID, R.MaxStack) || !Bot.Quests.CanCompleteFullCheck(7551))
                                        continue;

                                    Core.Logger($"Item is Null, Maxing: {R.Name}[{R.ID}] ({Bot.Inventory.GetQuantity(R.Name)}/{R.MaxStack})");
                                    Core.EnsureComplete(7551, R.ID);
                                    break;
                                }
                            }
                            else
                                Core.Logger("Failed to load quest 7551.");
                        }
                    }
                    if (Core.CheckInventory("Voucher of Nulgath (non-mem)") && Core.CheckInventory("Essence of Nulgath", 60))
                        Core.EnsureCompleteMulti(4778);
                }
            }
        }

        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Does "The Assistant" quest for the desired item.
    /// </summary>
    /// <param name="item">Desired item name. Pass null to farm all available drops.</param>
    /// <param name="quant">Desired item quantity.</param>
    /// <param name="farmGold">Whether to farm gold (default: true).</param>
    /// <param name="Reward">Swindles Return Policy quest reward (default: None).</param>
    public void TheAssistant(string? item = null, int quant = 1000, bool farmGold = true, SwindlesReturnReward Reward = SwindlesReturnReward.None)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;

        // List of available drops for "The Assistant" quest
        string[] selectedDrops = item != null ? new string[] { item } : bagDrops[..^11];
        Core.AddDrop(selectedDrops);

        //add `Receipt of Swindle` from swindles return rewards.
        Core.AddDrop("Receipt of Swindle");

        //if running standalone, add the reward slection.
        if (Reward != SwindlesReturnReward.None)
            Core.AddDrop((int)Reward);

        // Check if return policy and sell voucher are active
        sellMemVoucher = Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher) && _sellMemVoucher;
        returnPolicyDuringSupplies = Core.CBOBool("Nation_ReturnPolicyDuringSupplies", out bool _returnSupplies);

        Core.Logger(returnPolicyDuringSupplies ? "Return Policy During Supplies: true" : "Return Policy During Supplies: false");
        Core.Logger($"Sell Voucher of Nulgath: {sellMemVoucher}");

        string[]? rPDSuni = null;
        if (returnPolicyDuringSupplies)
        {
            rPDSuni = new[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20) };
            Core.AddDrop(rPDSuni);
            Core.AddDrop("Blood Gem of Nulgath");
        }

        // Register the "Swindles Return Policy" quest if specified
        if (returnPolicyDuringSupplies && Reward == SwindlesReturnReward.None)
            Core.RegisterQuests(7551);

        // Farm all drops if 'item' is null
        if (item == null)
        {
            Core.Logger("Null method");
            foreach (string Thing in selectedDrops)
            {
                // Find the corresponding item in quest rewards
                var rewards = Core.EnsureLoad(2859).Rewards;
                ItemBase? Item = rewards.Find(x => x.Name == Thing);

                if (Item == null)
                    continue;

                // Continue farming until the desired item quantity is obtained
                while (!Bot.ShouldExit && !Core.CheckInventory(Item.Name, Item.MaxStack))
                {
                    LogMobItemQuant2(Item, Item.MaxStack);
                    if (farmGold)
                        Farm.Gold(1000000);

                    Core.EnsureAccept(2859);
                    Core.BuyItem("yulgar", 41, "War-Torn Memorabilia", 10);
                    Core.EnsureCompleteMulti(2859);

                    // Process "Swindles Return Policy" quest if return policy is active
                    if (Core.CheckInventory(rPDSuni) && returnPolicyDuringSupplies)
                    {
                        var rewards2 = Core.EnsureLoad(7551).Rewards;
                        ItemBase? Item2 = rewards2.Find(x => x.ID == Item.ID);

                        if (Item2 == null)
                            continue;
                        Core.ResetQuest(7551);

                        Core.FarmingLogger(Item2.Name, Item2.MaxStack);
                        Core.EnsureAccept(7551);
                        Core.DarkMakaiItem("Dark Makai Rune");
                        if (Reward != SwindlesReturnReward.None)
                            Core.EnsureComplete(7551, Item2.ID);
                    }
                    if (Core.CheckInventory("Voucher of Nulgath (non-mem)") && Core.CheckInventory("Essence of Nulgath", 60))
                        Core.EnsureCompleteMulti(4778);
                }
            }
            Core.CancelRegisteredQuests();
        }
        else
        {
            Core.Logger("Non-null method");
            // Continue farming the specified item until the desired quantity is obtained
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                LogMobItemQuant(item, quant);
                if (farmGold)
                    Farm.Gold(1000000);

                Core.EnsureAccept(2859);
                Core.BuyItem("yulgar", 41, "War-Torn Memorabilia", 10);
                Bot.Wait.ForItemBuy(40);
                Core.EnsureCompleteMulti(2859);

                // Process "Swindles Return Policy" quest if return policy is active
                if (Core.CheckInventory(rPDSuni) && returnPolicyDuringSupplies)
                {
                    var rewards2 = Core.EnsureLoad(7551).Rewards;
                    ItemBase? Item2 = rewards2.Find(x => x.ID == (int)Reward);

                    if (Item2 == null)
                        continue;
                    Core.ResetQuest(7551);

                    Core.FarmingLogger(Item2.Name, Item2.MaxStack);
                    Core.EnsureAccept(7551);
                    Core.DarkMakaiItem("Dark Makai Rune");
                    if (Reward != SwindlesReturnReward.None)
                        Core.EnsureComplete(7551, (int)Reward);
                }
                if (Core.CheckInventory("Voucher of Nulgath (non-mem)") && Core.CheckInventory("Essence of Nulgath", 60))
                    Core.EnsureCompleteMulti(4778);
            }
        }
    }

    /// <summary>
    /// Logs the quantity of the specified item after a time interval.
    /// </summary>
    /// <param name="item">Item name</param>
    /// <param name="quant">Desired item quantity</param>
    void LogMobItemQuant(string item, int quant)
    {
        // Check if the specified item is in inventory
        if (!Core.CheckInventory(item))
            return;

        // Get the initial quantity of the item in the inventory
        int startQuant = Bot.Inventory.GetQuantity(item);

        // Wait for a short period (e.g., 1.5 seconds) to allow the item quantity to change
        // (e.g., after completing a quest, the quantity might increase)
        Core.Sleep(1500);

        // Get the current quantity of the item in the inventory
        int currentQuant = Bot.Inventory.GetQuantity(item);

        // If the quantity changes or increases during the interval, log the updated quantity
        if (currentQuant != startQuant || currentQuant > startQuant)
        {
            Core.FarmingLogger(item, quant);

            // Wait for a short period again (optional)
            Core.Sleep(1500);
        }
    }

    /// <summary>
    /// Logs the quantity of the specified item object after a time interval.
    /// </summary>
    /// <param name="item">Item object</param>
    /// <param name="quant">Desired item quantity</param>
    void LogMobItemQuant2(ItemBase item, int quant = 1)
    {
        // Check if the specified item is in inventory
        if (!Core.CheckInventory(item.Name))
            return;

        // Get the initial quantity of the item in the inventory
        int startQuant = item.Quantity;

        // Wait for a short period (e.g., 1.5 seconds) to allow the item quantity to change
        // (e.g., after completing a quest, the quantity might increase)
        Core.Sleep(1500);

        // Get the current quantity of the item in the inventory
        int currentQuant = item.Quantity;

        // If the quantity changes or increases during the interval, log the updated quantity
        if (currentQuant > startQuant)
        {
            Core.FarmingLogger(item.Name, quant > 1 ? item.MaxStack : 1);

            // Wait for a short period again (optional)
            Core.Sleep(1500);
        }
    }

    /// <summary>
    /// Performs the "Bamblooze vs. Drudgen" quest for the desired item.
    /// </summary>
    /// <param name="item">Desired item name.</param>
    /// <param name="quant">Desired item quantity.</param>
    /// <param name="KeepVoucher">Flag indicating if the voucher should be kept.</param>
    /// <param name="AssistantDuring">Flag indicating if the assistant should be active during the process.</param>
    /// <param name="ReturnItem">Item to return, if any.</param>
    /// <param name="ReturnItemQuant">Quantity of the return item.</param>
    /// <param name="CamefromSupplies">Flag indicating if the call came from the Supplies method.</param>
    public void BambloozevsDrudgen(string? item = null, int quant = 1, bool KeepVoucher = false, bool AssistantDuring = false, string? ReturnItem = null, int ReturnItemQuant = 1, bool CamefromSupplies = false)
    {
        if (!Core.CheckInventory(CragName) || (Core.CheckInventory(item, quant) && ReturnItem != null && Core.CheckInventory(ReturnItem, ReturnItemQuant)))
            return;

        Core.AddDrop("Relic of Chaos", "Tainted Core");
        Core.AddDrop(string.IsNullOrEmpty(item) ? bagDrops : new string[] { item });

        bool hasOBoNPet = Core.IsMember && Core.CheckInventory("Oblivion Blade of Nulgath") &&
                          Bot.Inventory.Items.Any(obon => obon.Category == Skua.Core.Models.Items.ItemCategory.Pet && obon.Name == "Oblivion Blade of Nulgath");
        if (hasOBoNPet || Core.CheckInventory("Oblivion Blade of Nulgath Pet (Rare)"))
            Core.AddDrop("Tainted Soul");

        bool returnPolicyDuringSupplies = Core.CBOBool("Nation_ReturnPolicyDuringSupplies", out bool _returnSupplies) && _returnSupplies == true;
        bool sellMemVoucher = Core.CBOBool("Nation_SellMemVoucher", out bool _sellMemVoucher) && _sellMemVoucher == true;
        bool HasLogged = false;
        if (!CamefromSupplies)
        {
            Core.Logger($"Keep Voucher set to: {KeepVoucher}");
            Core.Logger($"Sell Voucher of Nulgath: {sellMemVoucher}");
        }

        if (returnPolicyDuringSupplies)
            Core.AddDrop(Uni(1), Uni(6), Uni(9), Uni(16), Uni(20));

        Dictionary<string, int> rewardItemIds = new()
        {
        { "Dark Crystal Shard", 123 },
        { "Diamond of Nulgath", 456 },
        { "Gem of Nulgath", 789 },
        { "Tainted Gem", 101 },
        { "Unidentified 10", 202 }
    };

        List<ItemBase> rewards = Core.EnsureLoad(2857).Rewards;
        ItemBase? itemBase = rewards.Find(x => x.Name == item);

        if (!string.IsNullOrEmpty(item))
            Core.FarmingLogger(item, quant);

        // Choose the appropriate quest based on pet availability
        if (Core.CheckInventory("Oblivion Blade of Nulgath Pet (Rare)") && Core.IsMember)
            Core.RegisterQuests(2857, 609, 599);
        else if (hasOBoNPet)
            Core.RegisterQuests(2857, 609, 2561);
        else
            Core.RegisterQuests(2857, 609);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            Core.KillMonster("evilmarsh", "End", "Left", "Tainted Elemental", log: false);

            if (item != "Voucher of Nulgath" && sellMemVoucher && Core.CheckInventory("Voucher of Nulgath"))
            {
                do
                {
                    // Ensure not in combat or has a target
                    Bot.Combat.CancelTarget();
                    Bot.Wait.ForCombatExit();
                    Core.Sleep();

                    // Jump to "Enter" and wait until successfully in "Enter" cell
                    do
                    {
                        Core.Sleep();
                        Core.Jump("Enter", "Spawn");

                        if (Bot.Player.Cell == "Enter")
                            break;

                    }
                    while (!Bot.ShouldExit && Bot.Player.Cell != "Enter");

                }
                while (!Bot.ShouldExit && (Bot.Player.HasTarget || Bot.Player.InCombat) && Bot.Player.Cell != "Enter");

                // Pickup and sell the item
                Bot.Drops.Pickup("Voucher of Nulgath");
                Core.SellItem("Voucher of Nulgath", KeepVoucher ? 1 : 0, !KeepVoucher);
                Bot.Wait.ForItemSell();

                if (Bot.Player.Gold >= 1000000 && AssistantDuring)
                {
                    Core.JumpWait();

                    decimal calculatedAmount = Bot.Player.Gold / 100000M;
                    int quantityToBuy = (int)calculatedAmount;

                    quantityToBuy = Math.Min(quantityToBuy, 250);

                    Core.EnsureAccept(2859);
                    Core.BuyItem("yulgar", 41, "War-Torn Memorabilia", quantityToBuy);
                    Core.EnsureCompleteMulti(2859);
                }
            }

            //reduce spam
            Quest? quest = Bot.Quests.EnsureLoad(7551);
            if (quest != null)
            {
                if (quest.Rewards.All(x => Bot.Inventory.GetQuantity(x.ID) >= x.MaxStack))
                {
                    if (!HasLogged && returnPolicyDuringSupplies)
                    {
                        Core.Logger("All Swindles Return Items are maxed stopping Swindles Return");
                        Core.AbandonQuest(7551);
                        HasLogged = true;
                    }
                }
                if (returnPolicyDuringSupplies && Core.CheckInventory(new[] { Uni(1), Uni(6), Uni(9), Uni(16), Uni(20) })
                && quest.Rewards.Any(x => Bot.Inventory.GetQuantity(x.ID) < x.MaxStack))
                {
                    Core.ResetQuest(7551);
                    Core.DarkMakaiItem("Dark Makai Rune");
                    List<ItemBase> returnRewards = Core.EnsureLoad(7551).Rewards;
                    ItemBase? returnRewardsItem = rewards.Find(x => x.Name == item);

                    foreach (ItemBase rewardItem in quest.Rewards)
                    {
                        if (Core.CheckInventory(rewardItem.ID, rewardItem.MaxStack))
                            continue;

                        if (ReturnItem != null)
                            Core.EnsureComplete(7551, ReturnItem == "Receipt of Swindle" ? -1 : rewardItem.ID);
                        else
                        {
                            ItemBase? firstIncompleteReward = quest.Rewards.FirstOrDefault(x => Bot.Inventory.GetQuantity(x.ID) < x.MaxStack);

                            if (firstIncompleteReward != null)
                                Core.EnsureComplete(7551, firstIncompleteReward.ID);
                            else
                                Core.Logger("No incomplete rewards found.");
                        }
                        break;
                    }
                }
                if (Core.CheckInventory("Voucher of Nulgath (non-mem)") && Core.CheckInventory("Essence of Nulgath", 60))
                    Core.EnsureCompleteMulti(4778);
            }
            else
                Core.Logger("Failed to load quest 7551.");
        }
        HasLogged = false;

    }

    /// <summary>
    /// Does the "AssistingDrudgen" Quest for Fiend Tokens (and other possible drops).
    /// Requires either "Drudgen the Assistant" or "Twin Blade of Nulgath" to accept.
    /// </summary>
    /// <param name="item">Desired item name</param>
    /// <param name="quant">Desired item quantity</param>
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
                Core.HuntMonster("alteonbattle", "Ultra Alteon", "Seal of Light");
                Core.EnsureComplete(3826);
            }
            Core.EnsureComplete(5816);
        }
    }

    /// <summary>
    /// Completes the Feed the Fiend quest to obtain the specified item.
    /// </summary>
    /// <param name="item">The item to obtain (default: "Fiend Token").</param>
    /// <param name="quant">The quantity of the item to obtain (default: 30).</param>
    public void FeedtheFiend(string item = "Fiend Token", int quant = 30)
    {
        // Check if the desired item is already in inventory or if the player is not a member
        if (Core.CheckInventory(item, quant) || !Core.IsMember)
            return;

        // Update and register the necessary quests
        Bot.Quests.UpdateQuest(2215);
        Core.RegisterQuests(3053);

        // Equip the appropriate class for the quest
        Core.EquipClass(ClassType.Solo);

        // Continue the quest until the desired item and quantity are obtained
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            // Hunt monsters to complete the quest
            FarmDiamondofNulgath(1);
            Core.HuntMonster("lair", "Red Dragon", "Dragon Fiend Gem", 13, isTemp: false);
            Core.KillMonster("battleunderd", "r5", "Left", "Glacial Horror", "Glacial Bones", 3, isTemp: false);
            Core.HuntMonster("dreammaze", "Screamfeeder", "Screamfeeder Heart", isTemp: false);
        }

        // Wait for the item to be picked up and cancel any registered quests
        Bot.Wait.ForPickup(item);
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Completes the Void Knight Sword Quest to obtain the specified item.
    /// </summary>
    /// <param name="item">The item to obtain (default: "Any").</param>
    /// <param name="quant">The quantity of the item to obtain (default: 1).</param>
    public void VoidKightSwordQuest(string item = "Any", int quant = 1)
    {
        // Check if the desired item is already in inventory or if the required items are missing
        if (Core.CheckInventory(item, quant) || (!Core.CheckInventory(new[] { 38275, 38254 }, any: true)))
            return;

        // Add drops based on the provided item or bag drops
        Core.AddDrop(bagDrops);
        Core.AddDrop(item);

        if (item != "Any")
            Core.FarmingLogger(item, quant);

        // Register the appropriate quest based on the available items
        Core.RegisterQuests(Core.CheckInventory(38275) ? 5662 : 5659);

        // Continue the quest until the desired item and quantity are obtained
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            // Equip the Solo class and hunt monsters for quest completion
            Core.EquipClass(ClassType.Solo);
            Core.JoinSWF("mobius", "ChiralValley/town-Mobius-21Feb14.swf", "Slugfit", "Bottom");
            Core.HuntMonster("mobius", "Slugfit", "Slugfit Horn", 5);
            Core.HuntMonster("faerie", "Aracara", "Aracara Silk");

            // Equip the Farm class and hunt monsters for quest completion
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Makai Fang", 5, log: false);
            Core.HuntMonster("hydra", "Fire Imp", "Imp Flame", 3, log: false);
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Aquamarine of Nulgath", 3, false, log: false);

            // Pick up any dropped items
            Bot.Drops.Pickup(bagDrops);
        }

        // Cancel any registered quests once the desired items are obtained
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Do Diamond Exchange quest 1 time, if farmDiamond is true, will farm 15 Diamonds before if needed
    /// </summary>
    /// <param name="farmDiamond">Whether or not farm Diamonds</param>
    public void DiamondExchange(bool farmDiamond = true)
    {
        if ((!Core.CheckInventory("Diamond of Nulgath", 15) && !farmDiamond) || !Core.CheckInventory(CragName) || Core.CheckInventory(Uni(13), 13))
            return;

        Core.AddDrop("Diamond of Nulgath");

        if (farmDiamond)
            BambloozevsDrudgen("Diamond of Nulgath", 15);

        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && Core.CheckInventory("Diamond of Nulgath", 15) && !Core.CheckInventory(Uni(13), 13))
        {
            Core.ResetQuest(869);
            Core.DarkMakaiItem("Dark Makai Sigil");
            Core.EnsureCompleteMulti(869);
        }
        Bot.Options.AttackWithoutTarget = false;
    }

    /// <summary>
    /// Do Contract Exchange quest 1 time, if <paramref name="farmUni13"/> is true, will farm Uni 13 before if needed
    /// </summary>
    /// <param name="rewardEnum"></param>
    /// <param name="quant"></param>
    /// <param name="farmUni13">Whether or not farm Uni 13</param>
    public void ContractExchange(ContractExchangeRewards rewardEnum, int quant, bool farmUni13 = true)
    {
        string reward = rewardEnum.ToString().Replace("_", " ");
        if ((!Core.CheckInventory("Unidentified 13") && !farmUni13) || !Core.CheckInventory("Drudgen the Assistant"))
        {
            if (!Core.CheckInventory("Unidentified 13") && !farmUni13)
                Core.Logger($"{farmUni13} is probably set to false, please have a dev change it");
            if (!Core.CheckInventory("Drudgen the Assistant"))
                Core.Logger("Missing \"Drudgen the Assistant\"");
            return;
        }


        Core.AddDrop(bagDrops);
        Core.EquipClass(ClassType.Solo);
        Core.FarmingLogger(reward, quant);
        while (!Bot.ShouldExit && !Core.CheckInventory(reward, quant))
        {
            if (farmUni13 && !Core.CheckInventory("Unidentified 13"))
                FarmUni13(3);
            Core.ResetQuest(870);
            Core.KillMonster("tercessuinotlim", "m4", "Top", "Shadow of Nulgath", "Blade Master Rune", log: false);
            Core.EnsureComplete(870, (int)rewardEnum);
            Core.FarmingLogger(reward, quant);
        }
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
            Core.KillEscherion("Emerald Pickaxe");

        if (!Core.CheckInventory("Seraphic Grave Digger Spade"))
            Core.KillMonster("legioncrypt", "r1", "Top", "Gravedigger", "Seraphic Grave Digger Spade", isTemp: false);
        Core.EquipClass(ClassType.Solo);
        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory("Unidentified 10", quant))
        {
            Core.EnsureAccept(7818);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Slugbutter Digging Advice", publicRoom: true, log: false);
            Core.HuntMonster("crownsreach", "Chaos Tunneler", "Chaotic Tunneling Techniques", 2, log: false);
            Core.HuntMonster("downward", "Crystal Mana Construct", "Crystalized Corporate Digging Secrets", 3, log: false);
            Core.EnsureComplete(7818);
            Core.Logger($"Completed x{i++}");
            if (Bot.Inventory.GetQuantity("Unidentified 10") >= 1000)
                Core.Logger("Max Stack Hit.");
            else Core.FarmingLogger("Unidentified 10", quant);
        }
    }

    /// <summary>
    /// Farms Unidentified 13 with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 13 = max stack</param>
    public void FarmUni13(int quant = 13)
    {
        if (Core.CheckInventory("Unidentified 13", quant))
            return;

        Core.AddDrop("Unidentified 13");
        quant = quant > 13 ? 13 : quant;

        if (Core.CheckInventory(CragName))
            while (!Bot.ShouldExit && !Core.CheckInventory("Unidentified 13", quant))
                DiamondExchange();
        NewWorldsNewOpportunities("Unidentified 13", quant); //1minute turning  = 1x guaranteed
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
        DirtyDeedsDoneDirtCheap(quant);
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
        FarmContractExchage("Dark Crystal Shard", quant);
        NewWorldsNewOpportunities("Dark Crystal Shard", quant); //1minute turning  = 1x guaranteed
        VoidKightSwordQuest("Dark Crystal Shard", quant);
        Supplies("Dark Crystal Shard", quant); //xx:xx time turnin = 10% chance
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

        VoidKightSwordQuest("Diamond of Nulgath", quant);
        Supplies("Diamond of Nulgath", quant);
    }

    /// <summary>
    /// Farms Fiend Tokens using various methods.
    /// </summary>
    /// <param name="quant">Desired quantity of Fiend Tokens, 30 = default stack size.</param>
    public void FarmFiendToken(int quant = 30)
    {
        // Check if Fiend Tokens are already in inventory
        if (Core.CheckInventory("Fiend Token", quant))
            return;

        // Try different quest methods to obtain Fiend Tokens
        VoidKightSwordQuest("Fiend Token", quant);
        AssistingDrudgen("Fiend Token", quant);
        FeedtheFiend();
    }

    /// <summary>
    /// Farms Gem of Nulgath with the best method available
    /// </summary>
    /// <param name="quant">Desired quantity, 300 = max stack</param>
    public void FarmGemofNulgath(int quant = 1000)
    {
        if (Core.CheckInventory("Gem of Nulgath", quant))
            return;

        FarmContractExchage("Gem of Nulgath", quant);
        Core.AddDrop("Gem of Nulgath");
        VoidKightSwordQuest("Gem of Nulgath", quant);

        VoucherItemTotemofNulgath(VoucherItemTotem.Gem_of_Nulgath, quant);
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

        FarmContractExchage("Blood Gem of the Archfiend", quant);
        NewWorldsNewOpportunities("Blood Gem of the Archfiend", quant);
        VoidKightSwordQuest("Blood Gem of the Archfiend", quant);
        BloodyChaos(quant, true);
        KisstheVoid(quant);
    }

    public void FarmTaintedGem(int quant = 100)
    {
        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        Core.AddDrop("Tainted Gem");
        FarmContractExchage("Tainted Gem", quant);
        ForgeTaintedGems(quant);
        Supplies("Tainted Gem", quant);
    }

    /// <summary>
    /// Completes the lair questline to unlock Nation mats if not completed.
    /// </summary>
    public void DragonSlayerReward()
    {
        int[] questIds = { 165, 166, 167, 168, 169 };
        string[] questMonsterNames = { "Water Draconian", "Hole", "Ledge", "Red Dragon", "Hole" };
        string[] questMonsterItems = { "Dragonslayer Veteran Medal", "Dragonslayer Sergeant Medal", "Dragonslayer Captain Medal", "Dragonslayer Marshal Medal", "Wisp of Dragonspirit" };
        int[] requiredQuantities = { 8, 8, 8, 8, 12 };
        ClassType[] questClasses = { ClassType.Farm, ClassType.Farm, ClassType.Farm, ClassType.Solo, ClassType.Farm };

        for (int i = 0; i < questIds.Length; i++)
        {
            int questId = questIds[i];
            string monsterName = questMonsterNames[i];
            string monsterItem = questMonsterItems[i];
            int requiredQuantity = requiredQuantities[i];
            ClassType questClass = questClasses[i];

            // Check if the quest is already completed
            if (Core.isCompletedBefore(questId))
                continue;

            // Equip the required class for the quest
            Core.EquipClass(questClass);

            // Accept the quest and hunt the required monster
            Core.EnsureAccept(questId);
            Core.HuntMonster("lair", monsterName, monsterItem, requiredQuantity);
            Core.EnsureComplete(questId);
        }
    }

    /// <summary>
    /// Farms Totem of Nulgath with the best method available.
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack.</param>
    public void FarmTotemofNulgath(int quant = 100) //, bool Taro = true)
    {
        // Check if Totem of Nulgath is already in inventory
        if (Core.CheckInventory("Totem of Nulgath", quant))
            return;

        VoucherItemTotemofNulgath(VoucherItemTotem.Totem_of_Nulgath, quant);
    }

    /// <summary>
    /// Do Bloody Chaos quest for Blood Gems.
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack.</param>
    /// <param name="relic">Indicates if Relic of Chaos supplies are used.</param>
    public void BloodyChaos(int quant = 100, bool relic = false)
    {
        if (Core.CheckInventory("Blood Gem of the Archfiend", quant) || Bot.Player.Level < 80)
            return;

        Core.AddDrop("Blood Gem of the Archfiend", "Hydra Scale Piece");
        if (relic)
            Core.AddDrop(BloodyChaosSupplies);

        Core.FarmingLogger("Blood Gem of the Archfiend", quant);

        Core.RegisterQuests(relic ? new[] { 7816, 2857 } : new[] { 7816 });

        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            Core.KillEscherion("Escherion's Helm", isTemp: false);
            Core.KillVath("Shattered Legendary Sword of Dragon Control", isTemp: false);
            Core.HuntMonster("hydrachallenge", "Hydra Head 85", "Hydra Scale Piece", 200, false);
        }

        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Do Kiss the Void quest for Blood Gems.
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
    /// <param name="betrayalBlade"></param>
    public void KisstheVoid(int quant = 100, string? betrayalBlade = null)
    {
        if (betrayalBlade == null ? Core.CheckInventory("Blood Gem of the Archfiend", quant) : Core.CheckInventory(betrayalBlade))
            return;

        Core.AddDrop(betrayalBlade ?? "Tendurrr The Assistant", "Fragment of Chaos", "Blood Gem of the Archfiend", "Broken Betrayal Blade");
        Core.EquipClass(ClassType.Farm);

        if (betrayalBlade == null)
            Core.FarmingLogger("Blood Gem of the Archfiend", quant);
        else
            Core.FarmingLogger(betrayalBlade, 1);

        int i = 1;

        while (!Bot.ShouldExit && (betrayalBlade == null ? !Core.CheckInventory("Blood Gem of the Archfiend", quant) : !Core.CheckInventory(betrayalBlade)))
        {
            Core.EnsureAccept(3743);

            if (!Core.CheckInventory("Tendurrr The Assistant"))
            {
                Core.KillMonster("tercessuinotlim", "m2", "Left", "*", "Tendurrr The Assistant", isTemp: false, log: false);
                Core.JumpWait();
            }

            Core.KillMonster("blindingsnow", "r17", "Left", "*", "Fragment of Chaos", 80, false);
            Core.KillMonster("evilwarnul", "r13", "Left", "Legion Fenrir", "Broken Betrayal Blade", 8, false);
            Core.EnsureComplete(3743);

            string itemToPickup = betrayalBlade ?? "Blood Gem of the Archfiend";
            Bot.Wait.ForPickup(itemToPickup);
            Core.Logger($"Completed x{i++}");

            if (betrayalBlade == null)
            {
                if (Bot.Inventory.IsMaxStack(itemToPickup))
                    Core.Logger("Max Stack Hit.");
                else
                    Core.FarmingLogger(itemToPickup, quant);
            }
        }
    }

    /// <summary>
    /// Farms Gemstone Receipt of Nulgath with specific quantities.
    /// </summary>
    /// <param name="quant">Desired quantity of Gemstone Receipt of Nulgath</param>
    public void GemStoneReceiptOfNulgath(int quant = 10)
    {
        const int demandingApprovalQuest = 4917;
        const int receiptOfNulgathQuest = 4924;
        const int receiptItemId = 33451;

        if (!Core.IsMember)
        {
            Core.Logger("This quest requires membership to be able to accept it.");
            return;
        }

        if (Core.CheckInventory("Gemstone Receipt of Nulgath", quant))
            return;

        Core.AddDrop("Gemstone Receipt of Nulgath", "Receipt of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Gemstone Receipt of Nulgath", quant))
        {
            Core.EnsureAccept(demandingApprovalQuest);

            FarmUni13(3);
            Farm.VampireREP();

            if (!Core.CheckInventory(receiptItemId))
            {
                DwoboCoin(100);
                Core.BuyItem("crashedruins", 1212, receiptItemId);
            }

            Core.EnsureAccept(receiptOfNulgathQuest);
            ApprovalAndFavor(0, 100);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("Extinction", "Control Panel", "Coal", 15, isTemp: false, log: false);
            DwoboCoin(10);
            EssenceofNulgath(10);
            Core.BuyItem("Tercessuinotlim", 68, "Blade of Affliction");
            Core.EnsureComplete(receiptOfNulgathQuest);
            Bot.Wait.ForPickup("Receipt of Nulgath");

            FarmVoucher(true, true);
            FarmVoucher(false, true);
            EssenceofNulgath(100);
            FarmTotemofNulgath(1);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("ShadowfallWar", "Bonemuncher", "Ultimate Darkness Gem", 5, isTemp: false);
            Core.EnsureComplete(demandingApprovalQuest);
            Bot.Wait.ForPickup("Gemstone Receipt of Nulgath");
        }
    }

    /// <summary>
    /// Farms Dwobo Coins with the specified quest and quantity.
    /// </summary>
    /// <param name="quant">Desired quantity of Dwobo Coins</param>
    public void DwoboCoin(int quant)
    {
        if (Core.CheckInventory("Dwobo Coin", quant))
            return;

        Core.FarmingLogger("Dwobo Coin", quant);
        Core.RegisterQuests(Core.IsMember ? 4798 : 4797);
        Core.AddDrop("Dwobo Coin");

        while (!Bot.ShouldExit && !Core.CheckInventory("Dwobo Coin", quant))
        {
            int unluckyExplorerCount = Core.IsMember ? 8 : 10;
            int spacetimeAnomalyCount = Core.IsMember ? 5 : 7;

            Core.KillMonster("crashruins", "r2", "Left", "Unlucky Explorer", "Ancient Treasure", unluckyExplorerCount, log: false);
            Core.KillMonster("crashruins", "r2", "Left", "Spacetime Anomaly", "Pieces of Future Tech", spacetimeAnomalyCount, log: false);
            Core.HuntMonster("crashruins", "Cluckmoo Idol", "Idol Heart", log: false);
        }

        Bot.Wait.ForPickup("Dwobo Coin");
        Core.CancelRegisteredQuests();
    }

    /// <summary>
    /// Farm Gemstones of Nulgath for specified quantities
    /// </summary>
    /// <param name="bloodStone">Desired quantity of Bloodstone of Nulgath</param>
    /// <param name="quartz">Desired quantity of Quartz of Nulgath</param>
    /// <param name="tanzanite">Desired quantity of Tanzanite of Nulgath</param>
    /// <param name="uniGemStone">Desired quantity of Unidentified Gemstone of Nulgath</param>
    public void GemStonesOfnulgath(int bloodStone = 100, int quartz = 100, int tanzanite = 100, int uniGemStone = 1)
    {
        const int gemstonesForNulgathQuest = 4918;
        const int skeletalWarriorQuest1 = 374;
        const int skeletalWarriorQuest2 = 375;
        const int boneTerrorQuest = 376;
        const int unidentifiedWeaponQuest = 377;

        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it.");
            return;
        }

        FarmUni13(1);
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 4");

        Core.AddDrop("Gem of Nulgath", "Bloodstone of Nulgath", "Quartz of Nulgath", "Tanzanite of Nulgath", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && (!Core.CheckInventory("Bloodstone of Nulgath", bloodStone)
                                || !Core.CheckInventory("Quartz of Nulgath", quartz)
                                || !Core.CheckInventory("Tanzanite of Nulgath", tanzanite)
                                || !Core.CheckInventory("Unidentified Gemstone of Nulgath", uniGemStone)))
        {
            Core.EnsureAccept(gemstonesForNulgathQuest);

            if (!Core.isCompletedBefore(boneTerrorQuest))
            {
                if (!Core.isCompletedBefore(skeletalWarriorQuest1))
                {
                    Core.EnsureAccept(skeletalWarriorQuest1);
                    Core.HuntMonster("battleundera", "Skeletal Warrior", "Yara's Ring", log: false);
                    Core.EnsureComplete(skeletalWarriorQuest1);
                }

                if (!Core.isCompletedBefore(skeletalWarriorQuest2))
                {
                    Core.EnsureAccept(skeletalWarriorQuest2);
                    Core.HuntMonster("battleundera", "Skeletal Warrior", "Skeletal Claymore", 6, log: false);
                    Core.HuntMonster("battleundera", "Skeletal Warrior", "Bony Chestplate", 3, log: false);
                    Core.EnsureComplete(skeletalWarriorQuest2);
                }

                if (!Core.isCompletedBefore(boneTerrorQuest))
                {
                    Core.EnsureAccept(boneTerrorQuest);
                    Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror's Head", log: false);
                    Core.EnsureComplete(boneTerrorQuest);
                }
            }

            while (!Bot.ShouldExit && !Core.CheckInventory("Yara's Sword"))
            {
                Core.AddDrop("Yara's Sword");
                Core.EnsureAccept(unidentifiedWeaponQuest);
                Core.HuntMonster("battleundera", "Skeletal Warrior", "Unidentified Weapon", isTemp: false, log: false);
                Core.EnsureComplete(unidentifiedWeaponQuest);
            }

            Core.HuntMonster("Twilight", "Abaddon", "Balor's Cruelty", isTemp: false, log: false);
            Core.HuntMonster("ShadowfallWar", "Bonemuncher", "Ultimate Darkness Gem", isTemp: false, log: false);
            Core.EnsureComplete(gemstonesForNulgathQuest);
        }
    }

    /// <summary>
    /// [Member] Does Forge Tainted Gems for Nulgath [Quest] to get You Tainted Gems with your specific quantities
    /// </summary>
    /// <param name="quant">Desired quantity of Tainted Gems</param>
    public void ForgeTaintedGems(int quant = 1000)
    {
        const int forgeTaintedGemsQuest = 4919;

        if (!Core.CheckInventory("Gemstone of Nulgath") || !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it.");
            return;
        }

        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        Core.AddDrop("Tainted Gem", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.EnsureAccept(forgeTaintedGemsQuest);
            FarmGemofNulgath(1);
            GemStonesOfnulgath(0, 3, 1, 0);
            Core.EnsureComplete(forgeTaintedGemsQuest);
        }
    }

    /// <summary>
    /// [Member] Forges Dark Crystal Shards for Nulgath [Quest] to obtain Dark Crystal Shards with specific quantities.
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack</param>
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
            // Forge Dark Crystal Shards for Nulgath [Member] 4920
            Core.EnsureAccept(4920);
            FarmGemofNulgath(1);
            GemStonesOfnulgath(0, 5, 2, 0);
            Core.EnsureComplete(4920);
        }
    }

    /// <summary>
    /// [Member] Forges Diamonds for Nulgath [Quest] to obtain Diamonds for Nulgath with specific quantities.
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack.</param>
    public void ForgeDiamondsOfNulgath(int quant = 1000)
    {
        if (!Core.CheckInventory("Gemstone of Nulgath") && !Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it.");
            return;
        }

        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        Core.AddDrop("Diamonds for Nulgath", "Unidentified Gemstone of Nulgath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Diamonds for Nulgath", quant))
        {
            // Forge Diamonds for Nulgath [Member] 4921
            Core.EnsureAccept(4921);
            FarmGemofNulgath(1);
            GemStonesOfnulgath(0, 2, 0, 0);
            Core.EnsureComplete(4921);
        }
    }

    /// <summary>
    /// [Member] Forges Blood Gems for Nulgath [Quest] to obtain Blood Gem of the Archfiend with specific quantities.
    /// </summary>
    /// <param name="quant">Desired quantity, 100 = max stack.</param>
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
            // Forge Blood Gems for Nulgath [Member] 4922
            Core.EnsureAccept(4922);
            FarmGemofNulgath(7);
            GemStonesOfnulgath(3, 5, 0, 0);
            Core.EnsureComplete(4922);
        }
    }

    /// <summary>
    /// [Member] Carves a Uni Gemstone [Quest] to obtain specific items.
    /// </summary>
    /// <param name="item">Desired item name.</param>
    /// <param name="quant">Desired item quantity.</param>
    public void CarveUniGemStone(string? item = null, int quant = 1000)
    {
        string[] questDrops = { "Tainted Gem", "Dark Crystal Shard", "Diamond of Nulgath", "Gem of Nulgath", "Blood Gem of the Archfiend" };

        // Check if the player is a member and has the desired items or item.
        if (!Core.IsMember)
        {
            Core.Logger("This quest requires you to have Gemstone of Nulgath and membership to be able to accept it");
            return;
        }

        if ((item == null && Core.CheckInventory(questDrops, quant)) || (item != null && Core.CheckInventory(item, quant)))
            return;

        // Required items
        Core.KillMonster("tercessuinotlim", "m4", "Right", "Shadow of Nulgath", "Hadean Onyx of Nulgath", isTemp: false);
        GemStoneReceiptOfNulgath(1);
        Supplies("Unidentified 5");

        if (item != null)
            Core.AddDrop(item);
        else
            Core.AddDrop(questDrops);

        while (!Bot.ShouldExit && (item == null || !Core.CheckInventory(item, quant)))
        {
            // Carve the Unidentified Gemstone [Member] 4923
            Core.EnsureAccept(4923);
            Core.HuntMonster("WillowCreek", "Hidden Spy", "The Secret 1", isTemp: false);
            FarmGemofNulgath(7);
            GemStonesOfnulgath(1, 3, 1, 1);

            static int GetItemIdByName(string? itemName) => itemName switch
            {
                "Dark Crystal Shard" => 4770,
                "Diamond of Nulgath" => 4771,
                "Gem of Nulgath" => 6136,
                "Blood Gem of the Archfiend" => 22332,
                "Tainted Gem" => 4769,
                _ => -1,
            };

            int itemId = GetItemIdByName(item);
            if (itemId != -1)
            {
                Core.EnsureComplete(4923, itemId);
            }
            else
            {
                Core.EnsureCompleteChoose(4923); // Complete the quest without specifying item ID
            }

            if (item != null)
                Core.Logger(Bot.Inventory.IsMaxStack(item) ? "Max Stack Hit." : $"{item}: {Bot.Inventory.GetQuantity(item)}/{quant}");
        }
    }

    /// <summary>
    /// Farms gold through Leery Contract exchange.
    /// </summary>
    /// <param name="quant">Desired gold quantity.</param>
    public void LeeryExchangeGold(int quant = 100000000)
    {
        // Check if the player is a member or already has the desired gold quantity.
        if (!Core.IsMember || Bot.Player.Gold >= quant)
            return;

        // Add Unidentified 13 to the drops list.
        Core.AddDrop("Unidentified 13");

        // Toggle Gold Boost and register the required quest.
        Farm.ToggleBoost(BoostType.Gold);
        Core.RegisterQuests(554);

        // Continue farming until the desired gold quantity is reached.
        while (!Bot.ShouldExit && Bot.Player.Gold < quant)
        {
            // Farm Unidentified 13 for the exchange.
            FarmUni13(13);

            // Hunt the specified monster to exchange Unidentified 13 for gold.
            while (Core.CheckInventory("Unidentified 13"))
                Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune", log: false);
        }

        // Cancel the registered quest and disable Gold Boost.
        Core.CancelRegisteredQuests();
        Farm.ToggleBoost(BoostType.Gold, false);
    }

    /// <summary>
    /// Hires Nulgath Larvae.
    /// </summary>
    public void HireNulgathLarvae()
    {
        // Check if Nulgath Larvae is already in inventory or the player is not a member.
        if (Core.CheckInventory("Nulgath Larvae") || !Core.IsMember)
            return;

        // Add Nulgath Larvae to the drops list.
        Core.AddDrop("Nulgath Larvae");

        // Accept the required quest.
        Core.EnsureAccept(867);

        // Farm the required vouchers for the quest.
        FarmVoucher(true, true);

        // Hunt the specified monster to complete the quest.
        Core.HuntMonster("underworld", "Undead Legend", "Undead Legend Rune", log: false);

        // Ensure the quest is completed and wait for the pet pickup.
        Core.EnsureComplete(867);
        Bot.Wait.ForPickup("Nulgath Larvae");
    }

    /// <summary>
    /// Swindles Bilk method
    /// </summary>
    /// <param name="item">Desired item name</param>
    public void SwindlesBilk(string item)
    {
        if (string.IsNullOrEmpty(item))
        {
            throw new ArgumentException($"'{nameof(item)}' cannot be null or empty.", nameof(item));
        }

        string[] rPDSuni = new[] { Uni1(1), Uni1(6), Uni1(9), Uni1(16), Uni1(20) };
        Core.AddDrop(rPDSuni);
        Core.AddDrop("Blood Gem of Nulgath");
    }

    private static string Uni1(int nr) => $"Unidentified {nr}";

    /// <summary>
    /// Farms Voucher of Nulgath (member or not) with the best method available
    /// </summary>
    /// <param name="member">If true will farm Voucher of Nulgath; false Voucher of Nulgath (nom-mem)</param>
    /// <param name="KeepVoucher"></param>
    public void FarmVoucher(bool member, bool KeepVoucher = false)
    {
        if ((Core.CheckInventory("Voucher of Nulgath (non-mem)") && !member) || (Core.CheckInventory("Voucher of Nulgath") && member))
            return;

        Core.AddDrop(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
        Core.Logger($"KeepVoucher set to {KeepVoucher}");

        BambloozevsDrudgen(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)", KeepVoucher: KeepVoucher);
        NewWorldsNewOpportunities(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
        VoidKightSwordQuest(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)");
        Supplies(member ? "Voucher of Nulgath" : "Voucher of Nulgath (non-mem)", KeepVoucher: KeepVoucher);
    }

    /// <summary>
    /// Farms Tainted Gems using Dreadrock Gem Exchange quest.
    /// </summary>
    /// <param name="quant">The quantity of Tainted Gems to farm.</param>
    public void DreadrockGemExchange(int quant = 1000)
    {
        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        if (!Core.CheckInventory("Unidentified 13"))
        {
            Core.FarmingLogger("Unidentified 13");
            FarmUni13(1);
        }

        Core.AddDrop("Tainted Gem");

        Core.EquipClass(ClassType.Farm);

        Core.FarmingLogger("Tainted Gem", quant);

        Core.RegisterQuests(4853);
        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.KillMonster("dreadrock", "r3", "Bottom", "*", log: false);
        }
        Core.CancelRegisteredQuests();
    }
}

public enum ChooseReward
{
    Tainted_Gem = 4769,
    Dark_Crystal_Shard = 4770,
    Diamond_of_Nulgath = 4771,
    Gem_of_Nulgath = 6136,
    Blood_Gem_of_the_Archfiend = 22332,
    Totem_of_Nulgath = 5357,
}

public enum ContractExchangeRewards
{
    Tainted_Gem = 4769,
    Dark_Crystal_Shard = 4770,
    Diamond_of_Nulgath = 4771,
    Gem_of_Nulgath = 6136,
    Blood_Gem_of_the_Archfiend = 22332,
    All = 0
}

public enum SwindlesReturnReward
{
    Tainted_Gem = 4769,
    Dark_Crystal_Shard = 4770,
    Diamond_of_Nulgath = 4771,
    Gem_of_Nulgath = 6136,
    Blood_Gem_of_the_Archfiend = 22332,
    None = 0
};

public enum VoucherItemTotem
{
    Totem_of_Nulgath = 5357,
    Gem_of_Nulgath = 6136
}