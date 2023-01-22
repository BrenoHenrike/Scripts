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

public class CapeOfAwe
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

        GetCoA();

        Core.SetOptions(false);
    }

    public void GetCoA()
    {
        if (Core.CheckInventory("Cape of Awe"))
            return;

        Awe.GetAweRelic("Cape", 4178, 1, 1, "doomvault", "Binky");
        Core.BuyItem("museum", 1129, "Cape of Awe");

        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
}
