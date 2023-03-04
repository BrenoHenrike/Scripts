/*
name: ArchfiendDeathLord
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
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArchfiendDeathLord
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private Fiendshard_Story Fiendshard = new();
    private CoreNation Nation = new();
    private WillpowerExtraction Willpower = new();

    public string OptionsStorage = "ArchfiendDeathLord";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("OnlyArmor", "Only get the Armor?", "Whether to only get the Armor or all quest rewards"),
        new Option<RewardChoice>("RewardChoice", "Choose Your Reward", "", RewardChoice.Archfiend_DeathLord),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArm(Bot.Config.Get<bool>("OnlyArmor"));

        Core.SetOptions(false);
    }

    public void GetArm(bool OnlyArmor = true)
    {
        if (Bot.Config.Get<RewardChoice>("RewardChoice") == RewardChoice.None || Core.CheckInventory(Bot.Config.Get<RewardChoice>("RewardChoice").ToString()))
            return;

        else if (OnlyArmor && Bot.Config.Get<RewardChoice>("RewardChoice") != RewardChoice.Archfiend_DeathLord)
            Core.Logger("With \"OnlyArmor\" Please Select the \"Archfiend DeathLord\" Option from the list.", messageBox: true, stopBot: true);
        else Core.AddDrop(Bot.Config.Get<RewardChoice>("RewardChoice").ToString());

        Fiendshard.Fiendshard_Questline();

        Core.FarmingLogger(Bot.Config.Get<RewardChoice>("RewardChoice").ToString(), 1);
        Core.RegisterQuests(7900);
        while (!Bot.ShouldExit && !Core.CheckInventory(Bot.Config.Get<RewardChoice>("RewardChoice").ToString()))
        {
            Nation.FarmBloodGem(20);
            Nation.FarmUni13(5);
            Nation.FarmTotemofNulgath(3);
            Nation.FarmVoucher(false);
            Nation.FarmDiamondofNulgath(150);
            Nation.FarmGemofNulgath(50);
            Willpower.Unidentified34(10);

            Bot.Wait.ForPickup(Bot.Config.Get<RewardChoice>("RewardChoice").ToString());
        }
        Core.CancelRegisteredQuests();
    }

    private enum RewardChoice
    {
        None,
        Archfiend_DeathLord = 54366,
        Champion_Ender_of_Nulgath = 54377,
        Doomblade_of_Genocide = 54379,
        Deathlord_Blade_of_Nulgath = 54380,
        Archfiend_Annihilator_of_Death = 54381,
        Corpse_Hijacker_of_Nulgath = 54382,
        Soul_Jacker_of_Nulgath = 54383,
        Archfiendish_Spear_of_Death = 54384,
        Undeathly_SoulReaper_of_Nulgath = 54385,
        Dual_FiendLords_Claymores = 54385,
    };
}