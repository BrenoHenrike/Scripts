//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
using RBot;

public class CompleteAstraviaJudgement
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreAstravia Astravia => new CoreAstravia();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Astravia.AstraviaJudgement();

        Core.SetOptions(false);
    }
}