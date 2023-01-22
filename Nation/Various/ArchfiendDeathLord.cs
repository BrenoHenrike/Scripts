/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Story/Nation/Originul.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArchfiendDeathLord
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private Fiendshard_Story Fiendshard = new();
    private CoreNation Nation = new();
    private WillpowerExtraction Willpower = new();

    public string OptionsStorage = "ArchfiendDeathLord";
    public List<IOption> Options = new()
    {
        new Option<bool>("OnlyArmor", "Only get the Armor?", "Whether to only get the Armor or all quest rewards"),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArm(Bot.Config.Get<bool>("OnlyArmor"));

        Core.SetOptions(false);
    }

    public void GetArm(bool OnlyArmor = true)
    {
        string[] RewardsList = OnlyArmor ? new[] { "Archfiend DeathLord" } : Core.QuestRewards(7900);
        if (Core.CheckInventory(RewardsList))
            return;

        Core.AddDrop(RewardsList.ToArray());
        Fiendshard.Fiendshard_Questline();

        Core.RegisterQuests(7900);
        while (!Bot.ShouldExit && !Core.CheckInventory(RewardsList))
        {
            Nation.FarmBloodGem(20);
            Nation.FarmUni13(5);
            Nation.FarmTotemofNulgath(3);
            Nation.FarmVoucher(false);
            Nation.FarmDiamondofNulgath(150);
            Nation.FarmGemofNulgath(50);
            Willpower.Unidentified34(10);

            Bot.Wait.ForPickup("*");
        }
    }
}
