//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs

//cs_include Scripts/CoreFile(Or folder)/Filename.cs

using RBot;

public class YeeaaahhBabbyyThaaatsWhatImTalkingAbout
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WOOOOOOOOOOOOOOO();

        Core.SetOptions(false);
    }

    public void WOOOOOOOOOOOOOOO()
    {
        Core.KillQuest(8497, "Mverse", new[] { "Major Mushroom","Major Mushroom","Major Mushroom","Major Mushroom" }, hasFollowup: false);
    }
}