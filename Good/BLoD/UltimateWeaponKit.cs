//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using RBot;

public class UltimateWeaponKit
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        BLOD.UltimateWK("Bright Aura", 10000);

        Core.SetOptions(false);
    }
}
