//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
using RBot;

public class CompleteCelestialRealm
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreQOM QOM => new();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        QOM.CompleteCelestialRealmATheftofLight();

        Core.SetOptions(false);
    }
}