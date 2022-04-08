//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/4ShadowfallDarknessRising/CoreDarknessRising.cs

using RBot;

public class CompleteDarknessRising
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreDarknessRising DarknessRising = new();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DarknessRising.ShadowfallInvasion();

        Core.SetOptions(false);
    }
}