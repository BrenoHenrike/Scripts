//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class VoucherItemTotemofNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Totem();

        Core.SetOptions(false);
    }

    public void Totem()
    {
        while (!Bot.ShouldExit)
        {
            Nation.VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);
            // Comment the line above and uncomment the line bellow to farm Gem of Nulgath
            //Nation.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
        }
    }
}