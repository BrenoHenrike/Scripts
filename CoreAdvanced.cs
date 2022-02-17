using RBot;
using RBot.Items;
using RBot.Shops;
using System.Collections.Generic;
using System.Linq;

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
            _AutoEnhance(Empty, EquippedItems, Type, Special);
        else _AutoEnhance(EquippedWeapon, EquippedOther, Type, Special);
    }

    public void EnhanceItem(string ItemName, EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        List<InventoryItem> SelectedItem = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(i => i.Name == ItemName && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> SelectedWeapon = SelectedItem.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> SelectedOther = SelectedItem.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (SelectedItem.Count == 0)
        {
            Core.Logger($"You do not own \"{ItemName}\", enhancement failed");
            return;
        }

        if (SelectedWeapon.Count != 0)
            _AutoEnhance(SelectedWeapon, Empty, Type, Special);
        if (SelectedOther.Count != 0)
            _AutoEnhance(Empty, SelectedOther, Type, Special);
    }

    public void EnhanceItem(string[] ItemNames, EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        List<InventoryItem> SelectedItems = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(i => ItemNames.Contains(i.Name) && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> SelectedWeapons = SelectedItems.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> SelectedOthers = SelectedItems.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (SelectedItems.Count == 0)
        {
            Core.Logger($"You do not own \"{ItemNames}\", enhancement failed");
            return;
        }

        if (SelectedWeapons.Count != 0)
            _AutoEnhance(SelectedWeapons, Empty, Type, Special);
        if (SelectedOthers.Count != 0)
            _AutoEnhance(Empty, SelectedOthers, Type, Special);
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
    private List<InventoryItem> Empty = new List<InventoryItem>();
    private void _AutoEnhance(List<InventoryItem> WeaponList, List<InventoryItem> OtherList, EnhancementType Type, WeaponSpecial Special)
    {
        List<InventoryItem> FlexibleList = Special == WeaponSpecial.None ? WeaponList.Concat(OtherList).ToList() : OtherList;

        if (WeaponList.Count == 0 && OtherList.Count == 0)
        {
            Core.Logger("Please report what you were trying to enhance to Lord Exelot#9674, enchantment failed");
            return;
        }

        //Gear
        if (FlexibleList.Count != 0)
        {
            Core.Logger($"Best Enhancement of: {Type.ToString()}");
            if (Type == EnhancementType.Fighter) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 768 : 141);
            else if (Type == EnhancementType.Thief) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 767 : 142);
            else if (Type == EnhancementType.Wizard) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 765 : 144);
            else if (Type == EnhancementType.Healer) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 762 : 145);
            else if (Type == EnhancementType.Hybrid) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 766 : 143);
            else if (Type == EnhancementType.Lucky) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 763 : 147);
            else if (Type == EnhancementType.SpellBreaker) __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 146 : 146);
        }
        //Weapon Specials

        if (WeaponList.Count != 0 && Special != WeaponSpecial.None)
        {
            Core.Logger($"Best Enhancement of: {Type.ToString()}, {Special.ToString().Replace('_', ' ')}");
            if (Type == EnhancementType.Fighter) __AutoEnhance(WeaponList, 635, "museum");
            else if (Type == EnhancementType.Thief) __AutoEnhance(WeaponList, 637, "museum");
            else if (Type == EnhancementType.Wizard || Type == EnhancementType.SpellBreaker) __AutoEnhance(WeaponList, 636, "museum");
            else if (Type == EnhancementType.Healer) __AutoEnhance(WeaponList, 638, "museum");
            else if (Type == EnhancementType.Hybrid) __AutoEnhance(WeaponList, 633, "museum");
            else if (Type == EnhancementType.Lucky) __AutoEnhance(WeaponList, 639, "museum");
        }

        void __AutoEnhance(List<InventoryItem> Input, int ShopID, string Map = null)
        {
            List<ShopItem> ShopItems = new List<ShopItem>();

            foreach (InventoryItem Item in Input)
            {
                Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Searching]");
                Core.CheckInventory(Item.Name);

                if (Map != null)
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
                        SelectedEhn = BestTwo.Find(x => x.Upgrade);
                    else SelectedEhn = BestTwo.Find(x => !x.Upgrade);
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

        InventoryItem itemInv = Bot.Inventory.Items.Find(i => i.Name.ToLower() == ClassName.ToLower() && i.Category == ItemCategory.Class);
        if (itemInv == null)
            Core.Logger($"\"{itemInv.Name}\" is not a valid Class", messageBox: true, stopBot: true);
        if (itemInv.Quantity == 302500)
        {
            Core.Logger($"\"{itemInv.Name}\" is already Rank 10");
            return;
        }
        string ClassReAfter = Bot.Inventory.CurrentClass.Name;
        EnhancementType ReEnhanceAfter = _CurrentClassEnh();
        WeaponSpecial ReWEnhanceAfter = _CurrentWeaponSpecial();
        SmartEnhance(ClassName);
        EnhanceItem(BestGear(GearBoost.cp), _CurrentClassEnh(), _CurrentWeaponSpecial());
        Bot.Player.EquipItem(itemInv.Name);
        Farm.IcestormArena(1, true);
        Core.Logger($"\"{itemInv.Name}\" is now Rank 10");
        Bot.Player.EquipItem(ClassReAfter);
        EnhanceEquipped(ReEnhanceAfter, ReWEnhanceAfter);
    }

    /// <summary>
    /// Equipts the best gear available in a player's inventory/bank by checking what item has the highest boost value of the given type. Also works with damage stacking for monsters with a Race
    /// </summary>
    /// <param name="BoostType">Type "GearBoost." and then the boost of your choice in order to determine and equip the best available boosting gear</param>
    public string[] BestGear(GearBoost BoostType)
    {
        if (BoostType == GearBoost.None)
            return new[] { "" };

        if (LastBoostType == BoostType)
        {
            if (RaceBoosts.Contains(BoostType))
                return new[] { LastBestItem, LastBestItemDMGall };
            else return new[] { LastBestItem };
        }
        LastBoostType = BoostType;

        InventoryItem[] InventoryData = Bot.Inventory.Items.ToArray();
        InventoryItem[] BankData = Bot.Bank.BankItems.ToArray();
        InventoryItem[] BankInvData = InventoryData.Concat(BankData).ToArray();
        Dictionary<string, float> BoostedGear = new Dictionary<string, float>();
        string BestItemDMGall = null;

        foreach (InventoryItem Item in BankInvData)
        {
            if (Item.Meta != null && Item.Meta.Contains(BoostType.ToString()))
            {
                string CorrectData = Array.Find(Item.Meta.Split(','), i => i.Contains(BoostType.ToString()));
                float BoostFloat = float.Parse(CorrectData.Replace($"{BoostType.ToString()}:", ""));
                BoostedGear.Add(Item.Name, BoostFloat);
            }
        }
        string BestItem = BoostedGear.FirstOrDefault(x => x.Value == BoostedGear.Values.Max()).Key;
        ItemCategory BestItemCatagory = BankInvData.First(x => x.Name == BestItem).Category;

        if (RaceBoosts.Contains(BoostType))
        {
            Dictionary<string, float> BoostedGearDMGall = new Dictionary<string, float>();

            foreach (InventoryItem Item in BankInvData)
            {
                if (Item.Meta != null && Item.Meta.Contains("dmgAll") &&
                   (WeaponCatagories.Contains(BestItemCatagory) ^ WeaponCatagories.Contains(Item.Category)) &&
                    Item.Category != BestItemCatagory)
                {
                    string CorrectData = Array.Find(Item.Meta.Split(','), i => i.Contains("dmgAll"));
                    float BoostFloat = float.Parse(CorrectData.Replace($"dmgAll:", ""));
                    BoostedGearDMGall.Add(Item.Name, BoostFloat);
                }
            }
            BestItemDMGall = BoostedGearDMGall.FirstOrDefault(x => x.Value == BoostedGearDMGall.Values.Max()).Key;
            Core.JumpWait();
            Core.CheckInventory(BestItemDMGall);
            Bot.Player.EquipItem(BestItemDMGall);
            Bot.Sleep(Core.ActionDelay);
        }
        Core.JumpWait();
        Core.CheckInventory(BestItem);
        Bot.Player.EquipItem(BestItem);
        if (RaceBoosts.Contains(BoostType))
            return new[] { BestItem, BestItemDMGall };
        return new[] { BestItem };
    }
    private GearBoost[] RaceBoosts =
    {
        GearBoost.Chaos,
        GearBoost.Dragonkin,
        GearBoost.Elemental,
        GearBoost.Human,
        GearBoost.Undead
    };
    private GearBoost LastBoostType = GearBoost.None;
    private string LastBestItemDMGall;
    private string LastBestItem;

    private void _RaceGear(string Monster)
    {
        string MonsterRace = Bot.Monsters.MapMonsters.Find(x => x.Name == Monster).Race;
        if (MonsterRace != null)
        {
            string[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
            EnhanceItem(_BestGear, _CurrentClassEnh(), _CurrentWeaponSpecial());
            foreach (string Item in _BestGear)
                if (!Bot.Inventory.IsEquipped(Item))
                    Bot.Player.EquipItem(Item);
        }
    }

    private void _RaceGear(int MonsterID)
    {
        string MonsterRace = Bot.Monsters.MapMonsters.Find(x => x.ID == MonsterID).Race;
        if (MonsterRace != null)
        {
            string[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
            EnhanceItem(_BestGear, _CurrentClassEnh(), _CurrentWeaponSpecial());
            foreach (string Item in _BestGear)
                if (!Bot.Inventory.IsEquipped(Item))
                    Bot.Player.EquipItem(Item);
        }
    }

    private EnhancementType _CurrentClassEnh()
    {
        int EnhPatternID = Bot.GetGameObject<int>($"world.invTree.{Bot.Inventory.CurrentClass.ID}.EnhPatternID");
        if (EnhPatternID == 1 || EnhPatternID == 23)
            EnhPatternID = 9;
        return (EnhancementType)EnhPatternID;
    }

    private WeaponSpecial _CurrentWeaponSpecial()
    {
        InventoryItem EquippedWeapon = Bot.Inventory.Items.Find(i => i.Equipped == true && WeaponCatagories.Contains(i.Category));
        int ProcID = Bot.GetGameObject<int>($"world.invTree.{EquippedWeapon.ID}.ProcID");
        return (WeaponSpecial)ProcID;
    }

    #endregion

    #region Kill

    public void BoostKillMonster(string map, string cell, string pad, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad);
        _RaceGear(monster);
        EnhanceEquipped(_CurrentClassEnh(), _CurrentWeaponSpecial());

        Core.KillMonster(map, cell, pad, monster, item, quant, isTemp, log, publicRoom);
    }

    public void BoostKillMonster(string map, string cell, string pad, int monsterID, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad);
        _RaceGear(monsterID);
        EnhanceEquipped(_CurrentClassEnh(), _CurrentWeaponSpecial());

        Core.KillMonster(map, cell, pad, monsterID, item, quant, isTemp, log, publicRoom);
    }

    public void BoostHuntMonster(string map, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;

        Core.Join(map);
        _RaceGear(monster);
        EnhanceEquipped(_CurrentClassEnh(), _CurrentWeaponSpecial());

        Core.HuntMonster(map, monster, item, quant, isTemp, log, publicRoom);
    }

    public void KillUltra(string map, string cell, string pad, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = true)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;
        if (!isTemp && item != null)
            Core.AddDrop(item);

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        _RaceGear(monster);
        EnhanceEquipped(_CurrentClassEnh(), _CurrentWeaponSpecial());

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        Bot.Events.CounterAttack += _KillUltra;

        if (item == null)
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
        string Target = null;
        if (!faded)
        {
            Target = Bot.Player.Target.Name;
            shouldAttack = false;
            Bot.Player.CancelAutoAttack();
            Bot.Player.CancelTarget();
        }
        else
        {
            if (Target != null)
                Bot.Player.Attack(Target);
            shouldAttack = true;
        }
    }

    #endregion

    #region SmartEnhance

    private void SmartEnhance(string Class)
    {
        InventoryItem SelectedClass = Bot.Inventory.Items.Find(i => i.Name == Class && i.Category == ItemCategory.Class);
        if (SelectedClass.EnhancementLevel == 0)
        {
            Core.Logger("Ignore the message about the Hybrid Enhancement");
            EnhanceEquipped(EnhancementType.Hybrid);
        }
        if (!Bot.Inventory.IsEquipped(Class))
            Bot.Player.EquipItem(Class);
        switch (Class)
        {
            //Lucky
            case "Abyssal Angel":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Abyssal Angel’s Shadow":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Alpha DOOMmega":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Alpha Omega":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Alpha Pirate":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Arachnomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "ArchFiend":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "ArchPaladin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Artifact Hunter":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Assassin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Barber":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Bard":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Beast Warrior":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "BeastMaster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Berserker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Beta Berserker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "BladeMaster Assassin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "BladeMaster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Blood Ancient":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Blood Titan":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "CardClasher":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Chaos Avenger":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Chaos Avenger Member Preview":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Chaos Champion Prime":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Chaos Shaper":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Chaos Slayer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Chrono Assassin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Chrono Chaorruptor":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Chrono Commandant":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "ChronoCommander":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "ChronoCorrupter":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Chronomancer Prime":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Chunin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Alpha Pirate":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Barber":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Defender":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Classic DoomKnight":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic DragonLord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Classic Exalted Soul Cleaver":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Guardian":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Legion DoomKnight":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Paladin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Pirate":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Classic Soul Cleaver":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "ClawSuit":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Continuum Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Corrupted Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Cryomancer Mini Pet Coming Soon":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Dark Chaos Berserker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Dark Harbinger":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Dark Legendary Hero":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Dark Metal Necro":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Dark Ultra OmniNight":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "DeathKnight Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "DoomKnight OverLord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "DoomKnight":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Dragon Shinobi":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Dragonlord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Dragonslayer General":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Dragonslayer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "DragonSoul Shinobi":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Drakel Warlord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Empyrean Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Enchanted Vampire Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Enforcer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Eternal Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Eternal Inversionist":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Evolved ClawSuit":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Evolved Dark Caster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Evolved Leprechaun":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Evolved Pumpkin Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Exalted Harbinger":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Exalted Soul Cleaver":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Flame Dragon Warrior":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Glacial Berserker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Glacial Berserker Test":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Glacial Warlord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Great Thief":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Grunge Rocker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Guardian":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Heavy Metal Necro":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Heavy Metal Rockstar":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Heroic Naval Commander":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Horc Evader":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Immortal Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Imperial Chunin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Infinite Dark Caster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Infinite Legion Dark Caster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Infinity Titan":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Legendary Elemental Warrior":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Legendary Hero":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Legendary Naval Commander":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Legion BladeMaster Assassin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Legion DoomKnight":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Legion DoomKnight Tester":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Legion Evolved Dark Caster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Legion Revenant Member Test":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Legion SwordMaster Assassin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Leprechaun":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Lord of Order":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Lycan":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Master Ranger":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "MechaJouster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Naval Commander":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Nechronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Necromancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Necrotic Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Ninja":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Ninja Warrior":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "NOT A MOD":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Nu Metal Necro":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Oracle":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Overworld Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Paladin High Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Paladin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "PaladinSlayer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Pinkomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Pirate":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Prismatic ClawSuit":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "ProtoSartorium":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Pumpkin Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Ranger":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Renegade":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Rogue":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Royal Vampire Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Rustbucket":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Scarlet Sorceress":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Sentinel":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Shadow Dragon Shinobi":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Shadow Ripper":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Shadow Rocker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "ShadowFlame DragonLord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "ShadowScythe General":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "ShadowStalker of Time":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "ShadowWalker of Time":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "ShadowWeaver of Time":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Silver Paladin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "SkyCharged Grenadier":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "SkyGuard Grenadier":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Soul Cleaver":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Star Captain":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "StarLord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "StoneCrusher":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "SwordMaster Assassin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "SwordMaster":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Thief of Hours":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "TimeKeeper":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "TimeKiller":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Timeless Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Troubador of Love":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Ultra Elemental Warrior":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Ultra OmniKnight":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Unchained Rocker":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Unchained Rockstar":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            case "Undead Goat":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Undead Leperchaun":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "UndeadSlayer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Underworld Chronomancer":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Unlucky Leperchaun":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Vampire":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Vampire Lord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            case "Void Highlord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            case "Void Highlord Tester":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Warlord":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Warrior":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "WarriorScythe General":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            case "Yami no Ronin":
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            //Wizard
            case "Acolyte":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Arcane Dark Caster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "BattleMage":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "BattleMage of Love":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Blaze Binder":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Blood Sorceress":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Chrono DataKnight":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Chrono DragonKnight":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Cryomancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Daimon":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Mana_Vamp);
                break;
            case "Dark BattleMage":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Dark Caster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Dark Cryomancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Dark Lord":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Dark Master of Moglins":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Darkblood StormKing":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Darkside":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Defender":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Dragon Knight":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Elemental Dracomancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            case "Evolved Shaman":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Mana_Vamp);
                break;
            case "FireLord Summoner":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Frost SpiritReaver":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Grim Necromancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Healer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "HighSeas Commander":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Immortal Dark Caster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Infinity Knight":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Interstellar Knight":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Legion Paladin":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Legion Revenant":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "LightCaster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "LightCaster Test":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            case "LightMage":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Mana_Vamp);
                break;
            case "Love Caster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            case "Mage":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            case "Master of Moglins":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "MindBreaker":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Mana_Vamp);
                break;
            case "Mystical Dark Caster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Northlands Monk":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Pink Romancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Psionic MindBreaker":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Pyromancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Royal BattleMage":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Sakura Cryomancer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Shaman":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Mana_Vamp);
                break;
            case "Sorcerer":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            case "The Collector":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            case "Timeless Dark Caster":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            case "Troll Spellsmith":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            case "Vindicator of They":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Mana_Vamp);
                break;
            case "Witch":
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            //Fighter
            case "DeathKnight":
                EnhanceEquipped(EnhancementType.Fighter, WeaponSpecial.Awe_Blast);
                break;
            case "Frostval Barbarian":
                EnhanceEquipped(EnhancementType.Fighter, WeaponSpecial.Awe_Blast);
                break;
            //Healer
            case "Dragon of Time":
                EnhanceEquipped(EnhancementType.Healer, WeaponSpecial.Health_Vamp);
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
    Elemental,
    Human,
    Undead
}