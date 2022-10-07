//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs 
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
using Skua.Core.Interfaces;

public class LegionDBoN
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreLegion Legion = new CoreLegion();
    public CoreNation Nation = new();
    public DragonBladeofNulgath DBoN = new DragonBladeofNulgath();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(DBoN.TwistedItems);
        Core.BankingBlackList.AddRange(new[] { "DragonBlade of Nulgath", "Combat Trophy", "Basic War Sword", "Behemoth Blade of Shadow", "Behemoth Blade of Light" });

        Core.SetOptions();

        GetLegionDBoN();

        Core.SetOptions(false);
    }
    public void GetLegionDBoN()
    {
        if (Core.CheckInventory("Legion DragonBlade of Nulgath"))
            return;

        Legion.FarmLegionToken(5000);
        DBoN.GetDragonBlade();
        Bot.Wait.ForPickup("Legion DragonBlade of Nulgath");

        //Merge: 
        Core.BuyItem("underworld", 238, "Legion DragonBlade of Nulgath");
    }
}
