/*
name: MoglinPunter
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class MoglinPunter
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        Bot.Options.LagKiller = false;
        if (Core.HasWebBadge(badge) || !Core.isSeasonalMapActive("punt"))
        {
            Core.Logger($"Already have the {badge} badge, or the map is not available.");
            return;
        }
        Core.OneTimeMessage("Minigame Explanation", "This minigame works off of a \"value\" system for ponts, so 9999 is 99, for the quest so youll need to get a value of 10000 points which may take a while.", forcedMessageBox: true);

        Core.Logger($"Doing quest for {badge} badge");
        Core.Join("punt");
        int Punt = 0;
        while (!Bot.ShouldExit && !Core.HasWebBadge(badge))
        {
            Core.Jump("Enter", "Right");
            Bot.Sleep(Core.ActionDelay);
            Bot.Send.Packet("%xt%zm%ia%1%rval%btnPuntting%%");
            Bot.Wait.ForCellChange("Punt");
            Punt++;
        }
        Core.Logger($"Punts to get the badge: {Punt}");
    }

    private string badge = "Moglin Punter";

}
