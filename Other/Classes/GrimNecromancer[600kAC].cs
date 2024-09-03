/*
name: Grim Necromancer Class (600k AC)
description: This script will get Grim Necromancer class which requires 600k ACs badge.
tags: 600k, grimnecromancer, grim necromancer, grimtea, grim tea, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class GrimNecromancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGN();

        Core.SetOptions(false);
    }

    public void GetGN(bool rankUpClass = true)
    {
        if (Core.CheckInventory(45848))
            return;

        if (!Core.HasAchievement(29, "ip14"))
        {
            Core.Logger("This class requires you to have 600,000 ACs badge.");
            return;
        }

        Adv.BuyItem("whitemap", 1649, "Grim Necromancer");

        // The Guest List (6924)
        if (!Story.QuestProgression(6924))
        {
            Core.EnsureAccept(6924);
            Core.HuntMonster("bonecastle", "Vaden", "Vaden Invited");
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Big Jack Sprat Invited");
            Core.HuntMonster("thespan", "Moglin Ghost", "Moglin Ghost Invited");
            Core.EnsureComplete(6924);
        }

        // Sugar, Spice and everything Nice (6925)
        if (!Story.QuestProgression(6925))
        {
            Core.EnsureAccept(6925);
            Adv.BuyItem("sandsea", 245, "Cuppycake", 25);
            Adv.BuyItem("vendorbooths", 659, "Strawberry Frog Cookie");
            Core.HuntMonster("baconcatlair", "Ice Cream Shark", "Ice Cream Shark Defeated", 5);
            Core.EnsureComplete(6925);
        }

        // Big Mean Meanie Pants (6926)
        Story.KillQuest(6926, "vordredboss", "Vordred");

        // One Small Step (6927)
        if (!Story.QuestProgression(6927))
        {
            Core.EnsureAccept(6927);

            Core.AddDrop(45848, 48785);
            Core.EquipClass(ClassType.Farm);

            Core.RegisterQuests(6928);
            while (!Bot.ShouldExit && !Core.CheckInventory(48785, 50))
            {
                Core.HuntMonster("citadel", "Grizzly Bear", "Grizzly Bear Captured", 3);
                Core.HuntMonster("battlegroundb", "Polar Bear", "Polar Bear Captured", 3);
                Core.HuntMonster("huntersmoon", "Eclipsed One", "Eclipsed One Captured", 3);
                Bot.Wait.ForPickup(48785);
            }
            Core.CancelRegisteredQuests();

            Core.EnsureComplete(6927);
        }

        if (rankUpClass)
            Adv.RankUpClass("Grim Necromancer");
    }
}
