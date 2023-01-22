/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/Legion/DageChallengeStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class EmpoweredBladeMaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public CoreLegion Legion = new();
    public DageChallengeStory DageChallenge = new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "EmpoweredBladeMasterRewards";

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("GetAllRewards", "Pick Automatically", "if true, does the quest till you have all the rewards possible. otherwise Gets selcted item", false),
        new Option<rewards>("RewardSelect", "Choose Your Reward", "", rewards.Empowered_Blade_Master)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSet();

        Core.SetOptions(false);
    }

    public void GetSet()
    {
        // if (!Core.CheckInventory(new[] { "BladeMaster's Dual Katanas", "Living BladeMaster", "Dark Unicorn Rib" }))
        //     return;

        DageChallenge.DageChallengeQuests();

        List<ItemBase> RewardOptions = Core.EnsureLoad(8554).Rewards;

        Core.AddDrop(RewardOptions.Select(x => x.Name).ToArray());

        Farm.Experience(95);
        Core.EquipClass(ClassType.Solo);

        if (Bot.Config.Get<bool>("GetAllRewards"))
            Core.RegisterQuests(8554);
        else Core.EnsureAccept(8554);

        foreach (var Reward in RewardOptions)
        {
            if (Core.CheckInventory(Reward.Name, toInv: false))
                return;

            Core.FarmingLogger(Reward.Name, 1);

            Legion.FarmLegionToken(15000);
            DageInsignia(30);
            if (!Bot.Config.Get<bool>("GetAllRewards"))
            {
                Core.EnsureComplete(8554, (int)Bot.Config.Get<rewards>("RewardSelect"));
                return;
            }
            Core.JumpWait();
            Core.ToBank(Reward.Name);
        }
        Core.CancelRegisteredQuests();
    }

    public void DageInsignia(int quant)
    {
        //Dage the Evil Insignia
        Bot.Events.RunToArea += Event_RunToArea;
        if (!Core.CheckInventory("Dage the Evil Insignia", 30))
        {
            if (Bot.Quests.IsDailyComplete(8547))
                Core.Logger("Can't accept quest 8547 because the weekly is complete", messageBox: true, stopBot: true);
            Core.EnsureAccept(8547);
            Core.EquipClass(ClassType.Solo);

            Adv.BoostKillMonster("UltraDage", "Boss", "Right", "Dage the Dark Lord", "Dage the Dark Lord Defeated", isTemp: false, publicRoom: false);

            Core.EnsureComplete(8547);
            Bot.Wait.ForPickup("Dage the Evil Insignia");
        }
        Bot.Events.RunToArea -= Event_RunToArea;

        void Event_RunToArea(string zone)
        {
            switch (zone.ToLower())
            {
                case "a":
                    //Move to the left
                    Bot.Player.WalkTo(Bot.Random.Next(40, 175), Bot.Random.Next(400, 410), speed: 8);
                    break;
                case "b":
                    //Move to the right
                    Bot.Player.WalkTo(Bot.Random.Next(760, 930), Bot.Random.Next(410, 415), speed: 8);
                    break;
                default:
                    //Move to the center
                    Bot.Player.WalkTo(Bot.Random.Next(480, 500), Bot.Random.Next(300, 420), speed: 8);
                    break;
            }
        }
    }

    public enum rewards
    {
        Empowered_Blade_Master = 68470,
        Empowered_Blade_Masters_Katana = 68471,
        Empowered_Dual_Katanas = 68472
    };
}
