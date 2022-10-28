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

    public bool DontPreconfigure = true;
    public string OptionsStorage = "Archmage";
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("Extras?", "Get Extras?", "Get teh Extra items from the quests", false)
    };

    private string[] RequiredItems = { "Archmage", "Mystic Scribing Kit", "Prismatic Ether", "Arcane Locus", "Unbound Tome", "Book of Magus", "Book of Fire", "Book of Ice", "Book of Aether", "Book of Arcana", "Arcane Sigil", "Archmage" };
    private string[] BossDrops = { "Void Essentia", "Vital Exanima", "Everlight Flame", "Calamitous Ruin", "The Mortal Coil", "The Divine Will", "Insatiable Hunger", "Undying Resolve" };
    private string[] Extras = { "Providence", "Arcane Sigil", "Arcane Floating Sigil", "Sheathed Archmage's Staff", "Archmage's Cowl", "Archmage's Cowl and Locks", "Archmage's Staff", "Archmage's Robes", "Divine Mantle", "Divine Veil", "Divine Veil and Locks", "Prismatic Floating Sigil", "Sheathed Providence", "Prismatic Sigil", "Astral Mantle" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(RequiredItems);
        Core.BankingBlackList.AddRange(BLOD.BLoDItems);
        Core.BankingBlackList.Add("Ice Diamond");
        Core.SetOptions();

        GetAM();

        Core.SetOptions(false);
    }

    public void GetAM(bool rankUpClass = true, bool getExtras = false)
    {
        getExtras = Bot.Config.Get<bool>("Extras?") ? true : false;

        if (Core.CheckInventory("Archmage") && !getExtras)
        {
            Core.Logger("Extras not selected, Archmage Owned, Farm Finished.");
            return;
        }

        if (Core.CheckInventory("Archmage") && getExtras && Core.CheckInventory(Extras, toInv: false))
        {
            Core.Logger("Archmage owned, Extras Owned, Farm Finished.");
            return;
        }

        Core.AddDrop(RequiredItems.Concat(BossDrops).Concat(Extras).ToArray());

        RequiredStuffs();
        ItemCheck();

        if (!Core.CheckInventory("Archmage"))
        {
            //Archmage's Ascension 
            Magus();
            Fire();
            Ice();
            Aether();
            Arcana();

            Core.EquipClass(ClassType.Solo);
            Core.Logger("You May Need To Army These -- just warning you.");
            Core.HuntMonster("Archmage", "Prismata", "Elemental Binding", 250, false, publicRoom: true);
            Core.EnsureComplete(8918);

            Bot.Wait.ForPickup("Archmage");
            Core.ToBank(Extras);
        }
        if (rankUpClass)
            Adv.rankUpClass("Archmage");
        LuminaElementi();
    }

    //getExtras:
    void LuminaElementi()
    {
        Core.Logger("Extra Quest for 51% wep.");
        Core.EnsureAccept(8919);
        //Lumina Elementi
        UnboundTomb(30);
        Arcana();
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(8814, 8815);
        while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", 250))
            Core.HuntMonster("Streamwar", "Decaying Locust", "Timestream Medal", 5, log: false);
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
        Core.HuntMonster("Archmage", "Prismata", "Elemental Binding", 2500, false, publicRoom: true);
        Core.EnsureComplete(8919);
        Bot.Wait.ForPickup("Providence");
        Core.Logger("51% wepon Obtained.");
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
        Core.ToBank(Extras);
        Core.ToBank(BLOD.BLoDItems);

    }

    public void Fire()
    {
        //Book of Fire: Immolation
        if (Core.CheckInventory("Book of Fire"))
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
        Core.ToBank(Extras);

    }

    public void Ice()
    {
        if (Core.CheckInventory("Book of Ice"))
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
        Core.ToBank(Extras);

    }

    public void Aether()
    {
        //Book of Aether: Supernova
        if (Core.CheckInventory("Book of Aether"))
            return;

        Core.Logger("Book of Aether");

        UnboundTomb(1);
        Core.EnsureAccept(8916);

        Scroll.BuyScroll(Scrolls.Eclipse, 50);

        //these are hard bosses anyways
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("streamwar", "Second Speaker", "A Fragment of the Beginning", isTemp: false);
        Core.HuntMonster("fireavatar", "Avatar Tyndarius", "Everlight Flame", isTemp: false);

        // Core.Unbank("Void Essentia", "A Fragment of the Beginning", "Vital Exanima", "Everlight Flame");

        // //Army Bosses.  
        // if (!Core.CheckInventory(new[] { "Void Essentia", "A Fragment of the Beginning", "Vital Exanima", "Everlight Flame" }))
        //     Core.Logger("for the Following items You will Need to either public army them, sorry that we can't help *yet*" +
        //                 "Dage the Evil - dage - Vital Examina" +
        //                 "Flibbitiestgibbet thevoid - Void Essentia", stopBot: true);

        Core.EnsureComplete(8916);
        Bot.Wait.ForPickup("Book of Aether");
        Core.ToBank(Extras);

    }

    public void Arcana()
    {
        //Book of Arcana: Arcane Sigil
        if (Core.CheckInventory("Book of Arcana"))
            return;

        Core.Logger("Book of Arcana");

        UnboundTomb(1);
        Core.EnsureAccept(8917);

        Scroll.BuyScroll(Scrolls.EtherealCurse, 50);

        Core.EquipClass(ClassType.Solo);
        Adv.KillUltra("tercessuinotlim", "Boss2", "Right", "Nulgath", "The Mortal Coil", isTemp: false);

        // Core.Unbank("Calamitous Ruin", "The Mortal Coi", "The Divine Will", "Insatiable Hunger", "Undying Resolve");
        // // Army Bosses:
        // if (!Core.CheckInventory(new[] { "Calamitous Ruin", "The Mortal Coi", "The Divine Will", "Insatiable Hunger", "Undying Resolve" }))
        //     Core.Logger("for the Following items You will Need to either public army them, sorry that we can't help *yet*" +
        //                 "azalith - celestialpast - The Divine Will" +
        //                 "nightbane thevoid - insatiable hunger" +
        //                 "darkon - theworld - Undying resolve", stopBot: true);

        Core.EnsureComplete(8917);
        Bot.Wait.ForPickup("Book of Arcana");
        Core.ToBank(Extras);

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
            Core.RegisterQuests(3048);
            while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Mystic Quills", "Mystic Shards" }, 49))
                Core.KillMonster("castleundead", "Enter", "Spawn", "*", log: false);
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
            Core.KillMonster("skytower", "r3", "Bottom", "*", "Sky Locus", isTemp: false, log: false);
            Core.HuntMonster("natatorium", "*", "Sea Locus", isTemp: false, log: false);
            Core.HuntMonster("downward", "Crystal Mana Construct", "Earth Locus", isTemp: false, log: false);
            Core.HuntMonster("volcano", "Magman", "Fire Locus", isTemp: false, log: false);
            Core.HuntMonster("elemental", "Mana Golem", "Prime Locus", isTemp: false, log: false);

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

        Core.FarmingLogger("Arcane Locus", quant);

        MysticScribingKit(quant);
        PrismaticEther(quant);
        ArcaneLocus(quant);

        while (!Core.CheckInventory("Unbound Tome", quant))
        {
            Core.EnsureAccept(8912);
            // Farm.Gold(3000000);
            Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 30);
            Core.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 8844);
            Adv.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
            Adv.BuyItem("shadowfall", 89, "Forsaken Doom Seal");

            Core.EnsureComplete(8912);
            Bot.Wait.ForPickup("Unbound Tome");
        }
    }

    //Required Stuffs
    void RequiredStuffs()
    {
        Core.Logger("Completing Quests / Rep Requirements");
        SoW.CompleteCoreSoW();
        QOM.TheReshaper();
        Farm.Experience(60);
        Farm.SpellCraftingREP();
        Farm.EmberseaREP();
        Farm.ChaosREP(10);
        Farm.GoodREP(10);
        Farm.EvilREP(10);
        Farm.EtherStormREP();
        Farm.LoremasterREP();
        TOD.CompleteToD();
        Core.Logger("Done.");
    }

    //Item Check
    void ItemCheck(params string[] Items)
    {
        Core.Logger("Item Check.");
        if (Items == null)
            Items = BossDrops;

        if (!Core.CheckInventory(Items))
        {
            foreach (string item in Items)
            {
                switch (item)
                {
                    case "Void Essentia":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Flibbitiestgibbet] in [Thevoid]");
                        break;

                    case "Vital Exanima":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Dage] in [Dage]");
                        break;

                    case "Everlight Flame":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Tyndarius] in [Fireavatar]");
                        break;

                    case "Calamitous Ruin":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Nightmare Carnax] in [Darkcarnax]");
                        break;

                    case "The Mortal Coil":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Nulgath] in [Tercessuinotlim]");
                        break;

                    case "The Divine Will":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Azalith] in [celestialpast]");
                        break;

                    case "Insatiable Hunger":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Nightbane] in [Thevoid]");
                        break;

                    case "Undying Resolve":
                        if (Core.CheckInventory(item))
                            Core.Logger($"{item} Not Found, Can Be Farmed (with an Army) from [Encore Darkon] in [Theworld]");
                        break;
                }
            }
        }
        Core.Logger("All Boss drops Found. Item Check Complete, Continuing");
    }
}