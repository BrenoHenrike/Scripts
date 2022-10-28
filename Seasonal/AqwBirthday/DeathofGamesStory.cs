//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DeathofGames
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
        if (Core.isCompletedBefore(8924) && !Core.isSeasonalMapActive("DeathofGames"))
            return;

        Story.PreLoad(this);

        //Dungeons & DOOMFights 8899
        Story.KillQuest(8899, "deathofgames", new[] { "8-Bit Skelly", "8-Bit Sepulchure" });

        //AQ3Destruction 8900
        Story.KillQuest(8900, "deathofgames", new[] { "3D Flying Eye", "Clawg" , "Trolluk" });

        //OverSoul Under Attack 8901
        Story.KillQuest(8901, "deathofgames", new[] { "Vampire Knight", "Black Dragon" });

        //HeroSmashed 8902
        Story.KillQuest(8902, "deathofgames", new[] { "Rider", "Blaster Master", "Super Death" });

        //EpicDuel vs DoG 8907
        Story.KillQuest(8907, "deathofgames", new[] { "Cyber Hunter", "God of War" });

        //AQWorlds At Risk 8920
        Story.KillQuest(8920, "deathofgames", new[] { "Skeletal Fire Mage", "Drakath" });

        //MechQuest For Glory 8921
        Story.KillQuest(8921, "deathofgames", new[] { "Newbatron Prime", "ShadowScythe Mecha", "SkullCrusher Mecha" });

        //DragonFables &amp; Lore 8922
        Story.KillQuest(8922, "deathofgames", new[] { "Fire Elemental", "Xan", "Titan Fluffy" });

        //AdventureQuest for Victory 8923
        Story.KillQuest(8923, "deathofgames", new[] {  "Moglin Ghost", "Halenro the Paladin", "Mysterious Stranger" });

        //Battle On... FOREVER! 8924
        Story.KillQuest(8924, "deathofgames", "Death of Games");



    }
}
