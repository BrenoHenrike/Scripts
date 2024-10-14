/*
name: Free Boosts [10mns]
description: Farms Free Boosts from "Reagents for Zifwin" (cp/gold/rep)
tags: reagents for zifwin, free, boost, class points, reputation, gold
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;
using System.Collections.Generic;

public class FreeBoosts
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public static CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public string OptionsStorage = "Booster";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("GoldBoostQuant", "Gold Boost Quant", "Input the number of The Type of Boost For the Bot to Get [Max 9999]", 9999),
        new Option<int>("ClassBoostQuant", "Class Boost Quant", "Input the number of The Type of Boost For the Bot to Get [Max 9999]", 9999),
        new Option<int>("RepBoostQuant", "Rep Boost Quant", "Input the number of The Type of Boost For the Bot to Get [Max 9999]", 9999)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetBoostsSelect(Bot.Config!.Get<int>("GoldBoostQuant"), Bot.Config.Get<int>("ClassBoostQuant"), Bot.Config.Get<int>("RepBoostQuant"));
        Core.SetOptions(false);
    }

    public void GetBoostsSelect(int GoldBoostQuant, int ClassBoostQuant, int RepBoostQuant)
    {
        Core.AddDrop("GOLD Boost! (10 min)", "CLASS Boost! (10 min)", "REPUTATION Boost! (10 min)");
        Core.Logger("Drops are \"randomly\" received, and may take a while... be prepared if quantities are high.");
        Core.FarmingLogger("GOLD Boost! (10 min)", GoldBoostQuant);
        Core.FarmingLogger("CLASS Boost! (10 min)", ClassBoostQuant);
        Core.FarmingLogger("REPUTATION Boost! (10 min)", RepBoostQuant);
        Core.EquipClass(ClassType.Farm);

        bool allQuantitiesMet = Core.CheckInventory("GOLD Boost! (10 min)", GoldBoostQuant) &&
                                           Core.CheckInventory("CLASS Boost! (10 min)", ClassBoostQuant) &&
                                           Core.CheckInventory("REPUTATION Boost! (10 min)", RepBoostQuant);

        while (!Bot.ShouldExit && !allQuantitiesMet)
        {
            Core.HuntMonsterQuest(6208,
("bloodtusk", "Trollola Plant", ClassType.Solo),
        ("cloister", "Acornent", ClassType.Solo),
        ("nibbleon", "Dark Makai", ClassType.Solo)
);

            if (allQuantitiesMet)
                break; // Exit the loop when all quantities are met.

            InventoryItem? goldItem = Bot.Inventory.Items.FirstOrDefault(x => x.Name == "GOLD Boost! (10 min)");
            InventoryItem? classItem = Bot.Inventory.Items.FirstOrDefault(x => x.Name == "CLASS Boost! (10 min)");
            InventoryItem? repItem = Bot.Inventory.Items.FirstOrDefault(x => x.Name == "REPUTATION Boost! (10 min)");

            if (goldItem != null)
                goldItem.Quantity = Math.Min(GoldBoostQuant, goldItem.MaxStack);

            if (classItem != null)
                classItem.Quantity = Math.Min(ClassBoostQuant, classItem.MaxStack);

            if (repItem != null)
                repItem.Quantity = Math.Min(RepBoostQuant, repItem.MaxStack);
        }

        Core.CancelRegisteredQuests();
    }
}

public enum Booster
{
    Gold = 7140,
    Class = 8877,
    Reputation = 8879,
}
