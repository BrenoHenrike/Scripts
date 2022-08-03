//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class ShadowWar
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.isCompletedBefore(6852))
            return;

        Story.PreLoad();

        //Shadow Medals: Defend the Village! 6846
        Story.KillQuest(6846, "shadowwar", "Shadowflame Slasher");

        //Seed Spitter Oil 6847
        Story.KillQuest(6847, "shadowwar", "Seed Spitter");

        //Mega Shadow Medals 6848
        Story.KillQuest(6848, "shadowwar", "Shadowflame Slasher");

        //Shadow Samples 6849
        Story.KillQuest(6849, "shadowwar", "Umbral Goo");

        //Shadow Medals: Fight them Back! 6850
        Story.KillQuest(6850, "shadowwar", "Shadowflame Slasher");

        //Interrogation 6851
        Story.KillQuest(6851, "shadowwar", "Shadowflame Scout");

        //Defeat Malgor! 6852
        Story.KillQuest(6852, "malgor", "Malgor");
    }
}