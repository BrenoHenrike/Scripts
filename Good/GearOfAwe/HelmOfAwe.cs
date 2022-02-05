//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs


using RBot;

public class HelmOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public CoreFarms Farm = new CoreFarms();

    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetHoA();

        Core.SetOptions(false);
    }

    public void GetHoA()
    {
        if (Core.IsMember)
            LegendaryHoA();
        else if (Awe.GuardianCheck())
            GuardianHoA();
        else
            FreeHoA();
    }

    public void LegendaryHoA()
    {
        Core.AddDrop("Legendary Awe Pass", "Helm Shard", "Helm Fragment", "Helm Relic", "Helm of Awe");
        
        Core.BuyItem("museum", 1130, "Legendary Awe Pass");

        Core.EquipClass(ClassType.Solo);

        while (!Core.CheckInventory("Helm Fragment", 10))
            Awe.AweKill(4175, "helm");

        Core.BuyItem("museum", 1129, "Helm Relic");

        Core.BuyItem("museum", 1129, "Helm of Awe");

        Core.ToBank("Legendary Awe Pass");
    }
    
    public void GuardianHoA()
    {
        Core.AddDrop("Helm Shard", "Helm Fragment", "Helm Relic", "Helm of Awe");
        
        Farm.BladeofAweREP(5, false);

        Farm.Experience(35);

        Core.EquipClass(ClassType.Solo);

        while (!Core.CheckInventory("Helm Fragment", 10))
            Awe.AweKill(4176, "helm");

        Core.BuyItem("museum", 1129, "Helm Relic");

        Core.BuyItem("museum", 1129, "Helm of Awe");

        Core.ToBank("Guardian Awe Pass");
    }
    public void FreeHoA()
    {
        Core.AddDrop("Armor of Awe Pass", "Helm Shard", "Helm Fragment", "Helm Relic", "Helm of Awe");

        Farm.BladeofAweREP(10, false);

        Farm.Experience(55);

        Core.BuyItem("museum", 1130, "Armor of Awe Pass");

        Core.EquipClass(ClassType.Solo);

        while (!Core.CheckInventory("Helm Fragment", 10))
            Awe.AweKill(4177, "helm");

        Core.BuyItem("museum", 1129, "Helm Relic");

        Core.BuyItem("museum", 1129, "Helm of Awe");

        Core.ToBank("Armor of Awe Pass");
    }

}