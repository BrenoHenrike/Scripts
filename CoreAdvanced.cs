//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
using RBot.Items;
using RBot.Shops;
using RBot.Options;
using System.Globalization;
using System.Reflection;

public class CoreAdvanced
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.RunCore();
    }

    #region Enhancement

    /// <summary>
    /// Enhances your currently equipped gear
    /// </summary>
    /// <param name="Type">Example: EnhancementType.Lucky , replace Lucky with whatever enhancement you want to have it use</param>
    /// <param name="Special">Example: WeaponSpecial.Spiral_Carve , replace Spiral_Carve with whatever weapon special you want to have it use</param>
    public void EnhanceEquipped(EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        if (Core.CBO_Active && Core.CBOBool("DisableAutoEnhance"))
            return;

        List<InventoryItem> EquippedItems = Bot.Inventory.Items.FindAll(i => i.Equipped == true && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> EquippedWeapon = EquippedItems.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> EquippedOther = EquippedItems.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (Special == WeaponSpecial.None)
            _AutoEnhance(Core.EmptyList, EquippedItems, Type, Special);
        else _AutoEnhance(EquippedWeapon, EquippedOther, Type, Special);
    }

    /// <summary>
    /// Enhances a selected item
    /// </summary>
    /// <param name="ItemName">Name of the item you want to enhance</param>
    /// <param name="Type">Example: EnhancementType.Lucky , replace Lucky with whatever enhancement you want to have it use</param>
    /// <param name="Special">Example: WeaponSpecial.Spiral_Carve , replace Spiral_Carve with whatever weapon special you want to have it use</param>
    public void EnhanceItem(string? ItemName, EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        if (Core.CBO_Active && Core.CBOBool("DisableAutoEnhance"))
            return;

        if (string.IsNullOrEmpty(ItemName))
            return;

        List<InventoryItem> SelectedItem = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(i => i.Name.ToLower() == ItemName.ToLower() && EnhanceableCatagories.Contains(i.Category));
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

    /// <summary>
    /// Enhances multiple selected items
    /// </summary>
    /// <param name="ItemName">Names of the items you want to enhance (Case-Sensitive)</param>
    /// <param name="Type">Example: EnhancementType.Lucky , replace Lucky with whatever enhancement you want to have it use</param>
    /// <param name="Special">Example: WeaponSpecial.Spiral_Carve , replace Spiral_Carve with whatever weapon special you want to have it use</param>
    public void EnhanceItem(string[] ItemNames, EnhancementType Type, WeaponSpecial Special = WeaponSpecial.None)
    {
        if (Core.CBO_Active && Core.CBOBool("DisableAutoEnhance"))
            return;

        List<InventoryItem> SelectedItems = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList().FindAll(i => ItemNames.Contains(i.Name) && EnhanceableCatagories.Contains(i.Category));
        List<InventoryItem> SelectedWeapons = SelectedItems.FindAll(i => WeaponCatagories.Contains(i.Category));
        List<InventoryItem> SelectedOthers = SelectedItems.FindAll(i => !WeaponCatagories.Contains(i.Category));

        if (SelectedItems.Count == 0)
        {
            if (!SelectedItems.Any(x => x.Name == ""))
                Core.Logger($"You do not own \"{ItemNames[0]}\", enhancement failed");
            return;
        }

        if (SelectedWeapons.Count != 0)
            _AutoEnhance(SelectedWeapons, Core.EmptyList, Type, Special);
        if (SelectedOthers.Count != 0)
            _AutoEnhance(Core.EmptyList, SelectedOthers, Type, Special);
    }

    /// <summary>
    /// Determines what Enhancement Type the player has on their currently equipped class
    /// </summary>
    /// <returns>Returns the equipped Enhancement Type</returns>
    public EnhancementType CurrentClassEnh()
    {
        int EnhPatternID = Bot.Inventory.CurrentClass.EnhancementPatternID;
        if (EnhPatternID == 1 || EnhPatternID == 23)
            EnhPatternID = 9;
        return (EnhancementType)EnhPatternID;
    }

    /// <summary>
    /// Determines what Weapon Special the player has on their currently equipped weapon
    /// </summary>
    /// <returns>Returns the equipped Weapon Special</returns>
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

        if (WeaponList.Count == 0 && OtherList.Count == 0)
        {
            Core.Logger("Please report what you were trying to enhance to Lord Exelot#9674, enchantment failed");
            return;
        }

        //Gear
        if (FlexibleList.Count != 0)
        {
            Core.Logger($"Best Enhancement of: {Type.ToString()}");
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
            if (Bot.Player.Level == WeaponList.First().EnhancementLevel && (int)Type == WeaponList.First().EnhancementPatternID &&
                   (WeaponCatagories.Contains(WeaponList.First().Category) ? (int)Special == Bot.GetGameObject<int>($"world.invTree.{WeaponList.First().ID}.ProcID") : true))
                i++;
            else
            {
                Core.Logger($"Best Enhancement of: {Type.ToString()} + {Special.ToString().Replace('_', ' ')}");
                if (Type == EnhancementType.Fighter)
                    __AutoEnhance(WeaponList, 635);
                else if (Type == EnhancementType.Thief)
                    __AutoEnhance(WeaponList, 637);
                else if (Type == EnhancementType.Wizard || Type == EnhancementType.SpellBreaker)
                    __AutoEnhance(WeaponList, 636);
                else if (Type == EnhancementType.Healer)
                    __AutoEnhance(WeaponList, 638);
                else if (Type == EnhancementType.Hybrid)
                    __AutoEnhance(WeaponList, 633);
                else if (Type == EnhancementType.Lucky)
                    __AutoEnhance(WeaponList, 639);
            }
        }

        if (i > 0)
            Core.Logger($"Skipped enhancement for {i} item{(i > 1 ? 's' : null)}");
        if (i != WeaponList.Count + OtherList.Count)
            Core.Logger("Enhancements complete");

        void __AutoEnhance(List<InventoryItem> Input, int ShopID)
        {
            Core.JumpWait();

            Bot.Shops.Load(ShopID);
            List<ShopItem> ShopItems = Bot.Shops.ShopItems;

            foreach (InventoryItem Item in Input)
            {
                Core.CheckInventory(Item.Name);

                if (Bot.Player.Level == Item.EnhancementLevel && (int)Type == Item.EnhancementPatternID &&
                   (WeaponCatagories.Contains(Item.Category) ? (int)Special == Bot.GetGameObject<int>($"world.invTree.{Item.ID}.ProcID") : true))
                {
                    i++;
                    continue;
                }

                Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Searching]");

                List<ShopItem> AvailableEnh = new List<ShopItem>();

                foreach (ShopItem Enh in ShopItems)
                {
                    //Filtering out Member if you're non Member
                    if ((Core.IsMember || (!Core.IsMember && !Enh.Upgrade)) &&
                        //Filtering out the ones you're not high enough level for
                        Enh.Level <= Bot.Player.Level &&
                        //If Input is just the weapon, and if the name of the Special is seen in the items
                        ((Input.Count == 1 && isWeapon(Item) && Enh.Name.Contains(Special.ToString().Replace('_', ' '))) ||
                            //If Input is not just weapon, then
                            ((Input.Count > 1 || !isWeapon(Item)) &&
                                //If the Enhancement is for Classes
                                (Enh.Name.Contains("Armor") && Item.Category == ItemCategory.Class) ||
                                //If the Enhancement is for Helmets
                                (Enh.Name.Contains("Helm") && Item.Category == ItemCategory.Helm) ||
                                //If the Enhancement is for Capes
                                (Enh.Name.Contains("Cape") && Item.Category == ItemCategory.Cape) ||
                                //If the Enhancement is for Weapons
                                (Enh.Name.Contains("Weapon") && isWeapon(Item)))))
                        //Add to the list of selectable Enhancements
                        AvailableEnh.Add(Enh);
                }

                List<ShopItem> ListMinToMax = AvailableEnh.OrderBy(x => x.Level).ToList();
                List<ShopItem> BestTwo = ListMinToMax.Skip(ListMinToMax.Count - 2).ToList();
                ShopItem SelectedEhn = new ShopItem();

                if (BestTwo.First().Level == BestTwo.Last().Level)
                    if (Core.IsMember)
                        SelectedEhn = BestTwo.First(x => x.Upgrade);
                    else SelectedEhn = BestTwo.First(x => !x.Upgrade);
                else SelectedEhn = BestTwo.OrderByDescending(x => x.Level).First();

                Bot.SendPacket($"%xt%zm%enhanceItemShop%{Bot.Map.RoomID}%{Item.ID}%{SelectedEhn.ID}%{ShopID}%");
                Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Applied]");
                Bot.Sleep(Core.ActionDelay);
            }
        }
    }

    #endregion

    #region Gear

    /// <summary>
    /// Ranks up your class
    /// </summary>
    /// <param name="ClassName">Name of the class you want it to rank up</param>
    public void rankUpClass(string ClassName)
    {
        Bot.Wait.ForPickup(ClassName);

        if (!Core.CheckInventory(ClassName))
            Core.Logger($"Cant level up \"{ClassName}\" because you do not own it.", messageBox: true, stopBot: true);

        InventoryItem itemInv = Bot.Inventory.Items.First(i => i.Name.ToLower() == ClassName.ToLower() && i.Category == ItemCategory.Class);
        GearStore();

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
        string[]? CPBoost = BestGear(GearBoost.cp);
        EnhanceItem(CPBoost, CurrentClassEnh(), CurrentWeaponSpecial());
        Core.Equip(CPBoost);
        Farm.IcestormArena(1, true);
        Core.Logger($"\"{itemInv.Name}\" is now Rank 10");
        GearStore(true);
    }

    /// <summary>
    /// Equips the best gear available in a player's inventory/bank by checking what item has the highest boost value of the given type. Also works with damage stacking for monsters with a Race
    /// </summary>
    /// <param name="BoostType">Type "GearBoost." and then the boost of your choice in order to determine and equip the best available boosting gear</param>
    /// <param name="EquipItem">To Equip the found item(s) or not</param>
    public string[] BestGear(GearBoost BoostType, bool EquipItem = true)
    {
        if (Core.CBO_Active && Core.CBOBool("DisableBestGear"))
            return new[] { "" };

        if (BoostType == GearBoost.None)
            BoostType = GearBoost.dmgAll;
        if (LastBoostType == BoostType)
            return LastBestGear ?? new[] { "" };
        LastBoostType = BoostType;

        Core.Logger("Searching for the best available gear " + (isRacial() ? "against " : "for ") + BoostType.ToString());

        List<InventoryItem> BankInvData = Bot.Inventory.Items.Concat(Bot.Bank.BankItems).ToList();
        float TotalBoostValue = 0F;
        string[] ArrayOutput = new string[0];

        if (!isRacial())
        {
            Dictionary<string, float> BoostedGear = new();

            foreach (InventoryItem Item in BankInvData)
                if (Item.Meta != null && Item.Meta.Contains(BoostType.ToString()))
                    BoostedGear.Add(Item.Name, getBoostFloat(Item, BoostType.ToString()));

            if (BoostedGear.Count != 0)
            {
                TotalBoostValue = BoostedGear.MaxBy(x => x.Value).Value;
                ArrayOutput = new[] { BoostedGear.MaxBy(x => x.Value).Key };

                Dictionary<string, float> BoostedGearMultiple = BoostedGear.Where(x => x.Value == TotalBoostValue).ToDictionary(x => x.Key, y => y.Value);
                if (BoostedGearMultiple.Count() > 1)
                {
                    if (BoostedGearMultiple.Keys.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x)))
                        ArrayOutput = new[] { BoostedGearMultiple.First(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.Key)).Key };
                    else foreach (KeyValuePair<string, float> Gear in BoostedGearMultiple)
                        {
                            InventoryItem Item = BankInvData.First(x => x.Name == Gear.Key && x.Meta.Contains(BoostType.ToString()));
                            InventoryItem equippedItem = BankInvData.First(x => x.Equipped && x.ItemGroup == Item.ItemGroup);
                            if (Item != null && equippedItem != null
                                && Bot.GetGameObject<int>($"world.invTree.{Item.ID}.EnhID") == Bot.GetGameObject<int>($"world.invTree.{equippedItem.ID}.EnhID"))
                            {
                                ArrayOutput = new[] { Item.Name };
                                break;
                            }
                        }
                }

                if (EquipItem)
                {
                    //foreach (string Item in ArrayOutput)
                    //{
                    //    InventoryItem invItem = BankInvData.First(x => x.Name == Item);
                    //    if (!invItem.Equipped)
                    //        continue;

                    //    if (invItem.ItemGroup == "Weapon")
                    //    {
                    //        List<InventoryItem> theList = new();
                    //        theList.AddRange(Bot.Inventory.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));
                    //        if (theList.Count == 0)
                    //            theList.AddRange(Bot.Bank.BankItems.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));

                    //        if (theList.Count != 0)
                    //            Core.Equip(theList.First().Name);
                    //        else
                    //        {
                    //            Core.BuyItem(Bot.Map.Name, 299, "Battle Oracle Battlestaff");
                    //            Core.Equip("Battle Oracle Battlestaff");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Core.JumpWait();
                    //        Bot.SendPacket($"%xt%zm%unequipItem%{Bot.Map.RoomID}%{invItem.ID}%");
                    //    }
                    //}
                    Core.Equip(ArrayOutput);
                }

                LastBestGear = ArrayOutput;
            }
        }
        else
        {
            List<BestGearData> BestGearData = new();
            List<InventoryItem> AllRaceItems = new();
            List<InventoryItem> AllDMGallItems = new();

            foreach (InventoryItem Item in BankInvData)
            {
                if (!String.IsNullOrEmpty(Item.Meta))
                {
                    if (Item.Meta.Contains(BoostType.ToString()))
                        AllRaceItems.Add(Item);
                    if (Item.Meta.Contains("dmgAll"))
                        AllDMGallItems.Add(Item);
                }
            }

            if (AllRaceItems.Count != 0)
                foreach (InventoryItem iRace in AllRaceItems)
                {
                    float iRaceBoostFloat = getBoostFloat(iRace, BoostType.ToString());
                    BestGearData.Add(new(iRace.Name, "", iRaceBoostFloat));

                    foreach (InventoryItem iAll in AllDMGallItems)
                    {
                        if (iRace.ID == iAll.ID || iRace.Category == iAll.Category || iRace.ItemGroup == iAll.ItemGroup)
                            continue;

                        float iAllBoostFloat = getBoostFloat(iAll, "dmgAll");
                        BestGearData.Add(new(iRace.Name, iAll.Name, (iRaceBoostFloat * iAllBoostFloat)));
                        if (!BestGearData.Any(x => x.iDMGall == iAll.Name && x.iRace == ""))
                            BestGearData.Add(new("", iAll.Name, iAllBoostFloat));
                    }
                }
            else foreach (InventoryItem iAll in AllDMGallItems)
                    BestGearData.Add(new("", iAll.Name, getBoostFloat(iAll, "dmgAll")));

            BestGearData FinalCombo = BestGearData.MaxBy(x => x.BoostValue) ?? new("", "", 0);
            TotalBoostValue = FinalCombo.BoostValue;
            string BestRace = FinalCombo.iRace ?? "";
            string BestDMGAll = FinalCombo.iDMGall ?? "";
            List<string> ListOutput = new();

            //List<BestGearData> BestBestGearData = BestGearData.Where(x => x.BoostValue == TotalBoostValue).ToList();
            //if (BestBestGearData.Count() > 1)
            //{
            //    if (BestBestGearData.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.iRace)
            //     || BestBestGearData.Any(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.iDMGall))))
            //        ArrayOutput = new[] { BestBestGearData.First(x => BankInvData.Where(i => i.Equipped).Select(x => x.Name).Contains(x.Key)).Key };
            //    foreach (BestGearData Gear in BestGearData.Where(x => x.BoostValue == TotalBoostValue))
            //    {
            //        InventoryItem Item = BankInvData.First(x => x.Name == Gear.iRace || x.Name == Gear.iDMGall);
            //        InventoryItem equippedWeapon = BankInvData.First(x => x.Equipped == true && x.ItemGroup == "Weapon");
            //        if (Item != null && equippedWeapon != null
            //            && Bot.GetGameObject<int>($"world.invTree.{Item.ID}.EnhID") == Bot.GetGameObject<int>($"world.invTree.{equippedWeapon.ID}.EnhID"))
            //        {
            //            ArrayOutput = new[] { Item.Name };
            //            break;
            //        }
            //    }
            //}

            if (!String.IsNullOrEmpty(BestRace))
                ListOutput.Add(BestRace);
            if (!String.IsNullOrEmpty(BestDMGAll))
                ListOutput.Add(BestDMGAll);
            ArrayOutput = ListOutput.ToArray();

            if (EquipItem)
            {
                //foreach (string Item in ArrayOutput)
                //{
                //    InventoryItem invItem = BankInvData.First(x => x.Name == Item);
                //    if (!invItem.Equipped)
                //        continue;

                //    if (invItem.ItemGroup == "Weapon")
                //    {
                //        List<InventoryItem> theList = new();
                //        theList.AddRange(Bot.Inventory.Items.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));
                //        if (theList.Count == 0)
                //            theList.AddRange(Bot.Bank.BankItems.Where(x => x.Name != Item && x.ItemGroup == "Weapon" && x.EnhancementLevel > 0 && Core.IsMember ? true : !x.Upgrade));

                //        if (theList.Count != 0)
                //            Core.Equip(theList.First().Name);
                //        else
                //        {
                //            Core.BuyItem(Bot.Map.Name, 299, "Battle Oracle Battlestaff");
                //            Core.Equip("Battle Oracle Battlestaff");
                //        }
                //    }
                //    else
                //    {
                //        Core.JumpWait();
                //        Bot.SendPacket($"%xt%zm%unequipItem%{Bot.Map.RoomID}%{invItem.ID}%");
                //    }
                //}
                Core.Equip(ArrayOutput);
            }
            LastBestGear = ArrayOutput;
        }

        if (ArrayOutput.Length == 0)
        {
            Core.Logger("Best gear " + (isRacial() ? "against " : "for ") + $"{BoostType.ToString()} wasnt found!");
            return new[] { "" };
        }
        else if (ArrayOutput.Length == 1)
            Core.Logger("Best gear " + (isRacial() ? "against " : "for ") + $"{BoostType.ToString()} found: {ArrayOutput[0]} ({(TotalBoostValue - 1).ToString("+0.##%")})");
        else if (ArrayOutput.Length == 2)
            Core.Logger($"Best gear against {BoostType.ToString()} found: {ArrayOutput[0]} + {ArrayOutput[1]} ({(TotalBoostValue - 1).ToString("+0.##%")})");
        return ArrayOutput;

        bool isRacial()
        {
            return RaceBoosts.Contains(BoostType);
        }

        float getBoostFloat(InventoryItem Item, string BoostType)
        {
            string CorrectData = Item.Meta.Split(',').ToList().First(i => i.Contains(BoostType));
            return float.Parse(CorrectData.Replace($"{BoostType}:", ""), CultureInfo.InvariantCulture.NumberFormat);
        }
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
    private string[]? LastBestGear;
    private class BestGearData
    {
        public string? iRace { get; set; }
        public string? iDMGall { get; set; }
        public float BoostValue { get; set; }
        public BestGearData(string iRace, string iDMGall, float BoostValue)
        {
            this.iRace = iRace;
            this.iDMGall = iDMGall;
            this.BoostValue = BoostValue;
        }
    }

    /// <summary>
    /// Stores the gear a player has so that it can later restore these
    /// </summary>
    /// <param name="Restore">Set true to restore previously stored gear</param>
    public void GearStore(bool Restore = false)
    {
        if (!Restore)
        {
            foreach (InventoryItem Item in Bot.Inventory.Items.FindAll(i => i.Equipped == true))
                ReEquippedItems.Add(Item.Name);
            ReEnhanceAfter = CurrentClassEnh();
            ReWEnhanceAfter = CurrentWeaponSpecial();
        }
        else
        {
            Core.Equip(ReEquippedItems.ToArray());
            EnhanceEquipped(ReEnhanceAfter, ReWEnhanceAfter);
        }
    }
    private List<string> ReEquippedItems = new List<string>();
    private EnhancementType ReEnhanceAfter = EnhancementType.Lucky;
    private WeaponSpecial ReWEnhanceAfter = WeaponSpecial.None;


    /// <summary>
    /// Find out if an item is a weapon or not
    /// </summary>
    /// <param name="Item">The ItemBase object of the item</param>
    /// <returns>Returns if its a weapon or not</returns>
    public bool isWeapon(ItemBase Item)
    {
        return Item.ItemGroup == "Weapon";
    }

    private void _RaceGear(string Monster)
    {
        GearStore();

        string MonsterRace = "";
        if (Monster != "*")
            MonsterRace = Bot.Monsters.MapMonsters.First(x => x.Name.ToLower() == Monster.ToLower()).Race ?? "";
        else MonsterRace = Bot.Monsters.CurrentMonsters.First().Race ?? "";

        if (MonsterRace == null || MonsterRace == "")
            return;

        string[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
        if (_BestGear.Length == 0)
            return;
        EnhanceItem(_BestGear, CurrentClassEnh(), CurrentWeaponSpecial());
        Core.Equip(_BestGear);
        EnhanceEquipped(CurrentClassEnh(), CurrentWeaponSpecial());
    }

    private void _RaceGear(int MonsterID)
    {
        GearStore();

        string MonsterRace = Bot.Monsters.MapMonsters.First(x => x.ID == MonsterID).Race;

        if (MonsterRace == null || MonsterRace == "")
            return;

        string[] _BestGear = BestGear((GearBoost)Enum.Parse(typeof(GearBoost), MonsterRace));
        if (_BestGear.Length == 0)
            return;
        EnhanceItem(_BestGear, CurrentClassEnh(), CurrentWeaponSpecial());
        Core.Equip(_BestGear);
        EnhanceEquipped(CurrentClassEnh(), CurrentWeaponSpecial());
    }

    #endregion

    #region Shop

    public void BuyItem(string map, int shopID, int itemID, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        List<ShopItem> shopItem = Core.GetShopItems(map, shopID).Where(x => x.ID == itemID).ToList();
        ShopItem? item = parseShopItem(shopItem, shopID, itemID.ToString());
        if (item == null)
            return;

        GetItemReq(item);
        if (canBuy(new List<ShopItem>() { item }, shopID, itemID.ToString()))
            Core.BuyItem(map, shopID, itemID, quant, shopQuant, shopItemID);
    }

    public void BuyItem(string map, int shopID, string itemName, int quant = 1, int shopQuant = 1, int shopItemID = 0)
    {
        List<ShopItem> shopItem = Core.GetShopItems(map, shopID).Where(x => x.Name == itemName).ToList();
        ShopItem? item = parseShopItem(shopItem, shopID, itemName);
        if (item == null)
            return;

        GetItemReq(item);
        if (canBuy(new List<ShopItem>() { item }, shopID, itemName))
            Core.BuyItem(map, shopID, itemName, quant, shopQuant, shopItemID);
    }

    public void StartBuyAllMerge(string map, int shopID, Action findIngredients)
    {
        matsOnly = (int)Bot.Config.Get<mergeOptionsEnum>("mode") == 2;
        List<ShopItem> shopItems = Core.GetShopItems(map, shopID);
        List<ShopItem> items = new();

        foreach (ShopItem item in shopItems)
        {
            //No clue why I have to do a double if instead of a &&, but otherwise it will not do this statement correctly
            if (!miscCatagories.Contains(item.Category))
                if (Core.IsMember ? true : !item.Upgrade)
                {
                    if ((int)Bot.Config.Get<mergeOptionsEnum>("mode") != 1)
                        items.Add(item);
                    else if (item.Coins)
                        items.Add(item);
                }
        }

        int t = 1;
        for (int i = 0; i < 2; i++)
        {
            foreach (ShopItem item in items)
            {
                getIngredients(item);
                if (!matsOnly)
                {
                    if (!Core.CheckInventory(item.ID))
                        Core.Logger($"Buying {item.Name} (#{t++}/{items.Count})");
                    if (canBuy(new List<ShopItem>() { item }, shopID))
                        Core.BuyItem(map, shopID, item.ID);
                }
            }
            if (!matsOnly)
                i++;
        }

        void getIngredients(ShopItem item)
        {
            if (Core.CheckInventory(item.ID) || item.Requirements == null)
                return;

            if (!matsOnly)
                Core.Logger($"Farming to buy {item.Name} (#{t}/{items.Count})");

            foreach (ItemBase req in item.Requirements)
            {
                externalQuant =
                    matsOnly ?
                        (Bot.Inventory.IsMaxStack(req.Name) ?
                            req.MaxStack : ((req.Temp ? Bot.Inventory.GetTempQuantity(req.Name) : Bot.Inventory.GetQuantity(req.Name)) + req.Quantity)) : req.Quantity;
                if (Core.CheckInventory(req.Name, externalQuant) && (matsOnly ? req.MaxStack == 1 : true))
                    continue;

                if (shopItems.Select(x => x.ID).Contains(req.ID))
                {
                    ShopItem selectedItem = shopItems.First(x => x.ID == req.ID);
                    getIngredients(selectedItem);
                    if (canBuy(new List<ShopItem>() { selectedItem }, shopID))
                        Core.BuyItem(map, shopID, selectedItem.ID, req.Quantity);
                }
                else
                {
                    Core.AddDrop(req.Name);
                    externalItem = req;
                    findIngredients();
                }
            }
        }
    }
    private List<ItemCategory> miscCatagories = new() { ItemCategory.Note, ItemCategory.Item, ItemCategory.Resource, ItemCategory.QuestItem, ItemCategory.ServerUse };
    public ItemBase externalItem = new();
    public int externalQuant = 0;
    public bool matsOnly = false;

    public bool canBuy(string map, int shopID, string itemName)
    {
        List<ShopItem> shopItem = Core.GetShopItems(map, shopID).Where(x => x.Name == itemName).ToList();
        return canBuy(shopItem, shopID, itemName);
    }

    public bool canBuy(string map, int shopID, int itemID)
    {
        List<ShopItem> shopItem = Core.GetShopItems(map, shopID).Where(x => x.ID == itemID).ToList();
        return canBuy(shopItem, shopID, itemID.ToString());
    }

    public bool canBuy(List<ShopItem> shopItem, int shopID, string itemNameID = "")
    {
        ShopItem? item = parseShopItem(shopItem, shopID, itemNameID);
        if (item == null)
            return false;

        //Rep check
        if (!String.IsNullOrEmpty(item.Faction) && item.Faction != "None")
        {
            int reqRank = RepCPLevel.First(x => x.Key == item.RequiredReputation).Value;
            if (reqRank > Farm.FactionRank(item.Faction))
            {
                Core.Logger($"Cannot buy {item.Name} from {shopID} because you dont have rank {reqRank} {item.Faction}.");
                return false;
            }
        }

        //Merge item check
        if (item.Requirements != null)
        {
            foreach (ItemBase req in item.Requirements)
            {
                if (!Core.CheckInventory(req.Name, req.Quantity))
                {
                    Core.Logger($"Cannot buy {item.Name} from {shopID} because {req.Name} is missing.");
                    return false;
                }
            }
        }

        //Gold check
        if (item.Cost > Bot.Player.Gold)
        {
            Core.Logger($"Cannot buy {item.Name} from {shopID} because you are missing {item.Cost - Bot.Player.Gold} gold.");
            return false;
        }

        return true;
    }

    private ShopItem? parseShopItem(List<ShopItem> shopItem, int shopID, string itemNameID)
    {
        if (shopItem.Count == 0)
        {
            Core.Logger($"Item {itemNameID} not found in shop {shopID}.");
            return null;
        }
        else if (shopItem.Count > 1)
        {
            Core.Logger($"Multiple items found with the name {shopItem.First().Name} in shop {shopID}. The developer needs to specify the item ID.");
            return null;
        }

        return shopItem.First();
    }

    public void GetItemReq(ShopItem item)
    {
        if (item.Faction != null && item.RequiredReputation > 0)
            runRep(item.Faction, RepCPLevel.First(x => x.Key == item.RequiredReputation).Value);
        Farm.Experience(item.Level);
        Farm.Gold(item.Cost);
    }

    private void runRep(string faction, int rank)
    {
        faction = faction.Replace(" ", "");
        Type farmClass = Farm.GetType();
        MethodInfo? theMethod = farmClass.GetMethod(faction + "REP");
        if (theMethod == null)
        {
            Core.Logger("Failed to find " + faction + "REP. Make sure you have the correct name and capitalization.");
            return;
        }
        theMethod.Invoke(Farm, new object[] { rank });
    }

    private Dictionary<int, int> RepCPLevel = new()
    {
        { 0, 1 },
        { 900, 2 },
        { 3600, 3 },
        { 10000, 4 },
        { 22500, 5 },
        { 44100, 6 },
        { 78400, 7 },
        { 129600, 8 },
        { 202500, 9 },
        { 302500, 10 }
    };

    /// <summary>
    /// The list of ScriptOptions for any merge script.
    /// </summary>
    public List<IOption> MergeOptions = new List<IOption>()
    {
        new Option<mergeOptionsEnum>("mode", "Select the mode to use", "Regardless of the mode you pick, the bot wont (attempt to) buy Legend-only items if you're not a Legend.\n" +
                                                                     "Select the Mode Explanation item to get more information", mergeOptionsEnum.all),
        new Option<string>("blank", " ", "", ""),
        new Option<string>(" ", "Mode Explanation [all]", "Mode [all]:\t\tYou get all the items from shop, even if non-AC ones if any exist.", "click here"),
        new Option<string>(" ", "Mode Explanation [acOnly]", "Mode [acOnly]:\tYou get all the AC tagged items from the shop.", "click here"),
        new Option<string>(" ", "Mode Explanation [mergeMats]", "Mode [mergeMats]:\tYou dont buy any items but instead get the materials to buy them yourself, this way you can choose.", "click here"),
    };

    public enum mergeOptionsEnum
    {
        all = 0,
        acOnly = 1,
        mergeMats = 2
    };

    #endregion

    #region Kill

    /// <summary>
    /// Joins a map, jump & set the spawn point and kills the specified monster with the best available race gear
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void BoostKillMonster(string map, string cell, string pad, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        _RaceGear(monster);

        Core.KillMonster(map, cell, pad, monster, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Kills a monster using it's ID, with the specified monsters the best available race gear
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monsterID">ID of the monster</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void BoostKillMonster(string map, string cell, string pad, int monsterID, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, cell, pad, publicRoom: publicRoom);

        _RaceGear(monsterID);

        Core.KillMonster(map, cell, pad, monsterID, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Joins a map and hunt for the monster and kills the specified monster with the best available race gear
    /// </summary>
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to hunt the monster for, if null will just hunt & kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    public void BoostHuntMonster(string map, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;

        Core.Join(map, publicRoom: publicRoom);

        _RaceGear(monster);

        Core.HuntMonster(map, monster, item, quant, isTemp, log, publicRoom);

        GearStore(true);
    }

    /// <summary>
    /// Joins a map, jump & set the spawn point and kills the specified monster with the best available race gear. But also listens for Counter Attacks
    /// </summary>
    /// <param name="map">Map to join</param>
    /// <param name="cell">Cell to jump to</param>
    /// <param name="pad">Pad to jump to</param>
    /// <param name="monster">Name of the monster to kill</param>
    /// <param name="item">Item to kill the monster for, if null will just kill the monster 1 time</param>
    /// <param name="quant">Desired quantity of the item</param>
    /// <param name="isTemp">Whether the item is temporary</param>
    /// <param name="log">Whether it will log that it is killing the monster</param>
    public void KillUltra(string map, string cell, string pad, string monster, string item = "", int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = true, bool forAuto = false)
    {
        if (item != "" && Core.CheckInventory(item, quant))
            return;
        if (!isTemp && item != "")
            Core.AddDrop(item);

        Core.Join(map, cell, pad, publicRoom: publicRoom);
        if (!forAuto)
            _RaceGear(monster);
        Core.Jump(cell, pad);

        Bot.Events.CounterAttack += _KillUltra;

        if (item == "")
        {
            if (log)
                Core.Logger($"Killing Ultra-Boss {monster}");
            int i = 0;
            Bot.Events.MonsterKilled += b => i++;
            while (!Bot.ShouldExit() && i < 1)
                while (!Bot.ShouldExit() && shouldAttack)
                    Bot.Player.Kill(monster);
            Core.Rest();
        }
        else
        {
            if (log)
                Core.Logger($"Killing Ultra-Boss {monster} for {item} ({quant}) [Temp = {isTemp}]");
            while (!Bot.ShouldExit() && !Core.CheckInventory(item, quant))
            {
                while (!Bot.ShouldExit() && shouldAttack)
                    Bot.Player.Kill(monster);
                if (!isTemp && !Core.CheckInventory(item))
                {
                    Bot.Sleep(Core.ActionDelay);
                    Bot.Player.RejectExcept(item);
                }
                if (!Bot.Player.InCombat)
                    Core.Rest();
                Bot.Wait.ForDrop(item);
            }
        }

        Bot.Events.CounterAttack -= _KillUltra;

        if (!forAuto)
            GearStore(true);
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

    /// <summary>
    /// Automatically finds the best Enhancement for the given class and enhances all equipped gear with it too
    /// </summary>
    /// <param name="Class">Name of the class you wish to enhance</param>
    public void SmartEnhance(string Class)
    {
        InventoryItem? SelectedClass = Bot.Inventory.Items.Where(i => i.Name.ToLower() == Class.ToLower() && i.Category == ItemCategory.Class).FirstOrDefault();
        if (SelectedClass == null)
        {
            Core.Logger($"SmartEnhance: Class {Class} not found");
            return;
        }

        switch (SelectedClass.Name.ToLower())
        {
            //Lucky - Spiral Carve
            case "abyssal angel":
            case "abyssal angel's shadow":
            case "archpaladin":
            case "artifact hunter":
            case "assassin":
            case "beastmaster":
            case "berserker":
            case "beta berserker":
            case "blademaster assassin":
            case "blademaster":
            case "blood titan":
            case "cardclasher":
            case "chaos avenger member preview":
            case "chaos champion prime":
            case "chaos slayer":
            case "chaos slayer berserker":
            case "chaos slayer cleric":
            case "chaos slayer mystic":
            case "chaos slayer thief":
            case "chrono chaorruptor":
            case "chrono commandant":
            case "chronocommander":
            case "chronocorrupter":
            case "chunin":
            case "classic alpha pirate":
            case "classic barber":
            case "classic doomknight":
            case "classic exalted soul cleaver":
            case "classic guardian":
            case "classic legion doomknight":
            case "classic paladin":
            case "classic pirate":
            case "classic soul cleaver":
            case "continuum chronomancer":
            case "corrupted chronomancer":
            case "dark chaos berserker":
            case "dark harbinger":
            case "doomknight":
            case "empyrean chronomancer":
            case "eternal chronomancer":
            case "eternal inversionist":
            case "evolved clawsuit":
            case "evolved dark caster":
            case "evolved leprechaun":
            case "exalted harbinger":
            case "exalted soul cleaver":
            case "glacial warlord":
            case "great thief":
            case "immortal chronomancer":
            case "imperial chunin":
            case "infinite dark caster":
            case "infinite legion dark caster":
            case "infinity titan":
            case "legion blademaster assassin":
            case "legion doomknight":
            case "legion evolved dark caster":
            case "legion swordmaster assassin":
            case "leprechaun":
            case "lycan":
            case "master ranger":
            case "mechajouster":
            case "necromancer":
            case "ninja":
            case "ninja warrior":
            case "not a mod":
            case "overworld chronomancer":
            case "pinkomancer":
            case "prismatic clawsuit":
            case "ranger":
            case "renegade":
            case "rogue":
            case "rogue (rare)":
            case "scarlet sorceress":
            case "shadowscythe general":
            case "skycharged grenadier":
            case "skyguard grenadier":
            case "soul cleaver":
            case "starlord":
            case "stonecrusher":
            case "swordmaster assassin":
            case "swordmaster":
            case "timekeeper":
            case "timekiller":
            case "timeless chronomancer":
            case "undead goat":
            case "undead leperchaun":
            case "undeadslayer":
            case "underworld chronomancer":
            case "unlucky leperchaun":
            case "void highlord":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Lucky);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
                break;
            //Lucky - Mana Vamp
            case "alpha doommega":
            case "alpha omega":
            case "alpha pirate":
            case "beast warrior":
            case "blood ancient":
            case "chaos avenger":
            case "chaos shaper":
            case "classic defender":
            case "clawsuit":
            case "cryomancer mini pet coming soon":
            case "dark legendary hero":
            case "dark ultra omninight":
            case "doomknight overlord":
            case "dragonslayer general":
            case "drakel warlord":
            case "glacial berserker test":
            case "heroic naval commander":
            case "legendary elemental warrior":
            case "horc evader":
            case "legendary hero":
            case "legendary naval commander":
            case "legion doomknight tester":
            case "legion revenant member test":
            case "naval commander":
            case "paladin high lord":
            case "paladin":
            case "paladinslayer":
            case "pirate":
            case "pumpkin lord":
            case "shadowflame dragonlord":
            case "shadowstalker of time":
            case "shadowwalker of time":
            case "shadowweaver of time":
            case "silver paladin":
            case "thief of hours":
            case "ultra elemental warrior":
            case "ultra omniknight":
            case "void highlord tester":
            case "warlord":
            case "warrior":
            case "warrior (rare)":
            case "warriorscythe general":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Lucky);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Mana_Vamp);
                break;
            //Lucky - Awe Blast
            case "arachnomancer":
            case "bard":
            case "chrono assassin":
            case "chronomancer":
            case "chronomancer prime":
            case "dark metal necro":
            case "deathknight lord":
            case "dragon shinobi":
            case "dragonlord":
            case "evolved pumpkin lord":
            case "dragonsoul shinobi":
            case "glacial berserker":
            case "grunge rocker":
            case "guardian":
            case "heavy metal necro":
            case "heavy metal rockstar":
            case "hobo highlord":
            case "lord of order":
            case "nechronomancer":
            case "necrotic chronomancer":
            case "no class":
            case "nu metal necro":
            case "obsidian no class":
            case "oracle":
            case "protosartorium":
            case "shadow dragon shinobi":
            case "shadow ripper":
            case "shadow rocker":
            case "star captain":
            case "troubador of love":
            case "unchained rocker":
            case "unchained rockstar":
            case "yami no ronin":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Lucky);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Awe_Blast);
                break;
            //Lucky - Health Vamp
            case "archfiend":
            case "barber":
            case "classic dragonlord":
            case "dragonslayer":
            case "enchanted vampire lord":
            case "enforcer":
            case "flame dragon warrior":
            case "royal vampire lord":
            case "rustbucket":
            case "sentinel":
            case "vampire":
            case "vampire lord":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Lucky);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Lucky, WeaponSpecial.Health_Vamp);
                break;
            //Wizard - Awe Blast
            case "acolyte":
            case "arcane dark caster":
            case "battlemage":
            case "battlemage of love":
            case "blaze binder":
            case "blood sorceress":
            case "dark battlemage":
            case "dark master of moglins":
            case "dragon knight":
            case "firelord summoner":
            case "grim necromancer":
            case "healer":
            case "healer (rare)":
            case "highseas commander":
            case "infinity knight":
            case "interstellar knight":
            case "master of moglins":
            case "mystical dark caster":
            case "northlands monk":
            case "royal battlemage":
            case "timeless dark caster":
            case "witch":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Wizard);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Awe_Blast);
                break;
            //Wizard - Spiral Carve
            case "chrono dataknight":
            case "chrono dragonknight":
            case "cryomancer":
            case "dark caster":
            case "dark cryomancer":
            case "dark lord":
            case "darkblood stormking":
            case "darkside":
            case "defender":
            case "frost spiritreaver":
            case "immortal dark caster":
            case "legion paladin":
            case "legion revenant":
            case "lightcaster":
            case "pink romancer":
            case "psionic mindbreaker":
            case "pyromancer":
            case "sakura cryomancer":
            case "troll spellsmith":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Wizard);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Spiral_Carve);
                break;
            //Wizard - Health Vamp
            case "daimon":
            case "evolved shaman":
            case "lightmage":
            case "mindbreaker":
            case "shaman":
            case "vindicator of they":
            case "elemental dracomancer":
            case "lightcaster test":
            case "love caster":
            case "mage":
            case "mage (rare)":
            case "sorcerer":
            case "the collector":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Wizard);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Wizard, WeaponSpecial.Health_Vamp);
                break;
            //Fighter - Awe Blast
            case "deathknight":
            case "frostval barbarian":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Fighter);
                Core.Equip(SelectedClass.Name);
                EnhanceEquipped(EnhancementType.Fighter, WeaponSpecial.Awe_Blast);
                break;
            //Healer - Health Vamp
            case "dragon of time":
                if (SelectedClass.EnhancementLevel == 0)
                    EnhanceItem(Class, EnhancementType.Healer);
                Core.Equip(SelectedClass.Name);
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
