//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using RBot;

public class UltimateBLoD
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailies Daily = new();
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

            FindingFragments(2174, "Blinding Aura", 1);

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
                //Story.KillQuest(7291, "earthstorm", new[] { "Diamond Golem", "Emerald Golem", "Ruby Golem", "Sapphire Golem" });
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

    private void FindingFragments(int quest, string item, int quant)
    {
        if (Core.CheckInventory(item, quant))
            return;

        Core.AddDrop("Bone Dust", "Undead Essence", "Undead Energy", "Blinding Light Fragments", "Spirit Orb", "Loyal Spirit Orb", "Bright Aura", "Brilliant Aura", "Blinding Aura");
        
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(quest);
        while (!Core.CheckInventory(item, quant))
        {
            Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Blinding Light Fragments", 10, false, publicRoom: true);
        }
    }
}
