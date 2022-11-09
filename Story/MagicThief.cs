//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MagicThief
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(1916))
            return;

        // 1908|Sounds Like Cysero
        Story.MapItemQuest(1908, "twilightslums", 952);

        // 1909|Magic-flavored Misery
        Story.KillQuest(1909, "twilightslums", "Subrysa");

        // 1910|Sending out a Stress-OS
        Story.MapItemQuest(1910, "twilightslums", 954);

        // 1911|Lock-Blocked
        Story.MapItemQuest(1911, "palace", 955);

        // 1912|Bridge Over Troubled Wellmet
        Story.MapItemQuest(1912, "palace", 956);

        // 1913|Focus on the Locus
        Story.MapItemQuest(1913, "twilightslums", 957);

        // 1914|Locus Located
        Story.MapItemQuest(1914, "twilightslums", 958);

        // 1915|Demented Deorysa
        Story.KillQuest(1915, "palace", "Deorysa");

        // 1916|Traitor Takedown
        Story.ChainQuest(1916);
    }
}