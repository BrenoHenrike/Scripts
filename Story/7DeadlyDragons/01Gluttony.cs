using System.Collections.Concurrent;
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Gluttony
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GluttonySaga();

        Core.SetOptions(false);
    }

    public void GluttonySaga()
    {
        if (Core.isCompletedBefore(5915))
            return;

        // Guts for Glutes
        Story.KillQuest(5903, "gluttony", "Glutus");
        // Mucus be Joking
        Story.KillQuest(5904, "gluttony", "Mucus");
        // Build a Bone-a-fied Ladder
        Story.MapItemQuest(5905, "gluttony", 5346, 2);
        Story.KillQuest(5905, "gluttony", "Skeletal Slayer");
        // Talk About Reflux
        Story.MapItemQuest(5906, "gluttony", 5344, 1);
        // How'd HE get here?
        Story.MapItemQuest(5907, "gluttony", 5345, 1);
        // Glowworms not Glowsticks
        Story.KillQuest(5908, "gluttony", "Bowel Worm");
        Story.MapItemQuest(5908, "gluttony", 5347, 10);
        // Ossification Needed
        if(!Story.QuestProgression(5909))
        {
            Core.EnsureAccept(5909);
            Core.KillMonster("gluttony", "r6", "Left", "Bile", "Falgar's Bones", 9);
            Core.EnsureComplete(5909);
        }
        // Eye Need Bones
        Story.MapItemQuest(5910, "gluttony", 5348, 5);
        Story.KillQuest(5910, "gluttony", "Skeletal Slayer");
        // Bile Burns
        Story.KillQuest(5911, "gluttony", new[] { "Bile", "Bowel Worm"});
        // Find the Chest
        Story.MapItemQuest(5912, "gluttony", 5349, 1);
        // We Need the Key
        Story.KillQuest(5913, "gluttony", "Giant Tapeworm");
        // Cha Cha Cha
        Story.MapItemQuest(5914, "gluttony", 5350, 1);
        // Glutus, Take 2
        Story.KillQuest(5915, "gluttony", "Deflated Glutus");

    }
}
