//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;

public class LegionFealty2
{
    // If you can go to /Join Mummies change it to true.
    public bool MummiesUnlocked { get; } = false;
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.LR);
        Core.AddDrop(Legion.LF2);

        ConquestWreath();

        Core.SetOptions(false);
    }

    public void ConquestWreath(int quant = 6)
    {
        if (Core.CheckInventory("Conquest Wreath", quant))
            return;

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Conquest Wreath");
        while (!Core.CheckInventory("Conquest Wreath", quant))
        {
            Core.EnsureAccept(6898);

            if(MummiesUnlocked)
                Core.KillMonster("mummies", "Enter", "Spawn", "*", "Ancient Cohort Conquered", 500, false);
            else
                Core.KillMonster("cruxship", "r10", "Left", "Mummy", "Ancient Cohort Conquered", 500, false);
            
            Core.KillMonster("doomvault", "r1", "Right", "*", "Grim Cohort Conquered", 500, false);
            Core.KillMonster("wrath", "r5", "Left", "*", "Pirate Cohort Conquered", 500, false);
            Core.KillMonster("doomwar", "r6", "Left", "*", "Battleon Cohort Conquered", 500, false);
            Core.KillMonster("overworld", "Enter", "Spawn", "*", "Mirror Cohort Conquered", 500, false);
            Core.KillMonster("deathpits", "r1", "Left", "*", "Darkblood Cohort Conquered", 500, false);
            Core.KillMonster("maxius", "r2", "Left", "*", "Vampire Cohort Conquered", 500, false);
            Core.KillMonster("curseshore", "Enter", "Spawn", "*", "Spirit Cohort Conquered", 500, false);
            Core.KillMonster("dragonbone", "Enter", "Spawn", "*", "Dragon Cohort Conquered", 500, false);
            Core.KillMonster("doomwood", "r6", "Right", "*", "Doomwood Cohort Conquered", 500, false);

            Core.EnsureComplete(6898);
            Bot.Player.Pickup("Conquest Wreath");
            Core.Logger($"Completed x{i}");
            i++;
        }
    }
}