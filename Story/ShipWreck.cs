/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ShipWreck
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Golden Scale", "Crystal Fragment", "Powder Flask",
                                               "Broken Anti-Au Crystal", "Flagon of Water", "Old Weapon",
                                               "Shell", "Crystallized Crowbar", "Trapdoor Key",
                                               "What is THAT?!", "Lobthulu Claw" });
        Core.SetOptions();

        StoryLine();


        Core.SetOptions(false);

    }

    public void StoryLine()
    {
        if (Core.CheckInventory("Lobthulu Claw"))
            return;

        Story.LegacyQuestManager(QuestLogic, 4418, 4419, 4420, 4421, 4422, 4423, 4424, 4425, 4426, 4427, 4428);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4418: // Midas Touched 4418
                    Core.HuntMonster("shipwreck", "Gilded Merdraconian", "Mangled Merdraconian", 10);
                    break;

                case 4419: // Clearing the Cargo hold 4419
                    Core.HuntMonster("shipwreck", "Cursed Pirate", "Pirate Interrogated", 4);
                    Core.HuntMonster("shipwreck", "Gilded Crystal Undead", "Captured Crystal Crew", 8);
                    break;

                case 4420: // Au-Dacity to Attack 4420
                    Core.HuntMonster("shipwreck", "Cursed Pirate", "Pirate Pistols", 8);
                    break;

                case 4421: // Getting the Gauntlet 4421
                    Core.HuntMonster("shipwreck", "Cursed Pirate", "Snatched Material", 12);
                    break;

                case 4422: // Against the Elements 4422
                    Core.HuntMonster("shipwreck", "Gilded Water", "Liquified Living Water", 10);
                    break;

                case 4423: // Gold Men Tell No Tales 4423
                    Core.HuntMonster("shipwreck", "Cursed Pirate", "Detained Crewman", 12);
                    break;

                case 4424: // Taking Sides 4424
                    Core.HuntMonster("shipwreck", "Gilded Merdraconian", "Powerfully Persuaded Merdraconian", 11);
                    break;

                case 4425: // Crystallized Crew 4425
                    Core.HuntMonster("shipwreck", "Gilded Crystal Undead", "Crystallized Crowbar");
                    break;

                case 4426: // Getting a Nubar of Gold 4426
                    Core.HuntMonster("shipwreck", "Captain Nubar", "No More Nubar");
                    break;

                case 4427: // Killer Coral 4427
                    Core.GetMapItem(3573, 1, "shipwreck");
                    break;

                case 4428: // Driving for Xergon 4428
                    Core.HuntMonster("shipwreck", "Lobthulhu", "Lobster a la mode");
                    break;
            }
        }

        Core.Unbank("Golden Scale", "Crystal Fragment", "Powder Flask",
                            "Broken Anti-Au Crystal", "Flagon of Water", "Old Weapon",
                            "Shell", "Crystallized Crowbar", "Trapdoor Key",
                            "What is THAT?!"); // didn't add the last token to it
        Core.TrashCan("Golden Scale", "Crystal Fragment", "Powder Flask",
                                           "Broken Anti-Au Crystal", "Flagon of Water", "Old Weapon",
                                           "Shell", "Crystallized Crowbar", "Trapdoor Key",
                                           "What is THAT?!");

    }
}
