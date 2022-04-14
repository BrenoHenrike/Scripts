//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class Sloth
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SlothSaga();

        Core.SetOptions(false);
    }

    public void SlothSaga()
    {
        if (Core.isCompletedBefore(5960))
            return;

        // Protective Gear Required (Slot -1, will repeat even if completed before)
        Story.MapItemQuest(5944, "sloth", new[] { 5380, 5381 });

        Core.JumpWait();
        Bot.SendPacket($"%xt%zm%equipItem%{Bot.Map.RoomID}%40710%");

        // Are There Any Survivors?
        Story.KillQuest(5945, "sloth", "Plague Zombie");
        Story.MapItemQuest(5945, "sloth", 5382, 1);
        // Gathering Samples
        Story.KillQuest(5946, "sloth", new[] { "Snotgoblin", "Wandering Plague" });
        // Find a ‘Volunteer’
        Story.KillQuest(5947, "sloth", "Plague Zombie");
        // Cure the Volunteer
        Story.MapItemQuest(5948, "sloth", 5387, 1);
        // Herbal Help
        Story.KillQuest(5949, "sloth", "Marsh Thing");
        // Re-heal
        Story.MapItemQuest(5950, "sloth", 5383, 8);
        // Let's Try That Again
        Story.MapItemQuest(5951, "sloth", 5389, 1);
        // One More Time
        if (!Story.QuestProgression(5952))
        {
            Core.EnsureAccept(5952);
            Core.BuyItem("dragonhame", 865, "Airther Vitae");
            if (!Core.CheckInventory(1749))
                Core.BuyItem("arcangrove", 211, "Health Potion", shopItemID: 4711);
            Core.EnsureComplete(5952);
        }
        // Who’s Up for Round 3
        Story.MapItemQuest(5953, "sloth", 5391, 1);
        // Cure the Villagers
        Story.KillQuest(5954, "sloth", "Plague Zombie");
        // Gotta Clean Up
        Story.MapItemQuest(5955, "sloth", 5384, 10);
        // Clear the Castellum
        Story.MapItemQuest(5956, "sloth", 5385, 1);
        Story.KillQuest(5956, "sloth", "SnotGoblin Prime");
        // Get rid of Phlegnn
        Story.KillQuest(5957, "sloth", "Phlegnn");
        // Cured is NOT GOOD
        Story.KillQuest(5958, "sloth", "Cured Phlegnn");
        // Actual Sloth Dragon
        Story.KillQuest(5960, "sloth", "Actual Sloth Dragon");
        // Mutated Plague
        Story.KillQuest(5959, "sloth", "Mutated Plague");
    }
}
