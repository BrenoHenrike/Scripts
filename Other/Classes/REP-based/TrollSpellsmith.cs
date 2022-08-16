//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class TrollSpellsmith
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetTS();

        Core.SetOptions(false);
    }

    public void GetTS(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Troll Spellsmith"))
            return;

        Adv.BuyItem("bloodtusk", 306, "Troll Spellsmith");

        if (rankUpClass)
            Adv.rankUpClass("Troll Spellsmith");
    }
}