//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using RBot;

public class UltimateBLoD
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        LOC.Wolfwing();
        BLOD.DoAll();
        UltimateBlindingLightofDestiny();

        Core.SetOptions(false);
    }
    public void UltimateBlindingLightofDestiny()
    {
        if (Core.CheckInventory("Ultimate Blinding Light of Destiny"))
            return;

        Core.AddDrop(new[] { "Ultimate Blinding Light of Destiny", "Overwhelmed Axe", "Shard of An Orb", "Purified Undead Dragon Essence" });  

        // Getting Overwhelmed Axe
        if (!Core.CheckInventory("Overwhelmed Axe"))
        {
            BLOD.FindingFragmentsBlade(250, 100);
            BLOD.FindingFragmentsMace(10);

            int i = 1;
            Core.Logger(Core.CheckInventory("Blinding Aura") ? "Blinding Aura found." : "Farming for Blinding Aura");
            Core.Logger($"Farming 1 Blinding Aura");
            while (!Core.CheckInventory("Blinding Aura"))
            {
                FindingFragments(2174);
                Core.Logger($"Completed x{i}");
            }

            Core.BuyItem("techfortress", 1902, "Overwhelmed Axe", shopItemID: 7588);
        }

        // Farming Shard of An Orb
        Core.EquipClass(ClassType.Solo);
        while (!Core.CheckInventory("Shard of An Orb", 5))
        {
            Core.EnsureAccept(7654);

            Core.KillMonster($"dflesson", "r12", "Right", "Fluffy the Dracolich", "Fluffy’s Bones", 10, isTemp: false);
            Core.KillMonster("dflesson", "r3", "Right", "Fire Elemental", "Fire Elemental's Bracer", 5, isTemp: false);
            Core.KillMonster("dflesson", "r6", "Right", "Tog", "Tog Claw", 5, isTemp: false);

            Core.EnsureComplete(7654);
            Bot.Wait.ForPickup("Shard of An Orb");
        }

        // Getting Purified Undead Dragon Essence
        if (!Core.CheckInventory("Purified Undead Dragon Essence"))
        {
            Core.EnsureAccept(7655);

            Core.KillMonster("doomwood", "r10", "Right", "Undead Paladin", "Purification Orb", 10, isTemp: false);

            while (!Core.CheckInventory("Rainbow Moonstone", 5))
            {
                Core.AddDrop("Rainbow Moonstone");
                Core.EnsureAccept(7291);

                Core.HuntMonster("earthstorm", "Diamond Golem", "Chip of Diamond");
                Core.HuntMonster("earthstorm", "Emerald Golem", "Chip of Emerald");
                Core.HuntMonster("earthstorm", "Ruby Golem", "Chip of Ruby");
                Core.HuntMonster("earthstorm", "Sapphire Golem", "Chip of Sapphire");

                Core.EnsureComplete(7291);
                Bot.Wait.ForPickup("Rainbow Moonstone");
            }

            Core.KillMonster("desolich", "r3", "Left", "Desolich", "Desolich's Dark Horn", 3, isTemp: false);

            Core.EnsureComplete(7655);
            Bot.Wait.ForPickup("Purified Undead Dragon Essence");
        }

        Core.BuyItem("techfortress", 1902, "Ultimate Blinding Light of Destiny", shopItemID: 7585);
    }
}
