//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;
using RBot.Items;
using RBot.Shops;


public class AstraviaPastMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreDarkon Darkon = new CoreDarkon();


    int shopID = 2065;
    string map = "astraviapast";

    //Insert Item List Here  ↓

    string[] MergeItems =
    {
        "Prince Drago's Royal Attire",
        "Prince Drago's Royal Dark Attire",
        "Prince Drago's Morph",
        "Prince Drago's Scarred Morph",
        "Prince Drago's Morph",
        "Suki's Armor",
        "Suki's Morph",
        "Suki's Gauntlets",
        "Regulus' Morph",
        "Titania's Morph"
    };

    string[] MergeSecondaryItems =
    {
        "Prince Drago's Attire",
        "Prince Drago's Dark Attire",
        "Suki's Casual Armor",
        "Prince Drago's Hair",
        "Prince Drago's Morph",
        "Regulus' Hair",
        "Suki's Ponytail",
        "Titania's Hair"
    };

    // To get the entire list of mergeitems easily to copy, go ingame, and load the shop first,
    // then goto loader > at the botton "shopIDs", find teh shop id of current shop & copy it.

    //bot.Shops.Load(shopID); 
    //bot.Log($"\"{string.Join("\",\r\n\"", bot.Shops.ShopItems.Select(i => i.Name))}\"");

    //^ insert shopID you copied(with shop loaded), then copy the 2 lines above (without the //'s)
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
            Bot.Shops.Load(shopID);
            List<ShopItem> shopdata = Bot.Shops.ShopItems;
            Core.AddDrop(MergeSecondaryItems);

            foreach (string MergeItem in MergeItems)
            {
                if (!Core.CheckInventory(MergeItem, toInv: false))
                {
                    Core.Logger($"Started farming for {MergeItem}");
                    if (MergeItem == "Prince Drago's Royal Attire" && !Core.CheckInventory("Prince Drago's Attire"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Forsaken Husk", "Prince Drago's Attire");
                    }
                    if (MergeItem == "Prince Drago's Royal Dark Attire" && !Core.CheckInventory("Prince Drago's Dark Attire"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Forsaken Husk", "Prince Drago's Dark Attire");
                    }
                    if (MergeItem == "Suki's Armor" && !Core.CheckInventory("Suki's Casual Armor"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Aurola", "Suki's Casual Armor");
                    }
                    if (MergeItem == "Prince Drago's Morph" && !Core.CheckInventory("Prince Drago's Hair"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Forsaken Husk", "Prince Drago's Hair");
                    }
                    if (MergeItem == "Prince Drago's Scarred Morph" && !Core.CheckInventory("Prince Drago's Morph"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Forsaken Husk", "Prince Drago's Hair");
                        MergeMats(1, 8);
                        Core.BuyItem(map, shopID, "Prince Drago's Morph");                        
                    }
                    if (MergeItem == "Regulus' Morph" && !Core.CheckInventory("Regulus' Hair"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Regulus", "Regulus' Hair");
                    }
                    if (MergeItem == "Suki's Morph" && !Core.CheckInventory("Suki's Ponytail"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Aurola", "Suki's Ponytail");
                    }
                    if (MergeItem == "Titania's Morph" && !Core.CheckInventory("Titania's Hair"))
                    {
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster(map, "Titania", "Titania's Hair");
                    }

                }
                List<ItemBase> Requirements = shopdata.First(i => i.Name == MergeItem).Requirements;
                int Item1Quant = Requirements.First(i => i.Name == "Gold Voucher 25k").Quantity;
                int Item2Quant = Requirements.First(i => i.Name == "Suki's Prestige").Quantity;

                MergeMats(Item1Quant, Item2Quant);
                Core.BuyItem(map, shopID, MergeItem);
                Core.ToBank(MergeItem);
            }
        }
    }

    public void MergeMats(int Item1Quant, int Item2Quant)
    {
        if (Core.CheckInventory("Gold Voucher 25k", Item1Quant) && Core.CheckInventory("Suki's Prestige", Item2Quant))
            return;

        Core.BuyItem(map, shopID, "Gold Voucher 25k", Item1Quant);
        Darkon.SukisPrestiege(Item2Quant);
    }



}
