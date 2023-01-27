/*
name:  Ascended Drakath Gear
description:  Farms the ascended Drakath Set
tags: ascended drakath, set, drakath, ascended blade of awe, ascended light of destiny, ascended face of chaos
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
using Skua.Core.Interfaces;

public class AscendedDrakathGear
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public DrakathArmorBot DA = new DrakathArmorBot();
    public TowerOfDoom TOD = new TowerOfDoom();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        if (Core.CheckInventory(new[] { "Ascended Blade of Awe", "Ascended Light of Destiny", "Ascended Face of Chaos" }))
            return;

        Core.AddDrop("Ascended Blade of Awe", "Ascended Light of Destiny", "Ascended Face of Chaos");

        AscendedGear("Ascended Blade of Awe");
        AscendedGear("Ascended Light of Destiny");
        AscendedGear("Ascended Face of Chaos");
    }

    public void AscendedGear(string Target)
    {
        if (Core.CheckInventory(Target))
            return;

        DA.DrakathOriginalArmor();
        Core.AddDrop(Target);
        Core.RegisterQuests(3767);
        while (!Bot.ShouldExit && !Core.CheckInventory(Target))
        {
            Bot.Quests.UpdateQuest(159, 4);
            Core.HuntMonster("towerofdoom4", "Dread Stranglerfish", "Holy Wasabi");
            Bot.Wait.ForPickup(Target);
        }
        Core.CancelRegisteredQuests();
    }
}
