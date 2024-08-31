/*
name: Gear of Allwe
description: This bot will farm get you all the gear of awe
tags: armor, awe, good, pauldron, breastplate, vambrace, gauntlet, greaves, fragment, shard, relic
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
//cs_include Scripts/Other/Weapons/FortitudeAndHubris.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Good/GearOfAwe/Awescended.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;

public class GearofAllwe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreAwe Awe = new();
    public Awescended Awescended = new();

    public string OptionsStorage = "GearofAllwe";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("GetAwescended", "Get Awescended? [Long]", $"its a long grind.. are u sure? ᶠᵘᶜᵏ ᶦᵗ ʷᵉ ᵇᵃˡˡ", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAllwe();

        Core.SetOptions(false);
    }

    private void GetAllwe()
    {
        GetArmor();
        GetHoA();
        GetCoA();
        BoA();
        if (Bot.Config!.Get<bool>("GetAwescended"))
            Awescended.GetAwe();
    }

    public void GetArmor()
    {
        if (Core.CheckInventory("Armor of Awe"))
            return;

        if (Core.IsMember)
        {
            if (!Core.CheckInventory("Pauldron Relic"))
            {
                Farm.BladeofAweREP(10, false);
                Farm.Experience(55);
                Core.BuyItem("museum", 1130, "Armor of Awe Pass");
                Core.AddDrop("Pauldron Fragment");
                Core.EquipClass(ClassType.Solo);

                Core.RegisterQuests(4162);
                while (!Bot.ShouldExit && !Core.CheckInventory("Pauldron Fragment", 15))
                {
                    Core.HuntMonster("gravestrike", "Ultra Akriloth", "Pauldron Shard", 15, false);
                    Bot.Wait.ForPickup("Pauldron Fragment");
                }
                Core.CancelRegisteredQuests();

                Core.BuyItem("museum", 1129, "Pauldron Relic");
            }
        }
        else
            Awe.GetAweRelic("Pauldron", 4160, 15, 15, "gravestrike", "Ultra Akriloth");

        Awe.GetAweRelic("Breastplate", 4163, 10, 10, "aqlesson", "Carnax");
        Awe.GetAweRelic("Vambrace", 4166, 15, 15, "bloodtitan", "Ultra Blood Titan");
        Awe.GetAweRelic("Gauntlet", 4169, 25, 5, "alteonbattle", "ULTRA Alteon");
        Awe.GetAweRelic("Greaves", 4172, 10, 15, "bosschallenge", "Mutated Void Dragon");
        Core.BuyItem("museum", 1129, "Armor of Awe");

        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
    public void GetHoA()
    {
        if (Core.CheckInventory("Helm of Awe"))
            return;

        Awe.GetAweRelic("Helm", 4175, 10, 5, "doomvaultb", "Undead Raxgore");
        Core.BuyItem("museum", 1129, "Helm of Awe");

        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
    public void GetCoA()
    {
        if (Core.CheckInventory("Cape of Awe"))
            return;

        Awe.GetAweRelic("Cape", 4178, 1, 1, "doomvault", "Binky");
        Adv.BuyItem("museum", 1129, "Cape of Awe");

        Core.ToBank("Legendary Awe Pass", "Guardian Awe Pass", "Armor of Awe Pass");
    }
    public void BoA() => Farm.BladeofAweREP(6, true);



}

