//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DragonPirateStory.cs
using Skua.Core.Interfaces;

public class DragonCapitalStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public DragonPirateStory DPS = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DragonCapital();

        Core.SetOptions(false);
    }

    public void DragonCapital()
    {
        if (!Core.isSeasonalMapActive("dragoncapital"))
            return;
            
        if (Core.isCompletedBefore(8287))
            return;

        DPS.DragonPirate();

        Story.PreLoad(this);

        // 8279 Dracon-you Not
        Story.KillQuest(8279, "dragoncapital", "Pirate Draconian");

        // 8280 Fighting Pirates is a Drag(on)
        Story.KillQuest(8280, "dragoncapital", "Dragon Pirate");

        // 8281 Prepping a Trap
        if (!Story.QuestProgression(8281))
        {
            Core.EnsureAccept(8281);
            Core.HuntMonster("dragoncapital", "Dragon Pirate", "Spade", 1);
            Core.HuntMonster("dragoncapital", "Dragon Pirate", "Rope", 4);
            Core.HuntMonster("dragoncapital", "Dragon Pirate", "Smoke Bombs", 4);
            Core.EnsureComplete(8281);
        }

        // 8282 Planting a Surprise
        Story.MapItemQuest(8282, "dragoncapital", 8995, 4);

        // 8283 Water Droppings
        Story.KillQuest(8283, "dragoncapital", "Water Elemental");

        // 8284 Titan Suckers
        Story.KillQuest(8284, "dragoncapital", "Titan Leech");

        // 8285 T' The Throne Room
        Story.KillQuest(8285, "dragoncapital", "Titan Leech");

        // 8286 Get Past Scalebeard
        Story.KillQuest(8286, "dragoncapital", "Empowered Scalebeard");

        // 8287 The Water Titan
        Story.KillQuest(8287, "dragoncapital", "Leviathanius");
    }
}