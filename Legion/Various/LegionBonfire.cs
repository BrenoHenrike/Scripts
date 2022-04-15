//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class LegionBonfire
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetLegionBonfire();

        Core.SetOptions(false);
    }

    public void GetLegionBonfire()
    {
        if (Bot.Inventory.ContainsHouseItem("Legion Bonfire"))
            return;

        Legion.FarmLegionToken(10000);
        Farm.Gold(5000000);

        Core.BuyItem("underworld", 1985, "Legion Bonfire");
    }
}