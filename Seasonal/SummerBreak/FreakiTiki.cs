//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FreakiTikiStory
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
        if (Core.isCompletedBefore(5571))
            return;

        Story.PreLoad();

        //5558 | Drink #1: the Blue Moglin
        Story.MapItemQuest(5558, "yulgar", 5034);

        //5559 | A Little Something Extra
        Story.KillQuest(5559, "thespan", "Minx Fairy");

        //5560 | Mix a Blue Moglin
        Story.MapItemQuest(5560, "freakitiki", 5038);

        //5561 | Drink #2: the El Captain Rhubarb
        Story.KillQuest(5561, "freakitiki", new[] { "Spineapple", "Palm Treeant" });
        Story.MapItemQuest(5561, "freakitiki", 5035, 5);

        //5562 | Needs a Little Kick
        Story.KillQuest(5562, "pirates", "Undead Pirate");

        //5563 | Mix an El Captain Rhubarb
        Story.MapItemQuest(5563, "freakitiki", 5039);

        //5564 | Drink #3: the Blazing Beard
        Story.MapItemQuest(5564, "freakitiki", 5036, 5);
        Story.KillQuest(5564, "freakitiki", new[] { "Tiki Sneak", "Palm Treeant" });

        //5565 | Let’s Give It Some HEAT
        Story.KillQuest(5565, "fotia", "Fotia Spirit");

        //5566 | Mix a Blazing Beard
        Story.MapItemQuest(5566, "freakitiki", 5040);

        //5567 | System Cleanse
        Story.KillQuest(5567, "freakitiki", new[] { "Sneak Venom", "Sugar Imp", "Spicy Heat" });

        //5568 | Down the, um, Ear Hole
        Story.MapItemQuest(5568, "freakitiki", 5037);

        //5569 | Tire Him Out!
        if (!Story.QuestProgression(5569))
        {
            Core.EnsureAccept(5569);
            Core.HuntMonsterMapID("freakitiki", 22, "Subdue Memehano");
            Core.EnsureComplete(5569);
            Core.Logger("Completed Quest: [5569] - \"Tire Him Out!\"");
        }

        //5570 | Chase Him Down!
        if (!Story.QuestProgression(5570))
        {
            Core.EnsureAccept(5570);
            Core.HuntMonsterMapID("freakitiki", 22, "Subdue Memehano");
            Core.EnsureComplete(5570);
            Core.Logger("Completed Quest: [5570] - \"Chase Him Down!\"");
        }

        //5571 | Get Him CALM!
        if (!Story.QuestProgression(5571))
        {
            Core.EnsureAccept(5571);
            Core.HuntMonsterMapID("freakitiki", 32, "Subdue Memehano");
            Core.EnsureComplete(5571);
            Core.Logger("Completed Quest: [5571] - \"Get Him CALM!\"");
        }
    }
}