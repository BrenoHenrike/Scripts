//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/August/Rangda.cs
using Skua.Core.Interfaces;

public class KalaSeasonal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public RangdaSeasonal Ran = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.isSeasonalMapActive("kala"))
            return;
            
        Ran.StoryLine();

        if (Core.isCompletedBefore(8214))
            return;

        Story.PreLoad(this);

        // 8205 Thirst Quencher
        Story.KillQuest(8205, "kala", "Coconut Treeant");

        // 8206 Gain the Rice Grains
        Story.KillQuest(8206, "kala", "Jelangkung");

        // 8207 Finger Lickin' Good
        Story.KillQuest(8207, "kala", "Cemani Dricken");

        // 8208 Pulverized Puppets
        if (!Story.QuestProgression(8208))
        {
            Core.EnsureAccept(8208);
            Core.HuntMonster("kala", "Jelangkung", "Spare Cloth", 4);
            Core.HuntMonster("kala", "Coconut Treeant", "Sticks", 3);
            Core.HuntMonster("kala", "Coconut Treeant", "Coconut Shell", 3);
            Core.HuntMonster("kala", "Cemani Dricken", "Sacred Feet", 4);
            Core.EnsureComplete(8208);
        }

        // 8209 Keris-tal Clear
        if (!Story.QuestProgression(8209))
        {
            Core.Join("rangda");
            Bot.Quests.UpdateQuest(7622);
            Bot.Sleep(2000);
            Core.EnsureAccept(8209);
            Core.KillMonster("rangda", "r4", "Left", "Rangda", "Sacred Keris");
            Core.EnsureComplete(8209);
        }

        // 8210 Ceremonial Reflection
        Story.KillQuest(8210, "kala", "Jelangkung");

        // 8211 An Honorable Offering
        Story.MapItemQuest(8211, "kala", 8765);

        // 8213 Fangs of Fearlessness
        Story.KillQuest(8213, "rangda", "Leyak");

        // 8214 Kala-mity time
        Story.KillQuest(8214, "kala", "Kala");

        Core.Logger("Kala Story Complete");
    }
}
