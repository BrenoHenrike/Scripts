//cs_include Scripts/CoreBots.cs
using RBot;

public class SevenCircles
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.KillQuest(7968, "sevencircles", "Limbo Guard");
        Core.KillQuest(7969, "sevencircles", "Luxuria Guard");

        Core.KillQuest(7970, "sevencircles", new[] { "Limbo Guard|Luxuria Guard", "Limbo Guard", "Luxuria Guard" });

        Core.KillQuest(7971, "sevencircles", "Luxuria");

        if (!Bot.Quests.IsUnlocked(7973))
        {
            Core.EnsureAccept(7972);
            Core.GetMapItem(8206, 3, "sevencircles");
            Core.EnsureComplete(7972);
        }

        Core.KillQuest(7973, "sevencircles", "Gluttony Guard");

        Core.KillQuest(7974, "sevencircles", "Gluttony");

        Core.KillQuest(7975, "sevencircles", "Avarice Guard");

        Core.KillQuest(7976, "sevencircles", new[] { "Limbo Guard", "Luxuria Guard", "Gluttony Guard", "Avarice Guard" });

        Core.KillQuest(7977, "sevencircles", "Avarice", hasFollowup: false);

        Core.SetOptions(false);
    }
}