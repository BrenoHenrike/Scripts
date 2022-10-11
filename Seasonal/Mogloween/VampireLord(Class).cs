//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class VampireLord
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetClass(true);

        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = false)
    {
        if (!Core.isSeasonalMapActive("mogloween"))
            return;
        if (Core.CheckInventory(41575, toInv: false))
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

        Core.BuyItem("mogloween", 1477, "Vampire Lord", shopItemID: 5459);

        if (rankUpClass)
            Adv.rankUpClass("Vampire Lord");
    }
}
