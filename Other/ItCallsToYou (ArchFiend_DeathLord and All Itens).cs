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

public class ItCallsToYou
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
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        CallsToYou();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
    {
        "Archfiend DeathLord",
        "Champion Ender of Nulgath",
        "FiendLord's Claymore",
        "Doomblade of Genocide",
        "Deathlord Blade of Nulgath",
        "Archfiend Annihilator of Death",
        "Corpse Hijacker of Nulgath",
        "Soul Jacker of Nulgath",
        "Archfiendish Spear of Death",
        "Undeathly SoulReaper of Nulgath",
        "Dual FiendLord's Claymores"
    };

    public void CallsToYou()
    {
        Core.AddDrop(Nulgath.bagDrops);
        Core.AddDrop(Rewards);

        fiendshard.Fiendshard_Questline();

        int i = 1;
        while (!Core.CheckInventory(Rewards, toInv: false))
        {
        	Core.EnsureAccept(7900);
            Nulgath.FarmBloodGem(20);
            Nulgath.FarmUni13(5);
            Nulgath.FarmTotemofNulgath(3);
            Nulgath.FarmVoucher(false);
            Nulgath.FarmDiamondofNulgath(150);
            Nulgath.FarmGemofNulgath(50);
            Willpower.Unidentified34(10);

            Core.EnsureCompleteChoose(7900);
            Core.Logger($"Completed x{i++}");
        }
    }
}