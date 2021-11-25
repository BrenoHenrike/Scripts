//cs_include Scripts/CoreBots.cs
using RBot;

public class SevenCircles
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Core.SmartKillMonster(7968, "sevencircles", "Limbo Guard", completeQuest: true);
        
        Core.SmartKillMonster(7969, "sevencircles", "Luxuria Guard", completeQuest: true);
        
        Core.SmartKillMonster(7970, "sevencircles", new[] { "Limbo Guard|Luxuria Guard", "Limbo Guard", "Luxuria Guard" }, completeQuest: true);
        
        Core.SmartKillMonster(7971, "sevencircles", "Luxuria", completeQuest: true);

        Core.EnsureAccept(7972);
        Core.GetMapItem(8206, 3, "sevencircles");
        Core.EnsureComplete(7972);

        Core.SmartKillMonster(7973, "sevencircles", "Gluttony Guard", completeQuest: true);

        Core.SmartKillMonster(7974, "sevencircles", "Gluttony", completeQuest: true);

        Core.SmartKillMonster(7975, "sevencircles", "Avarice Guard", completeQuest: true);

        Core.SmartKillMonster(7976, "sevencircles", new[] { "Limbo Guard", "Luxuria Guard", "Gluttony Guard", "Avarice Guard" }, completeQuest: true);

        Core.SmartKillMonster(7977, "sevencircles", "Avarice", completeQuest: true);

        Core.SetOptions(false);
    }
}