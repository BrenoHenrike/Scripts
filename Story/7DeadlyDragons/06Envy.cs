//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Envy
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        EnvySaga();

        Core.SetOptions(false);
    }

    public void EnvySaga()
    {
        if (Core.isCompletedBefore(6000))
            return;

        // Talk to the Guard
        Story.MapItemQuest(5983, "dragoncrown", 5420, 1);
        // Talk to the Villager
        Story.MapItemQuest(5984, "dragoncrown", 5421, 1);
        // Fire and Ice
         Story.MapItemQuest(5985, "dragoncrown", 5422, 6);
        Story.KillQuest(5985, "dragoncrown", "Fire Sprite");
        // Round ‘em Up
        Story.KillQuest(5986, "dragoncrown", new[] {"Llama", "Rampaging Boar"});
        // Get Rockin’
        Story.KillQuest(5987, "dragoncrown", new[] {"Rock Elemental", "Earth Elementa"});
        // Stop Fightin’
         Story.MapItemQuest(5988, "dragoncrown", 5423, 1);
        // Find Riddrug
        Story.MapItemQuest(5989, "dragoncrown", 5424, 1);
        // Defeat The Red Champion
        Story.KillQuest(5990, "dragoncrown", "Torgat");
        // Defeat The Ice Champion
        Story.KillQuest(5991, "dragoncrown", "Fressa");
        // Defeat The Undead Champion
        Story.KillQuest(5992, "dragoncrown", "Radroth");
        // Defeat The Water Champion
        Story.KillQuest(5993, "dragoncrown", "Nizex");
        // Defeat The Wind Champion
        Story.KillQuest(5994, "dragoncrown", "Tathu");
        // Defeat The Yokai Champion
        Story.KillQuest(5995, "dragoncrown", "Lanshen");
        // Defeat The Green Champion
        Story.KillQuest(5996, "dragoncrown", "Ashax");
        // Defeat The Faerie Champion
        Story.KillQuest(5997, "dragoncrown", "Letori");
        // Defeat The Chaos Champion
        Story.KillQuest(5998, "dragoncrown", "Nayzol");
        // Defeat the Void Champion
        Story.KillQuest(5999, "dragoncrown", "Zathas");
        // Argo’s Not Stopping Us!
        Story.KillQuest(6000, "dragoncrown", "Argo");
    }
}
