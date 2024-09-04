/*
name: Chairman Platinum's Evolving Environmental Art Gallery
description: This script will finish the storyline in /ebilart.
tags: ebilart, ebil, art, ebil art, april fools, seasonal, story, ai, art gallery
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class EbilArt
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
        if (Core.isCompletedBefore(9659) || !Core.isSeasonalMapActive("ebilart"))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Going Going Is Bananas (9650)
        Story.KillQuest(9650, "ebilart", "GorillAIphant");

        // Your FrAInds! (9651)
        Story.MapItemQuest(9651, "ebilart", new[] { 12874, 12875 });

        // Green Soy Fodder (9652)
        Story.KillQuest(9652, "ebilart", "AI Clones");

        // WIPs (9653)
        Story.MapItemQuest(9653, "ebilart", 12876, 4);

        // Anatomical Nightmare (9654)
        Story.KillQuest(9654, "ebilart", "UNUNSkellingdens");

        // Dead Eyed (9655)
        Story.MapItemQuest(9655, "ebilart", new[] { 12877, 12878 });

        // Unspecified Seafood (9656)
        Story.KillQuest(9656, "ebilart", "Fish");

        // Too Literal (9657)
        Story.MapItemQuest(9657, "ebilart", new[] { 12879, 12880 });

        // Oversaturation (9658)
        Story.KillQuest(9658, "ebilart", new[] { "Fish", "UNUNSkellingdens" });

        // Pull the Plug (9659)
        if (!Core.isCompletedBefore(9659))
        {
            Core.EquipClass(ClassType.Solo);
            Story.KillQuest(9659, "ebilart", "Ebil AI Blender");
            Core.EquipClass(ClassType.Farm);
        }
    }
}
