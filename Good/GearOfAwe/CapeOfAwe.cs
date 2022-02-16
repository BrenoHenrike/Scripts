//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
using RBot;

public class CapeOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetCoA();

        Core.SetOptions(false);
    }

    public void GetCoA()
    {
        if(Core.IsMember)
            LegendaryAwe();
        else if(Awe.GuardianCheck())
            GuardianAwe();
        else 
            FreeAwe();
    }

    public void LegendaryAwe()
    {
        Core.AddDrop("Legendary Awe Pass", "Cape Shard", "Cape Fragment", "Cape Relic", "Cape of Awe");
        
        Core.BuyItem("museum", 1130, "Legendary Awe Pass");

        Core.EquipClass(ClassType.Solo);

        Awe.AweKill(4178, "cape");

        Core.BuyItem("museum", 1129, "Cape Relic");

        Core.BuyItem("museum", 1129, "Cape of Awe");

        Core.ToBank("Legendary Awe Pass");
    }
    
    public void GuardianAwe()
    {
        Core.AddDrop("Cape Shard", "Cape Fragment", "Cape Relic", "Cape of Awe");
        
        Farm.BladeofAweREP(5, false);

        Farm.Experience(35);

        Core.EquipClass(ClassType.Solo);

        Awe.AweKill(4179, "cape");

        Core.BuyItem("museum", 1129, "Cape Relic");

        Core.BuyItem("museum", 1129, "Cape of Awe");

        Core.ToBank("Guardian Awe Pass");
    }
    public void FreeAwe()
    {
        Core.AddDrop("Armor of Awe Pass", "Cape Shard", "Cape Fragment", "Cape Relic", "Cape of Awe");

        Farm.BladeofAweREP(10, false);

        Farm.Experience(55);

        Core.BuyItem("museum", 1130, "Armor of Awe Pass");

        Core.EquipClass(ClassType.Solo);

        Awe.AweKill(4180, "cape");

        Core.BuyItem("museum", 1129, "Cape Relic");

        Core.BuyItem("museum", 1129, "Cape of Awe");

        Core.ToBank("Armor of Awe Pass");
    }
}