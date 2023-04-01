/*
name: Firebird
description: Completes Fausto's Quests in firebird.
tags: carnaval, firebird, fausto, seasonal, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Carnaval/Batista.cs
//cs_include Scripts/Seasonal/Carnaval/TerraDeFesta.cs
using Skua.Core.Interfaces;

public class Firebird
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private TerraDeFesta TDF = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(5721) || !Core.isSeasonalMapActive("firebird"))
            return;

        TDF.Storyline();

        Story.PreLoad(this);

        //Turn Off the Heat (5711)
        Story.KillQuest(5711, "firebird", "Flaming Dancer");

        //One Man's Trash... (5712)
        Story.KillQuest(5712, "firebird", "Lantern");

        //Some Fiddling and the Right Spells (5713)
        Story.KillQuest(5713, "terradefesta", new[] { "GreenRat", "Crow", "Baron Sunday" });

        //I Need More Time (5714)
        Story.KillQuest(5714, "firebird", new[] { "Fire Elemental", "Phoenix" });

        //Trapped Flames (5715)
        Story.MapItemQuest(5715, "firebird", 5156, 10);

        //Get Inside! (5716)
        Story.KillQuest(5716, "firebird", "Flame Imp");

        //Where There is Smoke... (5717)
        Story.MapItemQuest(5717, "firebird", 5157);

        //Through the Flames (5718)
        Story.KillQuest(5718, "carnaval", new[] { "Mulher de Branco", "Boiuna" });

        //Battling the Inferno (5719)
        Story.KillQuest(5719, "firebird", "Belo Passaro");

        //Ultra Belo Battle (5721)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5721, "firebird", "Ultra Belo");
    }
}
