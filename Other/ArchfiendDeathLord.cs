//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\Nation\Fiendshard.cs
//cs_include Scripts/Nulgath\CoreNulgath.cs
//cs_include Scripts/Nulgath\AFDL\WillpowerExtraction.cs
//cs_include Scripts/Story\Nation\Originul.cs
using RBot;

public class ArchfiendDeathLord
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();
    public Fiendshard_Story fiendshard = new();
    public CoreNulgath Nulgath = new();
    public WillpowerExtraction Willpower = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetArm();

        Core.SetOptions(false);
    }
    public void GetArm()
    {
        if (Core.CheckInventory("Archfiend DeathLord"))
            return;

        fiendshard.Fiendshard_Questline();

        Core.EnsureAccept(7900);
        Nulgath.FarmBloodGem(20);
        Nulgath.FarmUni13(5);
        Nulgath.FarmTotemofNulgath(3);
        Nulgath.FarmVoucher(false);
        Nulgath.FarmDiamondofNulgath(150);
        Nulgath.FarmGemofNulgath(50);
        Willpower.Unidentified34(10);
        Core.EnsureComplete(7900, 54366);
        Bot.Wait.ForPickup("Archfiend DeathLord");
    }
}
