//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;

public class LegionCombatTrophy
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreLegion Legion = new CoreLegion();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Bot.Options.LagKiller = false;
        Adv.BestGear(GearBoost.Undead);
        //order of quants: Trophy - Technique - Scroll
        Legion.DagePvP(400, 50, 1000);

        Core.SetOptions(false);
    }
}