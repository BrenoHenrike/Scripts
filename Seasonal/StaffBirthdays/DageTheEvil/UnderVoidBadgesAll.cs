/*
name: (Badge) Battle Babysitter
description: This will get the Battle Babysitter badge.
tags: badge, doomwood, battle, baby-sitter
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal\StaffBirthdays\DageTheEvil\Undervoid.cs
using Skua.Core.Interfaces;

public class UnderVoidBadgesAll
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreAdvanced Adv = new();
    public UndervoidStory UndervoidStory = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    private void DoAll()
    {
        ConquestVictor();
        WarVictor();
        FamineVitor();
        DeathVictor();
    }

    public void ConquestVictor()
    {
        if (!Core.isSeasonalMapActive("undervoid"))
            return;

        UndervoidStory.CompleteUnderVoid();
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

        if (Core.HasWebBadge(badge1))
        {
            Core.Logger($"Already have the {badge1} badge");
            return;
        }
        else if (Daily.CheckDaily(3411))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge1} badge");
            Core.EnsureAccept(3411);
            Core.HuntMonster("undervoid", "Conquest", "Conquest Defeated");
            Core.EnsureComplete(3411);
        }
    }

    public void WarVictor()
    {
        if (!Core.isSeasonalMapActive("undervoid"))
            return;

        UndervoidStory.CompleteUnderVoid();
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

        if (Core.HasWebBadge(badge2))
        {
            Core.Logger($"Already have the {badge2} badge");
            return;
        }
        else if (Daily.CheckDaily(0000))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge2} badge");
            Core.EnsureAccept(3412);
            Core.HuntMonster("undervoid", "War", "War Defeated");
            Core.EnsureComplete(3412);
        }
    }

    public void FamineVitor()
    {
        if (!Core.isSeasonalMapActive("undervoid"))
            return;

        UndervoidStory.CompleteUnderVoid();
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

        if (Core.HasWebBadge(badge3))
        {
            Core.Logger($"Already have the {badge3} badge");
            return;
        }
        else if (Daily.CheckDaily(3413))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge3} badge");
            Core.EnsureAccept(3413);
            Core.HuntMonster("undervoid", "Famine", "Famine Defeated");
            Core.EnsureComplete(3413);
        }
    }

    public void DeathVictor()
    {
        if (!Core.isSeasonalMapActive("undervoid"))
            return;

        UndervoidStory.CompleteUnderVoid();
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

        if (Core.HasWebBadge(badge4))
        {
            Core.Logger($"Already have the {badge4} badge");
            return;
        }
        else if (Daily.CheckDaily(3414))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge4} badge");
            Core.EnsureAccept(3414);
            Core.HuntMonster("undervoid", "Death", "Death Defeated");
            Core.EnsureComplete(3414);
        }
    }
    private string badge1 = "Conquest Victor";
    private string badge2 = "War Victor";
    private string badge3 = "Famine Victor";
    private string badge4 = "Death Victor";
}