//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/YamiNoRonin/YokaiSwordScroll.cs
//cs_include Scripts/Legion/YamiNoRonin/BlademasterSwordScroll.cs
//cs_include Scripts/Story/DarkAlly.cs
//cs_include Scripts/Legion/SwordMaster.cs
using RBot;

public class YamiNoRonin
{
    public CoreBots Core => CoreBots.Instance;
    public TheEdgeofanEra TEoaE = new TheEdgeofanEra();
    public ThePathtoPower TPtP = new ThePathtoPower();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        YaminoRoninClass();

        Core.SetOptions(false);
    }

    public void YaminoRoninClass()
    {
        if (Core.CheckInventory("Yami no Ronin"))
            return;
        Core.AddDrop("Yami no Ronin");
        Core.EnsureAccept(7408);
        TPtP.BlademasterSwordScroll(1);
        TEoaE.YokaiSwordScroll(1);
        Core.EnsureComplete(7408);
    }

}