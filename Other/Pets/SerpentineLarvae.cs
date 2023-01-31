/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class SerpentineLarvae
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    public CoreNation Nation = new();

    public bool DontPreconfigure = true;

    public string OptionsStorage = "RewardsSelect";

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("SelectReward", "Choose the Reward", "Select the Reward the Bot will Get, then stop.", false),
        new Option<bool>("AutoRewardChoice", "Get All", "does the quest till you have all the rewards possible.", false),
        new Option<RewardsSelection>("RewardSelect", "Choose Your Reward", "", RewardsSelection.All)
    };


    string[] QuestRewards =
    {
    "Serpentine_Void Fang",
    "Serpentine_Void Fang Eyes",
    "Serpentine_Void Fang's Jaws",
    "Serpentine_Void Fang's Maw",
    "Serpentine_Void Backblade",
    "Serpentine_Void Backblade and Shield",
    "Serpentine_Void Blade",
    "Serpentine_Void Blades",
    "Serpentine_Void Slasher",
    "Serpentine_Void Slashers",
    "Serpentine_Void Flail",
    "Serpentine_Void Flail and Shield",
    "Serpentine_Void Armaments",
    "Serpentine_Void Claw",
    "Serpentine_Void Claws"
    };


    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(false);
    }

    public void DoQuest()
    {
        GetPet();

        if (Bot.Config.Get<bool>("SelectReward"))
            OptionsSelect(Bot.Config.Get<RewardsSelection>("RewardSelect"), 8944);
        else AutoReward(8944);
    }

    public void GetPet()
    {
        if (!Core.CheckInventory("Serpentine Larvae"))
            return;
        Core.HuntMonster("darkallaince", "Shadowflame Nulgath", "Serpentine Larvae", isTemp: false, log: false);
    }

    public void OptionsSelect(RewardsSelection reward = new(), int questID = 000)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        ItemBase item = Core.EnsureLoad(questID).Rewards.Find(x => x.ID == (int)Bot.Config.Get<RewardsSelection>("RewardSelect"));

        if (item == null)
        {
            Core.Logger($"{item.Name} not found in Quest Rewards");
            return;
        }

        if (Core.CheckInventory(item.Name))
            return;
        Core.FarmingLogger(item.Name, 1);
        while (!Core.CheckInventory(item.Name))
        {
            Core.EnsureAccept(questID);
            Core.HuntMonster(Core.IsMember ? "nulgath" : "evilmarsh", "Tainted Elemental", "Tainted Soul", 5, isTemp: false, log: false);
            Core.HuntMonster("northlands", "Aisha's Drake", "Blade of Holy Might", isTemp: false, log: false);
            Nation.FarmDarkCrystalShard(10);
            Nation.SwindleBulk(15);
            Adv.BuyItem("evilwarnul", 456, "Oversoul Witch of Nulgath");
            Core.HuntMonster("dragonhame", "Infected Dragon", "Infected Dragon Soul", 5, isTemp: false, log: false);

            if (Bot.Config.Get<RewardsSelection>("RewardsSelection") != RewardsSelection.All)
                Core.EnsureComplete(questID, item.ID);
            else Core.EnsureComplete(questID);
        }
    }

    public void AutoReward(int questID = 0000, int quant = 1)
    {
        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        foreach (ItemBase item in RewardOptions)
            Core.AddDrop(item.Name);

        foreach (ItemBase item in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item.ID, quant))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster(Core.IsMember ? "nulgath" : "evilmarsh", "Tainted Elemental", "Tainted Soul", 5, isTemp: false, log: false);
                Core.HuntMonster("northlands", "Aisha's Drake", "Blade of Holy Might", isTemp: false, log: false);
                Nation.FarmDarkCrystalShard(10);
                Nation.SwindleBulk(15);
                Adv.BuyItem("evilwarnul", 456, "Oversoul Witch of Nulgath");
                Core.HuntMonster("dragonhame", "Infected Dragon", "Infected Dragon Soul", 5, isTemp: false, log: false);
                Core.EnsureComplete(questID, item.ID);
            }
        }
    }

    public enum RewardsSelection
    {
        All,
        Serpentine_Void_Fang = 75621,
        Serpentine_Void_Fang_Eyes = 75622,
        Serpentine_Void_Fangs_Jaws = 75623,
        Serpentine_Void_Fangs_Maw = 75624,
        Serpentine_Void_Backblade = 75625,
        Serpentine_Void_Backblade_and_Shield = 75626,
        Serpentine_Void_Blade = 75627,
        Serpentine_Void_Blades = 75628,
        Serpentine_Void_Slasher = 75629,
        Serpentine_Void_Slashers = 75630,
        Serpentine_Void_Flail = 75631,
        Serpentine_Void_Flail_and_Shield = 75632,
        Serpentine_Void_Armaments = 75633,
        Serpentine_Void_Claw = 75634,
        Serpentine_Void_Claws = 75635,
    };
}
