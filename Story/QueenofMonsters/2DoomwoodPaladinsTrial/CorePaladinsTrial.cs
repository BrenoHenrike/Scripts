using RBot;

public class CorePaladinsTrial
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();

    public void CompleteCorePaladinsTrial()
    {
        //Progress Check
        if (Core.isCompletedBefore(5416))
            return;

        //Preload Quests
        Story.PreLoad();

        DoomPally();

    }

    public void DoomPally()
    {

        //A New Recruit
        if (!Story.QuestProgression(5404))
        {
            Core.EnsureAccept(5404);
            if (!Core.CheckInventory("Blessed Coffee of the Lightguard"))
            {
                Core.AddDrop("Blessed Coffee of the Lightguard");
                Core.EnsureAccept(5405);
                Core.HuntMonster("sandsea", "Sand Monkey", "Pally Luwak Beans");
                Core.EnsureComplete(5405);
            }
            Core.EnsureComplete(5404);
        }

        //SLAY some UNDEAD
        Story.KillQuest(5406, "doompally", "Doomwood Ectomancer");

        //SLAY some MORE UNDEAD!!
        Story.KillQuest(5407, "doompally", "Doomwood Soldier");

        //Slay BIGGER!!
        Story.KillQuest(5408, "doompally", "Doomwood Soldier|Doomwood Ectomancer");

        //Babe in the Woods
        Story.MapItemQuest(5409, "doompally", 4758);

        //The Dark Thicket
        Story.KillQuest(5410, "doompally", "Doomwood Bonemuncher|Doomwood Treeant");
        Story.MapItemQuest(5410, "doompally", 4759, 5);

        //Emily
        Story.MapItemQuest(5411, "doompally", 4761);
        Story.MapItemQuest(5411, "doompally", 4760, 5);

        //Sage Advice
        Story.KillQuest(5412, "doompally", "Doomwood Treeant");

        //Abra-Cadaver
        Story.KillQuest(5413, "doompally", "Doomwood Ectomancer");

        //A (Skele)TON of skulls
        Story.KillQuest(5414, "doompally", "Doomwood Bonemuncher|Doomwood Ectomancer|Doomwood Soldier");

        //Summoning the Subjugator
        Story.MapItemQuest(5415, "doompally", 4762);

        //Subjugator Wraithbone
        Story.KillQuest(5416, "doompally", "Skeletal Subjugator");
    }
}