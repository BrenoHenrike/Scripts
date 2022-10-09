//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class BloodMoonToken
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.Add("Blood Moon Token");
        BMToken();

        Core.SetOptions(false);
    }

    public void BMToken()
    {
        if (Core.CheckInventory("Blood Moon Token", 300))
            return;
        Core.FarmingLogger("Blood Moon Token", 300);
        Core.AddDrop("Blood Moon Token");
        // Core.RegisterQuests(Core.IsMember ? 6060 : 6059); // uncomment when registerquest is fixed. if more then 1 item is found in inv it only complets once then afks/
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Moon Token", 300))
        {
            Core.EnsureAccept(Core.IsMember ? 6060 : 6059);
            Core.KillMonster("bloodmoon", "r12a", "Left", "Black Unicorn", "Black Blood Vial", isTemp: false);
            Core.KillMonster("bloodmoon", "r4a", "Left", "Lycan Guard", "Moon Stone", isTemp: false);
            Core.EnsureComplete(Core.IsMember ? 6060 : 6059);
            Bot.Wait.ForPickup("Blood Moon Token");
        }
    }
}