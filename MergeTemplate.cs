//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;
using RBot.Items;
using RBot.Shops;


public class MergeTemplate
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    //Insert Item List Here  ↓

    string[] MergeItems =
    {
        
        // Merge items that Require other merge items add the required item in the "Refarm Section"
        
        
        //Merge items that dont Require other Merge items
        
        
        //Refarm Drops       
        

    };
    
    // To get the entire list of mergeitems easily to copy, go ingame, and load the shop first,
    // then goto loader > at the botton "Shopids", find teh shop id of current shop & copy it.

    //bot.Shops.Load(shopid); 
    //bot.Log($"\"{string.Join("\",\r\n\"", bot.Shops.ShopItems.Select(i => i.Name))}\"");

    //^ insert shopid you copied(with shop loaded), then copy the 2 lines above (without the //'s)
    //on the Client window: Tool > Consol > *paste* > "Run" (Make sure you have the Log open
    //just click the scripts button it has a logger built in.) copy the entire list, and paste above inside teh {}'s and sort it for yourself ^_^

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        MergePreReqs();
        Merge();

        Core.SetOptions(false);
    }

    public void MergePreReqs()
    {
        if (Core.isCompletedBefore(1)) // <-- Insert Questid of last quest required to start farming.
            return;
        // Story Quests if any are Required:
        // {

        // Helpfull Functions: 
        // Story.KillQuest(QuestID, "mapname", new[] { "monster", "monster" }); //<-- monsters must be in order of which their drops are ingame, 1 monster drops multiple put its name twice.
        // Story.KillQuest(QuestID, "mapname", "monster");
        // Story.MapItemQuest(QuestID, "mapname", new[] { MapItemID, MapItemID, MapItemID, Amount); //<-- MapItemIDs must be in order of which they are ingame. get "MapItemIDs" by: loader > Map Item IDs > Grab.
        // Story.MapItemQuest(QuestID, "mapname", MapItemID, Amount);
        // Story.BuyQuest(QuestID, "map", ShopID, "ItemName", amount);


        // }

    }

    public void Merge(string item = "all")
    {
        //------- FILLIN BELOW-------
        int ShopID = 0;
        string map = "mapname";

        //------- FILLIN ABOVE-------


        if (item == "all" && Core.CheckInventory(MergeItems))
            return;

        if (item != "all" && Core.CheckInventory(item))
            return;

        if (item != "all" && !Core.CheckInventory(item))
        {
            Core.Join(map); //<-- Replace with MapName the shop is in.
            Bot.Shops.Load(ShopID); //<--- Replace with Shop ID
            List<ShopItem> shopdata = Bot.Shops.ShopItems;

            List<ItemBase> Requirements = shopdata.First(i => i.Name == item).Requirements;
            int Item1Quant = Requirements.First(i => i.Name == "Item1").Quantity;
            int Item2Quant = Requirements.First(i => i.Name == "Item2").Quantity;
            int Item3Quant = Requirements.First(i => i.Name == "Item3").Quantity;
            int Item4Quant = Requirements.First(i => i.Name == "Item4").Quantity;
            // Fill The "Item#" in with the itemnames corresponding to the items Below in Mergemats()

            Core.Logger($"Farming Required Materials for {item}");
            MergeMats(0, 0, 0, 0); // <- Replace with Required Quants of the Items in order below            
            Core.BuyItem(map, ShopID, item);
            Core.ToBank(item);
        }


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

                    //If merge item requires aditional items put it below:
                    if (MergeItem == "Aditional Merge Item Name" && !Core.CheckInventory("Aditional Merge Item name", toInv: false))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("map", "Monster", "Aditional Merge Item Name");
                    }
                    List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;
                    int Item1Quant = Requirements.First(i => i.Name == "Item1").Quantity;
                    int Item2Quant = Requirements.First(i => i.Name == "Item2").Quantity;
                    int Item3Quant = Requirements.First(i => i.Name == "Item3").Quantity;
                    int Item4Quant = Requirements.First(i => i.Name == "Item4").Quantity;
                    // Fill The "Item#" in with the itemnames corresponding to the items Below in Mergemats()

                    MergeMats(0, 0, 0, 0); // <- Replace with Required Quants of the Items in order below            
                    Core.BuyItem(map, ShopID, MergeItem);
                    Core.ToBank(MergeItem);
                }
            }
        }
    }

    public void MergeMats(int Item1Quant, int Item2Quant, int Item3Quant, int Item4Quant)
    {
        if (Core.CheckInventory("Item1", Item1Quant) && Core.CheckInventory("Item2", Item2Quant) && Core.CheckInventory("Item3", Item3Quant) && Core.CheckInventory("Item4", Item4Quant))
            return;

        // Core.EquipClass(ClassType.Farm);
        // Core.EquipClass(ClassType.Solo); 

        // Uncomment Above ^^ (Cntrl+/ on the line) of the equipment type you want it to use.

        // --------- Replace Items Below ---------
        Core.HuntMonster("map", "monster", "Item1", Item1Quant);
        Core.HuntMonster("map", "monster", "item2", Item2Quant);
        Core.HuntMonster("map", "monster", "item3", Item3Quant);
        Core.HuntMonster("map", "monster", "item4", Item4Quant);
        // --------- Replace Items Above ---------
        // More can be added depending how many items your merges require.
    }



}
