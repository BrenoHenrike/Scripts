//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Lightguard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        StoryLine();
        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.IsMember)
        {
            Core.Logger("Michem Quests Is Member Only. Skipping this Script");
            return;
        }
            
        if (Core.isCompletedBefore(2036))
            return;

        Story.PreLoad(this);

        //Barely Made It 2031
        Story.MapItemQuest(2031, "doomwood", 983);

        //Plant Food 2032
        Story.KillQuest(2032, "doomwood", "Doomwood Treeant");

        //Supplies Party 2033
        Story.KillQuest(2033, "darkness", new[] { "Doomwood Bonemuncher", "Doomwood Ectomancer" });

        //Copious Notes 2034
        Story.KillQuest(2034, "doomwood", "Doomwood Bonemuncher");

        //Lightguard Keystone 2035
        Story.MapItemQuest(2035, "lightguard", 982);

        //Defend Lightguard Keep! 2036
        Story.KillQuest(2036, "lightguard", "Mysterious Spirit");

    }
}
