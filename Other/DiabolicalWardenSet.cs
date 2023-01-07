//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class DiabolicalWarden
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        string[] Drops = {
            "Diabolical Warden",
            "Diabolical Warden's Hair",
            "Diabolical Warden's Twintails",
            "Diabolical Warden's Visage",
            "Diabolical Warden's Visage + Locks",
            "Diabolical Zealot's Locks",
            "Diabolical Zealot's Ponytail"
        };

        if (Core.CheckInventory(Drops))
        {
            Core.Logger("You already have all of the items.");
            return;
        }

        Core.EquipClass(ClassType.Solo);

        Core.AddDrop(Drops);

        Bot.Quests.UpdateQuest(9044);

        while (!Bot.ShouldExit && !Core.CheckInventory(Drops))
            Core.HuntMonster("brokenwoods", "Eldritch Amalgamation", "*", isTemp: false);
    }
}