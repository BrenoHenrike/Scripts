/*
name: AninaQuestRewards
description: This Script will farm all the items in /lavarockshore from the NPC Anina
tags: forge, blaze, awe, blazelight, molten, core, burning, doom
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Good/GearOfAwe/BladeOfAwe.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class AninaQuestReward
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public BladeOfAwe BOA = new();
    public BuyScrolls Scroll = new();
    private CoreBLOD BLOD = new();
    private CoreNSOD NSOD = new();

    public string OptionsStorage = "Burning Weapons";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<WeaponChoice>("WeaponChoice", "Choose Your weapon", "", WeaponChoice.None),
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Weapon();

        Core.SetOptions(false);
    }

    public void Weapon(WeaponChoice WeaponChoice = WeaponChoice.None)
    {
        Core.OneTimeMessage("Skua Cannot do this", "Bot Cannot Get \"Volcanic Essence\", as it Requires you to kill an ultra, and skua cannot do this.", true, true);

        if (Bot.Config!.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.None)
            return;


        if (Bot.Config.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.Blaze_of_Awe || Bot.Config.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.All && !Core.CheckInventory("Blaze of Awe"))
        {
            BOA.GetBoA();

            Core.EnsureAccept(9255);
            Scroll.BuyScroll(Scrolls.ScorchedSteel, 99);
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Akriloth's Scale", 500, false, false);
            Core.HuntMonster("battleundere", "Lava Guard", "Molten Core", 50, false, false);
            Bot.Wait.ForPickup("Molten Core");

            if (!Core.CheckInventory("Volcanic Essence", 10))
            {
                Core.Logger("Bot Cannot Get \"Volcanic Essence\", as it Requires you to kill an ultra, and skua cannot do this.");

                Core.EnsureComplete(9255);
                return;
            }
        }

        if (Bot.Config.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.Blazing_Light_of_Destiny || Bot.Config.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.All && !Core.CheckInventory("Blazing Light of Destiny"))
        {
            BLOD.BlindingLightOfDestiny();

            Core.EnsureAccept(9256);
            Scroll.BuyScroll(Scrolls.ScorchedSteel, 99);
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Akriloth's Scale", 1000, false, false);
            Core.HuntMonster("battleundere", "Lava Guard", "Molten Core", 100, false, false);
            Bot.Wait.ForPickup("Molten Core");

            if (!Core.CheckInventory("Volcanic Essence", 10))
            {
                Core.Logger("Bot Cannot Get \"Volcanic Essence\", as it Requires you to kill an ultra, and skua cannot do this.");

                Core.EnsureComplete(9256);
                return;
            }
        }

        if (Bot.Config.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.Burning_Sword_of_Doom || Bot.Config.Get<WeaponChoice>("WeaponChoice") == WeaponChoice.All && !Core.CheckInventory("Burning Sword of Doom"))
        {
            NSOD.GetNSOD();

            Core.EnsureAccept(9257);
            Scroll.BuyScroll(Scrolls.ScorchedSteel, 99);
            Core.HuntMonster("shadowstrike", "Sepulchuroth", "Akriloth's Scale", 1500, false, false);
            Core.HuntMonster("battleundere", "Lava Guard", "Molten Core", 150, false, false);
            Core.HuntMonster("ashfallcamp", "Smoldur", "Flame Heart", 1, false, false);
            Bot.Wait.ForPickup("Molten Core");

            if (!Core.CheckInventory("Volcanic Essence", 10))
            {
                Core.Logger("Bot Cannot Get \"Volcanic Essence\", as it Requires you to kill an ultra, and skua cannot do this.");

                Core.EnsureComplete(9257);
                return;
            }
        }
    }

    public enum WeaponChoice
    {
        Blaze_of_Awe = 78094,
        Blazing_Light_of_Destiny = 78096,
        Burning_Sword_of_Doom = 78095,
        None,
        All
    };
}
