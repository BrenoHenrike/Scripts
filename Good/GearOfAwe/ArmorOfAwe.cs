//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
using RBot;

public class ArmorOfAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAwe Awe = new CoreAwe();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetArmor();

        Core.SetOptions(false);
    }

    public void GetArmor()
    {
        if(Core.IsMember)
            LegendaryAoA();
        else if(Awe.GuardianCheck())
            GuardianAoA();
        else 
            FreeAoA();
    }

    public void LegendaryAoA()
    {
        Core.AddDrop("Legendary Awe Pass", "Pauldron Shard", "Pauldron Fragment", "Pauldron Relic", "Breastplate Shard", "Breastplate Fragment", "Breastplate Relic", "Vambrace Shard",
        "Vambrace Fragment", "Vambrace Relic", "Gauntlet Shard", "Gauntlet Fragment", "Gauntlet Relic", "Greaves Shard", "Greaves Fragment", "Greaves Relic", "Armor of Awe");

        Core.EquipClass(ClassType.Solo);

        Core.BuyItem("museum", 1130, "Legendary Awe Pass");

        if (!Core.CheckInventory("Pauldron Relic", 1))
        {
            if (Awe.GuardianCheck())
            {
                Farm.BladeofAweREP(5, false);
                Farm.Experience(35);
                while (!Core.CheckInventory("Pauldron Fragment", 15))
                    Awe.AweKill(4161, "pauldron");
            }
            else
            {
                Farm.BladeofAweREP(10, false);
                Farm.Experience(55);
                while (!Core.CheckInventory("Pauldron Fragment", 15))
                    Awe.AweKill(4162, "pauldron");

            }

            Core.BuyItem("museum", 1129, "Pauldron Relic");
        }

        if (!Core.CheckInventory("Breastplate Relic", 1))
        {
            while (!Core.CheckInventory("Breastplate Fragment", 10))
                Awe.AweKill(4163, "breastplate");

            Core.BuyItem("museum", 1129, "Breastplate Relic");
        }

        if(!Core.CheckInventory("Vambrace Relic", 1))
        {
            while (!Core.CheckInventory("Vambrace Fragment", 15))
                Awe.AweKill(4166, "vambrace");

            Core.BuyItem("museum", 1129, "Vambrace Relic");
        }

        if(!Core.CheckInventory("Gauntlet Relic", 1, true))
        {
            while (!Core.CheckInventory("Gauntlet Fragment", 25, true))
                Awe.AweKill(4169, "gauntlet");

            Core.BuyItem("museum", 1129, "Gauntlet Relic");
        }

        if(!Core.CheckInventory("Greaves Relic", 1, true))
        {
           while (!Core.CheckInventory("Greaves Fragment", 10, true))
                Awe.AweKill(4172, "greaves");

            Core.BuyItem("museum", 1129, "Greaves Relic");
        }

        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank(new[] { "Legendary Awe Pass", "Greaves Shard", "Gauntlet Shard", "Vambrace Shard", "Breastplate Shard", "Pauldron Shard" });
    }

    public void GuardianAoA()
    {
        Core.AddDrop("Pauldron Shard", "Pauldron Fragment", "Pauldron Relic", "Breastplate Shard", "Breastplate Fragment", "Breastplate Relic", "Vambrace Shard", "Vambrace Fragment", 
        "Vambrace Relic", "Gauntlet Shard", "Gauntlet Fragment", "Gauntlet Relic", "Greaves Shard", "Greaves Fragment", "Greaves Relic", "Armor of Awe");

        Farm.BladeofAweREP(5, false);

        Farm.Experience(35);

        Core.EquipClass(ClassType.Solo);

        if (!Core.CheckInventory("Pauldron Relic", 1))
        {
            while (!Core.CheckInventory("Pauldron Fragment", 15))
                Awe.AweKill(4161, "pauldron");

            Core.BuyItem("museum", 1129, "Pauldron Relic");
        }

        if (!Core.CheckInventory("Breastplate Relic", 1))
        {
            while (!Core.CheckInventory("Breastplate Fragment", 10))
                Awe.AweKill(4164, "breastplate");

            Core.BuyItem("museum", 1129, "Breastplate Relic");
        }

        if(!Core.CheckInventory("Vambrace Relic", 1))
        {
            while (!Core.CheckInventory("Vambrace Fragment", 15))
                Awe.AweKill(4167, "vambrace");

            Core.BuyItem("museum", 1129, "Vambrace Relic");
        }

        if(!Core.CheckInventory("Gauntlet Relic", 1))
        {
            while (!Core.CheckInventory("Gauntlet Fragment", 25))
                Awe.AweKill(4170, "gauntlet");

            Core.BuyItem("museum", 1129, "Gauntlet Relic");
        }

        if(!Core.CheckInventory("Greaves Relic", 1))
        {
            while (!Core.CheckInventory("Greaves Fragment", 10))
                Awe.AweKill(4173, "greaves");

            Core.BuyItem("museum", 1129, "Greaves Relic");
        }

        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank(new[] { "Guardian Awe Pass", "Greaves Shard", "Gauntlet Shard", "Vambrace Shard", "Breastplate Shard", "Pauldron Shard" });
    }

    public void FreeAoA()
    {
        Core.AddDrop("Armor of Awe Pass", "Pauldron Shard", "Pauldron Fragment", "Pauldron Relic", "Breastplate Shard", "Breastplate Fragment", "Breastplate Relic", "Vambrace Shard",
        "Vambrace Fragment", "Vambrace Relic", "Gauntlet Shard", "Gauntlet Fragment", "Gauntlet Relic", "Greaves Shard", "Greaves Fragment", "Greaves Relic", "Armor of Awe");

        Core.BuyItem("museum", 1130, "Armor of Awe Pass");

        Farm.BladeofAweREP(10, false);

        Farm.Experience(55);

        Core.EquipClass(ClassType.Solo);

        if (!Core.CheckInventory("Pauldron Relic", 1))
        {
            while (!Core.CheckInventory("Pauldron Fragment", 15))
                Awe.AweKill(4162, "pauldron");

            Core.BuyItem("museum", 1129, "Pauldron Relic");
        }

        if (!Core.CheckInventory("Breastplate Relic", 1))
        {
            while (!Core.CheckInventory("Breastplate Fragment", 10))
                Awe.AweKill(4165, "breastplate");

            Core.BuyItem("museum", 1129, "Breastplate Relic");
        }

        if(!Core.CheckInventory("Vambrace Relic", 1))
        {
            while (!Core.CheckInventory("Vambrace Fragment", 15))
                Awe.AweKill(4168, "vambrace");

            Core.BuyItem("museum", 1129, "Vambrace Relic");
        }

        if(!Core.CheckInventory("Gauntlet Relic", 1))
        {
            while (!Core.CheckInventory("Gauntlet Fragment", 25))
                Awe.AweKill(4171, "gauntlet");

            Core.BuyItem("museum", 1129, "Gauntlet Relic");
        }

        if(!Core.CheckInventory("Greaves Relic", 1))
        {
            while (!Core.CheckInventory("Greaves Fragment", 10))
                Awe.AweKill(4174, "greaves");

            Core.BuyItem("museum", 1129, "Greaves Relic");
        }

        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank(new[] { "Armor of Awe Pass", "Greaves Shard", "Gauntlet Shard", "Vambrace Shard", "Breastplate Shard", "Pauldron Shard" });
    }
}