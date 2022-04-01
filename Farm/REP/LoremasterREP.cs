//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class LoremasterREP
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        UnlockLoreMaster();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.LoremasterREP();

        Core.SetOptions(false);
    }

    private void UnlockLoreMaster()
    {
        if (Core.IsMember && !Bot.Quests.IsUnlocked(3032))
        {
            // Rosetta Stones
            Story.KillQuest(3029, "druids", new[] { "Void Bear", "Void Larva", "Void Ghast" }, false);
            Story.KillQuest(3030, "druids", "Void Larva");
            Story.KillQuest(3031, "druids", "Void Ghast");
        }
    }
}