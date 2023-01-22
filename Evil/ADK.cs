/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class ArchDoomKnight
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public Core13LoC LOC => new();

    public static string[] Q1items = {
        "Arch DoomKnight Cape",
        "Undead Energy",
        "Human Souls",
        "Dragon Energy"
    };
    public static string[] Q2items = {
        "Arch DoomKnight Cape Sword",
        "Arch DoomKnight Polearm",
        "Death's Power",
        "Souls of the Dead"
    };
    public static string[] Q3items = {
        "Arch DoomKnight Sword",
        "Arch DoomKnight's Edge",
        "Escherion's Helm",
        "Legendary Sword of Dragon Control",
        "Hanzamune Dragon Koi Blade",
        "Wolfwing Armor",
        "One Eyed Doll Breaker",
        "Ledgermayne",
        "Tibicenas",
        "Soul of Chaos Armor",
        "Chaos Lionfang Armor",
        "Shorn Chaos King Crown",
        "Xiang Chaos",
        "Drakath's Sword",
        "Chaorrupted Hourglass",
        "Chaotic Power",
    };
    public static string[] Q4items = {
        "Arch DoomKnight",
        "Arch DoomKnight Open Helm",
        "Arch DoomKnight Helm",
        "Ultimate Darkness Gem",
        "Undead Energy",
        "(Necro) Scroll of Dark Arts",
        "Doom Heart",
        "Dread Knight Cleaver",
        "Reaper's Soul",
        "Desolich's Undead Eye"
    };
    public string[] Combined = Q1items.Concat(Q2items).Concat(Q3items).Concat(Q4items).ToArray();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll(bool BankRewards = false)
    {
        LOC.Wolfwing();
        Farm.EvilREP(7);
        GatheringPower();
        DeathsDoor();
        ChaoticLords();
        AMeansToAnEnd();
        if (BankRewards)
            Core.ToBank(Combined);
    }


    public void GatheringPower()
    {
        if (Core.CheckInventory("Arch DoomKnight Cape", toInv: false))
            return;

        Core.AddDrop(Q1items);
        Core.EnsureAccept(6795);

        Core.EquipClass(ClassType.Farm);
        Farm.BattleUnderB("Undead Energy", 1800);
        Core.HuntMonster("noxustower", "Lightguard Paladin", "Human Souls", 500, isTemp: false);
        Core.HuntMonster("lair", "Water Draconian", "Dragon Energy", 600, isTemp: false);
        Core.EnsureComplete(6795);
        Core.ToBank(Q1items);
    }

    public void DeathsDoor()
    {
        if (Core.CheckInventory(new[] { "Arch DoomKnight Cape Sword", "Arch DoomKnight Polearm" }, toInv: false))
            return;

        Core.AddDrop(Q2items);
        Core.EnsureAccept(6796);

        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Death's Power", 1, false);
        Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Souls of the Dead", 400, false);
        Core.EnsureComplete(6796);
        Core.ToBank(Q2items);
    }

    public void ChaoticLords()
    {
        if (Core.CheckInventory(new[] { "Arch DoomKnight Sword", "Arch DoomKnight's Edge" }, toInv: false))
            return;

        Core.AddDrop(Q3items);
        Core.EnsureAccept(6797);

        Core.EquipClass(ClassType.Solo);
        Core.KillEscherion("Escherion's Helm", publicRoom: true);
        Core.KillEscherion("Chaotic Power", 13, publicRoom: true);

        Core.KillVath("Legendary Sword of Dragon Control", isTemp: false, publicRoom: true);
        Core.HuntMonster("Kitsune", "Kitsune", "Hanzamune Dragon Koi Blade", isTemp: false, publicRoom: true);
        Core.HuntMonster("Wolfwing", "Wolfwing", "Wolfwing Armor", isTemp: false);
        Core.HuntMonster("palooza", "Kimberly", "One Eyed Doll Breaker", isTemp: false);
        Core.HuntMonster("Ledgermayne", "Ledgermayne", "Ledgermayne", isTemp: false, publicRoom: true);
        Core.HuntMonster("djinn", "Tibicenas", "Tibicenas", isTemp: false, publicRoom: true);
        Core.HuntMonster("dreamnexus", "Khasaanda", "Soul of Chaos Armor", isTemp: false);
        Core.HuntMonster("stormtemple", "Chaos Lord Lionfang", "Chaos Lionfang Armor", isTemp: false);
        Core.HuntMonster("swordhavenfalls", "Chaos Lord Alteon", "Shorn Chaos King Crown", isTemp: false, publicRoom: true);
        Core.KillXiang("Xiang Chaos", isTemp: false, publicRoom: true);
        Core.HuntMonster("ultradrakath", "Champion of Chaos", "Drakath's Sword", isTemp: false, publicRoom: true);
        Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chaorrupted Hourglass", isTemp: false);
        Core.EnsureComplete(6797);
        Core.ToBank(Q3items);
    }

    public void AMeansToAnEnd()
    {
        if (Core.CheckInventory(new[] { "Arch DoomKnight", "Arch DoomKnight Open Helm", "Arch DoomKnight Helm" }, toInv: false))
            return;

        Core.AddDrop(Q4items);
        Core.EnsureAccept(6798);

        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("shadowfallwar", "Skeletal Fire Mage", "Ultimate Darkness Gem", 50, isTemp: false);
        Farm.BattleUnderB("Undead Energy", 2000);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", 2, isTemp: false, publicRoom: true);
        Core.HuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", isTemp: false, publicRoom: true);
        Core.HuntMonster("sepulchure", "Dark Sepulchure", "Dread Knight Cleaver", isTemp: false, publicRoom: true);
        Core.HuntMonster("thevoid", "Reaper", "Reaper's Soul", 1, isTemp: false, publicRoom: true);
        Core.HuntMonster("Desolich", "Desolich", "Desolich's Undead Eye", 2, isTemp: false, publicRoom: true);
        Core.EnsureComplete(6798);
        Core.ToBank(Q4items);
    }

}
