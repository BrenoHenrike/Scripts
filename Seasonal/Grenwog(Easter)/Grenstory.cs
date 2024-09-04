/*
name: Grenstory
description: This script will complete the questline in grenstory.
tags: grenstory, seasonal, grenwog, easter
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Grenstory
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(4936) || !Core.isSeasonalMapActive("grenstory"))
            return;

        Story.PreLoad(this);

        // Egg-Streme Attraction (4925)
        Story.KillQuest(4925, "grenstory", "Chinchilizard");

        // Rotten Eggs?! (4926)
        Story.KillQuest(4926, "grenstory", "Imposter Egg");

        // Protect the Grenwog (4927)
        Story.KillQuest(4927, "grenstory", "Lemurphant");

        // Ceremony at Risk (4928)
        Story.KillQuest(4928, "grenstory", new[] { "Jurassic Orc", "Jurassic Orc", "Jurassic Orc" });

        // Bloodtusk Vision Quest (4929)
        Story.KillQuest(4929, "grenstory", new[] { "Ravine Tigriff", "Chinchilizard", "Rhison", "Lemurphant" });

        // Armed with Spirit (4930)
        Story.KillQuest(4930, "grenstory", new[] { "Lemurphant", "Rhison", "Dinoid" });

        // Cheers to You! (4931)
        Story.KillQuest(4931, "grenstory", new[] { "Rhison", "Imposter Egg", "Chinchilizard", "Chinchilizard" });

        // Staff of Heroes (4932)
        Story.KillQuest(4932, "grenstory", "Ravine Tigriff");

        // Ritual of Heroes (4933)
        Story.KillQuest(4933, "grenstory", "Jurassic Grenwog");

        // Grelot: Grenwog Gauntlet (4934)
        if (!Story.QuestProgression(4934))
        {
            Core.AddDrop("Grenwog Defeated");
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(4934);
            Core.HuntMonster("grenstory", "Jurassic Grenwog", "Grenwog Defeated", 50, false, false);
            Core.EnsureComplete(4934);
            Core.EquipClass(ClassType.Farm);
        }
    }
}
