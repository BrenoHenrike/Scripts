//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class AAKillUltra
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        while (!Bot.ShouldExit())
            Adv.KillUltra(Bot.Map.Name, Bot.Player.Cell, Bot.Player.Pad, Bot.Monsters.CurrentMonsters.MaxBy(x => x.HP).Name);
    }
}