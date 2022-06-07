//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class DualChainSawKatanas
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetWep();

        Core.SetOptions(false);
    }

    public void GetWep()
    {

        Core.EnsureAccept(8670);
        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("darkoviahorde", "r8", "Right", "Zombie", "Zombie Defeated", 100);
        Core.EnsureComplete(8670);
        Core.JumpWait();
        Bot.Sleep(Core.ActionDelay);
        Core.SetAchievement(10);
        Bot.Sleep(Core.ActionDelay);
        Core.BuyItem("Darkoviahorde", 1171, "Dual Chainsaw Katanas");
    }
}

