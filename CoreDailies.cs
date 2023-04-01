/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Newtonsoft.Json;

public class CoreDailies
{
    // [Can Change] Default metals to be acquired by MineCrafting quest
    public string[] MineCraftingMetalsArray = { "Barium", "Copper", "Silver" };
    // [Can Change] Default metals to be acquired by Hard Core Metals quest
    public string[] HardCoreMetalsMetalsArray = { "Arsenic", "Chromium", "Rhodium" };
    // [Can Change] Skip daily if you own max stack of reward
    public bool SkipOnMaxStack = true;

    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    /// <summary>
    /// Accepts the quest and kills the monster to complete, if no cell/pad is given will hunt for the monster.
    /// </summary>
    /// <param name="quest">ID of the quest</param>
    /// <param name="map">Map where the monster is</param>
    /// <param name="monster">Name of the monster</param>
    /// <param name="item">Item to get</param>
    /// <param name="quant">Quantity of the item</param>
    /// <param name="isTemp">Whether it is temporary</param>
    /// <param name="cell">Cell where the monster is (optional)</param>
    /// <param name="pad">Pad where the monster is</param>
    public void DailyRoutine(int quest, string map, string monster, string item, int quant = 1, bool isTemp = true, string? cell = null, string pad = "Left", bool publicRoom = false)
    {
        if (Bot.Quests.IsDailyComplete(quest))
            return;
        Bot.Drops.Add(item);
        Core.Join(map);
        Core.EnsureAccept(quest);
        if (cell != null)
            Core.KillMonster(map, cell, pad, monster, item, quant, isTemp, true, publicRoom);
        else
            Core.HuntMonster(map, monster, item, quant, isTemp, true, publicRoom);
        Core.EnsureComplete(quest);
        Bot.Wait.ForPickup("*");
    }

    /// <summary>
    /// Checks if the daily is complete, if not will add the specified drops and unbank if necessary
    /// </summary>
    /// <param name="quest">ID of the quest</param>
    /// <param name="items">Items to add to drop grabber and unbank</param>
    /// <returns></returns>
    public bool CheckDaily(int quest, bool any = true, params string[] items)
    {
        if (Bot.Quests.IsDailyComplete(quest))
        {
            Core.Logger("Daily/Weekly/Monthly quest not available right now");
            return false;
        }

        if (items != null)
        {
            List<InventoryItem> invBank = Bot.Inventory.Items.Concat(Bot.Bank.Items).ToList().FindAll(x => items.ToList().Contains(x.Name));
            int i = 0;

            if (any)
            {
                foreach (string item in items)
                {
                    InventoryItem? _item = invBank.Find(x => x.Name == item);
                    if (_item == null)
                        continue;

                    if (_item.Quantity == _item.MaxStack)
                    {
                        Core.Logger("You already own the maximum amount of: " + item);
                        return false;
                    }
                }
            }
            else
            {
                foreach (string item in items)
                {
                    InventoryItem? _item = invBank.Find(x => x.Name == item);
                    if (_item == null)
                        continue;

                    if (_item.Quantity == _item.MaxStack)
                        i++;
                }

                if (items.Length == i)
                {
                    Core.Logger("You already own the maximum amount of: " + string.Join(',', items));
                    return false;
                }
            }
            Bot.Drops.Add(items);
        }
        if (quest == 3075 || quest == 3076)
            Bot.Drops.Add(Core.EnsureLoad(quest).Rewards.Select(x => x.Name).ToArray());
        else Core.AddDrop(Core.EnsureLoad(quest).Rewards.Select(x => x.Name).Where(x => !Core.CheckInventory(x, toInv: false)).ToArray());
        Core.AddDrop(Core.EnsureLoad(quest).Requirements.Select(x => x.Name).ToArray());

        return true;
    }

    /// <summary>
    /// Does the Mine Crafting quest for 2 Barium, Copper and Silver by default.
    /// </summary>
    /// <param name="metals">Metals you want to be collected</param>
    /// <param name="quant">Quantity you want of the metals</param>
    public void MineCrafting(string[]? metals = null, int quant = 2, bool ToBank = false)
    {
        if (metals == null)
            metals = MineCraftingMetalsArray;
        Core.Logger($"Daily: Mine Crafting ({string.Join('/', metals)})");
        if (Core.CheckInventory(metals, quant))
        {
            Core.Logger($"All metals were found with the needed quantity ({quant}). Skipped");
            if (ToBank)
                Core.ToBank(metals);
            return;
        }
        if (!CheckDaily(2091, false, metals))
            return;

        Core.EnsureAccept(2091);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("stalagbite", "Balboa", "Axe of the Prospector", isTemp: false);
        Core.HuntMonster("stalagbite", "Balboa", "Raw Ore", 30);
        foreach (string metal in metals)
        {
            if (!Core.CheckInventory(metal, quant, false))
            {
                Core.AddDrop(metal);
                int metalID = (int)Enum.Parse(typeof(MineCraftingMetalsEnum), metal);
                Core.EnsureComplete(2091, metalID);
                Bot.Wait.ForPickup(metal);
                if (ToBank)
                    Core.ToBank(metals);
                break;
            }
        }
        if (Bot.Quests.IsInProgress(2091))
            Core.Logger($"All desired metals were found with the needed quantity ({quant}), quest not completed");
        Bot.Sleep(Core.ActionDelay);
    }

