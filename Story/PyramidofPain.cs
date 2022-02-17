//cs_include Scripts/CoreBots.cs
using RBot;
public class PyramidofPain
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        PyramidofPain_Line();

        Core.SetOptions(false);
    }

    public void PyramidofPain_Line()
    {
        if (Core.isCompletedBefore(3659))
            return;

        if (!Core.IsMember)
            Core.Logger("You need to be a member for complete this questline.", messageBox: true, stopBot: true);
        else
        // Prove Your Worth
        Core.KillQuest(3640, "pyramidpain", new[] { "Cactus Creeper ", "Golden Scarab "});
        // Still Not Impressed?
        Core.KillQuest(3641, "pyramidpain", "Pyramid Vase");
        // A Prickly Situation
        Core.KillQuest(3642, "pyramidpain", "Cactus Creeper ");
        // Beetle Battle Armor
        Core.KillQuest(3643, "pyramidpain", "Golden Scarab ");
        // Pyramid Pilgrimage
        Core.KillQuest(3644, "pyramidpain", new[] { "Cactus Creeper ", "Golden Scarab "});
        // Blood in the Sand
        Core.KillQuest(3645, "pyramidpain", "Sandshark ");
        // Defeat Tribal Traitors
        Core.KillQuest(3646, "pyramidpain", "Kalestri Worshipper ");
        // Marking The Territory
        Core.MapItemQuest(3647, "pyramidpain", 2758, 7);
        // Raiders of the Lost Artifact
        Core.KillQuest(3648, "pyramidpain", "Tomb Robber ");
        // Surviving the Swarms
        Core.KillQuest(3649, "pyramidpain", "Sandshark ");
        // The Legend of Eldo
        Core.KillQuest(3650, "pyramidpain", "Pyramid Vase");
        // There's No Point
        Core.KillQuest(3651, "pyramidpain", new[] { "Cactus Creeper ", "Golden Scarab "});
        // Trail of Naan Crumbs
        Core.MapItemQuest(3652, "pyramidpain", 2770, 13);
        // Tricky Trip Wires
        Core.KillQuest(3653, "pyramidpain", "Kalestri Worshipper ");
        // Shooing Away Scarabs
        Core.KillQuest(3655, "pyramidpain", "Golden Scarab ");
        // Humor is Close to the Heart
        Core.KillQuest(3654, "pyramidpain", "Golden Scarab ");
        // Purple is Not Your Color
        Core.KillQuest(3656, "pyramidpain", "Chaorrupted Robber ");
        // All Alone
        Core.KillQuest(3657, "pyramidpain", "Kalestri Worshipper ");
        // Beginning of the End
        Core.KillQuest(3658, "pyramidpain", "Pyramid Vase");
        // The Ultimate Sacrifice
        Core.KillQuest(3659, "pyramidpain", "Viridi");
    }
}
