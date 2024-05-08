/*
name: Blaze Beard Story
description: This will finish the Blaze Beard Story Quest.
tags: story, quest, blaze-beard, pirate
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class BlazeBeard
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        // Core.BankingBlackList.AddRange(RequiredItems);
        Core.SetOptions();

        TokenQuests();

        Core.SetOptions(false);
    }

    // string[] RequiredItems = { "Pirate Booty I", "Pirate Booty II", "Pirate Booty III", "Pirate Booty IV", "Pirate Booty V", "Pirate Booty VI", "Pirate Booty VII", "Pirate Booty VIII", "Pirate Booty IX", "Pirate Booty X", "Pirate Booty XI", "Pirate Booty XII", "Pirate Booty XIII", "Pirate Booty XIV", "Pirate Booty XV" };


    public void TokenQuests()
    {
        if (!Core.isSeasonalMapActive("BlazeBeard"))
            return;

        Story.PreLoad(this);

        Story.LegacyQuestManager(QuestLogic, 4513, 4514, 4515, 4516, 4517, 4518, 4519, 4520, 4521, 4522, 4523, 4524, 4525, 4526, 4527);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4514:
                    Core.HuntMonster("CrashSite", "Barrier Bot", "Dwakel Scrap Metal", 10);
                    break;

                case 4515:
                    Core.HuntMonster("Mafic", "Living Fire", "Mysterious Molten Metal", 12);
                    break;

                case 4516:
                    Core.HuntMonster("Elemental", "Mana Falcon", "Mana Droplets", 12);
                    break;

                case 4517:
                    Core.HuntMonster("EarthStorm", "Earth Elemental", "No Explosion", 15);
                    break;

                case 4518:
                    Core.GetMapItem(3725, 12, "BlazeBeard");
                    break;

                case 4519:
                    Core.GetMapItem(3725, 12, "BlazeBeard");
                    break;

                case 4520:
                    Core.HuntMonster("BlazeBeard", "Pirate Plunderer", "Blazebeard's Plans", 5);
                    break;

                case 4521:
                    Core.HuntMonster("BlazeBeard", "Pirate Plunderer", "Blazebeard's Plans", 5);
                    break;

                case 4522:
                    Core.HuntMonster("BlazeBeard", "Pirate Captain", "Captain Defeated");
                    break;

                case 4523:
                    Core.HuntMonster("ManaCannon", "Pirate Gunner", "Hand Cannons", 6);
                    break;

                case 4524:
                    Core.HuntMonster("ManaCannon", "Pirate Plunderer", "Location Researched", 10);
                    break;

                case 4525:
                    Core.HuntMonster("ManaCannon", "Pirate Crew", "Crew Defeated", 15);
                    break;

                case 4526:
                    Core.HuntMonster("ManaCannon", "Pirate Caster", "Pirate Caster Defeated", 15);
                    break;

                case 4527:
                    Core.HuntMonster("ManaCannon", "Blazebeard", "Blazebeard Doused");
                    break;
            }
        }
    }
}
