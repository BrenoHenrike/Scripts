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
        new Option<RewardChoice>("RewardChoice", "Choose Your Reward", "", RewardChoice.All),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArm(Bot.Config!.Get<bool>("OnlyArmor"), RewardChoice.All);

        Core.SetOptions(false);
    }

    public void GetArm(bool OnlyArmor, RewardChoice Reward)
    {
        if (Reward != RewardChoice.All && Core.CheckInventory((int)Reward, toInv: false))
            return;

        else if (OnlyArmor && Reward == RewardChoice.All)
            Core.Logger("With \"OnlyArmor\" Please Select the \"Archfiend DeathLord\" Option from the list.", messageBox: true, stopBot: true);
        else Core.AddDrop((int)Reward);

        Fiendshard.Fiendshard_Questline();

        if (Reward == RewardChoice.All)
        {
            foreach (RewardChoice reward in Enum.GetValues(typeof(RewardChoice)))
            {
                if (Core.CheckInventory((int)Reward, toInv: false))
                    break;

                Core.FarmingLogger(reward.ToString(), 1);
                while (!Bot.ShouldExit && !Core.CheckInventory((int)reward, toInv: false))
                {
                    Core.EnsureAccept(7900);
                    Nation.FarmBloodGem(20);
                    Nation.FarmUni13(5);
                    Nation.FarmTotemofNulgath(3);
                    Nation.FarmVoucher(false);
                    Nation.FarmDiamondofNulgath(150);
                    Nation.FarmGemofNulgath(50);
                    Willpower.Unidentified34(10);
                    Core.EnsureComplete(7900, (int)reward);

                    Bot.Wait.ForPickup((int)reward);
                }
            }
        }

        else
        {
            Core.FarmingLogger(Reward.ToString(), 1);
            while (!Bot.ShouldExit && !Core.CheckInventory((int)Reward, toInv: false))
            {
                Core.EnsureAccept(7900);
                Nation.FarmBloodGem(20);
                Nation.FarmUni13(5);
                Nation.FarmTotemofNulgath(3);
                Nation.FarmVoucher(false);
                Nation.FarmDiamondofNulgath(150);
                Nation.FarmGemofNulgath(50);
                Willpower.Unidentified34(10);
                Core.EnsureComplete(7900, (int)Reward);

                Bot.Wait.ForPickup((int)Reward);
            }
        }
    }

    public enum RewardChoice
    {
        All,
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