//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Story/Nation/Originul.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;


public class ArchfiendDeathLord
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();
    public Fiendshard_Story fiendshard = new();
    public CoreNation Nation = new();
    public WillpowerExtraction Willpower = new();

    public string OptionsStorage = "Class or All";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("OnlyArmor", "Only get the Armor?", "Whether to only get the Armor or all quest rewards"),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArm();

        Core.SetOptions(false);
    }

    public void GetArm(bool OnlyArmor = true)
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(7900).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        if (Core.CheckInventory("Archfiend DeathLord"))
            return;

        fiendshard.Fiendshard_Questline();

        Core.AddDrop(Rewards);

        Core.RegisterQuests(7900);

        if (OnlyArmor == true)
        {
            Core.Logger("only getting the class");
            Nation.FarmBloodGem(20);
            Nation.FarmUni13(5);
            Nation.FarmTotemofNulgath(3);
            Nation.FarmVoucher(false);
            Nation.FarmDiamondofNulgath(150);
            Nation.FarmGemofNulgath(50);
            Willpower.Unidentified34(10);
            Bot.Wait.ForPickup("Archfiend DeathLord");
        }
        else
        {
            foreach (string item in RewardsList)
            {
                Core.Logger($"Farming for {item}");

                while (!Bot.ShouldExit && !Core.CheckInventory(item))
                {

                    Nation.FarmBloodGem(20);
                    Nation.FarmUni13(5);
                    Nation.FarmTotemofNulgath(3);
                    Nation.FarmVoucher(false);
                    Nation.FarmDiamondofNulgath(150);
                    Nation.FarmGemofNulgath(50);
                    Willpower.Unidentified34(10);
                    Bot.Wait.ForPickup(item);
                }
            }
        }
        Core.CancelRegisteredQuests();
    }
}
