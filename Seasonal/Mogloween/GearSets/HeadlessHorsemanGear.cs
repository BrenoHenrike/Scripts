/*
name: Headless Horseman Gear
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Seasonal/Mogloween/MergeShops/MogloweenSeasonalMerge.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class HeadlessHorsemanGear
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();
    public MogloweenSeasonalMerge MogloweenMerge => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetGear();

        Core.SetOptions(false);
    }

    public void GetGear()
    {
        int[] QuestIDs = new[] { 9457, 9458, 9459, 9460, 9461 };

        if (!Core.isSeasonalMapActive("mogloween") || Core.CheckInventory(Core.QuestRewards(QuestIDs), toInv: false))
        {
            Core.Logger(Core.CheckInventory(Core.QuestRewards(QuestIDs)) ?
            "All Rewards Obtained." :
            "Seasonal Map not available.");
            return;
        }


        // Add all items to the drops
        Core.Logger("Adding all drops & requirements to DropLog");

        foreach (int QuestID in QuestIDs)
        {
            Core.EnsureLoad(QuestID)
                .Requirements
                .Concat(Core.EnsureLoad(QuestID).Rewards)
                .ToList()
                .ForEach(item => Core.AddDrop(item.ID));
        }


        foreach (int QuestID in QuestIDs)
        {
            Quest QuestData = Core.EnsureLoad(QuestID);

            if (!Core.CheckInventory(Core.QuestRewards(QuestID)))
            {
                switch (QuestID)
                {
                    case 9457:
                        Core.Logger("Get Required Quest item");
                        Core.EquipClass(ClassType.Solo);
                        Core.HuntMonster("crescentmoon", "Royce", "Oversoul's Headless Horseman", isTemp: false);
                        Core.EnsureAccept(QuestID);
                        Core.HuntMonster("crescentmoon", "Royce", "Royce's Direclaw", 10, isTemp: false);
                        Core.EquipClass(ClassType.Farm);
                        Core.HuntMonster("pie", "Myst Yaga", "Yaga Staff", 15);
                        Core.HuntMonster("twigsarcade", "Spirit Residue", "Spirit Residue", 15);
                        Core.HuntMonster("bloodmoon", "Black Unicorn", "Chunk of Armor", 10);
                        Core.EnsureComplete(QuestID);
                        break;

                    case 9458:
                        // The Horseman’s Cape 9458
                        Core.EnsureAccept(QuestID);
                        Core.KillMonster("mogloween", "Pit1", "Center", "Blister", "Torn Cloth", 10);
                        Core.HuntMonster("necrocarnival", "ZaZOOOL", "Shadow of Screams", isTemp: false);
                        Core.HuntMonster("candycorn", "Malik-EYE ", "Knife", 1);
                        Core.EnsureComplete(QuestID);
                        break;

                    case 9459:
                        // The Horseman’s Head 9459
                        Core.EnsureAccept(QuestID);
                        // Pumpkin Grin
                        Adv.BuyItem("asylum", 507, 36720, shopItemID: 21659);
                        
                        Core.HuntMonster("that", "Will O' The Wisp", "Flames", 30);
                        Core.HuntMonster("twigsarcade", "Ectoplasm", "Ectoplasm", 20);
                        Core.HuntMonster("tricktown", "Rotting Pumpkin", "Pumpkin Seeds", 50);
                        Core.EnsureComplete(QuestID);
                        break;

                    case 9460:
                        if (!Core.CheckInventory("Headtaking Headless Horseman"))
                            goto case 9459;

                        // The Horseman’s Axe 9460
                        Core.EnsureAccept(QuestID);
                        Core.HuntMonster("franken", "Frankenwerepire", "Shattered Metal Piece", 7);
                        Core.HuntMonster("that", "Shattered Hope", "Lost Hope", 10);
                        Core.HuntMonster("voltabolt", "Nightmare Dentist Chair", "Blade Sharpener");
                        Core.EnsureComplete(QuestID);
                        break;

                    case 9461:
                        if (!Core.CheckInventory("Oversoul's Rusted Head Axe"))
                            goto case 9460;

                        // The Horseman’s Second Axe 9461
                        Core.EnsureAccept(QuestID);
                        MogloweenMerge.BuyAllMerge("Sinister PumpKing Blade");
                        Core.HuntMonster("necronaut", "necronaut", "Aged Metal", 5, isTemp: false);
                        Core.HuntMonster("mogloweengrave", "Zombie Terror", "Oversoul Essence", 15, isTemp: false);
                        Core.HuntMonster("cask", "Nitre Golem", "Potassium Nitrate", 10, isTemp: false);
                        Core.HuntMonster("that", "Congealed Fear", "Fear Essence", 15, isTemp: false);
                        Core.EnsureComplete(QuestID);
                        break;
                }
            }
        }
    }
}
