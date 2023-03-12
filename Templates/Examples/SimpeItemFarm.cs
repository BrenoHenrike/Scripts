/*
name: Iten Farm Name
description: farms "itemname", until you have the desired amount
tags: itemname, tag, tag, tag, tag, tag, tag, tag, tag
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class SimpleItemFarm
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();
        // Example(quant if blank below, would go here then);

        Core.SetOptions(false);
    }

    //for the quant just put the Max quant of the item that can be gotten from the logger. or leave it blank and put the number in the Exmaple(here); above
    public void Example(int quant = 9999)
    {
        if (Core.CheckInventory("item", quant))
            return;

        //Example 1:
        while (!Bot.ShouldExit && !Core.CheckInventory("Item", quant))
            Core.HuntMonster("map", "mob", "item", quant);

        //Example 2:
        while (!Bot.ShouldExit && !Core.CheckInventory(itemID, quant))
            Core.HuntMonster("map", "mob");

        //Example 3:
        while (!Bot.ShouldExit && !Core.CheckInventory(itemID, quant))
        {
            /*
            Insert imported function from another script here after properly doing imports
            //Example: 
            Legion.FarmLegionTokens(quant);

            with the includes (no duplicates please check your script beforehand.):                
                //cs_include Scripts/CoreBots.cs
                //cs_include Scripts/CoreFarms.cs
                //cs_include Scripts/CoreAdvanced.cs
                //cs_include Scripts/CoreStory.cs
                //cs_include Scripts/Legion/CoreLegion.cs

            and the imports (no duplicates please check your script beforehand.):
                public CoreLegion Legion = new CoreLegion();
            */
        }





    }
}