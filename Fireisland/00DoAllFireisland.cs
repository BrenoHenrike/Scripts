//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Phoenixrise.cs
//cs_include Scripts/Story/Fireisland/CoreFireisland.cs

using Skua.Core.Interfaces;

public class DoAllFireisland
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public PhoenixriseStory PhoenixriseStory = new();


    public Fireisland Fireisland = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Fireisland.CompleteFireIsland();

        Core.SetOptions(false);
    }


}