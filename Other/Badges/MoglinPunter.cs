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

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        if (!Core.isSeasonalMapActive("Punt"))
            return;

        Core.Join("Punt");
        while (!Bot.ShouldExit && !Core.HasWebBadge("Moglin Punter"))
        {
            Core.Jump("Enter", "Spawn");
            Bot.Send.Packet("%xt%zm%ia%1%rval%btnPuntting%%");
        }
    }
}
