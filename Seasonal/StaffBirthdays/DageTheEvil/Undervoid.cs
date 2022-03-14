//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class UndervoidStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CompleteUnderVoid();

        Core.SetOptions(false);
    }

    public void CompleteUnderVoid()
    {
        //Progress Check
        if (Core.isCompletedBefore(3406))
            return;

        //Needed AddDrop
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
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(3403, "undervoid", "Conquest");

        //Conquer War
        Story.KillQuest(3404, "undervoid", "War");

        //Conquer Famine
        Story.KillQuest(3405, "undervoid", "Famine");

        //Conquer Death
        Story.KillQuest(3406, "undervoid", "Death");
    }
}