    /// <summary>
    /// Does the Hard Core Metals quest for 1 Arsenic, Chromium and Rhodium by default
    /// </summary>
    /// <param name="metals">Metals you want to be collected</param>
    /// <param name="quant">Quantity you want of the metals</param>
    public void HardCoreMetals(string[]? metals = null, int quant = 1, bool ToBank = false)
    {
        if (!Core.IsMember || !Core.isCompletedBefore(2098))
            return;
        if (metals == null)
            metals = HardCoreMetalsMetalsArray;
        Core.Logger($"Daily: Hard Core Metals ({string.Join('/', metals)})");
        if (Core.CheckInventory(metals, quant))
        {
            Core.Logger($"All metals were found with the needed quantity ({quant}). Skipped");
            if (ToBank)
                Core.ToBank(metals);
            return;
        }
        if (!CheckDaily(2098, false, metals))
            return;

        Core.EnsureAccept(2098);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("stalagbite", "Balboa", "Axe of the Prospector", 1, false);
        Core.HuntMonster("stalagbite", "Balboa", "Raw Ore", 30);
        foreach (string metal in metals)
        {
            if (!Core.CheckInventory(metal, quant, false))
            {
                Core.AddDrop(metal);
                int metalID = (int)Enum.Parse(typeof(HardCoreMetalsEnum), metal);
                Core.EnsureComplete(2098, metalID);
                Bot.Wait.ForPickup(metal);
                if (ToBank)
                    Core.ToBank(metals);
                break;
            }
        }
        if (Bot.Quests.IsInProgress(2098))
            Core.Logger($"All desired metals were found with the needed quantity ({quant}), quest not completed");
    }

    public void FungiforaFunGuy()
    {
        if (!Core.IsMember)
            return;
        Core.Logger("Daily: Fungi for a Fun Guy (BrightOak Reputation)");
        if (Bot.Reputation.GetRank("Brightoak") == 10)
        {
            Core.Logger("BrightOak is already rank 10. Skipped");
            return;
        }
        if (!CheckDaily(4465))
            return;

        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(4465);
        Core.HuntMonster("brightoak", "Grove Spore", "Colony Spore");
        Core.HuntMonster("brightoak", "Grove Spore", "Intact Spore");
        Core.EnsureComplete(4465);
    }

    public void BeastMasterChallenge()
    {
        if (!Core.IsMember)
            return;
        Core.Logger("Daily: Beast Master Class");
        if (Bot.Reputation.GetRank("BeastMaster") == 10)
        {
            Core.Logger("BeastMaster is already rank 10. Skipped");
            return;
        }
        if (!CheckDaily(3759))
            return;

        DailyRoutine(3759, "swordhavenbridge", "Purple Slime", "Purple Slime", 10);
    }

    public void CyserosSuperHammer()
    {
        Core.Logger("Daily: Cysero's Super Hammer");
        if (Core.CheckInventory("Cysero's SUPER Hammer", toInv: false))
        {
            Core.Logger("Skipped");
            return;
        }
        if (!Core.CheckInventory("Cysero's SUPER Hammer", toInv: false) && Core.CheckInventory("C-Hammer Token", 90))
        {
            Core.BuyItem("deadmoor", 500, "Cysero's SUPER Hammer");
            return;
        }
        if (!Core.CheckInventory("Mad Weaponsmith"))
        {
            Core.Logger("You don't own Mad Weaponsmith yet. Skipped");
            return;
        }
        if (!CheckDaily(4310, true, "C-Hammer Token") && !Core.IsMember)
            return;
        if (!CheckDaily(4311, true, "C-Hammer Token") && Core.IsMember)
            return;
        Core.EquipClass(ClassType.Solo);
        DailyRoutine(4310, "deadmoor", "Geist", "Geist's Chain Link");
        if (Core.IsMember)
            DailyRoutine(4311, "deadmoor", "Geist", "Geist's Pocket Lint");
        Core.ToBank("C-Hammer Token", "Mad Weaponsmith", "Cysero's SUPER Hammer");
    }

    public void MadWeaponSmith()
    {
        Core.Logger("Daily: Mad Weaponsmith");
        if (Core.CheckInventory("Mad Weaponsmith", toInv: false))
        {
            Core.Logger("Skipped");
            return;
        }
        if (!Core.CheckInventory("Mad Weaponsmith", toInv: false) && Core.CheckInventory("C-Armor Token", 90))
        {
            Core.BuyItem("deadmoor", 500, "Mad Weaponsmith");
            return;
        }
        if (!CheckDaily(4308, true, "C-Armor Token") && !Core.IsMember)
            return;
        if (!CheckDaily(4309, true, "C-Armor Token") && Core.IsMember)
            return;
        Core.EquipClass(ClassType.Solo);
        DailyRoutine(4308, "deadmoor", "Nightmare", "Nightmare Fire");
        if (Core.IsMember)
            DailyRoutine(4309, "deadmoor", "Nightmare", "Unlucky Horseshoe");
        Core.ToBank("C-Armor Token", "Mad Weaponsmith");
    }

