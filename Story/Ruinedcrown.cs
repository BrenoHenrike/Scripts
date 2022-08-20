//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/UltraTyndariusPrereqs.cs
using Skua.Core.Interfaces;

public class RuinedCrown
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Tyndarius Tyn = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Tyn.DoALl();
        StoryLine();
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8787))
            return;

        Story.PreLoad();

        // 8778 Mental Damage Sponge
        Story.MapItemQuest(8778, "ruinedcrown", new[] { 10380, 10382, 10383 });

        // 8779 Scraping the Barrel
        Story.KillQuest(8779, "ruinedcrown", new[] { "Mana-Burdened Minion", "Mana-Burdened Knight" });

        // 8780 Fractals
        Story.MapItemQuest(8780, "ruinedcrown", 10384, 6);

        // 8781 Blind Retaliation
        Story.KillQuest(8781, "ruinedcrown", "Mana-Burdened Mage");

        // 8782 Deafening Silence
        Story.KillQuest(8782, "ruinedcrown", new[] { "Mana-Burdened Minion", "Mana-Burdened Knight" });

        // 8784 Stilled Mind (Yes 8784 before 8783)
        Story.MapItemQuest(8784, "ruinedcrown", 10385, 6);

        // 8783 Volatile Nature
        Story.KillQuest(8783, "ruinedcrown", "Frenzied Mana");

        // 8785 Heartache
        Story.KillQuest(8785, "ruinedcrown", "Mana-Burdened Mage");

        // 8786 Clouded Vision
        Story.MapItemQuest(8786, "ruinedcrown", 10386);
        Story.KillQuest(8786, "ruinedcrown", "Frenzied Mana");

        // 8787 Guilt Complex
        Story.KillQuest(8787, "ruinedcrown", "Calamitous Warlic");

        // 8788 Em-pathetic Connection (Merge Shop Quest)
        Core.EnsureAccept(8788);
        Core.HuntMonster("ruinedcrown", "Frenzied Mana", "Mana Residue", 8);
        Core.HuntMonster("ruinedcrown", "Mana-Burdened Mage", "Mage’s Blood Sample", 8);
        Core.HuntMonster("ruinedcrown", "Calamitous Warlic", "Warlic’s Favor");
        Core.EnsureComplete(8788);
        // I'm going to assume they will fix the ’ to a normal ' one day so this will need to be fixed then (:
    }
}