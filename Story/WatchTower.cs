//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class WatchTower
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
        // if (Core.isCompletedBefore(2610))
        //     return;

        Story.PreLoad(this);

        //Chaos Investigation 2602
        Story.MapItemQuest(2602, "watchtower", 1605, 6);
        Story.KillQuest(2602, "watchtower", new[] { "Chaorrupted Wolf", "Chaotic Gorillaphant" });

        //Hunt for Answers 2603
        Story.KillQuest(2603, "watchtower", new[] { "Storagebox", "Chaos Spider" });

        //Decryption Hunt 2604
        Story.MapItemQuest(2604, "watchtower", 1606);
        Story.KillQuest(2604, "watchtower", "Chaorrupted Knight");

        //Prisoner Breakout! 2605
        Story.KillQuest(2605, "watchtower", "Chaorrupted Prisoner");

        //Hidden Secrets 2606
        Story.MapItemQuest(2606, "watchtower", new[] { 1607, 1608 });
        Story.KillQuest(2606, "watchtower", "Chaos Gorillaphant");

        //Mage Mystery 2607
        Story.MapItemQuest(2607, "watchtower", 1609, 4);
        Story.KillQuest(2607, "watchtower", "Chaos Sp-Eye");

        //Take Back Remains 2608
        Story.KillQuest(2608, "watchtower", "Chaorrupted Knight");

        //Activate Teleport Rune 2609
        Story.MapItemQuest(2609, "watchtower", 1610);
        Story.KillQuest(2609, "watchtower", "Chaorrupted Good Soldier");

        //Chaos Knight Attacks 2610
        Story.KillQuest(2610, "watchtower", "Chaos Knight");

    }
}
