//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BeastMakerStory
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
            Core.Logger("Beast Maker Storyline Is Member Only. Skipping this Script");
            return;
        }

        if (Core.isCompletedBefore(2234))
            return;

        Story.PreLoad(this);

        //Find the Shadow's Door 2222
        Story.MapItemQuest(2222, "neverlore", 1315);

        //One-way Trip to Neverworld 2223
        Story.KillQuest(2223, "neverlore", "Whablobble");

        //Slash the Shadows 2224
        Story.KillQuest(2224, "neverworld", "Spid-Squider|Snackistopheles");

        //Deprogramming for Dummies 2225
        Story.MapItemQuest(2225, "neverworld", 1316, 10);
        Story.KillQuest(2225, "neverworld", "Snackistopheles");

        //Guardian of the Lab-rary 2226
        Story.MapItemQuest(2226, "neverworld", 1317);
        if (!Story.QuestProgression(2226))
        {
            Core.EnsureAccept(2226);
            Core.HuntMonster("neverworld", "Snackistopheles", "Answer 1");
            Core.HuntMonster("neverworld", "Spid-Squider", "Answer 2");
            Core.HuntMonster("neverworld", "Snackistopheles", "Answer 3");
            Core.HuntMonster("neverworld", "Spid-Squider", "Answer 4");
            Core.HuntMonster("neverworld", "Fishizzle", "Answer 5");
            Core.EnsureComplete(2226);
        }

        //Quick switch! 2227
        Story.MapItemQuest(2227, "neverworld", 1318, 5);
        Story.MapItemQuest(2227, "neverworld", 1326);

        //We're BOOM-ed! 2228
        Story.MapItemQuest(2228, "neverworld", 1321);

        //De-Generation Situation 2229
        Story.KillQuest(2229, "neverworld", "Generator");

        //Shielded Against the Shadows 2230
        Story.MapItemQuest(2230, "neverworld", 1319);
        Story.KillQuest(2230, "neverworld", "Fishizzle");

        //Making a Many-armed Army 2231
        Story.KillQuest(2231, "neverworld", "Kennel Door");

        //Kreature Kibble  Khaos 2232
        Story.MapItemQuest(2232, "neverworld", 1320, 10);

        //The Gang's Not All Here 2233
        Story.KillQuest(2233, "neverworld", "Fishizzle|Spid-Squider");

        //Beast Maker Beat-down 2234
        Story.ChainQuest(2234);

    }
}