    public void BrightKnightArmor(bool checkArmor = true)
    {
        Core.Logger("Daily: Bright Knight Armor");
        if (checkArmor && Core.CheckInventory("Bright Knight", toInv: false))
        {
            Core.Logger("You already own the Bright Knight Armor, Skipped");
            return;
        }

        if (CheckDaily(3826, true, "Seal of Light"))
        {
            Core.EquipClass(ClassType.Solo);
            DailyRoutine(3826, "alteonbattle", "ULTRA Alteon", "Alteon Defeated");
        }
        if (CheckDaily(3825, true, "Seal of Darkness"))
        {
            Core.EquipClass(ClassType.Solo);
            DailyRoutine(3825, "sepulchurebattle", "ULTRA Sepulchure", "Sepulchure Defeated");
        }
        Core.JumpWait();
    }

    public void CollectorClass()
    {
        Core.Logger("Daily: The Collector Class");
        if (Core.CheckInventory("The Collector", toInv: false))
        {
            Core.Logger("You already own The Collector. Skipped");
            return;
        }
        if (CheckDaily(1316, true, "Tokens of Collection"))
        {
            Core.EquipClass(ClassType.Farm);
            Core.FarmingLogger("Token of Collection", 90);
            DailyRoutine(1316, "terrarium", "*", "This Might Be A Token", 2, false, "r2", "Right");
        }
        if (Core.IsMember)
        {
            Core.FarmingLogger("Token of Collection", 90);
            if (CheckDaily(1331, true, "Tokens of Collection"))
                DailyRoutine(1331, "terrarium", "*", "This Is Definitely A Token", 2, false, "r2", "Right");
            if (CheckDaily(1332, true, "Tokens of Collection"))
                DailyRoutine(1332, "terrarium", "*", "This Could Be A Token", 2, false, "r2", "Right");
        }
        if (Core.CheckInventory("Token of Collection", 90))
            Core.BuyItem("Collection", 324, "The Collector");
    }

    public void Cryomancer()
    {
        Core.Logger("Daily: Cryomancer Class");
        if (Core.CheckInventory("Cryomancer", toInv: false))
        {
            Core.Logger("You already own Cryomancer, Skipped");
            return;
        }

        if (Core.IsMember && CheckDaily(3965, true, "Glacera Ice Token"))
        {
            Core.EquipClass(ClassType.Farm);
            DailyRoutine(3965, "frozentower", "Frost Invader", "Dark Ice");
            Core.FarmingLogger("Glacera Ice Token", 84, "Glacera Ice Token");
            Core.ToBank("Glacera Ice Token");
        }

        if (CheckDaily(3966, true, "Glacera Ice Token"))
        {
            Core.EquipClass(ClassType.Farm);
            DailyRoutine(3966, "frozentower", "Frost Invader", "Dark Ice");
            Core.FarmingLogger("Glacera Ice Token", 84, "Glacera Ice Token");
            Core.ToBank("Glacera Ice Token");
        }
        if (Core.CheckInventory("Glacera Ice Token", 84))
            Core.BuyItem("frozenruins", 1056, "Cryomancer", shopItemID: 3041);
        Core.ToBank("Glacera Ice Token");
    }

    public void Pyromancer()
    {
        Core.Logger("Daily: Pyromancer Class");
        if (Core.CheckInventory("Pyromancer", toInv: false))
        {
            Core.Logger("You already own Pryomancer, Skipped");
            return;
        }
        Bot.Quests.UpdateQuest(2157);
        if (Core.IsMember && CheckDaily(2210, true, "Shurpu Blaze Token"))
        {
            Core.EquipClass(ClassType.Solo);
            DailyRoutine(2210, "xancave", "Shurpu Ring Guardian", "Guardian Shale");
            Core.FarmingLogger("Shurpu Blaze Token", 84, "Shurpu Blaze Token");
            Core.ToBank("Shurpu Blaze Token");
        }

        if (CheckDaily(2209, true, "Shurpu Blaze Token"))
        {
            Core.EquipClass(ClassType.Solo);
            DailyRoutine(2209, "xancave", "Shurpu Ring Guardian", "Guardian Shale");
            Core.FarmingLogger("Shurpu Blaze Token", 84, "Shurpu Blaze Token");
            Core.ToBank("Shurpu Blaze Token");
        }

        if (Core.CheckInventory("Shurpu Blaze Token", 84))
            Core.BuyItem("xancave", 447, "Pyromancer", shopItemID: 1278);
        Core.ToBank("Shurpu Blaze Token");
    }

    public void DeathKnightLord()
    {
        if (!Core.IsMember)
            return;

        Core.Logger("Daily: Death KnightLord Class");

        if (Core.CheckInventory(34780, toInv: false))
        {
            Core.Logger("You already own DeathKnight Lord Class, Skipped");
            return;
        }

        if (!CheckDaily(492, true, "Shadow Skull"))
            return;

        DailyRoutine(492, "bludrut4", "Shadow Serpent", "Shadow Scales", 5);

        Core.FarmingLogger("Shadow Skull", 30);
        if (Core.CheckInventory("Shadow Skull", 30))
            Core.BuyItem("bonecastle", 1242, 34780, shopItemID: 4397);

        Core.ToBank("Shadow Skull");
    }

