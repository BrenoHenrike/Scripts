//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
using Skua.Core.Interfaces;

public class UltimateBLoD
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        UltimateBlindingLightofDestiny();

        Core.SetOptions(false);
    }

    public void UltimateBlindingLightofDestiny()
    {
        if (Core.CheckInventory("Ultimate Blinding Light of Destiny"))
            return;

        BLOD.DoAll();

        OverwhelmedAxe();
        ShardOfAnOrb();
        PurifiedUndeadDragonEssence();

        Core.BuyItem("techfortress", 1902, "Ultimate Blinding Light of Destiny", shopItemID: 7585);
    }

    public void OverwhelmedAxe()
    {
        if (Core.CheckInventory("Overwhelmed Axe"))
            return;

        Core.AddDrop("Overwhelmed Axe");
        Core.FarmingLogger("Overwhelmed Axe", 1);

        BLOD.FindingFragmentsBlade(250, 100);
        BLOD.FindingFragmentsMace(10);

        Core.Logger(Core.CheckInventory("Blinding Aura") ? "Blinding Aura found." : "Farming for Blinding Aura");
        while (!Bot.ShouldExit && !Core.CheckInventory("Blinding Aura"))
            BLOD.FindingFragments(2174);

        Core.BuyItem("techfortress", 1902, "Overwhelmed Axe", shopItemID: 7588);
    }

    public void ShardOfAnOrb(int quant = 5)
    {
        if (Core.CheckInventory("Shard of An Orb", quant))
            return;

        Core.AddDrop("Shard of An Orb");
        Core.FarmingLogger("Shard of An Orb", quant);
        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(7654);
        while (!Bot.ShouldExit && !Core.CheckInventory("Shard of An Orb", quant))
        {

            Core.KillMonster($"dflesson", "r12", "Right", "Fluffy the Dracolich", "Fluffy’s Bones", 10, isTemp: false);
            Core.KillMonster("dflesson", "r3", "Right", "Fire Elemental", "Fire Elemental's Bracer", 5, isTemp: false);
            Core.KillMonster("dflesson", "r6", "Right", "Tog", "Tog Claw", 5, isTemp: false);

            Bot.Wait.ForPickup("Shard of An Orb");
        }
        Core.CancelRegisteredQuests();
    }

    public void PurifiedUndeadDragonEssence()
    {
        BLOD.DoAll();
        OverwhelmedAxe();
        if (Core.CheckInventory("Purified Undead Dragon Essence"))
            return;

        Core.AddDrop("Purified Undead Dragon Essence");
        Core.FarmingLogger("Purified Undead Dragon Essence", 1);

        Core.EnsureAccept(7655);
        Core.EquipClass(ClassType.Solo);

        Core.KillMonster("doomwood", "r10", "Right", "Undead Paladin", "Purification Orb", 10, isTemp: false);

        Core.AddDrop("Rainbow Moonstone");
        Core.RegisterQuests(7291);
        while (!Bot.ShouldExit && !Core.CheckInventory("Rainbow Moonstone", 5))
        {

            Core.HuntMonster("earthstorm", "Diamond Golem", "Chip of Diamond");
            Core.HuntMonster("earthstorm", "Emerald Golem", "Chip of Emerald");
            Core.HuntMonster("earthstorm", "Ruby Golem", "Chip of Ruby");
            Core.HuntMonster("earthstorm", "Sapphire Golem", "Chip of Sapphire");

            Bot.Wait.ForPickup("Rainbow Moonstone");
        }
        Core.CancelRegisteredQuests();

        Core.KillMonster("desolich", "r3", "Left", "Desolich", "Desolich's Dark Horn", 3, isTemp: false);

        Core.EnsureComplete(7655);
        Bot.Wait.ForPickup("Purified Undead Dragon Essence");
    }
}