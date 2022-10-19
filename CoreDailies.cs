//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class CoreDailies
{
    // [Can Change] Default metals to be acquired by MineCrafting quest
    public string[] MineCraftingMetals = { "Barium", "Copper", "Silver" };
    // [Can Change] Default metals to be acquired by Hard Core Metals quest
    public string[] HardCoreMetalsMetals = { "Arsenic", "Chromium", "Rhodium" };
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
    public void DailyRoutine(int quest, string map, string monster, string item, int quant = 1, bool isTemp = true, string cell = null, string pad = "Left", bool publicRoom = false)
    {
        if (Bot.Quests.IsDailyComplete(quest))
            return;
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
                    InventoryItem _item = invBank.Find(x => x.Name == item);
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
                    InventoryItem _item = invBank.Find(x => x.Name == item);
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
        else Core.AddDrop(Core.EnsureLoad(quest).Rewards.Select(x => x.Name).ToArray());
        Core.AddDrop(Core.EnsureLoad(quest).Requirements.Select(x => x.Name).ToArray());

        return true;
    }

    /// <summary>
    /// Does the Mine Crafting quest for 2 Barium, Copper and Silver by default.
    /// </summary>
    /// <param name="metals">Metals you want to be collected</param>
    /// <param name="quant">Quantity you want of the metals</param>
    public void MineCrafting(string[] metals = null, int quant = 2, bool ToBank = false)
    {
        if (metals == null)
            metals = MineCraftingMetals;
        Core.Logger($"Daily: Mine Crafting ({string.Join('/', metals)})");
        if (Core.CheckInventory(metals, quant))
        {
            Core.Logger($"All metals were found with the needed quantity ({quant}). Skipped");
            if (ToBank)
                Core.ToBank(MineCraftingMetals);
            return;
        }
        if (!CheckDaily(2091, false, metals))
            return;

        Core.EnsureAccept(2091);
        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("stalagbite", "Balboa", "Axe of the Prospector", 1, false);
        Core.HuntMonster("stalagbite", "Balboa", "Raw Ore", 30);
        foreach (string metal in metals)
        {
            if (!Core.CheckInventory(metal, quant, false))
            {
                Core.AddDrop(metal);
                int metalID = MetalID(metal);
                Core.EnsureComplete(2091, metalID);
                Bot.Wait.ForPickup(metal);
                if (ToBank)
                    Core.ToBank(MineCraftingMetals);
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
    public void HardCoreMetals(string[] metals = null, int quant = 1, bool ToBank = false)
    {
        if (!Core.IsMember)
            return;
        if (metals == null)
            metals = HardCoreMetalsMetals;
        Core.Logger($"Daily: Hard Core Metals ({string.Join('/', metals)})");
        if (Core.CheckInventory(metals, quant))
        {
            Core.Logger($"All metals were found with the needed quantity ({quant}). Skipped");
            if (ToBank)
                Core.ToBank(HardCoreMetalsMetals);
            return;
        }
        if (!CheckDaily(2098, false, metals))
            return;

        Core.EnsureAccept(2098);
        Core.HuntMonster("stalagbite", "Balboa", "Axe of the Prospector", 1, false);
        Core.HuntMonster("stalagbite", "Balboa", "Raw Ore", 30);
        foreach (string metal in metals)
        {
            if (!Core.CheckInventory(metal, quant, false))
            {
                Core.AddDrop(metal);
                int metalID = MetalID(metal);
                Core.EnsureComplete(2098, metalID);
                Bot.Wait.ForPickup(metal);
                if (ToBank)
                    Core.ToBank(HardCoreMetalsMetals);
                break;
            }
        }
        if (Bot.Quests.IsInProgress(2098))
            Core.Logger($"All desired metals were found with the needed quantity ({quant}), quest not completed");
    }

    private int MetalID(string metal)
    {
        switch (metal.ToLower())
        {
            case "arsenic":
                return 11287;
            case "beryllium":
                return 11534;
            case "chromium":
                return 11591;
            case "palladium":
                return 11864;
            case "rhodium":
                return 12032;
            case "thorium":
                return 12075;
            case "mercury":
                return 12122;
            case "aluminum":
                return 11608;
            case "barium":
                return 11932;
            case "gold":
                return 12157;
            case "iron":
                return 12263;
            case "copper":
                return 12297;
            case "silver":
                return 12308;
            case "platinum":
                return 12315;
        }
        Core.Logger($"Could not find {metal}, is it written right?", messageBox: true, stopBot: true);
        return 0;
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
        Core.ToBank("C-Hammer Token");
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
        Core.ToBank("C-Armor Token");
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
            DailyRoutine(3826, "alteonbattle", "Ultra Alteon", "Alteon Defeated");
        }
        if (CheckDaily(3825, true, "Seal of Darkness"))
        {
            Core.EquipClass(ClassType.Solo);
            DailyRoutine(3825, "sepulchurebattle", "Ultra Sepulchure", "Sepulchure Defeated");
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
        if (!Core.CheckInventory("Crag & Bamboozle"))
            return;
        Core.Logger("Daily: Grumble Grumble (Blood Gem of the Archfiend");
        if (!CheckDaily(592, false, "Diamond of Nulgath", "Blood Gem of the Archfiend"))
            return;
        Core.ChainComplete(592);
    }

    public void EldersBlood()
    {
        Core.Logger("Daily: Elders' Blood");
        if (Core.CheckInventory("Elders' Blood", 5))
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
        DailyRoutine(6187, "boxes", "Sneevil", "Metal Ore", cell: "Enter", pad: "Spawn");
        Core.ToBank("Crypto Token");
    }

    public void MonthlyTreasureChestKeys()
    {
        if (!Core.IsMember)
            return;

        Core.Logger("Montly: Treasure Chest Keys");
        if (!CheckDaily(1239))
            Core.Logger($"Next keys are available on {new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).ToLongDateString()}");
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
            Core.HuntMonster("thevoid", "Flibbitiestgibbet", "Flibbitigiblets", isTemp: false, publicRoom: true);
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
            if (Core.CheckInventory(item.ID) && Bot.Inventory.TryGetItem(item.ID, out InventoryItem _item))
                CompareDict.Add(item, _item.Quantity);
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
        string[] rewards =
        {
            "Golden Inquisitor of Shadowfall",
            "Gilded Inquisitor's Female Morph",
            "Gilded Inquisitor's Male Morph",
            "Golden Inquisitor's Locks",
            "Golden Inquisitor's Hair",
            "Golden Inquisitor's Helm",
            "Golden Inquisitor's Crested Helm",
            "Golden Inquisitor's Spear",
            "Golden Inquisitor's Blade",
            "Golden Inquisitor's Wrap",
            "Golden Inquisitor's Back Blade",
            "Golden Inquisitor's Back Blade + Wrap"
        };

        Core.Logger("Daily: Golden Inquisitor of Shadowfall");
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

            Core.Logger("Dedicating daily to " + pet);
            bool dailyDone = !CheckDaily(4159);

            if (!Core.CheckInventory("Moglin MEAL", 30) && !dailyDone)
            {
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
}