//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Good/CoreAwe.cs

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
            while (!Core.CheckInventory("Pauldron Fragment", 15))
            {
                Core.EnsureAccept(4160);
                Core.KillMonster("gravestrike", "r1", "Left", "Ultra Akriloth", "Pauldron Shard", 15, false, true);
                Core.EnsureComplete(4160);
            }

            Core.BuyItem("museum", 1129, "Pauldron Relic");
        }

        if (!Core.CheckInventory("Breastplate Relic", 1))
        {
            while (!Core.CheckInventory("Breastplate Fragment", 10))
            {
                Core.EnsureAccept(4163);
                Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Breastplate Shard", 10, false, true);
                Core.EnsureComplete(4163);
            }

            Core.BuyItem("museum", 1129, "Breastplate Relic");
        }

        if(!Core.CheckInventory("Vambrace Relic", 1))
        {
            while (!Core.CheckInventory("Vambrace Fragment", 15))
            {
                Core.EnsureAccept(4166);
                Core.KillMonster("bloodtitan", "Ultra", "Left", "Ultra Blood Titan", "Vambrace Shard", 15, false, true, true);
                Core.EnsureComplete(4166);
            }

            Core.BuyItem("museum", 1129, "Vambrace Relic");
        }

        if(!Core.CheckInventory("Gauntlet Relic", 1, true))
        {
            while (!Core.CheckInventory("Gauntlet Fragment", 25, true))
            {
                Core.EnsureAccept(4169);
                Core.KillMonster("alteonbattle", "Enter", "Spawn", "Ultra Alteon", "Gauntlet Shard", 5, false, true);
                Core.EnsureComplete(4169);
            }

            Core.BuyItem("museum", 1129, "Gauntlet Relic");
        }

        if(!Core.CheckInventory("Greaves Relic", 1, true))
        {
           while (!Core.CheckInventory("Greaves Fragment", 10, true))
            {
                Core.EnsureAccept(4172);
                Core.KillMonster("bosschallenge", "r17", "Left", "Mutated Void Dragon", "Greaves Shard", 15, false, true, true);
                Core.EnsureComplete(4172);
            }

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
            {
                Core.EnsureAccept(4161);
                Core.KillMonster("gravestrike", "r1", "Left", "Ultra Akriloth", "Pauldron Shard", 15, false, true);
                Core.EnsureComplete(4161);
            }

            Core.BuyItem("museum", 1129, "Pauldron Relic");
        }

        if (!Core.CheckInventory("Breastplate Relic", 1))
        {
            while (!Core.CheckInventory("Breastplate Fragment", 10))
            {
                Core.EnsureAccept(4164);
                Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Breastplate Shard", 10, false, true);
                Core.EnsureComplete(4164);
            }

            Core.BuyItem("museum", 1129, "Breastplate Relic");
        }

        if(!Core.CheckInventory("Vambrace Relic", 1))
        {
            while (!Core.CheckInventory("Vambrace Fragment", 15))
            {
                Core.EnsureAccept(4167);
                Core.KillMonster("bloodtitan", "Ultra", "Left", "Ultra Blood Titan", "Vambrace Shard", 15, false, true, true);
                Core.EnsureComplete(4167);
            }

            Core.BuyItem("museum", 1129, "Vambrace Relic");
        }

        if(!Core.CheckInventory("Gauntlet Relic", 1))
        {
            while (!Core.CheckInventory("Gauntlet Fragment", 25))
            {
                Core.EnsureAccept(4170);
                Core.KillMonster("alteonbattle", "Enter", "Spawn", "Ultra Alteon", "Gauntlet Shard", 5, false, true);
                Core.EnsureComplete(4170);
            }

            Core.BuyItem("museum", 1129, "Gauntlet Relic");
        }

        if(!Core.CheckInventory("Greaves Relic", 1))
        {while (!Core.CheckInventory("Greaves Fragment", 10))
            {
                Core.EnsureAccept(4173);
                Core.KillMonster("bosschallenge", "r17", "Left", "Mutated Void Dragon", "Greaves Shard", 15, false, true, true);
                Core.EnsureComplete(4173);
            }

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
            {
                Core.EnsureAccept(4162);
                Core.KillMonster("gravestrike", "r1", "Left", "Ultra Akriloth", "Pauldron Shard", 15, false, true);
                Core.EnsureComplete(4162);
            }

            Core.BuyItem("museum", 1129, "Pauldron Relic");
        }

        if (!Core.CheckInventory("Breastplate Relic", 1))
        {
            while (!Core.CheckInventory("Breastplate Fragment", 10))
            {
                Core.EnsureAccept(4165);
                Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Breastplate Shard", 10, false, true);
                Core.EnsureComplete(4165);
            }

            Core.BuyItem("museum", 1129, "Breastplate Relic");
        }

        if(!Core.CheckInventory("Vambrace Relic", 1))
        {
            while (!Core.CheckInventory("Vambrace Fragment", 15))
            {
                Core.EnsureAccept(4168);
                Core.KillMonster("bloodtitan", "Ultra", "Left", "Ultra Blood Titan", "Vambrace Shard", 15, false, true, true);
                Core.EnsureComplete(4168);
            }

            Core.BuyItem("museum", 1129, "Vambrace Relic");
        }

        if(!Core.CheckInventory("Gauntlet Relic", 1))
        {
            while (!Core.CheckInventory("Gauntlet Fragment", 25))
            {
                Core.EnsureAccept(4171);
                Core.KillMonster("alteonbattle", "Enter", "Spawn", "Ultra Alteon", "Gauntlet Shard", 5, false, true);
                Core.EnsureComplete(4171);
            }

            Core.BuyItem("museum", 1129, "Gauntlet Relic");
        }

        if(!Core.CheckInventory("Greaves Relic", 1))
        {
            while (!Core.CheckInventory("Greaves Fragment", 10))
            {
                Core.EnsureAccept(4174);
                Core.KillMonster("bosschallenge", "r17", "Left", "Mutated Void Dragon", "Greaves Shard", 15, false, true, true);
                Core.EnsureComplete(4174);
            }

            Core.BuyItem("museum", 1129, "Greaves Relic");
        }

        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank(new[] { "Armor of Awe Pass", "Greaves Shard", "Gauntlet Shard", "Vambrace Shard", "Breastplate Shard", "Pauldron Shard" });
    }
}