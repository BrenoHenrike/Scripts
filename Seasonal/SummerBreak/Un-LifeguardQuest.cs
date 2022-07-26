//cs_include Scripts/CoreBots.cs
using RBot;

public class UnLifeGuardQuest
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        string[] nonMemrewards = { "LifeGuard", "LifeGuard Cap + Locks", "LifeGuard Cap", "LifeGuard Tube" };
        string[] memRewards = { "LifeGuard", "LifeGuard Cap + Locks", "LifeGuard Cap", "LifeGuard Tube", "LifeGuard Tube of DOOM" };
        string[] rewards = Core.IsMember ? memRewards : nonMemrewards;
        if (Core.CheckInventory(rewards, toInv: false))
            return;

        Core.AddDrop(rewards);
        Core.RegisterQuests(7579);
        while (!Bot.ShouldExit() && Core.CheckInventory(rewards))
        {
            Core.HuntMonster("summerbreak", "Cyborg Shark", "Cyborg Shark Tooth", 7, false);
            Bot.Sleep(Core.ActionDelay);
        }
        Core.CancelRegisteredQuests();
    }
}
