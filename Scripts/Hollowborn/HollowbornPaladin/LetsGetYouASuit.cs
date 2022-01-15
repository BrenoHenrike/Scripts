//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/CoreHollowbornPaladin.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
using RBot;

public class LetsGetYouASuit
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreHollowborn HB = new CoreHollowborn();
    public CoreHollowbornPaladin HBPal = new CoreHollowbornPaladin();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Bot.Player.LoadBank();
        Core.SetOptions();

        HBPal.HBPaladin();

        Core.SetOptions(false);
    }
}
