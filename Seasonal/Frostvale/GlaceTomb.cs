/*
name: Glace Tomb
description: This will finish the Glace Tomb Storyline.
tags: glace-tomb-story, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class GlaceTomb
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GlaceTombQuest();
        
        Core.SetOptions(false);
    }

    public void GlaceTombQuest()
    {
        if (Core.isCompletedBefore(9506))
            return;

        Story.PreLoad(this);

        // PTA Meeting 9497
        Story.MapItemQuest(9497, "glacetomb", new[] { 12421, 12422 });

        // Bear Essentials 9498
        Story.KillQuest(9498, "glacetomb", "Auberon");

        // Powder Sugar Faeries 9499
        Story.KillQuest(9499, "glacetomb", "Snow Fairy");

        // Water Intoxication 9500
        Story.KillQuest(9500, "glacetomb", "Auberon");

        // Wet Pages 9501
        if (!Story.QuestProgression(9501))
        {
            Core.EnsureAccept(9501);
            Story.MapItemQuest(9501, "glacetomb", 12423, 7);
            Story.MapItemQuest(9501, "glacetomb", new[] { 12424, 12425 });
            Core.EnsureComplete(9501);
        }

        // IceBox Break In 9502
        if (!Story.QuestProgression(9502))
        {
            Core.EnsureAccept(9502);
            Story.KillQuest(9502, "glacetomb", "Snow Fairy");
            Story.MapItemQuest(9502, "glacetomb", 12426);
            Core.EnsureComplete(9502);
        }

        // Necrocollege Rejects 9503
        if (!Story.QuestProgression(9503))
        {
            Core.EnsureAccept(9503);
            Story.MapItemQuest(9503, "glacetomb", new[] { 12427, 12428 });
            Story.KillQuest(9503, "glacetomb", "Draugr");
            Core.EnsureComplete(9503);
        }

        // Necrocollege Rejects 9504
        Story.MapItemQuest(9504, "glacetomb", 12429, 5);

        // Exhibit on Ice 9505
        if (!Story.QuestProgression(9505))
        {
            Core.EnsureAccept(9505);
            Story.KillQuest(9505, "glacetomb", "Draugr");
            Story.MapItemQuest(9505, "glacetomb", 12430, 3);
            Core.EnsureComplete(9505);
        }

        // Academic Probation 9506
        Story.KillQuest(9506, "glacetomb", "Kriomein");
    }
}
