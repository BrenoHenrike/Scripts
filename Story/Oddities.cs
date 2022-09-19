//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Oddities
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
        if (Core.isCompletedBefore(8667) || !Core.IsMember)
            return;

        Story.PreLoad(this);

        // Shop of Vandalized Horrors 8654
        if (!Story.QuestProgression(8654))
        {
            Core.EnsureAccept(8654);
            Core.KillMonster("Oddities", "Enter", "Spawn", "*", "Cursed Artifact Slain", 13);
            Core.EnsureComplete(8654);
        }

        // Browsing for a Way Out 8655
        Story.MapItemQuest(8655, "Oddities", 10149);

        // Key for Free 8656
        Story.MapItemQuest(8656, "Oddities", 10150, 4);
        Story.MapItemQuest(8656, "Oddities", 10151);
        Story.KillQuest(8656, "Oddities", "Cursed Doll-Head");

        // Rampaging Desk Jockey 8657
        Story.KillQuest(8657, "Oddities", "Writing Desk");

        // Steeped in Porcelain 8658
        Story.MapItemQuest(8658, "Oddities", 10152);
        Story.MapItemQuest(8658, "Oddities", 10153, 2);
        Story.MapItemQuest(8658, "Oddities", 10154);
        Story.KillQuest(8658, "Oddities", new[] { "Cursed Curio", "Gothic Chest", "Oddity Swarm" });

        // Stuffy Guests  8659
        Story.KillQuest(8659, "Oddities", "Creepy Baby|Dready Bear");

        // Snotty Crumbs 8660
        Story.KillQuest(8660, "Oddities", new[] { "Oddity Swarm", "Cursed Doll-Head" });

        // Wobbly Rune Writing 8661
        Story.MapItemQuest(8661, "Oddities", 10155, 6);

        // Broken Sights 8662
        Story.KillQuest(8662, "Oddities", "Opera Glasses");

        // Teddy Tailor 8663
        if (!Story.QuestProgression(8663))
        {
            Core.EnsureAccept(8663);
            Core.HuntMonster("Oddities", "Cursed Curio", "Bow Tie");
            Core.HuntMonster("Oddities", "Creepy Baby", "Button Eye", 2);
            Core.HuntMonster("Oddities", "Dready Bear", "Party Hat");
            Core.HuntMonster("Oddities", "Dready Bear", "Fur Scrap", 6);
            Core.EnsureComplete(8663);
        }
        // Immaterial Dowsing 8664
        Story.KillQuest(8664, "Oddities", "Dready Bear");

        // Be Our Bait Guest 8665
        Story.MapItemQuest(8665, "Oddities", 10156);

        Core.EquipClass(ClassType.Solo);
        // Pint-Sized Poltergeist 8666
        Story.KillQuest(8666, "Oddities", "Cursed Spirit");

        // Deep Cleanse 8667
        Story.KillQuest(8667, "Oddities", new[] { "Cursed Curio", "Creepy Baby", "Cursed Spirit" }, GetReward: false);
    }
}
