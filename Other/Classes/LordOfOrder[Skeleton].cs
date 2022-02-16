//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class LordOfOrder
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetLoO();

        Core.SetOptions(false);
    }

    public void GetLoO(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Lord of Order"))
            return;



        if (rankUpClass)
            Farm.rankUpClass("Lord of Order");
    }
}