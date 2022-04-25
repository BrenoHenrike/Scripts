//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using RBot;

public class ArchDoomKnight
{
    public ScriptInterface Bot => ScriptInterface.Instance;
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

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        LOC.Wolfwing();
        Farm.EvilREP(7);
        GatheringPower();
        DeathsDoor();
        ChaoticLords();
        AMeansToAnEnd();
    }


    public void GatheringPower()
    {
        if (Core.CheckInventory("Arch DoomKnight Cape"))
            return;

        Core.AddDrop(Q1items);
        Core.EnsureAccept(6795);

        Core.EquipClass(ClassType.Farm);
        Farm.BattleUnderB(item: "Undead Energy", quant: 1800);
        Core.HuntMonster(map: "noxustower", monster: "Lightguard Paladin", item: "Human Souls", quant: 500, isTemp: false);
        Core.HuntMonster(map: "lair", monster: "Water Draconian", item: "Dragon Energy", quant: 600, isTemp: false);
        Core.EnsureComplete(6795);
        Core.ToBank(Q1items);
    }

    public void DeathsDoor()
    {
        if (Core.CheckInventory(new[] { "Arch DoomKnight Cape Sword", "Arch DoomKnight Polearm" }))
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
        if (Core.CheckInventory(new[] { "Arch DoomKnight Sword", "Arch DoomKnight's Edge" }))
            return;

        Core.AddDrop(Q3items);
        Core.EnsureAccept(6797);

        Core.EquipClass(ClassType.Solo);
        Core.KillEscherion(item: "Escherion's Helm", publicRoom: true);
        Core.KillEscherion(item: "Chaotic Power", quant: 13, publicRoom: true);

        Core.HuntMonster(map: "Stalagbite", monster: "Vath", item: "Legendary Sword of Dragon Control", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "Kitsune", monster: "Kitsune", item: "Hanzamune Dragon Koi Blade", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "Wolfwing", monster: "Wolfwing", item: "Wolfwing Armor", isTemp: false);
        Core.HuntMonster(map: "palooza", monster: "Kimberly", item: "One Eyed Doll Breaker", isTemp: false);
        Core.HuntMonster(map: "Ledgermayne", monster: "Ledgermayne", item: "Ledgermayne", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "djinn", monster: "Tibicenas", item: "Tibicenas", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "dreamnexus", monster: "Khasaanda", item: "Soul of Chaos Armor", isTemp: false);
        Core.HuntMonster(map: "stormtemple", monster: "Chaos Lord Lionfang", item: "Chaos Lionfang Armor", isTemp: false);
        Core.HuntMonster(map: "swordhavenfalls", monster: "Chaos Lord Alteon", item: "Shorn Chaos King Crown", isTemp: false, publicRoom: true);
        Core.KillXiang("Xiang Chaos", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "ultradrakath", monster: "Champion of Chaos", item: "Drakath's Sword", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "timespace", monster: "Chaos Lord Iadoa", item: "Chaorrupted Hourglass", isTemp: false);
        Core.EnsureComplete(6797);
        Core.ToBank(Q3items);
    }

    public void AMeansToAnEnd()
    {
        if (Core.CheckInventory(new[] { "Arch DoomKnight", "Arch DoomKnight Open Helm", "Arch DoomKnight Helm" }))
            return;

        Core.AddDrop(Q4items);
        Core.EnsureAccept(6798);

        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster(map: "shadowfallwar", monster: "Skeletal Fire Mage", item: "Ultimate Darkness Gem", quant: 50, isTemp: false);
        Farm.BattleUnderB(item: "Undead Energy", quant: 2000);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster(map: "epicvordred", monster: "Ultra Vordred", item: "(Necro) Scroll of Dark Arts", quant: 2, isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "sepulchurebattle", monster: "Ultra Sepulchure", item: "Doom Heart", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "sepulchure", monster: "Dark Sepulchure", item: "Dread Knight Cleaver", isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "thevoid", monster: "Reaper", item: "Reaper's Soul", quant: 1, isTemp: false, publicRoom: true);
        Core.HuntMonster(map: "Desolich", monster: "Desolich", item: "Desolich's Undead Eye", quant: 2, isTemp: false, publicRoom: true);
        Core.EnsureComplete(6798);
        Core.ToBank(Q4items);
    }

}