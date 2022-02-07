//cs_include Scripts/CoreBots.cs
using RBot;

public class SagaMythsong
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.AcceptandCompleteTries = 5;
        StoryLine();

        Core.SetOptions(false);
    }
    public void StoryLine()
    {
        Core.BuyItem("battleon", 950, "Rockstar");
        if (Core.CheckInventory("Rockstar", toInv: false))
        {
            Bot.Sleep(700);
            Core.ToBank("Rockstar");
            Core.Logger("Chapter: \"Chaos Lord Kimberly\" already complete. Skipping");
            return;
        }


        //Stairway to Heaven
        Core.KillQuest(648, "stairway", new[] { "Rock Lobster", "Grateful Undead" });

        //Rolling Stones
        Core.KillQuest(649, "stairway", "Rock Lobster");

        //Light My Fire
        Core.KillQuest(650, "stairway", "Grateful Undead");

        //Knockin' on Haven's Door
        Core.KillQuest(651, "stairway", new[] { "Elwood Bruise", "Jake Bruise" }, FollowupIDOverwrite: 658);

        //Staying Alive
        Core.KillQuest(658, "beehive", "Stinger");

        //Killer Queen
        Core.KillQuest(659, "beehive", "Killer Queen Bee");

        //Satisfaction
        Core.KillQuest(660, "beehive", "Lord Ovthedance", hasFollowup: false);

        //Dance with Great Godfather of Souls
        if (!Core.QuestProgression(661))
        {
            Core.EnsureAccept(661);
            Core.Join("beehive");
            Core.SendPackets("%xt%zm%tryQuestComplete%30004%661%-1%false%wvz%");
        }

        //Bad Moon Rising
        Core.KillQuest(675, "orchestra", "Mozard|Pachelbel's Cannon");

        //Burning Down The House
        Core.KillQuest(676, "orchestra", "Pachelbel's Cannon");

        //Superstition
        Core.KillQuest(677, "orchestra", "Mozard");

        //Soul Man
        Core.KillQuest(678, "orchestra", "Faust", hasFollowup: false);

        //Kimberly
        Core.KillQuest(710, "palooza", "Kimberly", hasFollowup: false);
        
        Core.Relogin();
        Core.BuyItem("battleon", 950, "Rockstar");
        Bot.Sleep(700);
        Core.ToBank("Rockstar");
    }
}
