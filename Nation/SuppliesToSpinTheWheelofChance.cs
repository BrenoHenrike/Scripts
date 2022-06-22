//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class SuppliesToSpinTheWheelofChance
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops[..^11]);
        Core.SetOptions();

        /*Core.AddDrop("Wicked Edge Cape", "ArchFiend's Spikes", "ArchFiend's Vampragon",
         * "Unidentified 14", "Unidentified 15", "Unidentified 16", "Unidentified 17", 
         * "Unidentified 18", "Unidentified 2", "Unidentified 1", "Unidentified 12", 
         * "Unidentified 20", "Unidentified 21", "Unidentified 26", "Unidentified 28", 
         * "Unidentified 29", "Unidentified 30", "Unidentified 31", "Unidentified 32", 
         * "Unidentified 33", "Ddog's Sea Serpent Armor", "Primal Dread Fang", "Claw of Nulgath", 
         * "Random Weapon of Nulgath", "Unidentified 3", "Unidentified 4", "Unidentified 5", 
         * "Unidentified 6", "Unidentified 7", "Unidentified 8", "Unidentified 9");*/

        Nation.Supplies();

        Core.SetOptions(false);
    }
}
