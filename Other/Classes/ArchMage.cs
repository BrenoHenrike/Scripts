//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\QueenofMonsters\CoreQOM.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Story\QueenofMonsters\Extra\CelestialArena.cs
//cs_include Scripts/Story\ThroneofDarkness\CoreToD.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Skills;
using Skua.Core.Options;

public class Archmage
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private CoreBLOD BLOD = new();
    private CoreQOM QOM = new();
    private CoreVHL VHL = new();
    private BuyScrolls Scroll = new();
    private CelestialArenaQuests CAQ = new();
    private CoreToD TOD = new();
    private CoreSoW SoW = new();
    public CoreStory Story = new CoreStory();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "Archmage";
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("Lumina Elementi", "Lumina Elementi", "Todo the last quest or not, for the 51% wep(takes awhileand will require aditional boss items.) [On by default]", true),
        new Option<bool>("Cosmetics", "Get Cosmetics", "Gets the cosmetic rewards (redoes quests if you don't have them, disable to just get Archmage and the weapon) [On by default]", true),
        new Option<bool>("Armying?", "Armying?", "use when running on 4 accounts at once only, will probably get out of sync.) [Off by default]", false)
    };

    private string[] RequiredItems = { "Archmage", "Providence", "Mystic Scribing Kit", "Prismatic Ether", "Arcane Locus", "Unbound Tome", "Book of Magus", "Book of Fire", "Book of Ice", "Book of Aether", "Book of Arcana", "Arcane Sigil", "Archmage" };
    private string[] BossDrops = { "Void Essentia", "Vital Exanima", "Everlight Flame", "Calamitous Ruin", "The Mortal Coil", "The Divine Will", "Insatiable Hunger", "Undying Resolve", "Elemental Binding" };
    private string[] Cosmetics = { "Arcane Sigil", "Arcane Floating Sigil", "Sheathed Archmage's Staff", "Archmage's Cowl", "Archmage's Cowl and Locks", "Archmage's Staff", "Archmage's Robes", "Divine Mantle", "Divine Veil", "Divine Veil and Locks", "Prismatic Floating Sigil", "Sheathed Providence", "Prismatic Sigil", "Astral Mantle" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(RequiredItems.Concat(BossDrops).ToArray());

        Core.SetOptions();

        GetAM();
        
        Core.SetOptions(false);
    }

    public void GetAM(bool rankUpClass = true)
    {

        if (Core.CheckInventory("Archmage", toInv: false) && !Bot.Config.Get<bool>("Lumina Elementi") && !Bot.Config.Get<bool>("Cosmetics"))
            Core.Logger("Archmage Owned, Farm Finished.", stopBot: true);
        if (Core.CheckInventory(new[] { "Archmage", "Providence" }, toInv: false) && Bot.Config.Get<bool>("Lumina Elementi") && !Bot.Config.Get<bool>("Cosmetics"))
            Core.Logger("Archmage and Providence Owned, Farm Finished.", stopBot: true);
        if (Bot.Config.Get<bool>("Lumina Elementi") && Bot.Config.Get<bool>("Cosmetics") && Core.CheckInventory("Archmage", toInv: false) && Core.CheckInventory(Cosmetics))
            Core.Logger("Archmage, Providence, and Extras Owned, Farm Finished.", stopBot: true);
        if (Bot.Config.Get<bool>("Armying?"))
            Core.Logger("Armying Set to True, Please have all accounts logged in and Following this Acc using the Tools > Butler.cs");

        Bot.Drops.Add(RequiredItems.Concat(BossDrops).Concat(Cosmetics).ToArray());

        RequiredStuffs();

        if (!Core.CheckInventory("Archmage") && Bot.Config.Get<bool>("Cosmetics"))
        {
            Core.EnsureAccept(8918);
            Core.Logger("Archmage: Cosmetics = true");
            ExtrasCheck();

            Magus();
            Fire(true);
            Ice(true);
            Aether(true);
            Arcana(true);

            Core.Unbank(new[] { "book of Magus", "book of Fire", "book of Ice", "book of Aether", "book of Arcana", "Elemental Binding" });
            Core.EnsureComplete(8918);

            Bot.Wait.ForPickup("Archmage");
            Core.ToBank(Cosmetics);

            if (rankUpClass)
                Adv.rankUpClass("Archmage");
        }

        else if (!Core.CheckInventory("Archmage") && !Bot.Config.Get<bool>("Cosmetics"))
        {
            //Archmage's Ascension 
            Core.EnsureAccept(8918);

            Core.Logger("Archmage: Cosmetics = false");

            Magus();
            Fire();
            Ice();
            Aether();
            Arcana();

            Core.ToBank(Cosmetics);

            Core.Unbank(new[] { "book of Magus", "book of Fire", "book of Ice", "book of Aether", "book of Arcana", "Elemental Binding" });

            Core.EnsureComplete(8918);

            Bot.Wait.ForPickup("Archmage");
            Core.ToBank(Cosmetics);

            if (rankUpClass)
                Adv.rankUpClass("Archmage");
        }

        if (Bot.Config.Get<bool>("Lumina Elementi"))
            LuminaElementi();
    }

    //getExtras:
    void LuminaElementi()
    {
        if (Bot.Config.Get<bool>("Cosmetics") && Core.CheckInventory(new[] { "Providence", "Divine Mantle", "Divine Veil", "Divine Veil and Locks", "Prismatic Floating Sigil", "Sheathed Providence", "Prismatic Sigil", "Astral Mantle" }, toInv: false))
            return;
        else if (Core.CheckInventory("Providence", toInv: false))
            return;

        //Lumina Elementi
        Core.EnsureAccept(8919);
        Core.Logger("Doing Extra Quest for 51% wep.");

        Arcana();
        UnboundTomb(30);

        BossItemCheck("Elemental Binding");

        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", 2000))
            Core.KillMonster("Streamwar", "r3a", "Left", "*", log: false);
        Core.CancelRegisteredQuests();

        Core.FarmingLogger("Unbound Thread", 100);
        Core.RegisterQuests(8869);
        while (!Bot.ShouldExit && !Core.CheckInventory("Unbound Thread", 100))
        {
            //Fallen Branches 8869
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("DeadLines", "Frenzied Mana", "Captured Mana", 8);
            Core.HuntMonster("DeadLines", "Shadowfall Warrior", "Armor Scrap", 8);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("DeadLines", "Eternal Dragon", "Eternal Dragon Scale");
            Bot.Wait.ForPickup("Unbound Thread");
        }
        Core.CancelRegisteredQuests();

        Core.EquipClass(ClassType.Solo);
        Core.EnsureComplete(8919);
        Bot.Wait.ForPickup("Providence");
        Core.Logger("51% wepon [Providence] Obtained.");
    }

    //Books:
    public void Magus()
    {
        //Book of Magus: Incantation
        if (Core.CheckInventory("Book of Magus"))
            return;

        Core.Logger("Book: Book of Magus");
        UnboundTomb(1);
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

    public void Fire(bool Extras = false)
    {
        //Book of Fire: Immolation
        if (Core.CheckInventory("Book of Fire") && !Extras)
            return;

        if (Extras && Core.CheckInventory(new[] { "Book of Fire", "Arcane Floating Sigil", "Sheathed Archmage's Staff" }, toInv: false))
            return;

        Core.Logger("Book of Fire");

        UnboundTomb(1);
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

    public void Ice(bool Extras = false)
    {
        if (Core.CheckInventory("Book of Ice") && !Extras)
            return;

        if (Extras && Core.CheckInventory(new[] { "Book of Ice", "Archmage's Cowl", "Archmage's Cowl and Locks" }, toInv: false))
            return;

        Core.Logger("Book of Ice");

        UnboundTomb(1);
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

    public void Aether(bool Extras = false)
    {
        //Book of Aether: Supernova
        if (Core.CheckInventory("Book of Aether") && !Extras)
            return;

        if (Extras && Core.CheckInventory(new[] { "Book of Aether", "Archmage's Staff" }, toInv: false))
            return;

        BossItemCheck("Void Essentia", "Vital Exanima", "Everlight Flame");

        Core.Logger("Book of Aether");

        UnboundTomb(1);
        Core.EnsureAccept(8916);

        Scroll.BuyScroll(Scrolls.Eclipse, 50);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("streamwar", "Second Speaker", "A Fragment of the Beginning", isTemp: false);
        // Core.HuntMonster("fireavatar", "Avatar Tyndarius", "Everlight Flame", isTemp: false); //1% Drop Rate
        Core.EnsureComplete(8916);
        Bot.Wait.ForPickup("Book of Aether");
        Core.ToBank(Cosmetics);

    }

    public void Arcana(bool Extras = false)
    {
        //Book of Arcana: Arcane Sigil
        if (Core.CheckInventory("Book of Arcana") && !Extras)
            return;

        if (Extras && Core.CheckInventory(new[] { "Book of Arcana", "Archmage's Robes" }, toInv: false))
            return;

        BossItemCheck("The Mortal Coil", "The Divine Will", "Insatiable Hunger", "Undying Resolve", "Calamitous Ruin");

        Bot.Options.AggroMonsters = false; //just incse for the equip.

        if (Core.CheckInventory("Yami No Ronin"))
        {
            Adv.GearStore();
            Core.Join("whitemap"); //till aggro shit gets fixed.
            Adv.BestGear(GearBoost.dmgAll);
            Core.Equip("Yami No Ronin");
            Bot.Skills.StartAdvanced("Yami No Ronin", false, ClassUseMode.Base);
            Core.HuntMonster("tercessuinotlim", "Nulgath", "The Mortal Coil", isTemp: false);
            Adv.GearStore(true);
        }


        Core.Logger("Book of Arcana");

        UnboundTomb(1);
        Core.EnsureAccept(8917);

        Scroll.BuyScroll(Scrolls.EtherealCurse, 50);

        Core.EquipClass(ClassType.Solo);
        Adv.KillUltra("tercessuinotlim", "Boss2", "Right", "Nulgath", "The Mortal Coil", isTemp: false); //just get lucky :4Head:
        Core.EnsureComplete(8917);
        Bot.Wait.ForPickup("Book of Arcana");
        Core.ToBank(Cosmetics);

    }

    //Materials:
    public void MysticScribingKit(int quant = 5)
    {
        if (Core.CheckInventory("Mystic Scribing Kit", quant))
            return;

        Core.FarmingLogger("Mystic Scribing Kit", quant);

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
        if (Core.CheckInventory("73339", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8911))
            PrismaticEther(1);

        Core.FarmingLogger("Arcane Locus", quant);
        Core.EquipClass(ClassType.Farm);

        while (!Bot.ShouldExit && !Core.CheckInventory("Arcane Locus", quant))
        {
            Core.EnsureAccept(8911);
            Core.KillMonster("skytower", "r13", "Bottom", "*", "Sky Locus", isTemp: false, log: false);
            Core.HuntMonster("natatorium", "*", "Sea Locus", isTemp: false, log: false);
            Core.HuntMonster("ectocave", "Ektorax", "Earth Locus", isTemp: false, log: false);
            Core.HuntMonster("drakonnan", "Drakonnan", "Fire Locus", isTemp: false, log: false);
            Core.HuntMonster("elemental", "Mana Golem", "Prime Locus Attunement", 30, isTemp: false, log: false);

            Core.EnsureComplete(8911);
            Bot.Wait.ForPickup("Arcane Locus");
        }
    }

    public void UnboundTomb(int quant)
    {
        if (Core.CheckInventory("Unbound Tome", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8912))
            ArcaneLocus();

        Core.FarmingLogger("Unbound Tome", quant);

        MysticScribingKit(quant - Bot.Inventory.GetQuantity("Unbound Tome"));
        PrismaticEther(quant - Bot.Inventory.GetQuantity("Unbound Tome"));
        ArcaneLocus(quant - Bot.Inventory.GetQuantity("Unbound Tome"));

        while (!Bot.ShouldExit && !Core.CheckInventory("Unbound Tome", quant))
        {
            Core.EnsureAccept(8912);
            if (Bot.Config.Get<bool>("Voucher"))
            {
                // 500k * 2
                Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 500k", 6);
                Adv.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 8845);
            }
            else
            {
                // 100k
                Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 30);
                Adv.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 8844);
            }
            Adv.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
            Adv.BuyItem("shadowfall", 89, "Forsaken Doom Seal");
            Core.EnsureComplete(8912);
            Bot.Wait.ForPickup("Unbound Tome");
        }
    }

    //Required Story & Reputations
    void RequiredStuffs()
    {
        Core.Logger("Completing Quests / Rep Requirements");
        SoW.CompleteCoreSoW();
        QOM.TheReshaper();
        Farm.Experience(100);
        Farm.SpellCraftingREP();
        Farm.EmberseaREP();
        Farm.ChaosREP();
        Farm.GoodREP();
        Farm.EvilREP();
        Farm.EtherStormREP();
        Farm.LoremasterREP();
        Core.Logger("Quests / Rep Requirements, Done.");
    }

    //Boss Items
    void BossItemCheck(params string[] Items)
    {
        Core.Logger("Item Check.");

        foreach (string item in Items)
        {
            switch (item)
            {
                case "Void Essentia":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("voidflibbi", "Flibbitiestgibbet", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Flibbitiestgibbet] in [voidflibbi]", stopBot: true);
                    break;

                case "Vital Exanima":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("Dage", "Dage the Evil", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Dage] in [Dage]", stopBot: true);
                    break;

                case "Everlight Flame":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("Fireavatar", "Avatar Tyndarius", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Tyndarius] in [Fireavatar]", stopBot: true);
                    break;

                case "Calamitous Ruin":
                    if (Bot.Config.Get<bool>("Armying?"))
                    {
                        Bot.Events.RunToArea += DarkCarnaxMove;
                        Core.Logger("You May need to Babysit this one... because of the laser");
                        Adv.KillUltra("DarkCarnax", "Boss", "Right", "Nightmare Carnax", "Calamitous Ruin", isTemp: false);
                        Bot.Events.RunToArea -= DarkCarnaxMove;
                    }
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Nightmare Carnax] in [Darkcarnax]", stopBot: true);
                    break;

                case "The Mortal Coil":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("Tercessuinotlim", "Nulgath", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Nulgath] in [Tercessuinotlim]", stopBot: true);
                    break;

                case "The Divine Will":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("celestialpast", "Azalith", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Azalith] in [celestialpast]", stopBot: true);
                    break;

                case "Insatiable Hunger":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("voidnightbane", "Nightbane", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Nightbane] in [voidnightbane]", stopBot: true);
                    break;

                case "Undying Resolve":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("Theworld", "Encore Darkon", item, isTemp: false);
                    if (!Core.CheckInventory(item))
                        Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Encore Darkon] in [Theworld]", stopBot: true);
                    break;

                case "Elemental Binding":
                    if (Bot.Config.Get<bool>("Armying?"))
                        Core.HuntMonster("Archmage", "Prismata", item, 250, isTemp: false);
                    if (!Core.CheckInventory(item, 250))
                        Core.Logger($"{item} x250 Not Found, Can Be Farmed (with an Army) from [Prismata] in [Archmage]", stopBot: true);
                    break;
            }
        }
    }

    //Cosmetics
    void ExtrasCheck(params string[] Items)
    {
        Core.Logger("Extra Items Check.");

        foreach (string item in Cosmetics)
        {
            if (!Core.CheckInventory(item, toInv: false))
                Core.Logger($"{item} Missing. Bot Will Refarm for it.");
            else Core.Logger($"{item} Found");
        }
    }

    //For Nightmare Carnax
    void DarkCarnaxMove(string zone)
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
}
