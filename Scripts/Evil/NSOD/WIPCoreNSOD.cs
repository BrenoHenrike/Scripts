using RBot;

public class CoreNSOD
{
    // [Can Change]
    // True = Farms each boss for 100 essence in "Retreive Void Auras"
    // False = Farms each boss for 20 essence in "Retreive Void Auras"
    public bool MaxStack = true;
    private int EssenceQuantity;

    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailys Daily = new CoreDailys();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreSDKA SDKA = new CoreSDKA();

    public void VoidAuras(int Quantity = 7500)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        CommandingShadowEssences(Quantity);
        RetrieveVoidAuras(Quantity);
    }

    public void CommandingShadowEssences(int Quantity = 7500)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;
        if (!Core.CheckInventory("Sepulchure's DoomKnight Armor") && Core.IsMember)
            SDKA.DoAll();
        Core.AddDrop("Void Aura", "Empowered Essence", "Malignant Essence");

        while (!Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4439);
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Empowered Essence", 50, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowrealmpast", "Shadow Lord", "Malignant Essence", 3, false, publicRoom: true);
            Core.EnsureComplete(4439);
        }
    }

    public void RetrieveVoidAuras(int Quantity = 7500)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        if (MaxStack)
            EssenceQuantity = 100;
        else EssenceQuantity = 20;
        Farm.EvilREP();
        Core.EquipClass(ClassType.Solo);
        Core.AddDrop(
            "Void Aura",
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
        );
        if (!Core.CheckInventory("Necromancer", toInv: false) && !Core.CheckInventory("Creature Shard", toInv: false))
            Core.AddDrop("Creature Shard");

        while (!Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4432);
            Core.HuntMonster("timespace", "Astral Ephemerite", "Astral Ephemerite Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("citadel", "Belrot the Fiend", "Belrot the Fiend Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("greenguardwest", "BKWest15", "Down", "Black Knight", "Black Knight Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("mudluk", "Boss", "Down", "Tiger Leech", "Tiger Leech Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("necrocavern", "r16", "Down", "Chaos Vordred", "Chaos Vordred Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("hachiko", "Dai Tengu", "Dai Tengu Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("timevoid", "Unending Avatar", "Unending Avatar Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("dragonchallenge", "Void Dragon", "Void Dragon Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("maul", "r3", "Down", "Creature Creation", "Creature Creation Essence", EssenceQuantity, false, publicRoom: true);
            for (int i = 0; i <= EssenceQuantity; i = i + 20)
                Core.ChainComplete(4432);
            Bot.Wait.ForPickup("Void Aura");
        }
    }
}