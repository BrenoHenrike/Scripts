/*
name: Batista's Quests
description: This script does Batista's Quests in carnaval.
tags: batista, carnaval, seasonal, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Batista
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
        if (Core.isCompletedBefore(2697) || !Core.isSeasonalMapActive("carnaval"))
            return;

        Story.PreLoad(this);

        //The Land of Monsters (2692)
        Story.MapItemQuest(2692, "carnaval", 1674);
        Story.KillQuest(2692, "carnaval", "Mapinguari");

        //Battle and the Beasts (2693)
        Story.KillQuest(2693, "carnaval", new[] { "Mula Sem Cabeça", "Mapinguari", "Cuco", "Boiuna", "Mulher de Branco" });

        //Festooned in Feathers (2694)
        Story.KillQuest(2694, "carnaval", "Boiuna");

        //Mask your Emotions (2695)
        Story.KillQuest(2695, "carnaval", "Boiuna");

        //Crafting the Carranca (2696)
        Story.MapItemQuest(2696, "carnaval", 1677, 5);
        Story.KillQuest(2696, "carnaval", new[] { "Boiuna", "Cuco", "Mulher de Branco", "Mula Sem Cabeça" });

        //Party Like a BOSS!
        Story.KillQuest(2697, "carnaval", "Lobisomem");
    }
}
