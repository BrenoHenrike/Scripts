//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class UndervoidStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CompleteUnderVoid();

        Core.SetOptions(false);
    }

    public void CompleteUnderVoid()
    {
        if (!Core.isSeasonalMapActive("undervoid"))
            return;
        if (Core.isCompletedBefore(3406))
            return;

        Story.PreLoad(this);

        Core.AddDrop("Hollowborn Soul Stealer");

        //Dark, Deadly Warmup
        Story.KillQuest(3399, "underworld", "Dark Makai");

        //Destroy the Good
        Story.KillQuest(3400, "alliance", "Good Soldier");

        //Destroy Chaorrupted Evil
        Story.KillQuest(3401, "alliance", "Chaorrupted Evil Soldier");

        //Legion Fenrir Gauntlet
        Story.KillQuest(3402, "underworld", "Legion Fenrir");

        //Conquer Conquest
        Story.KillQuest(3403, "undervoid", "Conquest");

        //Conquer War
        Story.KillQuest(3404, "undervoid", "War");

        //Conquer Famine
        Story.KillQuest(3405, "undervoid", "Famine");

        //Conquer Death
        Story.KillQuest(3406, "undervoid", "Death");
    }
}