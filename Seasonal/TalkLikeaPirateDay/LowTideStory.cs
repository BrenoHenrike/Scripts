/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class LowTideStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (!Core.isSeasonalMapActive("lowtide"))
            return;
        if (Core.isCompletedBefore(8845))
            return;

        Story.PreLoad(this);

        //Precious Rocks 8836
        Story.KillQuest(8836, "lowtide", "Lone Pirate");

        //Fake Seagull Food 8837
        Story.MapItemQuest(8837, "lowtide", 10520, 5);

        //Summer Chill 8838
        Story.KillQuest(8838, "lowtide", "Ghostly Eel");

        //Committed to the Sea 8839
        if (!Story.QuestProgression(8839))
        {
            Core.EnsureAccept(8839);
            Core.Logger("Doing Quest: [8839] - \"Committed to the Sea\"", "QuestProgression");
            Core.HuntMonster("lowtide", "Ghostly Eel", "Eels Exorcized", 6);
            Core.HuntMonster("lowtide", "Lone Pirate", "Pirates Apprehended", 8);
            Core.GetMapItem(10521, 5, "lowtide");
            Core.EnsureComplete(8839);
            Core.Logger("Completed Quest: [8839] - \"Committed to the Sea\"", "TryComplete");
        }
        else Core.Logger("Already Completed: [8839] - \"Committed to the Sea\"", "QuestProgression");

        //Briny Gelatin 8840
        Story.KillQuest(8840, "lowtide", "Spectral Jellyfish");

        //Sleeping with the Fishes 8841
        Story.MapItemQuest(8841, "lowtide", 10522, 3);

        //Local Delicacies 8842
        Story.KillQuest(8842, "lowtide", new[] { "Ghostly Eel", "Spectral Jellyfish" });

        //Myriad Andromeda 8843
        Story.MapItemQuest(8843, "lowtide", 10523);
        Story.KillQuest(8843, "lowtide", "Lone Pirate");

        //Ebbing Grudge 8844
        Story.MapItemQuest(8844, "lowtide", 10524);

        //Honeyed Apologies 8845
        Story.KillQuest(8845, "lowtide", "Exiled General Miel");
    }
}
