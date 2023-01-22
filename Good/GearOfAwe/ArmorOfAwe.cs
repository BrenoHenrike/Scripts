/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class ArmorOfAwe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArmor();

        Core.SetOptions(false);
    }

    public void GetArmor()
    {
        if (Core.CheckInventory("Armor of Awe"))
            return;

        if (Core.IsMember)
        {
            if (!Core.CheckInventory("Pauldron Relic"))
            {
                Core.AddDrop("Pauldron Fragment");
                Core.EquipClass(ClassType.Solo);

                Core.RegisterQuests(4162);
                while (!Bot.ShouldExit && !Core.CheckInventory("Pauldron Fragment", 15))
                {
                    Adv.BoostHuntMonster("gravestrike", "Ultra Akriloth", "Pauldron Shard", 15, false);
                    Bot.Wait.ForPickup("Pauldron Fragment");
                }
                Core.CancelRegisteredQuests();

                Core.BuyItem("museum", 1129, "Pauldron Relic");
            }
        }
        else
            Awe.GetAweRelic("Pauldron", 4160, 15, 15, "gravestrike", "Ultra Akriloth");

        Awe.GetAweRelic("Breastplate", 4163, 10, 10, "aqlesson", "Carnax");
        Awe.GetAweRelic("Vambrace", 4166, 15, 15, "bloodtitan", "Ultra Blood Titan");
        Awe.GetAweRelic("Gauntlet", 4169, 25, 5, "alteonbattle", "Ultra Alteon");
        Awe.GetAweRelic("Greaves", 4172, 10, 15, "bosschallenge", "Mutated Void Dragon");
        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
}
