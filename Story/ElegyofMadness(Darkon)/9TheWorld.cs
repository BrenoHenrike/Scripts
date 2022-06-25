//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;

public class CompleteTheWorld
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAstravia Astravia => new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Astravia.TheWorld();

        Core.SetOptions(false);
    }
}