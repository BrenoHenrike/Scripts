//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class JoinLegion_UndeadWarrior
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        Core.AddDrop("Ravaged Champion Soul");

        if(!Core.CheckInventory("Undead Warrior"))
            Core.Logger("You need to buy Undead Warrior first. If you don't have 200 Combat Trophies, farm them using Farm/CombatTrophy.cs", messageBox: true, stopBot: true);

        // Undead Champion Initiation
        Core.SmartKillMonster(789, "greenguardwest", "Black Knight", completeQuest: true);

        // Mourn the Soldiers
        Core.SmartKillMonster(790, "dwarfhold", "Chaos Drow", 100);
        Core.SmartKillMonster(790, "swordhavenundead", "Skeletal Soldier", 100);
        Core.SmartKillMonster(790, "pirates", "Fishman Soldier", 100);
        Core.SmartKillMonster(790, "willowcreek", "Dwakel Soldier", 100, true);

        // Understanding Undead Champions
        Core.SmartKillMonster(791, "battleunderb", "Undead Champion");

        // Player vs Power
        if(!Core.CheckInventory("Combat Trophy", 200))
            Farm.BludrutBrawlBoss(quant: 200);
        Core.ChainComplete(792);

        // Fail to the King
        Core.SmartKillMonster(793, "prison", "King Alteon's Knight", completeQuest: true);

        Core.SetOptions(false);
    }
}