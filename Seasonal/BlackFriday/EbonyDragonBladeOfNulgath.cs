//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem}.cs
//cs_include Scripts/Nation/Materials/DiamondofNulgath.cs
using Skua.Core.Interfaces;

public class EbonyDragonBladeofNulgath
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    public DragonBladeofNulgath DBoN = new();
    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();
        EbonyDbon();
        Core.SetOptions(false);
    }

    public void EbonyDbon()
    {
        if (Core.CheckInventory("Ebony DragonBlade of Nulgath"))
            return;

        if(!Core.IsMember && !Core.CheckInventory("DragonBlade of Nulgath"))
        {
            Core.Logger("You must be a member to farm DragonBlade of Nulgath! (Required to merge Ebony DBoN)");
            return;
        }
        
        Core.BankingBlackList.AddRange(new[] {"DragonBlade of Nulgath", "Diamond of Nulgath", "Ebony DragonBlade of Nulgath"});
        if(!Core.CheckInventory("Diamond of Nulgath", 100))
            Nation.FarmDiamondofNulgath(100);
        if(!Core.CheckInventory("DragonBlade of Nulgath"))
            DBoN.GetDragonBlade();
        Core.BuyItem("evilwarnul", 456, "Ebony DragonBlade of Nulgath");
    }
}