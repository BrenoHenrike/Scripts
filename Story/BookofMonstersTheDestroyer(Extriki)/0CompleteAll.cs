//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/BookofMonstersTheDestroyer(Extriki)/CoreExtriki.cs
using RBot;

public class CompleteExtriki
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreExtriki Extriki => new CoreExtriki();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Extriki.CompleteCoreExtriki();

        Core.SetOptions(false);
    }
}