    public void ShadowScytheClass()
    {
        Core.Logger("Daily: ShadowScythe General Class");
        if (Core.CheckInventory("ShadowScythe General", toInv: false))
        {
            Core.Logger("Skipped");
            return;
        }
        if (!Core.CheckInventory("ShadowScythe General") && Core.CheckInventory("Shadow Shield", 100))
        {
            Core.BuyItem("shadowfall", 1644, "ShadowScythe General");
            return;
        }
        if (!CheckDaily(3828, true, "Shadow Shield") && (Core.IsMember && !CheckDaily(3827, true, "Shadow Shield")))
            return;
        DailyRoutine(3828, "lightguardwar", "Citadel Crusader", "Broken Blade");
        if (Core.IsMember)
        {
            DailyRoutine(3827, "lightguardwar", "Citadel Crusader", "Broken Blade");
            if (Core.CheckInventory("Shadow Shield", 100))
                Core.BuyItem("shadowfall", 1644, "ShadowScythe General");
        }
        Core.Jump("Cut1", "Left");
    }

    public void GrumbleGrumble()
    {
        if (!Core.CheckInventory(4845))
            return;
        Core.Logger("Daily: Grumble Grumble (Blood Gem of the Archfiend)");
        if (!CheckDaily(592, false, new[] { "Diamond of Nulgath", "Blood Gem of the Archfiend" }))
            return;
        Core.ChainComplete(592);
    }

    public void EldersBlood()
    {
        Core.Logger("Daily: Elders' Blood");
        if (Core.CheckInventory("Elders' Blood", 20)) //AE keeps updating this shit, Laste update: 1/30/23, https://www.aq.com/gamedesignnotes/aqw-30jan23-mondayupdates-9076
            return;
        if (!CheckDaily(802, true, "Elders' Blood"))
            return;
        Core.EquipClass(ClassType.Farm);
        DailyRoutine(802, "arcangrove", "Gorillaphant", "Slain Gorillaphant", 50, cell: "Right", pad: "Left");
    }

    public void SparrowsBlood()
    {
        Core.Logger("Daily: Sparrow's Blood");
        if (!CheckDaily(803, true, "Sparrow's Blood"))
            return;
        Core.AddDrop("Sparrow's Blood");
        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(803);
        Core.HuntMonster("arcangrove", "Gorillaphant", "Blood Lily", 30);
        Core.HuntMonster("arcangrove", "Seed Spitter", "Snapdrake", 17);
        Core.HuntMonster("arcangrove", "Seed Spitter", "DOOM Dirt", 12);
        Core.EnsureComplete(803);
    }

    public void ShadowShroud()
    {
        Core.Logger("Daily: Shadow Shroud");
        if (!CheckDaily(486, true, "Shadow Shroud"))
            return;
        DailyRoutine(486, "bludrut2", "Shadow Creeper", "Shadow Canvas", 5, cell: "Enter", pad: "Down");
        Core.ToBank("Shadow Shroud");
    }

    public void DagesScrollFragment()
    {
        Core.Logger("Daily: Dage's Scroll Fragment");
        if (!CheckDaily(3596, true, "Dage's Scroll Fragment"))
            return;

        DailyRoutine(3596, "mountdoomskull", "*", "Chaos Power Increased", 6, cell: "b1", pad: "Left");

        Bot.Wait.ForPickup("Dage's Scroll Fragment");
        Core.ToBank("Dage's Scroll Fragment");
    }

    public void CryptoToken()
    {
        Core.Logger("Daily: Crypto Token (/curio)");
        if (!CheckDaily(6187, true, "Crypto Token"))
            return;
        Core.EquipClass(ClassType.Farm);
        DailyRoutine(6187, "boxes", "Sneevil", "Metal Ore", cell: "Closet", pad: "Center");
        Core.ToBank("Crypto Token");
    }

    public void MonthlyTreasureChestKeys()
    {
        if (!Core.IsMember)
            return;

        Core.Logger("Montly: Treasure Chest Keys");
        if (!CheckDaily(1239))
            Core.Logger($"Next keys are available on {new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).ToLongDateString()}");
        else Core.ChainComplete(1239);


        var questData = Core.EnsureLoad(1238);
        if (Core.CheckInventory(questData.Rewards.Select(x => x.Name).ToArray(), toInv: false))
            return;

        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();

        if (Core.CheckInventory("Magic Treasure Chest Key") && Core.CheckInventory("Treasure Chest", 1))
            Bot.Drops.Add(questData.Rewards.Select(x => x.Name).ToArray());

        while (!Bot.ShouldExit && Core.CheckInventory("Magic Treasure Chest Key") && Core.CheckInventory("Treasure Chest", 1))
        {
            Core.ChainComplete(1238);
            Bot.Wait.ForPickup("*");
        }

        Core.ToBank(Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray());
    }

