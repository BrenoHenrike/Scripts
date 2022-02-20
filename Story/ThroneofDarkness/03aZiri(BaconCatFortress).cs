//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class FlyingBaconCatFortress
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FlyingBaconCatFortressSaga();

        Core.SetOptions(false);
    }

    public void FlyingBaconCatFortressSaga()
    {
        if (Story.isCompletedBefore(5108))
            return;

        // How Rude!
        Story.MapItemQuest(5087, "baconcat", 4466, 7);
        // Bar Fight!
        Story.KillQuest(5088, "baconcat", "Yulgar Regular");
        // Number Two
        Story.KillQuest(5089, "baconcat", "Yulgar Regular");
        Story.MapItemQuest(5089, "baconcat", 4467, 1);
        // Forget the Mess
        Story.KillQuest(5090, "baconcat", "Slime|Spider");
        // The Chosen One!
        Story.MapItemQuest(5091, "baconcat", 4473, 1);
        // BACON PIZZA!
        Story.KillQuest(5092, "baconcat", new[] { "Baconcatzard", "Pizzacatzard" });
        // No More Clowns!
        Story.KillQuest(5093, "baconcat", new[] { "Creepy Clown", "Creepy Clown", "Creepy Clown" });
        // Life's a Beach
        Story.MapItemQuest(5094, "baconcat", 4468, 9);
        // Not all Sand is Cat Litter
        Story.KillQuest(5095, "baconcat", new[] { "Fart Elemental", "Litter Elemental" });
        // King Strong
        Story.KillQuest(5096, "baconcat", new[] { "Box", "King Strong", "King Strong" });
        // Snack Man!
        Story.MapItemQuest(5097, "baconcat", 4469, 4);
        // Ghost Busting
        Story.KillQuest(5098, "baconcat", new[] { "Oopy", "Bloopy", "Hoopy", "Frood" });
        // Super Ziri Brothers
        Story.KillQuest(5099, "baconcat", new[] { "Red Shell Turtle", "Snapper Shrub" });
        // It's-A-Me
        Story.KillQuest(5100, "baconcat", "Horcio");
        // Me So Corny!
        Story.KillQuest(5109, "baconcat", "Corn Minion");
        // Me Nom You Long Time
        Story.KillQuest(5110, "baconcat", "Non-GMO Brutalcorn");
        // Make a Wish
        Story.MapItemQuest(5101, "baconcat", 4470, 1);
        // Smell This!
        Story.KillQuest(5102, "baconcat", "Scent Trail");
        // Trial of being SMALL
        Story.KillQuest(5103, "baconcat", new[] { "Buttermancer", "Potato Knight" });
        // King of the Unbread
        Story.KillQuest(5104, "baconcat", "King of the Unbread");
        // Evil Undead!
        Story.KillQuest(5105, "baconcat", "Chainsaw Actor");
        // Pala-dinner
        Story.KillQuest(5106, "baconcat", "Paladin Actor");
        // Kitty Boo Boo, Overlord of the Catverse
        Story.KillQuest(5107, "baconcat", "Kitty Boo Boo");
        // Stop Hitting Yourself!
        Story.KillQuest(5108, "baconcatyou", "*");
        Core.Join("farm");
    }
}
