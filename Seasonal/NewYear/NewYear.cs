//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class NewYear
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(1531) || !Core.isSeasonalMapActive("newyear"))
            return;

        Story.PreLoad(this);

        //newyear

        //Circuit Breakers (463)
        Story.KillQuest(463, "newyear", "Sneevil");

        //Current Affairs (464)
        Story.KillQuest(464, "newyear", new[] { "Sneevil", "Sneevil" });

        //Not Finished Yet-i (465)
        Story.KillQuest(465, "newyear", "Ice Master Yeti");

        //newyearlab

        //Is the Area as Big Inside as Outside? (1524)
        Story.MapItemQuest(1524, "newyearlab", 762);

        //The Final Hour? (1525)
        Story.KillQuest(1525, "newyearlab", "Chaos-Saw Sneevil");

        //See-Sawing Through Time (1526)
        Story.MapItemQuest(1526, "newyearlab", 763, 6);

        //A Crack in Time Saves More Than Nine (1527)
        Story.KillQuest(1527, "newyearlab", new[] { "Chaos Rhino Beetle", "Chaos Rhino Beetle" });

        //Time Bashes On (1528)
        Story.KillQuest(1528, "newyearlab", "Chaorrupted Polar Bear");

        //SHUTDOWN Sequence (1530)
        Story.MapItemQuest(1530, "newyearlab", new[] { 764, 765, 766, 767 });

        //Chronomancy and Chaos (1531)
        Story.KillQuest(1531, "newyearlab", "Iadoa");
    }
}