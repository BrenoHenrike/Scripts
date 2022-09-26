//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class SofAPreFarm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        SofAMats();

        Core.SetOptions(false);
    }

    public void SofAMats()
    {
        if (Core.CheckInventory("Sin of the Abyss"))
            return;

        Nation.SwindleBulk(300);
        Nation.FarmDarkCrystalShard(500);
        Nation.FarmDiamondofNulgath(500);
        Nation.FarmTotemofNulgath(50);
        Nation.FarmGemofNulgath(150);
        Nation.FarmBloodGem(50);

        Core.Logger("Materials Farm finished, Get the Insignias yourself.");
    }
}