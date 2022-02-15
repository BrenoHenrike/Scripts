//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/LordofChaos/Core13LoC.cs
using RBot;

public class DarkbloodStormKing
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetDSK();

        Core.SetOptions(false);
    }

    public void GetDSK(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Darkblood StormKing"))
            return;

        LOC.Lionfang();
        Farm.ThunderForgeREP();

        Core.BuyItem("stormtemple", 544, "Darkblood StormKing", 8079);

        if (rankUpClass)
            Farm.rankUpClass("Darkblood StormKing");
    }
}