/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Shops;

public class BuyOut
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public List<IOption> Options = new()
    {
        new Option<string>("map_names", "Map Names", "Map Name of Shop to load, - for default, comma to delimit", "battleon,-"),
        new Option<string>("shop_ids", "Shop IDs", "Shop IDs to load, , to delimit", "1817,2104"),
        new Option<BuyEnums>("mode", "Select the mode to use", "Regardless of the mode you pick, the bot wont (attempt to) buy Legend-only items if you're not a Legend.\n" +
                                                                     "Select the Mode Explanation item to get more information", BuyEnums.all),
        new Option<string>("blank", " ", "", ""),
        new Option<string>(" ", "Mode Explanation [all]", "Mode [all]: \t\tYou get all the items from shop, even non-AC ones and expensive AC ones, if any.", "click here"),
        new Option<string>(" ", "Mode Explanation [zero_ac_only]", "Mode [zero_ac_only]: \tYou get all the AC tagged items from the shop that are 0 ACs.", "click here"),
        new Option<string>(" ", "Mode Explanation [zero_ac_and_gold]", "Mode [zero_ac_and_gold]: \tYou get all the AC tagged items from the shop that are 0 ACs, and non-AC items.", "click here"),
        new Option<string>(" ", "Mode Explanation [gold_only]", "Mode [zero_ac_and_gold]: \tYou get all the gold items from the shop.", "click here"),
        new Option<string>("blank", " ", "", ""),
    };

    public enum BuyEnums
    {
        all = 0,
        zero_ac_only = 1,
        zero_ac_and_gold = 2,
        gold_only = 3
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Buy();

        Core.SetOptions(false);
    }

    public void Buy()
    {
        List<string> map_names = Bot.Config.Get<string>("map_names").Split(",").ToList();
        List<int> shop_ids = Bot.Config.Get<string>("shop_ids").Split(",").Select(id => Int32.Parse(id)).ToList();
        BuyEnums mode = Bot.Config.Get<BuyEnums>("mode");
        Core.Logger($"User chose mode {mode}.");

        List<Tuple<int, string>> bought = new List<Tuple<int, string>>();
        foreach ((string map_name, int shop_id) in map_names.Zip(shop_ids))
        {
            string map = map_name == "-" ? Bot.Map.Name : map_name;

            List<ShopItem> shop_items = Core.GetShopItems(map, shop_id);
            int i = 0;
            foreach (ShopItem item in shop_items)
            {
                i++;
                if (Core.CheckInventory(item.ID, toInv: false))
                {
                    Core.Logger($"Already own, skipping item #{i} {item.Name}");
                }
                else if (item.Coins)
                {
                    if ((mode == BuyEnums.all) || (mode <= BuyEnums.zero_ac_and_gold && item.Cost == 0))
                    {
                        Core.BuyItem(map, shop_id, item.ID, shopItemID: item.ShopItemID);
                        bought.Add(Tuple.Create(item.ID, item.Name));
                        Core.ToBank(item.Name);
                    }
                    else
                    {
                        Core.Logger($"Don't wanna buy AC, skipping item #{i} {item.Name}");
                    }
                }
                else
                {
                    if ((mode == BuyEnums.all) || (mode >= BuyEnums.zero_ac_and_gold))
                    {
                        Core.BuyItem(map, shop_id, item.ID, shopItemID: item.ShopItemID);
                        bought.Add(Tuple.Create(item.ID, item.Name));
                    }
                    else
                    {
                        Core.Logger($"Don't wanna buy gold, skipping {i} {item.Name}");
                    }
                }
                Core.Logger($"Processed {i} / {shop_items.Count} {item.Name}");
            }
        }
        Core.Logger($"Bought {bought.Count} items.");
        foreach ((int id, string name) in bought)
            Core.Logger($"Bought item id {id} {name}");
    }
}
