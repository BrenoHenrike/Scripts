//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class NecroticSwordOfDoom
{
    // [Can Change]
    // True = Farms each boss for 100 essence in "Retreive Void Auras"
    // False = Farms each boss for 20 essence in "Retreive Void Auras"
    // Recommended: true
    private bool MaxStack = true;
    // [Can Change]
    // True = Bot will try to keep your inventory as empty as possible by farming each individual piece one by one when they are needed in the merge shop.
    // False = Bot will farm all items first and then merge them all. Best used if you want overview of how far you are. And will also be faster because less /join's
    // Recommended: false
    private bool OptimizeInv = true;
    private int EssenceQuantity;

    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreDailies Daily = new();
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();
    public CoreSDKA SDKA = new CoreSDKA();
    public Necromancer Necro = new Necromancer();
    public BattleUnder BattleUnder = new BattleUnder();
    public CoreAdvanced Adv = new CoreAdvanced();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Astral Ephemerite Essence", "Belrot the Fiend Essence", "Black Knight Essence", "Tiger Leech Essence", "Carnax Essence", "Chaos Vordred Essence", "Dai Tengu Essence", "Unending Avatar Essence", "Void Dragon Essence", "Creature Creation Essense", "Void Aura" });

        Core.SetOptions();

        GetNSOD();

        Core.SetOptions(false);
    }

    public void GetNSOD()
    {
        if (Core.CheckInventory("Necrotic Sword of Doom"))
            return;

        if (Core.CBO_Active)
            OptimizeInv = !Core.CBOBool("NSOD_PreFarm");

        Story.PreLoad();

        Barium();
        if (!OptimizeInv)
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
            Core.HuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", isTemp: false, publicRoom: true);
        }
        NSBlade();
        NSHilt();
        NSAura();
        Core.HuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", isTemp: false, publicRoom: true);
        VoidAuras(800);
        Core.BuyItem("shadowfall", 793, "Necrotic Sword of Doom");
        Adv.EnhanceItem("Necrotic Sword of Doom", EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);
        NSODBadge();
    }

    public void VoidAuras(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        CommandingShadowEssences(Quantity);
        RetrieveVoidAuras(Quantity);
    }

    public void CommandingShadowEssences(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;
        if (!Core.CheckInventory("Sepulchure's DoomKnight Armor") && Core.IsMember)
            SDKA.DoAll();
        if (!Core.CheckInventory("Sepulchure's DoomKnight Armor") && !Core.IsMember)
            return;
        Core.AddDrop("Void Aura", "Empowered Essence", "Malignant Essence");
        Core.Logger($"Gathering {Quantity} Void Aura's with SDKA Method");

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

    public void RetrieveVoidAuras(int Quantity)
    {
        if (Core.CheckInventory("Void Aura", Quantity))
            return;

        if (Core.CBO_Active)
            MaxStack = Core.CBOBool("NSOD_MaxStack");

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
        Core.Logger($"Gathering {Quantity} Void Aura's with Non-SDKA Method");

        while (!Core.CheckInventory("Void Aura", Quantity))
        {
            Core.EnsureAccept(4432);
            Core.HuntMonster("timespace", "Astral Ephemerite", "Astral Ephemerite Essence", EssenceQuantity, false);
            Core.HuntMonster("citadel", "Belrot the Fiend", "Belrot the Fiend Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("greenguardwest", "BKWest15", "Down", "Black Knight", "Black Knight Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("mudluk", "Boss", "Down", "Tiger Leech", "Tiger Leech Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("necrocavern", "r16", "Down", "Chaos Vordred", "Chaos Vordred Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("hachiko", "Dai Tengu", "Dai Tengu Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("timevoid", "Unending Avatar", "Unending Avatar Essence", EssenceQuantity, false, publicRoom: true);
            Core.HuntMonster("dragonchallenge", "Void Dragon", "Void Dragon Essence", EssenceQuantity, false, publicRoom: true);
            Core.KillMonster("maul", "r3", "Down", "Creature Creation", "Creature Creation Essence", EssenceQuantity, false, publicRoom: true);
            for (int i = 20; i <= EssenceQuantity; i = i + 20)
                Core.ChainComplete(4432);
            Bot.Wait.ForPickup("Void Aura");
        }
    }

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
        Farm.BattleUnderB("Undead Energy", 800);
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
        Necro.GetNecromancer();
        Adv.GearStore(true);

        Core.AddDrop("Unenhanced Aura");
        Core.EnsureAccept(4436);
        FindBlade();
        FindHilt();
        CHourglass(1);
        ScrollDarkArts(1);
        TimeLordNecro(1);
        VoidAuras(10);
        Core.EnsureComplete(4436);
    }

    public void BariumOfDoom(int Quantity)
    {
        if (Core.CheckInventory("Barium of Doom", Quantity))
            return;

        Core.CheckInventory("Barium", Quantity);
        VoidAuras(Quantity * 50);
        Core.BuyItem("shadowfall", 793, "Barium of Doom");
    }

    public void BonesVoidRealm(int Quantity)
    {
        if (Core.CheckInventory("Bones of the Void Realm", Quantity))
            return;

        Core.AddDrop("Undead Energy");
        Farm.BattleUnderB("Bone Dust", Quantity * 50);
        VoidAuras(Quantity * 50);
        Core.BuyItem("shadowfall", 793, "Bones from the Void Realm", Quantity);
    }

    public void TimeLordNecro(int Quantity)
    {
        if (Core.CheckInventory("Time Lord's Necronomicon", Quantity))
            return;

        CHourglass(Quantity * 10);
        ScrollDarkArts(Quantity);
        VoidAuras(Quantity * 100);
        Core.BuyItem("shadowfall", 793, "Time Lord's Necronomicon", Quantity);
    }

    public void CavernCelestite(int Quantity)
    {
        if (Core.CheckInventory("Cavern Celestite", Quantity))
            return;

        Core.AddDrop("Cavern Celestite");
        if (!Bot.Quests.IsUnlocked(939))
            BattleUnder.BattleUnderC();
        Core.Logger("Cavern Celestite");
        int i = 1;
        while (!Core.CheckInventory("Cavern Celestite", Quantity))
        {
            Core.EnsureAccept(939);
            Core.HuntMonster("battleundera", "Bone Terror", "Bone Terror Soul");
            Core.HuntMonster("battleunderb", "Undead Champion", "Undead Champion Soul");
            Core.HuntMonster("battleunderc", "Crystalized Jellyfish", "Jellyfish Soul");
            Core.EnsureComplete(939);
            Bot.Wait.ForPickup("Cavern Celestite");
            Core.Logger($"Completed {i++}x");
        }
        Core.Logger($"Farmed {Quantity} Cavern Celestite");
    }

    public void PrimarchHilt(int Quantity)
    {
        if (Core.CheckInventory("Primarch's Hilt", Quantity))
            return;

        Core.AddDrop("Primarch's Hilt");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("bosschallenge", "Colossal Primarch", "Primarch's Hilt", Quantity, false, publicRoom: true);
    }

    public void BladeEssence(int Quantity)
    {
        if (Core.CheckInventory("Blade Essence", Quantity))
            return;

        Core.AddDrop("Blade Essence");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("chaoscrypt", "Chaorrupted Armor", "Blade Essence", Quantity, false);
    }

    public void CHourglass(int Quantity)
    {
        if (Core.CheckInventory("Chaorrupted Hourglass", Quantity))
            return;

        Core.AddDrop("Chaorrupted Hourglass", "Dragonoid of Hours");
        Core.EquipClass(ClassType.Solo);
        if (!Core.CheckInventory("Dragonoid of Hours"))
            Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false, publicRoom: true);
        Core.HuntMonster("timespace", "Chaos Lord Iadoa", "Chaorrupted Hourglass", Quantity, false, publicRoom: true);
    }

    public void ScrollDarkArts(int Quantity)
    {
        if (Core.CheckInventory("(Necro) Scroll of Dark Arts", Quantity))
            return;

        Core.AddDrop("(Necro) Scroll of Dark Arts");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", Quantity, false, publicRoom: true);
    }

    private void Barium()
    {
        Core.CheckInventory("Barium");
        Core.CheckInventory("Barium of Doom");
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

    public void NSODBadge()
    {
        Core.Logger("Getting The NSOD Char. Page Badge");
        Core.EnsureAccept(7652);
        Core.HuntMonster("graveyard", "Skeletal Warrior", "Arcane Parchment");
        Core.EnsureComplete(7652);
        Core.Relogin();
    }
}