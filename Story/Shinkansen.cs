//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Shinkansen
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8124))
            return;

        Story.PreLoad(this);

        // Snack Run 8116
        Story.MapItemQuest(8116, "Shinkansen", 8496, 3);
        Story.KillQuest(8116, "Shinkansen", "Temptation 1");

        // Clean Up Crew 8117
        Story.KillQuest(8117, "Shinkansen", "Trash Pile");

        // Asking "Nicely" 8118
        Story.KillQuest(8118, "Shinkansen", "Civilian");

        // Get a Ticket 8119 & Buy Tickets 8120
        if (!Story.QuestProgression(8119))
        {
            if (!Core.CheckInventory("Shinkansen Tickets", 2))
            {
                Core.AddDrop("Shinkansen Tickets");

                Core.RegisterQuests(8118);
                while (!Core.CheckInventory("Credits", 8))
                    Core.HuntMonster("Shinkansen", "Civilian", "Credits \"Donated\"", 4);
                Core.CancelRegisteredQuests();

                Core.ChainComplete(8120);
                Bot.Wait.ForPickup("Shinkansen Tickets");
            }
            Core.ChainComplete(8119);
        }

        // Defeat the Soldiers  8121
        Story.KillQuest(8121, "Shinkansen", "Crystallis Soldier");

        // Shinkansen Key 8122
        Story.KillQuest(8122, "Shinkansen", "Crystallis Soldier");

        // The Twin Saints 8123
        Story.KillQuest(8123, "Shinkansen", new[] { "Saint Eta", "Saint Apa" });

        // Sometimes Losing is Winning 8124
        Story.KillQuest(8124, "Shinkansen", "Crystallis Soldier");
    }
}