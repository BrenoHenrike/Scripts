//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Monsters;

public class AAKillUltra
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Monster? Target = Bot.Monsters.CurrentMonsters.MaxBy(x => x.MaxHP);
        if (Target == null)
        {
            Core.Logger("No monsters found", messageBox: true);
            return;
        }
        while (!Bot.ShouldExit())
            Adv.KillUltra(Bot.Map.Name, Bot.Player.Cell, Bot.Player.Pad, Target.Name, log: false, forAuto: true);
    }
}