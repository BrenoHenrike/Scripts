/*
name: neofortressStory
description: gonna farm all storyline in /neofortress for you
tags: neo, fortress, story, line, lae, first, hollowborn
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class NewoFortress
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
        if (Core.isCompletedBefore(9290))
            return;

        Story.PreLoad(this);
        //Watch the Light 9281
        Story.MapItemQuest(9281, "neofortress", 11806, 9);

        //Uprootment Recruitment 9282
        Story.KillQuest(9282, "neofortress", "Vindicator Recruit");

        //Endless Hounding 9283
        Story.KillQuest(9283, "neofortress", "Vindicator Hound");

        //Mystery Creature 9284
        Story.KillQuest(9284, "neofortress", "Vindicator Beast");

        //Retrieve the Keys 9285
        Story.KillQuest(9285, "neofortress", "Vindicator Soldier");

        //Free the Prisoners 9286
        Story.MapItemQuest(9286, "neofortress", 11807, 5);

        //Vindicator General 9287
        Story.KillQuest(9287, "neofortress", "Vindicator General");

        //De-dicated 9288
        Story.KillQuest(9288, "neofortress", "Vindicator Recruit");

        //Get into the Chambers 9289
        Story.KillQuest(9289, "neofortress", "Vindicator General");

        //Tales from the Past 9290
        Story.MapItemQuest(9290, "neofortress", 11808);
    }
}
