//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class ChillysQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        //Make sure your email is verified!
        ChillysParticipation();

        Core.SetOptions(false);
    }

    public void ChillysParticipation()
    {
        if (Core.isCompletedBefore(9004))
            return;

        Core.EnsureAccept(9004);
        Core.HuntMonster("battleontown", "Festive Zard", "Reminder Delivered");
        Core.EnsureComplete(9004);
    }
}
