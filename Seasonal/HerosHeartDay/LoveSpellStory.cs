//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class LoveSpell
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        LoveSpellScript();

        Core.SetOptions(false);
    }

    public void LoveSpellScript()
    {
        if (Core.isCompletedBefore(7934))
            return;

        // Something Soft
        Story.KillQuest(7925, "lovespell", "Chinchilla");

        // A Shrubbery!
        Story.KillQuest(7926, "lovespell", "Love Shrub");

        // The Eyes Have It
        Story.KillQuest(7927, "lovespell", "Spring Tog");

        // Pollenate Love
        Story.KillQuest(7928, "lovespell", "Love Shrub");

        // Whiskers on... Chinchillas?
        Story.KillQuest(7929, "lovespell", "Chinchilla");

        // Smooth Mood
        Story.KillQuest(7930, "lovespell", "Mood Slime");

        // Scale it up
        Story.KillQuest(7931, "lovespell", "Spring Tog");

        // Fruitful
        Story.KillQuest(7932, "lovespell", "Love Shrub");

        // Very... heart-y
        Story.KillQuest(7933, "lovespell", "Mood Slime");

        // Break that Heart
        Story.KillQuest(7934, "lovespell", "Stolen Heart");
    }
}