//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\Glacera.cs
//cs_include Scripts/
//cs_include Scripts/
//cs_include Scripts/
//cs_include Scripts/
//cs_include Scripts/
//cs_include Scripts/
using RBot;

public class FrostSpiritReaver
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();
    public GlaceraStory Glacera = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetFSR();

        Core.SetOptions(false);
    }
    public void GetFSR()
    {
        if (Core.CheckInventory("Frost SpiritReaver"))
            return;

        Glacera.DoAll();
        ColdHearted();
        ColdBlooded();
        IceSeeYou();
    }

    public void ColdHearted()
    {
        if (Core.isCompletedBefore(7920))
            return;

        Core.EnsureAccept(7920);

        if (!Core.CheckInventory("Fallen Scythe of Vengeance"))
        {
            if (!Core.CheckInventory("Frigid Scythe of Vengeance"))
            {
                if (!Core.CheckInventory("Cold Scythe of Vengeance"))
                {
                    if (!Core.CheckInventory("Scythe of Vengeance"))
                    {
                        // Merge the following:
                        // Flame of Courage x25
                        while (!Core.CheckInventory("Flame of Courage", 25))
                        {
                            Core.EnsureAccept(3955);
                            Core.HuntMonster("frozenruins", "Frost Invader", "Spark of Courage");
                            Core.EnsureComplete(3955);
                        }

                        // Karok's Glaceran Gem x1
                        if (!Core.CheckInventory("Karok's Glaceran Gem"))
                        {
                            Core.HuntMonster("Northstar", "Karok the Fallen", "Karok's Glaceran Gem", isTemp: false);
                        }
                        Core.BuyItem("Glacera", 1055, "Scythe of Vengeance");
                    }
                    // Merge the following:
                    // Scythe of Vengeance x1   
                    Core.BuyItem("Glacera", 1055, "Cold Scythe of Vengeance");
                }
                // Merge the following:
                // Cold Scythe of Vengeance x1                
                Core.BuyItem("Glacera", 1055, "Frigid Scythe of Vengeance");
            }
            // Merge the following:
            // Frigid Scythe of Vengeance x1            
            Core.BuyItem("Glacera", 1055, "Fallen Scythe of Vengeance");
        }
        // Ice Needle x30
        Core.HuntMonster("icestormarena", "Arctic Wolf ", "Ice Needle", 30, isTemp: false);
        // Northern Crown x1
        Core.HuntMonster("Snowmore", "Jon S'Nooooooo", "Northern Crown", isTemp: false);
        // Ice Diamond x3  
        while (!Core.CheckInventory("Ice Diamond", 3))
        {
            Core.EnsureAccept(7279);
            Core.HuntMonster("kingcoal", "Snow Golem", "Frozen Coal", 10);
            Core.EnsureComplete(7279);
        }
        Bot.Wait.ForPickup("Ice Diamond");
        Core.EnsureComplete(7920);
    }

    public void ColdBlooded()
    {
        if (Core.isCompletedBefore(7921))
            return;

        Core.AddDrop("FrostSlayer", "IceBreaker Mage");

        Core.EnsureAccept(7921);

        if (!Core.CheckInventory("IceBreaker Mage"))
        {
            Core.HuntMonster("iceplane", "Enfield", "IceBreaker Mage", isTemp: false);
        }

        if (!Core.CheckInventory("FrostSlayer"))
        {
            Core.HuntMonster("iceplane", "Enfield", "FrostSlayer", isTemp: false);
        }

        Core.EnsureComplete(7921);
    }

    public void IceSeeYou()
    {
        if (Core.isCompletedBefore(7922) && Core.CheckInventory("Frost SpiritReaver"))
            return;

        Core.EnsureAccept(7922);

        if (!Core.CheckInventory("Envoy of Kyanos"))
        {
            Farm.Gold(50000);
            // Icy Token I x50
            Tokens(50, 0, 0, 0);
            // Icy Token II x30
            Tokens(0, 30, 0, 0);
            // Icy Token III x20
            Tokens(0, 0, 20, 0);
            // Icy Token IV x10
            Tokens(0, 0, 0, 10);

            // Favored of Kyanos x1   
            if (!Core.CheckInventory("Favored of Kyanos"))
            {
                Farm.Gold(50000);
                // Icy Token I x25
                Tokens(25, 0, 0, 0);
                // Icy Token II x15
                Tokens(0, 15, 0, 0);
                // Icy Token III x10
                Tokens(0, 0, 10, 0);
                // Icy Token IV x5
                Tokens(0, 0, 0, 5);
                // Warrior of Kyanos x1            
                if (!Core.CheckInventory("Warrior of Kyanos"))
                {
                    Core.HuntMonster("IceDungeon", "Shade of Kyanos", "Warrior of Kyanos", isTemp: false);
                    Bot.Wait.ForPickup("Warrior of Kyanos");
                }
                Core.BuyItem("icedungeon", 1948, "Warrior of Kyanos");
            }
            Core.BuyItem("icedungeon", 1948, "Favored of Kyanos");
        }
        Core.BuyItem("icedungeon", 1948, "Envoy of Kyanos");


        Core.EnsureComplete(7922);

        Bot.Wait.ForPickup("Frost SpiritReaver");
    }

    public void Tokens(int Token1 = 300, int Token2 = 300, int Token3 = 300, int Token4 = 300)
    {
        if (Core.CheckInventory("Icy Token I", Token1) && Core.CheckInventory("Icy Token II", Token2) && Core.CheckInventory("Icy Token III", Token3) && Core.CheckInventory("Icy Token IV", Token4))
            return;

        Core.AddDrop("Icy Token I", "Icy Token II", "Icy Token III", "Icy Token IV");

        if (Token1 > 0)
        {
            {
                while (!Core.CheckInventory("Icy Token I", Token1))
                {
                    Core.EnsureAccept(7838);
                    Core.HuntMonster("icedungeon", "Frosted Banshee", "Frosted Banshee Defeated", 10);
                    Core.HuntMonster("icedungeon", "Frozen Undead", "Frozen Undead Defeated", 10);
                    Core.HuntMonster("icedungeon", "Ice Symbiote", "Ice Symbiote Defeated", 10);
                    Core.EnsureComplete(7839);
                }
            }
            if (Token2 > 0)
            {
                {
                    while (!Core.CheckInventory("Icy Token II", Token2))
                    {
                        Core.EnsureAccept(7839);
                        Core.HuntMonster("icedungeon", "Spirit of Ice", "Spirit of Ice Defeated", 10);
                        Core.HuntMonster("icedungeon", "Ice Crystal", "Ice Crystal Defeated", 10);
                        Core.HuntMonster("icedungeon", "Frigid Spirit", "Frigid Spirit Defeated", 10);
                        Core.EnsureComplete(7839);
                    }

                }
                if (Token3 > 0)
                {
                    Core.EquipClass(ClassType.Farm);
                    while (!Core.CheckInventory("Icy Token III", Token3))
                    {
                        Core.EnsureAccept(7840);
                        Core.HuntMonster("icedungeon", "Living Ice", "Living Ice Defeated", 5);
                        Core.HuntMonster("icedungeon", "Crystallized Elemental", "Crystallized Elemental Defeated", 5);
                        Core.HuntMonster("icedungeon", "Frozen Demon", "Frozen Demon Defeated", 5);
                        Core.EnsureComplete(7840);
                    }

                }
                if (Token4 > 0)
                {
                    Core.EquipClass(ClassType.Solo);
                    while (!Core.CheckInventory("Icy Token IV", Token4))
                    {
                        Core.EnsureAccept(7841);
                        Core.HuntMonster("icedungeon", "Image of Glace", "Glace's Approval");
                        Core.HuntMonster("icedungeon", "Abel", "Abel's Approval");
                        Core.HuntMonster("icedungeon", "Shade of Kyanos", "Kyanos' Approval");
                        Core.EnsureComplete(7841);
                    }
                }
            }
        }
    }
}
