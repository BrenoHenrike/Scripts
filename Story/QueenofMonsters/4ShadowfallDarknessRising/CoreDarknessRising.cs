using RBot;

public class CoreDarknessRising
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public void ShadowfallInvasion()
    {

        //Progress Check
        if (Core.isCompletedBefore(5557))
            return;

        //Preload Quests
        Story.PreLoad();

        //Commander Tibias
        Story.MapItemQuest(5543, "ShadowfallInvasion", 5024);

        //Clear the Walls
        Story.KillQuest(5544, "ShadowfallInvasion", new[] { "Infernal Imp", "Infernal Knight" });

        //Load the Ballistas
        Story.MapItemQuest(5545, "ShadowfallInvasion", 5025, 4);
        Story.MapItemQuest(5545, "ShadowfallInvasion", 5026, 4);

        //Arm the Archers
        Story.KillQuest(5546, "ShadowfallInvasion", "Infernal Imp|Infernal Knight");

        //Reinforce the Walls
        Story.MapItemQuest(5547, "ShadowfallInvasion", 5027, 5);

        //Work Done!
        Story.MapItemQuest(5548, "ShadowfallInvasion", 5028);

        //Clear the Tower
        Story.KillQuest(5549, "ShadowfallInvasion", "Bone Creeper|Undead Knight");

        //Find the Passage
        Story.MapItemQuest(5550, "ShadowfallInvasion", 5029);

        //Go Through That Door
        Story.KillQuest(5551, "ShadowfallInvasion", new[] { "Bone Guardian", "Bone Guardian" });

        //Infernal Attack
        Story.KillQuest(5552, "ShadowfallInvasion", new[] { "Nethermage", "Diabolical Scryer", "Fallen Knight" });

        //Here's A Hammer, Get To Work
        Story.MapItemQuest(5553, "ShadowfallInvasion", 5030, 9);

        //The Next Step
        Story.MapItemQuest(5554, "ShadowfallInvasion", 5031);

        //Soul Fuel
        Story.KillQuest(5555, "ShadowfallInvasion", "Diabolical Scryer|Nethermage");
        Story.MapItemQuest(5555, "ShadowfallInvasion", 5032);

        //Time to Fly
        Story.MapItemQuest(5556, "ShadowfallInvasion", 5033);

        //YOU again!
        Story.KillQuest(5557, "ShadowfallInvasion", "Lord Balax'el");
    }
}