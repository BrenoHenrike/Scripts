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
        new Option<string>("shop_ids", "Shop IDs", "Shop IDs to load, , to delimit", "1817,2104")
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
        
        foreach ((string map_name, int shop_id) in map_names.Zip(shop_ids)) {
            string map = map_name == "-" ? Bot.Map.Name : map_name;

            List<ShopItem> shop_items = Core.GetShopItems(map, shop_id);
            foreach (ShopItem item in shop_items)
            {
                if (Core.CheckInventory(item.ID, toInv: false))
                    continue;

                Core.BuyItem(map, shop_id, item.ID, shopItemID: item.ShopItemID);
                if (item.Coins)
                    Core.ToBank(item.Name);
            }
        }
    }
}
