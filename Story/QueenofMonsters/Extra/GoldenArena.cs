//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class GoldenArena
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
        if (Core.isCompletedBefore(6085))
            return;

        Story.PreLoad(this);

        //Greenguard Champion Badge 6076
        Story.KillQuest(6076, "DragonChallenge", "Greenguard Dragon");

        //Draconic Laurel Challenge 6077
        Story.KillQuest(6077, "GoldenArena", "Blessed Dragon");

        //Inquisitor's Champion Badge 6078
        Story.KillQuest(6078, "Citadel", "Grand Inquisitor");

        //Grand Laurel Challenge 6079
        Story.KillQuest(6079, "GoldenArena", "Blessed Inquisitor");

        //SkyGuard Champion Badge 6080
        Story.KillQuest(6080, "Airship", "Gladius");

        //Aeriel Laurel Challenge 6081
        Story.KillQuest(6081, "GoldenArena", "Blessed Gladius");

        //Fallen Champion Badge 6082
        Story.KillQuest(6082, "northstar", "Karok the Fallen");

        //Ascension Laurel Challenge 6083
        Story.KillQuest(6083, "GoldenArena", "Blessed Karok");

        //Seraphic Champion Badge 6084
        Story.KillQuest(6084, "Envy ", "Queen of Envy");

        //Golden Laurel Challenge 6085
        Story.KillQuest(6085, "GoldenArena", "Queen of Hope");

    }
}
