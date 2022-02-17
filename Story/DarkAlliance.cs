//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/DarkAlly.cs
using RBot;

public class DarkAlliance_Story
{
    public CoreBots Core => CoreBots.Instance;
    public DarkAlly_Story DarkAlly = new DarkAlly_Story();
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

        Core.EquipClass(ClassType.Solo);

        //Clear the Shadows --DAGE--
        Core.MapItemQuest(7446, "darkalliance", 7224, 8);
        Core.KillQuest(7446, "darkalliance", "Shadow");
        //Destroy the Swords
        Core.MapItemQuest(7447, "darkalliance", 7225, 1);
        Core.KillQuest(7447, "darkalliance", "Shadowblade");
        //Fuel for the Forge
        Core.KillQuest(7448, "darkalliance", "Shadow Void");
        //Sever the Connection
        Core.KillQuest(7449, "darkalliance", "Shadow Makai");
        //Armor Against the Shadow
        Core.KillQuest(7450, "darkalliance", "Shadow Void");
        //Aid from the Pyromancer
        Core.MapItemQuest(7451, "darkalliance", 7226, 8);
        //Find the Underflame --Frozen Pyromancer--
        Core.MapItemQuest(7452, "darkalliance", 7227, 1);
        //Gather some Underlava
        Core.KillQuest(7453, "darkalliance", "Underlava");
        //Gather Sulfur
        Core.KillQuest(7454, "darkalliance", "Underworld Imp");
        //Coal for Flame
        Core.MapItemQuest(7455, "darkalliance", 7228, 6);
        //Defeat the Underflame Guardian
        Core.KillQuest(7456, "darkalliance", "Underflame Guardian");
        //Take the Flame
        Core.MapItemQuest(7457, "darkalliance", 7229, 1);
        //Remove the Shadows --dage--
        Core.KillQuest(7458, "darkalliance", "Shadow|Shadow Makai");
        //Burn the Shadows
        Core.MapItemQuest(7459, "darkalliance", 7230, 1);
        //Defeat Shadow Nulgath
        Core.KillQuest(7460, "darkalliance", "ShadowFlame Nulgath");
    }
}
