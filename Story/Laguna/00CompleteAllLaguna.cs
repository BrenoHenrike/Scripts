//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Laguna/01DualPlane.cs
//cs_include Scripts/Story/Laguna/02ChaosAmulet.cs
//cs_include Scripts/Story/Laguna/03LagunaBeach.cs
//cs_include Scripts/Story/Laguna/04Laguna.cs
using RBot;

public class CompleteLaguna
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public DualPlane s01 = new DualPlane();
    public ChaosAmulet s02 = new ChaosAmulet();
    public LagunaBeach s03 = new LagunaBeach();
    public Laguna s04 = new Laguna();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteALL();

        Core.SetOptions(false);
    }

    public void CompleteALL()
    {
        Core.Logger("DualPlane");
        s01.StoryLine();
        Core.Logger("ChaosAmulet");
        s02.StoryLine();
        Core.Logger("LagunaBeach");
        s03.StoryLine();
        Core.Logger("Laguna");
        s04.StoryLine();
    }
}
