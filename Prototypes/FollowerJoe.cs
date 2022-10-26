using Skua.Core.Interfaces;

public class FollowerJoe
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Bot.ShowMessageBox("FollowerJoe has been renamed and moved to \'Scripts/Tools/Butler.cs\'", "FollowerJoe is now Butler");
    }
}