    public void WheelofDoom()
    {
        Core.Logger($"{(Core.IsMember ? "Daily" : "Weekly")}: Wheel of Doom");
        List<string> PreQuestInv = Bot.Inventory.Items.Select(x => x.Name).ToList();

        if (Core.IsMember && CheckDaily(3075))
            Core.ChainComplete(3075);

        if (Core.CheckInventory("Gear of Doom", 3))
            Core.ChainComplete(3076);

        Bot.Wait.ForPickup("*");

        string[] Array = Bot.Inventory.Items.Select(x => x.Name).ToList().Except(PreQuestInv).ToArray();
        if (Array.Length == 0)
            return;

        Core.Logger("New items: " + string.Join(" | ", Array));
        Core.ToBank(Array);
    }

    public void NSoDDaily(bool IgnoreSwords = true)
    {
        if (!IgnoreSwords && Core.CheckInventory(new[] { "Necrotic Sword of Doom", "Dual Necrotic Swords of Doom" }, any: true) && Core.CheckInventory("Void Aura", 7500))
            return;

        Core.Logger("Daily: Void Auras");
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Void Aura", "(Necro) Scroll of Dark Arts");

        // Glimpse Into the Dark[Mem] - 8652
        if (Core.IsMember)
        {
            if (CheckDaily(8652))
            {
                Core.EnsureAccept(8652);
                if (Core.isCompletedBefore(3119))
                {
                    Core.AddDrop("Kraken Doubloon");
                    Core.RegisterQuests(3119);
                    while (!Bot.ShouldExit && !Core.CheckInventory("Kraken Doubloon", 13))
                    {
                        Core.HuntMonster("chaoskraken", "Chaos Kraken", "Kraken Keelhauled");
                    }
                    Core.CancelRegisteredQuests();
                }
                else Core.HuntMonster("chaoskraken", "Chaos Kraken", "Kraken Doubloon", 13, isTemp: false, publicRoom: true);
                Core.HuntMonster($"ancienttrigoras", "Ancient Trigoras", "Ancient Trigora’s Horns", 3, isTemp: false);
                Core.KillMonster("gravechallenge", "r19", "Left", "Graveclaw the Defiler", "Graveclaw's Broken Axe", isTemp: false);
                Core.EnsureComplete(8652);
                Bot.Wait.ForPickup("Void Aura");
            }
        }
        // The Encroaching Shadows - 8653
        if (CheckDaily(8653))
        {
            Core.EnsureAccept(8653);
            Core.HuntMonster("icestormarena", "Warlord Icewing", "Glacial Pinion", isTemp: false, publicRoom: true);
            Core.HuntMonster("hydrachallenge", "Hydra Head 90", "Hydra Eyeball", 3, isTemp: false);
            Core.HuntMonster("voidflibbi", "Flibbitiestgibbet", "Flibbitigiblets", isTemp: false, publicRoom: true);
            Core.EnsureComplete(8653);
            Bot.Wait.ForPickup("Void Aura");
        }
    }

    public void FreeDailyBoost()
    {
        if (!Core.IsMember)
            return;

        Core.Logger("Daily: Free Boost");

        if (!CheckDaily(4069))
            return;

        Quest quest = Core.EnsureLoad(4069);
        Dictionary<ItemBase, int> CompareDict = new();
        List<InventoryItem> InventoryData = Bot.Inventory.Items;

        foreach (ItemBase item in quest.Rewards)
        {
            if (item.ID == 27552 && Bot.Player.Level == 100)
                continue;
            if (Core.CheckInventory(item.ID) && Bot.Inventory.TryGetItem(item.ID, out InventoryItem? _item))
                CompareDict.Add(item, _item!.Quantity);
            else CompareDict.Add(item, 0);
        }
        // IWLQ = ItemWithLowestQuant
        ItemBase IWLQ = CompareDict.FirstOrDefault(x => x.Value == CompareDict.Values.Min()).Key;

        Core.AddDrop(IWLQ.Name);
        Core.ChainComplete(4069, IWLQ.ID);
        Bot.Wait.ForPickup(IWLQ.Name);
        Core.ToBank(IWLQ.Name);
    }

    public void BallyhooAdRewards()
    {
        if (AdCount() >= 3)
            return;

        Core.Logger($"Obtaining {3 - AdCount()} Ballyhoo Ad Reward{(AdCount() == 1 ? "" : "s")}");
        while (AdCount() < 3)
        {
            int PreGold = Bot.Player.Gold;
            int PreAC = PlayerAC();
            Bot.Send.Packet($"%xt%zm%getAdReward%{Bot.Map.RoomID}%");
            Bot.Sleep(Core.ActionDelay);
            Bot.Send.Packet($"%xt%zm%getAdData%{Bot.Map.RoomID}%");
            Bot.Sleep(1000);
            if (Bot.Player.Gold != PreGold)
                Core.Logger($"You received {Bot.Player.Gold - PreGold} Gold");
            else if (PlayerAC() != PreAC)
                Core.Logger($"You received {PlayerAC() - PreAC} AC!", messageBox: true);
        }

        int PlayerAC() => Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins");
        int AdCount() => Bot.Flash.GetGameObject<int>("world.myAvatar.objData.iDailyAds");
    }

