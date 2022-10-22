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
using Skua.Core.Interfaces;

public class ArchMage
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreBLOD BLOD = new();
    public CoreQOM QOM = new();
    public CoreVHL VHL = new();
    public BuyScrolls Scroll = new();
    public CelestialArenaQuests CAQ = new();
    public CoreToD TOD = new();

    private string[] RequiredItems = { "Mystic Scribing Kit", "Prismatic Ether", "Arcane Locus", "Unbound Tome", "Book of Magus", "Book of Fire", "Book of Ice", "Book of Aether", "Book of Arcana", "Arcane Sigil", "Archmage" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(RequiredItems);
        Core.SetOptions();

        GetAM();

        Core.SetOptions(false);
    }

    public void GetAM(bool rankUpClass = true, bool getExtras = true)
    {
        if (Core.CheckInventory("Archmage"))
            return;

        Core.AddDrop(RequiredItems);

        //Book of Magus: Incantation
        if (!Core.CheckInventory("Book of Magus"))
        {
            UnboundTomb(1);
            Core.EnsureAccept(8913);

            BLOD.FindingFragmentsMace(200);
            
            Scroll.BuyScroll(Scrolls.Mystify, 50);

            Core.EquipClass(ClassType.Farm);
            Core.RegisterQuests(8814, 8815);
            while (!Bot.ShouldExit && !Core.CheckInventory("Prismatic Seams", 250))
                Core.HuntMonster("Streamwar", "Decaying Locust", "Timestream Medal", 5, log: false);
            Core.CancelRegisteredQuests();

            Core.HuntMonster("timeinn", "Ezrajal", "Celestial Magia", 50, false);
            Core.HuntMonster("noxustower", "Lightguard Caster", "Mortal Essence", 100, false);
            Core.HuntMonster("portalmazec", "Pactagonal Knight", "Orthogonal Energy", 150, false);

            Core.EnsureComplete(8913);
            Bot.Wait.ForPickup("Book of Magus");
            Core.ToBank(BLOD.BLoDItems);
        }

        //Book of Fire: Immolation
        if (!Core.CheckInventory("Book of Fire"))
        {
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
            Core.ToBank("Arcane Floating Sigil", "Sheathed Archmage's Staff");
        }

        //Book of Ice: Glacial Impact
        if (!Core.CheckInventory("Book of Ice"))
        {
            UnboundTomb(1);
            Core.EnsureAccept(8915);

            Scroll.BuyScroll(Scrolls.Frostbite, 50);

            Core.HuntMonster("kingcoal", "Frost King", "Ice Diamond", 100, false);
            Core.HuntMonster("icepike", "Chained Kezeroth", "Rimeblossom", 100, false);
            Core.HuntMonster("icepike", "Karok the Fallen", "Starlit Frost", 100, false);
            Core.HuntMonster("icedungeon", "Shade of Kyanos", "Temporal Floe", 100, false);

            Core.EnsureComplete(8915);
            Bot.Wait.ForPickup("Book of Ice");
            Core.ToBank("Archmage's Cowl", "Archmage's Cowl and Locks");
        }

        //Book of Aether: Supernova
        if (!Core.CheckInventory("Book of Aether"))
        {
            UnboundTomb(1);
            Core.EnsureAccept(8916);

            Scroll.BuyScroll(Scrolls.Eclipse, 50);

            //these are hard bosses anyways
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("streamwar", "Second Speaker", "A Fragment of the Beginning", isTemp: false);
            Core.HuntMonster("fireavatar", "Avatar Tyndarius", "Everlight Flame", isTemp: false);

            //Army Bosses.            
            Core.Logger("for the Following items You will Need to either public army them, sorry that we can't help *yet*" +
                        "Dage the Evil - dage - Vital Examina" +
                        "Flibbitiestgibbet thevoid - Void Essentia");

            Core.EnsureComplete(8916);
            Bot.Wait.ForPickup("Book of Aether");
            Core.ToBank("Archmage's Staff");
        }

        //Book of Arcana: Arcane Sigil
        if (!Core.CheckInventory("Book of Arcana"))
        {
            UnboundTomb(1);
            Core.EnsureAccept(8917);

            Scroll.BuyScroll(Scrolls.EtherealCurse, 50);

            //The mortal coil (x4 roents) //once we get a nulgath army also add that
            VHL.VHLChallenge(4);

            //Army Bosses:
            Core.Logger("for the Following items You will Need to either public army them, sorry that we can't help *yet*" +
                        "azalith - celestialpast - the divine will" +
                        "nightbane thevoid - insatiable hunger" +
                        "darkon - theworld - Undying resolve");

            Core.EnsureComplete(8917);
            Bot.Wait.ForPickup("Book of Arcana");
            Core.ToBank("Archmage's Robes");
        }

        //Archmage's Ascension        

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("archmage", "Prismata", "Elemental Binding", 250, false, publicRoom: true);

        Core.ChainComplete(8918);

        if (rankUpClass)
            Adv.rankUpClass("ArchMage");

        if (!getExtras)
            return;

    }

    void MysticScribingKit(int quant = 5)
    {
        if (Core.CheckInventory("Mystic Scribing Kit", quant))
            return;

        Core.FarmingLogger("Mystic Scribing Kit", quant);
        Farm.Experience(60);
        QOM.CompleteEverything();

        while (!Bot.ShouldExit && !Core.CheckInventory("Mystic Scribing Kit", quant))
        {
            Core.EnsureAccept(8909);

            Core.RegisterQuests(3048);
            while (!Bot.ShouldExit && !Core.CheckInventory("Mystic Quills", 49))
                Core.KillMonster("castleundead", "Enter", "Spawn", "*", log: false);
            Core.CancelRegisteredQuests();
            Core.HuntMonster("underworld", "Skull Warrior", "Mystic Shards", 49, false, log: false);


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

    void PrismaticEther(int quant = 1)
    {
        if (Core.CheckInventory("Prismatic Ether", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8910))
            MysticScribingKit(1);

        Core.FarmingLogger("Prismatic Ether", quant);
        Farm.ChaosREP(10);
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

    void ArcaneLocus(int quant = 1)
    {
        if (Core.CheckInventory("73339", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8911))
            PrismaticEther(1);

        Core.FarmingLogger("Arcane Locus", quant);

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

    void UnboundTomb(int quant)
    {
        if (Core.CheckInventory("Unbound Tome", quant))
            return;

        if (!Bot.Quests.IsUnlocked(8912))
            ArcaneLocus();

        Core.FarmingLogger("Arcane Locus", quant);

        Farm.GoodREP(10);
        Farm.EvilREP(10);
        TOD.CompleteToD();

        MysticScribingKit(quant);
        PrismaticEther(quant);
        ArcaneLocus(quant);

        while (!Core.CheckInventory("Unbound Tome", quant))
        {
            Core.EnsureAccept(8912);
            // Farm.Gold(3000000);
            Adv.BuyItem("alchemyacademy", 395, "Gold Voucher 100k", 30);
            Core.BuyItem("alchemyacademy", 395, "Dragon Runestone", 30, 1, 8844);
            Adv.BuyItem("darkthronehub", 1308, "Exalted Paladin Seal");
            Adv.BuyItem("shadowfall", 89, "Forsaken Doom Seal");
            Core.EnsureComplete(8912);
            Bot.Wait.ForPickup("Unbound Tome");
        }
    }
}