/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Shinkansen.cs
//cs_include Scripts/Story/Eden.cs
using Skua.Core.Interfaces;

public class GachaponMachine
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public Eden Eden = new();

    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Rewards);
        Core.BankingBlackList.AddRange(Rewards2);
        Core.SetOptions();

        Gacha();

        Core.SetOptions(false);
    }

    public readonly string[] Rewards =
 {
        "Onsen Locks",
        "Onsen Long Locks",
        "Onsen Ponytail",
        "Onsen Bob",
        "Crystallis Bob",
        "Onsen Shag",
        "Onsen Hair",
        "Onsen MopTop",
        "Onsen Spikes",
        "Onsen Spikes + Ponytail",
        "Crystallis Candied Strawberries",
        "Crystallis Cheesedog",
        "Crystallis Dango",
        "Onsen Treats",
        "Gigantic Crystallis Cheesedog",
        "Gigantic Crystallis Dango",
        "Crystallis Onsen Milk",
        "Onsen Smile",
        "Onsen ManBun",
        "Crystallis Proto Morph + Locks",
        "Crystallis Proto Morph + Hair",
    };

    public readonly string[] Rewards2 =
{
        "Smug Face 001",
        "Totally Normal Face 001",
        "Smug Face 002",
        "Totally Normal Face 002",
        "Totally Not a Killer Face 002",
        "Math Notebook",
        "KotaBear 023M",
        "KotaBear 023XL",
        "KotaPon Prize A",
        "Klawaii Prisoner",
        "Klawaii Prisoner Hair",
        "Klawaii Prisoner Locks",
        "KotaPon Prize A Morph Hair",
        "KotaPon Prize A Morph Locks",
        "KotaPon Prize D",
        "Klawaii Enlightenment",
        "KotaPon Prize B",
        "KotaPon Prize C",
        "Eden Formal Outfit",
        "Eden Formal Hair",
        "Eden Formal Locks",
        "Eden High Formal Hair",
        "Eden High Formal Locks",
        "Eden High Formal Cape",
        "Eden Judgement Blade",
        "Eden Judgement Blades",
        "Eden Formal Cane",
        "A Story of Eden: Light Novel",
        "A Story of Eden: Special Edition",
    };

    public void Gacha()
    {
        Eden.StoryLine();
        Core.AddDrop(Rewards);
        Core.AddDrop(Rewards2);

        //1st Quest
        int i = 1;
        Core.Logger("Farming [I heard you like Gacha] Quest");
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            Core.EnsureAccept(7781);
            Core.BuyItem("onsen", 1926, "Gachapon Coin");
            Core.HuntMonster("yokaigrave", "Skello Kitty", "Skello Kitty Bone");
            Core.EnsureCompleteChoose(7781);
            Core.ToBank(Rewards);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops acquired from [I heard you like Gacha] Quest");

        //2nd Quest
        i = 1;
        Core.Logger("Farming [Kotapon II: Get'cha Gear] Quest");
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards2, toInv: false))
        {
            Core.EnsureAccept(8802);
            Core.HuntMonster("eden", "Cosplayer Anomaly", "Eden Resident Token", 10);
            Core.HuntMonster("eden", "Klawaii Machine", "Klawaii Coin");
            Core.EnsureCompleteChoose(8802);
            Core.ToBank(Rewards2);
            Core.Logger($"Completed x{i++}");
        }
        Core.Logger("All drops acquired from [Kotapon II: Get'cha Gear] Quest");
    }

}
