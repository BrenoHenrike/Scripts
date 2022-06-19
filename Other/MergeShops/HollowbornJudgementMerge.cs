//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;
using RBot.Items;
using RBot.Shops;


public class HollowbornJudgementMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();


    string[] MergeItems =
        {
        // Merge items that Require other merge items add the required item in the "Refarm Section"
        "Hollowborn Judge",
        "Hollowborn Judge In Officio",
        "Hollowborn Punitio",
        "Dual Hollowborn Punitio",
        "Hel Fö's Armor",
        "Hel Fö In Officio",
        "Hollowborn Remissio",
        "Hollowborn Lex et Ordo",
        
        //Merge items that dont Require other Merge items
        "Hel Fö's Crown + Band",
        "Hel Fö's Morph",
        "Hel Fö's Morph + Band",
        "Hel Fö's Hair",
        "Hollowborn Judge's Hood",
        "Hel Fö's Hat",
        "Hollowborn Altare Devotionis",
        "Hollowborn Aequitas",
        "Hollowborn Rune Judicii",
        "Hollowborn Dark Rune Judicii",
        "Hollowborn Judicium Imaginem",
        "Hollowborn Jurisdictio",
        "Hollowborn Judgement Ex Vi Legis",
        "Dual Hollowborn Judgement",
        "Hollowborn Virgam Luminum",
        "Hollowborn Consummatum Est",
        "Hollowborn Bis In Idem",
        "Hollowborn Vade Mecum",
        
        //Refarm Drops
        "Hollowborn Judge",
        "Hollowborn Punitio",
        "Hel Fö's Armor",
        "Hollowborn Remissio"
        };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Hollowborn Writ");
        Core.SetOptions();
        
        Merge();

        Core.SetOptions(false);
    }

    public void Merge(string item = "all")
    {
        List<ShopItem> shopdata = Bot.Shops.ShopItems;

        int ShopID = 2075;
        string map = "hbchallenge";


        if (item == "all" && Core.CheckInventory(MergeItems))
            return;

        if (item != "all" && Core.CheckInventory(item))
            return;

        if (item != "all" && !Core.CheckInventory(item))
        {
            Core.Join(map);
            Bot.Shops.Load(ShopID);

            List<ItemBase> Requirements = shopdata.First(i => i.Name == item).Requirements;
            int Item1Quant = Requirements.First(i => i.Name == "Item1").Quantity;

            if (item == "Dual Hollowborn Punitio" && !Core.CheckInventory("Dual Hollowborn Punitio", toInv: false))
            {
                Core.EquipClass(ClassType.Farm);
                MergeMats(40);
                Core.BuyItem(map, ShopID, "Hollowborn Punitio");
                Bot.Sleep(Core.ActionDelay);
                Core.BuyItem(map, ShopID, "Dual Hollowborn Punitio");
                Core.ToBank(item);
            }
            if (item == "Hollowborn Lex et Ordo" && !Core.CheckInventory("Hollowborn Lex et Ordo", toInv: false))
            {
                Core.EquipClass(ClassType.Farm);
                MergeMats(40);
                Core.BuyItem(map, ShopID, "Hollowborn Remissio");
                Bot.Sleep(Core.ActionDelay);
                Core.BuyItem(map, ShopID, "Hollowborn Lex et Ordo");
                Core.ToBank(item);
            }
            if (item == "Hollowborn Judge In Officio" && !Core.CheckInventory("Hollowborn Judge In Officio", toInv: false))
            {
                Core.EquipClass(ClassType.Farm);
                MergeMats(150);
                Core.BuyItem(map, ShopID, 65729);
                Bot.Sleep(Core.ActionDelay);
                Core.BuyItem(map, ShopID, 65728);
                Core.ToBank(item);
            }

            Core.Logger($"Farming Required Materials for {item}");
            MergeMats(Item1Quant);
            Core.BuyItem(map, ShopID, item);
            Core.ToBank(item);
        }


        if (item == "all" && !Core.CheckInventory(MergeItems))
        {
            Core.Join(map);
            Bot.Shops.Load(ShopID);

            foreach (string MergeItem in MergeItems)
            {
                if (!Core.CheckInventory(MergeItem, toInv: false))
                {
                    List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;
                    int Item1Quant = Requirements.First(i => i.Name == "Hollowborn Writ").Quantity;

                    Core.Logger($"Started farming for {MergeItem}, {Bot.Inventory.GetQuantity("Hollowborn Writ")}/{Item1Quant} Hollowborn Writ");
                    MergeMats(Item1Quant);
                    Core.BuyItem(map, ShopID, MergeItem);
                    Core.ToBank(MergeItem);
                }
            }
        }
    }

    public void MergeMats(int Item1Quant)
    {
        if (Core.CheckInventory("Hollowborn Writ", Item1Quant))
            return;

        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Hollowborn Writ");
        Core.Logger($"this will take {(Item1Quant - Bot.Inventory.GetQuantity("Hollowborn Writ")) / 3} Turnins [counter starts at 0]");

        Core.RegisterQuests(8418);
        while (!Core.CheckInventory("Hollowborn Writ", Item1Quant))
        {
            Core.Logger($"{Bot.Inventory.GetQuantity("Hollowborn Writ")}/{Item1Quant} Hollowborn Writ");
            Bot.Sleep(Core.ActionDelay);
            Core.KillMonster("hbchallenge", "r3", "Right", "Judge's Minion", "Judge's Minion Judged", 12, log: false);
        }
        Core.CancelRegisteredQuests();
    }
}
