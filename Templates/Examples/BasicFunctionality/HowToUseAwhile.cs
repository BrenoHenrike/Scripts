/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class HowToUseAwhile
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        if (Core.CheckInventory("item", 1))
            return;




        #region Learn Your "whiles" -- with Tato


        foreach (string HowToUseAwhile in new[] { "Level", "Gold", "Rep", "ItemBase (requires extra data)", "QuestData" })
        {
            switch (HowToUseAwhile)
            {

                #region Basics
                // Simple Conditions -----------------------------------------
                case "Level":
                    // Continue processing while Bot should not exit and player's level is less than 100
                    while (!Bot.ShouldExit && Bot.Player.Level < 100)
                    {
                        // Your logic here
                    }
                    break;

                case "Gold":
                    // Continue processing while Bot should not exit and player's gold is less than 1000000000
                    while (!Bot.ShouldExit && Bot.Player.Gold < 1000000000)
                    {
                        // Your logic here
                    }
                    break;

                case "Rep":
                    // This one uses CoreFarm, so both the using and the class should be imported.
                    // It is assumed that CoreFarms class has a method FactionRank(string factionName)
                    while (!Bot.ShouldExit && Farm.FactionRank("repname") < 10)
                    {
                        // Your logic here
                    }
                    break;
                // Simple Conditions -----------------------------------------
                #endregion Basics


                #region Complex
                // Complex Conditions --------------------------------------------------
                case "ItemBase":
                    // Requires an additional using at the top: "using Skua.Core.Models.Items;"
                    // Find an item by name and item ID from the inventory
                    ItemBase? Item = Bot.Inventory.Items.FirstOrDefault(x => x.Name == ""); // Fill the "" with the item's Name
                    ItemBase? Item1 = Bot.Inventory.Items.FirstOrDefault(x => x.ID == 0000); // Fill the 0's with the item's ItemID

                    // Both "Item" and "Item1" can use all their properties the same.
                    while (!Bot.ShouldExit &&
                        Core.CheckInventory(Item?.Name, Item1!.MaxStack)
                        || Core.CheckInventory(Item1?.ID)
                        || Item!.Coins // Check if the item is AC tagged or not
                        || Item!.Temp // Check if the item is temporary or not
                        || Item!.Upgrade) // Check if the item is a member or not
                    {
                        // Your logic here
                    }
                    break;

                case "QuestData":
                    // Requires an additional using at the top: "using Skua.Core.Models.Quests;"
                    Quest QuestData = Core.EnsureLoad(0000);
                    // Quest drops, accept requirements, and items required to complete all in one list
                    List<ItemBase> QuestDropsandRequirements = QuestData.Rewards.Concat(QuestData.AcceptRequirements).Concat(QuestData.Requirements).ToList();

                    // Create a comma-separated string of item names
                    string itemNames = string.Join(", ", QuestDropsandRequirements.Select(item => item.Name));

                    // Continue processing while Bot should not exit and the inventory meets quest requirements
                    while (!Bot.ShouldExit && Core.CheckInventory(itemNames))
                    {
                        // Your logic here
                    }
                    break;
                // Complex Conditions --------------------------------------------------
                #endregion Complex


                // We don't go past here (ChatGPT does tho)
                #region ChatGPT
                case "Dictionary":
                    // Define the desired quantities for each inventory item
                    Dictionary<string, int> inventoryQuantities = new()
                    {
                    // Specify the desired quantity for "Item1" in the inventory
                    { "Item1", 9999 },
                    
                    // Specify the desired quantity for "Item2" in the inventory
                    { "Item2", 50 }
                    
                    // Add more items and their desired quantities below:
                    // { "NewItem", quantity },
                };

                    // Loop until a specific condition is met
                    while (
                        // Check if the bot should continue processing and the inventory conditions are not met
                        !Bot.ShouldExit &&

                        // Check if the quantity of "Item1" in the inventory is less than the desired quantity
                        !(Core.CheckInventory("Item1", inventoryQuantities["Item1"]) &&
                            Bot.Inventory.Items.FirstOrDefault(item => item.Name == "Item1")?.Quantity >= inventoryQuantities["Item1"]) &&

                        // Check if the quantity of "Item2" in the inventory is less than the desired quantity
                        !(Core.CheckInventory("Item2", inventoryQuantities["Item2"]) &&
                            Bot.Inventory.Items.FirstOrDefault(item => item.Name == "Item2")?.Quantity >= inventoryQuantities["Item2"])
                    )
                    {
                        // Your loop body here

                        // This section will be executed as long as the loop conditions are not met
                    }
                    break;

                    #endregion ChatGPT
            }
        }

        #endregion Learn Your "whiles" -- with Tato
    }
}
