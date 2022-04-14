//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class LivingDungeon
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        LivingDungeonStory();

        Core.SetOptions(false);
    }

    public void LivingDungeonStory()
    {
        if (Core.isCompletedBefore(4384))
            return;

        Story.PreLoad();

        // Titan Hollow
        Story.ChainQuest(4348);

        // Roots of all Evil
        Story.KillQuest(4349, "livingdungeon", "Root of Evil");

        // Venus Hero Trap
        Story.KillQuest(4350, "livingdungeon", "Seed Spitter");

        // Bark is worse than its bite
        Story.KillQuest(4351, "livingdungeon", new[] { "Evil Plant Horror", "Titan Decay" });

        // Knot what you expected
        Story.KillQuest(4352, "livingdungeon", "Weeping Widowmaker");

        // Cha Cha Cha Chia!
        Story.KillQuest(4353, "livingdungeon", "Chia Warrior");

        // Leaf me alone!
        Story.KillQuest(4354, "livingdungeon", new[] { "Seed Spitter", "Evil Plant Horror", "Titan Decay" });

        // Evil Faerie Ambush!
        Story.KillQuest(4355, "livingdungeon", "Evil Tree Faerie");

        // Check the Trunk
        Story.KillQuest(4356, "livingdungeon", "Vulchurion");

        // Committing Tree-son
        Story.KillQuest(4357, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie", "Vulchurion" });

        // Heartwood
        Story.KillQuest(4358, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie", "Vulchurion" });

        // Drayko BOSS FIGHT!
        Story.KillQuest(4359, "livingdungeon", "Drayko");

        // Foilaged again!
        Story.KillQuest(4360, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie" });

        // Mind Games
        Story.KillQuest(4361, "livingdungeon", new[] { "Evil Plant Horror", "Evil Tree Faerie" });

        // DRAGON vs TITAN
        Story.KillQuest(4362, "treetitanbattle", "Dakka the Dire Dragon");

        // Smells like trouble!
        Story.KillQuest(4363, "livingdungeon", "Lil' Poot");

        // EPIC DROP!
        Story.KillQuest(4364, "livingdungeon", "Epic Drop");

        // Re-Run Titan Hollow
        Story.KillQuest(4377, "livingdungeon", "Root of Evil");

        // Evil Plant Horrors
        Story.KillQuest(4378, "livingdungeon", "Evil Plant Horror");

        // Weeping Widowmakers!
        if (!Story.QuestProgression(4379))
        {
            Core.EnsureAccept(4379);
            Core.HuntMonster("livingdungeon", "Weeping Widowmaker", "Widowmaker deboned", 5);
            Core.EnsureComplete(4379);
        }

        // Chia Warriors
        Story.KillQuest(4380, "livingdungeon", "Chia Warrior");

        // Evil Tree Faeries
        Story.KillQuest(4381, "livingdungeon", "Evil Tree Faerie");

        // Vulchurions
        Story.KillQuest(4382, "livingdungeon", "Vulchurion");

        // Drayko Battle!
        Story.KillQuest(4383, "livingdungeon", "Drayko");

        // DRAGON vs TITAN Rematch!
        Story.KillQuest(4384, "treetitanbattle", "Dakka the Dire Dragon");
    }
}