//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class DarkVictorySeal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DarkVictorySealFarm();

        Core.SetOptions(false);
    }

    public void DarkVictorySealFarm()
    {
        if (!Core.isSeasonalMapActive("dagerecruit"))
            return;
        Core.AddDrop("Dark Victory Seal");

        //Progress Check
        if (!Core.isCompletedBefore(8575))
        {
            Core.Logger("Please run the DageRecruit Story bot before this one as you have not unlocked the required quests.");
            return;
        }

        //Dark Victory Seal
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Victory Seal", 1000))
        {
            Core.EnsureAccept(8576);
            Core.HuntMonster("dagerecruit", "Dark Makai", "Dark Makai Defeated", 6);
            Core.HuntMonster("dagerecruit", "Dreadfiend", "Dreadfiend Defeated", 6);
            Core.HuntMonster("dagerecruit", "Bloodfiend", "Bloodfiend Defeated", 6);
            Core.HuntMonster("dagerecruit", "Infernal Fiend", "Infernal Fiend Defeated", 6);
            Core.EnsureComplete(8576);
        }
    }
}