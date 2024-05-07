/*
name: Murder Moon Story
description: This will complete the Murder Moon story.
tags: story, quest, seasonal, murder, moon, may-the-4th
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class MurderMoon
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MurderMoonStory();

        Core.SetOptions(false);
    }

    public void MurderMoonStory()
    {
        if (Core.isCompletedBefore(9224) || !Core.isSeasonalMapActive("murdermoon"))
            return;

        //That Is The Way
        if (!Story.QuestProgression(8062))
        {
            Core.EnsureAccept(8062);
            Core.KillMonster("murdermoon", "r2", "Left", "Tempest Soldier", "Soldiers Defeated", 6);
            Core.EnsureComplete(8062);
        }

        //Murder Moon Plans
        if (!Story.QuestProgression(8063))
        {
            Core.EnsureAccept(8063);
            Core.KillMonster("murdermoon", "r2", "Left", "Tempest Soldier", "Murder Moon Plans");
            Story.MapItemQuest(8063, "murdermoon", 8373, 5);
        }

        //Revenge of the Fifth
        Story.KillQuest(8064, "murdermoon", "Fifth Sepulchure");

        // Tempest Proofing (9223)
        Story.KillQuest(9223, "murdermoon", "Tempest Soldier");

        // Liberty's Ghost (9224)
        if (!Story.QuestProgression(9224))
        {
            Adv.GearStore();
            if ((!Core.CheckInventory("Dark Lord") && !Core.CheckInventory("Darkside")) || !Core.isCompletedBefore(8821))
            {
                Core.Logger("This quest requires either Dark Lord or Darkside class and Elysium enhancement, use army.");
                return;
            }
            if (Core.CheckInventory("Dark Lord"))
                Core.Equip("Dark Lord");
            else
                Core.Equip("Darkside");
            InventoryItem? EquippedWeapon = Bot.Inventory.Items.Find(i => i != null && i.Equipped && Adv.WeaponCatagories.Contains(i.Category));
            Adv.EnhanceItem(EquippedWeapon!.Name, EnhancementType.Wizard, wSpecial: WeaponSpecial.Elysium);
            Story.KillQuest(9224, "murdermoon", "Fourth Lynaria");
            Adv.GearStore(true);
        }
    }
}
