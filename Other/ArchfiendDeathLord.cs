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
using RBot.Items;
using RBot.Options;


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

    public string OptionsStorage = "Class or All";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("OnlyClass", "Only get Class?", "Whether to only get the class or all quest rewards"),
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetArm();

        Core.SetOptions(false);
    }

    public void GetArm(bool OnlyClass = true)
    {
        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(7900).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        if (Core.CheckInventory("Archfiend DeathLord"))
            return;

        fiendshard.Fiendshard_Questline();

        Core.AddDrop(Rewards);

        Core.RegisterQuests(7900);

        if (OnlyClass == true)
        {
            Core.Logger("only getting the class");
            Nulgath.FarmBloodGem(20);
            Nulgath.FarmUni13(5);
            Nulgath.FarmTotemofNulgath(3);
            Nulgath.FarmVoucher(false);
            Nulgath.FarmDiamondofNulgath(150);
            Nulgath.FarmGemofNulgath(50);
            Willpower.Unidentified34(10);
            Bot.Wait.ForPickup("Archfiend DeathLord");
        }
        else
        {
            foreach (string item in RewardsList)
            {
                Core.Logger($"Farming for {item}");

                while (!Bot.ShouldExit() && !Core.CheckInventory(item))
                {

                    Nulgath.FarmBloodGem(20);
                    Nulgath.FarmUni13(5);
                    Nulgath.FarmTotemofNulgath(3);
                    Nulgath.FarmVoucher(false);
                    Nulgath.FarmDiamondofNulgath(150);
                    Nulgath.FarmGemofNulgath(50);
                    Willpower.Unidentified34(10);
                    Bot.Wait.ForPickup(item);
                }
            }
        }
        Core.CancelRegisteredQuests();
    }
}
