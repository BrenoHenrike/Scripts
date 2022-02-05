//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Legion/CoreLegion.cs
using RBot;
public class InfiniteLegionDC
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetILDC();

        Core.SetOptions(false);
    }

    public void GetILDC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Infinite Legion Dark Caster"))
            return;
        
        Legion.FarmLegionToken(2000);
        Core.BuyItem("underworld", 238, "Infinite Legion Dark Caster");
        //if (rankUpClass)
        //    Farm.rankUpClass("Infinite Legion Dark Caster");
        //Needs auto enhance
    }
}
