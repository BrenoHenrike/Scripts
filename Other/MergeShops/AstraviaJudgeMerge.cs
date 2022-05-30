//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;
using RBot.Items;
using RBot.Shops;


public class AstraviaJudgeMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreDarkon Darkon = new CoreDarkon();


    int ShopID = 2065;
    string map = "astraviajudge";

    //Insert Item List Here  ↓

    string[] MergeItems =
    {        
        "Gold Voucher 25k",
        "Prince Drago's Royal Attire",
        "Prince Drago's Royal Dark Attire",
        "Prince Drago's Morph",
        "Prince Drago's Scarred Morph",
        "Suki's Armor",
        "Suki's Morph",
        "Suki's Gauntlets",
        "Regulus' Morph",
        "Titania's Morph",
        
        

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

        Merge();

        Core.SetOptions(false);
    }


    public void Merge(string item = "all")
    {
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
                    if (MergeItem == "Re's Party Arms" && !Core.CheckInventory("Re's Party Attire"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "La", "Re's Party Attire");
                    }
                    List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;
                    int Item1Quant = Requirements.First(i => i.Name == "Gold Voucher 25k").Quantity;
                    int Item2Quant = Requirements.First(i => i.Name == "A Melody").Quantity;

                    MergeMats(Item1Quant, Item2Quant);          
                    Core.BuyItem(map, ShopID, MergeItem);
                    Core.ToBank(MergeItem);
                }
            }
        }
    }

    public void MergeMats(int Item1Quant, int Item2Quant)
    {
        if (Core.CheckInventory("Gold Voucher 25k", Item1Quant) && Core.CheckInventory("A Melody", Item2Quant))
            return;

        Core.BuyItem(map, ShopID, "Gold Voucher 25k", Item1Quant);
        Darkon.AMelody(Item2Quant);
    }



}