    public void PowerGem()
    {
        Core.Logger("Weekly: Power Gems");
        if (Core.CheckInventory("Power Gem", 1000, false))
        {
            Core.Logger("You have the maximum amount of Power Gems");
            return;
        }

        Core.JumpWait();
        int PreQuant = Bot.Inventory.GetQuantity("Power Gem");
        Bot.Send.Packet($"%xt%zm%powergem%{Bot.Map.RoomID}%");
        Bot.Sleep(Core.ActionDelay);
        if (Bot.Inventory.GetQuantity("Power Gem") != PreQuant)
            Core.Logger($"You received {Bot.Inventory.GetQuantity("Power Gem") - PreQuant} Power Gem");
        else Core.Logger("You received no Power Gem");
        Core.ToBank("Power Gem");
    }

    public void GoldenInquisitor()
    {
        Core.Logger("Daily: Golden Inquisitor of Shadowfall");
        var rewards = Core.QuestRewards(491);
        if (Core.CheckInventory(rewards, toInv: false) || !CheckDaily(491))
            return;

        Core.EnsureAccept(491);
        Bot.Drops.Add(Core.EnsureLoad(491).Rewards.Select(x => x.Name).ToArray());
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("citadel", "Inquisitor Guard", "Inquisitor Contract", 7);
        Core.EnsureComplete(491);
        Bot.Wait.ForPickup("*");
        Core.ToBank(rewards);
    }

    public void DesignNotes()
    {
        Core.Logger("Weekly: Read the Design Notes!");

        if (Bot.Reputation.GetRank("Loremaster") != 10 && CheckDaily(1213))
            Core.ChainComplete(1213);
    }

