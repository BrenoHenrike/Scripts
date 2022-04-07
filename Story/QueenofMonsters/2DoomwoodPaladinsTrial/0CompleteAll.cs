//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/2DoomwoodPaladinsTrial/CorePaladinsTrial.cs

using RBot;

public class CompletePaladinsTrial
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CorePaladinsTrial PaladinsTrial = new();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        PaladinsTrial.CompleteCorePaladinsTrial();

        Core.SetOptions(false);
    }
}