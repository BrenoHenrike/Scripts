using RBot;
using RBot.Items;
using RBot.Shops;
using System.Globalization;

public class CoreAdvanced
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    #region Enhancement 

    public void EnhanceEquipped(EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        List<InventoryItem> EquippedItems = Bot.Inventory.Items.FindAll(i => i.Equipped == true && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> EquippedWeapon = EquippedItems.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> EquippedOther = EquippedItems.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (Special == WeaponSpecial.None)
            _AutoEnhance(Core.EmptyList, EquippedItems, Type, Special);
        else _AutoEnhance(EquippedWeapon, EquippedOther, Type, Special);
    }

    public void EnhanceItem(string ItemName, EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        List<InventoryItem> SelectedItem = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(i => i.Name == ItemName && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> SelectedWeapon = SelectedItem.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> SelectedOther = SelectedItem.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (SelectedItem.Count == 0)
        {
            if (!SelectedItem.Any(x => x.Name == ""))
                Core.Logger($"You do not own \"{ItemName}\", enhancement failed");
            return;
        }

        if (SelectedWeapon.Count != 0)
            _AutoEnhance(SelectedWeapon, Core.EmptyList, Type, Special);
        if (SelectedOther.Count != 0)
            _AutoEnhance(Core.EmptyList, SelectedOther, Type, Special);
    }

    public void EnhanceItem(string?[] ItemNames, EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        List<InventoryItem> SelectedItems = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(i => ItemNames.Contains(i.Name) && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> SelectedWeapons = SelectedItems.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> SelectedOthers = SelectedItems.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (SelectedItems.Count == 0)
        {
            if (!SelectedItems.Any(x => x.Name == ""))
                Core.Logger($"You do not own \"{ItemNames}\", enhancement failed");
            return;
        }

        if (SelectedWeapons.Count != 0)
            _AutoEnhance(SelectedWeapons, Core.EmptyList, Type, Special);
        if (SelectedOthers.Count != 0)
            _AutoEnhance(Core.EmptyList, SelectedOthers, Type, Special);
    }

    public EnhancementType CurrentClassEnh()
    {
        int EnhPatternID = Bot.GetGameObject<int>($"world.invTree.{Bot.Inventory.CurrentClass.ID}.EnhPatternID");
        if (EnhPatternID == 1 || EnhPatternID == 23)
            EnhPatternID = 9;
        return (EnhancementType)EnhPatternID;
    }

    public WeaponSpecial CurrentWeaponSpecial()
    {
        InventoryItem EquippedWeapon = Bot.Inventory.Items.First(i => i.Equipped == true && WeaponCatagories.Contains(i.Category));
        int ProcID = Bot.GetGameObject<int>($"world.invTree.{EquippedWeapon.ID}.ProcID");
        return (WeaponSpecial)ProcID;
    }

    private static ItemCategory[] EnhanceableCatagories =
    {
        ItemCategory.Sword,
        ItemCategory.Axe,
        ItemCategory.Dagger,
        ItemCategory.Gun,
        ItemCategory.HandGun,
        ItemCategory.Rifle,
        ItemCategory.Bow,
        ItemCategory.Mace,
        ItemCategory.Gauntlet,
        ItemCategory.Polearm,
        ItemCategory.Staff,
        ItemCategory.Wand,
        ItemCategory.Whip,
        ItemCategory.Class,
        ItemCategory.Helm,
        ItemCategory.Cape,

    };
    private ItemCategory[] WeaponCatagories = EnhanceableCatagories[..12];

    private void _AutoEnhance(List<InventoryItem> WeaponList, List<InventoryItem> OtherList, EnhancementType Type, WeaponSpecial Special)
    {
        if (Special != WeaponSpecial.None && !Core.isCompletedBefore(2937))
        {
            Special = WeaponSpecial.None;
            Core.Logger("Awe enhancements are not unlocked yet. Using a normal enhancement");
        }

        List<InventoryItem> FlexibleList = Special == WeaponSpecial.None ? WeaponList.Concat(OtherList).ToList() : OtherList;
        int i = 0;
        bool LogOnce = false;

        if (WeaponList.Count == 0 && OtherList.Count == 0)
        {
            Core.Logger("Please report what you were trying to enhance to Lord Exelot#9674, enchantment failed");
            return;
        }

        //Gear
        if (FlexibleList.Count != 0)
        {
            if (Type == EnhancementType.Fighter)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 768 : 141);
            else if (Type == EnhancementType.Thief)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 767 : 142);
            else if (Type == EnhancementType.Wizard)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 765 : 144);
            else if (Type == EnhancementType.Healer)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 762 : 145);
            else if (Type == EnhancementType.Hybrid)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 766 : 143);
            else if (Type == EnhancementType.Lucky)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 763 : 147);
            else if (Type == EnhancementType.SpellBreaker)
                __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 764 : 146);
        }

        //Weapon Specials
        if (WeaponList.Count != 0 && Special != WeaponSpecial.None)
        {
            if (Bot.Player.Level == WeaponList.First().EnhancementLevel && (int)Type == Bot.GetGameObject<int>($"world.invTree.{WeaponList.First().ID}.EnhPatternID") &&
                   (WeaponCatagories.Contains(WeaponList.First().Category) ? (int)Special == Bot.GetGameObject<int>($"world.invTree.{WeaponList.First().ID}.ProcID") : true))
                i++;
            else
            {
                Core.Logger($"Best Enhancement of: {Type.ToString()} + {Special.ToString().Replace('_', ' ')}");
                if (Type == EnhancementType.Fighter)
                    __AutoEnhance(WeaponList, 635, "museum");
                else if (Type == EnhancementType.Thief)
                    __AutoEnhance(WeaponList, 637, "museum");
                else if (Type == EnhancementType.Wizard || Type == EnhancementType.SpellBreaker)
                    __AutoEnhance(WeaponList, 636, "museum");
                else if (Type == EnhancementType.Healer)
                    __AutoEnhance(WeaponList, 638, "museum");
                else if (Type == EnhancementType.Hybrid)
                    __AutoEnhance(WeaponList, 633, "museum");
                else if (Type == EnhancementType.Lucky)
                    __AutoEnhance(WeaponList, 639, "museum");
            }
        }

        if (i > 0)
            Core.Logger($"Skipped enhancement for {i} item{(i > 1 ? 's' : null)}");
        if (i != WeaponList.Count + OtherList.Count)
            Core.Logger("Enhancements complete");

        void __AutoEnhance(List<InventoryItem> Input, int ShopID, string Map = "")
        {
            List<ShopItem> ShopItems = new List<ShopItem>();

            foreach (InventoryItem Item in Input)
            {
                Core.CheckInventory(Item.Name);

                if (Bot.Player.Level == Item.EnhancementLevel && (int)Type == Bot.GetGameObject<int>($"world.invTree.{Item.ID}.EnhPatternID") &&
                   (WeaponCatagories.Contains(Item.Category) ? (int)Special == Bot.GetGameObject<int>($"world.invTree.{Item.ID}.ProcID") : true))
                {
                    i++;
                    continue;
                }

                if (!LogOnce && (!WeaponCatagories.Contains(Item.Category) || Special != WeaponSpecial.None))
                {
                    Core.Logger($"Best Enhancement of: {Type.ToString()}");
                    LogOnce = true;
                }

                Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Searching]");

                if (Map != "")
                    Core.Join(Map);
                Core.JumpWait();

                Bot.Shops.Load(ShopID);
                ShopItems = Bot.Shops.ShopItems;

                List<ShopItem> AvailableEnh = new List<ShopItem>();

                foreach (ShopItem Enh in ShopItems)
                {
                    if ((Core.IsMember || (!Core.IsMember && !Enh.Upgrade)) &&                             //Filtering out Member if you're non Member
                        Enh.Level <= Bot.Player.Level &&                                                   //Filtering out the ones you're not high enough level for
                        ((Input.Count == 1 && Enh.Name.Contains(Special.ToString().Replace('_', ' '))) ||     //If Input is just the weapon, and if the name of the Special is seen in the items
                        (Input.Count > 1 &&                                                               //If Input is not just weapon, then
                        (Enh.Name.Contains("Armor") && Item.Category == ItemCategory.Class) ||                  //If the Enhancement is for Classes
                        (Enh.Name.Contains("Helm") && Item.Category == ItemCategory.Helm) ||                    //If the Enhancement is for Helmets
                        (Enh.Name.Contains("Cape") && Item.Category == ItemCategory.Cape) ||                    //If the Enhancement is for Capes
                        (Enh.Name.Contains("Weapon") && WeaponCatagories.Contains(Item.Category)))))            //If the Enhancement is for Weapons
                        AvailableEnh.Add(Enh);                                                          //Add to the list of selectable Enhancements
                }

                List<ShopItem> ListMinToMax = AvailableEnh.OrderBy(x => x.Level).ToList();
                List<ShopItem> BestTwo = ListMinToMax.Skip(ListMinToMax.Count - 2).ToList();
                ShopItem SelectedEhn = new ShopItem();

                if (BestTwo.First().Level == BestTwo.Last().Level)
                    if (Core.IsMember)
                        SelectedEhn = BestTwo.First(x => x.Upgrade);
                    else SelectedEhn = BestTwo.First(x => !x.Upgrade);
                else SelectedEhn = BestTwo.OrderByDescending(x => x.Level).First();

                if (Bot.GetGameObject<int>($"world.invTree.{Item.ID}.EnhID") == SelectedEhn.ID)
                    Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Already applied]");
                else
                {
                    Bot.SendPacket($"%xt%zm%enhanceItemShop%{Bot.Map.RoomID}%{Item.ID}%{SelectedEhn.ID}%{ShopID}%");
                    Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Applied]");
                    Bot.Sleep(Core.ActionDelay);
                }
            }
        }
    }

    #endregion

    #region Gear

    public void rankUpClass(string ClassName)
    {
        if (!Core.CheckInventory(ClassName))
            Core.Logger($"Cant level up \"{ClassName}\" because you do not own it.", messageBox: true, stopBot: true);

        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ClassName.ToLower() && i.Category == ItemCategory.Class);
        string EquippedWeapon = Bot.Inventory.Items.First(i => i.Equipped == true && WeaponCatagories.Contains(i.Category)).Name;
        string ClassReAfter = Bot.Inventory.CurrentClass.Name;
        EnhancementType ReEnhanceAfter = CurrentClassEnh();
        WeaponSpecial ReWEnhanceAfter = CurrentWeaponSpecial();
        if (itemInv == null)
        {
            Core.Logger($"\"{ClassName}\" is not a valid Class", messageBox: true, stopBot: true);
            return;
        }
        if (itemInv.Quantity == 302500)
        {
            Core.Logger($"\"{itemInv.Name}\" is already Rank 10");
            return;
        }
        SmartEnhance(ClassName);
        EnhanceItem(BestGear(GearBoost.cp), CurrentClassEnh(), CurrentWeaponSpecial());
        Farm.IcestormArena(1, true);
        Core.Logger($"\"{itemInv.Name}\" is now Rank 10");
        if (ClassReAfter != Bot.Inventory.CurrentClass.Name)
        {
            Core.Equip(ClassReAfter);
            EnhanceEquipped(ReEnhanceAfter, ReWEnhanceAfter);
        }
        Core.Equip(EquippedWeapon);
    }

    /// <summary>
    /// Equipts the best gear available in a player's inventory/bank by checking what item has the highest boost value of the given type. Also works with damage stacking for monsters with a Race
    /// </summary>
    /// <param name="BoostType">Type "GearBoost." and then the boost of your choice in order to determine and equip the best available boosting gear</param>
    public string?[] BestGear(GearBoost BoostType, bool EquipItem = true)
    {
        if (BoostType == GearBoost.None)
            BoostType = GearBoost.dmgAll;

        if (LastBoostType == BoostType)
        {
            if (RaceBoosts.Contains(BoostType))
                return new[] { LastBestItem, LastBestItemDMGall };
            else return new[] { LastBestItem };
        }
        LastBoostType = BoostType;

        if (RaceBoosts.Contains(BoostType))
            Core.Logger("Searching for the best available gear against: " + BoostType.ToString());
        else Core.Logger("Searching for the best available gear to boost: " + BoostType.ToString());

        List<InventoryItem> InventoryData = Bot.Inventory.Items;
        List<InventoryItem> BankData = Bot.Bank.BankItems;
        List<InventoryItem> BankInvData = InventoryData.Concat(BankData).ToList();
        Dictionary<string, float> BoostedGear = new Dictionary<string, float>();
        ItemCategory BestItemCategory = ItemCategory.Unknown;
        string BestItemDMGall = "";
        float BestValueDMGall = 0F;

        foreach (InventoryItem Item in BankInvData)
        {
            if (Item.Meta != null && Item.Meta.Contains(BoostType.ToString()))
            {
                string CorrectData = Item.Meta.Split(',').ToList().First(i => i.Contains(BoostType.ToString()));
                float BoostFloat = float.Parse(CorrectData.Replace($"{BoostType.ToString()}:", ""), CultureInfo.InvariantCulture.NumberFormat);
                if (!BoostedGear.Values.Contains(BoostFloat))
                    BoostedGear.Add(Item.Name, BoostFloat);
            }
        }
        string BestItem = BoostedGear.FirstOrDefault(x => x.Value == BoostedGear.Values.Max()).Key;
        float BestValue = BoostedGear.FirstOrDefault(x => x.Value == BoostedGear.Values.Max()).Value;
        if ((BankInvData.Find(x => x.Name == BestItem) != null))
            BestItemCategory = BankInvData.First(x => x.Name == BestItem).Category;

        if (RaceBoosts.Contains(BoostType))
        {
            Dictionary<string, float> BoostedGearDMGall = new Dictionary<string, float>();

            foreach (InventoryItem Item in BankInvData)
            {
                if (Item.Meta != null && Item.Meta.Contains("dmgAll") &&
                   (WeaponCatagories.Contains(BestItemCategory) ^ WeaponCatagories.Contains(Item.Category)) &&
                    Item.Category != BestItemCategory)
                {
                    string CorrectData = Item.Meta.Split(',').ToList().First(i => i.Contains("dmgAll"));
                    float BoostFloat = float.Parse(CorrectData.Replace("dmgAll:", ""), CultureInfo.InvariantCulture.NumberFormat);
                    BoostedGearDMGall.Add(Item.Name, BoostFloat);
                }
            }
            BestItemDMGall = BoostedGearDMGall.FirstOrDefault(x => x.Value == BoostedGearDMGall.Values.Max()).Key;
            BestValueDMGall = BoostedGearDMGall.FirstOrDefault(x => x.Value == BoostedGearDMGall.Values.Max()).Value;
            if (EquipItem)
                Core.Equip(BestItemDMGall);
            Bot.Sleep(Core.ActionDelay);
        }
        if (EquipItem)
            Core.Equip(BestItem);
        LastBestItem = BestItem;

        if (RaceBoosts.Contains(BoostType))
        {
            if (BestItem == null && BestItemDMGall == null)
            {
                Core.Logger($"Best gear for [{BoostType.ToString()}] wasnt found!");
                return new[] { "" };
            }
            else if (BestItem == null && BestItemDMGall != null)
            {
                Core.Logger($"Best gear against {BoostType.ToString()} found: {BestItemDMGall} ({(BestValueDMGall - 1).ToString("+0.##%")})");
                LastBestItemDMGall = BestItemDMGall;
                return new[] { BestItemDMGall };
            }
            else if (BestItem != null && BestItemDMGall == null)
            {
                Core.Logger($"Best gear against {BoostType.ToString()} found: {BestItem} ({(BestValue - 1).ToString("+0.##%")})");
                return new[] { BestItem };
            }
            Core.Logger($"Best gear against {BoostType.ToString()} found: {BestItem} ({(BestValue - 1).ToString("+0.##%")}) & {BestItemDMGall} ({(BestValueDMGall - 1).ToString("+0.##%")})");
            Core.Logger($"Combined they give a boost of " + (BestValue * BestValueDMGall - 1).ToString("+0.##%"));
            LastBestItemDMGall = BestItemDMGall ?? "";
            return new[] { BestItem, BestItemDMGall };
        }
        if (BestItem != null)
            Core.Logger($"Best gear for [{BoostType.ToString()}] found: {BestItem} ({(BestValue - 1).ToString("+0.##%")})");
        else Core.Logger($"Best gear for [{BoostType.ToString()}] wasnt found!");
        return new[] { BestItem };
    }
    private GearBoost[] RaceBoosts =
    {
        GearBoost.Chaos,
        GearBoost.Dragonkin,
        GearBoost.Drakath,
        GearBoost.Elemental,
        GearBoost.Human,
        GearBoost.Orc,
        GearBoost.Undead
    };
    private GearBoost LastBoostType = GearBoost.None;
    private string LastBestItemDMGall = "";
    private string LastBestItem = "";

    private void _RaceGear(string Monster)
    {
        string MonsterRace = "";
        if (Monster != "*")
            MonsterRace = Bot.Monsters.MapMonsters.First(x => x.Name == Monster).Race;
        else MonsterRace = Bot.Monsters.CurrentMonsters.First().Race;

        if (MonsterRace == null || MonsterRace == "")
            return;

        string?[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
        if (_BestGear.Length == 0)
            return;
        EnhanceItem(_BestGear, CurrentClassEnh(), CurrentWeaponSpecial());
        Core.Equip(_BestGear);

    }

    private void _RaceGear(int MonsterID)
    {
        string MonsterRace = Bot.Monsters.MapMonsters.First(x => x.ID == MonsterID).Race;

        if (MonsterRace == null || MonsterRace == "")
            return;

        string?[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
        if (_BestGear.Length == 0)
            return;
        EnhanceItem(_BestGear, CurrentClassEnh(), CurrentWeaponSpecial());
        Core.Equip(_BestGear);

    }

    #endregion

    #region Kill

    public void BoostKillMonster(string map, string cell, string pad, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad);
        _RaceGear(monster);
        EnhanceEquipped(CurrentClassEnh(), CurrentWeaponSpecial());

        Core.KillMonster(map, cell, pad, monster, item, quant, isTemp, log, publicRoom);
    }

    public void BoostKillMonster(string map, string cell, string pad, int monsterID, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad);
        _RaceGear(monsterID);
        EnhanceEquipped(CurrentClassEnh(), CurrentWeaponSpecial());

        Core.KillMonster(map, cell, pad, monsterID, item, quant, isTemp, log, publicRoom);
    }

    public void BoostHuntMonster(string map, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map);
        _RaceGear(monster);
        EnhanceEquipped(CurrentClassEnh(), CurrentWeaponSpecial());

        Core.HuntMonster(map, monster, item, quant, isTemp, log, publicRoom);
    }

    public void KillUltra(string map, string cell, string pad, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = true)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;
        if (!isTemp && item != "")
            Core.AddDrop(item);

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        _RaceGear(monster);
        EnhanceEquipped(CurrentClassEnh(), CurrentWeaponSpecial());

        Core.Join(map, cell, pad, publicRoom: publicRoom);
        Core.Jump(cell, pad);

        Bot.Events.CounterAttack += _KillUltra;

        if (item == "")
        {
            if (log)
                Core.Logger($"Killing Ultra-Boss {monster}");
            int i = 0;
            Bot.Events.MonsterKilled += b => i++;
            while (i < 1)
                while (shouldAttack)
                    Bot.Player.Kill(monster);
            Core.Rest();
        }
        else
        {
            if (log)
                Core.Logger($"Killing Ultra-Boss {monster} for {item} ({quant}) [Temp = {isTemp}]");
            while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
            {
                while (shouldAttack)
                    Bot.Player.Kill(monster);
                if (!isTemp && !Core.CheckInventory(item))
                {
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Player.RejectExcept(item);
                }
                if (!Bot.Player.InCombat)
                    Core.Rest();
            }
        }

        Bot.Events.CounterAttack -= _KillUltra;
    }

    private bool shouldAttack = true;
    private void _KillUltra(ScriptInterface bot, bool faded)
    {
        string Target = "";
        if (!faded)
        {
            Target = Bot.Player.Target.Name;
            shouldAttack = false;
            Bot.Player.CancelAutoAttack();
            Bot.Player.CancelTarget();
        }
        else
        {
            if (Target != "")
                Bot.Player.Attack(Target);
            shouldAttack = true;
        }
    }

    #endregion

    #region SmartEnhance

    public void SmartEnhance(string Class)
    {
        InventoryItem SelectedClass = Bot.Inventory.Items.First(i => i.Name == Class && i.Category == ItemCategory.Class);
        if (SelectedClass.EnhancementLevel == 0)
        {
            Core.Logger("Ignore the message about the Hybrid Enhancement");
            EnhanceEquipped(EnhancementType.Hybrid);
        }
        Core.Equip(Class);
        switch (Class)
        {
            //Lucky - Spiral Carve
            case "Abyssal Angel":
            case "Abyssal Angel’s Shadow":
            case "ArchPaladin":
            case "Artifact Hunter":
            case "Assassin":
            case "BeastMaster":
            case "Berserker":
            case "Beta Berserker":
            case "BladeMaster Assassin":
            case "BladeMaster":
            case "Blood Titan":
            case "CardClasher":
            case "Chaos Avenger Member Preview":
            case "Chaos Champion Prime":
            case "Chaos Slayer":
            case "Chaos Slayer Berserker":
            case "Chaos Slayer Cleric":
            case "Chaos Slayer Mystic":
            case "Chaos Slayer Thief":
            case "Chrono Chaorruptor":
            case "Chrono Commandant":
            case "ChronoCommander":
            case "ChronoCorrupter":
            case "Chunin":
            case "Classic Alpha Pirate":
            case "Classic Barber":
            case "Classic DoomKnight":
            case "Classic Exalted Soul Cleaver":
            case "Classic Guardian":
            case "Classic Legion DoomKnight":
            case "Classic Paladin":
            case "Classic Pirate":
            case "Classic Soul Cleaver":
            case "Continuum Chronomancer":
            case "Corrupted Chronomancer":
            case "Dark Chaos Berserker":
            case "Dark Harbinger":
            case "DoomKnight":
            case "Empyrean Chronomancer":
            case "Eternal Chronomancer":
            case "Eternal Inversionist":
            case "Evolved ClawSuit":
            case "Evolved Dark Caster":
            case "Evolved Leprechaun":
            case "Exalted Harbinger":
            case "Exalted Soul Cleaver":
            case "Glacial Warlord":
            case "Great Thief":
            case "Immortal Chronomancer":
            case "Imperial Chunin":
            case "Infinite Dark Caster":
            case "Infinite Legion Dark Caster":
            case "Infinity Titan":
            case "Legion BladeMaster Assassin":
            case "Legion DoomKnight":
            case "Legion Evolved Dark Caster":
            case "Legion SwordMaster Assassin":
            case "Leprechaun":
            case "Lycan":
            case "Master Ranger":
            case "MechaJouster":
            case "Necromancer":
            case "Ninja":
            case "Ninja Warrior":
            case "NOT A MOD":
            case "Overworld Chronomancer":
            case "Pinkomancer":
            case "Prismatic ClawSuit":
            case "Ranger":
            case "Renegade":
            case "Rogue":
            case "Scarlet Sorceress":
            case "ShadowScythe General":
            case "SkyCharged Grenadier":
            case "SkyGuard Grenadier":
            case "Soul Cleaver":
            case "StarLord":
            case "StoneCrusher":
            case "SwordMaster Assassin":
            case "SwordMaster":
            case "TimeKeeper":
            case "TimeKiller":
            case "Timeless Chronomancer":
            case "Undead Goat":
            case "Undead Leperchaun":
            case "UndeadSlayer":
            case "Underworld Chronomancer":
            case "Unlucky Leperchaun":
            case "Void Highlord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            //Lucky - Mana Vamp
            case "Alpha DOOMmega":
            case "Alpha Omega":
            case "Alpha Pirate":
            case "Beast Warrior":
            case "Blood Ancient":
            case "Chaos Avenger":
            case "Chaos Shaper":
            case "Classic Defender":
            case "ClawSuit":
            case "Cryomancer Mini Pet Coming Soon":
            case "Dark Legendary Hero":
            case "Dark Ultra OmniNight":
            case "DoomKnight OverLord":
            case "Dragonslayer General":
            case "Drakel Warlord":
            case "Glacial Berserker Test":
            case "Heroic Naval Commander":
            case "Legendary Elemental Warrior":
            case "Horc Evader":
            case "Legendary Hero":
            case "Legendary Naval Commander":
            case "Legion DoomKnight Tester":
            case "Legion Revenant Member Test":
            case "Naval Commander":
            case "Paladin High Lord":
            case "Paladin":
            case "PaladinSlayer":
            case "Pirate":
            case "Pumpkin Lord":
            case "ShadowFlame DragonLord":
            case "ShadowStalker of Time":
            case "ShadowWalker of Time":
            case "ShadowWeaver of Time":
            case "Silver Paladin":
            case "Thief of Hours":
            case "Ultra Elemental Warrior":
            case "Ultra OmniKnight":
            case "Void Highlord Tester":
            case "Warlord":
            case "Warrior":
            case "WarriorScythe General":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            //Lucky - Awe Blast
            case "Arachnomancer":
            case "Bard":
            case "Chrono Assassin":
            case "Chronomancer":
            case "Chronomancer Prime":
            case "Dark Metal Necro":
            case "DeathKnight Lord":
            case "Dragon Shinobi":
            case "Dragonlord":
            case "Evolved Pumpkin Lord":
            case "DragonSoul Shinobi":
            case "Glacial Berserker":
            case "Grunge Rocker":
            case "Guardian":
            case "Heavy Metal Necro":
            case "Heavy Metal Rockstar":
            case "Lord of Order":
            case "Nechronomancer":
            case "Necrotic Chronomancer":
            case "Nu Metal Necro":
            case "Oracle":
            case "ProtoSartorium":
            case "Shadow Dragon Shinobi":
            case "Shadow Ripper":
            case "Shadow Rocker":
            case "Star Captain":
            case "Troubador of Love":
            case "Unchained Rocker":
            case "Unchained Rockstar":
            case "Yami no Ronin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            //Lucky - Health Vamp
            case "ArchFiend":
            case "Barber":
            case "Classic DragonLord":
            case "Dragonslayer":
            case "Enchanted Vampire Lord":
            case "Enforcer":
            case "Flame Dragon Warrior":
            case "Royal Vampire Lord":
            case "Rustbucket":
            case "Sentinel":
            case "Vampire":
            case "Vampire Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            //Wizard - Awe Blast
            case "Acolyte":
            case "Arcane Dark Caster":
            case "BattleMage":
            case "BattleMage of Love":
            case "Blaze Binder":
            case "Blood Sorceress":
            case "Dark BattleMage":
            case "Dark Master of Moglins":
            case "Dragon Knight":
            case "FireLord Summoner":
            case "Grim Necromancer":
            case "Healer":
            case "HighSeas Commander":
            case "Infinity Knight":
            case "Interstellar Knight":
            case "Master of Moglins":
            case "Mystical Dark Caster":
            case "Northlands Monk":
            case "Royal BattleMage":
            case "Timeless Dark Caster":
            case "Witch":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            //Wizard - Spiral Carve
            case "Chrono DataKnight":
            case "Chrono DragonKnight":
            case "Cryomancer":
            case "Dark Caster":
            case "Dark Cryomancer":
            case "Dark Lord":
            case "Darkblood StormKing":
            case "Darkside":
            case "Defender":
            case "Frost SpiritReaver":
            case "Immortal Dark Caster":
            case "Legion Paladin":
            case "Legion Revenant":
            case "LightCaster":
            case "Pink Romancer":
            case "Psionic MindBreaker":
            case "Pyromancer":
            case "Sakura Cryomancer":
            case "Troll Spellsmith":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            //Wizard - Health Vamp
            case "Daimon":
            case "Evolved Shaman":
            case "LightMage":
            case "MindBreaker":
            case "Shaman":
            case "Vindicator of They":
            case "Elemental Dracomancer":
            case "LightCaster Test":
            case "Love Caster":
            case "Mage":
            case "Sorcerer":
            case "The Collector":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            //Fighter - Awe Blast
            case "DeathKnight":
            case "Frostval Barbarian":
                EnhanceEquipped(EnhancementType.Fighter, WeaponSpecial.Awe_Blast);
                break;
            //Healer - Health Vamp
            case "Dragon of Time":
                EnhanceEquipped(EnhancementType.Healer, WeaponSpecial.Health_Vamp);
                break;
            default:
                Core.Logger($"Class: \"{Class}\" is not found in the Smart Enhance Library, please report to Lord Exelot#9674", messageBox: true);
                break;
        }
    }

    #endregion
}

public enum EnhancementType
{
    Fighter = 2,
    Thief = 3,
    Hybrid = 5,
    Wizard = 6,
    Healer = 7,
    SpellBreaker = 8,
    Lucky = 9,
}

public enum WeaponSpecial
{
    None = 0,
    Spiral_Carve = 2,
    Awe_Blast = 3,
    Health_Vamp = 4,
    Mana_Vamp = 5,
    Powerword_Die = 6
}

public enum GearBoost
{
    None,
    cp,
    gold,
    rep,
    exp,
    dmgAll,
    Chaos,
    Dragonkin,
    Drakath,
    Elemental,
    Human,
    Orc,
    Undead
}