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
            _AutoEnhance(Empty ,EquippedItems, Type, Special);
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

        Bot.Log(SelectedItem.Count.ToString());
        Bot.Log(SelectedWeapon.Count.ToString());
        Bot.Log(SelectedOther.Count.ToString());

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

    #endregion

    #region Kill

    public void KillUltra(string map, string cell, string pad, string monster, string item = null, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = true)
    {
        if (item != null && Core.CheckInventory(item, quant))
            return;
        if (!isTemp && item != null)
            Core.AddDrop(item);
        Core.Join(map, publicRoom: publicRoom);
        Core.Jump(cell, pad);
        
        /*
        string TargetRace = Bot.Monsters.CurrentMonsters.Find(x => x.Name == monster).Race;
        if (TargetRace != null)
        {
            string[] _BestGear = Core.BestGear((GearBoost)Enum.Parse(typeof(GearBoost), TargetRace));
            EnhanceItem(_BestGear, )
        }
        */
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

    #endregion

    #region Backend

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
            if (Type == EnhancementType.Fighter)            __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 768 : 141);
            else if (Type == EnhancementType.Thief)         __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 767 : 142);
            else if (Type == EnhancementType.Wizard)        __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 765 : 144);
            else if (Type == EnhancementType.Healer)        __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 762 : 145);
            else if (Type == EnhancementType.Hybrid)        __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 766 : 143);
            else if (Type == EnhancementType.Lucky)         __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 763 : 147);
            else if (Type == EnhancementType.SpellBreaker)  __AutoEnhance(FlexibleList, Bot.Player.Level >= 50 ? 146 : 146);
        }
        //Weapon Specials
        
        if (WeaponList.Count != 0 && Special != WeaponSpecial.None)
        {
            Core.Logger($"Best Enhancement of: {Type.ToString()}, {Special.ToString().Replace('_', ' ')} Enhancement");
            if (Type == EnhancementType.Fighter)                                                __AutoEnhance(WeaponList, 635, "museum");
            else if (Type == EnhancementType.Thief)                                             __AutoEnhance(WeaponList, 637, "museum");
            else if (Type == EnhancementType.Wizard || Type == EnhancementType.SpellBreaker)    __AutoEnhance(WeaponList, 636, "museum");
            else if (Type == EnhancementType.Healer)                                            __AutoEnhance(WeaponList, 638, "museum");
            else if (Type == EnhancementType.Hybrid)                                            __AutoEnhance(WeaponList, 633, "museum");
            else if (Type == EnhancementType.Lucky)                                             __AutoEnhance(WeaponList, 639, "museum");
        }

        void __AutoEnhance(List<InventoryItem> Input, int ShopID, string Map = null)
        {
            List<ShopItem> ShopItems = new List<ShopItem>();

            foreach (InventoryItem Item in Input)
            {
                Core.Logger($"Best Enhancement for: \"{Item.Name}\" [Searching]");
                Core.CheckInventory(Item.Name);

                if (Map != null)
                    Core.Join(Map, "Wait", "Enter");

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
                List<ShopItem> BestTwo = ListMinToMax.Skip(ListMinToMax.Count-2).ToList();
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

}

public enum EnhancementType
{
    Fighter, 
    Thief,
    Wizard,
    Healer,
    Hybrid,
    Lucky,
    SpellBreaker
}

public enum WeaponSpecial
{
    None,
    Spiral_Carve,
    Awe_Blast,
    Health_Vamp,
    Mana_Vamp,
    Powerword_Die
}
