//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class VoucherItemTotemofNulgath
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Totem();

        Core.SetOptions(false);
    }

    public void Totem()
    {
        while (!Bot.ShouldExit())
        {
            Nation.VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);
            // Comment the line above and uncomment the line bellow to farm Gem of Nulgath
            //Nation.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
        }
    }
}