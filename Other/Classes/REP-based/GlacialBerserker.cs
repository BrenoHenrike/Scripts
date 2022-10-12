//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Glacera.cs

using Skua.Core.Interfaces;

public class GlacialBerserker
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public GlaceraStory Glacera = new GlaceraStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGB();

        Core.SetOptions(false);
    }

    public void GetGB(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Glacial Berserker"))
            return;

        Glacera.IceWindPass();
        Adv.BestGear(GearBoost.rep);
        Farm.GlaceraREP();

        Core.BuyItem("icewindpass", 1339, 38084);

        if (rankUpClass)
            Adv.rankUpClass("Glacial Berserker");
    }
}