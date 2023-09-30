/*
name: Blaze Beard Story
description: This will finish the Blaze Beard Story Quest.
tags: story, quest, blaze-beard, pirate
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LegacyTemplate
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        // Core.BankingBlackList.AddRange(RequiredItems);
        Core.SetOptions();

        TokenQuests();

        Core.SetOptions(false);
    }

    public void TokenQuests()
    {
        Story.PreLoad(this);

        //LegacyQuestManager handles drops & required tokens and will sort out automaticly where you are in the questline.
        //Insert Questids after "Questlogic," in order.
        Story.LegacyQuestManager(QuestLogic, 1, 2, 3);
        
        void QuestLogic()
        {
            //InsertQuestID from the LegacyQuestManager in order, per case
            switch (Story.LegacyQuestID)
            {
                case 1:
                    Core.HuntMonster("map", "mob", "item", quant);
                    break;

                case 2:
                    //ItemID - How to Get: Goto Quest Map > Tools > Grabber > GetMap Item Ids > ItemID is the first # in the bit lines that show up.
                    Core.GetMapItem(ItemID, quant, "map");
                    break;

                case 3:
                    //There area  few ways to use Core.BuyItem (itemname, ItemID, or ShopItemID)
                    //ShopItemID can be gotten through the Grabber, just open teh shop goto the shop items tab in grabber, and hit grab. the ShopItemID will be on the Right you may need to scroll.
                    //ShopItemID is used when there are more then 1 item in the shop with the same name.
                    Core.BuyItem("map", shopID, "ItemName", quant);
                    Core.BuyItem("map", shopID, itemID, quant);
                    Core.BuyItem("map", shopID, "ItemName", ShopItemID, quant);
                    break;

            }
        }
    }
}