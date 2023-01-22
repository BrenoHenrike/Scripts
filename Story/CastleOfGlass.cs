/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CastleOfGlass
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
        if (Core.isCompletedBefore(5356))
            return;

        Story.PreLoad(this);

        //Where’s Tiffany? 5339
        Story.MapItemQuest(5339, "castleofglass", 4698);

        //Trapped! 5340
        Story.MapItemQuest(5340, "castleofglass", new[] { 4699, 4710 });
        Story.KillQuest(5340, "castleofglass", "Glass Wyvern");

        //If At First You Don’t Succeed... 5341
        Story.MapItemQuest(5341, "castleofglass", new[] { 4700, 4711 });
        Story.KillQuest(5341, "castleofglass", "Glass Panther");

        //Try, Try Again 5342
        Story.MapItemQuest(5342, "castleofglass", new[] { 4701, 4712 });
        Story.KillQuest(5342, "castleofglass", "Mirror Knight");

        //Escape! 5343
        Story.MapItemQuest(5343, "castleofglass", 4702);

        //Smash It Up 5344
        Story.KillQuest(5344, "castleofglass", "Glass Golem");

        //Find the Pieces 5345
        Story.KillQuest(5345, "castleofglass", "Glass Wyvern");

        //Finish the First Mosaic 5346
        Story.MapItemQuest(5346, "castleofglass", 4703);

        //Find More Pieces 5347
        Story.KillQuest(5347, "castleofglass", new[] { "Glass Golem", "Glass Panther" });

        //Finish the Second Mosaic 5348
        Story.MapItemQuest(5348, "castleofglass", 4704);

        //Even  More Pieces 5349
        Story.KillQuest(5349, "castleofglass", new[] { "Glass Golem", "Glass Panther", "Glass Wyvern" });

        //Finish the Third Mosaic 5350
        Story.MapItemQuest(5350, "castleofglass", 4705);

        //Cut and Run 5351
        Story.MapItemQuest(5351, "castleofglass", 4706);

        //The Shard Golem 5352
        Story.KillQuest(5352, "castleofglass", "Shard Golem");

        //The Shardy Boys 5353
        Story.MapItemQuest(5353, "castleofglass", 4707, 5);
        Story.KillQuest(5353, "castleofglass", "Glass Golem");

        //Cold Iron 5354
        Story.MapItemQuest(5354, "castleofglass", 4708);

        //Smash It Up Again 5355
        Story.MapItemQuest(5355, "castleofglass", 4709, 7);

        //What The Heck Is This Thing?! 5356
        Story.KillQuest(5356, "castleofglass", "Chihuly");

    }
}
