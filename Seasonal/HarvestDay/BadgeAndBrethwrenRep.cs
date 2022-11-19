//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Seasonal/HarvestDay/CoreHarvestDay.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class BadgeAndRepMethod2
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    CoreHarvestDay HarvestDay = new();
    public string OptionsStorage = "BirdsWithHarms";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("GetBadge", "Get Character Page", "Will get character page badge", false),
        new Option<bool>("RepFarm", "Farm Rep", "Will farm Brethwren to rank 10 Mem = 2 Quest, Non Mem = 1 Quest Ducks in a row", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        if (Bot.Config.Get<bool>("GetBadge"))
        {
            HarvestDay.BirdsWithHarms();
            GetBadge();
        }

        if (Bot.Config.Get<bool>("RepFarm"))
        {
            HarvestDay.BirdsWithHarms();
            RepFarm();
        }

        Core.SetOptions(false);
    }
    public void GetBadge()
    {
        if (!Bot.Quests.IsAvailable(8992) && !Bot.Quests.IsUnlocked(8992) || !Core.isSeasonalMapActive("birdswithharms"))
            {
                Core.Logger($"Quest [8992] \"No Egrets Badge\", has yet to be completed, please run \"Seasonal/HarvestDay/16BirdsWithHarmsStory.cs\"", messageBox: true);
                return;
            }
        
        Story.KillQuest(8992, "birdswithharms", "Turkonian");
    }

    public void RepFarm()
    {
        if (!Bot.Quests.IsUnlocked(8988) || !Core.isSeasonalMapActive("birdswithharms"))
            {
                Core.Logger($"Quest [8988] \"Ducks in a Row\", has yet to be completed, please run \"Seasonal/HarvestDay/16BirdsWithHarmsStory.cs\"", messageBox: true);
                return;
            }

        if (Farm.FactionRank("Brethwren") >= 10)
            return;

        if (Core.IsMember)
            Core.RegisterQuests(8988, 8993);
        else if (!Core.IsMember)
            Core.RegisterQuests(8993);
        while (!Bot.ShouldExit && Farm.FactionRank("Brethwren") < 10)
        {
            if (Core.IsMember)
                Core.HuntMonster("birdswithharms", "Swole Swan", "Barbell", 2, log: false);

            Core.HuntMonster("birdswithharms", "Robber Ducky", "Robber Roasted", 8, log: false);
        }
        Core.CancelRegisteredQuests();
    }

}