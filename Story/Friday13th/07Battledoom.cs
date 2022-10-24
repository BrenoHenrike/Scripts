//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Friday13th/CoreFriday13th.cs
using Skua.Core.Interfaces;

public class Battledoom
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFriday13th CoreFriday13th = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreFriday13th.Battledoom();

        Core.SetOptions(false);
    }

}