    public void MoglinPets()
    {
        Core.Logger("Daily: Moglin Pets");
        string[] pets = { "Twig Pet", "Twiggy Pet", "Zorbak Pet" };
        if (Core.CheckInventory(pets, toInv: false))
            return;

        foreach (string pet in pets)
        {
            if (Core.CheckInventory(pet, toInv: false))
                continue;

            bool dailyDone = !CheckDaily(4159);

            if (!Core.CheckInventory("Moglin MEAL", 30) && !dailyDone)
            {
                Core.Logger("Dedicating daily to " + pet);
                Core.AddDrop("Moglin MEAL");
                Core.EnsureAccept(4159);
                Core.HuntMonster("nexus", "Frogzard", "Frogzard Meat", 3);
                Core.EnsureComplete(4159);
                Bot.Wait.ForPickup("Moglin MEAL");
                dailyDone = true;
            }

            if (Core.CheckInventory("Moglin MEAL", 30))
                Core.BuyItem("ariapet", 1081, pet);

            Core.ToBank("Moglin MEAL");

            if (dailyDone)
                break;
        }
    }
#nullable enable
    #region Friendship
    public void Friendships()
    {
        bool waitForPacket = false;
        string? _friendshipInfo = null;
        Bot.Events.ExtensionPacketReceived += friendshipPacketReader;

        if (!RefreshFriendshipData(out var friends))
            return;
        if (friends.All(f => !f.CanGift && (!f.CanTalk || f.NPC == "Linus")))
        {
            Core.Logger($"All the friendship dailies have already been completed today.");
            return;
        }

        Bot.Drops.Add(frGiftIDs);
        Core.AddDrop(frRewards);

        // Battleodium
        if (Core.isCompletedBefore(793))
            handleFriendship("Dage the Evil", frGift.Crached_Opal);
        handleFriendship("Gravelyn", frGift.Blood_Roseberry);
        handleFriendship("Nulgath", frGift.Apples);
        handleFriendship("Twig", frGift.Melons);
        handleFriendship("Twilly", frGift.Apples, frGift.Orchids);
        handleFriendship("Maya", frGift.Chrysanthemums, frGift.Apples);
        handleFriendship("Yulgar", frGift.Turqoise, frGift.Orchids, frGift.Melons);
        handleFriendship("Mi", frGift.Sapphires, frGift.Lilies);
        handleFriendship("Lord Brentan", frGift.Oranges, frGift.Rubies);
        handleFriendship("Warlic", frGift.Sapphires, frGift.Sunflowers);
        handleFriendship("Zorbak", frGift.Apples);
        handleFriendship("Smoglin", frGift.Turqoise, frGift.Apples);

        // Greyguard
        handleFriendship("Drakath", frGift.Chaos_Diemond);
        handleFriendship("Xang", frGift.Emeralds, frGift.Grapes);
        handleFriendship("Linus", frGift.A_Fish);
        handleFriendship("Sally", frGift.Rubies, frGift.Tulips);
        handleFriendship("Xing", frGift.Opals, frGift.Bananas);

        Bot.Events.ExtensionPacketReceived -= friendshipPacketReader;
        Core.ToBank(frGiftIDs);
        Core.ToBank(frRewards[3..]);

        #region Local methods
        void handleFriendship(string npc, params frGift[] gifts)
        {
            if (!friends.Any(f => f.NPC.ToLower() == npc.ToLower()))
            {
                Core.Logger($"NPC \"{npc}\" not found. Check for typos");
                return;
            }
            FriendshipInfo friend = friends.First(f => f.NPC.ToLower() == npc.ToLower());

            if ((!friend.CanTalk || friend.NPC == "Linus") && !friend.CanGift)
            {
                Core.Logger($"Friendship dail{(friend.NPC == "Linus" ? "y" : "ies")} unavailable: {friend.NPC}");
                return;
            }
            else Core.Logger($"Daily: Friendship ({friend.NPC})");

            Core.Join(friend.Map);
            SendWaitedPacket($"%xt%zm%friendshipInfo%{Bot.Map.RoomID}%{friend.NPC}%");

            if (friend.CanTalk && friend.NPC != "Linus")
            {
                SendWaitedPacket($"%xt%zm%friendshipTalk%{Bot.Map.RoomID}%");
                SendWaitedPacket($"%xt%zm%friendshipChoice%{Bot.Map.RoomID}%1%");
                InformLogger($"Talked to {friend.NPC}. Through the bot this has a 50% chance of giving hearts.", ref friend);
            }
            if (friend.CanGift)
            {
                int[] _gifts = gifts.Select(x => (int)x).ToArray();
                if (!Core.CheckInventory(_gifts, any: true))
                {
                    if (gifts.Count() == 1)
                        Core.FarmingLogger(gifts[0].ToString().Replace('_', ' '), 1);
                    else Core.Logger("Farming for one of the following items: " + String.Join(" | ", gifts.Select(x => x.ToString().Replace('_', ' ')).ToArray()));

                    switch (gifts[0])
                    {
                        case frGift.Chrysanthemums:
                        case frGift.Orchids:
                        case frGift.Roses:
                        case frGift.Sunflowers:
                        case frGift.Tulips:
                            Core.EquipClass(ClassType.Farm);
                            while (!Bot.ShouldExit && !Core.CheckInventory(_gifts, any: true))
                                Core.HuntMonster("battleodium", "Widowing", log: false);
                            break;

                        case frGift.Lilies:
                            Core.EquipClass(ClassType.Farm);
                            while (!Bot.ShouldExit && !Core.CheckInventory(_gifts, any: true))
                                Core.HuntMonster("greyguard", "Gloombloom", log: false);
                            break;

                        case frGift.Chaos_Diemond:
                            Core.EquipClass(ClassType.Farm);
                            Core.KillMonster("battleodium", "r6", "Left", "*", "Grapes", 1, false, false);
                            Core.KillMonster("battleodium", "r6", "Left", "*", "Diamonds", 1, false, false);
                            Core.BuyItem("battleodium", 2236, "Chaos Diemond");
                            break;

                        case frGift.Crached_Opal:
                            Core.EquipClass(ClassType.Farm);
                            Core.KillMonster("battleodium", "r6", "Left", "Vileture", "Melons", 1, false, false);
                            Core.KillMonster("battleodium", "r6", "Left", "Diemond", "Opals", 1, false, false);
                            Core.BuyItem("battleodium", 2236, "Cracked Opal");
                            break;

                        case frGift.Blood_Roseberry:
                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("battleodium", "Widowing", "Roses", 1, false, false);
                            Core.KillMonster("battleodium", "r6", "Left", "*", "Strawberries", 1, false, false);
                            Core.BuyItem("battleodium", 2236, "Blood Roseberry");
                            break;

                        case frGift.A_Fish:
                            if (!Bot.Quests.IsUnlocked(9107))
                            {
                                Core.EquipClass(ClassType.Solo);
                                Core.HuntMonster("greyguard", "Odium", "A Fish", 1, false, false);
                            }
                            else
                            {
                                Core.EquipClass(ClassType.Farm);
                                Core.HuntMonster("battleodium", "Widowing", "Roses", 1, false, false);
                                Core.KillMonster("battleodium", "r6", "Left", "*", "Strawberries", 1, false, false);
                                while (!Bot.ShouldExit && !Core.CheckInventory(76286)) ///multiple items with name "Rubies"
                                    Core.KillMonster("battleodium", "r6", "Left", "*", log: false);
                                Core.ChainComplete(9107);
                                Bot.Wait.ForPickup((int)gifts[0]);
                            }
                            break;

                        case frGift.Apples:
                        case frGift.Bananas:
                        case frGift.Grapes:
                        case frGift.Melons:
                        case frGift.Oranges:
                        case frGift.Strawberries:
                            Core.EquipClass(ClassType.Farm);
                            Core.KillMonster("battleodium", "r6", "Left", "Vileture", log: false);
                            break;


                        default:
                            Core.EquipClass(ClassType.Farm);
                            while (!Bot.ShouldExit && !Core.CheckInventory(_gifts, any: true))
                                Core.KillMonster("battleodium", "r6", "Left", "Diemond", log: false);
                            break;
                    }
                }

                Core.JumpWait();
                InventoryItem? selectedGift = null;
                Bot.Wait.ForTrue(() =>
                {
                    selectedGift = Bot.Inventory.Items.Find(x => _gifts.Contains(x.ID));
                    return selectedGift != null;
                }, 30);
                if (selectedGift == null)
                {
                    if (gifts.Count() > 1)
                        Core.Logger("Failed to parse any of the following items from your inventory: " + String.Join(" | ", gifts.Select(x => x.ToString())).Replace('_', ' '));
                    else Core.Logger($"Failed to find \"{gifts[0].ToString().Replace('_', ' ')}\" in your inventory.");
                    return;
                }
                SendWaitedPacket($"%xt%zm%friendshipGift%{Bot.Map.RoomID}%{selectedGift.ID}%{selectedGift.CharItemID}%");
                InformLogger($"Gifted {selectedGift.Name} to {friend.NPC}.", ref friend);
            }
        }

        void friendshipPacketReader(dynamic packet)
        {
            string type = packet["params"].type;
            dynamic data = packet["params"].dataObj;
            if (type is not null and "json")
            {
                string cmd = data.cmd.ToString();
                switch (cmd)
                {
                    case "friendshipInfo":
                    case "friendshipTalk":
                    case "friendshipChoice":
                        waitForPacket = true;
                        break;

                    case "friendshipStats":
                        _friendshipInfo = data.friendships.ToString();
                        break;
                }
            }
        }

        bool RefreshFriendshipData(out List<FriendshipInfo> friends)
        {
            _friendshipInfo = null;
            Bot.Send.Packet($"%xt%zm%friendshipStats%{Bot.Map.RoomID}%");
            Bot.Wait.ForTrue(() => _friendshipInfo != null, 30);
            if (_friendshipInfo == null)
            {
                Core.Logger("Something went wrong, friendshipInfo is null");
                friends = new();
                return false;
            }
            friends = JsonConvert.DeserializeObject<List<FriendshipInfo>>(_friendshipInfo)!;
            return friends.Count > 0;
        }

        void InformLogger(string text, ref FriendshipInfo info)
        {
            float oldNum = info.DisplayHearts;
            bool refreshed = RefreshFriendshipData(out friends);
            string npc = info.NPC;
            if (refreshed)
                info = friends.First(f => f.NPC == npc);
            float addNum = info.DisplayHearts - oldNum;

            Core.Logger(text +
                (refreshed ?
                    $" You gained {addNum} heart{(addNum > 1 ? "s" : String.Empty)}" :
                    String.Empty)
            );
        }

        void SendWaitedPacket(string packet)
        {
            waitForPacket = false;
            Bot.Send.Packet(packet);
            Bot.Wait.ForTrue(() => waitForPacket, 30);
        }
        #endregion
    }

