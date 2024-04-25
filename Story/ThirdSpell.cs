/*
name: Third Spell Story
description: This will finish the Third Spell Story.
tags: story, quest, thirdspell
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThirdSpell
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] {
                                "Brainz n' Eggs",
                                "Mana Token I", "Mana Token II", "Mana Token III", "Mana Token IV", "Mana Token V", "Mana Token VI", "Mana Token VI",
                                "Mana Token VII", "Mana Token VIII", "Mana Token IX", "Mana Token X", "Mana Token XI",
                                "Sun Token I", "Sun Token II", "Sun Token III", "Sun Token IV", "Sun Token V", "Sun Token VI", "Sun Token VII", "Sun Token VIII",
                                "Heart of the Sun"});
        Core.SetOptions();
        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine(bool HoTS = false)
    {
        Core.Logger($"Heart of the Sun Mode Enabled?: {HoTS}");

        if (HoTS)
            HeartoftheSun();

        else
        {
            if (Core.CheckInventory("Sun Token VIII", toInv: false))
            {
                Core.Logger("Heart of the Sun Mode: False, \"Sun Token VIII\" Owned.");
                return;
            }

            Story.LegacyQuestManager(QuestLogic, 4474, 4475, 4476, 4477, 4478, 4479, 4480, 4481, 4482, 4483, 4484, 4485, 4486, 4487, 4488, 4489, 4490, 4491, 4492, 4493);

            void QuestLogic()
            {
                switch (Story.LegacyQuestID)
                {
                    case 4474: //Breakfast With... Brains?
                        Core.HuntMonster("doomwood", "Doomwood Treeant", "Braaaainz", 10);
                        break;

                    case 4475: //Post-Elemental Apocalypse
                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Plane Monster Defeated", 12);
                        break;

                    case 4476: //Phoenix’s First Birthday
                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Proxy Eggs", 8);
                        break;

                    case 4477: //Elementals Are From the Sun, We Are From Lore
                        Core.GetMapItem(3668, 10, "thirdspell");
                        break;

                    case 4478: //Sssssmoking...
                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Elemental Pathway Cleared", 20);
                        break;

                    case 4479: //Green-Eyed Incantations
                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Green Eye", 5);
                        break;

                    case 4480: //A Lonely Cysero
                        Core.GetMapItem(3671, map: "thirdspell");
                        break;

                    case 4481: //Body & Soul & The Hero
                        Core.GetMapItem(3675, map: "thirdspell");
                        break;

                    case 4482: //Abduction
                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Abducted Mana Phoenix");
                        break;

                    case 4483: //Truth or De-Feathering
                        Core.GetMapItem(3672, map: "thirdspell");
                        break;

                    case 4484: //The Art of Persuasion
                        Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated");
                        break;

                    case 4485: //Frozen Mana
                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Phoenix Egg", 7);
                        break;

                    case 4486: //Angry Elements
                        Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated Again");
                        break;

                    case 4487: //The Sunspots, They Are Changing!
                        Core.GetMapItem(3673, 3, "thirdspell");
                        break;

                    case 4488: //I Enjoy Being a Soul
                        Core.KillMonster("thirdspell", "r12", "Left", "Living Fire", "Sun Monster Ember", 15);
                        break;

                    case 4489: //Burning Like Me!
                        Core.HuntMonster("thirdspell", "Sun Flare", "Sun Flare Defeated", 10);
                        Core.KillMonster("thirdspell", "r12", "Left", "Living Fire", "Living Fire Defeated", 5);
                        break;

                    case 4490: //Assault With a Deadly Shadow
                        Core.GetMapItem(3674, map: "thirdspell");
                        break;

                    case 4491: // 4491|Mother Knows The Sun
                        Core.HuntMonster("thirdspell", "Solar Incarnation ", "Heart of the Sun Received");
                        break;

                    case 4492: // 4492|Selfishness
                        Core.GetMapItem(3676, map: "thirdspell");
                        break;

                    case 4493: // 4493|See the Hero Run        
                        Core.GetMapItem(3677, map: "thirdspell");
                        break;
                }
            }
        }

    }

    void HeartoftheSun()
    {
        if (Core.CheckInventory("Heart of the Sun"))
            return;
        Core.AddDrop(
                "Brainz n' Eggs",
                "Mana Token I", "Mana Token II", "Mana Token III", "Mana Token IV", "Mana Token V", "Mana Token VI", "Mana Token VI",
                "Mana Token VII", "Mana Token VIII", "Mana Token IX", "Mana Token X", "Mana Token XI",
                "Sun Token I", "Sun Token II", "Sun Token III", "Sun Token IV", "Sun Token V", "Sun Token VI",
                "Heart of the Sun");

        Core.EquipClass(ClassType.Farm);

        if (!Core.CheckInventory("Heart of the Sun"))
        {
            // Check for "Sun Token V"
            if (!Core.CheckInventory("Sun Token V"))
            {
                // Check for "Sun Token IV"
                if (!Core.CheckInventory("Sun Token IV"))
                {
                    // Check for "Sun Token III"
                    if (!Core.CheckInventory("Sun Token III"))
                    {
                        // Check for "Sun Token II"
                        if (!Core.CheckInventory("Sun Token II"))
                        {
                            // Check for "Sun Token I"
                            if (!Core.CheckInventory("Sun Token I"))
                            {
                                // Check for "Mana Token XI"
                                if (!Core.CheckInventory("Mana Token XI"))
                                {
                                    // Check for "Mana Token X"
                                    if (!Core.CheckInventory("Mana Token X"))
                                    {
                                        // Check for "Mana Token IX"
                                        if (!Core.CheckInventory("Mana Token IX"))
                                        {
                                            // Check for "Mana Token VIII"
                                            if (!Core.CheckInventory("Mana Token VIII"))
                                            {
                                                // Check for "Mana Token VII"
                                                if (!Core.CheckInventory("Mana Token VII"))
                                                {
                                                    // Check for "Mana Token VI"
                                                    if (!Core.CheckInventory("Mana Token VI"))
                                                    {
                                                        // Check for "Mana Token V"
                                                        if (!Core.CheckInventory("Mana Token V"))
                                                        {
                                                            // Check for "Mana Token IV"
                                                            if (!Core.CheckInventory("Mana Token IV"))
                                                            {
                                                                // Check for "Mana Token III"
                                                                if (!Core.CheckInventory("Mana Token III"))
                                                                {
                                                                    // Check for "Mana Token II"
                                                                    if (!Core.CheckInventory("Mana Token II"))
                                                                    {
                                                                        // Check for "Mana Token I"
                                                                        if (!Core.CheckInventory("Mana Token I"))
                                                                        {
                                                                            // Check for "Brainz n' Eggs"
                                                                            if (!Core.CheckInventory("Brainz n' Eggs"))
                                                                            {
                                                                                Core.Logger("Token: Brainz n' Eggs");
                                                                                Core.EnsureAccept(4474);
                                                                                Core.HuntMonster("doomwood", "Doomwood Treeant", "Braaaainz", 10);
                                                                                Core.EnsureComplete(4474);
                                                                                Bot.Wait.ForPickup("Brainz n' Eggs");
                                                                            }
                                                                            Core.Logger("Token: Mana Token II");
                                                                            Core.EnsureAccept(4475);
                                                                            Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Plane Monster Defeated", 12);
                                                                            Core.EnsureComplete(4475);
                                                                            Bot.Wait.ForPickup("Mana Token I");
                                                                        }
                                                                        Core.Logger("Token: Mana Token III");
                                                                        Core.EnsureAccept(4476);
                                                                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Proxy Eggs", 8);
                                                                        Core.EnsureComplete(4476);
                                                                        Bot.Wait.ForPickup("Mana Token II");
                                                                    }
                                                                    Core.Logger("Token: Mana Token IV");
                                                                    Core.EnsureAccept(4477);
                                                                    Core.GetMapItem(3668, 10, "thirdspell");
                                                                    Core.EnsureComplete(4477);
                                                                    Bot.Wait.ForPickup("Mana Token III");
                                                                }
                                                                Core.Logger("Token: Mana Token V");
                                                                Core.EnsureAccept(4478);
                                                                Core.HuntMonster("thirdspell", "Mana Phoenix", "Elemental Pathway Cleared", 20);
                                                                Core.EnsureComplete(4478);
                                                                Bot.Wait.ForPickup("Mana Token IV");
                                                            }
                                                            Core.Logger("Token: Mana Token VI");
                                                            Core.EnsureAccept(4479);
                                                            Core.HuntMonster("thirdspell", "Mana Phoenix", "Green Eye", 5);
                                                            Core.EnsureComplete(4479);
                                                            Bot.Wait.ForPickup("Mana Token V");
                                                        }
                                                        Core.Logger("Token: Mana Token VII");
                                                        Core.EnsureAccept(4480);
                                                        Core.GetMapItem(3671, map: "thirdspell");
                                                        Core.EnsureComplete(4480);
                                                        Bot.Wait.ForPickup("Mana Token VI");
                                                    }
                                                    Core.Logger("Token: Mana Token VIII");
                                                    Core.EnsureAccept(4481);
                                                    Core.GetMapItem(3675, map: "thirdspell");
                                                    Core.EnsureComplete(4481);
                                                    Bot.Wait.ForPickup("Mana Token VII");
                                                }
                                                Core.Logger("Token: Mana Token IX");
                                                Core.EnsureAccept(4482);
                                                Core.HuntMonster("thirdspell", "Mana Phoenix", "Abducted Mana Phoenix");
                                                Core.EnsureComplete(4482);
                                                Bot.Wait.ForPickup("Mana Token VIII");
                                            }
                                            Core.Logger("Token: Mana Token X");
                                            Core.EnsureAccept(4483);
                                            Core.GetMapItem(3672, map: "thirdspell");
                                            Core.EnsureComplete(4483);
                                            Bot.Wait.ForPickup("Mana Token IX");
                                        }
                                        Core.Logger("Token: Mana Token XI");
                                        Core.EnsureAccept(4484);
                                        Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated");
                                        Core.EnsureComplete(4484);
                                        Bot.Wait.ForPickup("Mana Token X");
                                    }
                                    Core.Logger("Token: Sun Token I");
                                    Core.EnsureAccept(4485);
                                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Phoenix Egg", 7);
                                    Core.EnsureComplete(4485);
                                    Bot.Wait.ForPickup("Mana Token XI");
                                }
                                Core.Logger("Token: Sun Token II");
                                Core.EnsureAccept(4486);
                                Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated Again");
                                Core.EnsureComplete(4486);
                                Bot.Wait.ForPickup("Sun Token I");
                            }
                            Core.Logger("Token: Sun Token III");
                            Core.EnsureAccept(4487);
                            Core.GetMapItem(3673, 3, "thirdspell");
                            Core.EnsureComplete(4487);
                            Bot.Wait.ForPickup("Sun Token II");
                        }
                        Core.Logger("Token: Sun Token IV");
                        Core.EquipClass(ClassType.Solo);
                        Core.EnsureAccept(4488);
                        Core.KillMonster("thirdspell", "r12", "Left", "Living Fire", "Sun Monster Ember", 15);
                        Core.EnsureComplete(4488);
                        Bot.Wait.ForPickup("Sun Token III");
                    }
                    Core.Logger("Token: Sun Token V");
                    Core.EnsureAccept(4489);
                    Core.EquipClass(ClassType.Solo);
                    Core.HuntMonster("thirdspell", "Sun Flare", "Sun Flare Defeated", 10);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("thirdspell", "r12", "Left", "Living Fire", "Living Fire Defeated", 5);
                    Core.EnsureComplete(4489);
                    Bot.Wait.ForPickup("Sun Token IV");
                }
                Core.Logger("Token: Sun Token VI");
                Core.EnsureAccept(4490);
                Core.GetMapItem(3674, map: "thirdspell");
                Core.EnsureComplete(4490);
                Bot.Wait.ForPickup("Sun Token V");
            }
            Core.Logger("Token: Heart of the Sun");
            Core.EnsureAccept(4491);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("thirdspell", "Solar Incarnation ", "Heart of the Sun Received");
            Core.EnsureComplete(4491);
            Bot.Wait.ForPickup("Heart of the Sun");
        }
    }
}
