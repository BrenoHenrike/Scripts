//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Lust
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        LustSaga();

        Core.SetOptions(false);
    }

    public void LustSaga()
    {
        if (Core.isCompletedBefore(5976))
            return;

        // Hopelessly Devoted
        Story.KillQuest(5961, "lust", "Devoted Admirer");
        // Fighting the Feeling
        Story.MapItemQuest(5962, "lust", 5405, 1);
        Story.MapItemQuest(5962, "lust", 5406, 1);
        Story.MapItemQuest(5962, "lust", 5407, 1);
        Story.MapItemQuest(5962, "lust", 5408, 1);
        Story.MapItemQuest(5962, "lust", 5409, 1);
        // Love Potion #9.1
        Story.KillQuest(5963, "lust", "Golden Vase");
        // Leather is Better
        Story.KillQuest(5964, "lust", "Enamored Guard");
        // Gird your Loins
        Story.MapItemQuest(5965, "lust", 5410, 1);
        Story.MapItemQuest(5965, "lust", 5411, 1);
        Story.KillQuest(5965, "lust", "Devoted Admirer");
        // Get the Keys
        Story.KillQuest(5966, "lust", "Enamored Guard");
        // Open the Cages
        Story.MapItemQuest(5967, "lust", 5412, 5);
        // Get Past the Guards
        Story.KillQuest(5968, "lust", "Elite Guard");
        Story.MapItemQuest(5968, "lust", 5413, 1);
        // Viscyra’lly Yours
        Story.KillQuest(5969, "lust", "Viscyra");
        Story.MapItemQuest(5969, "lust", 5414, 1);
        // Lascivia’sness
        Story.KillQuest(5970, "lust", "Lascivia");
        // Talk to the Guards
        Story.KillQuest(5971, "lust", "Elite Guard");
        // Lascivia Stunk Pretty
        Story.KillQuest(5972, "lust", new[] {"Golden Vase", "Golden Vase"});
        // You Broke It, You Fix It
        Story.KillQuest(5973, "lust", "Devoted Admirer");
        // Elite Energy
        Story.MapItemQuest(5974, "lust", 5415, 8);
        Story.KillQuest(5974, "lust", "Elite Guard");
        // No Pillow Unturned
        Story.MapItemQuest(5975, "lust", 5416, 1);
        Story.MapItemQuest(5975, "lust", 5417, 1);
        // Take Down Killek
        Story.KillQuest(5976, "lust", "Killek Deadchewer");
     
    }
}