    public int[] frGiftIDs = ((frGift[])Enum.GetValues(typeof(frGift))).Select(x => (int)x).ToArray();
    public string[] frGiftNames = ((frGift[])Enum.GetValues(typeof(frGift))).Select(x => x.ToString()).ToArray();
    public string[] frRewards =
    {
        "Gold Voucher 25k",
        "Gold Voucher 100",
        "Gold Voucher 500k",

        "Combat Trophy",
        "Super-Fan Swag Token A",
        "Super-Fan Swag Token B",
        "Dragon Runestone",
        "Faded Pigment",
        "Daily Login Gold Boost! (20 Min)",
        "Daily Login XP Boost! (20 min)",
        "Daily Login Rep Boost! (20 Min)",
        "Arcane Quill",
        "Spirit Orb",
        "Legion Token",
        "Void Aura",
        "Unidentified 10",
    };

    private enum frGift
    {
        Roses = 76272,
        Lilies = 76273,
        Tulips = 76274,
        Sunflowers = 76275,
        Chrysanthemums = 76276,
        Orchids = 76277,
        Apples = 76278,
        Oranges = 76279,
        Bananas = 76280,
        Strawberries = 76281,
        Grapes = 76282,
        Melons = 76283,
        Diamonds = 76284,
        Emeralds = 76285,
        Rubies = 76286,
        Sapphires = 76287,
        Opals = 76288,
        Turqoise = 76289,
        Chaos_Diemond = 76355,
        A_Fish = 76322,
        Crached_Opal = 76657,
        Blood_Roseberry = 76658,
    };

    private class FriendshipInfo
    {
        [JsonProperty("strName")]
        public string NPC { get; set; } = String.Empty;

        [JsonProperty("iHearts")]
        public int Hearts { get; set; }
        [JsonIgnore]
        public float DisplayHearts
        {
            get
            {
                return (float)Hearts / (float)4;
            }
        }

        [JsonProperty("strLocation")]
        public string Map { get; set; } = String.Empty;

        [JsonProperty("bTalk")]
        public bool CanTalk { get; set; }

        [JsonProperty("iGifts")]
        public int GiftCount { get; set; }
        [JsonIgnore]
        public bool CanGift
        {
            get
            {
                return GiftCount == 0;
            }
        }

        public override string ToString()
        {
            return $"{NPC}: {DisplayHearts} Hearts | Talked = {!CanTalk} | Gifted = {!CanGift}";
        }
    }
    #endregion

}

public enum MineCraftingMetalsEnum
{
    Aluminum = 11608,
    Barium = 11932,
    Gold = 12157,
    Iron = 12263,
    Copper = 12297,
    Silver = 12308,
    Platinum = 12315,
}

public enum HardCoreMetalsEnum
{
    Arsenic = 11287,
    Beryllium = 11534,
    Chromium = 11591,
    Palladium = 11864,
    Rhodium = 12032,
    Thorium = 12075,
    Mercury = 12122,
}
