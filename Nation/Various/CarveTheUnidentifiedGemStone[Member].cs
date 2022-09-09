//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class CarveTheUnidentifiedGemStone
{

    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Nation.CarveUniGemStone();

        Core.SetOptions(false);
    }
}