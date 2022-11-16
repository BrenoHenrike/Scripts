//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LovesCurse
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
        if (!Core.isSeasonalMapActive("lovescurse") || Core.isCompletedBefore(1671))
            return;

        Story.PreLoad(this);

        //Find the Rest of J6 (1656)
        Story.KillQuest(1656, "curseshore", new[] { "Escaped Ghostly Zardman", "Escaped Ghostly Zardman", "Escaped Ghostly Zardman" });

        //Mer'Angel Hunting (1657)
        Story.MapItemQuest(1657, "curseshore", 865);

        //Into the Underworld (1666)
        Story.ChainQuest(1666);

        //Search for the Cursed Lovers (1658)
        Story.MapItemQuest(1658, "curseblue", 866);

        //Love Ghosts (1667)
        Story.ChainQuest(1667);

        //Sing the Blues (1659)
        Story.KillQuest(1659, "curseblue", "Ghostly Aracara");

        //Land of the Red (1660)
        Story.MapItemQuest(1660, "cursered", 868);

        //Help Moore (1668)
        Story.ChainQuest(1668);

        //Find the Source of the Curse (1661)
        Story.KillQuest(1661, "cursered", new[] { "Cyclops Warlord Ghost", "Cyclops Warlord Ghost" });

        //Defeat the CurseMaker (1662)
        Story.KillQuest(1662, "mercutio", "*");

        //Mercutio (1669)
        Story.ChainQuest(1669);

        //One Moore Time (1663)
        Story.MapItemQuest(1663, "cursered", 869);

        //Out of Time (1670)
        Story.ChainQuest(1670);

        //You Only Get One Shot at True Love (1664)
        Story.MapItemQuest(1664, "curseblue", 867);

        //Happy Ending (1671)
        Story.ChainQuest(1671);
    }
}