/*
name: Terra de Festa
description: Completes Frevo's Quests in terradefesta.
tags: freva, terra de festa, seasonal, carnaval, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Carnaval/Batista.cs
using Skua.Core.Interfaces;

public class TerraDeFesta
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private Batista Bat = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(3392) || !Core.isSeasonalMapActive("terradefesta"))
            return;

        Bat.Storyline();

        Story.PreLoad(this);

        //Frevo's Dancers Need Feathers! (3398)
        Story.KillQuest(3398, "carnaval", "Cuco");

        //Dancers Need Feathers (3382)
        Story.KillQuest(3382, "carnaval", "Cuco");

        //And Dolls Need Heads (3383)
        Story.KillQuest(3383, "terradefesta", "GreenRat");

        //A Head Needs A Body (3384)
        Story.KillQuest(3384, "terradefesta", new[] { "Crow", "Crow" });

        //Bind It All Together (3385)
        Story.KillQuest(3385, "carnaval", "Mulher de Branco");

        //Take 'Em Apart (3386)
        Story.KillQuest(3386, "terradefesta", new[] { "Voodoo Doll", "Voodoo Doll", "Voodoo Doll", "Voodoo Doll" });

        //Who Could It Be? (3387)
        Story.KillQuest(3387, "terradefesta", new[] { "Party Zombie", "Party Zombie", "Party Zombie", "Party Zombie" });

        //A Bag For A Talisman (3388)
        Story.KillQuest(3388, "terradefesta", "GreenRat");

        //Their... third eyes? (3389)
        Story.KillQuest(3389, "terradefesta", "Crow");

        //Hexy Ladies (3390)
        Story.KillQuest(3390, "terradefesta", "Dancing Zombie");

        //Tracking Down A Baron (3391)
        Story.KillQuest(3391, "terradefesta", new[] { "GreenRat", "Crow" });

        //Defeat Baron Sunday (3392)
        Story.KillQuest(3392, "terradefesta", "Baron Sunday");
    }
}
