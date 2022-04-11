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
        if (Core.isCompletedBefore(5120))
            return;

        Core.EquipClass(ClassType.Farm);

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
        if (!Story.QuestProgression(5098))
        {
            Core.EnsureAccept(5098);
            Core.KillMonster("baconcat", "r11a", "Left", "Oopy", "Oopy Defeated");
            Core.KillMonster("baconcat", "r11a", "Left", "Bloopy", "Bloopy Defeated");
            Core.KillMonster("baconcat", "r11a", "Left", "Hoopy", "Hoopy Defeated");
            Core.KillMonster("baconcat", "r11a", "Left", "Frood", "Frood Defeated");
            Core.EnsureComplete(5098);
        }

        // Super Ziri Brothers
        if (!Story.QuestProgression(5099))
        {
            Core.EnsureAccept(5099);
            Core.KillMonster("baconcat", "r12", "Left", "Red Shell Turtle", "Turtle Shells", 5);
            Core.KillMonster("baconcat", "r12", "Left", "Snapper Shrub", "Power Flower", 3);
            Core.EnsureComplete(5099);
        }

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

        // Cloud Sharks!
        Story.KillQuest(5111, "baconcatlair", "Cloud Shark");

        // Get Those Waffle Cones Ready
        Story.KillQuest(5112, "baconcatlair", "Ice Cream Shark");

        //Grody
        Story.KillQuest(5113, "baconcatlair", "Ice Cream Shark");
        Story.MapItemQuest(5113, "baconcatlair", 4474, 6);

        //We're Gonna Need A Bigger Eraser
        if (!Story.QuestProgression(5114))
        {
            Core.EnsureAccept(5114);
            Core.BuyItem("librarium", 651, 35184, shopItemID: 20849);
            Core.EnsureComplete(5114);
        }

        // Second Draft
        Story.KillQuest(5115, "baconcatlair", "Sketchy Shark");
        Story.MapItemQuest(5115, "baconcatlair", 4475, 4);

        // Game on!
        if (!Story.QuestProgression(5116))
        {
            Core.EnsureAccept(5116);
            Core.KillMonster("baconcat", "r12", "Left", "*", "Cheat Codes", 4);
            Core.EnsureComplete(5116);
        }

        // Game Sharks
        Story.KillQuest(5117, "baconcatlair", "8-bit Shark");
        Story.MapItemQuest(5117, "baconcatlair", 4476, 4);

        // Save the Kittarians
        if (!Story.QuestProgression(5118))
        {
            Core.EnsureAccept(5118);
            Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Kittarian Clothes", 6);
            Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Kittarian Spoon", 4);
            Core.HuntMonster("baconcatlair", "Cat Clothed Shark", "Kittarian Fork", 4);
            Core.EnsureComplete(5118);
        }

        // Bacon Cat Force Needs YOU!
        Story.KillQuest(5119, "baconcatlair", new[] { "Cloud Shark", "Ice Cream Shark", "Sketchy Shark", "8-bit Shark", "Cat Clothed Shark" });

        // Ziri Is Also Tough
        Story.KillQuest(5120, "baconcatlair", "Cloud Shark");
    }
}
