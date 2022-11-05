//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class EpilTakeOver
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
        if (Core.isCompletedBefore(8953) && !Core.isSeasonalMapActive("EbilTakeOver"))
            return;

        Story.PreLoad(this);

        // Turning Traitor 8937
        Story.KillQuest(8937, "ebiltakeover", "Traitor Goon");

        // Bait and Switch 8938
        Story.MapItemQuest(8938, "ebiltakeover", 10859, 4);

        // Fishman Flop 8939
        Story.KillQuest(8939, "ebiltakeover", "Ebil Fishman");

        // Chief Fish Marketing Officer 8940
        Story.KillQuest(8940, "ebiltakeover", "Ebil Kuro");

        //Slime Time 8941
        Story.KillQuest(8941, "ebiltakeover", "EbilCorp Slime");

        //Keycard Access 8942
        Story.KillQuest(8942, "ebiltakeover", "Traitor Goon");

        //Lay Offs 8943
        Story.KillQuest(8943, "ebiltakeover", "EbilCorp Slime");

        // Human Remains Officer 8945
        Story.KillQuest(8945, "ebiltakeover", "Ebil Jack Sprat");

        //Traitor Traps 8946
        Story.MapItemQuest(8946, "ebiltakeover", 10860, 5);

        //Drones and Drones 8947
        Story.KillQuest(8947, "ebiltakeover", new[] { "Traitor Goon", "Ebil Battle Drone" });

        //Tech Collection 8948
        Story.KillQuest(8948, "ebiltakeover", "Ebil Battle Drone");

        // General General 8949
        Story.KillQuest(8949, "ebiltakeover", "Ebil General Porkon");

        //Variety Is The Spice Of Life 8950
        Story.KillQuest(8950, "ebiltakeover", new[] { "Ebil Fishman", "EbilCorp Slime", "Ebil Battle Drone" });

        //Draconian Destruction 8951
        Story.KillQuest(8951, "ebiltakeover", "Ebil Draconian");

        //Final Notice 8952
        Story.KillQuest(8952, "ebiltakeover", "Traitor Goon");

        // Chief Immolation Officer 8953
        Story.KillQuest(8953, "ebiltakeover", "Ebil Red Dragon");
    }
}
