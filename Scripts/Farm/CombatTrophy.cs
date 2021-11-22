//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class CombatTrophy
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        // Change to false if you need to kill the Restorers and Brawlers
        Farm.BludrutBrawlBoss(canSoloBoss: true);

        Core.SetOptions(false);
    }
}