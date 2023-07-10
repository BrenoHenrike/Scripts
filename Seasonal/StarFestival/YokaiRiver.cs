/*
name: Yokai River
description: This script will complete the storyline in /yokairiver.
tags: star,festival,yokai,river,story,seasonal
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal\StarFestival\Akiba.cs
using Skua.Core.Interfaces;

public class YokaiRiver
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private Akiba Akiba = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(6448) || !Core.isSeasonalMapActive("yokairiver"))
            return;

        Akiba.Storyline();

        Story.PreLoad(this);

        // Gather the Spirits (6443)
        Story.KillQuest(6443, "yokairiver", "Ghostly Chickencow");
        Story.MapItemQuest(6443, "yokairiver", 5949, 5);

        // Spiders and Dreamsilk (6444)
        Story.KillQuest(6444, "yokairiver", "Spirit Spider");

        // Dye-ing to Reunite (6445)
        Story.KillQuest(6445, "yokairiver", "Fallen Star");

        // Clear Water to Clear Skies (6446)
        Story.KillQuest(6446, "yokairiver", "Funa-yurei");

        // 1,000 Cranes to 1,000 Wishes (6447)
        Story.MapItemQuest(6447, "yokairiver", 5950, 18);

        // Slay Uji No Hashihime (6448)
        Story.KillQuest(6448, "yokairiver", "Uji No Hashihime");
    }
}
