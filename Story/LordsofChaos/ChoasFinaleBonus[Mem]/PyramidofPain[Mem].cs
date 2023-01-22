/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class PyramidofPain
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
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
        {
            Core.Logger("You need to be a member for complete this questline.");
            return;
        }

        Story.PreLoad(this);

        // Prove Your Worth
        Story.KillQuest(3640, "pyramidpain", new[] { "Cactus Creeper ", "Golden Scarab " });
        // Still Not Impressed?
        Story.KillQuest(3641, "pyramidpain", "Pyramid Vase");
        // A Prickly Situation
        Story.KillQuest(3642, "pyramidpain", "Cactus Creeper ");
        // Beetle Battle Armor
        Story.KillQuest(3643, "pyramidpain", "Golden Scarab ");
        // Pyramid Pilgrimage
        Story.KillQuest(3644, "pyramidpain", new[] { "Cactus Creeper ", "Golden Scarab " });
        // Blood in the Sand
        Story.KillQuest(3645, "pyramidpain", "Sandshark ");
        // Defeat Tribal Traitors
        Story.KillQuest(3646, "pyramidpain", "Kalestri Worshipper ");
        // Marking The Territory
        Story.MapItemQuest(3647, "pyramidpain", 2758, 7);
        // Raiders of the Lost Artifact
        Story.KillQuest(3648, "pyramidpain", "Tomb Robber ");
        // Surviving the Swarms
        Story.KillQuest(3649, "pyramidpain", "Sandshark ");
        // The Legend of Eldo
        Story.KillQuest(3650, "pyramidpain", "Pyramid Vase");
        // There's No Point
        Story.KillQuest(3651, "pyramidpain", new[] { "Cactus Creeper ", "Golden Scarab " });
        // Trail of Naan Crumbs
        Story.MapItemQuest(3652, "pyramidpain", 2770, 13);
        // Tricky Trip Wires
        Story.KillQuest(3653, "pyramidpain", "Kalestri Worshipper ");
        // Shooing Away Scarabs
        Story.KillQuest(3655, "pyramidpain", "Golden Scarab ");
        // Humor is Close to the Heart
        Story.KillQuest(3654, "pyramidpain", "Golden Scarab ");
        // Purple is Not Your Color
        Story.KillQuest(3656, "pyramidpain", "Chaorrupted Robber ");
        // All Alone
        Story.KillQuest(3657, "pyramidpain", "Kalestri Worshipper ");
        // Beginning of the End
        Story.KillQuest(3658, "pyramidpain", "Pyramid Vase");
        // The Ultimate Sacrifice
        Story.KillQuest(3659, "pyramidpain", "Viridi");
    }
}
