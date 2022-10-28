//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Glacera.cs
using Skua.Core.Interfaces;

public class FrostSpiritReaver
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public GlaceraStory Glacera = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetFSR();

        Core.SetOptions(false);
    }

    public void GetFSR(bool rankUpClass = true)
    {
        if (Core.CheckInventory(59178))
            return;

        Glacera.DoAll();

        if (!Core.CheckInventory("Envoy of Kyanos"))
        {
            if (!Core.CheckInventory("Envoy of Kyanos"))
                Core.Logger("Getting Quest Item Requirements for \"Ice See You\"");

            if (!Core.CheckInventory("Favored of Kyanos"))
            {
                Core.Logger("Farming the requirements to buy \"Favored of Kyanos\"");
                Core.HuntMonster("icedungeon", "Shade of Kyanos", "Warrior of Kyanos", isTemp: false);
                Tokens(25, 15, 10, 5);

                Core.BuyItem("icedungeon", 1948, "Favored of Kyanos");
            }
            Core.Logger("Farming the requirements to buy \"Envoy of Kyanos\"");
            Tokens(50, 30, 20, 10);

            Core.BuyItem("icedungeon", 1948, "Envoy of Kyanos");
        }

        IceNinth(9);
        GlaceranAttunement(15);
        Core.AddDrop("Frost SpiritReaver");
        Core.ChainComplete(7922);
        Bot.Wait.ForPickup("Frost SpiritReaver");

        if (rankUpClass)
            Adv.rankUpClass("Frost SpititReaver");
    }

    public void IceNinth(int quant)
    {
        if (Core.CheckInventory("Ice-Ninth", quant))
            return;

        Core.AddDrop("Ice-Ninth", "Ice Diamond");
        Core.FarmingLogger("Ice-Ninth", quant);

        if (!Core.CheckInventory("Fallen Scythe of Vengeance"))
        {
            Core.Logger("Getting the quest item requirements for \"Cold Hearted\"");
            Core.AddDrop("Flame of Courage");

            Core.RegisterQuests(3955);
            while (!Bot.ShouldExit && !Core.CheckInventory("Flame of Courage", 25))
                Core.HuntMonster("frozenruins", "Frost Invader", "Spark of Courage");
            Core.CancelRegisteredQuests();

            Core.HuntMonster("Northstar", "Karok the Fallen", "Karok's Glaceran Gem", isTemp: false);
            Adv.BuyItem("Glacera", 1055, "Scythe of Vengeance");
            Adv.BuyItem("Glacera", 1055, "Cold Scythe of Vengeance");
            Adv.BuyItem("Glacera", 1055, "Frigid Scythe of Vengeance");
            Adv.BuyItem("Glacera", 1055, "Fallen Scythe of Vengeance");
        }

        Core.RegisterQuests(7920);
        while (!Bot.ShouldExit && !Core.CheckInventory("Ice-Ninth", quant))
        {
            Core.HuntMonster("icestormarena", "Arctic Wolf", "Ice Needle", 30, isTemp: false);
            Core.HuntMonster("Snowmore", "Jon S'Nooooooo", "Northern Crown", isTemp: false);
            while (!Bot.ShouldExit && !Core.CheckInventory("Ice Diamond", 3))
            {
                Core.EnsureAccept(7279);
                Core.HuntMonster("kingcoal", "Snow Golem", "Frozen Coal", 10, log: false);
                Core.EnsureComplete(7279);
                Bot.Wait.ForPickup("Ice Diamond");
            }
            Bot.Wait.ForPickup("Ice-Ninth");
        }
        Core.CancelRegisteredQuests();
    }

    public void GlaceranAttunement(int quant)
    {
        if (Core.CheckInventory("Glaceran Attunement", quant))
            return;

        Core.AddDrop("Glaceran Attunement");
        Core.FarmingLogger("Glaceran Attunement", quant);

        Core.EquipClass(ClassType.Solo);
        if (!Core.CheckInventory("IceBreaker Mage") && !Core.CheckInventory("FrostSlayer"))
        {
            Core.Logger("Getting the quest item requirements for \"Cold Blooded\"");
            Core.HuntMonster("iceplane", "Enfield", "IceBreaker Mage", isTemp: false);
            Core.HuntMonster("iceplane", "Enfield", "FrostSlayer", isTemp: false);
        }

        Core.RegisterQuests(7921);
        while (!Bot.ShouldExit && !Core.CheckInventory("Glaceran Attunement", quant))
        {
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("cryowar", "Super-Charged Karok", "Glacial Crystal", 100, isTemp: false);
            Core.HuntMonster("frozenlair", "Legion Lich Lord", "Sapphire Orb", 2, isTemp: false);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("frozenlair", "Frozen Legionnaire", "Ice Spike", 20, isTemp: false);
            Core.HuntMonster("frozenlair", "Frozen Legionnaire", "Ice Splinter", 20, isTemp: false);

            Bot.Wait.ForPickup("Glaceran Attunement");
        }
        Core.CancelRegisteredQuests();
    }

    public void Tokens(int Token1 = 300, int Token2 = 300, int Token3 = 300, int Token4 = 300)
    {
        if (!Core.CheckInventory("Icy Token I", Token1))
        {
            Core.AddDrop("Icy Token I");
            Core.FarmingLogger("Icy Token I", Token1);
            Core.EquipClass(ClassType.Farm);

            Core.RegisterQuests(7840, 7838);
            while (!Bot.ShouldExit && !Core.CheckInventory("Icy Token I", Token1))
            {
                Core.HuntMonster("icedungeon", "Frosted Banshee", "Frosted Banshee Defeated", 10, log: false);
                Core.HuntMonster("icedungeon", "Frozen Undead", "Frozen Undead Defeated", 10, log: false);
                Core.HuntMonster("icedungeon", "Ice Symbiote", "Ice Symbiote Defeated", 10, log: false);
            }
            Core.CancelRegisteredQuests();
        }

        if (!Core.CheckInventory("Icy Token II", Token2))
        {
            Core.AddDrop("Icy Token II");
            Core.FarmingLogger("Icy Token II", Token2);
            Core.EquipClass(ClassType.Farm);

            Core.RegisterQuests(7839);
            while (!Bot.ShouldExit && !Core.CheckInventory("Icy Token II", Token2))
            {
                Core.HuntMonster("icedungeon", "Spirit of Ice", "Spirit of Ice Defeated", 10, log: false);
                Core.HuntMonster("icedungeon", "Ice Crystal", "Ice Crystal Defeated", 10, log: false);
                Core.HuntMonster("icedungeon", "Frigid Spirit", "Frigid Spirit Defeated", 10, log: false);

                Bot.Wait.ForPickup("Icy Token II");
            }
            Core.CancelRegisteredQuests();
        }

        if (!Core.CheckInventory("Icy Token III", Token3))
        {
            Core.AddDrop("Icy Token III");
            Core.FarmingLogger("Icy Token III", Token3);
            Core.EquipClass(ClassType.Farm);

            Core.RegisterQuests(7840);
            while (!Bot.ShouldExit && !Core.CheckInventory("Icy Token III", Token3))
            {
                Core.HuntMonster("icedungeon", "Living Ice", "Living Ice Defeated", 5, log: false);
                Core.HuntMonster("icedungeon", "Crystallized Elemental", "Crystallized Elemental Defeated", 5, log: false);
                Core.HuntMonster("icedungeon", "Frozen Demon", "Frozen Demon Defeated", 5, log: false);

                Bot.Wait.ForPickup("Icy Token III");
            }
            Core.CancelRegisteredQuests();
        }

        if (!Core.CheckInventory("Icy Token IV", Token4))
        {
            Core.AddDrop("Icy Token IV");
            Core.FarmingLogger("Icy Token IV", Token4);
            Core.EquipClass(ClassType.Solo);

            Core.RegisterQuests(7841);
            while (!Bot.ShouldExit && !Core.CheckInventory("Icy Token IV", Token4))
            {
                Core.HuntMonster("icedungeon", "Image of Glace", "Glace's Approval");
                Core.HuntMonster("icedungeon", "Abel", "Abel's Approval");
                Core.HuntMonster("icedungeon", "Shade of Kyanos", "Kyanos' Approval");

                Bot.Wait.ForPickup("Icy Token IV");
            }
            Core.CancelRegisteredQuests();
        }
    }
}
