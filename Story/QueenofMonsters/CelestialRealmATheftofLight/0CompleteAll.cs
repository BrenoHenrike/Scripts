//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/CelestialRealmATheftofLight/CoreCelestialRealm.cs
using RBot;

public class CompleteCelestialRealm
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreCelestialRealm Celestial => new CoreCelestialRealm();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Celestial.CompleteCoreCelestialRealm();

        Core.SetOptions(false);
    }
}