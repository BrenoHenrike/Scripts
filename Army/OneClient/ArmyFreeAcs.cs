//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyFreeAcs
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private BankAllItems BAI = new();
    private CoreArmyLite Army = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FreeAcs();

        Core.SetOptions(false);
    }

    public void FreeAcs()
    {
        while (Army.doForAll())
        {
            if (!Core.isCompletedBefore(9057))
            {
                Core.EnsureAccept(9057);
                Core.KillMonster("battleontown", "Enter", "Spawn", "Frogzard", "Free AC Giftbox");
                Core.EnsureComplete(9057);
            }
        }
    }
}