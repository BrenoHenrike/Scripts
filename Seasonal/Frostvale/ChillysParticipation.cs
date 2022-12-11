//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class ChillysQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ChillysParticipation();

        Core.SetOptions(false);
    }

    public void ChillysParticipation()
    {
        Core.Logger("Make sure email is verified!");
        if (Core.isCompletedBefore(9004))
            return;

        Core.EnsureAccept(9004);
        Core.HuntMonster("battleontown", "Festive Zard", "Reminder Delivered");
        Core.EnsureComplete(9004);
    }
}