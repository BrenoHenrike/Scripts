//cs_include Scripts/CoreBots.cs

using RBot;

public class CoreAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

public bool GuardianCheck()
    {
        Core.Logger("Checking AQ Guardian");
        if (Core.CheckInventory("Guardian Awe Pass", 1, true))
        {
            Core.Logger("You're AQ Guardian!");
            return true;
        }
        Core.BuyItem("museum", 53, "Guardian Awe Pass");
        if (Core.CheckInventory("Guardian Awe Pass"))
        {
            Core.Logger("Guardian Awe Pass bought successfully! You're AQ Guardian!");
            return true;
        }
        else
        {
            Core.Logger("You're not AQ Guardian.");
            return false;
        }
    }

    public void AweKill(int questID, string gear)
    {
       if(gear.Equals("pauldron"))
       {
            Core.EnsureAccept(questID);
            Core.KillMonster("gravestrike", "r1", "Left", "Ultra Akriloth", "Pauldron Shard", 15, false, true);
            Core.EnsureComplete(questID);
            return;
        }
        else if(gear.Equals("breastplate"))
        {
            Core.EnsureAccept(questID);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Breastplate Shard", 10, false, true);
            Core.EnsureComplete(questID);
            return;
        }
        else if(gear.Equals("vambrace"))
        {
            Core.EnsureAccept(questID);
            Core.KillMonster("bloodtitan", "Ultra", "Left", "Ultra Blood Titan", "Vambrace Shard", 15, false, true, true);
            Core.EnsureComplete(questID);
            return;
        }
        else if(gear.Equals("gauntlet"))
        {
            Core.EnsureAccept(questID);
            Core.KillMonster("alteonbattle", "Enter", "Spawn", "Ultra Alteon", "Gauntlet Shard", 5, false, true);
            Core.EnsureComplete(questID);
            return;
        }
        else if(gear.Equals("greaves"))
        {
            Core.EnsureAccept(questID);
            Core.KillMonster("bosschallenge", "r17", "Left", "Mutated Void Dragon", "Greaves Shard", 15, false, true, true);
            Core.EnsureComplete(questID);
            return;
        }
        else if(gear.Equals("helm"))
        {
            Core.EnsureAccept(questID);
            Core.KillMonster("doomvaultb", "r26", "Left", "Undead Raxgore", "Helm Shard", 5, false, true, true);
            Core.EnsureComplete(questID);
            return;
        }
        else
        {
            Core.EnsureAccept(questID);
            Core.HuntMonster("doomvault", "Binky", "Cape Shard", 1, false, true, false);
            Core.EnsureComplete(questID);
            return;
        }
    }

}