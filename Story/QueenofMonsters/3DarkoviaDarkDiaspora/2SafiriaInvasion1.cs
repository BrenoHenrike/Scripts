//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/QueenofMonsters/3DarkoviaDarkDiaspora/CoreDarkDiaspora.cs

using RBot;

public class CompleteSafiriaInvasion
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreDarkDiaspora DarkDiaspora = new();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DarkDiaspora.SafiriaInvasion();

        Core.SetOptions(false);
    }
}