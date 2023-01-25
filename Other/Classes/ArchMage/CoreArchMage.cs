/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQOM.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Skills;
using Skua.Core.Options;

public class CoreArchMage
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private BuyScrolls Scroll = new();
    private CoreBLOD BLOD = new();
    private CoreQOM QOM = new();
    private CoreSoW SoW = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "ArchMage";
    public List<IOption> Options = new()
    {
        new Option<bool>("lumina_elementi", "Lumina Elementi", "Todo the last quest or not, for the 51% wep(takes awhileand will require aditional boss items.) [On by default]", true),
        new Option<bool>("cosmetics", "Get Cosmetics", "Gets the cosmetic rewards (redoes quests if you don't have them, disable to just get ArchMage and the weapon) [On by default]", true),
        new Option<bool>("army", "Armying?", "use when running on 4 accounts at once only, will probably get out of sync.) [Off by default]", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(RequiredItems.Concat(BossDrops).ToArray());
        Core.SetOptions();

        GetAM();

        Core.SetOptions(false);
    }

    public void GetAM(bool rankUpClass = true)
    {
        bool cosmetics = Bot.Config.Get<bool>("cosmetics");
        bool lumina = Bot.Config.Get<bool>("lumina_elementi");
        army = Bot.Config.Get<bool>("army");

        if (Core.CheckInventory("ArchMage", toInv: false))
        {
            if (!lumina)
            {
                if (!cosmetics)
                {
                    Core.Logger("You own \"ArchMage\", farm complete.");
                    return;
                }
                else if (Core.CheckInventory(Cosmetics, toInv: false))
                {
                    Core.Logger("You own \"ArchMage\" and the extra cometics, farm complete.");
                    return;
                }
            }
            else if (Core.CheckInventory("Providence", toInv: false))
            {
                if (!cosmetics)
                {
                    Core.Logger("You own \"ArchMage\" and \"Providence\", farm complete.");
                    return;
                }
                else if (Core.CheckInventory(Cosmetics, toInv: false))
                {
                    Core.Logger("You own \"ArchMage\", \"Providence\", and the extra cometics, farm complete.");
                    return;
                }
            }
        }

        if (army)
            Core.Logger("Armying Set to True, Please have all accounts logged in and Following this Acc using the Tools > Butler.cs");
        Bot.Drops.Add(RequiredItems.Concat(BossDrops).Concat(Cosmetics).ToArray());

        Core.Logger("The bot will now farm all requierments for ArchMage");
        SoW.CompleteCoreSoW();
        QOM.TheReshaper();

        Farm.SpellCraftingREP();
        Farm.EmberseaREP();
        Farm.ChaosREP();
        Farm.GoodREP();
        Farm.EvilREP();
        Farm.EtherStormREP();
        Farm.LoremasterREP();

        Farm.Experience(100);

        Core.Logger("Requirements complete");

        if (!Core.CheckInventory("ArchMage"))
        {
            Core.EnsureAccept(8918);
            Core.Logger($"ArchMage: Cosmetics = {cosmetics}");

            BookOfMagus();
            BookOfFire(cosmetics);
            BookOfIce(cosmetics);
            BookOfAether(cosmetics);
            BookOfArcana(cosmetics);

            Core.ToBank(Cosmetics);
            BossItemCheck(250, "Elemental Binding");

            Core.Unbank("Book of Magus", "Book of Fire", "Book of Ice", "Book of Aether", "Book of Arcana", "Elemental Binding");
            Core.EnsureComplete(8918);

            Bot.Wait.ForPickup("ArchMage");
            Core.ToBank(Cosmetics);

            if (rankUpClass)
                Adv.rankUpClass("ArchMage");
        }

        if (lumina)
            LuminaElementi();
    }

    public void LuminaElementi(bool standalone = false)
    {
        if (standalone || Bot.Config.Get<bool>("cosmetics") ?
                Core.CheckInventory(Core.EnsureLoad(8919).Rewards.Select(x => x.ID).ToArray(), toInv: false) :
                Core.CheckInventory("Providence", toInv: false))
            return;

        if (Bot.Quests.IsUnlocked(8919))
            GetAM(false);

        Core.EnsureAccept(8919);
        Core.Logger("Doing the extra quest for the 51% weapon \"Providence\"");

        BookOfArcana();
        UnboundTome(30);
        BossItemCheck(2500, "Elemental Binding");

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", 2000))
            Core.KillMonster("Streamwar", "r3a", "Left", "*", log: false);
        Core.CancelRegisteredQuests();

        Core.FarmingLogger("Unbound Thread", 100);
        //Fallen Branches 8869
        Core.RegisterQuests(8869);
        while (!Bot.ShouldExit && !Core.CheckInventory("Unbound Thread", 100))
        {
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("DeadLines", "Frenzied Mana", "Captured Mana", 8);
            Core.HuntMonster("DeadLines", "Shadowfall Warrior", "Armor Scrap", 8);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("DeadLines", "Eternal Dragon", "Eternal Dragon Scale");
            Bot.Wait.ForPickup("Unbound Thread");
        }
        Core.CancelRegisteredQuests();

        Core.EnsureComplete(8919);
        Bot.Wait.ForPickup("Providence");
        Core.Logger("Weapon obtained: \"Providence\" [51% damage to all]");
    }

    #region Books
    public void BookOfMagus()
    {
        //Book of Magus: Incantation
        if (Core.CheckInventory("Book of Magus"))
            return;

        Core.FarmingLogger("Book of Magus", 1);
        UnboundTome(1);
        Core.EnsureAccept(8913);

        BLOD.FindingFragmentsMace(200);

        Scroll.BuyScroll(Scrolls.Mystify, 50);

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", 250))
            Core.HuntMonster("Streamwar", "Decaying Locust", "Timestream Medal", 5, log: false);
        Core.CancelRegisteredQuests();

        Core.HuntMonster("noxustower", "Lightguard Caster", "Mortal Essence", 100, false);
        Core.HuntMonster("portalmazec", "Pactagonal Knight", "Orthogonal Energy", 150, false);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("timeinn", "Ezrajal", "Celestial Magia", 50, false);

        Core.EnsureComplete(8913);
        Bot.Wait.ForPickup("Book of Magus");
        Core.ToBank(BLOD.BLoDItems);

    }

    public void BookOfFire(bool Extras = false)
    {
        //Book of Fire: Immolation
        if (Extras ?
                Core.CheckInventory(new[] { "Book of Fire", "Arcane Floating Sigil", "Sheathed Archmage's Staff" }, toInv: false) :
                Core.CheckInventory("Book of Fire"))
            return;

        Core.FarmingLogger("Book of Fire", 1);

        UnboundTome(1);
        Core.EnsureAccept(8914);

        Scroll.BuyScroll(Scrolls.FireStorm, 50);

        Core.EquipClass(ClassType.Farm);
        Core.HuntMonster("fireavatar", "Shadefire Cavalry", "ShadowFire Wisps", 200, false);
        Core.HuntMonster("fotia", "Femme Cult Worshiper", "Spark of Life", 200, false);
        Core.HuntMonster("mafic", "*", "Emblazoned Basalt", 200, false);

        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("underlair", "r6", "left", "Void Draconian", "Dense Dragon Crystal", 200, false);

        Core.EnsureComplete(8914);
        Bot.Wait.ForPickup("Book of Fire");
        Core.ToBank(Cosmetics);
    }

    public void BookOfIce(bool Extras = false)
    {
        if (Extras ?
                Core.CheckInventory(new[] { "Book of Ice", "Archmage's Cowl", "Archmage's Cowl and Locks" }, toInv: false) :
                Core.CheckInventory("Book of Ice"))
            return;

        Core.FarmingLogger("Book of Ice", 1);

        UnboundTome(1);
        Core.EnsureAccept(8915);

        Scroll.BuyScroll(Scrolls.Frostbite, 50);

        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && !Core.CheckInventory("Ice Diamond", 100))
        {
            Core.EnsureAccept(7279);
            Core.HuntMonster("kingcoal", "Snow Golem", "Frozen Coal", 10, log: false);
            Core.EnsureComplete(7279);
            Bot.Wait.ForPickup("Ice Diamond");
        }
        Core.HuntMonster("icepike", "Chained Kezeroth", "Rimeblossom", 100, false);
        Core.HuntMonster("icepike", "Karok the Fallen", "Starlit Frost", 100, false);
        Core.HuntMonster("icedungeon", "Shade of Kyanos", "Temporal Floe", 100, false);

        Core.EnsureComplete(8915);
        Bot.Wait.ForPickup("Book of Ice");
        Core.ToBank(Cosmetics);

    }

    public void BookOfAether(bool Extras = false)
    {
        if (Extras ?
                Core.CheckInventory(new[] { "Book of Aether", "Archmage's Staff" }, toInv: false) :
                Core.CheckInventory("Book of Aether"))
            return;

        Core.FarmingLogger("Book of Aether", 1);

        BossItemCheck(1, "Void Essentia", "Vital Exanima", "Everlight Flame");

        UnboundTome(1);
        Core.EnsureAccept(8916);

        Scroll.BuyScroll(Scrolls.Eclipse, 50);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("streamwar", "Second Speaker", "A Fragment of the Beginning", isTemp: false);
        // Core.HuntMonster("fireavatar", "Avatar Tyndarius", "Everlight Flame", isTemp: false); //1% Drop Rate
        Core.EnsureComplete(8916);
        Bot.Wait.ForPickup("Book of Aether");
        Core.ToBank(Cosmetics);

    }

    public void BookOfArcana(bool Extras = false)
    {
        if (Extras ?
                Core.CheckInventory(new[] { "Book of Arcana", "Archmage's Robes" }, toInv: false) :
                Core.CheckInventory("Book of Arcana") && !Extras)
            return;

        Core.FarmingLogger("Book of Arcana", 1);

        BossItemCheck(1, "The Mortal Coil", "The Divine Will", "Insatiable Hunger", "Undying Resolve", "Calamitous Ruin");

        UnboundTome(1);
        Core.EnsureAccept(8917);

        Scroll.BuyScroll(Scrolls.EtherealCurse, 50);

        Core.EnsureComplete(8917);
        Bot.Wait.ForPickup("Book of Arcana");
        Core.ToBank(Cosmetics);
    }

    #endregion

    #region Materials
    public void MysticScribingKit(int quant = 5)
    {
        if (Core.CheckInventory("Mystic Scribing Kit", quant))
            return;

        Core.FarmingLogger("Mystic Scribing Kit", quant);
        Core.AddDrop("Mystic Scribing Kit");

        while (!Bot.ShouldExit && !Core.CheckInventory("Mystic Scribing Kit", quant))
        {
            Core.EnsureAccept(8909);

            Core.EquipClass(ClassType.Farm);
            Core.FarmingLogger("Mystic Quills", 49);
            Core.FarmingLogger("Mystic Shards", 49);

            Core.RegisterQuests(3050);
            while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Mystic Shards", "Mystic Quills" }, 49))
            {
                Core.KillMonster("gilead", "r3", "Left", "Water Elemental", "Water Core", log: false);
                Core.KillMonster("gilead", "r4", "Left", "Fire Elemental", "Fire Core", log: false);
                Core.KillMonster("gilead", "r4", "Left", "Wind Elemental", "Air Core", log: false);
                Core.KillMonster("gilead", "r3", "Left", "Earth Elemental", "Earth Core", log: false);
                Core.KillMonster("gilead", "r8", "Left", "Mana Elemental", "Mana Core", log: false);
            }
            Core.CancelRegisteredQuests();

            Core.EquipClass(ClassType.Solo);
            if (!Core.CheckInventory("Semiramis Feather"))
            {
                Core.AddDrop("Semiramis Feather");
                Core.EnsureAccept(6286);
                Core.HuntMonster("guardiantree", "Terrane", "Terrane Defeated");
                Core.EnsureComplete(6286);
                Bot.Wait.ForPickup("Semiramis Feather");
            }
            Core.HuntMonster("deepchaos", "Kathool", "Mystic Ink", isTemp: false);

            Core.EnsureComplete(8909);
            Bot.Wait.ForPickup("Mystic Scribing Kit");
        }
    }

    public void PrismaticEther(int quant = 1)
    {
        if (Core.CheckInventory("Prismatic Ether", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8910))
            MysticScribingKit(1);

        Core.FarmingLogger("Prismatic Ether", quant);
        Core.AddDrop("Prismatic Ether");
        Core.EquipClass(ClassType.Solo);
        Bot.Quests.UpdateQuest(6042);

        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Ether", quant))
        {
            Core.EnsureAccept(8910);
            Core.HuntMonster("celestialarenad", "Aranx", "Celestial Ether", isTemp: false, log: false);
            Core.HuntMonster("eternalchaos", "Eternal Drakath", "Chaotic Ether", isTemp: false, log: false);
            Core.HuntMonster("shadowattack", "Death", "Mortal Ether", isTemp: false, log: false);
            Core.HuntMonster("gaiazor", "Gaiazor", "Vital Ether", isTemp: false, log: false);
            Core.HuntMonster("fiendshard", "Nulgath's Fiend Shard", "Infernal Ether", isTemp: false, log: false);

            Core.EnsureComplete(8910);
            Bot.Wait.ForPickup("Prismatic Ether");
        }
    }

    public void ArcaneLocus(int quant = 1)
    {
        if (Core.CheckInventory(73339, quant))
            return;

        if (!Bot.Quests.IsUnlocked(8911))
            PrismaticEther(1);

        Core.FarmingLogger("Arcane Locus", quant);
        Core.AddDrop(73339);
        Core.EquipClass(ClassType.Farm);

        while (!Bot.ShouldExit && !Core.CheckInventory(73339, quant))
        {
            Core.EnsureAccept(8911);
            Core.KillMonster("skytower", "r13", "Bottom", "*", "Sky Locus", isTemp: false, log: false);
            Core.HuntMonster("natatorium", "*", "Sea Locus", isTemp: false, log: false);
            Core.HuntMonster("ectocave", "Ektorax", "Earth Locus", isTemp: false, log: false);
            Core.HuntMonster("drakonnan", "Drakonnan", "Fire Locus", isTemp: false, log: false);
            Core.HuntMonster("elemental", "Mana Golem", "Prime Locus Attunement", 30, isTemp: false, log: false);

            Core.EnsureComplete(8911);
            Bot.Wait.ForPickup(73339);
        }
    }

    public void UnboundTome(int quant)
    {
        if (Core.CheckInventory("Unbound Tome", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8912))
            ArcaneLocus();

        Core.FarmingLogger("Unbound Tome", quant);
        Core.AddDrop("Unbound Tome");

        MysticScribingKit(quant - Bot.Inventory.GetQuantity("Unbound Tome"));
        PrismaticEther(quant - Bot.Inventory.GetQuantity("Unbound Tome"));
        ArcaneLocus(quant - Bot.Inventory.GetQuantity("Unbound Tome"));

        while (!Bot.ShouldExit && !Core.CheckInventory("Unbound Tome", quant))
        {
            Core.EnsureAccept(8912);
            Adv.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 8845);
            Adv.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
            Adv.BuyItem("shadowfall", 89, "Forsaken Doom Seal");

            Core.EnsureComplete(8912);
            Bot.Wait.ForPickup("Unbound Tome");
        }
    }

    #endregion

    private void BossItemCheck(int quant = 1, params string[] Items)
    {
        foreach (string item in Items)
        {
            if (Core.CheckInventory(item))
                continue;

            switch (item)
            {
                case "Void Essentia":
                    Item("voidflibbi", "Flibbitiestgibbet", item, quant);
                    break;

                case "Vital Exanima":
                    if (Core.CheckInventory("Yami No Ronin") || Core.CheckInventory("Void Highlord") || Core.CheckInventory("Void HighLord (IoDA)"))
                    {
                        if (Core.CheckInventory("Yami No Ronin"))
                            Bot.Skills.StartAdvanced("Yami no Ronin", true, ClassUseMode.Def);
                        else Bot.Skills.StartAdvanced(Core.CheckInventory("Void Highlord") ? "Void Highlord" : "Void HighLord (IoDA)", true, ClassUseMode.Def);
                        Adv.KillUltra("dage", "Boss", "Right", "Dage the Evil", item, isTemp: false);
                    }
                    else Item("dage", "Dage the Evil", item, quant);
                    break;

                case "Everlight Flame":
                    if (Core.CheckInventory("Void Highlord") || Core.CheckInventory("Void HighLord (IoDA)"))
                    {
                        Bot.Skills.StartAdvanced(Core.CheckInventory("Void Highlord") ? "Void Highlord" : "Void HighLord (IoDA)", true, ClassUseMode.Def);
                        Adv.KillUltra("fireavatar", "r9", "Left", "Avatar Tyndarius", item, isTemp: false);
                    }
                    else Item("fireavatar", "Avatar Tyndarius", item, quant);
                    break;

                case "Calamitous Ruin":
                    if (army)
                    {
                        Bot.Events.RunToArea += DarkCarnaxMove;
                        Core.Logger("You might need to babysit this one due to the laser");
                        Adv.KillUltra("darkcarnax", "Boss", "Right", "Nightmare Carnax", "Calamitous Ruin", isTemp: false);
                        Bot.Events.RunToArea -= DarkCarnaxMove;
                    }
                    else Item("tercessuinotlim", "Nulgath", item, quant);
                    break;

                case "The Mortal Coil":
                    if (Core.CheckInventory("Yami No Ronin"))
                    {
                        Core.Logger("This may Take a few trys to kill it but it'll work Trust the Potato.");
                        Bot.Skills.StartAdvanced("Yami No Ronin", true, ClassUseMode.Def);
                        Adv.KillUltra("tercessuinotlim", "Boss2", "Right", "Nulgath", item, isTemp: false);
                    }
                    else Item("tercessuinotlim", "Nulgath", item, quant);
                    break;

                case "The Divine Will":
                    Item("celestialpast", "Azalith", item, quant);
                    break;

                case "Insatiable Hunger":
                    Item("voidnightbane", "Nightbane", item, quant);
                    break;

                case "Undying Resolve":
                    Bot.Quests.UpdateQuest(8732);
                    Item("theworld", "Encore Darkon", item, quant);
                    break;

                case "Elemental Binding":
                    Item("archmage", "Prismata", item, quant);
                    break;
            }
        }

        void Item(string map, string monster, string item, int quant)
        {
            if (army)
                Core.HuntMonster(map, monster, item, quant, isTemp: false);
            else if (!Core.CheckInventory(item, quant))
                Core.Logger($"{item} x{quant} not found, it can be farmed (with an army) from \"{monster}\" in /{map.ToLower()}", stopBot: true);
        }
    }

    //For Nightmare Carnax
    private void DarkCarnaxMove(string zone)
    {
        switch (zone.ToLower())
        {
            case "a":
                //Move to the right
                Bot.Player.WalkTo(Bot.Random.Next(600, 930), Bot.Random.Next(380, 475));
                Bot.Sleep(2500);
                break;
            case "b":
                //Move to the left
                Bot.Player.WalkTo(Bot.Random.Next(25, 325), Bot.Random.Next(380, 475));
                Bot.Sleep(2500);
                break;
            default:
                //Move to the center
                Bot.Player.WalkTo(Bot.Random.Next(325, 600), Bot.Random.Next(380, 475));
                Bot.Sleep(2500);
                break;
        }
    }

    private string[] RequiredItems = {
        "ArchMage",
        "Providence",
        "Mystic Scribing Kit",
        "Prismatic Ether",
        "Arcane Locus",
        "Unbound Tome",
        "Book of Magus",
        "Book of Fire",
        "Book of Ice",
        "Book of Aether",
        "Book of Arcana",
        "Arcane Sigil",
        "Archmage"
    };
    private string[] BossDrops = {
        "Void Essentia",
        "Vital Exanima",
        "Everlight Flame",
        "Calamitous Ruin",
        "The Mortal Coil",
        "The Divine Will",
        "Insatiable Hunger",
        "Undying Resolve",
        "Elemental Binding"
    };
    private string[] Cosmetics = {
        "Arcane Sigil",
        "Arcane Floating Sigil",
        "Sheathed Archmage's Staff",
        "Archmage's Cowl",
        "Archmage's Cowl and Locks",
        "Archmage's Staff",
        "Archmage's Robes",
        "Divine Mantle",
        "Divine Veil",
        "Divine Veil and Locks",
        "Prismatic Floating Sigil",
        "Sheathed Providence",
        "Prismatic Sigil",
        "Astral Mantle"
    };
    private bool army = false;


}
