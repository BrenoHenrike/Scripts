//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
using RBot;

public class ArmorOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetArmor();

        Core.SetOptions(false);
    }

    public void GetArmor()
    {
        if (Core.CheckInventory("Armor of Awe"))
            return;

        Awe.GetAweRelic("Pauldon", 4160, 15, 15, "gravestrike", "Ultra Akriloth");
        Awe.GetAweRelic("Breastplate", 4163, 10, 10, "aqlesson", "Carnax");
        Awe.GetAweRelic("Vambrace", 4166, 15, 15, "bloodtitan", "Ultra Blood Titan");
        Awe.GetAweRelic("Gauntlet", 4169, 25, 5, "alteonbattle", "Ultra Alteon");
        Awe.GetAweRelic("Greaves", 4172, 10, 15, "bosschallenge", "Mutated Void Dragon");
        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
}