//cs_include Scripts/CoreBots.cs
using RBot;
public class FlyingBaconCatFortress
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FlyingBaconCatFortressSaga();

        Core.SetOptions(false);
    }

    public void FlyingBaconCatFortressSaga()
    {
        if (Core.isCompletedBefore(5108))
            return;

        // How Rude!
        Core.MapItemQuest(5087, "baconcat", 4466, 7);
        // Bar Fight!
        Core.KillQuest(5088, "baconcat", "Yulgar Regular");
        // Number Two
        Core.KillQuest(5089, "baconcat", "Yulgar Regular");
        Core.MapItemQuest(5089, "baconcat", 4467, 1);
        // Forget the Mess
        Core.KillQuest(5090, "baconcat", "Slime|Spider");
        // The Chosen One!
        Core.MapItemQuest(5091, "baconcat", 4473, 1);
        // BACON PIZZA!
        Core.KillQuest(5092, "baconcat", new[] { "Baconcatzard", "Pizzacatzard" });
        // No More Clowns!
        Core.KillQuest(5093, "baconcat", new[] { "Creepy Clown", "Creepy Clown", "Creepy Clown" });
        // Life's a Beach
        Core.MapItemQuest(5094, "baconcat", 4468, 9);
        // Not all Sand is Cat Litter
        Core.KillQuest(5095, "baconcat", new[] { "Fart Elemental", "Litter Elemental" });
        // King Strong
        Core.KillQuest(5096, "baconcat", new[] { "Box", "King Strong", "King Strong" });
        // Snack Man!
        Core.MapItemQuest(5097, "baconcat", 4469, 4);
        // Ghost Busting
        Core.KillQuest(5098, "baconcat", new[] { "Oopy", "Bloopy", "Hoopy", "Frood" });
        // Super Ziri Brothers
        Core.KillQuest(5099, "baconcat", new[] { "Red Shell Turtle", "Snapper Shrub" });
        // It's-A-Me
        Core.KillQuest(5100, "baconcat", "Horcio");
        // Me So Corny!
        Core.KillQuest(5109, "baconcat", "Corn Minion");
        // Me Nom You Long Time
        Core.KillQuest(5110, "baconcat", "Non-GMO Brutalcorn");
        // Make a Wish
        Core.MapItemQuest(5101, "baconcat", 4470, 1);
        // Smell This!
        Core.KillQuest(5102, "baconcat", "Scent Trail");
        // Trial of being SMALL
        Core.KillQuest(5103, "baconcat", new[] { "Buttermancer", "Potato Knight" });
        // King of the Unbread
        Core.KillQuest(5104, "baconcat", "King of the Unbread");
        // Evil Undead!
        Core.KillQuest(5105, "baconcat", "Chainsaw Actor");
        // Pala-dinner
        Core.KillQuest(5106, "baconcat", "Paladin Actor");
        // Kitty Boo Boo, Overlord of the Catverse
        Core.KillQuest(5107, "baconcat", "Kitty Boo Boo");
        // Stop Hitting Yourself!
        Core.KillQuest(5108, "baconcatyou", "*");
        Core.Join("farm");
    }
}
