//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class MystcroftStory {
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot) {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline() {
        if (Core.isCompletedBefore(5427))
            return;

        Story.PreLoad(this);

        //Tome of The Otherworld 5418
        Story.KillQuest(5418, "mystcroftforest", "Eerie Apparition");

        //Monster Data 5419
        Story.KillQuest(5419, "mystcroftforest", "Eerie Apparition");

        //The Best Costumes Need the Best Fabric 5420
        Story.KillQuest(5420, "mystcroftforest", "Eerie Apparition");

        //Accessorize! 5421
        Story.MapItemQuest(5421, "mystcroftforest", 4801, 8);
        Story.KillQuest(5421, "mystcroftforest", "Grim Goblin");

        //Make those Munchies 5422
        Story.KillQuest(5422, "mystcroftforest", "Eerie Apparition");
        Story.MapItemQuest(5422, "mystcroftforest", 4802, 8);

        //Every Party Needs Drinks 5423
        Story.KillQuest(5423, "mystcroftforest", "Grim Goblin");

        //Beautiful Creatures and their Uses 5424
        Story.KillQuest(5424, "timevoid", "Void Phoenix");

        //Ooh, Shiny! 5425
        Story.KillQuest(4358, "skytower", new[] { "Moonstone", "Sunstone", "Star Sapphire" });

        //Time to get Dressed Up! 5426
        Story.MapItemQuest(5426, "mystcroftforest", 4800, 1);
        Story.KillQuest(5426, "mystcroftforest", "Grim Goblin");

        //EEEEK 5427
        Story.KillQuest(5427, "mystcroftforest", "Barghest");
    }
}
