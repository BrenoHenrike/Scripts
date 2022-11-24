//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BeleensDream
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
        if (Core.isCompletedBefore(3365))
            return;

        Story.PreLoad(this);

        //Crabby Valentine 3356
        Story.KillQuest(3356, "beleensdream", "Beach Crab");

        //Chinchillin' 3357
        Story.KillQuest(3357, "beleensdream", "Chinchilla");

        //Confectionary Wisdom 3358
        Story.KillQuest(3358, "beleensdream", "Fortune Cookie");

        //Loony balloons 3359
        Story.MapItemQuest(3359, "beleensdream", 2475);
        Story.MapItemQuest(3359, "beleensdream", 2476, 3);
        Story.KillQuest(3359, "beleensdream", "Bulloon");

        //Disgruntled Draconians 3360
        Story.KillQuest(3360, "beleensdream", "Disgruntled Draconian");

        //Paws n' D'awwws! 3361
        Story.KillQuest(3361, "beleensdream", "Disgruntled Doomkitten");

        //Power of Love... Elementals 3362
        Story.KillQuest(3362, "beleensdream", "Heart Elemental");

        //Red Velvet Cake Rope 3363
        if (!Story.QuestProgression(3363))
        {
            Core.EnsureAccept(3363);
            Core.HuntMonster("beleensdream", "Beach Crab", "Crustacean Construct");
            Core.HuntMonster("beleensdream", "Fortune Cookie", "Solidified Sugar");
            Core.HuntMonster("beleensdream", "Bulloon", "Indestructible Latex");
            Core.HuntMonster("beleensdream", "Disgruntled Draconian", "Draconian's Blade");
            Core.EnsureComplete(3363);
        }

        //The Legendary Cherry 3364
        Story.MapItemQuest(3364, "beleensdream", 2477);

        //Your Just Deserts 3365
        Story.KillQuest(3365, "beleensdream", "Bluddron the Betrayer");

    }
}