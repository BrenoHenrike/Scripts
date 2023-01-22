/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class CoreNSOD
{
    private bool OptimizeInv = true;

    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreDailies Daily = new();

    public CoreBLOD BLOD = new();
    public CoreSDKA SDKA = new();
    public Necromancer Necro = new();
    public BattleUnder BattleUnder = new();

    public string OptionsStorage = "NecroticSwordOfDoomOptions";
    public bool DontPreconfigure = true;
    public Option<bool> MaxStack = new Option<bool>("MaxStack", "Max Stack", "Max Stack Monster Essences in \"Retreive Void Auras\"\nRecommended setting: True", true);
    public Option<bool> PreFarm = new Option<bool>("PreFarm", "Pre Farm Materials", "Farm all requiered items before merging everything. Not recommended if you already did a merge yourself.\nRecommended setting: False", false);
    public Option<bool> GetSDKA = new Option<bool>("getSDKA", "Get SDKA first [Mem]", "If true, the bot will attempt to get SDKA first, so that it can use the fastest Void Aura farm available\nMember-Only\nRecommended setting: True", true);

    public string[] Essences =
    {
        "Astral Ephemerite Essence",
        "Belrot the Fiend Essence",
        "Black Knight Essence",
        "Tiger Leech Essence",
        "Carnax Essence",
        "Chaos Vordred Essence",
        "Dai Tengu Essence",
        "Unending Avatar Essence",
        "Void Dragon Essence",
        "Creature Creation Essence"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void GetNSOD()
    {
        if (Core.CheckInventory("Necrotic Sword of Doom") && Core.HasWebBadge("Necrotic Sword of Doom"))
            return;

        if (!Core.CheckInventory("Necrotic Sword of Doom"))
        {
            Barium();
            if (Bot.Config.Get<bool>("PreFarm"))
            {
                VoidAuras(7500);
                CavernCelestite(1600);
                Core.AddDrop("Bone Dust", "Undead Energy");
                Farm.BattleUnderB("Bone Dust", 5100);
                Farm.BattleUnderB("Undead Energy", 10000);
                PrimarchHilt(2);
                BladeEssence(2);
                CHourglass(31);
                ScrollDarkArts(4);
                Core.HuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", isTemp: false, publicRoom: true, log: false);
            }
            NSBlade();
            NSHilt();
            NSAura();
            Core.HuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", isTemp: false, publicRoom: true, log: false);
            VoidAuras(800);

            Core.BuyItem("shadowfall", 793, "Necrotic Sword of Doom");
            Adv.EnhanceItem("Necrotic Sword of Doom", EnhancementType.Lucky, wSpecial: WeaponSpecial.Spiral_Carve);
        }

        Core.Logger("Getting the NSOD character page badge");
        Core.EnsureAccept(7652);
        Core.HuntMonster("graveyard", "Skeletal Warrior", "Arcane Parchment", log: false);
        Core.EnsureComplete(7652);
        Core.Relogin();

        if (!Core.CheckInventory(14474) && !Core.IsMember)
            Core.Logger("Congratulations on completing the longest farm in the game!!!", messageBox: true);
    }

    public void GetNBOD()
    {
        if (Core.CheckInventory("Necrotic Blade of Doom"))
            return;

        GetNSOD();
        VoidAuras(750);
        if (!Core.CheckInventory("Void Essentia"))
        {
            Core.Logger("Flibbitiestgibbet is a very tough monster, I hope you brought your army/butler/friends!");
            Core.KillMonster("voidflibbi", "Enter", "Spawn", "Flibbitiestgibbet", "Void Essentia", isTemp: false, log: false);
        }
        Core.BuyItem("shadowfall", 793, "Necrotic Blade of Doom");
        Core.Logger("Don't forget to use AE's Buy-Back system to retreive your Necrotic Sword of Doom", messageBox: true);
    }

    #region Void Auras

    public void VoidAuras(int quant = 7500, bool getSDKA = false)
    {
        if (Core.CheckInventory("Void Aura", quant))
            return;

        if (getSDKA)
            SDKA.DoAll();
        CommandingShadowEssences(quant);
        GatheringUnstableEssences(quant);
        RetrieveVoidAuras(quant);
    }

    public void CommandingShadowEssences(int quant = 7500)
    {
        if (Core.CheckInventory("Void Aura", quant) || !Core.CheckInventory(14474))
            return;

        Core.AddDrop("Void Aura");
        Core.Logger($"Farming Void Aura's ({Bot.Inventory.GetQuantity("Void Aura")}/{quant}) with SDKA Method");

        Core.RegisterQuests(4439);
        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", quant))
        {
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Empowered Essence", 50, false, log: false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Malignant Essence", 3, false, publicRoom: true, log: false);

            Bot.Wait.ForPickup("Void Aura");
            Core.Logger($"{Bot.Inventory.GetQuantity("Void Aura")}/{quant})");
        }
        Core.CancelRegisteredQuests();
    }

    public void GatheringUnstableEssences(int quant = 7500)
    {
        if (Core.CheckInventory("Void Aura", quant) || !Core.IsMember)
            return;

        Farm.EvilREP();
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Void Aura");
        Core.Logger($"Farming Void Aura's ({Bot.Inventory.GetQuantity("Void Aura")}/{quant}) with Member-Only Method");

        Core.RegisterQuests(4438);
        while (!Core.CheckInventory("Void Aura", quant))
        {
            Core.KillMonster("reddeath", "r2", "Left", "*", "Mirror Essence", 175, false, log: false);
            Core.KillMonster("neverworldb", "r2", "Left", "*", "Twisted Essence", 25, false, log: false);
            Core.HuntMonster("doomwar", "Zombie King Alteon", "Transposed Essence", 1, false, log: false);

            Bot.Wait.ForPickup("Void Aura");
            Core.Logger($"{Bot.Inventory.GetQuantity("Void Aura")}/{quant})");
        }
        Core.CancelRegisteredQuests();
    }

    public void RetrieveVoidAuras(int quant = 7500)
    {
        if (Core.CheckInventory("Void Aura", quant))
            return;

        int Essencequant = Bot.Config.Get<bool>("MaxStack") ? 100 : 20;

        Farm.EvilREP();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop("Void Aura");
        Core.AddDrop(Essences);
        if (!Core.CheckInventory("Necromancer", toInv: false) && !Core.CheckInventory("Creature Shard", toInv: false))
            Core.AddDrop("Creature Shard");
        Core.Logger($"Gathering {quant} Void Aura's with Non-Mem/Non-SDKA Method");

        Core.RegisterQuests(4432);
        while (!Bot.ShouldExit && !Core.CheckInventory("Void Aura", quant))
        {
            Core.HuntMonster("timespace", "Astral Ephemerite", "Astral Ephemerite Essence", Essencequant, false, log: false);
            Core.HuntMonster("citadel", "Belrot the Fiend", "Belrot the Fiend Essence", Essencequant, false, publicRoom: true, log: false);
            Core.KillMonster("greenguardwest", "BKWest15", "Down", "Black Knight", "Black Knight Essence", Essencequant, false, publicRoom: true, log: false);
            Core.KillMonster("mudluk", "Boss", "Down", "Tiger Leech", "Tiger Leech Essence", Essencequant, false, publicRoom: true, log: false);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Essence", Essencequant, false, publicRoom: true, log: false);
            Core.KillMonster("necrocavern", "r16", "Down", "Chaos Vordred", "Chaos Vordred Essence", Essencequant, false, publicRoom: true, log: false);
            Core.HuntMonster("hachiko", "Dai Tengu", "Dai Tengu Essence", Essencequant, false, publicRoom: true, log: false);
            Core.HuntMonster("timevoid", "Unending Avatar", "Unending Avatar Essence", Essencequant, false, publicRoom: true, log: false);
            Core.HuntMonster("dragonchallenge", "Void Dragon", "Void Dragon Essence", Essencequant, false, publicRoom: true, log: false);
            Core.KillMonster("maul", "r3", "Down", "Creature Creation", "Creature Creation Essence", Essencequant, false, publicRoom: true, log: false);

            Bot.Wait.ForPickup("Void Aura");
            Core.Logger($"{Bot.Inventory.GetQuantity("Void Aura")}/{quant})");
        }
        Core.CancelRegisteredQuests();
    }

    #endregion

    #region Blades, Hilts & Auras

    public void NSBlade()
    {
        if (Core.CheckInventory("Necrotic Sword's Blade"))
            return;

        Core.Logger("Necrotic Sword's Blade");
        EnergizedBlade();
        BariumOfDoom(1);
        VoidAuras(200);
        Core.BuyItem("shadowfall", 793, "Necrotic Sword's Blade");
    }

    public void NSHilt()
    {
        if (Core.CheckInventory("Necrotic Sword's Hilt"))
            return;

        Core.Logger("Necrotic Sword's Hilt");
        EnergizedHilt();
        BonesVoidRealm(1);
        VoidAuras(200);
        Core.BuyItem("shadowfall", 793, "Necrotic Sword's Hilt");
    }

    public void NSAura()
    {
        if (Core.CheckInventory("Necrotic Sword's Aura"))
            return;

        Core.Logger("Necrotic Sword's Aura");
        EnergizedAura();
        TimeLordNecro(1);
        VoidAuras(300);
        Core.BuyItem("shadowfall", 793, "Necrotic Sword's Aura");
    }

    public void EnergizedBlade()
    {
        if (Core.CheckInventory("Energized Blade"))
            return;

        Core.Logger("Energized Blade");
        FindBlade();
        BariumOfDoom(1);
        VoidAuras(100);
        Core.BuyItem("shadowfall", 793, "Energized Blade");
    }

    public void EnergizedHilt()
    {
        if (Core.CheckInventory("Energized Hilt"))
            return;

        Core.Logger("Energized Hilt");
        FindHilt();
        BonesVoidRealm(1);
        VoidAuras(100);
        Core.BuyItem("shadowfall", 793, "Energized Hilt");
    }

    public void EnergizedAura()
    {
        if (Core.CheckInventory("Energized Aura"))
            return;

        Core.Logger("Energized Aura");
        FindAura();
        TimeLordNecro(1);
        VoidAuras(150);
        Core.BuyItem("shadowfall", 793, "Energized Aura");
    }

    public void FindBlade()
    {
        if (Core.CheckInventory("Unenhanced Doom Blade"))
            return;

        Core.Logger("Unenhanced Doom Blade");
        Core.AddDrop("Unenhanced Doom Blade");
        Core.EnsureAccept(4433);
        BladeEssence(1);
        BariumOfDoom(1);
        VoidAuras(10);
        Core.EnsureComplete(4433);
    }

    public void FindHilt()
    {
        if (Core.CheckInventory("Unenhanced Hilt"))
            return;

        Core.Logger("Unenhanced Hilt");
        Core.AddDrop("Unenhanced Hilt", "Bone Dust");
        Core.EnsureAccept(4434);
        CavernCelestite(800);
        Farm.BattleUnderB("Undead Energy", 5000);
        PrimarchHilt(1);
        BonesVoidRealm(50);
        VoidAuras(10);
        Core.EnsureComplete(4434);
    }

    public void FindAura()
    {
        if (Core.CheckInventory("Unenhanced Aura"))
            return;

        Core.Logger("Unenhanced Aura");
        Adv.GearStore();
        Necro.GetNecromancer(true);
        Adv.GearStore(true);

        Core.AddDrop("Unenhanced Aura");
        Core.EnsureAccept(4436);
        FindBlade();
        FindHilt();
        CHourglass(2);
        ScrollDarkArts(1);
        TimeLordNecro(1);
        VoidAuras(10);
        Core.EnsureComplete(4436);
    }

    #endregion

    #region Crafting Materials

    public void BariumOfDoom(int quant)
    {
        if (Core.CheckInventory("Barium of Doom", quant))
            return;

        Core.CheckInventory("Barium", quant);
        VoidAuras(quant * 50);
        Core.BuyItem("shadowfall", 793, "Barium of Doom");
    }

    private void Barium()
    {
        Core.Unbank("Barium", "Barium of Doom");
        int i = 0;

        string[] Blades = { "Unenhanced Doom Blade", "Energized Blade", "Necrotic Sword's Blade" };
        if (Core.CheckInventory(new[] { "Unenhanced Aura", "Energized Aura", "Necrotic Sword's Aura" }, any: true))
            i++;

        foreach (string Item in Blades)
            if (Core.CheckInventory(Item))
                i = i + Array.IndexOf(Blades, Item) + 1;

        i = i + Bot.Inventory.GetQuantity("Barium") + Bot.Inventory.GetQuantity("Barium of Doom");
        if (i >= 4)
            return;

        BLOD.UnlockMineCrafting();
        Daily.MineCrafting(new[] { "Barium" }, 4 - i);
    }

    public void BonesVoidRealm(int quant)
    {
        if (Core.CheckInventory("Bones from the Void Realm", quant))
            return;

        Core.AddDrop("Undead Energy");
        Farm.BattleUnderB("Bone Dust", quant * 50);
        VoidAuras(quant * 50);
        Core.BuyItem("shadowfall", 793, "Bones from the Void Realm", quant);
    }

    public void TimeLordNecro(int quant)
    {
        if (Core.CheckInventory("Time Lord's Necronomicon", quant))
            return;

        CHourglass(quant * 10);
        ScrollDarkArts(quant);
        VoidAuras(quant * 100);
        Core.BuyItem("shadowfall", 793, "Time Lord's Necronomicon", quant);
    }

    public void CavernCelestite(int quant)
    {
        if (Core.CheckInventory("Cavern Celestite", quant))
            return;

        Core.AddDrop("Cavern Celestite");
        if (!Bot.Quests.IsUnlocked(939))
            BattleUnder.BattleUnderC();
        Core.FarmingLogger("Cavern Celestite", quant);

        Core.RegisterQuests(939);
        while (!Bot.ShouldExit && !Core.CheckInventory("Cavern Celestite", quant))
        {
            Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror Soul", log: false);
            Core.HuntMonster("battleunderb", "Undead Champion", "Undead Champion Soul", log: false);
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Jellyfish Soul", log: false);

            Bot.Wait.ForPickup("Cavern Celestite");
        }
        Core.CancelRegisteredQuests();
    }

    public void PrimarchHilt(int quant)
    {
        if (Core.CheckInventory("Primarch's Hilt", quant))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("bosschallenge", "Colossal Primarch", "Primarch's Hilt", quant, false, publicRoom: true, log: false);
    }

    public void BladeEssence(int quant)
    {
        if (Core.CheckInventory("Blade Essence", quant))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("chaoscrypt", "Chaorrupted Armor", "Blade Essence", quant, false, log: false);
    }

    public void CHourglass(int quant)
    {
        if (Core.CheckInventory("Chaorrupted Hourglass", quant))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false, publicRoom: true, log: false);
        Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chaorrupted Hourglass", quant, false, publicRoom: true, log: false);
    }

    public void ScrollDarkArts(int quant)
    {
        if (Core.CheckInventory("(Necro) Scroll of Dark Arts", quant))
            return;

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", quant, false, publicRoom: true, log: false);
    }

    #endregion
}
