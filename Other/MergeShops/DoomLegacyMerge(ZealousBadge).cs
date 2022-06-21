//cs_include Scripts/CoreBots.cs
using RBot;

public class ZealousBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(false);
    }

    public void DoQuest()
    {
        Core.AddDrop("Zealous Badge");

        while (!Bot.ShouldExit() && !Core.CheckInventory("Zealous Badge", 300))
        {
            Core.EnsureAccept(7616);
            Core.HuntMonster("techdungeon", "Kalron the Cryptborg", "Immutable Dedication", 7);
            Core.HuntMonster("techdungeon", "DoomBorg Guard", "Paladin Armor Scraps", 30);
            Core.EnsureComplete(7616);
        }
    }
}