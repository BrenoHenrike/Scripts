/*
name: Magic Meaderp Story
description: This will finish the Magic Meaderp storyline.
tags: magic, meaderp, seasonal, april, fools
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MagicMeaderp
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CompleteStory();
        Core.SetOptions(false);
    }

    public void CompleteStory()
    {
        if (!Core.isSeasonalMapActive("magicmeaderp"))
            return;

        if (Core.isCompletedBefore(9183))
        {
            Core.Logger("You have already completed this storyline");
            return;
        }

        Story.PreLoad(this);

        // 9176 Claws and Effect
        Story.MapItemQuest(9176, "magicmeaderp", 11466);

        // 9177 Dog and Pony Show
        Story.MapItemQuest(9177, "magicmeaderp", 11467);

        // 9178 Fine Vulpine
        Story.MapItemQuest(9178, "magicmeaderp", 11468);

        // 9179 Feathered Foray
        Story.MapItemQuest(9179, "magicmeaderp", 11469);

        // 9180 Carrot Quest
        Story.MapItemQuest(9180, "magicmeaderp", 11470);

        // 9181 Funguy
        Story.MapItemQuest(9181, "magicmeaderp", 11471);

        // 9182 Bone Voyage
        Story.MapItemQuest(9182, "magicmeaderp", 11472);

        // 9183 The Pearly Gaits
        Story.MapItemQuest(9183, "magicmeaderp", 11473);
    }
}
