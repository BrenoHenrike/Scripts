//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;
public class PurifiedClaymoreOfDestiny
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetPCoD();

        Core.SetOptions(false);
    }

    public void GetPCoD()
    {
        if (Core.CheckInventory("Purified Claymore of Destiny"))
            return;

        Core.AddDrop("Purified Claymore of Destiny");

        Farm.Experience(15);
        Farm.GoodREP(8);

        Core.EnsureAccept(548);
        Core.HuntMonster("battleundera", "Undead Berserker", "Warrior Claymore Blade", isTemp: false);
        Core.EnsureComplete(548);
    }
}