//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BirdsWithHarmsStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        Chirpa();
        DuckNorris();
        Goose();
        Hoo();
        Gandalf();
    }

    public void Chirpa()
    {
        if (Core.isCompletedBefore(8974) || !Core.isSeasonalMapActive("birdswithharms"))
            return;

        // 8972 Angriest Birds
        Story.MapItemQuest(8972, "birdswithharms", new[] { 10913, 10924 });

        // 8973 Harms in Arms
        // Story.KillQuest(8973, "birdswithharms", new[] { "Birdbarian", "Fencing Finch", "Unsettling Sparrow", "Robber Ducky" });
        if (!Story.QuestProgression(8973))
        {
            Core.EnsureAccept(8973);
            Core.HuntMonster("birdswithharms", "Birdbarian", "Birdbarian Weapon", 1, log: false);
            Core.HuntMonster("birdswithharms", "Fencing Finch", "Finch Weapon", 1, log: false);
            Core.HuntMonster("birdswithharms", "Unsettling Sparrow", "Sparrow Weapon", 1, log: false);
            Core.HuntMonster("birdswithharms", "Robber Ducky", "Ducky Weapon", 1, log: false);
            Core.EnsureComplete(8973);
        }

        // 8974 Literal Arms Dealer
        Story.MapItemQuest(8974, "birdswithharms", 10914);
    }
    public void DuckNorris()
    {
        if (Core.isCompletedBefore(8977) || !Core.isSeasonalMapActive("birdswithharms"))
            return;

        // 8975 What the Duck?
        Story.MapItemQuest(8975, "birdswithharms", 10915);

        // 8976 Like Feather, Like Son
        // Story.KillQuest(8976, "birdswithharms", new[] { "Birdbarian", "Fencing Finch", "Unsettling Sparrow", "Robber Ducky", "Swole Swan" });
        if (!Story.QuestProgression(8976))
        {
            Core.EnsureAccept(8976);
            Core.HuntMonster("birdswithharms", "Birdbarian", "Birdbarian Feather", 1, log: false);
            Core.HuntMonster("birdswithharms", "Fencing Finch", "Finch Feather", 1, log: false);
            Core.HuntMonster("birdswithharms", "Unsettling Sparrow", "Sparrow Feather", 1, log: false);
            Core.HuntMonster("birdswithharms", "Robber Ducky", "Ducky Feather", 1, log: false);
            Core.HuntMonster("birdswithharms", "Swole Swan", "Swan Feather", 1, log: false);
            Core.EnsureComplete(8976);
        }


        // 8977 Literal Arms Dealer
        Story.MapItemQuest(8977, "birdswithharms", 10916);
    }
    public void Goose()
    {
        if (Core.isCompletedBefore(8979) || !Core.isSeasonalMapActive("birdswithharms"))
            return;

        // 8978 Sketchy Dead Parrot
        Story.MapItemQuest(8978, "birdswithharms", 10922);

        // 8979 Hoo Are You?
        Story.MapItemQuest(8979, "birdswithharms", 10917);
    }
    public void Hoo()
    {
        if (Core.isCompletedBefore(8984) || !Core.isSeasonalMapActive("birdswithharms"))
            return;

        // 8980 Bully Birdies
        Story.KillQuest(8980, "birdswithharms", "Bully Owl");
        Story.MapItemQuest(8980, "birdswithharms", 10918, 6);

        // 8981 Hoo Are You?
        Story.KillQuest(8981, "birdswithharms", "Knight Owl");
        Story.MapItemQuest(8981, "birdswithharms", 10919, 2);

        // 8982 Hoodini's Magic Act
        Story.KillQuest(8982, "birdswithharms", "Hoodini");

        // 8983 Owl Give You the Answer
        Core.BuyItem("birdswithharms", 2183, "Stunned?! Parrot");
        Story.MapItemQuest(8983, "birdswithharms", 10923);

        // 8984 Down Feather Up to No Good
        Story.MapItemQuest(8984, "birdswithharms", 10920);
    }
    public void Gandalf()
    {
       

        // 8985 United We Stand, Divided We Fowl
        Story.KillQuest(8985, "birdswithharms", "Turkonian");

        // 8986 Race against the Cluck
        Story.KillQuest(8986, "birdswithharms", "Schwarzenegger");

        // 8987 Without Feather Ado
        Story.KillQuest(8987, "birdswithharms", "Turking");
    }

}
