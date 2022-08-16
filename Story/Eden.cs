//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/Shinkansen.cs
using RBot;

public class Eden
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Shinkansen Shin = new();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        EdenStoryline();

        Core.SetOptions(false);
    }

    public void EdenStoryline()
    {
        if (Core.isCompletedBefore(8801))
            return;

        Story.PreLoad();
        Shin.Storyline();

        //Welcome to Eden! 8795
        Story.KillQuest(8795, "eden", "Harmless Choir");
        //The Fashion and Arcade District 8796
        Story.KillQuest(8796, "eden", "Cosplayer");
        Story.KillQuest(8796, "eden", "Klawaii Machine");
        //Eden City KotaMart 8797
        Story.KillQuest(8797, "eden", "Cosplayer");
        Story.KillQuest(8797, "eden", "SalaryMan");
        //Save the Citizens! 8798
        Story.MapItemQuest(8798, "eden", 10448, 5);
        Story.KillQuest(8798, "eden", "SalaryMan Anomaly");
        Story.KillQuest(8798, "eden", "Cosplayer Anomaly");
        Story.KillQuest(8798, "eden", "Police Anomaly");
        //Armorchy 8799
        Story.KillQuest(8799, "eden", "CRC Power Armor");
        //Clear the Way! 8800
        Story.MapItemQuest(8800, "eden", 10449, 3);
        Story.KillQuest(8800, "eden", "Yokaified Experiment 1");
        //Protect the Reactor! 8801
        Story.KillQuest(8801, "eden", "Major Anomaly");
    }
}
