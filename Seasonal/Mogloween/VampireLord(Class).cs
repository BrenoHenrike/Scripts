//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Other/BloodMoonToken.cs
using Skua.Core.Interfaces;

public class VampireLord
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();
    public BloodMoonToken BMToken = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetClass(true);

        Core.SetOptions(false);
    }

    public void GetClass(bool rankUpClass = false)
    {
        if (Core.CheckInventory(41575,toInv: false))
            return;

        BMToken.BMToken(300);
        Core.BuyItem("mogloween", 1477, "Vampire Lord", shopItemID: 5459);

        if (rankUpClass)
            Adv.rankUpClass("Vampire Lord");
    }
}
