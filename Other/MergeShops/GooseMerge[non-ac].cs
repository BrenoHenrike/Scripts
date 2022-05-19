//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;
using RBot.Items;
using RBot.Shops;


public class GooseMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    string[] MergeItems =
    {
        //Insert Item List Here
        "Queen's Sage",
        "Queen's Sage Hood",
        "Queen's Sage Cape",
        "Queen's Sage Scythe",
        "GIANT Mountain of Socks",
        "Cyser-Duck Painting",
        "Bucket of Paint Helm",
        "Sock Ape",
        "Chris P. Bacon",
        "Grandhonk Goose the Gray",
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Merge();

        Core.SetOptions(false);
    }

    public void Merge(string item = "all")
    {

        int ShopID = 58;
        string map = "Goose";

        if (item == "all" && Core.CheckInventory(MergeItems))
            return;

        if (item == "all" && !Core.CheckInventory(MergeItems))
        {
            Core.Join(map);
            Bot.Shops.Load(ShopID);
            List<ShopItem> shopdata = Bot.Shops.ShopItems;

            foreach (string MergeItem in MergeItems)
            {
                if (!Core.CheckInventory(MergeItem, toInv: false))
                {
                    Core.Logger($"Started farming for {MergeItem}");
                    List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;
                    int Item1Quant = Requirements.First(i => i.Name == "Cysero's Cookie").Quantity;

                    MergeMats(Item1Quant); 
                    Core.BuyItem(map, ShopID, MergeItem);                   
                }
            }
        }
    }

    public void MergeMats(int Item1Quant)
    {
        if (Core.CheckInventory("Cysero's Cookie", Item1Quant))
            return;

        Core.EquipClass(ClassType.Farm);


        Core.KillMonster("Goose", "r5", "Left", "*", "Cysero's Cookie", Item1Quant);
    }



}
