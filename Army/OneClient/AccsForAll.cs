/*
name: Army Free 500 accs
description: the 500 free acs quest
tags: acs, free, thefamily, army.
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyFreeAcs
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Core.Logger("Quest has been Removed, blame AE");
        // FreeAcs();

        Core.SetOptions(false);
    }

    private void FreeAcs()
    {
        List<string> warnings = new();
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        while (!Bot.ShouldExit && Army.doForAll())
        {
            if (!Story.QuestProgression(9937))
            {
                Core.EnsureAccept(9937);
                Core.HuntMonster("yulgar", "Agitated Orb", "Free ACs... and Yogurt");
                Core.EnsureComplete(9937);
            }
        }
    }

    // public void FreeAcs()
    // {
    //     List<string> warnings = new();
    //     Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

    //     while (!Bot.ShouldExit && Army.doForAll())
    //     {
    //         // Verified Email
    //         if (Bot.Flash.CallGameFunction<bool>("world.myAvatar.isEmailVerified"))
    //         {
    //             if (!Core.isCompletedBefore(9578))
    //             {
    //                 Core.EnsureAccept(9578);
    //                 Bot.Quests.UpdateQuest(7522);
    //                 Core.EquipClass(ClassType.Solo);
    //                 Core.HuntMonster("borgars", "Burglinster", "Cookie Dough");
    //                 Core.EnsureComplete(9578);
    //             }
    //         }
    //         else
    //         {
    //             Core.Logger($"Unverified Email: {Core.Username()} - Skipping");
    //             continue;
    //         }


    //     }
    // }

}


#region Preious years (just copy and paste, then comment out)

#region 2023
/*
2023: ID: 9444
2024: ID: 9937
while (!Bot.ShouldExit && Army.doForAll())
{

    if (Story.QuestProgression(9937))
    {
        Core.Logger("Quest not avaible / is already completed.");
    }
    else
    {
        Core.EnsureAccept(9937);
        Core.HuntMonster("yulgar", "Agitated Orb", "Free ACs... and Yogurt");
        Core.EnsureComplete(9937);
    }
}
*/
#endregion 2023

#endregion Preious years (just copy and paste, then comment out)
