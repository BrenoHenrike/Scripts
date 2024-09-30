/*
name: (Badge) UnderVoid Badges
description: This will get all 4 bades in undervoid
tags: badge, undervoid, conquest victor, war victor, famine vitor, death victor
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/CoreDageBirthday.cs
using Skua.Core.Interfaces;

public class UnderVoidBadgesAll
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreDailies Daily = new();
    public CoreAdvanced Adv = new();
    private CoreDageBirthday Dage = new();
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

        Dage.Undervoid();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Conquest's Pride");

        if (Core.HasWebBadge(badge1))
        {
            Core.Logger($"Already have the {badge1} badge");
            return;
        }
        else if (Daily.CheckDailyv2(3411))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge1} badge");
            //Adv.BestGear(RacialGearBoost.Undead);
            Core.EnsureAccept(3411);
            Core.HuntMonster("undervoid", "Conquest", "Conquest Defeated");
            Core.EnsureComplete(3411);
            if (Core.CheckInventory("Conquest's Pride", 7))
                Core.ChainComplete(3415);
        }
    }

    public void WarVictor()
    {
        if (!Core.isSeasonalMapActive("undervoid") || !Core.isCompletedBefore(3415))
        {
            Core.Logger(!Core.isSeasonalMapActive("undervoid") ? $"Map not Avaiable" : $"Quest Not Unlocked Yet. each Daily takes 7x the previou (wtf i know right)");
            return;
        }

        Dage.Undervoid();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("War's Pride");


        if (Core.HasWebBadge(badge2))
        {
            Core.Logger($"Already have the {badge2} badge");
            return;
        }
        else if (Daily.CheckDailyv2(3412))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge2} badge");
            Core.EnsureAccept(3412);
            Core.HuntMonster("undervoid", "War", "War Defeated");
            Core.EnsureComplete(3412);

            if (Core.CheckInventory("War's Pride", 7))
                Core.ChainComplete(3416);
        }
    }

    public void FamineVitor()
    {
        if (!Core.isSeasonalMapActive("undervoid") || !Core.isCompletedBefore(3416))
        {
            Core.Logger(!Core.isSeasonalMapActive("undervoid") ? $"Map not Avaiable" : $"Quest Not Unlocked Yet. each Daily takes 7x the previou (wtf i know right)");
            return;
        }

        Dage.Undervoid();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Famine's Pride");

        if (Core.HasWebBadge(badge3))
        {
            Core.Logger($"Already have the {badge3} badge");
            return;
        }

        else if (Daily.CheckDailyv2(3413))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge3} badge");
            Core.EnsureAccept(3413);
            Core.HuntMonster("undervoid", "Famine", "Famine Defeated");
            Core.EnsureComplete(3413);

            if (Core.CheckInventory("Famine's Pride", 7))
                Core.ChainComplete(3417);
        }
    }

    public void DeathVictor()
    {
        if (!Core.isSeasonalMapActive("undervoid") || !Core.isCompletedBefore(3417))
        {
            Core.Logger(!Core.isSeasonalMapActive("undervoid") ? $"Map not Avaiable" : $"Quest Not Unlocked Yet. each Daily takes 7x the previou (wtf i know right)");
            return;
        }

        Dage.Undervoid();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Death's Pride");

        if (Core.HasWebBadge(badge4))
        {
            Core.Logger($"Already have the {badge4} badge");
            return;
        }
        else if (Daily.CheckDailyv2(3414))
        {
            Core.Logger($"Doing UnderVoid Quest for {badge4} badge");
            Core.EnsureAccept(3414);
            Core.HuntMonster("undervoid", "Death", "Death Defeated");
            Core.EnsureComplete(3414);

            if (Core.CheckInventory("Death's Pride", 7))
                Core.ChainComplete(3418);
        }
    }
    private string badge1 = "Conquest Victor";
    private string badge2 = "War Victor";
    private string badge3 = "Famine Victor";
    private string badge4 = "Death Victor";
}
