//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class VoucherItemTotemofNulgath
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

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
            Nulgath.VoucherItemTotemofNulgath(ChooseReward.TotemofNulgath);
            // Comment the line above and uncomment the line bellow to farm Gem of Nulgath
            //Nulgath.VoucherItemTotemofNulgath(ChooseReward.GemofNulgath);
        }
    }
}