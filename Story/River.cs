//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class River
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
        if (Core.isCompletedBefore(74))
            return;

        Story.PreLoad(this);

        // Bait and Switch 63
        Story.KillQuest(63, "river", "River Fishman");

        // Restocking 64
        Story.KillQuest(64, "river", "Zardman Fisher");
        
        // Spear Sabotage 65
        Story.KillQuest(65, "river", "Zardman Fisher");
        
        // Retaliation 66
        Story.KillQuest(66, "river", "Kuro");

        if (!Core.IsMember)
        {
            Core.Logger("You must be a Member to complete Fishing Bait and Flood of Power quests.");
            return;
        }
        
        // Fishing Bait 72
        Story.KillQuest(72, "river", "Kuro");

        // Flood of Power 74
        Story.KillQuest(74, "shallow", "Water Elemental");
    }
}
