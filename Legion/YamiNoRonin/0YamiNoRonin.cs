//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/YamiNoRonin/YokaiSwordScroll.cs
//cs_include Scripts/Legion/YamiNoRonin/BlademasterSwordScroll.cs
//cs_include Scripts/Story/Legion/DarkAlly.cs
//cs_include Scripts/Legion/SwordMaster.cs
using RBot;

public class YamiNoRonin
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();
    public TheEdgeofanEra TEoaE = new TheEdgeofanEra();
    public ThePathtoPower TPtP = new ThePathtoPower();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        YaminoRoninClass();

        Core.SetOptions(false);
    }

    public void YaminoRoninClass(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Yami no Ronin"))
            return;

        Core.AddDrop("Yami no Ronin");

        Core.EnsureAccept(7408);
        TPtP.BlademasterSwordScroll(1);
        TEoaE.YokaiSwordScroll(1);
        Core.EnsureComplete(7408);
        Bot.Wait.ForPickup("Yami no Ronin");

        if (rankUpClass)
            Adv.rankUpClass("Yami no Ronin");
    }

}