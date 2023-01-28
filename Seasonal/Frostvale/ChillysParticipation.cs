/*
name: Chilly's Participation
description: This will finish the quest that is required to get free acs throughout the event.
tags: chillys-participation, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class ChillysQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ChillysParticipation();

        Core.SetOptions(false);
    }

    public void ChillysParticipation()
    {
        if (Core.isCompletedBefore(9004))
            return;

        if (!Bot.Flash.CallGameFunction<bool>("world.myAvatar.isEmailVerified"))
            Core.Logger("Your email adres is not verified!", messageBox: true, stopBot: true);
        if (Bot.Player.Level < 30)
            Core.Logger("Level 30+ required.", messageBox: true, stopBot: true);
            
        Core.EnsureAccept(9004);
        Core.HuntMonster("battleontown", "Festive Zard", "Reminder Delivered");
        Core.EnsureComplete(9004);
    }
}
