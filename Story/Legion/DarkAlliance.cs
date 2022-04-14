//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
using RBot;

public class DarkAlliance_Story
{
    public CoreBots Core => CoreBots.Instance;
    public DarkAlly_Story DarkAlly = new DarkAlly_Story();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DarkAlly.DarkAlly_Questline();
        DarkAlliance_Questline();

        Core.SetOptions(false);
    }

    public void DarkAlliance_Questline()
    {
        if (Core.isCompletedBefore(7460))
            return;

        Story.PreLoad();

        //Clear the Shadows --DAGE--
        Story.MapItemQuest(7446, "darkalliance", 7224, 8);
        Story.KillQuest(7446, "darkalliance", "Shadow");
        //Destroy the Swords
        Story.MapItemQuest(7447, "darkalliance", 7225, 1);
        Story.KillQuest(7447, "darkalliance", "Shadowblade");
        //Fuel for the Forge
        Story.KillQuest(7448, "darkalliance", "Shadow Void");
        //Sever the Connection
        Story.KillQuest(7449, "darkalliance", "Shadow Makai");
        //Armor Against the Shadow
        Story.KillQuest(7450, "darkalliance", "Shadow Void");
        //Aid from the Pyromancer
        Story.MapItemQuest(7451, "darkalliance", 7226, 8);
        //Find the Underflame --Frozen Pyromancer--
        Story.MapItemQuest(7452, "darkalliance", 7227, 1);
        //Gather some Underlava
        Story.KillQuest(7453, "darkalliance", "Underlava");
        //Gather Sulfur
        Story.KillQuest(7454, "darkalliance", "Underworld Imp");
        //Coal for Flame
        Story.MapItemQuest(7455, "darkalliance", 7228, 6);
        //Defeat the Underflame Guardian
        Story.KillQuest(7456, "darkalliance", "Underflame Guardian");
        //Take the Flame
        Story.MapItemQuest(7457, "darkalliance", 7229, 1);
        //Remove the Shadows --dage--
        Story.KillQuest(7458, "darkalliance", "Shadow|Shadow Makai");
        //Burn the Shadows
        Story.MapItemQuest(7459, "darkalliance", 7230, 1);
        //Defeat Shadow Nulgath
        Story.KillQuest(7460, "darkalliance", "ShadowFlame Nulgath");
    }
}
