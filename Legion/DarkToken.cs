//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/WorldSoul.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class DarkToken
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public WorldSoul WS = new WorldSoul();
    public CoreAdvanced Adv = new CoreAdvanced();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WS.WorldSoulQuests();
        Legion.DarkToken();

        Core.SetOptions(false);
    }
}