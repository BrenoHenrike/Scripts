//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class BurningBeach
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(7577))
            return;

        Story.PreLoad();

        //7573 | Suited to Shield
        Story.KillQuest(7573, "burningbeach", new[] { "Shark", "Water Goblin" });

        //7574 | Through Fumes and Flame
        Story.KillQuest(7574, "burningbeach", "Lavazard");

        //7575 | Fiamme's Fang
        Story.KillQuest(7575, "lavarun", "Phedra");

        //7576 | In Lava with Maladrite
        Story.KillQuest(7576, "lavarun", "Lava Guardian");

        //7577 | Fahrenheit FEAR - 51
        Story.KillQuest(7577, "lavarun", "Maladrite");
    }
}